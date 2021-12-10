$(document).ready(function () {
  $(".updatecartitem").click(function (event) {
    event.preventDefault();
    var itemid = $(this).attr("data-itemid");
    var quantity = $("#Quantity-" + itemid).val();
    $.ajax({
      type: "POST",
      url: "@Url.RouteUrl('updatecart')",
      data: {
        itemid: itemid,
        quantity: quantity,
      },
      success: function (result) {
        window.location.href = "@Url.RouteUrl('cart')";
      },
    });
  });
});
