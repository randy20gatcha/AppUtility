
$(document).ready(function () {
    $('#example').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
       
        "ajax": { 
            "url": "/api/employee",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [

            { "name": "Id", "data": "id", "targets": 0, "visible": false },
            { "name": "FisrtName", "data": "firstName", "targets": 1 },
            { "name": "LastName", "data": "lastName", "targets": 2 },
            { "name": "EmailAddress", "data": "emailAddress", "targets": 3 },

            {
                "targets": -1,
                "data": null,
                "render": function (data, type, row, meta) {
                    return '<a href="/Crud/Edit/' + row.id + '" class="btn btn-success">Edit</a> | <a href="/Crud/Details/' + row.id + '" class="btn btn-primary">Details</a> | <a href="/Crud/Delete/' + row.id + '" class="btn btn-warning">Delete</a>';
                },
                "sortable": false
            },
        ],
        "order": [[0, "desc"]]
    });
});


