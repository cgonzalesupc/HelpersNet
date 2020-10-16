using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;

namespace ProyectoAyuda2
{
    public partial class UnirPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerarFicha07();
        }

        internal static bool Merge(string strFileTarget, string[] arrStrFilesSource)
        {
            bool blnMerged = false;

            // Crea el PDF de salida
            try
            {
                using (System.IO.FileStream stmFile = new System.IO.FileStream(strFileTarget, System.IO.FileMode.Create))
                {
                    Document objDocument = null;
                    PdfWriter objWriter = null;

                    // Recorre los archivos
                    for (int intIndexFile = 0; intIndexFile < arrStrFilesSource.Length; intIndexFile++)
                    {
                        PdfReader objReader = new PdfReader(arrStrFilesSource[intIndexFile]);
                        int intNumberOfPages = objReader.NumberOfPages;

                        // La primera vez, inicializa el documento y el escritor
                        if (intIndexFile == 0)
                        { // Asigna el documento y el generador
                            objDocument = new Document(objReader.GetPageSizeWithRotation(1));
                            objWriter = PdfWriter.GetInstance(objDocument, stmFile);
                            // Abre el documento
                            objDocument.Open();
                        }
                        // Añade las páginas
                        for (int intPage = 0; intPage < intNumberOfPages; intPage++)
                        {
                            int intRotation = objReader.GetPageRotation(intPage + 1);
                            PdfImportedPage objPage = objWriter.GetImportedPage(objReader, intPage + 1);

                            // Asigna el tamaño de la página
                            objDocument.SetPageSize(objReader.GetPageSizeWithRotation(intPage + 1));
                            // Crea una nueva página
                            objDocument.NewPage();
                            // Añade la página leída
                            if (intRotation == 90 || intRotation == 270)
                                objWriter.DirectContent.AddTemplate(objPage, 0, -1f, 1f, 0, 0,
                                                                    objReader.GetPageSizeWithRotation(intPage + 1).Height);
                            else
                                objWriter.DirectContent.AddTemplate(objPage, 1f, 0, 0, 1f, 0, 0);
                        }
                    }
                    // Cierra el documento
                    if (objDocument != null)
                        objDocument.Close();
                    // Cierra el stream del archivo
                    stmFile.Close();
                }
                // Indica que se ha creado el documento
                blnMerged = true;
            }
            catch (Exception objException)
            {
                System.Diagnostics.Debug.WriteLine(objException.Message);
            }
            // Devuelve el valor que indica si se han mezclado los archivos
            return blnMerged;
        }

        public void UnirPDFxcx() {
            List<byte[]> pdf = new List<byte[]>();


            byte[] by_1 = fileToByteArray(@"D:\DescargasChrome\Constancia_virtual 20202[R].pdf");
            byte[] by_2 = fileToByteArray(@"D:\DescargasChrome\Constancia_virtual 20203[R].pdf");
            pdf.Add(by_1);
            pdf.Add(by_2);
            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document())
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        document.Open();

                        for (int i = 0; i < pdf.Count; ++i)
                        {
                            PdfReader reader = new PdfReader(pdf[i]);
                            // loop over the pages in that document
                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n;)
                            {
                                copy.AddPage(copy.GetImportedPage(reader, ++page));
                            }
                        }
                    }
                }
                mergedPdf = ms.ToArray();
            }


            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Document_" + DateTime.Now.ToShortDateString().ToString() + ".pdf"));
            Response.BinaryWrite(mergedPdf);
            Response.End();

        }

        public byte[] fileToByteArray(String fileName)
        {
            BinaryReader binReader = new BinaryReader(File.Open(fileName, FileMode.Open, FileAccess.Read));
            binReader.BaseStream.Position = 0;
            byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
            binReader.Close();
            return binFile;
        }


        public static MemoryStream GenerarFicha05()
        {
            Font oFONT_TITULO = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 11);
            Font oFONT_TITULOxxx = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 8);
            Font oFONT_BOLD = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 9);
            Font oFONT_NORM = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 9);
            byte[] bDocument = null;

            using (MemoryStream mStream = new MemoryStream())
            {
                Document oDocument = new Document(PageSize.A4);
                PdfWriter pdfWrite = PdfWriter.GetInstance(oDocument, mStream);
                oDocument.Open();

                for (int i = 0; i < 1; i++)
                {
                    if (i > 0)
                    {
                        oDocument.NewPage();
                    }

                    #region Titulo

                    string sDE_RUTA_LOGO = System.Web.HttpContext.Current.Server.MapPath("~/Imagenes/") + "logo_sineace.png";

                    if (!string.IsNullOrEmpty(sDE_RUTA_LOGO))
                    {
                        try
                        {
                            Image gif;
                            gif = Image.GetInstance(File.ReadAllBytes(sDE_RUTA_LOGO));
                            gif.ScalePercent(80);
                            gif.SetAbsolutePosition(55, 775);
                            gif.ScaleAbsoluteWidth(400);
                            gif.ScaleAbsoluteHeight(40);
                            oDocument.Add(gif);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    PdfPTable tbTB_0001 = new PdfPTable(1);
                    var tbTB_COLU_0001 = new[] { 100 };
                    tbTB_0001.SetWidths(tbTB_COLU_0001);
                    tbTB_0001.WidthPercentage = 100f;



                    PdfPCell celCU_DOCU_CABE = new PdfPCell();



                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n\n\n Datos del evaluador y coordinador", oFONT_TITULO));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n", oFONT_TITULO));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n Nombre y Apellido de Candidato: " + "", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n Código del candidato: " + "", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    oDocument.Add(tbTB_0001);

                    #endregion

                    #region Cuerpo
                    PdfPTable tbTB_0002 = new PdfPTable(4);
                    //var tbTB_COLU_0002 = new[] { 15, 4, 5, 9, 5, 10, 10, 5, 8, 5, 15, 14 };
                    var tbTB_COLU_0002 = new[] { 10, 50, 50, 10 };
                    tbTB_0002.SetWidths(tbTB_COLU_0002);
                    tbTB_0002.WidthPercentage = 100f;


                    PdfPCell celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Nombres y Apellidos", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Cargo", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                   
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    //celCU_DOCU.BorderWidthTop = 0.5f;
                    //celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    //Nueva Linea
                    celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Evaluador", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0002.AddCell(celCU_DOCU);


                    //Nueva Linea
                    celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Cordinador", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0002.AddCell(celCU_DOCU);

                    #endregion

                    //NUEVA LINEA

                    celCU_DOCU = new PdfPCell(new Phrase("\n\n", oFONT_BOLD));
                    celCU_DOCU.Colspan = 4;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0002.AddCell(celCU_DOCU);


                    oDocument.Add(tbTB_0002);
                }

                oDocument.Close();
                pdfWrite.Close();

                bDocument = mStream.ToArray();

                return mStream;

                

            }

            /*Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Document_" + DateTime.Now.ToShortDateString().ToString() + ".pdf"));
            Response.BinaryWrite(bDocument);
            Response.End();*/


        }


        public void GenerarFicha06()
        {
            Font oFONT_TITULO = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 11);
            Font oFONT_TITULOxxx = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 8);
            Font oFONT_BOLD = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 9);
            Font oFONT_NORM = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 9);
            byte[] bDocument = null;

            using (MemoryStream mStream = new MemoryStream())
            {
                Document oDocument = new Document(PageSize.A4);
                PdfWriter pdfWrite = PdfWriter.GetInstance(oDocument, mStream);
                oDocument.Open();

                for (int i = 0; i < 1; i++)
                {
                    if (i > 0)
                    {
                        oDocument.NewPage();
                    }

                    #region Titulo

                    string sDE_RUTA_LOGO = System.Web.HttpContext.Current.Server.MapPath("~/Imagenes/") + "logo_sineace.png";

                    if (!string.IsNullOrEmpty(sDE_RUTA_LOGO))
                    {
                        try
                        {
                            Image gif;
                            gif = Image.GetInstance(File.ReadAllBytes(sDE_RUTA_LOGO));
                            gif.ScalePercent(80);
                            gif.SetAbsolutePosition(55, 775);
                            gif.ScaleAbsoluteWidth(100);
                            gif.ScaleAbsoluteHeight(40);
                            oDocument.Add(gif);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    PdfPTable tbTB_0001 = new PdfPTable(1);
                    var tbTB_COLU_0001 = new[] { 100 };
                    tbTB_0001.SetWidths(tbTB_COLU_0001);
                    tbTB_0001.WidthPercentage = 100f;



                    PdfPCell celCU_DOCU_CABE = new PdfPCell();



                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n\n\n Datos del evaluador y coordinador", oFONT_TITULO));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n", oFONT_TITULO));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n Nombre y Apellido de Candidato: " + "", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n Código del candidato: " + "", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    oDocument.Add(tbTB_0001);

                    #endregion

                    #region Cuerpo
                    PdfPTable tbTB_0002 = new PdfPTable(8);
                    //var tbTB_COLU_0002 = new[] { 15, 4, 5, 9, 5, 10, 10, 5, 8, 5, 15, 14 };
                    var tbTB_COLU_0002 = new[] { 15,15,15,15,15,15,15,15 };
                    tbTB_0002.SetWidths(tbTB_COLU_0002);
                    tbTB_0002.WidthPercentage = 100f;


                    PdfPCell celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("Código de NC", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Elemento", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;

                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Código de Instrumento", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Criterios de Desempeño", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    celCU_DOCU = new PdfPCell(new Phrase("Evidencias", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Técnica de Evaluación", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Instrumento de Evaluación", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Link de la Evidencia", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);




                    //Nueva Linea
                    celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU); ;


                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU); ;


                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    //Nueva Linea
                    celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU); ;


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Cordinador", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU); ;


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Cordinador", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    #endregion

                    //NUEVA LINEA

                    celCU_DOCU = new PdfPCell(new Phrase("\n\n", oFONT_BOLD));
                    celCU_DOCU.Colspan = 4;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0002.AddCell(celCU_DOCU);


                    oDocument.Add(tbTB_0002);
                }

                oDocument.Close();
                pdfWrite.Close();

                bDocument = mStream.ToArray();

                //return mStream;



            }

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Document_" + DateTime.Now.ToShortDateString().ToString() + ".pdf"));
            Response.BinaryWrite(bDocument);
            Response.End();


        }

        public void GenerarFicha07()
        {
            Font oFONT_TITULO = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 11);
            Font oFONT_TITULOxxx = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 8);
            Font oFONT_BOLD = new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, false), 9);
            Font oFONT_NORM = new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 9);
            byte[] bDocument = null;

            using (MemoryStream mStream = new MemoryStream())
            {
                Document oDocument = new Document(PageSize.A4);
                PdfWriter pdfWrite = PdfWriter.GetInstance(oDocument, mStream);
                oDocument.Open();

                for (int i = 0; i < 1; i++)
                {
                    if (i > 0)
                    {
                        oDocument.NewPage();
                    }

                    #region Titulo

                    string sDE_RUTA_LOGO = System.Web.HttpContext.Current.Server.MapPath("~/Imagenes/") + "logo_sineace.png";

                    if (!string.IsNullOrEmpty(sDE_RUTA_LOGO))
                    {
                        try
                        {
                            Image gif;
                            gif = Image.GetInstance(File.ReadAllBytes(sDE_RUTA_LOGO));
                            gif.ScalePercent(80);
                            gif.SetAbsolutePosition(55, 775);
                            gif.ScaleAbsoluteWidth(100);
                            gif.ScaleAbsoluteHeight(40);
                            oDocument.Add(gif);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    PdfPTable tbTB_0001 = new PdfPTable(1);
                    var tbTB_COLU_0001 = new[] { 100 };
                    tbTB_0001.SetWidths(tbTB_COLU_0001);
                    tbTB_0001.WidthPercentage = 100f;



                    PdfPCell celCU_DOCU_CABE = new PdfPCell();



                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n\n\n Datos del evaluador y coordinador", oFONT_TITULO));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n", oFONT_TITULO));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n Nombre y Apellido de Candidato: " + "", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n Código del candidato: " + "", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    celCU_DOCU_CABE = new PdfPCell();
                    celCU_DOCU_CABE = new PdfPCell(new Phrase("\n", oFONT_NORM));
                    celCU_DOCU_CABE.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU_CABE.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbTB_0001.AddCell(celCU_DOCU_CABE);

                    oDocument.Add(tbTB_0001);

                    #endregion

                    #region Cuerpo
                    PdfPTable tbTB_0002 = new PdfPTable(8);
                    //var tbTB_COLU_0002 = new[] { 15, 4, 5, 9, 5, 10, 10, 5, 8, 5, 15, 14 };
                    var tbTB_COLU_0002 = new[] { 15, 15, 15, 15, 15, 15, 15, 15 };
                    tbTB_0002.SetWidths(tbTB_COLU_0002);
                    tbTB_0002.WidthPercentage = 100f;


                    PdfPCell celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("Evidencias", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Elemento - Criterio", oFONT_BOLD));
                    celCU_DOCU.Colspan = 2;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;

                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Campo de Aplicación", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Código de IE", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    celCU_DOCU = new PdfPCell(new Phrase("Fecha y Hora", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Instrumento de Evaluación", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Estado de Cumplimiento", oFONT_BOLD));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);




                    //Nueva Linea
                    celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU); ;


                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 2;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU); ;


                    celCU_DOCU = new PdfPCell(new Phrase("Cesar Gonzales Solorzano", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    //Nueva Linea
                    celCU_DOCU = new PdfPCell();
                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU); ;


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 2;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Cordinador", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU); ;


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);


                    celCU_DOCU = new PdfPCell(new Phrase("Kareen Raquel Jimenez Cardozo", oFONT_NORM));
                    celCU_DOCU.Colspan = 1;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_LEFT;
                    celCU_DOCU.BorderWidthTop = 0.5f;
                    celCU_DOCU.BorderWidthLeft = 0.5f;
                    celCU_DOCU.BorderWidthBottom = 0.5f;
                    celCU_DOCU.BorderWidthRight = 0.5f;
                    tbTB_0002.AddCell(celCU_DOCU);

                    #endregion

                    //NUEVA LINEA

                    celCU_DOCU = new PdfPCell(new Phrase("\n\n", oFONT_BOLD));
                    celCU_DOCU.Colspan = 4;
                    celCU_DOCU.Border = PdfPCell.NO_BORDER;
                    celCU_DOCU.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbTB_0002.AddCell(celCU_DOCU);


                    oDocument.Add(tbTB_0002);
                }

                oDocument.Close();
                pdfWrite.Close();

                bDocument = mStream.ToArray();

                //return mStream;



            }

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Document_" + DateTime.Now.ToShortDateString().ToString() + ".pdf"));
            Response.BinaryWrite(bDocument);
            Response.End();


        }
    }
}