$(document).ready(function () {

    //Validate Register Employee
    var forms = document.getElementsByClassName('needs-validation-register');
    var validation = Array.prototype.filter.call(forms, function(form) {
        form.addEventListener('submit', function(event) {
            if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            } else{

                event.preventDefault();

                let obj = {}
                obj.nik_employee = $("#nik_employee").val();
                obj.name_employee = $("#name_employee").val();
                obj.genderEmployee = $("#genderEmployee").val();
                obj.datebirth = new Date($("#datebirth").val());
                obj.ageEmployee = $("#ageEmployee").val();
                obj.phone_number = $("#phone_number").val();
                obj.email_employee = $("#email_employee").val();
                obj.username = $("#usernames").val();
                obj.password = $("#passwords").val();
                

                console.log(obj)

                $.ajax({
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    url: "/Account/Register/Employee",
                    type: "POST",
                    data: JSON.stringify(obj),
                    dataType: "json",
                    beforeSend: function (request) {
                        request.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                }).done((result) => {
                    console.log(result);
                    if(result == 200){
                        Swal.fire({
                            icon: 'success',
                            title: 'Pendaftaran Akun MCCBOOTCAMP Anda Berhasil',
                            showConfirmButton: false,
                            timer: 1000
                        }).then(() => {
                            $("#registerModal").modal('hide');
                        })
                    }else if(result == 409){
                        Swal.fire({
                            icon: 'error',
                            title: 'Email sudah terdaftar !!',
                            text: 'Harap gunakan email lain yang belum digunakan di sistem ini',
                        })
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Pendaftaran Gagal',
                            text: 'Harap periksa kembali data yang anda intupkan !!',
                        })
                    }
                });

            }
            form.classList.add('was-validated');
        }, false);
    });

    //Validate Register Company
    var forms = document.getElementsByClassName('needs-validation-register-company');
    var validation = Array.prototype.filter.call(forms, function(form) {
        form.addEventListener('submit', function(event) {
            if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            } else{

                event.preventDefault();

                let obj = {}
                obj.name_company = $("#name_company").val();
                obj.email_company = $("#email_company").val();
                obj.address_company = $("#address_company").val();
                obj.phone_number = $("#phone_number").val();
                obj.username = $("#usernames").val();
                obj.password = $("#passwords").val();
                
                console.log(obj)

                $.ajax({
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    url: "/Account/Register/Company",
                    type: "POST",
                    data: JSON.stringify(obj),
                    dataType: "json",
                    beforeSend: function (request) {
                        request.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                }).done((result) => {
                    console.log(result);
                    if(result == 200){
                        Swal.fire({
                            icon: 'success',
                            title: 'Pendaftaran Akun Perusahaan Anda Berhasil !',
                            showConfirmButton: false,
                            timer: 1000
                        }).then(() => {
                            $("#registerModal").modal('hide');
                        })
                    }else if(result == 409){
                        Swal.fire({
                            icon: 'error',
                            title: 'Email sudah digunakan !!',
                            text: 'Harap gunakan email lain yang belum digunakan di sistem ini !!',
                        })
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Pendaftaran Gagal',
                            text: 'Harap periksa kembali data yang anda intupkan !!',
                        })
                    }
                });

            }
            form.classList.add('was-validated');
        }, false);
    });


    // Validate Login Employee
    var forms = document.getElementsByClassName('needs-validation');
    var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener('submit', function (event) {
            if (form.checkValidity() === false) {
                event.preventDefault();
                event.stopPropagation();
            } else {

                event.preventDefault();

                let payLoad = JSON.stringify({
                    username: $("#username").val(),
                    password: $("#password").val()
                });

                console.log(payLoad)

                $.ajax({
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    url: "/Account/Auth",
                    type: "POST",
                    data: payLoad,
                    dataType: "json",
                    beforeSend: function (request) {
                        request.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                }).done((result) => {
                    console.log(result);
                    if (result.status == 200) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Login Berhasil',
                            showConfirmButton: false,
                            timer: 1000
                        }).then(function () {
                            let role = result.data.role[0].roleEmployee;
                            if (role == 'OPERASIONAL') {
                                window.location = '/Dashboard/Operasional'
                            } else if (role == 'TRAINNER') {
                                window.location = '/Dashboard/Trainner';
                            } else if (role == 'EMPLOYEEBOOTCAMP') {
                                window.location = '/Dashboard/Bootcamp';
                            } else if (role == 'MANAGER') {
                                window.location = '/Dashboard/Index';
                            }
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Login Gagal',
                            text: 'Username atau Password salah',
                        })
                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });

    // Validate Login
    var forms = document.getElementsByClassName('needs-validation-company');
    var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener('submit', function (event) {
            if (form.checkValidity() === false) {
                event.preventDefault();
                event.stopPropagation();
            } else {

                event.preventDefault();

                let payLoad = JSON.stringify({
                    username: $("#username").val(),
                    password: $("#password").val()
                });

                console.log(payLoad)

                $.ajax({
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    url: "/Account/AuthCompany",
                    type: "POST",
                    data: payLoad,
                    dataType: "json",
                    beforeSend: function (request) {
                        request.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                }).done((result) => {
                    console.log(result);
                    if (result.status == 200) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Login Berhasil',
                            showConfirmButton: false,
                            timer: 1000
                        }).then(function () {
                            window.location = "/dashboard/company";
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Login Gagal',
                            text: 'Username atau Password salah',
                        })
                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });

})