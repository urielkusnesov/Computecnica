$(document).ready(function () {
    $("#dialog-confirm").hide();

    $('.remove').on('click', function () {
        var removeId = $(this).data().id;
        $("#dialog-confirm").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                "Confirmar": function () {
                    location.href = $('#removeUrl').val() + removeId
                },
                "Cancelar": function () {
                    $(this).dialog("close");
                }
            }
        });
    });
});

