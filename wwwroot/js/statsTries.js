let chart;
function removeChart(chart) {
    try {
        
        chart.destroy();
    }
    catch (e) {

    }
}
function MakeChart(dataSet) {
    removeChart(chart);
    if (dataSet.length === 0) {
        $('canvas').hide();
        $('.para').show();
        return;
    }

    let averages = dataSet.map((s) => {
        for (let obj in s) {
            return s[obj];
        }
    });
    console.log(averages);
    let labels = dataSet.map((s) => {
        for (let obj in s) {
            return obj
        }
    });
    $('canvas').show();
    $('canvas')
    $('.para').hide();
    let ctx = $('#tries')[0].getContext('2d');
    
    chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Average per Country',
                backgroundColor: 'grey',
                borderColow: 'black',
                data: averages,
                borderWidth: 1
            }]
        },
        options: {
            // Elements options apply to all of the options unless overridden in a dataset
            // In this case, we are setting the border of each horizontal bar to be 2px wide
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            responsive: true,
            legend: {
                display: false
            },
            title: {
                display: true,
                text: 'Average Number of Tries'
            }
        }
    });
}
MakeChart(stats.Overall);
$('a').click((e) => {
    if ($(e.target).attr('for')) {
        $('a').removeClass('active');
        $(e.target).addClass('active');
        MakeChart(stats[$(e.target).attr('for')]);
    }
});