function MakeChart(dataSet) {
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
    $('canvas').show();
    $('.para').hide();
    console.log(data); 
    console.log(dataSet);
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
MakeChart(stats.Overall);
console.log(stats);
$('a').click((e) => {
    if ($(e.target).attr('for')) {
        $('a').removeClass('active');
        $(e.target).addClass('active');
        MakeChart(stats[$(e.target).attr('for')]);
    }
});