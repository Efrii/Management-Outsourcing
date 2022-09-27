var host = window.location.protocol + "//" + window.location.host;

function ClosModalSendEmail(){
    $("#ModalSendEmail").modal('hide');
}

function sendEmail(id_employee) {  

    $.ajax({
        url: host + "/Employee/Get/" + id_employee,
        method: "Get"
    }).done((result) => {
        console.log(result);
        let modal = `
                <input type="text" id="name_employe" name="name_employe" hidden disabled value="${result.name_employee}"/>
                <input type="text" id="email" name="email" hidden disabled value="${result.email_employee}"/>
                <div class="form-group">
                    <div class="col mb-3">
                        <label for="company_name">Pilih Perusahaan</label>
                        <select class="form-control" id="company_name" required>
                        </select>
                        <div class="invalid-feedback">
                            Perusahaan Wajib Diisi
                        </div>
                    </div>
                </div>
                <div class="form-group" id="SelectJobs">
            
                </div>          
            `;

        $("#modal").html(modal);
        $("#ModalSendEmail").modal('show');

        // Get Data Company
        $.ajax({
            url: host + "/Company/Get",
            method: "Get"
        }).done((result) => {
            let textClass = '<option value="">Pilih Perusahaan</option>';
            $.each(result, function (key, val){
                textClass += '<option value="'+val.name_company+'">'+val.name_company+'</option>'
            });
            $("#company_name").html(textClass);
            $("#company_name").change(createSummary);

            function createSummary() {
                let eventType = $("#company_name option:selected").val();   
                console.log(eventType);

                $.ajax({
                    url: host + "/Jobs/GetJoinName/" + eventType,
                    method: "Get"
                }).done((result) => {
                    console.log(result);
                    let textElement = `
                    <div class="col mb-3">
                        <label for="job_selected">Pilih Pekerjaan</label>
                            <select class="form-control" id="job_position" name="job_position" required>\
                            </select>
                        <div class="invalid-feedback">
                            Pekerjaan Wajib Diisi
                        </div>
                    </div>
                    `;
                    let textClass = '<option value="">Pilih Pekerjaan</option>';
                    $.each(result, function (key, val){
                        textClass += '<option value="'+val.name_jobs+'">'+val.name_jobs+'</option>'
                    });
                    
                    $("#SelectJobs").html(textElement);
                    $("#job_position").html(textClass);
                });
            }

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
                        obj.name_operasional = $("#name_operasional").val();
                        obj.name_employe = $("#name_employe").val();
                        obj.email = $("#email").val();
                        obj.company_name = $("#company_name").val();
                        obj.job_position = $("#job_position").val();

                    console.log(obj);

                    // ADD DATA SCORE
                    $.ajax({
                        headers: { 
                            'Accept': 'application/json',
                            'Content-Type': 'application/json' 
                        },
                        url: "/Employee/SendJobMapping",
                        type: "POST",
                        dataType: "json",
                        data: JSON.stringify(obj),
                        beforeSend: function (data) {
                            $("#loading-bro").show();
                            data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                        },
                        success: function(result){
                            console.log(result);
                            $('#ModalSendEmail').modal('hide');
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
                form.classList.add('was-validated');
            }, false);
        });

    });
}

function detailClass(id_class) { 
    
    let text = `
    <div class="modal fade bd-example-modal-xl" id="ModalDetailClass${id_class}" tabindex="-1" role="dialog" aria-labelledby="ModalEditData" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalEditData">Detail Data Kelas</h5>
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
                                    Email
                                </th>
                                <th>
                                    Nilai
                                </th>
                                <th>
                                    Predikat
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
        "order": [[3, 'desc']],
        "ajax": {
            "url": host + "/Score/GetJoinClassId/" + id_class,
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
                data: "employees.email_employee"
            },
            {
                data: null,
                render: function (data, type, row){ 
                    if(row.total >= 80){
                        return `
                            <div class="badge badge-success"> ${row.total} </div>
                        `
                    }else if (row.total <= 79 && row.total >= 59){
                        return `
                            <div class="badge badge-warning"> ${row.total} </div>
                        `
                    } else {
                        return `
                            <div class="badge badge-danger"> ${row.total} </div>
                        `
                    }
                }
            },
            {
                data: null,
                render: function (data, type, row){ 
                    if(row.total >= 80){
                        return `
                            <div class="badge badge-success"> A </div>
                        `
                    }else if (row.total <= 79 && row.total >= 59){
                        return `
                            <div class="badge badge-warning"> B </div>
                        `
                    } else {
                        return `
                            <div class="badge badge-danger"> C </div>
                        `
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
                    if(row.employees.is_place == false){
                        return `<div class="text-center">
                                    <a class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="sendEmail('${row.employees.id_employee}')" href="#"><i class="mdi mdi-email" style="font-size: 1.5em;"></i></a>
                                </div>`
                    } else{
                        return `<div class="text-center">
                                <button disabled class="btn btn-primary" style="padding: 10px 4px 5px 10px;" onClick="sendEmail('${row.employees.id_employee}')" href="#"><i class="mdi mdi-email" style="font-size: 1.5em;"></i></button>
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
                "targets": [2,6],
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
                                <a href="#" onClick="detailClass(${val.id_class})" class="btn btn-primary"><i class="mdi mdi-comment-text"></br>Pemetaan</i></a>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });
        
        $("#cardClass").html(textClass);
    });

});