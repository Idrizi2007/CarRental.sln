// DataTables grid for the admin Models screen.
// Rows come from GET /Admin/VehicleModel/GetAll, which returns { data: [...] }.
// Each row includes its Brand, because GetAll passes includeProperties: "Brand".

$(document).ready(function () {
    $('#tblVehicleModels').DataTable({
        ajax: {
            url: '/Admin/VehicleModel/GetAll',
            dataSrc: 'data'
        },
        columns: [
            { data: 'name', width: '30%' },
            {
                // The nested Brand object arrives because of the Include.
                // Without it this would be null and the column would be empty.
                data: 'brand.name',
                width: '30%',
                defaultContent: '<span class="text-muted">&mdash;</span>'
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
                width: '25%',
                orderable: false,
                searchable: false,
                render: function (row) {
                    var upsertUrl = '/Admin/VehicleModel/Upsert/' + row.id;
                    var deleteUrl = '/Admin/VehicleModel/Delete/' + row.id;
                    return '<a href="' + upsertUrl + '" class="btn btn-sm btn-secondary">Edit</a> ' +
                        '<button type="button" class="btn btn-sm btn-danger" ' +
                        'onclick="confirmDelete(\'' + deleteUrl + '\', \'' + escapeJsString(row.name) + '\')">' +
                        'Delete</button>';
                }
            }
        ],
        order: [[0, 'asc']],
        pageLength: 15,
        language: {
            search: 'Search models:',
            emptyTable: 'No models yet. Add one to get started.'
        }
    });
});

// Names go inside a single-quoted JS string in the onclick attribute,
// so quotes and backslashes have to be escaped or the attribute breaks.
function escapeJsString(value) {
    return String(value)
        .replace(/\\/g, '\\\\')
        .replace(/'/g, "\\'")
        .replace(/"/g, '&quot;');
}
