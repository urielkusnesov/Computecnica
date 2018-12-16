$(document).ready(function(){
    $("#volver").on("click", function () {
        window.location.href = "/Home/Index";
    });

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

    $("#dialog-sure").hide();
    $("#send").on("click", function () {
        $("#dialog-sure").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                "Enviar": function () {
                    $(this).dialog("close");
                    $("#form").submit();
                },
                "Cancelar": function () {
                    $(this).dialog("close");
                }
            }
        });
    });
});