$(function () {
    var hfooter = $('footer').height()
    $('main').css('padding-bottom', hfooter + 'px')
    InitSolomentoUno()
})
function InitSolomentoUno() {
    var navHeight = $('.navbar').outerHeight()
    $('body').attr('style', 'padding-top:' + navHeight + 'px !important')
    $('.offcanvas-collapse').attr('style', 'top: ' + navHeight + 'px !important')

    $('[data-toggle="offcanvas"]').on('click', function () {
        $('.offcanvas-collapse').toggleClass('open')
    })
}
function ShowLoadingMessage(elem, message) {
    var html = '<span class="spinner-grow spinner-grow-sm" role="status"></span><span class="sr-only">Loading...</span> '
    html = message ? html + message : html
    $(elem).html(html);
}
