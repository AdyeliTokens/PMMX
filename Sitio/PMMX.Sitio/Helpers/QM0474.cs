using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PMMX.Modelo.Entidades.Warehouse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Sitio.Helpers
{
    public class QM0474
    {
        public static PdfDocument CreateDocument(Ventana ventana)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = ventana.PO + "_" + ventana.Proveedor.NombreCorto + "_" + ventana.NombreCarrier + "_" + ventana.Evento.FechaInicio.ToString("yyyy-MM-dd"); 

            Document doc = new Document();
            Section section = doc.AddSection();

            CreateHeader(doc);
            CreatePage(doc, ventana);

            var pdfRenderer = new DocumentRenderer(doc);
            pdfRenderer.PrepareDocument();

            int pages = pdfRenderer.FormattedDocument.PageCount;
            for (int i = 1; i <= pages; ++i)
            {
                var page = document.AddPage();

                PageInfo pageInfo = pdfRenderer.FormattedDocument.GetPageInfo(i);
                page.Width = pageInfo.Width;
                page.Height = pageInfo.Height;
                page.Orientation = pageInfo.Orientation;

                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    gfx.MUH = PdfFontEncoding.Unicode;
                    gfx.MFEH = PdfFontEmbedding.Default;
                    pdfRenderer.RenderPage(gfx, i);
                }
            }

            return document;
        }

        public static void CreateHeader(Document document)
        {
            MigraDoc.DocumentObjectModel.Tables.Table header = new MigraDoc.DocumentObjectModel.Tables.Table();
            header.Shading.Color = Colors.MidnightBlue;
            header.Format.Font.Color = Colors.White;
            header.Format.Alignment = ParagraphAlignment.Center;
            header.Format.Font.Bold = true;

            Column h_column = header.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(4));
            h_column.Format.Alignment = ParagraphAlignment.Center;

            h_column = header.AddColumn("2.5cm");
            h_column.Format.Alignment = ParagraphAlignment.Center;

            h_column = header.AddColumn("2.5cm");
            h_column.Format.Alignment = ParagraphAlignment.Center;

            h_column = header.AddColumn("2.5cm");
            h_column.Format.Alignment = ParagraphAlignment.Center;

            h_column = header.AddColumn("2.5cm");
            h_column.Format.Alignment = ParagraphAlignment.Center;

            h_column = header.AddColumn("2.5cm");
            h_column.Format.Alignment = ParagraphAlignment.Center;

            Row h_row = header.AddRow();
            Cell h_cell = h_row.Cells[5];

            h_row = header.AddRow();
            h_cell = h_row.Cells[0];
            h_row.Cells[0].MergeDown = 2;
            h_cell.AddImage(HttpContext.Current.Server.MapPath("~/img/pmm-mini.png")).Width = "3.5cm";
            h_cell = h_row.Cells[1];
            h_cell.VerticalAlignment = VerticalAlignment.Top;
            h_cell.AddParagraph("Datos del Transporte");
            h_row.Cells[1].MergeRight = 4;

            h_row = header.AddRow();
            h_row.HeadingFormat = true;
            h_row.Format.Font.Size = 6;
            h_row.VerticalAlignment = VerticalAlignment.Top;
            h_cell = h_row.Cells[1];
            h_cell.AddParagraph("Código del Formato:");
            h_cell = h_row.Cells[2];
            h_cell.AddParagraph("Fecha de Efectividad:");
            h_cell = h_row.Cells[3];
            h_cell.AddParagraph("Tiempo de Retención:");
            h_cell = h_row.Cells[4];
            h_cell.AddParagraph("Versión:");
            h_cell = h_row.Cells[5];
            h_cell.AddParagraph("Página:");

            h_row = header.AddRow();
            h_row.Format.Font.Size = 6;
            h_cell = h_row.Cells[1];
            h_cell.AddParagraph("QM0474.F01");
            h_cell = h_row.Cells[2];
            h_cell.AddParagraph(DateTime.Now.ToString());
            h_cell = h_row.Cells[3];
            h_cell.AddParagraph("");
            h_cell = h_row.Cells[4];
            h_cell.AddParagraph("1.0");
            h_cell = h_row.Cells[5];
            h_cell.AddParagraph("1 de 1");

            header.SetEdge(0, 0, 6, 4, Edge.Box, MigraDoc.DocumentObjectModel.BorderStyle.Single, .5, Colors.Black);
            document.LastSection.Add(header);
        }

        public static void CreatePage(Document document, Ventana ventana)
        {
            
            MigraDoc.DocumentObjectModel.Tables.Table table = new MigraDoc.DocumentObjectModel.Tables.Table();
            table.Borders.Width = 0.5;

            Column column = table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(4));
            column.Format.Alignment = ParagraphAlignment.Left;

            table.AddColumn(MigraDoc.DocumentObjectModel.Unit.FromCentimeter(12.5));

            Row row = table.AddRow();
            row.Shading.Color = Colors.White;
            Cell cell = row.Cells[0];
            row.Cells[0].MergeRight = 1;

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("PO:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.PO);
            
            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Recursos:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Recurso);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Cantidades (#pp):");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Cantidad);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Linea Transportista:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Carrier.NombreCorto);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Operador:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Conductor);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Movil:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.MovilConductor);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Origen:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Procedencia.NombreCorto);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Destino:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Destino.NombreCorto);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Fecha:");
            cell = row.Cells[1];
            cell.AddParagraph((ventana.Evento.FechaInicio).ToString());

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("No ECO Tractor:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.NumeroEconomico);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Placas Tractor:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.NumeroPlaca);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("No Caja/Contenedor:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.EconomicoRemolque);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Placas Caja/Contenedor:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.PlacaRemolque);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Modelo:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.ModeloContenedor);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Color:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.ColorContenedor);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Tipo Unidad:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.TipoUnidad);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Dimensión:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Dimension);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Sellos:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Sellos);

            row = table.AddRow();
            row.Format.Font.Size = 6;
            cell = row.Cells[0];
            cell.AddParagraph("Temperatura Controlada:");
            cell = row.Cells[1];
            cell.AddParagraph(ventana.Temperatura);
            
            table.SetEdge(0, 0, 2, 20, Edge.Box, MigraDoc.DocumentObjectModel.BorderStyle.Single, .5, Colors.Black);

            document.LastSection.Add(table);
        }
    }
}