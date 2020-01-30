

$(function () {
    ListRegister()
    LoadCart()
})

function LoadCart() {
    $.get('/Register/LoadCart/', function (data) {
        $('#div_cart').html(data)
    })
}

function ListRegister() {
    $.get('/Register/List/', function (data) {
        $('#div_list').html(data)
    })
}


function AddLineItem(id) {
    $('.line-item').prop('disabled', true)
    $('#add_item_id').val(id)
    var model = $('#form_add_item').serialize()
    $.ajax({
        url: '/Register/AddLineItem/',
        type: 'POST',
        data: model,
        success: function (data) {
            LoadCart()
            ListRegister()

        },
        error: function (request, error) {
            console.log("Request: " + JSON.stringify(request));
        }
    });
}


function RemoveItem(id) {
    $('#remove_item_id').val(id)
    var model = $('#form_remove_item').serialize()
    $.ajax({
        url: '/Register/RemoveItem/',
        type: 'POST',
        data: model,
        success: function (data) {
            LoadCart()
        },
        error: function (request, error) {
            console.log("Request: " + JSON.stringify(request));
        }
    });
}


function RemoveLineItem(id) {
    $('.quantity-counter').prop('disabled', true)
    $('#remove_line_item_id').val(id)
    var model = $('#form_remove_line_item').serialize()
    $.ajax({
        url: '/Register/RemoveLineItem/',
        type: 'POST',
        data: model,
        success: function (data) {
            LoadCart()
        },
        error: function (request, error) {
            console.log("Request: " + JSON.stringify(request));
        }
    });
}