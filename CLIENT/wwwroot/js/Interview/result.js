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

function Result(id_interview) {  

    $.ajax({
        url: host + "/Interview/GetInterviewsByIdInter/" + id_interview,
        method: "GET",
    }).done((result) => {
    
        let hasil = '';
        if(result.is_accepted == 2){
            hasil = 'SELAMAT ANDA DITERIMA';
        } else if (result.is_accepted == 1){
            hasil = 'MAAF ANDA KAMI TOLAK';
        }

        let object = {};
            object.name_operasional = $("#name_operasional").val();
            object.name_employee = result.employees.name_employee;
            object.name_company = result.companys.name_company;
            object.email_employee = result.employees.email_employee;
            object.job_position = result.job_position;
            object.interviewer_name = result.interviewer_name;
            object.date_interview = formatDate(result.date_interview);
            object.hasil_interview = hasil;

        console.log(result);
        console.log(object);
        
        Swal.fire({
            title: 'Apakah Anda Ingin Mengirimkan Email ?',
            html: "Anda akan mengirimkan email hasil interview kepada kandidat " + `<b>${result.employees.name_employee}</b>`,
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
                    url: host + "/Employee/SendInterviewResult",
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

function AddToAcceptment(id_interview) {  

    $.ajax({
        url: host + "/Interview/GetInterviewsByIdInter/" + id_interview,
        method: "GET",
    }).done((result) => {

        let obj = {};
            obj.id_employee = result.employees.id_employee;
            obj.name_company = result.companys.name_company;
            obj.job_position = result.job_position;
            obj.is_place = Boolean(true);

        console.log(result);
        console.log(obj);
        
        Swal.fire({
            title: 'Apakah kandidat sudah diterima ?',
            html: "Kamu akan menambahkan kandidat employe ke bagian penerimaan atau acceptment",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, Sudah Diterima !!',
            cancelButtonText: 'Batal'
          }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: host + "/Acceptment/PostAcceptment/",
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    success: function(data){
                        Swal.fire({
                            icon: 'success',
                            text: 'Kandidat Masuk Daftar Penerimaan!!'
                        })
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Terdapat Masalah Harap Coba Lagi'
                        })
                    }
                });
            }
        });
    });

}

$(document).ready(function () {
    
    $("#TableAcceptment").DataTable({
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
            "url": host + "/Interview/GetJoin",
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
                data: "employees.class.name_class",
            },
            {
                data: "companys.name_company"
            },
            {
                data: "job_position"
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
                render: function(data, type, row){
                    if(row.is_accepted == 0){
                        return '<label class="badge badge-warning">PENDING</label>'
                    } else if (row.is_accepted == 1){
                        return '<label class="badge badge-danger">DITOLAK</label>'
                    } else if (row.is_accepted == 2) {
                        return '<label class="badge badge-success">DITERIMA</label>'
                    }
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    if(row.is_accepted == 0){
                        return `<div class="text-center">
                                    <button disabled class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="Acceptment('${row['id_interview']}')" href="#" ><i class="mdi mdi-check-circle" style="font-size: 1.5em;"></i></button>
                                    <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="Result('${row['id_interview']}')" href="#" ><i class="mdi mdi-email" style="font-size: 1.5em;"></i></button>
                                </div>`
                    } else if (row.is_accepted == 1){
                        return `<div class="text-center">
                                    <button disabled class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="Acceptment('${row['id_interview']}')" href="#" ><i class="mdi mdi-check-circle" style="font-size: 1.5em;"></i></button>
                                    <button class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="Result('${row['id_interview']}')" href="#" ><i class="mdi mdi-email" style="font-size: 1.5em;"></i></button>
                                </div>`
                    } else if (row.is_accepted == 2) {
                        if(row.employees.is_place == true){
                            return `<div class="text-center">
                                        <button disabled class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="Acceptment('${row['id_interview']}')" href="#" ><i class="mdi mdi-check-circle" style="font-size: 1.5em;"></i></button>
                                        <button class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="Result('${row['id_interview']}')" href="#" ><i class="mdi mdi-email" style="font-size: 1.5em;"></i></button>
                                    </div>`
                        } else {
                            return `<div class="text-center">
                                        <button class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="AddToAcceptment('${row['id_interview']}')" href="#" ><i class="mdi mdi-check-circle" style="font-size: 1.5em;"></i></button>
                                        <button class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="Result('${row['id_interview']}')" href="#" ><i class="mdi mdi-email" style="font-size: 1.5em;"></i></button>
                                    </div>`
                        }
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
                "targets": [5,6,7],
                "orderable": false
            }
        ]
    });

    var paginate = document.getElementById("TableAcceptment_paginate");
    paginate.classList.add("col");

    var info = document.getElementById("TableAcceptment_info");
    info.classList.add("col");

    var lenght = document.getElementById("TableAcceptment_length");
    lenght.classList.add("col");

    var filter = document.getElementById("TableAcceptment_filter");
    filter.classList.add("col");

});