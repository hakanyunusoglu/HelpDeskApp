$(document).ready(function() {
    $.ajax({
        type: "POST",
        url: "/Index/StatusChart",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(chData) {
            var aData = chData;
            var aLabels = aData[0];
            var aDatasets1 = aData[1];
            var dataT = {
                labels: aLabels,
                datasets: [
                    {
                        label: "Toplam ",
                        data: aDatasets1,
                        fill: false,
                        backgroundColor:
                        [
                            "rgba(54, 162, 235, 0.2)",
                            "rgba(255, 99, 132, 0.2)",
                            "rgba(255, 159, 64, 0.2)",
                            "rgba(255, 205, 86, 0.2)",
                            "rgba(75, 192, 192, 0.2)",
                            "rgba(153, 102, 255, 0.2)",
                            "rgba(201, 203, 207, 0.2)"
                        ],

                        borderColor:
                        [
                            "rgb(54, 162, 235)",
                            "rgb(255, 99, 132)",
                            "rgb(255, 159, 64)",
                            "rgb(255, 205, 86)",
                            "rgb(75, 192, 192)",
                            "rgb(153, 102, 255)",
                            "rgb(201, 203, 207)"
                        ],
                        borderWidth: 1
                    }
                ]
            };
            
            var ctx = $("#TalepDurumChart").get(0).getContext("2d");
            var myNewChart = new Chart(ctx,
                {
                    type: 'bar',
                    data: dataT,
                    options: {
                        responsive: true,
                        title: { display: true, text: 'TALEP DURUM İSTATİĞİ' },
                        legend: { position: 'top' },
                        scales: {
                            xAxes: [
                                {
                                    gridLines: { display: true },
                                    display: true,
                                    scaleLabel: { display: true, labelString: '' }
                                }
                            ],
                            yAxes: [
                                {
                                    gridLines: { display: true },
                                    display: true,
                                    scaleLabel: { display: true, labelString: '' },
                                    ticks: { stepSize: aDatasets1 , beginAtZero: true }
                                }
                            ]
                        },
                    }
                });
        }
    });

    $.ajax({
        type: "POST",
        url: "/Index/RequestOwnerChart",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            var aData = chData;
            var aLabels = aData[0];
            var aDatasets1 = aData[1];
            var dataT = {
                labels: aLabels,
                datasets: [
                    {
                        label: "Toplam ",
                        data: aDatasets1,
                        fill: false,
                        backgroundColor:
                            [
                                "rgba(54, 162, 235, 0.2)",
                                "rgba(255, 99, 132, 0.2)",
                                "rgba(255, 159, 64, 0.2)",
                                "rgba(255, 205, 86, 0.2)",
                                "rgba(75, 192, 192, 0.2)",
                                "rgba(153, 102, 255, 0.2)",
                                "rgba(201, 203, 207, 0.2)",
                                "rgba(125, 100, 45, 0.2)",
                                "rgba(65, 187, 55, 0.2)",
                                "rgba(165, 149, 132, 0.2)"

                            ],

                        borderColor:
                            [
                                "rgb(54, 162, 235)",
                                "rgb(255, 99, 132)",
                                "rgb(255, 159, 64)",
                                "rgb(255, 205, 86)",
                                "rgb(75, 192, 192)",
                                "rgb(153, 102, 255)",
                                "rgb(201, 203, 207)",
                                "rgb(125, 100, 45)",
                                "rgb(65, 187, 55)",
                                "rgb(165, 149, 132)"
                            ],
                        borderWidth: 1
                    }
                ]
            };

            var ctx = $("#TalepSahibiChart").get(0).getContext("2d");
            var myNewChart = new Chart(ctx,
                {
                    type: 'bar',
                    data: dataT,
                    options: {
                        responsive: true,
                        title: { display: true, text: 'TALEP SAHİBİ İSTATİĞİ' },
                        legend: { position: 'top' },
                        scales: {
                            xAxes: [
                                {
                                    gridLines: { display: true },
                                    display: true,
                                    scaleLabel: { display: true, labelString: '' }
                                }
                            ],
                            yAxes: [
                                {
                                    gridLines: { display: true },
                                    display: true,
                                    scaleLabel: { display: true, labelString: '' },
                                    ticks: { stepSize: aDatasets1, beginAtZero: true }
                                }
                            ]
                        },
                    }
                });
        }
    });
});