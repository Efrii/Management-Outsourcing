var host = window.location.protocol + "//" + window.location.host;

$(document).ready(function () {
    
    let id = $("#name_trainner").val();

    console.log(id);

    $.ajax({
        url: host + "/Employee/GetTrainner/" + id,
        method: "Get"
    }).done((result) => {
        console.log(result);
        let textClass = '';
        $.each(result, function (key, val){
            textClass = `
                <h6 class="mb-4">TRAINNER KAMU</h6>
                <h4 class="mb-2" >${val.name_trainner}</h4>
            `;
        });
        $("#trainner").html(textClass);
    });

    $.ajax({
        url: host + "/Score/GetJoinIdEmployee/" + id,
        method: "Get"
    }).done((result) => {
        console.log(result);
        let grade = '';
        if(result.total >= 80){
            grade = 'A';
        }else if (result.total <= 79 && result.total >= 59){
            grade = 'B';
        } else if (result.total <= 58 && result.total >= 1){
            grade = 'C';
        } else {
            grade = 'Belum Ada Nilai'
        }

        let textClass = `
            <h6 class="mb-4">GRADE KAMU</h6>
            <h4 class="mb-2"> ${grade} </h4>
        `;
        $("#grade").html(textClass);
    });

    $.ajax({
        url: host + "/Acceptment/GetJoinIdEmployee/" + id,
        method: "Get"
    }).done((result) => {
        console.log(result);
        let textClass = '';
        if(result.employee.is_place == true){
            textClass = `
                <h6 class="mb-4">BEKERJA DI PERUSAHAAN CLIENT</h6>
                <h4 class="mb-2">${result.name_Company}</h4>
                <h4 class="mb-2">${result.job_position}</h4>
            `; 
        } else {
            textClass = `
                <h6 class="mb-4">BEKERJA DI PERUSAHAAN CLIENT</h6>
                <h4 class="mb-2">Belum Bekerja</h4>
            `;
        }
        $("#acceptment").html(textClass);
    });

    $.ajax({
        'url': host + "/score/GetJoinIdEmployee/" + id,
        'method': "GET",
        'contentType': 'application/json'
    }).done(function (result) {


        console.log(result);

        // add chart bar vertical
        var ctx = document.getElementById("barChartBootcamp");
        var myBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Segment 1','Segment 2','Segment 3','Segment 4'],
            datasets: [{
                label: "Nilai",
                data: [result.segment1,result.segment2,result.segment3,result.segment4],
                backgroundColor: [
                    '#4c4ca6',
                    '#4c4ca6',
                    '#4c4ca6',
                    '#4c4ca6',
                  ],
                  borderColor: [
                    '#4c4ca6',
                    '#4c4ca6',
                    '#4c4ca6',
                    '#4c4ca6',
                  ],
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

    $.ajax({
        url: host + "/Score/GetJoinIdEmployee/" + id,
        method: "Get"
    }).done((result) => {
        console.log(result);
        if(result.total != null){
            $("#proses1").addClass("js-active")
        }
        if(result.employees.is_place == true){
            $("#proses7").addClass("js-active")
        }
    });

    $.ajax({
        url: host + "/Cv/GetCvBy/" + id,
        method: "Get"
    }).done((result) => {
        console.log(result);
        if(result.cv_employee != null){
            $("#proses2").addClass("js-active")
        }
        if(result.is_check == true){
            $("#proses3").addClass("js-active")
        }
    });    

    $.ajax({
        url: host + "/Interview/GetInterviewById/" + id,
        method: "Get"
    }).done((result) => {
        console.log(result);

        let data = result[result.length - 1];

        if(data.date_interview != null){
            $("#proses4").addClass("js-active")
        }

        if(data.is_accepted == 2){
            $("#proses6").addClass("js-active")
        }else if (data.is_accepted == 1){
            $("#proses6").addClass("js-active text-danger")
        } else{
            $("#proses6").addClass("")
        }

        if(data.is_done == true){
            $("#proses5").addClass("js-active")
        }

    });    

});