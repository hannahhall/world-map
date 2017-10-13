function clearDiv() {
    $(".canvas-holder").empty();
    $('.canvas-holder').removeClass('col-md-6 col-md-offset-3');
}

function createChart(ctx, labels, data) {
    let chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: "Scores by date",
                borderColor: 'rgb(255, 99, 132)',
                data: data,
            }]
        },
        options: {
            legend: {
                display: false,
                labels: {
                    fontSize: 14
                }
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

function MakeChart(dataSet) {
    clearDiv()
    if (dataSet.length === 0) {
        $('canvas').hide();
        $('.para').show();
        return;
    }
    
    let data = dataSet.map((s) => {
        for (let obj in s) {
            return s[obj];
        }
    });
    let labels = dataSet.map((s) => {
        for (let obj in s) {
            let date = obj.slice(0, obj.indexOf("T"));
            return date;
        }
    });
    $('.canvas-holder').show();
    $('.para').hide();
    $('.canvas-holder').addClass('col-md-10 col-md-offset-1');
    $('.canvas-holder').append($('<h3>Progress by date for all Continents and areas</h3><canvas id="date"></canvas>'));
    let ctx = $('#date')[0].getContext('2d');
    createChart(ctx, labels, data);
    
}
MakeChart(stats.Overall);
function MakeAreaChart(dataset) {
    console.log(dataset);
    clearDiv()
    if (dataset.length === 0) {
        $('.canvas-holder').hide();
        $('.para').show();
        return;
    }
    for (item in dataset) {
        let obj = dataset[item];
        console.log(obj);
        let country = Object.keys(obj)[0];
        let labels = []
        let data = []
        obj[country].forEach((countryData) => {
            let date = Object.keys(countryData)[0]
            labels = labels.concat(date.slice(0, date.indexOf("T")));
            data = data.concat(Object.values(countryData));
        }) 
        console.log(labels);

        let id = country.replace(/\s+/g, "-").replace(/[(,)]/g, '');
        let $canvasDiv = `<div class=""><h3>${country}</h3><canvas id="${id}"></canvas><div>`;
        $('.canvas-holder').append($canvasDiv);
        let ctx = $(`#${id}`)[0].getContext('2d');
        createChart(ctx, labels, data);

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