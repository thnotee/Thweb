// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function m_menu_open() {
    var menu = document.getElementById("m_menu");
    $("#m_menu").toggleClass("-translate-x-full");
};


function AddToCart(productId) {
    $.ajax({
        type: 'POST',
        url: "/Customer/Cart/AddToCart",
        data: { productId: productId },
        success: function (response) {
            if (response.seccess)
            {
                Swal.fire("장바구니 추가되었습니다.");
            }
        },
        error: function (xhr, status, error)
        {
            Swal.fire("에러가 발생했습니다. 잠시 후 다시 시도해주세요.");
        }
    });
}