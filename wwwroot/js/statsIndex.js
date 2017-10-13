function clearDiv() {
    $(".canvas-holder").empty();
    $('.canvas-holder').removeClass('col-md-6 col-md-offset-3');
}

function createChart (ctx, data) {
    let chart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ["Right", "Wrong"],
            datasets: [{
                label: "Percent Right or Wrong",
                backgroundColor: ["rgb(255, 99, 132)", "rgb(54, 162, 235)"],
                borderColor: 'rgb(255, 99, 132)',
                data: data
            }]
        },
        options: {
            legend: {
                display: true,
                labels: {
                    fontSize: 14
                }
            }
        }
    });
}

function MakeChart(dataSet) {
    clearDiv();
    console.log(dataSet);
    if (dataSet.length === 0){
        $('.canvas-holder').hide();
        $('.para').show();
        return;
    } 
    let data = dataSet.map((s) => {
        for (let obj in s) {
            return s[obj];
        }
    });
    $('.canvas-holder').show();
    $('.canvas-holder').addClass('col-md-6 col-md-offset-3');
    $('.para').hide();
    $('.canvas-holder').append($('<h3>Average score for all continents and countries</h3><canvas id="percentage"></canvas>'));
    let ctx = $('#percentage')[0].getContext('2d');
    createChart(ctx, data);
}
MakeChart(stats.Overall);

function MakeAreaChart (dataset) {
    console.log(dataset);
    clearDiv();
    if (dataset.length === 0) {
        $('.canvas-holder').hide();
        $('.para').show();
        return;
    }
    for (item in dataset) {
        let obj  = dataset[item];
        var key = Object.keys(obj)[0];
        let percentageRight = obj[key] * 100;
        let data = [percentageRight, 100 - percentageRight];
        let id = key.replace(/\s+/g, "-").replace(/[(,)]/g, '');
        let $canvasDiv = `<div class=""><h3>${key}</h3><canvas id="${id}"></canvas><div>`;
        $('.canvas-holder').append($canvasDiv);
        let ctx = $(`#${id}`)[0].getContext('2d');
        createChart(ctx, data);

    }
}

$('a').click((e) => {
    if ($(e.target).attr('for')) {
        $('a').removeClass('active');
        $(e.target).addClass('active');
        var which = $(e.target).attr('for');
        if (which === "Overall") {
            MakeChart(stats[which]);
        } else {
            MakeAreaChart(stats[which]);
        }       
    }
});