using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using DAL = OzyaysanDataEngine;

namespace OzyaysanBusinessEngine
{
    public class User : BaseClass
    {
        #region Fields
        private string m_LastName;
        private Enumarations.UserType m_UserType;
        private string m_Password;       
        #endregion
        #region Properties
        public string LastName
        {
            get { return m_LastName; }
            set { m_LastName = value; }
        }
        public Enumarations.UserType UserType
        {
            get { return m_UserType; }
            set { m_UserType = value; }
        }
        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }
        #endregion
         #region Constructers
        public User()
            : this(0, Enumarations.State.Aktif)
        {

        }

        public User(int UID, Enumarations.State MinState)
        {
            this.ID = UID;
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
                    oDS = oSqlClientProvider.GetUserDetailByUID(this.ID,(int)MinState);
                    if (Utility.CheckDataSetState(oDS) && (Utility.CheckDataTableState(oDS.Tables[0])))
                    {
                        if (oDS.Tables[0].Rows.Count > 0)
                        {
                            bTemp = LoadUserFromDataRow(oDS.Tables[0].Rows[0], MinState);
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
        private bool LoadUserFromDataRow(DataRow oRow, Enumarations.State nMinState)
        {
            bool bResult = false;
            if (oRow != null)
            {
                this.ID = Int32.Parse(oRow["UID"].ToString());
                this.UserType = (Enumarations.UserType)Int32.Parse(oRow["UTID"].ToString());
                this.Name = oRow["UName"].ToString();
                this.Password = oRow["UPassword"].ToString();
               
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


                    nNewUID = oSqlClientProvider.SaveUser(this.ID, this.Name,this.Password, out nResult);

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
        public static DataSet VerifyPassword(string UName, string UPassword)
        {
            DataSet ds = null;
            try
            {

                DAL.DataProvider.SqlClientProvider oSqlClientProvider = new DAL.DataProvider.SqlClientProvider();
                ds = oSqlClientProvider.VerifyPassword(UName, UPassword);
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
