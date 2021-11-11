// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Load Data and Pagination ...
let lastC = 1
let lastO = 1
let lastCat = ''
let lastOrder = ''
let catCount = 0
let orderCount = 0
const catPage = document.getElementById('cat-page')
const orderPage = document.getElementById('order-page')
catPage.style.visibility = 'hidden'
orderPage.style.visibility = 'hidden'
function loadByCat(cat, page = 1) {
    if (page == 1)
        lastC = 1
    lastCat = cat
    fetch("/home/categories/" + cat + '/' + page, {
        method: 'GET',
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('display-by-categorie').innerHTML = data
            document.getElementById('categorie').innerText = cat
            catCount = document.getElementById('display-by-categorie').childElementCount
            document.getElementById('categorie-count').innerText = catCount + '  '
            catPage.style.visibility = 'visible'
        })
        .catch(error => console.error('Unable to add item.', error));
}

function loadByOrder(order, page = 1) {
    if (page == 1)
        lastO = 1
    lastOrder = order
    fetch("/home/orderby/" + order + '/' + page, {
        method: 'GET',
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('display-by-order').innerHTML = data
            document.getElementById('order-by').innerText = order
            orderCount = document.getElementById('display-by-order').childElementCount
            document.getElementById('order-count').innerText = orderCount + '  '
            orderPage.style.visibility = 'visible'
        })
        .catch(error => console.error('Unable to add item.', error));
}

function loadNextPage(type) {
    switch (type) {
        case 'cat':
            if (catCount < 9)
                return
            lastC++
            loadByCat(lastCat, lastC)
            break;
        case 'order':
            if (orderCount < 9)
                return
            lastO++
            loadByOrder(lastOrder, lastO)
            break;
    }
}
function loadLastPage(type) {
    switch (type) {
        case 'cat':
            if (lastC == 1)
                return
            lastC--
            loadByCat(lastCat, lastC)
            break;
        case 'order':
            if (lastO == 1)
                return
            lastO--
            loadByOrder(lastOrder, lastO)
            break;
    }
}
// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Modal Detail

function displayDetailModal(attID) {
    fetch("/home/ObjectsDetail?attID=" + attID, {
        method: 'GET',
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('detailModal').innerHTML = data
            $('#detailModal').modal('show')
        })
        .catch(error => console.error('Unable to add item.', error));
}

// ---------------------------------------------------------------------------------------------------------------------------------------------------
// DropDown Login From
let openOrClose = false
const dropDown = document.getElementById('dropdown')
document.getElementById('dropdown-btn').onclick = function () {
    if (openOrClose) { // Close Form
        dropDown.classList.remove('open')
    } else { // Open Form
        dropDown.classList.add('open')
    }
    openOrClose = !openOrClose
}
// ---------------------------------------------------------------------------------------------------------------------------------------------------
// add to Basket

function addToBasket(productID) {
    fetch("/home/Basket?productID=" + productID, {
        method: 'POST',
    })
        .then(
            alert('added')
        )
        .catch(error => console.error('Unable to add item.', error));
}

// remove from Basket

function removeFromBasket(productID) {
    fetch("/home/Payment?productID=" + productID, {
        method: 'GET',
    })
        .then( response => response.text()
            
    ).then(data => loadBasket(false))
        .catch(error => console.error('Unable to add item.', error));
}

// Payment from Basket (check discountCode)

function paymentDiscountCode() {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    const tokenField = $('#discountCode')
    const price = $('#price').val()
    $.ajax({
        url: "/home/DiscountCode/",
        method: "POST",
        dataType: 'json',
        data: {
            __RequestVerificationToken: token,
            discountCode: tokenField.val(),
            price: price,
        },
        success: function (data) {
            if (data.resualt) {
                tokenField.css("border-color", "#147300")
                $('#total-price').text(convertToPriceFormat(data.price))
            }
            else {
                tokenField.css("border-color", "#f00")
                $('#total-price').text(convertToPriceFormat(data.price))
            }
        },
    })
}

// Payment from Basket

function payment(discountCode) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    fetch("/home/Payment/", {
        method: 'POST',
        data: {

        },
    })
        .then(
            $('#detailModal').modal('toggle')
        )
        .catch(error => console.error('Unable to add item.', error));
}

// ---------------------------------------------------------------------------------------------------------------------------------------------------
// need
// Conver currency
function convertToPriceFormat(price) {
    var formated = ''
    var counter = 0
    price = price.toString()
    price = price.split('.')
    float = price[1]
    price = price[0]
    price = price.split('')
    price = price.reverse()
    price = price.join('')
    const len = price.length
    for (const i in price) {
        counter += 1
        formated += price[i]
        if (counter % 3 === 0 && parseInt(i) + 1 != len)
            formated += ','
    }
    return ((formated.split('')).reverse()).join('') + '.' + float
}
// Load from Basket
function loadBasket(displayModal = true) {
    return fetch("/home/Basket", {
        method: 'GET',
    })
        .then(response => response.text())
        .then(data => {
            document.getElementById('basketModal').innerHTML = data
            if (displayModal)
                $('#basketModal').modal('show')
        })
        .catch(error => console.error('Unable to add item.', error))
}