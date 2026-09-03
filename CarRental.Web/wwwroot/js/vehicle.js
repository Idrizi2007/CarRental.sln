// DataTables grid for the admin Vehicles screen.
// Rows come from GET /Admin/Vehicle/GetAll, which returns { data: [...] }.
// Each row carries its related objects because GetAll passes
// includeProperties: "VehicleModel.Brand,Category,FuelType,TransmissionType".

$(document).ready(function () {
    $('#tblVehicles').DataTable({
        ajax: {
            url: '/Admin/Vehicle/GetAll',
            dataSrc: 'data'
        },
        columns: [
            { data: 'registrationPlate', width: '12%' },
            {
                // "Volkswagen Golf (2019)" — brand comes from the two-level include.
                data: null,
                width: '25%',
                render: function (row) {
                    var brand = row.vehicleModel && row.vehicleModel.brand
                        ? row.vehicleModel.brand.name
                        : '';
                    var model = row.vehicleModel ? row.vehicleModel.name : '';
                    return escapeHtml((brand + ' ' + model).trim() + ' (' + row.year + ')');
                }
            },
            {
                data: 'category.name',
                width: '13%',
                defaultContent: '<span class="text-muted">&mdash;</span>'
            },
            {
                data: 'rentalPricePerDay',
                width: '12%',
                render: function (value, type, row) {
                    if (!row.isForRent) {
                        return '<span class="text-muted">not for rent</span>';
                    }
                    return formatEuro(value);
                }
            },
            {
                data: 'salePrice',
                width: '13%',
                render: function (value, type, row) {
                    if (!row.isForSale) {
                        return '<span class="text-muted">not for sale</span>';
                    }
                    return formatEuro(value);
                }
            },
            {
                data: 'isActive',
                width: '10%',
                render: function (isActive) {
                    return isActive
                        ? '<span class="badge bg-success">Yes</span>'
                        : '<span class="badge bg-secondary">No</span>';
                }
            },
            {
                data: null,
                width: '15%',
                orderable: false,
                searchable: false,
                render: function (row) {
                    var upsertUrl = '/Admin/Vehicle/Upsert/' + row.id;
                    var deleteUrl = '/Admin/Vehicle/Delete/' + row.id;
                    return '<a href="' + upsertUrl + '" class="btn btn-sm btn-secondary">Edit</a> ' +
                        '<button type="button" class="btn btn-sm btn-danger" ' +
                        'onclick="confirmDelete(\'' + deleteUrl + '\', \'' +
                        escapeJsString(row.registrationPlate) + '\')">Delete</button>';
                }
            }
        ],
        order: [[0, 'asc']],
        pageLength: 15,
        language: {
            search: 'Search vehicles:',
            emptyTable: 'No vehicles yet. Add one to get started.'
        }
    });
});

function formatEuro(value) {
    if (value === null || value === undefined) {
        return '<span class="text-muted">&mdash;</span>';
    }
    return '€' + Number(value).toLocaleString('en-IE', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
}

function escapeHtml(value) {
    return $('<div>').text(value).html();
}

// Plates go inside a single-quoted JS string in the onclick attribute.
function escapeJsString(value) {
    return String(value)
        .replace(/\\/g, '\\\\')
        .replace(/'/g, "\\'")
        .replace(/"/g, '&quot;');
}
