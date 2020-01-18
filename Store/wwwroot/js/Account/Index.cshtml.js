
function Earnings(interval) {
    $.get('/Account/Earnings', { interval: interval }, function (data) {
        $('#earnings').html(data)
    })
}
function RevenueSources(interval) {
    $.get('/Account/RevenueSources', { interval: interval }, function (data) {
        $('#revenue_sources').html(data)
    })
}
$(function () {
    Earnings('yearly')
    RevenueSources('yearly')
})