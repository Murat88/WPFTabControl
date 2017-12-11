using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using DAL = OzyaysanDataEngine;


namespace OzyaysanBusinessEngine
{
    public class Stock : BaseClass
    {
        #region Fields
        private int m_ProductId;
        private int m_ColorId;
        private int m_Count;
        #endregion

        #region Field


        public int ProductId
        {
            get
            {
                return m_ProductId;
            }

            set
            {
                m_ProductId = value;
            }
        }

        public int ColorId
        {
            get
            {
                return m_ColorId;
            }

            set
            {
                m_ColorId = value;
            }
        }

        public int Count
        {
            get
            {
                return m_Count;
            }

            set
            {
                m_Count = value;
            }
        }
        #endregion
        #region Constructers

        public Stock()
            : this(0)
        {

        }

        public Stock(int PID)
        {
            this.ID = PID;
            IsVerified = this.Load(PID);
        }
        public Stock(int PID,int ColorID)
        {
            this.ProductId = PID;
            this.ColorId = ColorID;
            IsVerified = this.Load(PID,ColorID);
        }
        #endregion

        #region  Methots
        protected virtual bool Load(int PID)
        {

            bool bTemp = false;

            DataSet oDS;
            oDS = null;

            if (this.ID > 0)
            {

                try
                {


                    DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                    oDS = oSqlClientProvider.GetStockDetailByProductId(PID);
                    if (Utility.CheckDataSetState(oDS) && (Utility.CheckDataTableState(oDS.Tables[0])))
                    {
                        if (oDS.Tables[0].Rows.Count > 0)
                        {
                            bTemp = LoadOperatorFromDataRow(oDS.Tables[0].Rows[0], MinState);
                        }
                    }
                }
                catch (Exception Exp)
                {

                }
                finally
                {

                }
            }

            return bTemp;
        }
        protected virtual bool Load(int PID,int ColorID)
        {

            bool bTemp = false;

            DataSet oDS;
            oDS = null;

            if (PID > 0)
            {

                try
                {


                    DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                    oDS = oSqlClientProvider.GetStockDetailByProductIdAndColorId(PID, ColorID);
                    if (Utility.CheckDataSetState(oDS) && (Utility.CheckDataTableState(oDS.Tables[0])))
                    {
                        if (oDS.Tables[0].Rows.Count > 0)
                        {
                            bTemp = LoadOperatorFromDataRow(oDS.Tables[0].Rows[0], MinState);
                        }
                    }
                }
                catch (Exception Exp)
                {

                }
                finally
                {

                }
            }

            return bTemp;
        }
        private bool LoadOperatorFromDataRow(DataRow oRow, Enumarations.State nMinState)
        {
            bool bResult = false;
            if (oRow != null)
            {
                this.ID = Int32.Parse(oRow["Id"].ToString());
                this.ProductId = Int32.Parse(oRow["ProductId"].ToString());
                this.ColorId = Int32.Parse(oRow["ColorId"].ToString());
                this.Count= Int32.Parse(oRow["Count"].ToString());              
                bResult = true;
            }
            return bResult;
        }
        public int Save()
        {

            int nResult = -1;
            int nNewUID = -1;
            DataSet ds = new DataSet();
            using (TransactionScope scope1 = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();


                 //   nNewUID = oSqlClientProvider.SaveStock(this.ID, this.ProductId, this.ColorId, out nResult);

                    if (nResult == 0)
                    {
                        this.ID = nNewUID;
                        scope1.Complete();

                    }

                }
                catch (System.Data.SqlClient.SqlException Exp)
                {
                    //bool rethrow =
                    // ExceptionPolicy.HandleException(Exp, "StoreLocator Data Policy");

                    //if (rethrow)
                    //{
                    //    throw;
                    //}
                }
                catch (Exception Exp)
                {
                    //bool rethrow =
                    // ExceptionPolicy.HandleException(Exp, "StoreLocator Business Policy");

                    //if (rethrow)
                    //{
                    //    throw;
                    //}
                }
            }
            return nResult;
        }
        #endregion

        #region Static Methots
        public static DataSet getOperatorList(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet ds = null;
            try
            {

                DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                ds = oSqlClientProvider.GetOperatorListWithPagingAndSorting(strWhere, PageIndex, NumberofRowsRetrieved, SortColumn, SortOrder);
            }
            catch (System.Data.SqlClient.SqlException Exp)
            {
                //bool rethrow =
                // ExceptionPolicy.HandleException(Exp, "StoreLocator Data Policy");

                //if (rethrow)
                //{
                //    throw;
                //}
            }
            return ds;

        }
        public static DataTable GetOpearatorListForDDL()
        {
            DAL.DataProvider.SqlClientProvider oProvider = new DAL.DataProvider.SqlClientProvider();
            return oProvider.GetOperatorListForDDL().Tables[0];
        }
        #endregion
    }
}
