var host = window.location.protocol + "//" + window.location.host;

function formatDate(dateString) {
    let date = new Date(dateString),
        day = date.getDate(),
        month = date.getMonth(),
        year = date.getFullYear(),
        hours = date.getHours(),
        min = date.getMinutes(),
        days = ['Minggu', 'Senin', 'Selasa', 'Rabu', 'Kamis', 'Jum`at', 'Sabtu'],
        months = ['Januari','Februari','Maret','April','Mei','Juni','July','Agustus','September','Oktober','November','Desember'];
          
    return days[date.getDay()] + ', ' + day + '-' + months[month] + '-' + year + ', ' + hours + ':' + min;
}

// function SendInterview(id_interview) {  

//     $.ajax({
//         url: host + "/interview/GetInterviewsByIdInter/" + id_interview,
//         method: "Get"
//     }).done((result) => {
//         let tgl = formatDate(result.date_interview)
//         console.log(result);
//         console.log(tgl);
//         let modal = `
//                 <input type="text" id="name_employee" name="name_employee" hidden disabled value="${result.employees.name_employee}"/>
//                 <input type="text" id="email_employee" name="email_employee" hidden disabled value="${result.employees.email_employee}"/>       
//                 <input type="text" id="name_company" name="name_company" hidden disabled value="${result.companys.name_company}"/>       
//                 <input type="text" id="job_position" name="job_position" hidden disabled value="${result.job_position}"/>       
//                 <input type="text" id="interviewer_name" name="interviewer_name" hidden disabled value="${result.interviewer_name}"/>       
//                 <input type="text" id="date_interview" name="date_interview" hidden disabled value="${tgl}"/>       
//                 <input type="text" id="url_Intertview" name="url_Intertview" hidden disabled value="${result.url_Intertview}"/>       
//             `;

//         $("#modal").html(modal);
//         $("#ModalSendEmail").modal('show');

//         // VALIDATE ADD SMARTPHONE 
//         var forms = document.getElementsByClassName('needs-validation');
//         var validation = Array.prototype.filter.call(forms, function(form) {
//             form.addEventListener('submit', function(event) {
//                 if (form.checkValidity() === false) {
//                 event.preventDefault();
//                 event.stopPropagation();
//                 } else{

//                     event.preventDefault();

//                     let obj = {};
//                         obj.name_operasional = $("#name_operasional").val();
//                         obj.name_employee = $("#name_employee").val();
//                         obj.name_company = $("#name_company").val();
//                         obj.email_employee = $("#email_employee").val();
//                         obj.job_position = $("#job_position").val();
//                         obj.interviewer_name = $("#interviewer_name").val();
//                         obj.date_interview = $("#date_interview").val();
//                         obj.url_Intertview = $("#url_Intertview").val();

//                     console.log(obj);

//                     // ADD DATA SCORE
//                     $.ajax({
//                         headers: { 
//                             'Accept': 'application/json',
//                             'Content-Type': 'application/json' 
//                         },
//                         url: "/Employee/SendInterview",
//                         type: "POST",
//                         dataType: "json",
//                         data: JSON.stringify(obj),
//                         beforeSend: function (data) {
//                             $("#loading-bro").show();
//                             data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
//                         },
//                         success: function(result){
//                             console.log(result);
//                             $('#ModalSendEmail').modal('hide');
//                             $("#loading-bro").hide()
//                             if(result == 200){
//                                 Swal.fire({
//                                     icon: 'success',
//                                     text: 'Email berhasil dikirim'
//                                 })
//                             } else {
//                                 $("#loading-bro").hide();
//                                 Swal.fire({
//                                     icon: 'error',
//                                     title: 'Gagal Mengirimakan Email',
//                                     text: 'Coba beberapa saat lagi atau anda bisa laporkan kebagian pengembang',
//                                 })
//                             }
//                         }
//                     });
//                 }
//                 form.classList.add('was-validated');
//             }, false);
//         });

//     });
// }

