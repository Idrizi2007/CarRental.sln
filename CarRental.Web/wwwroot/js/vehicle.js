// Card grid for the admin Vehicles screen.
//
// Rows come from GET /Admin/Vehicle/GetAll, which returns { data: [...] }.
// The controller must include VehicleImages in includeProperties, otherwise
// every card falls back to the "no photo" placeholder.

let allVehicles = [];

$(document).ready(function () {
    loadVehicles();

    // Filtering is client-side: the whole list is already here, so there is
    // no reason to go back to the server on every keystroke.
    $('#vehicleSearch').on('input', function () {
        renderVehicles(filterVehicles($(this).val()));
    });
});

function loadVehicles() {
    $.getJSON('/Admin/Vehicle/GetAll', function (response) {
        allVehicles = response.data || [];
        renderVehicles(allVehicles);
    });
}

function filterVehicles(term) {
    term = (term || '').trim().toLowerCase();
    if (term === '') {
        return allVehicles;
    }
    return allVehicles.filter(function (v) {
        return searchableText(v).indexOf(term) !== -1;
    });
}

// Everything a person might reasonably type into the box.
function searchableText(v) {
    const brand = v.vehicleModel && v.vehicleModel.brand ? v.vehicleModel.brand.name : '';
    const model = v.vehicleModel ? v.vehicleModel.name : '';
    const category = v.category ? v.category.name : '';
    return [v.registrationPlate, brand, model, category, v.year, v.color]
        .join(' ')
        .toLowerCase();
}

function renderVehicles(vehicles) {
    const grid = $('#vehicleGrid');
    grid.empty();

    $('#vehicleEmpty').prop('hidden', vehicles.length > 0);
    $('#vehicleCount').text(
        vehicles.length === allVehicles.length
            ? allVehicles.length + ' vehicles'
            : vehicles.length + ' of ' + allVehicles.length + ' vehicles'
    );

    vehicles.forEach(function (v) {
        grid.append(vehicleCard(v));
    });
}

function vehicleCard(v) {
    const brand = v.vehicleModel && v.vehicleModel.brand ? v.vehicleModel.brand.name : '';
    const model = v.vehicleModel ? v.vehicleModel.name : '';
    const title = escapeHtml((brand + ' ' + model).trim() || 'Unnamed vehicle');
    const category = v.category ? escapeHtml(v.category.name) : '';

    const upsertUrl = '/Admin/Vehicle/Upsert/' + v.id;
    const deleteUrl = '/Admin/Vehicle/Delete/' + v.id;

    return `
<div class="col-sm-6 col-lg-4 col-xxl-3">
  <div class="card h-100 ${v.isActive ? '' : 'opacity-75'}">
    ${coverPhoto(v, title)}
    <div class="card-body pb-2">
      <div class="d-flex justify-content-between align-items-start gap-2">
        <h2 class="h6 mb-1">${title}</h2>
        <span class="badge ${v.isActive ? 'bg-success' : 'bg-secondary'}">
          ${v.isActive ? 'Active' : 'Hidden'}
        </span>
      </div>
      <p class="text-muted small mb-2">
        ${escapeHtml(v.registrationPlate)} &middot; ${v.year}${category ? ' &middot; ' + category : ''}
      </p>
      ${priceLines(v)}
    </div>
    <div class="card-footer bg-transparent d-flex gap-2">
      <a href="${upsertUrl}" class="btn btn-sm btn-outline-secondary flex-fill">Edit</a>
      <button type="button" class="btn btn-sm btn-outline-danger flex-fill"
              onclick="confirmDelete('${deleteUrl}', '${escapeJsString(v.registrationPlate)}')">
        Delete
      </button>
    </div>
  </div>
</div>`;
}

// The cover photo is the image with the lowest SortOrder — the same rule the
// public catalogue will use. No IsPrimary flag to keep in sync.
function coverPhoto(v, title) {
    const images = v.vehicleImages || [];
    if (images.length === 0) {
        return `
<div class="d-flex align-items-center justify-content-center bg-light text-muted small"
     style="height: 180px;">
  No photo
</div>`;
    }

    const cover = images.reduce(function (lowest, img) {
        return img.sortOrder < lowest.sortOrder ? img : lowest;
    });

    const alt = cover.altText ? escapeHtml(cover.altText) : title;
    const extra = images.length > 1
        ? `<span class="badge bg-dark position-absolute top-0 end-0 m-2">${images.length} photos</span>`
        : '';

    return `
<div class="position-relative">
  <img src="${escapeHtml(cover.imageUrl)}" class="card-img-top" alt="${alt}"
       style="height: 180px; object-fit: cover;" />
  ${extra}
</div>`;
}

function priceLines(v) {
    const lines = [];
    if (v.isForRent) {
        lines.push('<span class="me-3"><strong>' + formatEuro(v.rentalPricePerDay) + '</strong> / day</span>');
    }
    if (v.isForSale) {
        lines.push('<span><strong>' + formatEuro(v.salePrice) + '</strong> to buy</span>');
    }
    if (lines.length === 0) {
        return '<p class="small text-muted mb-0">Not listed for rent or sale</p>';
    }
    return '<p class="small mb-0">' + lines.join('') + '</p>';
}

function formatEuro(value) {
    if (value === null || value === undefined) {
        return '&mdash;';
    }
    return '€' + Number(value).toLocaleString('en-IE', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
}

function escapeHtml(value) {
    return $('<div>').text(value === null || value === undefined ? '' : value).html();
}

// Plates go inside a single-quoted JS string in the onclick attribute.
function escapeJsString(value) {
    return String(value)
        .replace(/\\/g, '\\\\')
        .replace(/'/g, "\\'")
        .replace(/"/g, '&quot;');
}
