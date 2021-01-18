
$('.product-image-thumb').on('click', function () {
    var image_element = $(this).find('img');
    var image_delete_link = $(this).find('.delete-image');
    var image_edit_on_click = $(this).find('.edit-image');
    console.log(image_delete_link);
    console.log(image_edit_on_click);
    console.log($(image_edit_on_click).attr('onclick'));
    var imageSrc = $(image_element).attr('src').replace("/thumbs","");
    console.log(imageSrc);
    $('.product-image').prop('src', imageSrc);
    $('#delete-button').prop('href', $(image_delete_link).attr('href'));
    $('#edit-button').attr('onclick', $(image_edit_on_click).attr('onclick'));
    $('.product-image-thumb.active').removeClass('active');
    $(this).addClass('active');
})

function openImageEditModal(actionUrl, title = null) {
    if (title) {
        $("#edit-image-title").html(title)
    }
    $('#edit-image-action').attr("action", actionUrl);
    $("#edit-image-modal").modal("show");
}

function filePreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#product-photo-form + img').remove();
            $('#product-photo-form').after('<img src="'+e.target.result+'" width="500" height="380"/>');
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$("#product-photo").change(function () {
    filePreview(this);
});
