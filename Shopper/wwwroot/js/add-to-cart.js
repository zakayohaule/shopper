$("#add-to-cart").click(function () {
    var qtyInput = $("#quantity");
    var priceInput = $("#price");
    var productInput = $("#products");
    var productNameText = $("#products option:selected");

    var price = parseInt(priceInput.val().replace(",", ""));
    var qty = qtyInput.val();
    var product = productInput.val();
    productName = productNameText.text();

    console.log(productName + " added to card");

    var markup = "<tr>" +
        "<td>" + productName + "</td>" +
        "<td>" + qty + "</td>" +
        "<td>" + price + "</td>" +
        "<td>" + price * qty + "</td>" +
        "</tr>";
    $("#sales-table").append(markup);

    qtyInput.val(null);
    priceInput.val(null);
    productInput.val(null);
    productInput.change();
});

// Find and remove selected table rows
$(".delete-row").click(function () {
    $("table tbody").find('input[name="record"]').each(function () {
        if ($(this).is(":checked")) {
            $(this).parents("tr").remove();
        }
    });
});
