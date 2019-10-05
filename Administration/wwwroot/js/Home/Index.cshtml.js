
function Earnings(interval) {
    $.get('/Home/Earnings', { interval: interval }, function (data) {
        $('#earnings').html(data)
    })
}
function RevenueSources(interval) {
    $.get('/Home/RevenueSources', { interval: interval }, function (data) {
        $('#revenue_sources').html(data)
    })
}
$(function () {
    Earnings('yearly')
    RevenueSources('yearly')
})