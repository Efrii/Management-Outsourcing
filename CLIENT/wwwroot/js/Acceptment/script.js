var host = window.location.protocol + "//" + window.location.host;

$(document).ready(function () {
    
    $("#TableAcceptments").DataTable({
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
            "url": host + "/Acceptment/GetJoin",
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
                data: "employee.name_employee",
            },
            {
                data: "employee.class.name_class",
            },
            {
                data: "name_Company"
            },
            {
                data: "job_position"
            },
            {
                data: null,
                render: function(data, type, row){
                    if(row.employee.is_place == false){
                        return '<label class="badge badge-warning">BELUM BEKERJA</label>'
                    } else {
                        return '<label class="badge badge-success">SUDAH BEKERJA</label>'
                    }
                }
            }
        ], 
        "columnDefs": [
            {
                "targets": [0,1,2,3,4,5],
                "className": 'text-center'
            },
            {
                "targets": [5],
                "orderable": false
            }
        ]
    });

    var paginate = document.getElementById("TableAcceptments_paginate");
    paginate.classList.add("col");

    var info = document.getElementById("TableAcceptments_info");
    info.classList.add("col");

    var lenght = document.getElementById("TableAcceptments_length");
    lenght.classList.add("col");

    var filter = document.getElementById("TableAcceptments_filter");
    filter.classList.add("col");

});