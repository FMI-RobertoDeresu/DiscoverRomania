(function() {
    var objectiveId;

    function openDeleteObjectiveModal(objId) {
        objectiveId = objId;
        $("#confirmDeleteObjectiveModal").modal("show");
    }

    function deleteObjective() {
        $.ajax({
            url: `/Admin/Delete/${objectiveId}`,
            type: "DELETE",
            contentType: "application/json",
            success: function(res) {
                if (res.isError === true) {
                    toastr.error(res.messages[0]);
                    return;
                }
                toastr.success("Obiectivul a fost sters!");
                location.reload();
            },
            error: function() {
                toastr.error("A aparut o eroare!");
            }
        });
    }

    window.openDeleteObjectiveModal = openDeleteObjectiveModal;
    window.deleteObjective = deleteObjective;

})();