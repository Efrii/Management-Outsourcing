var host = window.location.protocol + "//" + window.location.host;

function detailClass(id_class) { 
    
    let text = `
    <div class="modal fade bd-example-modal-xl" id="ModalDetailClass${id_class}" tabindex="-1" role="dialog" aria-labelledby="ModalEditData" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalEditData">Detail Data Class Bootcamp</h5>
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
                                    Hasil
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
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
        ],
        "columnDefs": [
            {
                "targets": [0,1,2,3,4,5],
                "className": 'text-center'
            },
            {
                "targets": [2],
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
                                <a href="#" onClick="detailClass(${val.id_class})" class="btn btn-primary"><i class="mdi mdi-comment-text"></br>Detail</i></a>
                            </div>
                        </div>
                    </div>
                </div>
            `;
        });
        
        $("#loading-bro").remove();
        $("#cardClass").html(textClass);
    });

});