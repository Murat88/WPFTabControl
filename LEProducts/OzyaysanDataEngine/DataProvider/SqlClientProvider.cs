using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Data.SqlClient;
using System.Configuration;

namespace OzyaysanDataEngine.DataProvider
{
    public class SqlClientProvider
    {
        protected Database GetDatabase()
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConfigurationManager.ConnectionStrings["connStr"].ToString());
           
            return (Database)db;
        }

        #region Product Related Part
        public int SaveProduct(int nID, string Name, string Code, string ExtraCode1, string ExtraCode2, string ExtraCode3, string ExtraCode4, int CategoryID, float diameter, float wirediameter, int num_of_forms, int num_of_turns, string rawmaterialcharacter, float StraighteningLength, decimal Price, float Weight, string Picture1, string Picture2, string Picture3, string Picture4, string TecniquePicture, string PDFDocument,int HatveArasi,int ToplamBoy,int KancaArasi,int Ictenice,string KancaYonu, int nState, int nUpdaterID, out int nErrorNo)
        {
            nErrorNo = 1;
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveProduct";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "PID", DbType.Int32, nID);
                db.AddInParameter(dbCommand, "PName", DbType.String, Name);
                db.AddInParameter(dbCommand, "Code", DbType.String, Code);
                db.AddInParameter(dbCommand, "ExtraCode1", DbType.String, ExtraCode1);
                db.AddInParameter(dbCommand, "ExtraCode2", DbType.String, ExtraCode2);
                db.AddInParameter(dbCommand, "ExtraCode3", DbType.String, ExtraCode3);
                db.AddInParameter(dbCommand, "ExtraCode4", DbType.String, ExtraCode4);
                db.AddInParameter(dbCommand, "Category", DbType.Int32, CategoryID);
                db.AddInParameter(dbCommand, "Diameter", DbType.Double, diameter);
                db.AddInParameter(dbCommand, "WireDiameter", DbType.Double, wirediameter);
                db.AddInParameter(dbCommand, "NumberOfForm", DbType.Int32, num_of_forms);
                db.AddInParameter(dbCommand, "NumberOfTurns", DbType.Int32, num_of_turns);
                db.AddInParameter(dbCommand, "RawMaterialsCharacteristic", DbType.String, rawmaterialcharacter);
                db.AddInParameter(dbCommand, "StraighteningLength", DbType.Double, StraighteningLength);
                db.AddInParameter(dbCommand, "Price", DbType.Decimal, Price);
                db.AddInParameter(dbCommand, "Weight", DbType.Double, Weight);
                db.AddInParameter(dbCommand, "Picture1", DbType.String, Picture1);
                db.AddInParameter(dbCommand, "Picture2", DbType.String, Picture2);
                db.AddInParameter(dbCommand, "Picture3", DbType.String, Picture3);
                db.AddInParameter(dbCommand, "Picture4", DbType.String, Picture4);
                db.AddInParameter(dbCommand, "TecniquePicture", DbType.String, TecniquePicture);
                db.AddInParameter(dbCommand, "PDFDocument", DbType.String, "");
                //db.AddInParameter(dbCommand, "HatveArasi", DbType.Int32, HatveArasi);
                //db.AddInParameter(dbCommand, "ToplamBoy", DbType.Int32, ToplamBoy);
                //db.AddInParameter(dbCommand, "KancaArasi", DbType.Int32, KancaArasi);
                //db.AddInParameter(dbCommand, "Ictenice", DbType.Int32, Ictenice);
                //db.AddInParameter(dbCommand, "KancaYonu", DbType.String, KancaYonu);
                db.AddInParameter(dbCommand, "UpdaterUID", DbType.Int32, nUpdaterID);
                db.AddInParameter(dbCommand, "State", DbType.Int32, nState);


                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nErrorNo = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["PID"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public virtual DataSet GetProductListWithPagingAndSorting(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetProductListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public virtual DataSet GetProductDetailByPID(int PID, int State)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetProductDetailByPID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "PID", DbType.String, PID);
                //db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                //db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                //db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                //db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public DataSet GetProductListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetProductListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        public DataSet CalculateProductionTimeByPIDAndCount(int PID,int Count)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();
               
                string sqlCommand = "LP_Ozyaysan_CalculateProductionTime";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                db.AddInParameter(dbCommand, "Count", DbType.Int32, Count);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        public virtual DataSet GetSoldProductListWithPagingAndSorting(string strWhere,string strHaving, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetSoldProductListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "@HavingStr", DbType.String, strHaving);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
                
        #endregion

        #region Category Related Part
        public DataSet GetProductCategoryListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetProductCategoryListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        #endregion
       
        #region State Related Part
        public DataSet GetStateListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetStateListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        public DataSet GetStateListForDDLWithoutChose()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetStateListForDDLWithoutChose";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        #endregion

        #region Customer Related part
        public DataSet GetCustomerDetailByCID(int CID, int State)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetCustomerDetailByCID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "CID", DbType.String, CID);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public int SaveCustomer(int nID, string Name,string Country,string City,string Email,string Address, int nState, int nUpdaterID, out int nErrorNo)
        {
            nErrorNo = 1;
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveCustomer";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "CID", DbType.Int32, nID);
                db.AddInParameter(dbCommand, "CName", DbType.String, Name);
                db.AddInParameter(dbCommand, "Country", DbType.String, Country);
                db.AddInParameter(dbCommand, "City", DbType.String, City);
                db.AddInParameter(dbCommand, "Email", DbType.String, Email);
                db.AddInParameter(dbCommand, "Address", DbType.String, Address);
                db.AddInParameter(dbCommand, "UpdaterUID", DbType.Int32, nUpdaterID);
                db.AddInParameter(dbCommand, "State", DbType.Int32, nState);


                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nErrorNo = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["CID"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public virtual DataSet GetCustomerListWithPagingAndSorting(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetCustomerListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public DataSet GetCustomerListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetCustomerListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        #endregion

        #region Color Related Part
        public DataSet GetColorDetailByCID(int CID, int State)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetColorDetailByCID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "CID", DbType.String, CID);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public int SaveColor(int nID, string Name,string RGBCode, int nState, int nUpdaterID, out int nErrorNo)
        {
            nErrorNo = 1;
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveColor";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "CID", DbType.Int32, nID);
                db.AddInParameter(dbCommand, "CName", DbType.String, Name);
                db.AddInParameter(dbCommand, "RGBCode", DbType.String, RGBCode);
                db.AddInParameter(dbCommand, "UpdaterUID", DbType.Int32, nUpdaterID);
                db.AddInParameter(dbCommand, "State", DbType.Int32, nState);


                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nErrorNo = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["CID"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public virtual DataSet GetColorListWithPagingAndSorting(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetColorListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public DataSet GetColorListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetColorListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        #endregion

        #region Order Realated Part

        public DataSet GetOrderDetailByOID(int OID, int State)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetOrderDetailByOID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "OID", DbType.String, OID);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public int SaveOrder(int nID, int Customer, int OrderStatus, DateTime DateOfDelivery,string Products,int EstimatedTotalProductionTime,decimal TotalCost, int nState, int nUpdaterID, out int nErrorNo)
        {
            nErrorNo = 1;
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveOrder";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "OID", DbType.Int32, nID);

                db.AddInParameter(dbCommand, "Customer", DbType.Int32, Customer);
                db.AddInParameter(dbCommand, "OrderStatus", DbType.Int32, OrderStatus);
                db.AddInParameter(dbCommand, "DateOfDelivery", DbType.DateTime, DateOfDelivery);
                db.AddInParameter(dbCommand, "Products", DbType.String, Products);
                db.AddInParameter(dbCommand, "EstimatedTotalProductionTime", DbType.Int32, EstimatedTotalProductionTime);
                db.AddInParameter(dbCommand, "TotalCost", DbType.Decimal, TotalCost);
                db.AddInParameter(dbCommand, "UpdaterUID", DbType.Int32, nUpdaterID);
                db.AddInParameter(dbCommand, "State", DbType.Int32, nState);

                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nErrorNo = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["OID"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public virtual DataSet GetOrderListWithPagingAndSorting(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetOrderListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public DataSet GetOrderListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetOrderListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        public int SaveOrderDetail(int OID, int PID, int OrderCount, int UsedStockCountColored, int UsedStockCountNonColored, int ProductionCount, int CID, string PackageType,int OPStatus)
        {
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {          
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveOrderDetail";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "OID", DbType.Int32, OID);
                db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                db.AddInParameter(dbCommand, "CID", DbType.Int32, CID);
                db.AddInParameter(dbCommand, "OrderCount", DbType.Int32, OrderCount);
                db.AddInParameter(dbCommand, "UsedStockCountColored", DbType.Int32, UsedStockCountColored);
                db.AddInParameter(dbCommand, "UsedStockCountNonColored", DbType.Int32, UsedStockCountNonColored);
                db.AddInParameter(dbCommand, "ProductionCount", DbType.Int32, ProductionCount);
                db.AddInParameter(dbCommand, "PackageType", DbType.String, PackageType);
                db.AddInParameter(dbCommand, "OPStatus", DbType.Int32, OPStatus);
                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());

                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public int DeleteOrderDetail(int OID)
        {
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_DeleteOrderDetail";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "OID", DbType.Int32, OID);
                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());

                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public int UpdateOrderDetail(int OID, int PID, int OrderCount, int UsedStockCountColored, int UsedStockCountNonColored, int ProductionCount, int CID, string PackageType, int OPStatus, int Operator ,int ProductedAmount)
        {
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_UpdateOrderDetail";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "OID", DbType.Int32, OID);
                db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                db.AddInParameter(dbCommand, "CID", DbType.Int32, CID);
                db.AddInParameter(dbCommand, "Count", DbType.Int32, OrderCount);
                db.AddInParameter(dbCommand, "Count", DbType.Int32, UsedStockCountColored);
                db.AddInParameter(dbCommand, "Count", DbType.Int32, UsedStockCountNonColored);
                db.AddInParameter(dbCommand, "Count", DbType.Int32, ProductionCount);
                db.AddInParameter(dbCommand, "PackageType", DbType.String, PackageType);
                db.AddInParameter(dbCommand, "OPStatus", DbType.Int32, OPStatus);
                db.AddInParameter(dbCommand, "Operator", DbType.Int32, Operator);
                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());

                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public int UpdateOrderDetailForStock(int OID, int PID, int IsStockUsed,int UsedStockAmount)
        {
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_UpdateOrderDetailForStock";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "OID", DbType.Int32, OID);
                db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                db.AddInParameter(dbCommand, "IsUsedStock", DbType.Int32, IsStockUsed);
                db.AddInParameter(dbCommand, "UsedStockAmount", DbType.Int32, UsedStockAmount);
            
                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());

                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public int UpdateOrderDetailForRawMaterials(int OID, int PID, int IsRawMaterialsUsed, int UsedRawMaterialsAmount, int RawMaterialsID)
        {
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_UpdateOrderDetailForRawMaterials";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "OID", DbType.Int32, OID);
                db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                db.AddInParameter(dbCommand, "IsUsedRawMaterials", DbType.Int32, IsRawMaterialsUsed);
                db.AddInParameter(dbCommand, "UsedRawMaterialsAmount", DbType.Int32, UsedRawMaterialsAmount);
                db.AddInParameter(dbCommand, "RawMaterialsID", DbType.Int32, RawMaterialsID);
                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());

                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public DataSet GetOrderDetailListByOrderID(int OID)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetOrderDetailListByOrderID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "@OID", DbType.String, OID);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        #endregion

        #region Machine Related part
       
        public DataSet GetMachineDetailByMID(int MID, int State)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetMachineDetailByMID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "MID", DbType.String, MID);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public int SaveMachine(int nID, string Name, int nState, int nUpdaterID, out int nErrorNo)
        {
            nErrorNo = 1;
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveMachine";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "MID", DbType.Int32, nID);
                db.AddInParameter(dbCommand, "MCode", DbType.String, Name);
                db.AddInParameter(dbCommand, "UpdaterUID", DbType.Int32, nUpdaterID);
                db.AddInParameter(dbCommand, "State", DbType.Int32, nState);


                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nErrorNo = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["MID"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public virtual DataSet GetMachineListWithPagingAndSorting(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetMachineListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public DataSet GetMachineListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetMachineListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        public int SaveProductionPhase(int PID,int MID,float PTime)
        {
           
           int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveProductionPhase";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                db.AddInParameter(dbCommand, "MID", DbType.Int32, MID);
                db.AddInParameter(dbCommand, "PTime", DbType.Double, PTime);
                


                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());                 
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        #endregion

        #region RawMaterials RelatedPart
        public DataSet GetRawMaterialsDetailByID(int Id, int State)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetRawMaterialsDetailByMID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "Id", DbType.Int32, Id);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public int SaveRawMaterials(int nID, int WireDiameterr,int Hardness,string Compound,int Amount,string Quality,string SurfaceCondition, int nState, int nUpdaterID, out int nErrorNo)
        { 
            nErrorNo = 1;
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveRawMaterials";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "Id", DbType.Int32, nID);
                db.AddInParameter(dbCommand, "WireDiameter", DbType.Int32, WireDiameterr);
                db.AddInParameter(dbCommand, "Hardness", DbType.Int32, Hardness);
                db.AddInParameter(dbCommand, "Compound", DbType.String, Compound);
                db.AddInParameter(dbCommand, "Amount", DbType.Int32, Amount);
                db.AddInParameter(dbCommand, "Quality", DbType.String, Quality);
                db.AddInParameter(dbCommand, "SurfaceCondition", DbType.String, SurfaceCondition);
                db.AddInParameter(dbCommand, "UpdaterID", DbType.Int32, nUpdaterID);
                db.AddInParameter(dbCommand, "State", DbType.Int32, nState);


                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nErrorNo = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["Id"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public virtual DataSet GetRawMaterialsListWithPagingAndSorting(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetRawMaterialsListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public DataSet GetRawMaterialsListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetRawMaterialsListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
       
        #endregion

        #region Operator Related Part

        public DataSet GetOperatorDetailByOPID(int OPID, int State)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetOperatorDetailByOPID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "OPID", DbType.String, OPID);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public int SaveOperator(int nID, string Name, string lastname , int nState, int nUpdaterID, out int nErrorNo)
        {
            nErrorNo = 1;
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveOperator";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "OPID", DbType.Int32, nID);
                db.AddInParameter(dbCommand, "OPName", DbType.String, Name);
                db.AddInParameter(dbCommand, "OPLastName", DbType.String, lastname);
                db.AddInParameter(dbCommand, "UpdaterUID", DbType.Int32, nUpdaterID);
                db.AddInParameter(dbCommand, "State", DbType.Int32, nState);


                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nErrorNo = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["OPID"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
        public virtual DataSet GetOperatorListWithPagingAndSorting(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetOperatorListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public DataSet GetOperatorListForDDL()
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetOperatorListForDDL";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Data Policy");
                //if (rethrow)
                //{
                //    throw;
                //}
            }
            catch (Exception Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "G2M Business Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return tempDS;
        }
        #endregion

        #region Paint Product Related Part
                public virtual DataSet GetPaintProductListWithPagingAndSorting(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetPaintProductListWithPagingAndSorting";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


                db.AddInParameter(dbCommand, "@WhereStr", DbType.String, strWhere);
                db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
                db.AddInParameter(dbCommand, "NumRows", DbType.Int32, NumberofRowsRetrieved);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.Int32, SortOrder);


                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
                public int ChangeOrderProductPaintStatus(int OID, int PID, int Status,int PaintedAmount,int DeliveredAmount)
                {
                    int nResult = -1;
                    DataSet oDS = new DataSet();
                    try
                    {
                        Database db = GetDatabase();

                        string sqlCommand = "LP_Ozyaysan_ChangeOrderProductPaintStatus";
                        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                        db.AddInParameter(dbCommand, "OID", DbType.Int32, OID);
                        db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                        db.AddInParameter(dbCommand, "Status", DbType.Int32, Status);
                        db.AddInParameter(dbCommand, "PaintedAmount", DbType.Int32, PaintedAmount);
                        db.AddInParameter(dbCommand, "DeliveredAmount", DbType.Int32, DeliveredAmount);
                        oDS = db.ExecuteDataSet(dbCommand);

                        if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                        {
                            nResult = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());

                        }
                    }
                    catch (System.Data.SqlClient.SqlException Exp)
                    {
                        bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                        if (rethrow)
                        {
                            throw;
                        }
                    }
                    catch (Exception Exp)
                    {
                        bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                        if (rethrow)
                        {
                            throw;
                        }
                    }
                    return nResult;
                }
        #endregion

        #region User Related Part
         public DataSet GetUserDetailByUID(int UID, int State)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetUserDetailByUID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "UID", DbType.String, UID);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
      public int SaveUser(int nID, string Name,string Password , out int nErrorNo)
        {
            nErrorNo = 1;
            int nResult = -1;
            DataSet oDS = new DataSet();
            try
            {
                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_SaveUser";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "UID", DbType.Int32, nID);
                db.AddInParameter(dbCommand, "UName", DbType.String, Name);
                db.AddInParameter(dbCommand, "UPassword", DbType.String, Password);
             


                oDS = db.ExecuteDataSet(dbCommand);

                if (Utility.CheckDataSetState(oDS) && Utility.CheckDataTableState(oDS.Tables[0]))
                {
                    nErrorNo = Int32.Parse(oDS.Tables[0].Rows[0]["hata_no"].ToString());
                    nResult = Int32.Parse(oDS.Tables[0].Rows[0]["UID"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow = ExceptionPolicy.HandleException(Exp, " Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return nResult;
        }
      public virtual DataSet VerifyPassword(string UName, string UPassword)
      {
          DataSet tempDS = new DataSet();
          try
          {

              Database db = GetDatabase();

              string sqlCommand = "LP_Ozyaysan_VerifyPassword";
              DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


              db.AddInParameter(dbCommand, "@UName", DbType.String, UName);
              db.AddInParameter(dbCommand, "@UPassword", DbType.String, UPassword);
             


              tempDS = db.ExecuteDataSet(dbCommand);
          }
          catch (System.Data.SqlClient.SqlException Exp)
          {
              bool rethrow =
               ExceptionPolicy.HandleException(Exp, "Data Policy");
              if (rethrow)
              {
                  throw;
              }
          }
          catch (Exception Exp)
          {
              bool rethrow =
               ExceptionPolicy.HandleException(Exp, "Business Policy");

              if (rethrow)
              {
                  throw;
              }
          }
          return tempDS;
      }

        #endregion

        #region Stock Related Part
        public DataSet GetStockDetailByProductIdAndColorId(int PID, int ColorId)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetStockDetailByProductIdAndColorId";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                db.AddInParameter(dbCommand, "CID", DbType.Int32, ColorId);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        public DataSet GetStockDetailByProductId(int PID)
        {
            DataSet tempDS = new DataSet();
            try
            {

                Database db = GetDatabase();

                string sqlCommand = "LP_Ozyaysan_GetStockDetailByProductId";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "PID", DbType.Int32, PID);
                tempDS = db.ExecuteDataSet(dbCommand);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Data Policy");
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception Exp)
            {
                bool rethrow =
                 ExceptionPolicy.HandleException(Exp, "Business Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            return tempDS;
        }
        #endregion
    }
}
