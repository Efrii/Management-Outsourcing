var host = window.location.protocol + "//" + window.location.host;

$(document).ready(function () {
    
    $.ajax({
        'url': host + "/Score/GetJoin",
        'method': "GET",
        'contentType': 'application/json'
    }).done(function (result) {

        let arrayBekerja = [];
        let arrayBelum = []

        for(let i = 0; i < result.length; i++){
            console.log(result[i]);
            let ya = result[i].employees.is_place;
            if( ya == true){
                arrayBekerja.push(ya);
            }
            if(ya == false){
                arrayBelum.push(ya);
            }
        }

        console.log(arrayBekerja.length);
        console.log(arrayBelum.length);

        // add chart bar vertical
        var ctx = document.getElementById("barChartOperasional");
        var myBarChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: ['Sudah Bekerja','Belum Bekerja'],
            datasets: [{
                label: 'Data karyawan',
                data: [arrayBekerja.length,arrayBelum.length],
                backgroundColor: [
                    '#4c4ca6',
                    '#7978E9',
                  ],
                  borderColor: [
                    '#4c4ca6',
                    '#7978E9',
                  ]
                }],
            }
        });  
    });

});