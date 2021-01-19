using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UNi_Portal.Controllers
{
    public class DBQueries : Controller
    {
        private static SqlConnection _SqlConnection = new SqlConnection( "Data Source=.; Initial Catalog=UNi Portal DB; Persist Security Info=True; User ID=haroon; Password=haroontech2299" );

        public static string DBID = "";
        
        //For Select Data
        public static string DBFilDTable( ref DataTable nDTable, string SQLQuery )
        {
            string nRtnValue = "N";

            try
            {

                SqlDataAdapter mSQLDA = new SqlDataAdapter( SQLQuery, _SqlConnection );

                nDTable.Clear();
                nDTable.Columns.Clear();

                mSQLDA.Fill( nDTable );

                nRtnValue = ( nDTable.Rows.Count > 0 ) ? "Y" : "N";
            }
            catch ( SqlException SqlEx )
            {
                nRtnValue = SqlEx.ToString();
            }
            catch ( Exception ex )
            {

                //Throw ex
                nRtnValue = ex.Message;
            }

            return nRtnValue;
        }

        //For Save, Update and Delete Data
        public static string DB_ExecuteNonQuery( string pSQL )
        {
            string nRtnValue = "Started";

            try
            {

                if ( _SqlConnection.State != ConnectionState.Open )
                    _SqlConnection.Open();

                SqlCommand _SqlCommand = new SqlCommand( pSQL, _SqlConnection );

                _SqlCommand.CommandType = CommandType.Text;
                _SqlCommand.ExecuteNonQuery();


                if ( _SqlConnection.State != ConnectionState.Closed )
                    _SqlConnection.Close();

                nRtnValue = "Y";
            }
            catch ( SqlException SqlEx )
            {
                nRtnValue = SqlEx.ToString();
            }
            catch ( Exception ex )
            {

                //Throw ex
                nRtnValue = ex.Message;
            }

            return nRtnValue;

        }
    }
}