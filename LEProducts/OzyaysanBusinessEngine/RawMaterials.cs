using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using DAL = OzyaysanDataEngine;

namespace OzyaysanBusinessEngine
{
    public class RawMaterials: BaseClass
    {
        private string m_CoilNo = "";
        #region Fields
        public int WireDiameter { get; set; }
        public int Hardness { get; set; }
        public string Compound { get; set; }
        public int Amount { get; set; }
        public string Quality { get; set; }
        public string SurfaceCondition { get; set; }
        public string RegistryNo { get; set; }
        public string CoilNo { get { return this.m_CoilNo; } set { this.m_CoilNo =value; } }
        public decimal BeginningInventory { get; set; }
        public decimal CurrentInventory { get; set; }
        #endregion

    

        #region Constructers
        public RawMaterials()
            : this(0, Enumarations.State.Aktif)
        {

        }

        public RawMaterials(int Id, Enumarations.State MinState)
        {
            this.ID = Id;
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
                    oDS = oSqlClientProvider.GetRawMaterialsDetailByID(this.ID,(int)MinState);
                    if (Utility.CheckDataSetState(oDS) && (Utility.CheckDataTableState(oDS.Tables[0])))
                    {
                        if (oDS.Tables[0].Rows.Count > 0)
                        {
                            bTemp = LoadRawMaterialsFromDataRow(oDS.Tables[0].Rows[0], MinState);
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
        private bool LoadRawMaterialsFromDataRow(DataRow oRow, Enumarations.State nMinState)
        {
            bool bResult = false;
            if (oRow != null)
            {
                this.ID = Int32.Parse(oRow["Id"].ToString());
                this.WireDiameter = Int32.Parse(oRow["WireDiameter"].ToString());
                this.Hardness = Int32.Parse(oRow["Hardness"].ToString());
                this.Amount = Int32.Parse(oRow["Amount"].ToString());
                this.Compound = oRow["Compound"].ToString();
                this.Quality = oRow["Quality"].ToString();
                this.SurfaceCondition = oRow["SurfaceCondition"].ToString();
                this.State = (Enumarations.State)(Int32.Parse(oRow["State"].ToString()));               
                this.CreationUserID = Int32.Parse(oRow["CreatorID"].ToString());
                this.CreationDate = Convert.ToDateTime(oRow["CreationDate"].ToString()).ToLocalTime();
                this.UpdateUserID = Int32.Parse(oRow["UpdaterID"].ToString());
                this.UpdateDate = Convert.ToDateTime(oRow["UpdateDate"].ToString()).ToLocalTime();
                
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


                    nNewUID = oSqlClientProvider.SaveRawMaterials(this.ID, this.WireDiameter,this.Hardness,this.Compound,this.Amount,this.Quality,this.SurfaceCondition, (int)this.State, this.UpdateUserID, out nResult);

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
        public static DataSet getRawMaterialsList(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet ds = null;
            try
            {

                DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                ds = oSqlClientProvider.GetRawMaterialsListWithPagingAndSorting(strWhere, PageIndex, NumberofRowsRetrieved, SortColumn, SortOrder);
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
        public static DataTable GetRawMaterialsListForDDL()
        {
            DAL.DataProvider.SqlClientProvider oProvider = new DAL.DataProvider.SqlClientProvider();
            return oProvider.GetRawMaterialsListForDDL().Tables[0];
        }
       
        #endregion
    }
}
