(function () {
    "use strict"; // Start of use strict

    var id = $("#Id").val();
    start();

    function start() {
        var idCategoria = $("#IdCategoria").val();
        $("#opciones-ventana").hide();
        $('#subVista').hide();
    }

    $("#btn-CreateVentana").on("click", function () {
        var url = ("/Warehouse/Ventana/Upload");

        $.get(url, function (data) {
            $('#createAssetContainer').html(data);
            jQuery.noConflict();
            $('#createAssetModal').modal('show');
        });
    });

    $("#btn-Editar").on("click", function () {
        var url = ("/Operaciones/Evento/Edit/" + id);

        $.get(url, function (data) {
            $('#createAssetContainer').html(data);
            jQuery.noConflict();
            $('#createAssetModal').modal('show');
        });
    });

    $("#btn-Info").on("click", function () {
        $.ajax({
            dataType: "json",
            contentType: "application/json",
            url: "/Warehouse/Ventana/GetVentanabyEvento?idEvento=" + id,
            success: function (data) {
                $.each(data, function (i, k) {
                    if (k != null) {
                        var url = ("/Warehouse/Ventana/Details/" + k.Id);

                        $("#IdVentanaEvento").val(k.Id);

                        $("#opciones-ventana").show();
                        $("#btn-Rechazo").hide();

                        if (k.Cancelado[0] != 0) {
                            $("#btn-Rechazo").show();
                        }

                        $.get(url, function (data) {
                            $('#subVista').show();
                            $('#subVista').html(data);
                        });
                    }
                    else {
                        alert("Sin datos en ventana");
                    }
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('There was an error while fetching data!');
            }
        });
    });


    $("#btn-Delete").on("click", function () {
        var url = ("/Operaciones/Evento/Delete/" + id);

        $.get(url, function (data) {
            $('#createAssetContainer').html(data);
            jQuery.noConflict();
            $('#createAssetModal').modal('show');
        });
    });

    $("#btn-Continuar").on("click", function () {
        var url = ("/Operaciones/StatusVentana/Create");

        $.get(url, function (data) {
            $('#createAssetContainer').html(data);
            jQuery.noConflict();
            $('#createAssetModal').modal('show');
        });
    });

    $("#btn-Rechazo").on("click", function (e) {
        var url = ("/Warehouse/BitacoraVentana/Create");

        $.get(url, function (data) {
            $('#createAssetContainer').html(data);
            jQuery.noConflict();
            $('#createAssetModal').modal('show');
        });
    });

})(jQuery); // End of use strict