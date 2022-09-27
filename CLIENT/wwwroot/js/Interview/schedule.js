function formatDate(dateString) {
    let date = new Date(dateString),
        day = date.getDate(),
        month = date.getMonth(),
        year = date.getFullYear(),
        hours = date.getHours(),
        min = date.getMinutes(),
        days = ['Minggu', 'Senin', 'Selasa', 'Rabu', 'Kamis', 'Jum`at', 'Sabtu'],
        months = ['Januari','Februari','Maret','April','Mei','Juni','July','Agustus','September','Oktober','November','Desember'];
          
    return days[date.getDay()] + ', ' + day + '-' + months[month] + '-' + year + ', ' + hours + ':' + min;
}

function updateInterview(id_interview){
    console.log(id_interview);

    let obj = {};
    obj.id_interview = parseInt($("#id_interview"+id_interview).val());
    obj.is_done = Boolean($("#is_done"+id_interview).val());

    console.log(obj);

    Swal.fire({
        title: 'Apakah Kamu Sudah Siap ?',
        html: "Kamu akan melaksanakan interview persiapkan semua dengan baik, Good Luck !!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya, Interview Sekarang!',
        cancelButtonText: 'Batal'
      }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                headers: { 
                    'Accept': 'application/json',
                    'Content-Type': 'application/json' 
                },
                url: "/Interview/IsDoneInterview",
                type: "POST",
                dataType: "json",
                data: JSON.stringify(obj),
                success: function(data){
                    Swal.fire({
                        icon: 'success',
                        text: 'Selamat Interview'
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
                        text: 'Gagal melakukan interview harap coba lagi'
                    })
                }
            });
        }
    });
}

$(document).ready(() => {

   var id = $("#id").val();

   $.ajax({
    url: "GetInterviewById/" + id,
    method: "GET",
    }).done((result) => {
        console.log(result); 
        let text = '';
        let progres = '';
        let button = '';
        $.each(result, function (key, isi){
            
            if(isi.is_done == false){
                progres = '<label class="badge badge-warning">BELUM INTERVIEW</label>';
            } else{
                progres = '<label class="badge badge-success">SELESAI</label>'
            }

            if(isi.is_done == false){               
                button = `<a class="btn btn-primary" onClick="updateInterview('${isi.id_interview}')" href="#" >Interview Sekarang</a>`
            } else{
                button = `<button disabled class="btn btn-primary" onClick="updateInterview('${isi.id_interview}')" href="#" >Interview Sekarang</button>`
            }

            text += `
                <div class="col mb-3">
                    <div class="card" style="width: 27rem;">
                        <img style="border-radius: 10px; margin: 0 auto; width: 250px;" src="/images/interview.jpeg" class="card-img-top" alt="...">
                        <div class="card-body text-center">
                            <h5 class="card-title">${isi.companys.name_company}</h5>
                            <p class="card-text">Silahkan lakukan interview sesuai dengan jadwal dan jangan lupa baca note interview di email</p>
                            <table class="mb-3 table" style="width: 390px;">
                                <tr>
                                    <td style="width: 80px;"> Pekerjaan </td>
                                    <td> : </td>
                                    <td>
                                        ${isi.job_position}    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80px;"> Date </td>
                                    <td> : </td>
                                    <td>
                                        ${formatDate(isi.date_interview)}      
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80px;"> Interviewer </td>
                                    <td> : </td>
                                    <td>
                                        ${isi.interviewer_name}      
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80px;"> Progress </td>
                                    <td> : </td>
                                    <td>
                                        ${progres}      
                                    </td>
                                </tr>
                            </table>
                            <div class="text-center">
                                <input type="text" class="form-control" name="id_interview" id="id_interview${isi.id_interview}" required value="${isi.id_interview}" hidden disabled>
                                <input type="text" class="form-control" name="is_done" id="is_done${isi.id_interview}" required value="true" hidden disabled>
                                ${button}
                            </div>
                        </div>
                    </div>
                </div>
            `
        });

        $("#loading-bro").remove()
        $("#cardScheduleInterview").html(text);


    });

})