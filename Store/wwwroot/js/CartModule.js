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

    return {
        init: function (Args) {
            _args = Args;
        },
        addItem: function () {
            _addItem()
        }
    };
}());
