using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using DAL = OzyaysanDataEngine;

namespace OzyaysanBusinessEngine
{
    public class Order : BaseClass
    {
        #region Fields
        private int m_CustomerID;
        private Customer m_Customer;
        private Enumarations.OrderStatus m_OrderStatus;
        private DateTime m_DateOfDelivery;
        private string m_Products;
        private string m_DateOfDelivery2;
        private int m_EstimatedTotalProductionTime;
        private decimal m_TotalCost;
        private string m_OrderNo;




        #endregion

        #region Properties
        public decimal TotalCost
        {
            get { return m_TotalCost; }
            set { m_TotalCost = value; }
        }

        public int EstimatedTotalProductionTime
        {
            get { return m_EstimatedTotalProductionTime; }
            set { m_EstimatedTotalProductionTime = value; }
        }
        public string DateOfDelivery2
        {
            get { return m_DateOfDelivery2; }
            set { m_DateOfDelivery2 = value; }
        }
        public string Products
        {
            get { return m_Products; }
            set { m_Products = value; }
        }
        public int CustomerID
        {
            get { return m_CustomerID; }
            set { m_CustomerID = value; }
        }
        internal Customer Customer
        {
            get { return m_Customer; }
            set { m_Customer = value; }
        }
        public Enumarations.OrderStatus OrderStatus
        {
            get { return m_OrderStatus; }
            set { m_OrderStatus = value; }
        }
        public DateTime DateOfDelivery
        {
            get { return m_DateOfDelivery; }
            set { m_DateOfDelivery = value; }
        }

        public string OrderNo
        {
            get
            {
                return m_OrderNo;
            }

            set
            {
                m_OrderNo = value;
            }
        }
        #endregion

        #region Constructers
        public Order()
            : this(0, Enumarations.State.Aktif)
        {

        }

        public Order(int OID, Enumarations.State MinState)
        {
            this.ID = OID;
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
                    oDS = oSqlClientProvider.GetOrderDetailByOID(this.ID, (int)MinState);
                    if (Utility.CheckDataSetState(oDS) && (Utility.CheckDataTableState(oDS.Tables[0])))
                    {
                        if (oDS.Tables[0].Rows.Count > 0)
                        {
                            bTemp = LoadOrderFromDataRow(oDS.Tables[0].Rows[0], MinState);
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
        private bool LoadOrderFromDataRow(DataRow oRow, Enumarations.State nMinState)
        {
            bool bResult = false;
            if (oRow != null)
            {
                this.ID = Int32.Parse(oRow["OID"].ToString());
                this.OrderNo = oRow["OrderNo"].ToString();
                this.CustomerID = Int32.Parse(oRow["Customer"].ToString());
                this.OrderStatus = (Enumarations.OrderStatus)Int32.Parse(oRow["OrderStatus"].ToString());
                this.DateOfDelivery = Convert.ToDateTime(oRow["DateOfDelivery"]);
                this.Products = oRow["Products"].ToString();
                this.EstimatedTotalProductionTime = Int32.Parse(oRow["EstimatedTotalProductionTime"].ToString());
                this.State = (Enumarations.State)(Int32.Parse(oRow["State"].ToString()));
                this.TotalCost = decimal.Parse(oRow["TotalCost"].ToString());
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

            DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();


            nNewUID = oSqlClientProvider.SaveOrder(this.ID, this.CustomerID, (int)this.OrderStatus, this.DateOfDelivery, this.Products, this.EstimatedTotalProductionTime, this.TotalCost, (int)State, this.UpdateUserID, out nResult);

            if (nResult == 0)
            {
                this.ID = nNewUID;
            }

            return nResult;
        }

        #endregion

        #region Static Methots
        public static DataSet getOrderList(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet ds = null;
            try
            {

                DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                ds = oSqlClientProvider.GetOrderListWithPagingAndSorting(strWhere, PageIndex, NumberofRowsRetrieved, SortColumn, SortOrder);
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
        public static DataTable GetOrderListForDDL()
        {
            DAL.DataProvider.SqlClientProvider oProvider = new DAL.DataProvider.SqlClientProvider();
            return oProvider.GetOrderListForDDL().Tables[0];
        }
        public static DataTable GetOrderDetailListByOrderID(int OID)
        {
            DAL.DataProvider.SqlClientProvider oProvider = new DAL.DataProvider.SqlClientProvider();
            return oProvider.GetOrderDetailListByOrderID(OID).Tables[0];
        }
        #endregion

        #region OrderDetail Related Part

        public static int SaveOrderDetail(int OID, int PID, int OrderCount, int UsedStockCountColored, int UsedStockCountNonColored, int ProductionCount, int CID, string PackageType, int OPStatus)
        {                    
            DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
            return  oSqlClientProvider.SaveOrderDetail(OID, PID, OrderCount, UsedStockCountColored, UsedStockCountNonColored, ProductionCount, CID, PackageType, OPStatus);            
        }
        public static int DeleteOrderDetail(int OID)
        {
            DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
            return oSqlClientProvider.DeleteOrderDetail(OID);
        }
        public static int UpdateOrderDetail(int OID, int PID, int OrderCount, int UsedStockCountColored, int UsedStockCountNonColored, int ProductionCount, int CID, string PackageType, int OPStatus, int Operator, int ProductedAmount)
        {

            int nResult = -1;

            DataSet ds = new DataSet();
            using (TransactionScope scope1 = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();


                    nResult = oSqlClientProvider.UpdateOrderDetail(OID, PID,  OrderCount,  UsedStockCountColored,  UsedStockCountNonColored,  ProductionCount, CID, PackageType, OPStatus, Operator, ProductedAmount);

                    if (nResult == 0)
                    {
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
        public static int ChangeOrderProductPaintStatus(int OID, int PID, int Status, int PaintedAmount, int DeliveredAmount)
        {
            int nResult = -1;

            DataSet ds = new DataSet();
            using (TransactionScope scope1 = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();


                    nResult = oSqlClientProvider.ChangeOrderProductPaintStatus(OID, PID, Status, PaintedAmount, DeliveredAmount);

                    if (nResult == 0)
                    {
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
        public static int UpdateOrderDetailForStock(int OID, int PID, int IsStockUsed, int UsedStockAmount)
        {

            int nResult = -1;

            DataSet ds = new DataSet();
            using (TransactionScope scope1 = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();


                    nResult = oSqlClientProvider.UpdateOrderDetailForStock(OID, PID, IsStockUsed, UsedStockAmount);

                    if (nResult == 0)
                    {
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
        public static int UpdateOrderDetailForRawMaterials(int OID, int PID, int IsRawMaterialsUsed, int UsedRawMaterialsAmount, int RawMaterialsID)
        {

            int nResult = -1;

            DataSet ds = new DataSet();
            using (TransactionScope scope1 = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();


                    nResult = oSqlClientProvider.UpdateOrderDetailForRawMaterials(OID, PID, IsRawMaterialsUsed, UsedRawMaterialsAmount, RawMaterialsID);

                    if (nResult == 0)
                    {
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

    }
}
