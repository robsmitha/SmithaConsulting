function HandleKey(e) {
    if (e.keyCode && e.keyCode === 13) {
        Speak()
    }
}
function Speak() {
    var query = $('#utterance').val()
    if (query) {
        Loading(true)
        $.get('/Chat/Speak/', { query: query }, function (data) {
            $('#utterance').val('')
            Loading(false)
            List()
        })
    }
}
function List() {
    $.get('/Chat/List/', function (data) {
        $('#div_chat_list').html(data)
        var d = new Date();
        $('#timestamp').html(formatTime(d))
    })
}
function Loading(loading) {
    $('#ask').prop('disabled', loading)
    $('#utterance').prop('disabled', loading)
    if (loading) {
        $('#ask').html('<span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span><span class="sr-only">Loading...</span><span class="d-none d-sm-inline"> Thinking</span>')
    } else {
        $('#ask').html('Ask<span class="d-none d-sm-inline"> Away!</span>')
    }
}
function formatTime(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}
$(function () {
    $("#utterance").autocomplete({
        source: function (request, response) {
            let value = $(this.element).val()
            $.get('/Chat/Source/', { value: value }, function (data) {
                response(data)
            })
        },
        select: function (event, ui) {
            $('#utterance').val(ui.item.value)
            Speak()
        }
    });
    List()
})