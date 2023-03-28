//Se utiliza DataTables para darle formato
$(document).ready(function () {
    $('#tablaClientes').DataTable(
        {
            "language": { "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json" },
            /* Cambia el color de la fila al pasar el mouse */
            "rowCallback": function (row, data, index) {
                $(row).hover(
                    function () {
                        $(this).addClass('highlight');
                    },
                    function () {
                        $(this).removeClass('highlight');
                    }
                );
            }

        }
    ),
    $('#suiddeustedes').DataTable(
        {
            "language": { "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json" },
            /* Cambia el color de la fila al pasar el mouse */
            "rowCallback": function (row, data, index) {
                $(row).hover(
                    function () {
                        $(this).addClass('highlight');
                    },
                    function () {
                        $(this).removeClass('highlight');
                    }
                );
            }

        }
    );
});

