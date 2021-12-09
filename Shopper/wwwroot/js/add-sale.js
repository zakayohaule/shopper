var productOptions = $('#products').html();
var counter = 0;
var prices = [];
var discounts = [];
var sellingPrices = [];

$('#addProduct').on('click', function(event) {
  var counter = $('#sales-table tr').length - 2;
  event.preventDefault();

  var newRow = $('<tr>');
  var cols = '';

  // Table columns
  cols += '<td style="text-align: center"><select class="form-control select2bs4 product-select" name="Products[]" id="product-' +
      counter + '">' + productOptions +
      '</select></td>';
  cols += '<td style="text-align: center"><select class="form-control select2bs4 attribute-select" name="Attributes[]" id="attribute-' +
      counter + '"></select></td>';
  cols += '<td style="text-align: center"><input class="form-control rounded-0 total-amount-change" type="number" min="1" name="Quantities[]" value="1" id="quantity-' +
      counter + '"></td>';
  cols += '<td style="text-align: center"><input class="form-control rounded-0 total-amount-change" type="text" name="Prices[]" id="price-' +
      counter + '"></td>';
  cols += '<td style="text-align: center"><span id="total-' + counter +
      '"></td>';
  cols += '<td style="text-align: center"><button class="btn btn-danger rounded-0 delete-sale"><i class="fa fa-trash"></i></button</td>';

  // Insert the columns inside a row
  newRow.append(cols);

  // Insert the row inside a table
  $('#sales-table').append(newRow);
  // $('.select2bs4').select2({
  //           theme: 'bootstrap4',
  //         });

});

$('#sales-table').on('click', '.delete-sale', function(event) {
  $(this).closest('tr').remove();
  counter--;
});

$('#sales-table').on('change', '.product-select', function() {
  var productId = $(this).val();
  var counterNumber = $(this).attr('id');
  counterNumber = counterNumber.split('-')[1];
  clearOtherRelated(counterNumber);
  if (counterNumber) {
    $.ajax({
      type: 'GET',
      url: '/sales/' + productId + '/product-attributes',
      success: function(response) {
        var skuSelect = $('#attribute-' + counterNumber);
        skuSelect.html(response);
      },
    });
  }
});

$('#sales-table').on('change', '.attribute-select', function() {
  var skuId = $(this).val();
  var counterNumber = $(this).attr('id');
  counterNumber = counterNumber.split('-')[1];

  $.ajax({
    type: 'GET',
    url: '/sales/' + skuId + '/price',
    success: function(response) {
      var priceInput = $('#price-' + counterNumber);
      var totalSpan = $('#total-' + counterNumber);
      $('#quantity-' + counterNumber).val(1)
      var formattedPrice = numberWithCommas(response);
      priceInput.val(formattedPrice);
      totalSpan.html(formattedPrice);
      prices[counterNumber] = response;
    },
  });
});

$('#sales-table').on('change', '.total-amount-change', function() {
  var counterNumber = $(this).attr('id').split("-")[1];
  var quantityValue = $('#quantity-' + counterNumber).val();
  var price = $('#price-' + counterNumber).val().replace(",", "");
  if (price !== prices[counterNumber]){
    var discount = prices[counterNumber] - price;
    discounts[counterNumber] = discount*quantityValue;
  }
  sellingPrices[counterNumber] = price*quantityValue;
  var newTotal = price * quantityValue;
  $("#total-" + counterNumber).html(numberWithCommas(newTotal));
  var totalDiscount = discounts.reduce((pSum, d) => pSum + d);
  var totalAmount = sellingPrices.reduce((pSum, d) => pSum + d);
  $("#total-discount").html(numberWithCommas(totalDiscount)+"/=")
  $("#total-amount").html(numberWithCommas(totalAmount)+"/=")
});

function numberWithCommas(x) {
  return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
}

function clearOtherRelated(counterNumber) {
  $('#attribute-' + counterNumber).html('');
  $('#price-' + counterNumber).val(null);
  $('#total-' + counterNumber).html('');
  $('#quantity-' + counterNumber).val(null);
}
