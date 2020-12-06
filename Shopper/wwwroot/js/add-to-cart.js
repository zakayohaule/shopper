$("#add-to-cart").click(function () {
    var totalText = $("#total-amount");
    var qtyInput = $("#quantity");
    var priceInput = $("#price");
    var skuInput = $("#skus");
    var skuNameText = $("#skus option:selected");

    var price = parseInt(priceInput.val().replace(",", ""));
    var qty = qtyInput.val();
    var productTotal = qty * price;
    var sku = skuInput.val();
    skuName = skuNameText.text();
    console.log(skuName + " added to cart");

    var markup = "<tr>" +
        "<td>" + skuName + "</td>" +
        "<td>" + qty + "</td>" +
        "<td>" + price + "</td>" +
        "<td>" + productTotal+ "</td>" +
        "</tr>";
    $("#sales-table").append(markup);
    var currentTotal = parseInt(totalText.html());

    totalText.html(currentTotal + productTotal);

    qtyInput.val(null);
    priceInput.val(null);
    skuInput.val(null);
    skuInput.change();
    $('#edit-modal').modal('hide');
});

// Find and remove selected table rows
$(".delete-row").click(function () {
    $("table tbody").find('input[name="record"]').each(function () {
        if ($(this).is(":checked")) {
            $(this).parents("tr").remove();
        }
    });
});
