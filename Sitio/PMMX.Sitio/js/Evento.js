(function ($) {

    //$.getScript("bower_components/bootstrap/dist/js/bootstrap.min.js", function () { });

    "use strict"; // Start of use strict

    $('.select2').select2();

    setInterval(function () {
        GetEvents(formatDate(new Date()), "/Evento/GetEvents?date=" + formatDate(new Date()));
    }, 600000);
   
    getCategorias();
    GetEvents(formatDate(new Date()), "/Evento/GetEvents?date=" + formatDate(new Date()));

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

        switch (view.name) {
            case "month":
                var date = $.fullCalendar.formatDate($('#calendar').fullCalendar('getDate'), "YYYY-MM-DD");
                var url = "/Evento/GetEvents?date=" + date;
                GetEvents(date, url);
                break;
            case "timelineWeek":
                var d = new Date($('#calendar').fullCalendar('getDate'));
                var date = formatCurrentDate(d.setDate(d.getDate() + 7));

                $('#calendar').fullCalendar('gotoDate', date);
                $('#calendar').fullCalendar('changeView', view.name);
                break;
            case "timelineDay":
                var d = new Date($('#calendar').fullCalendar('getDate'));
                var date = formatCurrentDate(d.setDate(d.getDate() + 1));

                $('#calendar').fullCalendar('gotoDate', date);
                $('#calendar').fullCalendar('changeView', view.name);
                break;
        }
    });

    $('#calendar').on('click', '.fc-basicWeek-button', function () {
        var date = formatCurrentDate(new Date());

        $('#calendar').fullCalendar('gotoDate', date);
        $('#calendar').fullCalendar('changeView', 'timelineWeek');
    });

    $('#calendar').on('click', '.fc-basicDay-button', function () {
        var date = formatCurrentDate(new Date());

        $('#calendar').fullCalendar('gotoDate', date);
        $('#calendar').fullCalendar('changeView', 'timelineDay');
    });

    function GetEvents(date, url) {
        $.ajax({
            dataType: "json",
            contentType: "application/json",
            data: "{}",
            url: url,
            dataType: "json",
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
                        event.start = moment(item.FechaInicio).utc();
                        event.end = moment(item.FechaFin).utc();
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

    function getCategorias() {
        $.ajax({
            dataType: "json",
            contentType: "application/json",
            url: "/Operaciones/Categoria/GetCategorias",
            success: function (data) {
                var items = '<option>Categoria</option>';
                $.each(data.lista, function (i, k) {
                    items += "<option value='" + k.Id + "'>" + k.Nombre + "</option>";
                });
                $('#slt-Categorias').html(items);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert('There was an error while fetching data!');
            }
        });
    }

    $('#slt-Categorias').on("change", function () {
        var date = $.fullCalendar.formatDate($('#calendar').fullCalendar('getDate'), "YYYY-MM-DD");
        var url = "/Evento/GetEvents?date=" + date;

        if ($('#slt-Categorias').val() == "Categoria")
            GetEvents(date, url);
        else {
            url = "/Evento/GetEventsByCategoria?IdCategoria=" + $('#slt-Categorias').val() + "&Date=" + date,
                GetEvents(date, url);
        }
    });

})(jQuery); // End of use strict