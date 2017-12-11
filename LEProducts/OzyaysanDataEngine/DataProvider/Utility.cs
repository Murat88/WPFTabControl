using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Configuration;


namespace OzyaysanDataEngine.DataProvider
{
    public static class Utility
    {
        public static bool CheckDataSetState(DataSet tempDS)
        {
            bool bResult = false;
            if (tempDS != null && tempDS.Tables.Count > 0)
            {
                bResult = true;
            }
            return bResult;
        }

        public static bool CheckDataTableState(DataTable tempDT)
        {
            bool bResult = false;
            if (tempDT != null && tempDT.Rows.Count > 0)
            {
                bResult = true;
            }
            return bResult;
        }
        internal static string GetDecryptedConnectionString()
        {
            string sResult;
            sResult = Cryptographer.DecryptString(ConfigurationManager.ConnectionStrings["LuckyEyeConnStr"].ToString(), "M1n0t3urK1nG");
            return sResult;
        }
       
    }

}
