using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using WRun = DocumentFormat.OpenXml.Wordprocessing.Run;
using WBreak = DocumentFormat.OpenXml.Wordprocessing.Break;
using WText = DocumentFormat.OpenXml.Spreadsheet.Text;
using Assignment_4.Model;


namespace Assignment_4.Utilities
{
    public class Word
    {
        public static void Data(string baseUrl, string filePat)
        {
            string json = new WebClient().DownloadString(baseUrl);
            string fileName = @"D:\BDAT\myimage.jpg";            

            List<Users> items = JsonConvert.DeserializeObject<List<Users>>(json);
            using (WordprocessingDocument wordDocument =
                    WordprocessingDocument.Create(filePat, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Paragraph PageBreakParagraph = new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Break() { Type = BreakValues.Page }));
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                wordDocument.MainDocumentPart.Document.Body.Append(PageBreakParagraph);
                foreach (var item in items)
                {
                    //para = new Paragraph(new WRun(new WText(item.name)));
                    //Paragraph Para = new Paragraph(new Text(item.name));
                    //body.Append(Para);
                    ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                    using (FileStream stream = new FileStream(fileName, FileMode.Open))
                    {
                        imagePart.FeedData(stream);
                    }
                    AddImageToBody(wordDocument, mainPart.GetIdOfPart(imagePart));
                    para = new Paragraph(new WRun
                                 (new WBreak() { Type = BreakValues.Page }));
                    body.Append(para);
                }

                mainPart.Document.Save();
            }
        }

        private static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = 990000L, Cy = 792000L },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            // Append the reference to body, the element should be in a Run.
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        }
    }
    }

