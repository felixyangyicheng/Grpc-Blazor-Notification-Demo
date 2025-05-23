"use strict";
let connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:8080/loghub").build();

let dataArray = [];
let svg = d3.select("svg"),
    width = +svg.attr("width"),
    height = +svg.attr("height"),
    g = svg.append("g").attr("transform", "translate(32," + (height / 2) + ")");

function update(data) {
    let t = d3.transition()
        .duration(750);
    // JOIN new data with old elements.
    let circle = g.selectAll("circle")
        .data(data);
    // EXIT old elements not present in new data.
    circle.exit()
        .attr("class", "exit")
        .transition(t)
        .attr("cy", d => (d.data.value - 60))
        .style("fill-opacity", 1e-6)
        .remove();
    // UPDATE old elements present in new data.
    circle.attr("class", "update")
        .attr("cy", d => (d.data.value - 60))

    .attr("r", 5)
        .style("fill-opacity", 1)
        .transition(t)
        .attr("cx", function(d, i) { return i * 32; });
    // ENTER new elements present in new data.

    circle.enter().append("circle")
        .attr("class", "enter")
        //.attr("dy", ".35em")
        .attr("r", 5)
        .attr("cy", 60)
        .attr("cx", (d, i) => { return i * 32; })
        .style("fill-opacity", 1e-6)
        //.text((d) => { return d; })
        .transition(t)
        .attr("cy", d => (d.data.value - 60))
        .style("fill-opacity", 1);
    //console.log(data);

}

update(dataArray);
connection.on("refreshLog", function(data) {

    if (dataArray.length >= 20) {
        dataArray.push({
            data
        });
        dataArray = dataArray.slice(5, -(dataArray.length - (dataArray.length - 5)));
        update(dataArray);
    } else {
        dataArray.push({
            data
        });
        update(dataArray);
    }

});

connection
    .start(() => console.log("connection on"))
    .catch(function(err) {
        return console.error(err.toString());
    });