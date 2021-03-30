using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp.Server;
using WebSocketSharp;
using Newtonsoft.Json;
using PdfiumViewer;
using System.Drawing.Printing;
using System.Drawing;

namespace Printservice
{
    class PrintServer : WebSocketBehavior
    {

     

        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Print Received");
            var clientRequest = JsonConvert.DeserializeObject<ClientRequestViewModel>(e.Data);
          
            if (PrintInvoice.myconnection() == 0)
            {

                if (clientRequest.RequestType.ToLower() == "printerlist")
                {
                    Sessions.Broadcast(PrinterList());
                }
                else
                {

                    var fileName = Guid.NewGuid() + ".pdf";
                    clientRequest.pdfContent = clientRequest.pdfContent.Replace("data:application/pdf;base64,", "").Trim();
                    byte[] bytes = Convert.FromBase64String(clientRequest.pdfContent);
                    System.IO.File.WriteAllBytes(fileName, bytes);
                   
                    var path = fileName;
                    using (var document = PdfDocument.Load(fileName,"kamal"))
                    {
                        
                        var pagesize = document.PageSizes;
                        Size size = pagesize[0].ToSize();
                        Console.WriteLine(size.Width.ToString());
                        Console.WriteLine(size.Height.ToString());
                        using (var printDocument = document.CreatePrintDocument())
                        {
                            
                            PaperSize sizeofpaper = new PaperSize("custom", size.Width, size.Height);
                            sizeofpaper.RawKind = (int)PaperKind.Custom;
                            printDocument.DefaultPageSettings.PaperSize = sizeofpaper;
                            printDocument.PrinterSettings.PrinterName = clientRequest.PrinterName;
                            printDocument.DocumentName = "file.pdf";
                            printDocument.PrinterSettings.PrintFileName = "file.pdf";
                            printDocument.PrintController = new StandardPrintController();
                            printDocument.Print();
                            

                        }  
                    }
                    
                    Sessions.Broadcast("Successfully Printed to : " + clientRequest.PrinterName);
                    System.IO.File.Delete(path);
                }
            }
            else
            {
                Console.WriteLine("Print Not Authorized");
            }
            Console.WriteLine("Print Success");
        }

        private string PrinterList()
        {
            var printerLst = "printerlist";
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printerLst += "___" + printer;
            }
            return printerLst;
        }
        
    }
}
