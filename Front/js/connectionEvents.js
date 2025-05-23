"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:8080/loghub").build();



connection.on("refreshLog", function(data) {

    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${data.value}`;
});

connection
    .start(() => console.log("connection on"))
    .catch(function(err) {
        return console.error(err.toString());
    });