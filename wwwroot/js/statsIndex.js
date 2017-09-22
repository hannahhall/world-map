function MakeChart(dataSet) {
    if (dataSet.length === 0){
        $('canvas').hide();
        $('.para').show();
        return;
    } 
    let data = dataSet.map((s) => {
        for (let obj in s) {
            return s[obj]
        }
    });
    $('canvas').show();
    $('.para').hide();
    let ctx = $('#percentage')[0].getContext('2d');
    let chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'pie',
        // The data for our dataset
        data: {
            labels: ["Right", "Wrong"],
            datasets: [{
                label: "My First dataset",
                backgroundColor: ["rgb(255, 99, 132)", "rgb(54, 162, 235)"],
                borderColor: 'rgb(255, 99, 132)',
                data: data
            }]
        },
        // Configuration options go here
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
MakeChart(stats.Overall);

$('a').click((e) => {
    if ($(e.target).attr('for')) {
        $('a').removeClass('active');
        $(e.target).addClass('active');
        MakeChart(stats[$(e.target).attr('for')]);
    }
});