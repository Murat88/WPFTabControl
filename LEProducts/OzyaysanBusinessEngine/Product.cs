using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using DAL = OzyaysanDataEngine;
namespace OzyaysanBusinessEngine
{
    public class Product : BaseClass
    {

        #region Fields
        private Category m_Category;
        private int m_CategoryID;       
        private int m_OrderCount;
        private Color m_Color;
        private int m_UsedStockCountColored;
        private int m_UsedStockCountNonColored;
        private int m_ProductionCount;
        private string m_CurrentPackageType;
        private decimal m_CurrentSellPrice;
        private string m_Code;
        private string m_ExtraCode1;
        private string m_ExtraCode2;
        private string m_ExtraCode3;
        private string m_ExtraCode4;
        private int m_NumberOfTurns;
        private int m_NumberOfForm;
        private float m_Diameter;
        private float m_WireDiameter;
        private float m_StraighteningLength;
        private float m_Weight;
        private string m_RawMaterialsCharacteristic;
        private decimal m_Price;
        private string m_Picture1;
        private string m_Picture2;
        private string m_Picture3;
        private string m_Picture4;
        private string m_TecniquePicture;
        private string m_PDFDocument;
        private int m_CurrentOPStatus;
        private int m_HatveArasi;
        private int m_ToplamBoy;
        private int m_KancaArasi;
        private int m_Ictenice;
        private string m_KancaYonu = "";


        #endregion

        #region Properties
        public decimal CurrentSellPrice
        {
            get { return m_CurrentSellPrice; }
            set { m_CurrentSellPrice = value; }
        }
        public int HatveArasi
        {
            get { return m_HatveArasi; }
            set { m_HatveArasi = value; }
        }


        public int ToplamBoy
        {
            get { return m_ToplamBoy; }
            set { m_ToplamBoy = value; }
        }


        public int KancaArasi
        {
            get { return m_KancaArasi; }
            set { m_KancaArasi = value; }
        }


        public int Ictenice
        {
            get { return m_Ictenice; }
            set { m_Ictenice = value; }
        }


        public string KancaYonu
        {
            get { return m_KancaYonu; }
            set { m_KancaYonu = value; }
        }


        public float WireDiameter
        {
            get { return m_WireDiameter; }
            set { m_WireDiameter = value; }
        }

        public string Picture1
        {
            get { return m_Picture1; }
            set { m_Picture1 = value; }
        }


        public string Picture2
        {
            get { return m_Picture2; }
            set { m_Picture2 = value; }
        }


        public string Picture3
        {
            get { return m_Picture3; }
            set { m_Picture3 = value; }
        }


        public string Picture4
        {
            get { return m_Picture4; }
            set { m_Picture4 = value; }
        }


        public string TecniquePicture
        {
            get { return m_TecniquePicture; }
            set { m_TecniquePicture = value; }
        }


        public string PDFDocument
        {
            get { return m_PDFDocument; }
            set { m_PDFDocument = value; }
        }
        public string ExtraCode1
        {
            get { return m_ExtraCode1; }
            set { m_ExtraCode1 = value; }
        }


        public string ExtraCode2
        {
            get { return m_ExtraCode2; }
            set { m_ExtraCode2 = value; }
        }


        public string ExtraCode3
        {
            get { return m_ExtraCode3; }
            set { m_ExtraCode3 = value; }
        }


        public string ExtraCode4
        {
            get { return m_ExtraCode4; }
            set { m_ExtraCode4 = value; }
        }



        public int CurrentOPStatus
        {
            get { return m_CurrentOPStatus; }
            set { m_CurrentOPStatus = value; }
        }

