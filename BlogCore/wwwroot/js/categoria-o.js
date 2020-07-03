var dataTable; 

$(document).ready(function () {
    cargarDataTable();
});
//creación de la funcion
function cargarDataTable() {
    dataTable = $("tblcategorias").DataTable({
        "ajax": {
            //obtenemos la url y con el método GetAll obtenemos todas las categorias
            "url": "/Admin/Categorias/GetAll", 
            //Método GET para obtenerlas
            "type": "GET",
            //en el tipo de dato JSON
            "Datatype": "json"
        },
        //se configura para que nos devuelva los datos como nosotros queramos
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "50%" },
            { "data": "orden", "width": "20%" },
            //botones que van a hacer la funcionalidad de editar y borrar
            {
                "data": "id",
                //template engine de ECMAScript
                "render": function (data) {
                    return `
                        <div class="text-center">
                        <a href="/Admin/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;"></a>
                        <i class="fas fa-edit"></i> Editar 
                        </a>
                        &nbsp;
                        <div class="text-center">
                        <a onclick=Delete("/Admin/Categorias/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;"></a>
                        <i class="fas fa-edit"></i> Borrar
                        </a>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"
    });
}

function Delete(url) {
    //para que salga la ventana emergente de sweet alert
    swal({
        title: "¿Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        ConfirmButtonColor: "#DD6B55",
        ConfirmButtonText: "Si, Borrar!",
        closeOnconfirm: true
    }, function () {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message)
                    }
                }
            });
    }

    );
}