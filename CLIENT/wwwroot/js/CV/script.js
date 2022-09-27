// function DeleteCV(id_cv) {
//     // GET DETAIL Supplier
//     $.ajax({
//         url: "/Cv/Get/"+id_cv,
//         method: "GET",
//     }).done((result) => {
//         // Using Swal for delete
//         console.log(result);
//         Swal.fire({
//             title: 'Are you sure?',
//             html: "You want to delete the data Cv",
//             icon: 'warning',
//             showCancelButton: true,
//             confirmButtonColor: '#3085d6',
//             cancelButtonColor: '#d33',
//             confirmButtonText: 'Yes, delete it!'
//           }).then((result) => {
//             if (result.isConfirmed) {

//                 // action delete
//                 $.ajax({
//                     headers: { 
//                         'Accept': 'application/json',
//                         'Content-Type': 'application/json' 
//                     },
//                     url: "/Cv/Delete/"+id_cv,
//                     type: "DELETE",
//                     dataType: "json",
//                     success: function(){
//                         $("#TableCV").DataTable().ajax.reload();
//                         Swal.fire(
//                             'Deleted!',
//                             'Your data Score has been deleted.',
//                             'success'
//                           )
//                     },
//                     error: function (errormessage) {
//                         console.log(errormessage)
//                         Swal.fire({
//                             icon: 'error',
//                             title: 'Oops...',
//                             text: 'Error Code '+errormessage.responseJSON.status +' With '+ errormessage.responseJSON.title
//                         })    
//                     }
//                 });
//             }
//         });
//     });
// }

// function editCV(id_cv){
 
//     let arrayEmployee;
//     let arrayCompany;
//     let jobPosition;
//     console.log(id_cv);

//     $.ajax({
//         url: "/Cv/Get/"+id_cv,
//         type: "GET",
//     }).done((result) => {
//         let val = result;
//         arrayEmployee = val.id_employee;
//         arrayCompany = val.id_company;
//         jobPosition = val.job_selected;
//         let text = `
//                 <input type="text" class="form-control" name="id_cv" id="id_interviews" required value="${val.id_cv}" hidden disabled>
//                     <div class="form-group">
//                         <div class="col mb-3">
//                             <label for="id_employees">Select Employee</label>
//                             <select class="form-control" id="id_employees" required>
                            
//                             </select>
//                             <div class="invalid-feedback">
//                                 Employee is required
//                             </div>
//                         </div>
//                     </div>
//                     <div class="form-group">
//                         <div class="col mb-3">
//                             <label for="id_companys">Select Company</label>
//                             <select class="form-control" id="id_companys" required>
//                             </select>
//                             <div class="invalid-feedback">
//                                 Class is required
//                             </div>
//                         </div>
//                     </div>
//                     <div class="form-group" id="SelectJobss">
                    
//                     </div>
//                     <div class="form-group">
//                         <div class="col mb-3">
//                             <label for="segment1">Upload CV</label>
//                             <input type="file" class="form-control" id="cv_employee" name="cv_employee" placeholder="Date Interview" required>
//                             <div class="invalid-feedback">
//                                 Date interview is required
//                             </div>
//                         </div>
//                     </div>
//             `;
        
//         console.log(text);
//         $("#textEditCV").html(text);

// console.log(arrayCompany);
//         // GET Employee
//         $.ajax({
//             url: "Employee/Get",
//             method: "GET",
//         }).done((result) => {
//             let text = '<option value="">Pilih Employee</option>';
//             $.each(result, function (key, isi){
//                 text += '<option value="'+isi.id_employee+'">'+isi.name_employee+'</option>'
//             });

//             $("#id_employees").html(text);

//             //ADD SELECTED ON SELECT OPTION
//             for (var option of document.getElementById("id_employees").options)
//             {
//                 if (option.value == arrayEmployee)
//                 {
//                     option.selected = true;
//                     return;
//                 }
//             }
//         });

//         $.ajax({
//             url: "Company/Get",
//             method: "Get"
//         }).done((result) => {
//             let textClass = '<option value="">Pilih Company</option>';
//             $.each(result, function (key, val){
//                 textClass += '<option value="'+val.id_company+'">'+val.name_company+'</option>'
//             });
//             $("#id_companys").html(textClass);

//             $("#id_companys").change(createSummary);

//             //ADD SELECTED ON SELECT OPTION
//             for (var option of document.getElementById("id_companys").options)
//             {
//                 if (option.value == arrayCompany)
//                 {
//                     option.selected = true;
//                     return;
//                 }
//             }

//             function createSummary() {
//                 let eventType = $("#id_companys option:selected").val();  
//                 console.log(eventType);

//                 $.ajax({
//                     url: "Jobs/GetJoinID/"+eventType,
//                     method: "Get"
//                 }).done((result) => {
//                     console.log(result);
//                     let textElement = `
//                     <div class="col mb-3">
//                         <label for="job_selected">Select Company</label>
//                             <select class="form-control" id="job_positions" name="job_position" required>\
//                             </select>
//                         <div class="invalid-feedback">
//                             Job osition is required
//                         </div>
//                     </div>
//                     `;
//                     let textClass = '<option value="">Pilih Jobs</option>';
//                     $.each(result, function (key, val){
//                         textClass += '<option value="'+val.name_jobs+'">'+val.name_jobs+'</option>'
//                     });
                    
