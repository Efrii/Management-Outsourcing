var host = window.location.protocol + "//" + window.location.host;

let id = $("#id_company").val();
console.log(id);

function Acceptment(id_interview) {  

    Swal.fire({
        title: 'Apakah anda ingin meneima kandidat atau tidak dari hasil interview sebelumnya ?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Terima',
        denyButtonText: `Tidak Diterima`,
        cancelButtonText: 'Batal',
      }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {

            let obj = {};
            obj.id_interview = parseInt(id_interview);
            obj.Is_accepted = parseInt(2);

            console.log(obj);

            $.ajax({
                headers: { 
                    'Accept': 'application/json',
                    'Content-Type': 'application/json' 
                },
                url: "/Interview/IsAcceptmentInterview",
                type: "POST",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function(data){
                    Swal.fire({
                        icon: 'success',
                        text: 'Selamat Kandidat Diterima !!'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $("#TableAcceptment").DataTable().ajax.reload();
                        }
                    });
                },
                error: function (errormessage) {
                    console.log(errormessage)
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Gagal menerima kandidat harap coba lagi '
                    })
                }
            });

        } else if (result.isDenied) {

            let obj = {};
            obj.id_interview = parseInt(id_interview);
            obj.Is_accepted = parseInt(1);

            console.log(obj);

            $.ajax({
                headers: { 
                    'Accept': 'application/json',
                    'Content-Type': 'application/json' 
                },
                url: "/Interview/IsAcceptmentInterview",
                type: "POST",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function(data){
                    Swal.fire({
                        icon: 'info',
                        text: 'Kandidat Ditolak !!'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $("#TableAcceptment").DataTable().ajax.reload();
                        }
                    });
                },
                error: function (errormessage) {
                    console.log(errormessage)
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Gagal menolak kandidat harap coba lagi '
                    })
                }
            });

        }
      })

}

$(document).ready(function () {

    $("#TableAcceptment").DataTable({
        "processing": true,
        "serverside": true,
        "responsive": true,
        "ajax": {
            "url": host + "/Interview/GetInterviewByIdCompany/" + id,
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
                data: "job_position"
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
                                    <button class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="Acceptment('${row['id_interview']}')" href="#" ><i class="mdi mdi-check-circle" style="font-size: 1.5em;"></i></button>
                                </div>`
                    } else if (row.is_accepted == 1){
                        return `<div class="text-center">
                                    <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="Acceptment('${row['id_interview']}')" href="#" ><i class="mdi mdi-check-circle" style="font-size: 1.5em;"></i></button>
                                </div>`
                    } else if (row.is_accepted == 2) {
                        return `<div class="text-center">
                                    <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="Acceptment('${row['id_interview']}')" href="#" ><i class="mdi mdi-check-circle" style="font-size: 1.5em;"></i></button>
                                </div>`
                    }
                }
            }
        ], 
        "columnDefs": [
            {
                "targets": [0,1,2,3,4,5,6],
                "className": 'text-center'
            },
            {
                "targets": [4,5,6],
                "orderable": false
            }
        ]
    });

})