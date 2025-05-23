let test = [{
    x: 5,
    y: 6
}, { x: 7, y: 8 }, { x: 9, y: 10 }, { x: 11, y: 12 }]
console.log(test)
console.log(test.slice(0, 1))

let dataArray = [{
    x: 5,
    y: 6
}];
var svg = d3.select("svg"),
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
        .attr("cy", d => (d.y - 60))
        .style("fill-opacity", 1e-6)
        .remove();
    // UPDATE old elements present in new data.
    circle.attr("class", "update")
        .attr("cy", d => (d.y - 60))

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
        .attr("cy", d => (d.y - 60))
        .style("fill-opacity", 1);
    //console.log(data);

}

update(dataArray);

d3.interval(() => {
    if (dataArray.length >= 10) {
        dataArray.push({
            x: Math.floor(Math.random() * 26),
            y: Math.floor(Math.random() * 26)
        });
        dataArray = dataArray.slice(5, -(dataArray.length - (dataArray.length - 5)));
        update(dataArray);

    } else {
        dataArray.push({
            x: Math.floor(Math.random() * 26),
            y: Math.floor(Math.random() * 26)
        });
        update(dataArray);
    }

}, 1500);