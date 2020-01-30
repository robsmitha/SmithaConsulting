var CartModule = CartModule || (function () {
    // private
    var _args = {};

    function _addItem() {
        var model = _args.addItemForm.serialize()
        $.ajax({
            url: _args.addItemUrl,
            type: 'POST',
            data: model,
            success: function (data) {
                _args.onAddItemComplete(data)
            },
            error: function (request, error) {
                console.log("Request: " + JSON.stringify(request));
            }
        });
    }

    function _removeItem() {
        var model = _args.removeItemForm.serialize()
        $.ajax({
            url: _args.removeItemUrl,
            type: 'POST',
            data: model,
            success: function (data) {
                _args.onRemoveItemComplete(data)
            },
            error: function (request, error) {
                console.log("Request: " + JSON.stringify(request));
            }
        });
    }

    function _removeLineItem() {
        var model = _args.removeLineItemForm.serialize()
        $.ajax({
            url: _args.removeLineItemUrl,
            type: 'POST',
            data: model,
            success: function (data) {
                _args.onRemoveLineItemComplete(data)
            },
            error: function (request, error) {
                console.log("Request: " + JSON.stringify(request));
            }
        });
    }

    return {
        init: function (Args) {
            _args = Args;
        },
        addItem: function () {
            _addItem()
        },
        removeItem: function () {
            _removeItem()
        },
        removeLineItem: function () {
            _removeLineItem()
        }
    };
}());
