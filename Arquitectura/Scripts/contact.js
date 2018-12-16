$(document).ready(function () {
    $("#dialog-confirm").hide();
    if ($("#result").val() == "ok") {
        $("#dialog-confirm").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                "Ok": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
});