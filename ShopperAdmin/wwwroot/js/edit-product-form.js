var form = $('#edit-form');

var elements = form[0].elements;
for (var i = 0; i < elements.length; i++) {
    var element = elements[i];
    if (!"button, submit, reset".includes(element.type)) {
        if (element.id) {
            var label = $("label[for='" + element.id + "']");
            element.id += "-Edit";
            if (label != null) {
                label.attr("for", element.id);
            }
        }
    }
}

function openEditModal(dataUrl, actionUrl, title = null, size = null) {

    var elements = form[0].elements;
    $.ajax({
        type: "GET",
        url: dataUrl.toString(),
        success: function (response) {
            console.log(response);
            if (title) {
                $("#edit-title").html(title)
            }
            if (size) {
                $("#modal-dialog").addClass("modal-" + size)
            }
            $.each(response, function (key, val) {
                var $el = form.find('[name="' + key + '"]');
                var type = $el.attr('type');
                if ($el.is('select')) {
                    type = 'select';
                }
                console.log(type + " - " + key + " - " + val);
                if ($el.attr('hidden') != null) {
                    console.log("This element is hidden! " + key + " = " + val)
                    $el.attr("value", val);
                }
                switch (type) {
                    case 'checkbox':
                        if (val) {
                            $el.attr('checked', true);
                        } else {
                            $el.attr('checked', false)
                        }
                        break;
                    case 'radio':
                        $el.filter('[value="' + val + '"]').attr('checked', true);
                        break;
                    case 'number':
                        $el.val(val)
                        break;
                    case 'select':
                        if (key === 'ProductCategoryId') {
                            $el.prop("disabled", false)
                            for (var i = 0; i < response.CategoryItems.length; i++) {
                                var item = response.CategoryItems[i];
                                $el.append(new Option(item.Text, item.Value, false, item.Selected));
                            }
                        } else if(key === 'ProductTypeId'){
                            $el.prop("disabled", false)
                            for (var i = 0; i < response.TypeItems.length; i++) {
                                var item = response.TypeItems[i];
                                $el.append(new Option(item.Text, item.Value, false, item.Selected));
                            }
                        } else {
                            $el.val(val).trigger('change');
                        }
                        break;
                    default:
                        if (val) {
                            $el.val(val).change();
                        }
                }
            });
            form.attr("action", actionUrl);
            console.log(response);
            $("#edit-modal").modal("show");
        }
    });
}
