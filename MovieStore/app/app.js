var hub = $.connection.chatHub;
var chatHistory = $('.chat-history');

hub.client.message=function(you,msg){
    $("#message").append('<li><div class="message-data"><span class="message-data-name"><i class="fa fa-circle online"></i> ' + you + '</span></div><div class="message my-message">'
        + msg + ' </div></li>');
}

hub.client.user = function (
    msg) {
    $("#user").append('<li class="clearfix"> <div class="about"><div class="name">' +
        msg + '</div><div class="status"><i class="fa fa-circle online"></i> online</div></div></li>');
}

$.connection.hub.start(function (){
    $("#send").click(function(){
        hub.server.send($("#message-to-send").val());
        chatHistory.scrollTop(chatHistory[0].scrollHeight);
        $("#message-to-send").val(" ");
    })
})