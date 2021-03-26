using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp.Server;
using WebSocketSharp;
using Newtonsoft.Json;
using PdfiumViewer;
using System.Drawing.Printing;
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
                    using (var document = PdfDocument.Load(fileName))
                    {
                        
                        using (var printDocument = document.CreatePrintDocument())
                        {
                            //printDocument.PrinterSettings.PrintFileName = "Letter_SkidTags_Report_9ae93aa7-4359-444e-a033-eb5bf17f5ce6.pdf";
                            
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
