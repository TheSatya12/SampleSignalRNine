var connectionChat = new signalR.HubConnectionBuilder().withUrl("/hubs/chatCode").build();

document.getElementById("sendMessage").disabled = true;

connectionChat.on("MessageReceived", function (user, message) {
    console.log(`${user} - ${message}`);

    var li = document.createElement("li"); 
    li.textContent = `${user} - ${message}`;
    document.getElementById("messagesList").appendChild(li);
});

document.getElementById("sendMessage").addEventListener("click", function (event) {
    var Sender = document.getElementById("senderEmail").value.trim();
    var Message = document.getElementById("chatMessage").value.trim();
    var Receiver = document.getElementById("receiverEmail").value.trim();

    console.log(Receiver.length + " " + Receiver);

    if (Receiver && Receiver.length > 0) {
        connectionChat.send("SendMessageToReceiver", Sender, Receiver, Message)
            .catch(function (err) {
                console.error(err.toString());
            });
    } else {
        connectionChat.send("SendMessageToAll", Sender, Message)
            .catch(function (err) {
                console.error(err.toString());
            });
    }

    event.preventDefault();
});

connectionChat.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
    console.log("Connecting chat success");
}).catch(function (err) {
    console.log("Failed to connect: " + err);
});
