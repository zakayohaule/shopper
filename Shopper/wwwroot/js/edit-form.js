var form = $('#edit-form');

var elements = form[0].elements;
for (var i = 0; i < elements.length; i++) {
    var element = elements[i];
    if (!"button, submit, reset".includes(element.type)) {
        if (element.id) {
            element.id += "-Edit";
            // console.log(element.id);
        }
    }
}

function openEditModal(dataUrl, actionUrl, title = null, size = null) {

    var elements = form[0].elements;
    $.ajax({
        type: "GET",
        url: dataUrl.toString(),
        success: function (response) {
            if (title) {
                $("#edit-title").html(title)
            }
            if (size) {
                $("#modal-dialog").addClass("modal-" + size)
            }
            $.each(response, function (key, val) {
                var $el = form.find('[name="' + key + '"]');
                var type = $el.attr('type');
                console.log(type + " - " + key + " - " + val);
                if ($el.is(":hidden")) {
                    console.log("This eleement is hidden! " + key + " = " + val)
                    $el.attr("value", val);
                }
                switch (type) {
                    case 'checkbox':
                        $el.attr('checked', 'checked');
                        break;
                    case 'radio':
                        $el.filter('[value="' + val + '"]').attr('checked', 'checked');
                        break;
                    case 'number':
                        $el.val(val)
                        break;
                    default:
                        $el.val(val).change();
                }
            });
            console.log("Editing form action " + actionUrl);
            form.attr("action", actionUrl);
            console.log(response);
            // $('.edit-select2bs4').select2({
            //     theme: 'bootstrap4'
            // })
            $("#edit-modal").modal("show");
        }
    });
}
