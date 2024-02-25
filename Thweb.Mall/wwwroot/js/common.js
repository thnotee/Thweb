// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function checkValue(value) {
    if (value === null || value === "" || value === undefined) {
        return true;
    } else {
        return false;
    }
}


function AddToCart(productId) {
    $.ajax({
        type: 'post',
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
            console.log(xhr);
            console.log(status);
            console.log(error);
        }
    });
}

/**
 * 천의 자리마다 . 찍어서 반환합니다. 1000 => 1,000
 * @param {any} number
 * @returns
 */
function formatNumberWithoutDecimal(number) {
    // 숫자를 문자열로 변환합니다.
    const numberStr = number.toString();
    // 정규 표현식을 사용하여 천 단위로 쉼표를 삽입합니다.
    const formattedNumber = numberStr.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return formattedNumber;
}

/**
 * 전화번호 형식 반환
 * @param {any} inVal
 * @returns
 */
function formatPhoneNumObj(Obj) {
    var retVal = Obj.value.replace(/[^0-9]/g, "").replace(/(^02|^0505|^1[0-9]{3}|^0[0-9]{2})([0-9]+)?([0-9]{4})/, '$1-$2-$3').replace("--", "-");
    Obj.value = retVal;
}