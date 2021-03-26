using System;
using System.Collections.Generic;
using System.Text;

namespace Printservice
{
    class ClientRequestViewModel
    {
        public string RequestType { get; set; }
        public string ConnectionString { get; set; }
        public string PrinterName { get; set; }
        public string PrintingType { get; set; }
        public string BillNo { get; set; }
        public string PatientId { get; set; }
        public string billMasterid { get; set; }
        //[JsonIgnore]
        public string pdfContent { get; set; }
        public string sampleNo { get; set; }
        public string userID { get; set; }
    }
}
