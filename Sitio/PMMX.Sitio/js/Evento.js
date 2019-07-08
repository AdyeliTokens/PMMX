(function ($) {

    //$.getScript("bower_components/bootstrap/dist/js/bootstrap.min.js", function () { });

    "use strict"; // Start of use strict

    $('.select2').select2();

    init();
    
    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            //day = '' + d.getDate(),
            day = '01',
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    }

    function formatCurrentDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    }

    $('#calendar').on('click', '.fc-next-button, .fc-prev-button', function () {
        var view = $('#calendar').fullCalendar('getView');
        var d, date;

        switch (view.name) {
            case "month":
                date = $.fullCalendar.formatDate($('#calendar').fullCalendar('getDate'), "YYYY-MM-DD");                
                var url = $("#IdSubCategoria").val() == 0
                    ? "/Evento/GetEvents?date=" + date
                    : "/Evento/GetEventsBySubCategoria?IdSubCategoria=" + $("#IdSubCategoria").val() + "&date=" + date;
                GetEvents(date, url);
                break;
            case "timelineWeek":
                d = new Date($('#calendar').fullCalendar('getDate'));
                date = formatCurrentDate(d.setDate(d.getDate() + 7));
                
                $('#calendar').fullCalendar('gotoDate', date);
                $('#calendar').fullCalendar('changeView', view.name);
                break;
            case "timelineDay":
                d = new Date($('#calendar').fullCalendar('getDate')); 
                date = formatCurrentDate(d.setDate(d.getDate() + 1));
                
                $('#calendar').fullCalendar('gotoDate', date);
                $('#calendar').fullCalendar('changeView', view.name);
                break;
        }
    });

    $('#calendar').on('click', '.fc-basicWeek-button', function () {
        var date = $.fullCalendar.formatDate($('#calendar').fullCalendar('getDate'), "YYYY-MM-DD");

        $('#calendar').fullCalendar('gotoDate', date);
        $('#calendar').fullCalendar('changeView', 'timelineWeek');
    });

    $('#calendar').on('click', '.fc-basicDay-button', function () {
        var date = $.fullCalendar.formatDate($('#calendar').fullCalendar('getDate'), "YYYY-MM-DD");

        $('#calendar').fullCalendar('gotoDate', date);
        $('#calendar').fullCalendar('changeView', 'timelineDay');
    });

    $('.subcategoria').on('click', 'button', function () {
        var id = $(this).attr("id");
        var date = $.fullCalendar.formatDate($('#calendar').fullCalendar('getDate'), "YYYY-MM-DD");

        $("#IdSubCategoria").val(id);
        var url = $("#IdSubCategoria").val() == 0
            ? "/Evento/GetEvents?date=" + date
            : "/Evento/GetEventsBySubCategoria?IdSubCategoria=" + $("#IdSubCategoria").val() + "&date=" + date;
        GetEvents(date, url);
    });

    function GetSubCategorias() {
        $.ajax({
            dataType: "json",
            contentType: "application/json",
            data: { "IdCategoria": 10, "Opcion": true },//Ventana
            url: "/SubCategoria/GetSubCategoriasByCategoria",
            success: function (data) {
                if ($("#0").length == 0) {
                    $('<button type="button" id="0" class="btn btn-sm btn-info" title="Mostrar Todos"><i class="fa fa-calendar"></i></button>').appendTo('#div-subcategoria');
                    $('<button type="button" id="1" class="btn btn-sm btn-info" title="Local">Local</button>').appendTo('#div-subcategoria');
                    $.each(data.lista, function (i, val) {
                        $('<button type="button" id="' + val.Id + ' " class="btn btn-sm btn-info" title="' + val.Nombre + '" ">' + val.Nombre + '</button>').appendTo('#div-subcategoria');
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('There was an error while fetching data!');
            }
        });
    }

    function GetEvents(date, url) {
        $.ajax({
            dataType: "json",
            contentType: "application/json",
            data: "{}",
            url: url,
            success: function (data) {
                $('#calendar').fullCalendar('destroy');
                $('#calendar').fullCalendar('render');

                $('#calendar').fullCalendar({
                    schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
                    theme: false,
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,basicWeek,basicDay'
                    },
                    defaultView: 'month',
                    editable: false,
                    lang: 'es',
                    eventLimit: 15,
                    eventLimitText: 'More',
                    weekMode: 'liquid',
                    displayEventTime: false,
                    slotEventOverlap: false,
                    nowIndicator: true,
                    resources: [
                        {
                            id: '15',
                            title: 'Impo',
                            children: [
                                {
                                    id: 'Importacion-Mañana',
                                    title: 'Mañana'
                                },
                                {
                                    id: 'Importacion-Tarde',
                                    title: 'Tarde'
                                }
                            ]
                        },
                        {
                            id: '16',
                            title: 'Expo',
                            children: [
                                {
                                    id: 'Exportacion-Mañana',
                                    title: 'Mañana'
                                },
                                {
                                    id: 'Exportacion-Tarde',
                                    title: 'Tarde'
                                }
                            ]
                        },
                        {
                            id: '17',
                            title: 'Local',
                            children: [
                                {
                                    id: 'Local-Mañana',
                                    title: 'Mañana'
                                },
                                {
                                    id: 'Local-Tarde',
                                    title: 'Tarde'
                                }
                            ]
                        },
                        {
                            id: 'd',
                            title: 'Sin Datos'
                        },
                    ],
                    events: $.map(data.events, function (item, i) {
                        var event = new Object();
                        event.start = moment(item.FechaInicio).parseZone();
                        event.end = moment(item.FechaFin).parseZone();
                        event.title = item.Descripcion;
                        event.brief = item.Nota;
                        event.id = item.Id;
                        event.color = item.Color;
                        event.resourceId = item.Clasificacion;
                        return event;
                    }),
                    eventClick: function (calEvent, jsEvent, view) {
                        var url = "/Operaciones/Evento/Details/" + calEvent.id;

                        $.get(url, function (data) {
                            $('#createAssetContainer').html(data);
                            jQuery.noConflict();
                            $('#createAssetModal').modal('show');
                            $('#IdEventoModal').val(calEvent.id);
                        });
                    }
                });

                $('#calendar').fullCalendar('gotoDate', date);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('There was an error while fetching events!');
            }
        });
    }

    function randomColor() {
        return '#' + ('00000' + (Math.random() * 16777216 << 0).toString(16)).substr(-6);
    }

    function GetSimbologia() {
        $.ajax({
            dataType: "json",
            contentType: "application/json",
            data: { "IdCategoria": 10 },//Ventana
            url: "/Estatus/GetAllStatus",
            success: function (data) {
                $.each(data.estatus, function (i, val) {
                    $('<li><a href="#"><i class="fa fa-square" style="color: ' + val.Color +'"></i> '+ val.Nombre +'</a></li>').appendTo("#symbols");
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('There was an error while fetching data!');
            }
        });
    }

    function init()
    {
        $("#perfil").val() == "Supplier" ? $("#add-new").hide() : $("#add-new").show(); 

        GetEvents(formatDate(new Date()), "/Evento/GetEvents?date=" + formatDate(new Date()));
        GetSubCategorias();
        GetSimbologia();
    }

})(jQuery); // End of use strict