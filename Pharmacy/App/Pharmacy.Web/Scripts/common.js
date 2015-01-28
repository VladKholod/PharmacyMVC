function changePrice() {
    var $price = $("#Price");

    var $dropDownListSelectedVal = parseInt($("#MedicamentId option:selected").val());

    $.ajax({
        url: "/OrderDetails/GetPrice",
        method: "GET",
        data: { medicamentId : $dropDownListSelectedVal },
        success: function (data) {
            $price.val(data.Price);
        },
        error: function() {
            alert("AJAX error!");
        }
    });
}

$(document).ready(function() {
    $("#MedicamentId").change(function() {
        changePrice();
    });
    changePrice();
});