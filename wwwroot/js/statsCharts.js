var $chartsDiv = $('.charts');

$('a').click((e) => {
    $('a').removeClass('active');
    $(e.target).addClass('active');
    var chartId = $(e.target).attr('for');
    var $canvas = $(`<canvas id="${chartId}"></canvas>`);
    $chartsDiv.html($canvas);
    switch (chartId){
        case 'date':
            getDateChart();
        case 'avgNumTries':
            getAvgNumTries();
        default:
            break;
    }

});

function getAvgNumTries(){
    let splitByTries = _.groupBy(stats, "Tries");
    let labels = Object.keys(splitByTries);
    let data = [];
    let average = 0;
    for (let set in splitByTries) {
        average += _.sumBy(splitByTries[set], "Tries");
        data.push(splitByTries[set].length);
        console.log
    }
    
    let ctx = $('#avgNumTries')[0].getContext('2d');
    let chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: `Average Number of Tries: ${(average/stats.length).toFixed(2)}`,
                borderColor: 'rgb(255, 99, 132)',
                data: data,
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
    })
}

function getDateChart(){
    let splitByDate = _.groupBy(stats, function (s){
        return s.DateCreated.split("T")[0];
    });
    let labels = Object.keys(splitByDate);
    let data = [];
    for (let set in splitByDate)
    {
        console.log(set);
        var percentage = (splitByDate[set].filter(x => x.Success === 1).length / splitByDate[set].length) * 100;
        data.push(percentage);
    }
    let ctx = $('#date')[0].getContext('2d');
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
                display: true,
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
