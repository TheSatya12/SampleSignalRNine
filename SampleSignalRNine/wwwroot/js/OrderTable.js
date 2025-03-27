var ConnectionOrderTable = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/OrderTable")
    .build();

ConnectionOrderTable.start().then(function () {
    console.log("ConnectionOrderTable Connected");
}, function () {
    console.log("failed to Connect");
});