//                     $("#SelectJobss").html(textElement);
//                     $("#job_positions").html(textClass);

//                     //ADD SELECTED ON SELECT OPTION
//                     for (var option of document.getElementById("job_positions").options)
//                     {
//                         if (option.value == jobPosition)
//                         {
//                             option.selected = true;
//                             return;
//                         }
//                     }
//                 });
//             }
//         });

//     });
// }

// $(document).ready(function () {

//     // Get Data Employee
//     $.ajax({
//         url: "Employee/Get",
//         method: "GET",
//     }).done((result) => {
//         let text = '<option value="">Pilih Employee</option>';
//         $.each(result, function (key, val){
//             text += '<option value="'+val.id_employee+'">'+val.name_employee+'</option>'
//         });
        
//         $("#id_employee").html(text);

//         // Get Data Company
//         $.ajax({
//             url: "Company/Get",
//             method: "Get"
//         }).done((result) => {
//             let textClass = '<option value="">Pilih Company</option>';
//             $.each(result, function (key, val){
//                 textClass += '<option value="'+val.id_company+'">'+val.name_company+'</option>'
//             });

//             $("#id_company").html(textClass);
//             $("#id_company").change(createSummary);

//             function createSummary() {
//                 let eventType = $("#id_company option:selected").val();  
//                 console.log(eventType);

//                 $.ajax({
//                     url: "Jobs/GetJoinID/"+eventType,
//                     method: "Get"
//                 }).done((result) => {
//                     console.log(result);
//                     let textElement = `
//                     <div class="col mb-3">
//                         <label for="job_selected">Select Company</label>
//                             <select class="form-control" id="job_selected" name="job_selected" required>\
//                             </select>
//                         <div class="invalid-feedback">
//                             Class is required
//                         </div>
//                     </div>
//                     `;
//                     let textClass = '<option value="">Pilih Jobs</option>';
//                     $.each(result, function (key, val){
//                         textClass += '<option value="'+val.name_jobs+'">'+val.name_jobs+'</option>'
//                     });
                    
//                     $("#SelectJobs").html(textElement);
//                     $("#job_selected").html(textClass);
//                 });
//             }

//             // VALIDATE ADD SMARTPHONE 
//             var forms = document.getElementsByClassName('needs-validation');
//             var validation = Array.prototype.filter.call(forms, function(form) {
//                 form.addEventListener('submit', function(event) {
//                     if (form.checkValidity() === false) {
//                     event.preventDefault();
//                     event.stopPropagation();
//                     } else{

//                         event.preventDefault();

//                         let obj = {};
//                             obj.id_employee = parseInt($("#id_employee").val());
//                             obj.id_company = parseInt($("#id_company").val());
//                             obj.job_selected = $("#job_selected").val();
//                             obj.cv_employee = $("#cv_employee").val();
//                             // obj.is_done = parseInt($("#is_done").val());
//                             let myForm = document.getElementById('PostCV');
//                             var formData = new FormData(myForm);

//                             console.log(formData);

//                             for (let [key, value] of formData.entries()) { 
//                                 console.log(key, value);
//                             }

//                         // ADD DATA INTERVIEW
//                         $.ajax({
//                             type: 'POST',
//                             url: "/cv/uploadcv",
//                             data: formData,
//                             processData: false,
//                             contentType: false,
//                             success: function(result){
//                                 console.log(result);
//                                 $("#TableCV").DataTable().ajax.reload();

//                                 Swal.fire({
//                                     icon: 'success',
//                                     text: 'Data has been saved'
//                                 }).then((result) => {
//                                     if (result.isConfirmed) {
//                                     }
//                                 }); 
//                             },
//                             error: function (errormessage) {
//                                 console.log(errormessage)
//                                 Swal.fire({
//                                     icon: 'error',
//                                     title: 'Oops...',
//                                     text: 'Error Code '+errormessage.responseJSON.status +' With '+ errormessage.responseJSON.title
//                                 })
//                             }
//                         });
//                     }
//                     form.classList.add('was-validated');
//                 }, false);
//             });

//         });
//     });

//     $("#TableCV").DataTable({
//         "processing": true,
//         "serverside": true,
//         "responsive": true,
//         "ajax": {
//             "url": "/Cv/Get",
//             "dataType": "json",
//             "dataSrc": ""
//         },
//         "columns": [
//             {
//                 data: null,
//                 render: function (data, type, row, meta) {
//                  return meta.row + meta.settings._iDisplayStart + 1;
//                 }  
//             },
//             {
//                 data: null,
//                 render: function (data, type, row) {
//                     return `<img src="${row['cv_employee']}" class="img-fluid" />`
//                 }
//             },
//             {
//                 data: null,
//                 render: function (data, type, row) {
//                     return `<div class="text-center">
//                                 <a class="btn btn-primary" onClick="editCV('${row['id_cv']}')" href="#"  data-toggle="modal" data-target="#ModalEditData" ><i class="mdi mdi-table-edit"></i></a>
//                                 <a class="btn btn-primary" onClick="DeleteCV('${row['id_cv']}')" href="#" ><i class="mdi mdi-delete-forever"></i></a>
//                             </div>`
//                 }
//             }
//         ]
//     });
// })

