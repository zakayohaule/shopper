function openEditModal(dataUrl, actionUrl, title = null, size = null) {
    var form = $('#form');
    var modalOriginTitle = $(".modal-title").html();
    var formOriginalAction = form.attr('action');
    $.ajax({
        type: "GET",
        url: dataUrl.toString(),
        success: function (response) {
            if (title) {
                $(".modal-title").html(title)
            }
            if (size) {
                $(".modal-dialog").addClass("modal-" + size)
            }
            $.each(response, function (key, val) {
                var $el = $('[name="' + key + '"]');
                var type = $el.attr('type');
                console.log(type + " - " + val);
                switch (type) {
                    case 'checkbox':
                        $el.attr('checked', 'checked');
                        break;
                    case 'radio':
                        $el.filter('[value="' + val + '"]').attr('checked', 'checked');
                        break;
                    default:
                        $el.val(val).change();
                }
            });
            console.log("Editing form action " + actionUrl);
            form.attr("action", actionUrl);
            console.log(response);
            $(".modal").modal("show");
        }
    });
    $(".modal").on("hidden.bs.modal", function () {
        $(".modal-title").html(modalOriginTitle);
        var elements = form[0].elements;
        for (var i = 0; i < elements.length; i++) {
            var element = elements[i];
            if (!"button, submit, reset, hidden".includes(element.type)) {
                if (element.type === "checkbox") {
                    element.prop("checked", false).change();
                } else if (element.type === "radio") {
                    element.prop("checked", false).change();
                    $('[name="' + element.name + '"]').val("").change();
                } else {
                    $('[name="' + element.name + '"]').val("").change();
                }
            }
        }
        console.log("Original form action " + formOriginalAction);
        form.attr("action", formOriginalAction);
    });
}
