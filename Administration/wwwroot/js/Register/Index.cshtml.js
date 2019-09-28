

$(function () {
    var currentOrderId = $('.current-order-id').val();
    ListRegister(currentOrderId)
    LoadCart(currentOrderId)
})

function LoadCart(orderId) {
    $.get('/Register/LoadCart/', { orderId: orderId }, function (data) {
        $('#div_cart').html(data)
    })
}

function ListRegister(orderId) {
    $.get('/Register/List/', { orderId: orderId }, function (data) {
        $('#div_list').html(data)
    })
}

function EditRegister(itemId, orderId) {
    $.get('/Register/Edit/', { itemId: itemId, orderId: orderId }, function (data) {
        $('#div_edit_register').html(data)
    })
}

function SaveRegister() {
    var model = $('#form_edit_register').serialize()
    $.ajax({
        url: '/Register/Edit/',
        type: 'POST',
        data: model,
        success: function (data) {
            console.log(data)
            $('#modal_edit_register').modal('hide')
            LoadCart(data.orderId)
            ListRegister(data.orderId)
            ShowFeedbackMessage(String(data.success))
        },
        error: function (request, error) {
            console.log("Request: " + JSON.stringify(request));
        }
    });
}


function AddLineItem(id, edit) {
    $('.line-item').prop('disabled', true)
    $('#add_item_id').val(id)
    var model = $('#form_add_item').serialize()
    $.ajax({
        url: '/Register/AddLineItem/',
        type: 'POST',
        data: model,
        success: function (data) {
            console.log(data)
            $('.current-order-id').val(data.orderId)
            if (edit) {
                EditRegister(id, data.orderId)
            }
            else {
                $('#modal_edit_register').modal('hide')
            }
            LoadCart(data.orderId)
            ListRegister(data.orderId)

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
            console.log(data)
            $('#modal_edit_register').modal('hide')
            LoadCart(data.orderId)
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
            console.log(data)
            LoadCart(data.orderId)
            EditRegister(id, data.orderId)
        },
        error: function (request, error) {
            console.log("Request: " + JSON.stringify(request));
        }
    });
}