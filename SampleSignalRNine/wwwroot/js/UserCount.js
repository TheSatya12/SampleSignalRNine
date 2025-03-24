//Create Connecdtion
var connectionUserCount = new signalR.HubConnectionBuilder()
//.configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/userCount",signalR.HttpTransportType.ServerSentEvents).build();

//Connect to methods that hub invokes => receives notification from Hub

connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUserCounter");
    newCountSpan.innerText = value.toString();
});


//invoke hub method (send notification to hub)
function newWindowLoadOnClient() {
    connectionUserCount.invoke("NewWindowLoaded","Satya D").then((value)=>console.log(value));
}


//Start Connection
function fullFilled() {
    console.log("Connection to UserHub was Successfull");
    newWindowLoadOnClient();
    console.log("End");
}

function rejected() {
    console.log("Connection to UserHub was Failed");
}

connectionUserCount.start().then(fullFilled, rejected);

