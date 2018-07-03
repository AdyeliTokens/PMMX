var config = {
    apiKey: "AIzaSyDUJMwUqbaoYb7XPVKKEgd27REuJ2DbWXk",
    authDomain: "serverpmi.tr3sco.net",
    databaseURL: "https://pmmx-a0173.firebaseio.com",
    projectId: "pmmx-a0173",
    messagingSenderId: "575772503257"
};
firebase.initializeApp(config);



function createModal() {
    var url = $(this).data("url");
    var html = '';

    $.get(url, function (data) {
        html += "<div class='modal fade' id='createAssetModal' tabindex='- 1' role='dialog' aria-labelledby='CreateAssetModal' aria-hidden='true' data-backdrop='static'>";
        html += " <div class='modal-dialog' >"
        html += "  <div class='modal-content'>"
        html += "   <div class='modal-header alert-info'><button type='button' class='close' data-dismiss='modal'>&times;</button></div>"
        html += "   <div id='createAssetContainer' class='modal-body'></div>"
        html += "  </div>"
        html += " </div>"
        html += "</div>"

        $('#divContainer').html(html);// Need to create a div with id like divContainer
        $('#createAssetContainer').html(data);
        $('#createAssetModal').modal('show');
    });
}

function divLoading() {
    var div = "";
    div += "";
    div += "    <div class=\"row\" id=\"divLoading\">";
    div += "        <div class=\"col-md-12\">";
    div += "            <div class=\"box box-primary\">";
    div += "                <div class=\"box-header\"><h3 class=\"box-title\">Espere un momento. Estamos trabajando</h3></div>";
    div += "                <div class=\"box-body\">Cargando...</div>";
    div += "                <div class=\"overlay\"><i class=\"fa fa-refresh fa-spin fa-4x\"></i></div>";
    div += "            </div>";
    div += "        </div>";
    div += "    </div>";


    $("#row-content").html(div);
    //ChangeUrl("Cambio", "Peru/Cambio");
}


function ChangeUrl(page, url) {
    if (typeof (history.pushState) != "undefined") {
        var obj = { Page: page, Url: url };
        history.pushState(obj, obj.Page, obj.Url);

    } else {
        alert("Browser does not support HTML5.");
    }

}