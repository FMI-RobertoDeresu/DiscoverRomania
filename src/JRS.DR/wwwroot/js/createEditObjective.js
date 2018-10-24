"use strict";

(function() {
    var defaultUrl = ($("#DefaultUrl").val() + "/").replace("//", "/");

    if ($("#createObjectiveForm").length === 0)
        return;

    var langId = parseInt($("#LanguageId").data("value"));
    var objTypeId = parseInt($("#ObjectiveTypeId").data("value"));

    $("#createObjectiveForm").submit(function(e) {
        e.preventDefault();
        $("#Html").val($("#summernote").summernote("code"));
        this.submit();
    });

    function getLanguages() {
        $.ajax({
            url: defaultUrl + "Admin/GetLanguages",
            type: "GET",
            data: {},
            dataType: "json",
            success: function(res) {
                for (let i = 0; i < res.length; i++) {
                    $("#LanguageId").append(`<option value=${res[i].id}>${res[i].name}</option>`);
                }
                if (langId) {
                    $("#LanguageId").val(langId);
                    getObjectiveTypes(langId);
                    langId = null;
                }
            },
            error: function() {
                toastr.error("A aparut o eroare");
            }
        });
    }

    $("#LanguageId").change(function() {
        const languageId = $("#LanguageId").val();
        getObjectiveTypes(languageId);
    });

    function getObjectiveTypes(languageId) {
        $("#ObjectiveTypeId option").remove();
        $("#ObjectiveTypeId").append('<option value="">Alegeti categoria obiectivului</option>');
        $.ajax({
            url: defaultUrl + "Admin/GetObjectiveTypes",
            type: "GET",
            data: { 'languageId': languageId },
            dataType: "json",
            success: function(res) {
                for (let i = 0; i < res.length; i++) {
                    $("#ObjectiveTypeId")
                        .append(`<option value=${res[i].id}>${res[i].name}</option>`);
                }
                if (objTypeId) { // on edit
                    $("#ObjectiveTypeId").val(objTypeId);
                    objTypeId = null;
                }
            },
            error: function() {
                toastr.error("A aparut o eroare");
            }
        });
    }

    getLanguages();
    $("#summernote").summernote({ height: 200 });
    $("#summernote").summernote("code", $("#Html").val());

})();