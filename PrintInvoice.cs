using System;
using System.Collections.Generic;
using System.Text;

namespace Printservice
{
    class PrintInvoice
    {
        public static int status = 0;
        public static int myconnection()
        {
            status = 0;
            //try
            //{
            //    SqlDataAdapter sccmd = new SqlDataAdapter("printgetstatus", myconn);
            //    sccmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            //    DataTable dt = new DataTable();
            //    sccmd.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {
            //        status = Convert.ToInt32(dt.Rows[0][0]);
            //    }
            //    else
            //    {
            //        status = 0;
            //    }
            //    return status;
            //}
            //catch
            //{
            //}
            return status;
        }
    }
}
