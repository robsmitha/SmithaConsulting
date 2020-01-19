
$.fn.ListComponent = function (options) {

    var $this = $(this)

    // Default options
    var settings = $.extend({
        loadingHtml: function () {
            return '<span class="h2 font-weight-light">Loading </span>' +
                '<div class="spinner-border text-dark" role="status">' +
                '<span class="sr-only">Loading...</span>' +
                '</div>';
        },
        onComplete: function (data) { console.log(data) }
    }, options);

    function _list(args) {
        $this.html(settings.loadingHtml);
        $.get(settings.url, args, function (data) {
            $this.html(data)
            settings.onComplete(data)
        })
    }

    _list(settings.defaultArgs)

    return {
        list: function (args) {
            _list(args)
        }
    }
}