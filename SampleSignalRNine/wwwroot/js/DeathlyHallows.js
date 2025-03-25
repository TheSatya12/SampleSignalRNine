var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
var wandSpan = document.getElementById("wandCounter");

//Create Connecdtion
var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/deathlyHallows").build();

//Connect to methods that hub invokes => receives notification from Hub

connectionDeathlyHallows.on("updateDeathlyHallowCount", (cloak,stone,wand) => {
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
    console.log(cloak.toString() + " " + stone.toString() + " " + wand.toString());
});
 
//Start Connection
function fullFilled() {
    connectionDeathlyHallows.invoke("GetRaceStatus").then((raceCounter) => {
        cloakSpan.innerText = raceCounter.cloak.toString();
        stoneSpan.innerText = raceCounter.stone.toString();
        wandSpan.innerText = raceCounter.wand.toString();
    })
    console.log("Connection to DeathlyHallows was Successfull");
    console.log("End");
}

function rejected() {
    console.log("Connection to DeathlyHallows was Failed");
}

connectionDeathlyHallows.start().then(fullFilled, rejected);


