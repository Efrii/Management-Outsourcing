var host = window.location.protocol + "//" + window.location.host;

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
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Keluar</button>
                        </div>
                    </div>
                </div>
            </div>        
            `;

        $("#modaladd").html(modal);
        $("#ModalDetailCV").modal('show');
    
    });
}

function AddInterview(id_employee) {  

    $.ajax({
        url: host + "/Cv/GetCvBy/" + id_employee,
        method: "Get"
    }).done((result) => {
        console.log(result);
        let modal = `
                <input type="text" id="id_employee" name="id_employee" hidden disabled value="${result.employees.id_employee}"/>
                <input type="text" id="job_position" name="job_position" hidden disabled value="${result.job_selected}"/>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="segment1">Tanggal Interview</label>
                        <input type="datetime-local" class="form-control" id="date_interview" name="date_interview" placeholder="Tanggal Interview" required>
                        <div class="invalid-feedback">
                            Tangal Interview Wajib Diisi
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="segment2">URL</label>
                        <input type="text" class="form-control" id="url_Intertview" name="url_Intertview" placeholder="URL" required>
                        <div class="invalid-feedback">
                            URL Wajib Diisi
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="segment3">Interviewer</label>
                        <input type="text" class="form-control" id="interviewer_name" name="interviewer_name" placeholder="Interviewer" required>
                        <div class="invalid-feedback">
                            Interviewer Wajib Diisi
                        </div>
                    </div>
                </div>         
            `;

        $("#modal").html(modal);
        $("#ModalSendInterview").modal('show');
    
        // // VALIDATE 
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
                        obj.id_company = parseInt($("#id_company").val());
                        obj.date_interview = new Date($("#date_interview").val());
                        obj.job_position = $("#job_position").val();
                        obj.url_Intertview = $("#url_Intertview").val();
                        obj.interviewer_name = $("#interviewer_name").val();

                    console.log(obj);

                    // ADD DATA INTERVIEW
                    $.ajax({
                        headers: { 
                            'Accept': 'application/json',
                            'Content-Type': 'application/json' 
                        },
                        url: "/Interview/Add",
                        type: "POST",
                        dataType: "json",
                        data: JSON.stringify(obj),
                        beforeSend: function (data) {
                            data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                        },
                        success: function(result){
                            console.log(result);
                            $("#ModalSendInterview").modal('hide');
                            Swal.fire({
                                icon: 'success',
                                text: 'Data interview berhasil ditambahkan'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $("#id_employee").val('');
                                    $("#id_company").val('');
                                    $("#job_position").val('');
                                    $("#date_interview").val('');
                                    $("#url_Intertview").val('');
                                    $("#interviewer_name").val('');
                                }
                            }); 
                        },
                        error: function (errormessage) {
                            console.log(errormessage)
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: 'Data jadwal interview gagal di tambahkan harap coba lagi'
                            })
                        }
                    });
                }
                form.classList.add('was-validated');
            }, false);
        });

    });
}

$(document).ready(function (){

    let id = $("#id_company").val();

    $("#TableCV").DataTable({
        "processing": true,
        "serverside": true,
        "responsive": true,
        "ajax": {
            "url": host + "/cv/GetCvByCompany/" + id,
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
                data: "employees.name_employee"
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
                render: function (data, type, row) {
                    if(row.employees.is_place == true){
                        return `<div class="text-center">
                                    <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="detailCV('${row.employees.id_employee}')" href="#"><i class="mdi mdi-magnify-plus" style="font-size: 1.5em;"></i></a>
                                    <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="AddInterview('${row.employees.id_employee}')" href="#" ><i class="mdi mdi-comment-account" style="font-size: 1.5em;"></i></button>
                                </div>`
                    } else {
                        return `<div class="text-center">
                                    <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="detailCV('${row.employees.id_employee}')" href="#"><i class="mdi mdi-magnify-plus" style="font-size: 1.5em;"></i></a>
                                    <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="AddInterview('${row.employees.id_employee}')" href="#" ><i class="mdi mdi-comment-account" style="font-size: 1.5em;"></i></a>
                                </div>`
                    }
                    
                }
            }
        ],
        "columnDefs": [
            {
                "targets": [0,1,2,3,4],
                "className": 'text-center'
            },
            {
                "targets": [1,2,3,4],
                "orderable": false
            }
        ]
    });

})