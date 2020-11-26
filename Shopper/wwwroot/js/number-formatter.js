function formatNumber(event) {
    // skip for arrow keys
    if(event.keyCode >= 37 && event.keyCode <= 40) return;

    // format number
    $('#' + event.target.id).val(function(index, value) {
        return value
            .replace(/\D/g, "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    });
}

function formatDecimal(event){
    // skip for arrow keys
    if(event.keyCode >= 37 && event.keyCode <= 40) return;

    // format number
    $('#' + event.target.id).val(function(index, value) {
        return parseFloat(value).toLocaleString();
    });
}