var host = window.location.protocol + "//" + window.location.host;

function IsCheck(id_cv) {  

    $.ajax({
        url: host + "/Cv/Get/" + id_cv,
        method: "GET",
    }).done((result) => {
        
        let obj = {};
            obj.id_cv = parseInt(id_cv);
            obj.is_check = Boolean(true);
        
        console.log(obj);

        Swal.fire({
            title: 'Apakah sudah anda periksa CVnya ?',
            html: "Anda akan mengirimkan cv kebagian client perusahaan untuk ditinjau lebih lanjut ",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, Kirim !',
            cancelButtonText: 'Batal'
          }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    headers: { 
                        'Accept': 'application/json',
                        'Content-Type': 'application/json' 
                    },
                    url: "/Cv/IsCheck",
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(obj),
                    success: function(data){
                        console.log(data);
                        $("#TableClass").DataTable().ajax.reload();
                        Swal.fire({
                            icon: 'success',
                            text: 'Data cv berhasil di kirim'
                        })
                    },
                    error: function (errormessage) {
                        console.log(errormessage)
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Terdapat Kesahalan atau error'
                        })
                    }
                });
            }
        });
    });

}

function detailCV(id_employee){

    $.ajax({
        url: host + "/cv/GetCvBy/" + id_employee,
        method: "Get"
    }).done((result) => {
        console.log(result);
        let modal = `
            <div class="modal fade bd-example-modal-lg" id="ModalDetailCV" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="ModalEditData">Detail CV ${result.employees.name_employee}</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group text-center">
                                <img src="${result.cv_employee}" class="img-fluid" alt="Responsive image">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
                        </div>
                    </div>
                </div>
            </div>        
            `;

        $("#modaladd").html(modal);
        $("#ModalDetailCV").modal('show');
    
    });
}

function detailClass(id_class) {  
    
    let text = `
    <div class="modal fade bd-example-modal-xl" id="ModalDetailClass${id_class}" tabindex="-1" role="dialog" aria-labelledby="ModalEditData" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalEditData">Detail Data Cv</h5>
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
                                    Nama
                                </th>
                                <th>
                                    Perusahaan
                                </th>
                                <th>
                                    Pekerjaan
                                </th>
                                <th>
                                    Cv
                                </th>
                                <th>
                                    Check
                                </th>
                                <th>
                                    Status
                                </th>
                                <th>
                                    Action
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
            "url": host + "/Cv/GetJoinByIdClass/" + id_class,
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
                data: "job_selected"
            },
            {
                data: null,
                render: function (data, type, row) {
                    return `<img src="${row['cv_employee']}" class="img-fluid" style="width: 100px;"/>`
                }
            },
            {
                data: null,
                render: function (data, type, row){
                    if(row.is_check == false){
                        return '<label class="badge badge-danger">BELUM DICEK</label>';
                    } else {
                        return '<label class="badge badge-success">SUDAH DICEK</label>';
                    }
                }
            },
            {
                date: null,
                render: function(data, type, row){
                    if(row.employees.is_place == false){
                        return '<label class="badge badge-danger">PENDING</label>';
                    } else{
                        return '<label class="badge badge-success">SUDAH BEKERJA</label>';
                    }
                }
            },
            {
                data: null,
                render: function (data, type, row) {    
                    if(row.is_check == false){
                        return `
                            <a class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="detailCV('${row.employees.id_employee}')" href="#"><i class="mdi mdi-magnify-plus" style="font-size: 1.5em;"></i></a>
                            <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="IsCheck('${row.id_cv}')" href="#"><i class="mdi mdi-check" style="font-size: 1.5em;"></i></a>
                        `  
                    } else{
                        return `
                            <a class="btn btn-primary mb-1" style="padding: 10px 4px 5px 10px;" onClick="detailCV('${row.employees.id_employee}')" href="#"><i class="mdi mdi-magnify-plus" style="font-size: 1.5em;"></i></a>
                            <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="IsCheck('${row.id_cv}')" href="#"><i class="mdi mdi-check" style="font-size: 1.5em;"></i></button>
                        `
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
                "targets": [4,5,6,7],
                "orderable": false
            }
        ]
    });

}

$(document).ready(function () {
    $.ajax({
        url: host + "/Class/Get",
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
                                <a href="#" onClick="detailClass(${val.id_class})" class="btn btn-primary"><i class="mdi mdi-comment-text"></br>Lihat CV</i></a>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });
        
        $("#loading-bro").remove()
        $("#cardClass").html(textClass);
    });

});