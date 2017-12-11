using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL = OzyaysanDataEngine;
namespace OzyaysanBusinessEngine
{
    public class Category:BaseClass
    {
        public Category()
        {
        
        }

        #region Static Methots
        public static DataTable GetProductCategoryListForDDL()
        {
            DAL.DataProvider.SqlClientProvider oProvider = new DAL.DataProvider.SqlClientProvider();
            return  oProvider.GetProductCategoryListForDDL().Tables[0];
        }
        #endregion
    }
}
