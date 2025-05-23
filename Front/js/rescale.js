let dataArray = [];
//select svg element
let sensors = d3.select("#sensors_chart");
//#region x, y scales definition
let xscale = d3.scaleTime().domain([new Date(Date.now() - 60000), Date.now()])//option + shift+ 5 pour [] sur apple
    .range([0, 500]);
let yscale = d3.scaleLinear().domain([-5, 5]).range([200, 10]);
//#endregion x, y scales definition
//#region x, y scales parameters
let xAxis = d3.axisTop(xscale).ticks(d3.timeSecond.every(5));
let yAxis = d3.axisLeft(yscale).ticks(2);
//#endregion x, y scales parameters
//#region x, y scales graph append and location
const gx = sensors.append("g").attr("transform", `translate(20,  200)`)
    .attr("class", "sensor_xAxis");
const gy = sensors.append("g").attr("transform", "translate(20,0)")
    .attr("class", "sensor_yAxis")
//#endregion x, y scales graph append and location

//#region circles and path graph append
const gDot = sensors.append('g').attr("class", "gDot")
const gLine = sensors.append('g').attr("class", "gLine").append("path")
    .attr("stroke", "red").style("opacity", "0.4").style("stroke-width", 0.5).style("fill", "none")
//#endregion circles and path graph append

//#region update or init function
function update(data) {
    let dot = gDot.selectAll("circle").data(data);
    let t = d3.transition().duration(1750);
    d3.select('.sensor_xAxis')
        .transition(t)
        .call(xAxis);
    d3.select(".sensor_yAxis")
        .transition(t)
        .call(yAxis);

    dot.exit().attr("class", "exit")
        .transition(t)
        .attr("cx", d => xscale(d.logDate))
        .attr("cy", d => yscale(d.value))
        .attr("r", 1.5)
        .style("fill-opacity", 1e-6)
        .remove();
    dot.attr("class", "update")
        .transition(t)
        .attr("cx", d => xscale(d.logDate))
        .attr("cy", d => yscale(d.value))
        .style("fill-opacity", 1)
        ;

    dot.enter().append("circle")
        .attr("class", "enter")

        .attr("cx", d => {
            return xscale(d.logDate)
        })
        .attr("cy", d => {
            return yscale(d.value);
        })
        .attr("r", 1.5)
        .transition(t)
        .style("fill", d => {
            let color;
            dataArray.length > 100 ? color = "green" : color = "black";
            return color
        })
    gLine.datum(data).transition(t)
        .attr("d", d3.line()
            .x(d => xscale(+d.logDate))
            .y(d => yscale(+d.value))
        )
}
//#endregion update or init function

update(dataArray);

function updateScaleDomain(data) {
    let Xmin = d3.min(dataArray, d => d.logDate);
    let Xmax = Date.now();
    let interval = Xmax-Xmin  ;

    if (interval < 60000) // 1 minute
    {
        xAxis = d3.axisTop(xscale).ticks(d3.timeSecond.every(5));
    }
    else if (60002 < interval ||interval< 900000) // 15 minutes
    {
        xAxis = d3.axisTop(xscale).ticks(d3.timeSecond.every(30));
    }
    else if(900002< interval|| interval<1800000) //30 minutes
    {
        xAxis = d3.axisTop(xscale).ticks(d3.timeMinute.every(5));
    }
    else if(1800002< interval|| interval<3600000) //60 minutes
    {
        xAxis = d3.axisTop(xscale).ticks(d3.timeMinute.every(15));
    }
    let Ymin = d3.max(dataArray, d => d.value) + 20;
    let Ymax = d3.min(dataArray, d => d.value) - 20;
    xscale.domain([Xmin, Xmax]);
    yscale.domain([Ymin, Ymax]);
    update(data);
}

setInterval(function () {
    let data = { value: Math.random() * 100, logDate: new Date(Date.now()) };
    dataArray.push(data);
    updateScaleDomain(dataArray);
}, 5000);//run this thang every 5  seconds