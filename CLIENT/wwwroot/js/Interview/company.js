var host = window.location.protocol + "//" + window.location.host;

let id = $("#id_company").val();

function formatDate(dateString) {
    let date = new Date(dateString),
        day = date.getDate(),
        month = date.getMonth(),
        year = date.getFullYear(),
        hours = date.getHours();
        min = date.getMinutes();
        months = ['Januari','Februari','Maret','April','Mei','Juni','July','Agustus','September','Oktober','November','Desember'];
          
    return day + '-' + months[month] + '-' + year + ', ' + hours + ':' + min;
}

function deleteInterview(id_interview) {  

    $.ajax({
        url: "/Interview/Get/"+id_interview,
        method: "GET",
    }).done((result) => {
        // Using Swal for delete
        console.log(result);
        Swal.fire({
            title: 'Apakah kamu yakin ?',
            html: "Anda akan menghapus data pekerjaan " + result.job_position,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, Hapus!'
          }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Interview/Delete/" + id_interview,
                    type: "DELETE",
                    dataType: "json",
                    success: function(){
                        $("#TableInterviewByCompany").DataTable().ajax.reload();
                        Swal.fire(
                            'Deleted!',
                            'Data interview berhasil di hapus',
                            'success'
                          )
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Gagal menghapus data harap coba lagi'
                        })    
                    }
                });
            }
        });
    });

}

function editInterview(id_interview) {  

    $.ajax({
        url: host + "/Interview/Get/" + id_interview,
        method: "Get"
    }).done((result) => {
        console.log(result);
        let modal = `
                <input type="text" id="id_interviews" name="id_interview" hidden disabled value="${result.id_interview}"/>
                <input type="text" id="id_employees" name="id_employee" hidden disabled value="${result.id_employee}"/>
                <input type="text" id="job_positions" name="job_position" hidden disabled value="${result.job_position}"/>
                <input type="text" id="is_done" name="is_done" hidden disabled value=""/>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="segment1">Tanggal Interview</label>
                        <input type="datetime-local" class="form-control" id="date_interviews" name="date_interview" placeholder="Tanggal Interview Interview" value="${result.date_interview}" required>
                        <div class="invalid-feedback">
                            Tanggal Interview Wajib Diisi
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="segment2">URL</label>
                        <input type="text" class="form-control" id="url_Intertviews" name="url_Intertview" placeholder="URL Interview" value="${result.url_Intertview}" required>
                        <div class="invalid-feedback">
                            URL interview Wajib Diisi
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="segment3">Interviewer</label>
                        <input type="text" class="form-control" id="interviewer_names" name="interviewer_name" placeholder="Interviewer" value="${result.interviewer_name}" required>
                        <div class="invalid-feedback">
                            Interviewer Wajib Diisi
                        </div>
                    </div>
                </div>         
            `;

        $("#textEditInterview").html(modal);
        $("#ModalSendInterview").modal('show');
       
    });
    
}

$(document).ready(function (){

    // VALIDATE EDIT DATA
    var forms = document.getElementsByClassName('needs-validation-edit');
    var validation = Array.prototype.filter.call(forms, function(form) {
        form.addEventListener('submit', function(event) {
            if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            } else{

                event.preventDefault();
                
                let obj = {};
                obj.id_interview = parseInt($("#id_interviews").val());
                obj.id_employee = parseInt($("#id_employees").val());
                obj.id_company = parseInt(id);
                obj.job_position = $("#job_positions").val();
                obj.date_interview = new Date($("#date_interviews").val());
                obj.url_Intertview = $("#url_Intertviews").val();
                obj.interviewer_name = $("#interviewer_names").val();
                obj.is_done = Boolean($("#is_done").val());

                console.log(obj);

                // EDIT DATA
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Interview/Edit",
                    type: "PUT",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    success: function(data){
                        console.log(data);
                        $("#TableInterviewByCompany").DataTable().ajax.reload();
                        $('#ModalEditInterview').modal('hide');
                        Swal.fire({
                            icon: 'success',
                            text: 'Data interview berhasil di ubah'
                        })
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Gagal ubah data harap coba lagi'
                        })
                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });

    $("#TableInterviewByCompany").DataTable({
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
                data: null,
                render: function (data, type, row) {
                    return formatDate(row['date_interview'])
                }
            },
            {
                data: null,
                render: function (data, type, row){
                    return `
                        <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" href="${row.url_Intertview}"><i class="mdi mdi-google-chrome" style="font-size: 1.5em;"></i></a>
                    `
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
                                    <a class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="editInterview('${row['id_interview']}')" href="#"  data-toggle="modal" data-target="#ModalEditInterview" ><i class="mdi mdi-table-edit" style="font-size: 1.5em;"></i></a>
                                    <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="deleteInterview('${row['id_interview']}')" href="#" ><i class="mdi mdi-delete-forever" style="font-size: 1.5em;"></i></a>
                                </div>`
                    } else {
                        return `<div class="text-center">
                                    <button disabled class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="editInterview('${row['id_interview']}')" href="#"  data-toggle="modal" data-target="#ModalEditInterview" ><i class="mdi mdi-table-edit" style="font-size: 1.5em;"></i></button>
                                    <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="deleteInterview('${row['id_interview']}')" href="#" ><i class="mdi mdi-delete-forever" style="font-size: 1.5em;"></i></button>
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
                "targets": [2,4,6,7],
                "orderable": false
            }
        ]
    });

})