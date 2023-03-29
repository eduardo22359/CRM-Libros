$(document).ready(function () {

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
});
