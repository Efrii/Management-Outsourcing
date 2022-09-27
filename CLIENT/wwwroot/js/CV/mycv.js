var host = window.location.protocol + "//" + window.location.host;

$(document).ready(function () {

    var id = $("#id_employees").val();
    $.ajax({
        url: host + "/Cv/GetCvBy/" + id,
        method: "GET",
        }).done((result) => {
            console.log(result); 
            let text = `<img style="width: 100%;" src="${result.cv_employee}" >`;
            let param = '<option value="'+result.id_cv+'"></option>';
            
            $("#image").html(text);
            $("#id_cv").html(param);

            if(result.id_cv == 0){
                $("#PutCVV").remove();
            } else{
                $("#PostCVV").remove();
            }
    
        });

        // Get Data Company
        $.ajax({
            url: host + "/Company/Get",
            method: "Get"
        }).done((result) => {
            let textClass = '<option value="">Pilih Perusahaan</option>';
            $.each(result, function (key, val){
                textClass += '<option value="'+val.id_company+'">'+val.name_company+'</option>'
            });

            $("#id_company").html(textClass);
            $("#id_company").change(createSummary);

            function createSummary() {
                let eventType = $("#id_company option:selected").val();  
                console.log(eventType);

                $.ajax({
                    url: host + "/Jobs/GetJoinID/" + eventType,
                    method: "Get"
                }).done((result) => {
                    console.log(result);
                    let textElement = `
                    <div class="col mb-3">
                        <label for="job_selected">Pilih Perusahaan</label>
                            <select class="form-control" id="job_selected" name="job_selected" required>\
                            </select>
                        <div class="invalid-feedback">
                            Perusahaan Wajib Diisi
                        </div>
                    </div>
                    `;
                    let textClass = '<option value="">Pilih Pekerjaan</option>';
                    $.each(result, function (key, val){
                        textClass += '<option value="'+val.name_jobs+'">'+val.name_jobs+'</option>'
                    });
                    
                    $("#SelectJobs").html(textElement);
                    $("#job_selected").html(textClass);
                });
            }

            // VALIDATE ADD CV 
            var forms = document.getElementsByClassName('needs-validation-post');
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
                            obj.job_selected = $("#job_selected").val();
                            obj.cv_employee = $("#cv_employee").val();

                            let myForm = document.getElementById('PostCV');
                            var formData = new FormData(myForm);

                            console.log(formData);

                            for (let [key, value] of formData.entries()) { 
                                console.log(key, value);
                            }

                        // ADD DATA INTERVIEW
                        $.ajax({
                            type: 'POST',
                            url: host + "/cv/uploadcv",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function(result){
                                console.log(result);
                                $("#TableCV").DataTable().ajax.reload();
                                Swal.fire({
                                    icon: 'success',
                                    text: 'Data CV berhasil di uppload'
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
                                    text: 'Gagal menambahakan data CV harap coba lagi '
                                })
                            }
                        });
                    }
                    form.classList.add('was-validated');
                }, false);
            });

            // VALIDATE PUT CV 
            var forms = document.getElementsByClassName('needs-validation-put');
            var validation = Array.prototype.filter.call(forms, function(form) {
                form.addEventListener('submit', function(event) {
                    if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                    } else{

                        event.preventDefault();

                        let obj = {};
                            obj.id_cv = parseInt($("#id_cv"));
                            obj.id_employee = parseInt($("#id_employee").val());
                            obj.id_company = parseInt($("#id_company").val());
                            obj.job_selected = $("#job_selected").val();
                            obj.cv_employee = $("#cv_employee").val();

                            let myForm = document.getElementById('PutCV');
                            var formData = new FormData(myForm);

                            console.log(formData);

                            for (let [key, value] of formData.entries()) { 
                                console.log(key, value);
                            }

                        // ADD DATA INTERVIEW
                        $.ajax({
                            type: 'PUT',
                            url: host + "/cv/putcv",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function(result){
                                console.log(result);
                                Swal.fire({
                                    icon: 'success',
                                    text: 'Data CV berhasil di ubah'
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
                                    text: 'Gagal ubah data CV harap coba lagi'
                                })
                            }
                        });
                    }
                    form.classList.add('was-validated');
                }, false);
            });

        });

});