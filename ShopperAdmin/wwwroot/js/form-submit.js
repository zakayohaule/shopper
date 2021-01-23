function submitForm(formId, data, url, redirectUrl) {
    var form = $('#'+formId);
    form.on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            type: 'post',
            url: url,
            data: data,
            success: function () {
                alert("Redirect to: " + redirectUrl);
            }
        });

    });
}