function SendInterview(id_interview) {  

    $.ajax({
        url: host + "/Interview/GetInterviewsByIdInter/" + id_interview,
        method: "GET",
    }).done((result) => {

        let object = {};
            object.name_operasional = $("#name_operasional").val();
            object.name_employee = result.employees.name_employee;
            object.name_company = result.companys.name_company;
            object.email_employee = result.employees.email_employee;
            object.job_position = result.job_position;
            object.interviewer_name = result.interviewer_name;
            object.date_interview = formatDate(result.date_interview);
            object.url_Intertview = result.url_Intertview;

        console.log(result);
        console.log(object);
        
        Swal.fire({
            title: 'Apakah Anda Ingin Mengirimkan Email ?',
            html: "Anda akan mengirimkan email jadwal interview dengan perusahaan client ke kandidat " + `<b>${result.employees.name_employee}</b>`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, Kirimkan !!',
            cancelButtonText: 'Batal'
          }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: host + "/Employee/SendInterview",
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(object),
                    beforeSend: function() {
                            $("#loading-bro").show();
                    },
                    success: function(result){
                        $("#loading-bro").hide()
                        if(result == 200){
                            Swal.fire({
                                icon: 'success',
                                text: 'Email berhasil dikirim'
                            })
                        } else {
                            $("#loading-bro").hide();
                            Swal.fire({
                                icon: 'error',
                                title: 'Gagal Mengirimakan Email',
                                text: 'Coba beberapa saat lagi atau anda bisa laporkan kebagian pengembang',
                            })
                        }
                    }
                }); 
            }
        });
    });
}

$(document).ready(function () {

    $("#TableInterview").DataTable({
        "processing": true,
        "serverside": true,
        "responsive": true,
        "dom": '<"row"B><"row mt-3"lf>t<"row"ip><"clear">',
        "buttons": {
            "buttons": [
                { 
                    extend: 'copyHtml5', 
                    text: '<i class="mdi mdi-content-copy"></i>',
                    className: 'btn btn-primary',
                    titleAttr: 'Copy',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                { 
                    extend: 'excelHtml5', 
                    text: '<i class="mdi mdi-file-excel-box"></i>',
                    className: 'btn btn-primary',
                    titleAttr: 'Excel',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                { 
                    extend: 'pdfHtml5', 
                    text: '<i class="mdi mdi-file-pdf"></i>',
                    className: 'btn btn-primary',
                    titleAttr: 'Pdf',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                { 
                    extend: 'print', 
                    text: '<i class="mdi mdi-printer"></i>',
                    className: 'btn btn-primary',
                    titleAttr: 'Print',
                    exportOptions: {
                        columns: ':visible'
                    }
                },
                {
                    extend: 'colvis',
                    text: 'Colvis ',
                    className: 'btn btn-primary dropdown-toggledropdown-toggle',
                    titleAttr: 'Colvis'
                }
            ],
            "dom": {
                "button": {
                    className: 'btn'
                }
            }
        },
        "ajax": {
            "url": "/Interview/GetJoin",
            "dataType": "json",
            "dataSrc": ""
        },
        "columns": [
            {
                data: null,
                render: function (data, type, row, meta) {
                 return meta.row + meta.settings._iDisplayStart + 1;
                }  
            },
            {
                data: "employees.name_employee",
            },
            {
                data: "companys.name_company"
            },
            {
                data: "job_position"
            },
            {
                data: null,
                render: function (data, type, row) {
                    return formatDate(row['date_interview'])
                }
            },
            {
                data: "interviewer_name"
            },
            {
                data: null,
                render: function(data, type, row){
                    if(row.is_done == false){
                        return '<label class="badge badge-warning">PENDING</label>'
                    } else {
                        return '<label class="badge badge-success">SELESAI</label>'
                    }
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    if(row.is_done == false){
                        return `<div class="text-center">
                                    <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="SendInterview('${row['id_interview']}')" href="#"><i class="mdi mdi-send" style="font-size: 1.5em;"></i></a>
                                </div>`
                    } else {
                        return `<div class="text-center">
                            <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="SendInterview('${row['id_interview']}')" href="#"><i class="mdi mdi-send" style="font-size: 1.5em;"></i></button>
                        </div>`
                    }
                }
            }
        ], 
        "columnDefs": [
            {
                "targets": [0,1,2,3,4,5,6,7],
                "className": 'text-center'
            },
            {
                "targets": [6,7],
                "orderable": false
            }
        ]
    });
    var paginate = document.getElementById("TableInterview_paginate");
    paginate.classList.add("col");

    var info = document.getElementById("TableInterview_info");
    info.classList.add("col");

    var lenght = document.getElementById("TableInterview_length");
    lenght.classList.add("col");

    var filter = document.getElementById("TableInterview_filter");
    filter.classList.add("col");
})