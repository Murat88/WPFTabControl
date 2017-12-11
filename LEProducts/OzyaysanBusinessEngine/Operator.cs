using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using DAL = OzyaysanDataEngine;


namespace OzyaysanBusinessEngine
{
   public class Operator : BaseClass
    {
        #region Fields
        private string m_LastName;
        #endregion

        #region Field
        public string LastName
        {
            get { return m_LastName; }
            set { m_LastName = value; }
        }
        #endregion
        #region Constructers

        public Operator()
            : this(0, Enumarations.State.Aktif)
        {

        }

        public Operator(int OPID, Enumarations.State MinState)
        {
            this.ID = OPID;
            IsVerified = this.Load(MinState);
        }
        #endregion

        #region  Methots
        protected virtual bool Load(Enumarations.State MinState)
        {

            bool bTemp = false;

            DataSet oDS;
            oDS = null;

            if (this.ID > 0)
            {

                try
                {


                    DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                    oDS = oSqlClientProvider.GetOperatorDetailByOPID(this.ID, (int)MinState);
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
                this.ID = Int32.Parse(oRow["OPID"].ToString());
                this.Name = oRow["OPName"].ToString();
                this.LastName = oRow["OPLastName"].ToString();
                this.State = (Enumarations.State)(Int32.Parse(oRow["State"].ToString()));
                this.OrderNo = Int32.Parse(oRow["OrderNo"].ToString());
                this.CreationUserID = Int32.Parse(oRow["CreatorUID"].ToString());
                this.CreationDate = Convert.ToDateTime(oRow["CreationDate"].ToString()).ToLocalTime();
                this.CreationIP = oRow["CreatorIP"].ToString();
                this.UpdateUserID = Int32.Parse(oRow["UpdaterUID"].ToString());
                this.UpdateDate = Convert.ToDateTime(oRow["UpdateDate"].ToString()).ToLocalTime();
                this.UpdateIP = oRow["UpdaterIP"].ToString();
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


                    nNewUID = oSqlClientProvider.SaveOperator(this.ID, this.Name,this.LastName, (int)this.State, this.UpdateUserID, out nResult);

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
