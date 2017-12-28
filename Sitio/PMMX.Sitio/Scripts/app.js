
 $(".btn-action").on("click", function () {
     var url = $(this).data("url");
     var html = '';

     $.get(url, function (data)
     {
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
 });

 ﻿function divLoading() {
     var div = "";
     div += "";
     div += "<div class=\"box\" id=\"divLoading\">";
     div += "    <div class=\"box-header with-border\">";
     div += "        <h3 class=\"box-title\">Title</h3>";
     div += "        <div class=\"box-tools pull-right\">";
     div += "            <button type=\"button\" class=\"btn btn-box-tool\" data-widget=\"collapse\" data-toggle=\"tooltip\" title=\"Collapse\">";
     div += "                <i class=\"fa fa-minus\"></i>";
     div += "            </button>";
     div += "            <button type=\"button\" class=\"btn btn-box-tool\" data-widget=\"remove\" data-toggle=\"tooltip\" title=\"Remove\">";
     div += "                <i class=\"fa fa-times\"></i>";
     div += "            </button>";
     div += "        </div>";
     div += "    </div>";
     div += "    <div class=\"box-body\">";
     div += "        <i class=\"fa fa-refresh fa-spin fa-3x fa-fw\" aria-hidden=\"true\"></i>";
     div += "        <span class=\"sr-only\">Refreshing...</span>Start creating your amazing application!";
     div += "                </div>";
     div += "    <!-- /.box-body -->";
     div += "                <div class=\"box-footer\">";
     div += "        Footer";
     div += "                </div>";
     div += "    <!-- /.box-footer-->";
     div += "            </div>";


     $("#row-content").html(div);
 }
