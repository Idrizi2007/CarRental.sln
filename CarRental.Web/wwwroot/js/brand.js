// DataTables grid for the admin Brands screen.
// Rows come from GET /Admin/Brand/GetAll, which returns { data: [...] }.
// Search, sorting and paging are handled by DataTables in the browser.

$(document).ready(function () {
    $('#tblBrands').DataTable({
        ajax: {
            url: '/Admin/Brand/GetAll',
            // The endpoint wraps the array in a "data" property, which is what
            // DataTables looks for by default.
            dataSrc: 'data'
        },
        // Note: JSON property names arrive camelCase (name, logoUrl, isActive),
        // even though the C# properties are PascalCase.
        columns: [
            { data: 'name', width: '35%' },
            {
                data: 'logoUrl',
                width: '20%',
                orderable: false,
                render: function (logoUrl) {
                    if (!logoUrl) {
                        return '<span class="text-muted">&mdash;</span>';
                    }
                    return '<img src="' + escapeHtml(logoUrl) + '" alt="" style="height:24px" />';
                }
            },
            {
                data: 'isActive',
                width: '15%',
                render: function (isActive) {
                    return isActive
                        ? '<span class="badge bg-success">Yes</span>'
                        : '<span class="badge bg-secondary">No</span>';
                }
            },
            {
                data: null,
                width: '30%',
                orderable: false,
                searchable: false,
                render: function (row) {
                    var editUrl = '/Admin/Brand/Upsert/' + row.id;
                    var deleteUrl = '/Admin/Brand/Delete/' + row.id;
                    return '<a href="' + editUrl + '" class="btn btn-sm btn-secondary">Edit</a> ' +
                        '<button type="button" class="btn btn-sm btn-danger" ' +
                        'onclick="confirmDelete(\'' + deleteUrl + '\', \'' + escapeJs(row.name) + '\')">' +
                        'Delete</button>';
                }
            }
        ],
        order: [[0, 'asc']],
        pageLength: 15,
        language: {
            search: 'Search brands:',
            emptyTable: 'No brands yet.'
        }
    });
});

// Brand names are shown inside HTML, so escape them.
function escapeHtml(value) {
    return $('<div>').text(value).html();
}

// Names are also passed inside a single-quoted JS string in the onclick attribute,
// so quotes and backslashes have to be escaped or the attribute breaks.
function escapeJs(value) {
    return String(value)
        .replace(/\\/g, '\\\\')
        .replace(/'/g, "\\'")
        .replace(/"/g, '&quot;');
}
