$(document).ready(function () {






    $('.eliminar-btn').click(function (e) {
        e.preventDefault();
        // Mostrar SweetAlert
        var botonEliminar = $(this);
        Swal.fire({
            title: '¿Está seguro que desea eliminar este registro?',
            text: "Esta acción no se puede deshacer",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar'
        }).then((result) => {
            if (result.isConfirmed) {
                // Enviar una solicitud de eliminación al servidor
                $.ajax({
                    url: botonEliminar.attr('href'),
                    type: 'POST',
                    data: {
                        id: botonEliminar.data('id')
                    },
                    success: function (response) {
                        // Mostrar SweetAlert de éxito
                        Swal.fire(
                            'Eliminado!',
                            'El registro ha sido eliminado.',
                            'success'
                        )

                        botonEliminar.closest('tr').remove();

                    },
                    error: function (error) {
                        // Mostrar SweetAlert de error
                        Swal.fire(
                            'Error!',
                            'Ocurrió un error al intentar eliminar el registro debido a que pertence a otras tablas.',
                            'error'
                        )
                    }
                });
            };
        });
    });

    // Función para crear una tabla con DataTables y configurar los botones
    function crearTabla(tablaId) {
        $(tablaId).DataTable({
            "language": { "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json" },
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'excelHtml5',
                    autoFilter: true,
                    sheetName: 'Tabla'
                },
                {
                    extend: 'pdfHtml5',
                    orientation: 'landscape',
                    pageSize: 'LEGAL'
                },
                'print',
                'colvis'
            ]
        });
    }

    // Crear tablas y configurar los botones
    crearTabla('#tablaClientes');
    crearTabla('#tablaProductos');
    crearTabla('#tablaEmpleados');
    crearTabla('#tablaVentas');
});
