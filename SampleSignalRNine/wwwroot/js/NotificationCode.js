var connectionNotification = new signalR.HubConnectionBuilder().withUrl("/hubs/notificationCode").build();

document.getElementById("sendButton").disabled = true;

connectionNotification.on("LoadNotification", function (messages, counter) {
    document.getElementById("messageList").innerHTML = "";
    var NotificationCounter = document.getElementById("notificationCounter");
    NotificationCounter.innerHTML = `(${counter})`
    for (let i = messages.length - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = "Notification -" + messages[i];
        document.getElementById("messageList").appendChild(li);
        console.log("for");
    }
    console.log("Done");
});

document.getElementById("sendButton").addEventListener("click",
    function (event) {
        var message = document.getElementById("notificationInput").value;

        connectionNotification.send("SendMessage", message).then(function () {
            document.getElementById("notificationInput").value = "";
            console.log("SendMessage done");
        });

        event.preventDefault();
    });

function failed() {
    console.log("NotificationCode signalR connection was failed");

}

connectionNotification.start().then(function () {
    connectionNotification.send("LoadMessages");
    document.getElementById("sendButton").disabled = false;

}, failed);