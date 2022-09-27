var host = window.location.protocol + "//" + window.location.host;

function onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
}

$(document).ready(function () {
    
    $.ajax({
        'url': host + "/score/GetJoin/",
        'method': "GET",
        'contentType': 'application/json'
    }).done(function (result) {

        let arrayJumlah = [];
        let arrayName = [];

        for(let i = 0; i < result.length; i++){
            let data = result[i].employees.class.name_class
            arrayName.push(data);
        }

        let d = arrayName.filter(onlyUnique);
        for(let i = 0; i < d.length; i++){
            let filterCat = arrayName.filter(x => x == d[i]);
            arrayJumlah.push(filterCat.length);
        }

        let arrayKelas = arrayName.filter((item, i, ar) => ar.indexOf(item) === i);

        console.log(result);
        console.log(arrayKelas);
        console.log(arrayJumlah);

        // add chart bar vertical
        var ctx = document.getElementById("barChart");
        var myBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: arrayKelas,
            datasets: [{
                label: "Jumlah Murid ",
                data: arrayJumlah,
                backgroundColor: '#4c4ca6',
                  borderColor: '#4c4ca6',
                  borderWidth: 1
                }],
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }

        });  
    });

});