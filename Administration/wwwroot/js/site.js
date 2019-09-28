$(function () { 
    InitHeader()
    InitFooter()
})
function InitFooter() {
    var hfooter = $('footer').height()
    $('main').css('padding-bottom', hfooter + 'px')
}
function InitHeader() {
    var navHeight = $('.navbar').outerHeight()
    $('body').attr('style', 'padding-top:' + navHeight + 'px !important')
    $('.offcanvas-collapse').attr('style', 'top: ' + navHeight + 'px !important')
}
