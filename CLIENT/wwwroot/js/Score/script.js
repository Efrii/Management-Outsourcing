// Function for Delete Supplier
function deleteScore(id_score) {
    $.ajax({
        url: "/Score/Get/"+id_score,
        method: "GET",
    }).done((result) => {
        // Using Swal for delete
        console.log(result);
        Swal.fire({
            title: 'Apakah kamu yakin ?',
            html: "Kamu akan menghapus data nilai",
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
                    url: "/Score/Delete/"+id_score,
                    type: "DELETE",
                    dataType: "json",
                    success: function(){
                        $("#TableScore").DataTable().ajax.reload();
                        Swal.fire(
                            'Deleted!',
                            'Data nilai berhasil dihapus',
                            'success'
                          )
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Error Code '+errormessage.responseJSON.status +' With '+ errormessage.responseJSON.title
                        })    
                    }
                });
            }
        });
    });
}

function editScore(id_score){

    let arrayEmployee;
    let arrayClass;
    console.log(id_score);

    $.ajax({
        url: "/Score/GetJoinID/"+id_score,
        type: "GET",
    }).done((result) => {
        let val = result;
        arrayEmployee = val.id_employee;
        arrayClass = val.employees.class.id_class;
        let text = `
                <input type="text" class="form-control" name="id_score" id="id_score" required value="${val.id_score}" hidden disabled>
                <div class="form-group">
                    <div class="col mb-3">
                            <label for="id_employees">Pilih Karyawan Bootcamp</label>
                            <select class="form-control" id="id_employees" required>
                            
                            </select>
                        <div class="invalid-feedback">
                            Karyawan Wajib Diisi
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col mb-3">
                            <label for="id_classess">Pilih Kelas</label>
                            <select class="form-control" id="id_classess" required>
                            
                            </select>
                        <div class="invalid-feedback">
                            Kelas Wajib Diisi
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <div class="col mb-3">
                                <label for="segment1">Nilai Segment 1</label>
                                <input type="text" class="form-control" id="segments1" name="segment1" placeholder="Nilai Segment 1" value="${val.segment1}" required>
                                <div class="invalid-feedback">
                                    Nilai Segment 4 Wajib Diisi
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <div class="col mb-3">
                                <label for="segment2">Nilai Segment 2</label>
                                <input type="text" class="form-control" id="segments2" name="segment2" placeholder="Nilai Segment 2" value="${val.segment2}" required>
                                <div class="invalid-feedback">
                                    Nilai Segment 2 Wajib Diisi
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <div class="col mb-3">
                                <label for="segment3">Nilai Segment 3</label>
                                <input type="text" class="form-control" id="segments3" name="segment3" placeholder="Nilai Segment 3" value="${val.segment3}" required>
                                <div class="invalid-feedback">
                                    Nilai Segment 3 Wajib Diisi
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <div class="col mb-3">
                                <label for="segment4">Nilai Segment 4</label>
                                <input type="text" class="form-control" id="segments4" name="segment4" placeholder="Nilai Segment 4" value="${val.segment4}" required>
                                <div class="invalid-feedback">
                                    Nilai Segment 4 Wajib Diisi
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        
        console.log(text);
        $("#textEditScore").html(text);
        console.log(arrayClass);


        // GET Employee
        $.ajax({
            url: "Employee/GetEmployeeByRole",
            method: "GET",
        }).done((result) => {
            let text = '<option value="">Pilih Karyawan Bootcamp</option>';
            $.each(result, function (key, isi){
                text += '<option value="'+isi.user.employees.id_employee+'">'+isi.user.employees.name_employee+'</option>'
            });

            $("#id_employees").html(text);

            //ADD SELECTED ON SELECT OPTION
            for (var option of document.getElementById("id_employees").options)
            {
                if (option.value == arrayEmployee)
                {
                    option.selected = true;
                    return;
                }
            }
        });


        // GET SUPPLIER
        $.ajax({
            url: "Class/Get",
            method: "GET",
        }).done((result) => {
            let text = '<option value="">Pilih Kelas</option>';
            $.each(result, function (key, isi){
                text += '<option value="'+isi.id_class+'">'+isi.name_class+'</option>'
            });

            $("#id_classess").html(text);

            //ADD SELECTED ON SELECT OPTION
            for (var option of document.getElementById("id_classess").options)
            {
                if (option.value == arrayClass)
                {
                    option.selected = true;
                    return;
                }
            }
        });

    });
}

$(document).ready(function () {

    let id_trainner = $("#id_trainner").val();
    console.log(id_trainner);

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
                obj.id_score = parseInt($("#id_score").val());
                obj.id_trainner = parseInt(id_trainner);
                obj.id_employee = parseInt($("#id_employees").val());
                obj.id_class = parseInt($("#id_classess").val());
                obj.segment1 = parseInt($("#segments1").val());
                obj.segment2 = parseInt($("#segments2").val());
                obj.segment3 = parseInt($("#segments3").val());
                obj.segment4 = parseInt($("#segments4").val());
                obj.total = parseInt((obj.segment1 + obj.segment2 + obj.segment3 + obj.segment4)/4);

                console.log(obj);

                // EDIT DATA
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Score/PutScore",
                    type: "PUT",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    success: function(data){
                        $("#TableScore").DataTable().ajax.reload();
                        $('#ModalEditData').modal('hide');
                        Swal.fire({
                            icon: 'success',
                            text: 'Data nilai berhasil di ubah'
                        })
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Terdapat masalah harap coba lagi'
                        })
                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });

    // Get Data Class
    $.ajax({
        url: "Class/Get",
        method: "Get"
    }).done((result) => {
        let textClass = '<option value="">Pilih Kelas</option>';
        $.each(result, function (key, val){
            textClass += '<option value="'+val.id_class+'">'+val.name_class+'</option>'
        });
        $("#id_class").html(textClass);
    })

    // Get Data Employee By Parameter Role
    $.ajax({
        url: "Employee/GetEmployeeByRole",
        method: "GET",
    }).done((result) => {
        let text = '<option value="">Pilih Karyawan Bootcamp</option>';
        $.each(result, function (key, val){
            console.log(val.user.employees);
            text += '<option value="'+val.user.employees.id_employee+'">'+val.user.employees.name_employee+'</option>'
        });
        
        $("#id_employee").html(text);
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
                    obj.id_employee = parseInt($("#id_employee").val());
                    obj.id_trainner = parseInt(id_trainner);
                    obj.id_class = parseInt($("#id_class").val());
                    obj.segment1 = parseInt($("#segment1").val());
                    obj.segment2 = parseInt($("#segment2").val());
                    obj.segment3 = parseInt($("#segment3").val());
                    obj.segment4 = parseInt($("#segment4").val());
                    obj.total = parseInt((obj.segment1 + obj.segment2 + obj.segment3 + obj.segment4)/4);

                console.log(obj);

                // ADD DATA SCORE
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Score/PostScore",
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    beforeSend: function (data) {
                        data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                    success: function(result){
                        console.log(result);
                        $("#TableScore").DataTable().ajax.reload();
                        $('#ModalAddData').modal('hide');

                        if(result == 200){
                            Swal.fire({
                                icon: 'success',
                                text: 'Data nilai berhasil ditambahkan'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $("#id_employee").val('');
                                    $("#id_class").val('');
                                    $("#segment1").val('');
                                    $("#segment2").val('');
                                    $("#segment3").val('');
                                    $("#segment4").val('');
                                }
                            });
                        }else if(result == 409){
                            Swal.fire({
                                icon: 'error',
                                title: 'Nilai karyawan bootcamp sudah ada !!',
                                text: 'Harap masukkan data nilai yang belum memiliki niali !!',
                            })
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Tambah Data Error',
                                text: 'Tambah data gagal harap coba lagi',
                            })
                        }
                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });
     

    $("#TableScore").DataTable({
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
            "url": "/Score/GetJoin",
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
                data: "employees.class.name_class"
            },
            {
                data: "segment1"
            },
            {
                data: "segment2",
            },
            {
                data: "segment3"
            },
            {
                data: "segment4"
            },
            {
                data: null,
                render: function (data, type, row){
                    if(row.total >= 80){
                        return `
                            <div class="badge badge-success">${row.total} / A </div>
                        `
                    }else if (row.total <= 79 && row.total >= 59){
                        return `
                            <div class="badge badge-warning">${row.total} / B </div>
                        `
                    } else {
                        return `
                            <div class="badge badge-danger">${row.total} / C </div>
                        `
                    }
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    if(row.employees.is_place == true){
                        return `<div class="text-center">
                                    <a class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="editScore('${row['id_score']}')" href="#"  data-toggle="modal" data-target="#ModalEditData" ><i class="mdi mdi-table-edit" style="font-size: 1.5em;"></i></a>
                                    <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="deleteScore('${row['id_score']}')" href="#" ><i class="mdi mdi-delete-forever" style="font-size: 1.5em;"></i></button>
                                </div>`
                    } else {
                        return `<div class="text-center">
                                    <a class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="editScore('${row['id_score']}')" href="#"  data-toggle="modal" data-target="#ModalEditData" ><i class="mdi mdi-table-edit" style="font-size: 1.5em;"></i></a>
                                    <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="deleteScore('${row['id_score']}')" href="#" ><i class="mdi mdi-delete-forever" style="font-size: 1.5em;"></i></a>
                                </div>`
                    }
                }
            }
        ], 
        "columnDefs": [
            {
                "targets": [0,1,2,3,4,5,6,7,8],
                "className": 'text-center'
            },
            {
                "targets": [2,8],
                "orderable": false
            }
        ]
    });
    var paginate = document.getElementById("TableScore_paginate");
    paginate.classList.add("col");

    var info = document.getElementById("TableScore_info");
    info.classList.add("col");

    var lenght = document.getElementById("TableScore_length");
    lenght.classList.add("col");

    var filter = document.getElementById("TableScore_filter");
    filter.classList.add("col");

});