using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Web;
using System.IO;
using System.Xml;
using DAL = OzyaysanDataEngine;
namespace OzyaysanBusinessEngine
{
    public sealed class Utility
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
        public static bool IsNumeric(string expression)
        {
            bool hasDecimal = false;
            if (expression != string.Empty && expression.Trim().Length > 0)
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '.')
                    {
                        if (hasDecimal)
                            return false;
                        else
                        {
                            hasDecimal = true;
                            continue;
                        }
                    }
                    if (!char.IsNumber(expression[i]))
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
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
        #region State Related Part
        public static DataTable GetStateListForDDL()
        {
            DataSet oDS = new DataSet();
            DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
            oDS = oSqlClientProvider.GetStateListForDDL();
            return oDS.Tables[0];
        }
        public static DataTable GetStateListForDDLWithoutChose()
        {
            DataSet oDS = new DataSet();
            DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
            oDS = oSqlClientProvider.GetStateListForDDLWithoutChose();
            return oDS.Tables[0];
        }
        #endregion

     
    }
}