        public string CurrentPackageType
        {
            get { return m_CurrentPackageType; }
            set { m_CurrentPackageType = value; }
        }
        public int OrderCount
        {
            get { return m_OrderCount; }
            set { m_OrderCount = value; }
        }
        public Color CColor
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public int CategoryID
        {
            get { return m_CategoryID; }
            set { m_CategoryID = value; }
        }
        public Category Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        public string Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }
        public int NumberOfTurns
        {
            get { return m_NumberOfTurns; }
            set { m_NumberOfTurns = value; }
        }
        public int NumberOfForm
        {
            get { return m_NumberOfForm; }
            set { m_NumberOfForm = value; }
        }
        public float Diameter
        {
            get { return m_Diameter; }
            set { m_Diameter = value; }
        }
        public float Weight
        {
            get { return m_Weight; }
            set { m_Weight = value; }
        }
        public string RawMaterialsCharacteristic
        {
            get { return m_RawMaterialsCharacteristic; }
            set { m_RawMaterialsCharacteristic = value; }
        }
        public float StraighteningLength
        {
            get { return m_StraighteningLength; }
            set { m_StraighteningLength = value; }
        }
        public decimal Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }

        public int UsedStockCountColored
        {
            get
            {
                return m_UsedStockCountColored;
            }

            set
            {
                m_UsedStockCountColored = value;
            }
        }

        public int UsedStockCountNonColored
        {
            get
            {
                return m_UsedStockCountNonColored;
            }

            set
            {
                m_UsedStockCountNonColored = value;
            }
        }

        public int ProductionCount
        {
            get
            {
                return m_ProductionCount;
            }

            set
            {
                m_ProductionCount = value;
            }
        }


        #endregion

        #region Constructers
        public Product()
            : this(0, Enumarations.State.Aktif)
        {

        }

        public Product(int PID, Enumarations.State MinState)
        {
            this.ID = PID;
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
                    oDS = oSqlClientProvider.GetProductDetailByPID(this.ID, (int)MinState);
                    if (Utility.CheckDataSetState(oDS) && (Utility.CheckDataTableState(oDS.Tables[0])))
                    {
                        if (oDS.Tables[0].Rows.Count > 0)
                        {
                            bTemp = LoadProductFromDataRow(oDS.Tables[0].Rows[0], MinState);
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
        private bool LoadProductFromDataRow(DataRow oRow, Enumarations.State nMinState)
        {
            bool bResult = false;
            if (oRow != null)
            {
                this.ID = Int32.Parse(oRow["PID"].ToString());
                Category oCategory = new Category();
                oCategory.ID = Int32.Parse(oRow["Category"].ToString());
                this.Category = oCategory;
                this.CategoryID = Int32.Parse(oRow["Category"].ToString());
                this.Name = oRow["PName"].ToString();
                this.Code = oRow["Code"].ToString();
                this.ExtraCode1 = oRow["ExtraCode1"].ToString();
                this.ExtraCode2 = oRow["ExtraCode2"].ToString();
                this.ExtraCode3 = oRow["ExtraCode3"].ToString();
                this.ExtraCode4 = oRow["ExtraCode4"].ToString();
                this.NumberOfTurns = Int32.Parse(oRow["NumberOfTurns"].ToString());
                this.NumberOfForm = Int32.Parse(oRow["NumberOfForm"].ToString());
                this.Diameter = float.Parse(oRow["Diameter"].ToString());
                this.WireDiameter = float.Parse(oRow["WireDiameter"].ToString());
                this.StraighteningLength = float.Parse(oRow["StraighteningLength"].ToString());
                this.Weight = float.Parse(oRow["Weight"].ToString());
                this.RawMaterialsCharacteristic = oRow["RawMaterialsCharacteristic"].ToString();
                this.Price = decimal.Parse(oRow["Price"].ToString());
                this.Picture1 = oRow["Picture1"].ToString();
                this.Picture2 = oRow["Picture2"].ToString();
                this.Picture3 = oRow["Picture3"].ToString();
                this.Picture4 = oRow["Picture4"].ToString();
                this.TecniquePicture = oRow["TecniquePicture"].ToString();
                this.PDFDocument = oRow["PDFDocument"].ToString();
                this.HatveArasi = Int32.Parse(oRow["HatveArasi"].ToString());
                this.ToplamBoy = Int32.Parse(oRow["ToplamBoy"].ToString());
                this.KancaArasi = Int32.Parse(oRow["KancaArasi"].ToString());
                this.Ictenice = Int32.Parse(oRow["Ictenice"].ToString());
                this.KancaYonu = oRow["KancaYonu"].ToString();
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


                    nNewUID = oSqlClientProvider.SaveProduct(this.ID, this.Name, this.Code, this.ExtraCode1, this.ExtraCode2, this.ExtraCode3, this.ExtraCode4, this.Category.ID, this.Diameter, this.WireDiameter, this.NumberOfForm, this.NumberOfTurns, this.RawMaterialsCharacteristic, this.StraighteningLength, this.Price, this.Weight, this.Picture1, this.Picture2, this.Picture3, this.Picture4, this.TecniquePicture, this.PDFDocument, this.HatveArasi, this.ToplamBoy, this.KancaArasi, this.Ictenice, this.KancaYonu, (int)State, this.UpdateUserID, out nResult);

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
        public static DataSet getProductList(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet ds = null;
            try
            {

                DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                ds = oSqlClientProvider.GetProductListWithPagingAndSorting(strWhere, PageIndex, NumberofRowsRetrieved, SortColumn, SortOrder);
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
        public static DataTable GetProductListForDDL()
        {
            DAL.DataProvider.SqlClientProvider oProvider = new DAL.DataProvider.SqlClientProvider();
            return oProvider.GetProductListForDDL().Tables[0];
        }
        public static DataTable CalculateProductionTimeByPIDAndCount(int PID, int Count)
        {
            DAL.DataProvider.SqlClientProvider oProvider = new DAL.DataProvider.SqlClientProvider();
            return oProvider.CalculateProductionTimeByPIDAndCount(PID, Count).Tables[0];
        }
        public static DataSet getSoldProductList(string strWhere, string strHaving, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet ds = null;
            try
            {

                DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                ds = oSqlClientProvider.GetSoldProductListWithPagingAndSorting(strWhere, strHaving, PageIndex, NumberofRowsRetrieved, SortColumn, SortOrder);
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

        #endregion

        #region PaintRelatedPart
        public static DataSet getPaintProductList(string strWhere, int PageIndex, int NumberofRowsRetrieved, string SortColumn, int SortOrder)
        {
            DataSet ds = null;
            try
            {

                DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                ds = oSqlClientProvider.GetPaintProductListWithPagingAndSorting(strWhere, PageIndex, NumberofRowsRetrieved, SortColumn, SortOrder);
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

        #endregion

    }
}
