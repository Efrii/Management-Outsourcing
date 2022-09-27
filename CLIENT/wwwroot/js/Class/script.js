function formatDate(dateString) {
    let date = new Date(dateString),
        day = date.getDate(),
        month = date.getMonth(),
        year = date.getFullYear(),
        months = ['Januari','Februari','Maret','April','Mei','Juni','July','Agustus','September','Oktober','November','Desember'];
          
    return day + ' - ' + months[month] + ' - ' + year;
}

function detailClass(id_class) { 
    
    let text = `
    <div class="modal fade bd-example-modal-xl" id="ModalDetailClass${id_class}" tabindex="-1" role="dialog" aria-labelledby="ModalDetailClass" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalDetailClass">Detail Data Kelas</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="display expandable-table dataTable no-footer" style="width: 100%;" id="TableClass">
                        <thead>
                            <tr>
                                <th>
                                    No
                                </th>
                                <th>
                                    NIK
                                </th>
                                <th>
                                    Nama
                                </th>
                                <th>
                                    Tanggal Lahir
                                </th>
                                <th>
                                    Email
                                </th>
                                <th>
                                    Umur
                                </th>
                                <th>
                                    Gender
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Keluar</button>
                </div>
            </div>
        </div>
    </div>
    `
    $("#card").html(text);
    $("#ModalDetailClass"+id_class).modal('show');

    $("#TableClass").DataTable({
        "processing": true,
        "serverside": true,
        "responsive": true,
        "ajax": {
            "url": "Employee/GetDetailClass/" + id_class,
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
                data: "nik_employee",
            },
            {
                data: "name_employee"
            },
            {
                data: null,
                render: function (data, type, row){
                    return formatDate(row.datebirth)
                }
            },
            {
                data: null,
                render: function (data, type, row){
                    return `
                        <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" href="${row.email_employee}"><i class="mdi mdi-google-chrome" style="font-size: 1.5em;"></i></a>
                    `
                }
            },
            {
                data: "age_employee"
            },
            {
                data: null,
                render: function (data, type, row) {  
                    if(row.gender_Employee == 'L'){
                        return '<i class="mdi mdi-gender-male" style="font-size: 1.5em;"></i>'
                    } else {
                        return '<i class="mdi mdi-gender-female" style="font-size: 1.5em;"></i>'
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
                "targets": [4,6],
                "orderable": false
            }
        ]
    });

}

function editClass(id_class) { 
    
    $.ajax({
        url: "/Class/Get/" + id_class,
        type: "GET",
    }).done((result) => {
        console.log(result)
        let text = `
                <input type="text" class="form-control" name="id_class" id="id_class" required value="${result.id_class}" hidden disabled>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="id_class">Nama Kelas</label>
                        <input type="text" class="form-control" id="name_classes" name="name_class" placeholder="Nama Kelas" value="${result.name_class}" required>
                        <div class="invalid-feedback">
                            Nama Kelas Wajib Diisi
                        </div>
                    </div>
                </div>
            `;
    
        $("#textEditClass").html(text);

    });

}

function deleteClass(id_class) { 
    
    $.ajax({
        url: "/Class/Get/" + id_class,
        method: "GET",
    }).done((result) => {
        // Using Swal for delete
        console.log(result);
        Swal.fire({
            title: 'Apakah anda ingin menghapus ?',
            html: "anda akan mengahapus data kelas " + result.name_class,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, Hapus !'
          }).then((result) => {
            if (result.isConfirmed) {
                // action delete
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Class/Delete/" + id_class,
                    type: "DELETE",
                    dataType: "json",
                    success: function(){
                        Swal.fire(
                            'Deleted!',
                            'Berhasil menghapus data kelas',
                            'success'
                        ).then((result) => {
                            if (result.isConfirmed) {
                                window.location.reload();
                            }
                        });
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Gagal menghapus data kelas harap coba lagi '
                        })    
                    }
                });
            }
        });
    });

}

$(document).ready(function () {

    $.ajax({
        url: "Class/Get",
        method: "Get"
    }).done((result) => {
        console.log(result);
        let textClass = '';
        $.each(result, function (key, val){
            textClass += `
                <div class="col mb-3">
                    <div class="card" style="width: 18rem;">
                            <img src="/images/class.jpg" class="card-img-top img-fluid" style="border-radius: 20px;" alt="...">
                        <div class="card-body">
                            <h5 class="card-title text-center">Kelas ${val.name_class}</h5>
                            <div class="text-center">
                                <a href="#" onClick="detailClass(${val.id_class})" class="btn btn-primary"><i class="mdi mdi-comment-text"></i></a>
                                <a href="#" onClick="editClass(${val.id_class})" data-toggle="modal" data-target="#ModalEditData" class="btn btn-primary"><i class="mdi mdi-tooltip-edit"></i></a>
                                <a href="#" onClick="deleteClass(${val.id_class})" class="btn btn-primary"><i class="mdi mdi-delete"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });
        
        $("#loading-bro").remove()
        $("#cardClass").html(textClass);
    });

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
                    obj.id_class = parseInt($("#id_class").val());
                    obj.name_class = $("#name_classes").val();

                console.log(obj);

                // EDIT DATA
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Class/Edit",
                    type: "PUT",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    success: function(data){
                        $('#ModalEditData').modal('hide');
                        Swal.fire({
                            icon: 'success',
                            text: 'Berhasil ubah data kelas'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.reload();
                            }
                        });
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Gagal edit data kelas harap coba lagi'
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
                    obj.name_class = $("#name_class").val();

                console.log(obj);

                // ADD DATA SCORE
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Class/Add",
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    beforeSend: function (data) {
                        data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                    success: function(result){
                        $("#ModalAddClass").modal('hide');
                        Swal.fire({
                            icon: 'success',
                            text: 'Tambah data kelas berhasil'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.reload();
                            }
                        });
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Gagal menambah data kelas harap coba lagi'
                        })
                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });

});