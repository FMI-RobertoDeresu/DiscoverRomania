"use strict";

(function() {

    var objectiveId;

    function openDeleteObjectiveModal(objId) {
        objectiveId = objId;
        $("#confirmDeleteObjectiveModal").modal("show");
    }

    function deleteObjective() {
        $.ajax({
            url: "/admin/delete",
            type: "delete",
            data: { objectiveId: objectiveId },
            success: function(res) {
                if (res.isError === undefined) {
                    toastr.error("A aparut o eroare!");
                    return;
                }

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