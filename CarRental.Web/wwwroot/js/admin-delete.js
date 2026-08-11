// Shared delete flow for the admin lookup screens.
// Confirms with SweetAlert2, then calls the DELETE endpoint.
//
// The endpoint reports the outcome with the HTTP status code:
//   200 OK        -> deleted
//   400 Bad Req   -> id missing or invalid
//   404 Not Found -> no such row
//   409 Conflict  -> row is referenced and cannot be deleted (added once Vehicle exists)
//
// The body carries only a message, for both success and failure.
function confirmDelete(url, itemName) {
    Swal.fire({
        title: 'Delete ' + (itemName || 'this item') + '?',
        text: "This can't be undone.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Yes, delete it',
        cancelButtonText: 'Cancel'
    }).then(function (result) {
        if (!result.isConfirmed) {
            return;
        }

        fetch(url, {
            method: 'DELETE',
            headers: { 'Accept': 'application/json' }
        })
            .then(function (response) {
                // Read the body whatever the status — it holds the message either way.
                // If the response isn't JSON (e.g. an unhandled 500), fall back to an empty object.
                return response.json()
                    .catch(function () { return {}; })
                    .then(function (body) {
                        return { ok: response.ok, body: body };
                    });
            })
            .then(function (result) {
                if (!result.ok) {
                    toastr.error(result.body.message || 'Delete failed.');
                    return;
                }

                toastr.success(result.body.message || 'Deleted.');
                // The table is server-rendered, so reload to show the change.
                setTimeout(function () { location.reload(); }, 700);
            })
            .catch(function () {
                // Network-level failure: the request never got a response.
                toastr.error('Could not reach the server.');
            });
    });
}
