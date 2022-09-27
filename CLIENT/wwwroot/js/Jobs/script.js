// Function for Delete Supplier
function deleteJobs(id_jobs) {
    $.ajax({
        url: "/Jobs/Get/"+id_jobs,
        method: "GET",
    }).done((result) => {
        // Using Swal for delete
        console.log(result);
        Swal.fire({
            title: 'Apakah kamu yakin ?',
            html: "Kamu akan menghapus data pekerjaan " + result.name_jobs,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, Hapus!'
          }).then((result) => {
            if (result.isConfirmed) {

                // action delete
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Jobs/Delete/" + id_jobs,
                    type: "DELETE",
                    dataType: "json",
                    success: function(){
                        $("#TableJobs").DataTable().ajax.reload();

                        Swal.fire(
                            'Deleted!',
                            'Data pekerjaan berhasil dihapus',
                            'success'
                          )
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Gagal menghapus data pekerjaan harap coba lagi'
                        })    
                    }
                });
            }
        });
    });
}

function editJobs(id_jobs){

    $.ajax({
        url: "/Jobs/Get/" + id_jobs,
        type: "GET",
    }).done((result) => {
        let text = `
                <input type="text" class="form-control" name="id_jobs" id="id_jobs" required value="${result.id_jobs}" hidden disabled>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="id_class">Nama Pekerjaan</label>
                        <input type="text" class="form-control" id="name_jobss" name="name_jobs" placeholder="Nama Pekerjaan" value="${result.name_jobs}" required>
                        <div class="invalid-feedback">
                            Nama Pekerjaan Wajib Diisi
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="id_class">Recruitment Skill</label>
                        <textarea class="form-control" name="recruitment_skill" id="recruitment_skills" rows="2" required>${result.recruitment_skill}</textarea>
                        <div class="invalid-feedback">
                            Recruitment skill Wajib Diisi
                        </div>
                    </div>
                </div>
            `;
        
        console.log(text);
        $("#textEditJobs").html(text);

    });
}

$(document).ready(function () {

    let id = $("#id_company").val();

    // // VALIDATE EDIT DATA
    var forms = document.getElementsByClassName('needs-validation-edit');
    var validation = Array.prototype.filter.call(forms, function(form) {
        form.addEventListener('submit', function(event) {
            if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            } else{

                event.preventDefault();
                
                let obj = {};
                    obj.id_jobs = parseInt($("#id_jobs").val());
                    obj.id_company = parseInt(id);
                    obj.name_jobs = $("#name_jobss").val();
                    obj.recruitment_skill = $("#recruitment_skills").val();

                console.log(obj);

                // EDIT DATA
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Jobs/Edit",
                    type: "PUT",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    success: function(data){
                        $("#TableJobs").DataTable().ajax.reload();
                        $('#ModalEditData').modal('hide');

                        Swal.fire({
                            icon: 'success',
                            text: 'Data pekerjaan berhasil di edit'
                        })
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Gagal mengedit data pekerjaan harap coba lagi'
                        })
                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });

    // VALIDATE ADD SMARTPHONE 
    var forms = document.getElementsByClassName('needs-validation');
    var validation = Array.prototype.filter.call(forms, function(form) {
        form.addEventListener('submit', function(event) {
            if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            } else{

                event.preventDefault();

                let obj = {};
                    obj.id_company = parseInt(id);
                    obj.name_jobs = $("#name_jobs").val();
                    obj.recruitment_skill = $("#recruitment_skill").val();

                console.log(obj);

                // ADD DATA SCORE
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Jobs/Add",
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    beforeSend: function (data) {
                        data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                    success: function(result){
                        console.log(result);
                        $("#TableJobs").DataTable().ajax.reload();
                        $('#ModalAddData').modal('hide');

                        if(result == 200){
                            Swal.fire({
                                icon: 'success',
                                text: 'Data pekerjaan berhasil ditambahkan'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $("#name_jobs").val('');
                                    $("#recruitment_skill").val('');
                                }
                            });
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Opps..',
                                text: 'Gagal menambah data pekerjaan harap coba lagi',
                            })
                        }

                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });
     

    $("#TableJobs").DataTable({
        "processing": true,
        "serverside": true,
        "responsive": true,
        "ajax": {
            "url": "/Jobs/GetJoinId/" + id,
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
                data: "name_jobs",
            },
            {
                data: "recruitment_skill"
            },
            {
                data: null,
                render: function (data, type, row) {
                    return `<div class="text-center">
                                <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="editJobs('${row['id_jobs']}')" href="#"  data-toggle="modal" data-target="#ModalEditData" ><i class="mdi mdi-table-edit" style="font-size: 1.5em;"></i></a>
                                <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="deleteJobs('${row['id_jobs']}')" href="#" ><i class="mdi mdi-delete-forever" style="font-size: 1.5em;"></i></a>
                            </div>`
                }
            }
        ], 
        "columnDefs": [
            {
                "targets": [0,1,2,3],
                "className": 'text-center'
            },
            {
                "targets": [1,2,3],
                "orderable": false
            }
        ]
    });
    var paginate = document.getElementById("TableJobs_paginate");
    paginate.classList.add("col");

    var info = document.getElementById("TableJobs_info");
    info.classList.add("col");

    var lenght = document.getElementById("TableJobs_length");
    lenght.classList.add("col");

    var filter = document.getElementById("TableJobs_filter");
    filter.classList.add("col");

});