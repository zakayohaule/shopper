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


function fillPriceField(val)
{
    if (val){
        console.log("new sku selected")
        console.log(val)
        var price = $('#price-for-'+val).text();
        var discount = $('#discount-for-'+val).text();
        console.log("the discount is: " + discount)
        if (discount !== "0"){
            $('#maximum-discount').text("Maximum discount: " + discount + "/-");
        } else {
            $('#maximum-discount').text("")
        }
        console.log("Product's price is: " + price)
        $('#price').val(Number(price).toLocaleString());
    }else {
        $('#maximum-discount').text("")
    }
}
