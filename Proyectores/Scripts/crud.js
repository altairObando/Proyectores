function PopupForm(url, titulo) {
    var formDiv = $('<div/>');
    $.get(url)
    .done(function (response) {
        formDiv.html(response);

        Popup = formDiv.dialog({
            autoOpen: true,
            resizable: false,
            title: titulo,
            height: 600,
            width: 600,
            close: function () {
                Popup.dialog('destroy').remove();
            }

        });
    });
}

function Delete(url) {
    if (confirm("Eliminar el registro?")) {
        $.ajax({
            type: "POST",
            url: url,
            success: function (data) {
                if (data.success) {
                    dataTable.ajax.reload();
                    Notificacion("warning", data.message, "pe-7s-check");
                }else
                    Notificacion("warning", data.message, "pe-7s-bell");
            },
            error: function(){
                Notificacion("danger", "Error: No se ha eliminado", "pe-7s-close");
        }

        });
    }
    else
    {
        Notificacion("info", "Sin cambios", "pe-7s-smile");
    }
}

function SubmitForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    Popup.dialog('close');
                    dataTable.ajax.reload();
                    Notificacion("success", data.message, "pe-7s-check");
                }
                else {
                    Notificacion("warning", data.message, "pe-7s-close");
                }
            }
        });
    }
    return false;
}