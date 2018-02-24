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

                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    })

                }
            },
            error: function(){
                alert("No se pudo eliminar");
        }

        });
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

                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    })

                }
            }
        });
    }
    return false;
}