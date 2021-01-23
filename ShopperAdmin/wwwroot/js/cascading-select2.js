/**
 * A Javascript module to loadeding/refreshing options of a select2 list box using ajax based on selection of another select2 list box.
 *
 * @url : https://gist.github.com/ajaxray/187e7c9a00666a7ffff52a8a69b8bf31
 * @auther : Anis Uddin Ahmad <anis.programmer@gmail.com>
 *
 * Live demo - https://codepen.io/ajaxray/full/oBPbQe/
 * w: http://ajaxray.com | t: @ajaxray
 */
var Select2Cascade = ( function(window, $) {

    function Select2Cascade(parent, child, url, select2Options) {
        var afterActions = [];
        var options = select2Options || {};

        // Register functions to be called after cascading data loading done
        this.then = function(callback) {
            afterActions.push(callback);
            return this;
        };

        parent.select2(select2Options).on("change", function (e) {

            child.prop("disabled", true);
            var _this = this;

            $.getJSON(url.replace(':parentId:', $(this).val()), function(items) {
                var newOptions = '<option value="">-- Select --</option>';
                for(var id in items) {
                    newOptions += '<option value="'+ id +'">'+ items[id] +'</option>';
                }

                child.select2('destroy').html(newOptions).prop("disabled", false)
                    .select2(options);

                afterActions.forEach(function (callback) {
                    callback(parent, child, items);
                });
            });
        });
    }

    return Select2Cascade;

})( window, $);

function populateChildSelect(parentName, childName, url, selectPlaceholder = null, parentPlaceHolder = null, grandChildName=null)
{

    $('[name=' + parentName + ']').on("select2:select", function (event) {

        var parentId = $(this).val();
        var placeHolder = "-- Select --";
        if (selectPlaceholder)
        {
            if (parentId){
                placeHolder = "-- Select " + selectPlaceholder + " --";
            }
            else if(parentPlaceHolder){
                placeHolder = "-- Select " + parentPlaceHolder + " First --";
            }
        }
        var childSelect = $('[name=' + childName + ']');
        childSelect.empty();
        childSelect.append($('<option/>', {
            value: "",
            text: placeHolder
        }));
        childSelect.prop("disabled", true);
        if (grandChildName){
            var grandChildSelect = $('[name=' + grandChildName + ']');
            grandChildSelect.empty();
            grandChildSelect.append($('<option/>', {
                value: "",
                text: "-- Select " + selectPlaceholder + " First --"
            }));
            grandChildSelect.prop("disabled", true);
        }
        if (parentId)
        {
            $.ajax({
                url: url.replace(':parentId:', parentId),
                type: 'GET',
                cache: false,
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (result) {
                    childSelect.prop("disabled", false)
                    console.log(result);
                    var markup;
                    for (var i = 0; i < result.length; i++) {
                        var item = result[i];
                        childSelect.append(new Option(item.text, item.value, false, item.selected));
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(thrownError);
                }
            });
        }
    })
}
