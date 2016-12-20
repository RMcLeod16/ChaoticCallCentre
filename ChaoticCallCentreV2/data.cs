using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaoticCallCentreV2
{
    /// <summary>
    /// Class to handle all comminucations between appplication and database
    /// </summary>
    class data
    {
        #region vars
        
        /// <summary>
        /// Connection string used to connect to the database
        /// </summary>
        static string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ccc.accdb;Persist Security Info=True";

        /// <summary>
        /// dbConnection used to connect to the database
        /// </summary>
        OleDbConnection dbConn = new OleDbConnection(conn);     // Make new dbConenction based on above connection string

        /// <summary>
        /// dbAdapter used to populate the DataSet
        /// </summary>
        OleDbDataAdapter dbAdapter;

        /// <summary>
        /// dbDataReader used to read data from database
        /// </summary>
        public OleDbDataReader dbReader;

        /// <summary>
        /// DataSet to hold data from the database
        /// </summary>
        public DataSet ds;

        /// <summary>
        /// dbCommand used to send commands to the database
        /// </summary>
        OleDbCommand dbCmd;

        #endregion vars

        #region methods

        /// <summary>
        /// Method for getting data from database and binding to DataSet
        /// </summary>
        /// <param name="query">SQL query to send to the database</param>
        public void GetDataSet(string query)
        {
            try                                                 // Try to...
            {
                ds = new DataSet();                             // Make new DataSet
                dbAdapter = new OleDbDataAdapter(query, conn);  // Make new DbAdapter using supplied query
                myDebug.WriteLine(query);

                dbConn.Open();                                  // Open connection to database
                dbAdapter.Fill(ds);                             // Fill the DataSet with obtained data
                dbConn.Close();                                 // Close connection to database
            }
            catch (Exception ex)                                // ...but if something goes wrong...
            {
                Console.WriteLine(ex.Message);                  // Log the Exception message to Console
            }
        }

        /// <summary>
        /// Method to read data from database
        /// Hacky, but much easier on CPU usage compared to GetDataSet()
        /// Will need to use CloseConnection() when finished with this...
        /// </summary>
        /// <param name="query">SQL query to send to the database</param>
        public void GetDataReader(string query)
        {
            try                                                 // Try to...
            {
                dbCmd = new OleDbCommand(query, dbConn);        // Make new dbCmd using supplied query
                myDebug.WriteLine(query);

                dbCmd.Connection.Open();                        // Open connection to database
                dbReader = dbCmd.ExecuteReader();               // Build dbReader using created dbCmd
                dbReader.Read();                                // Read the data from the dbReader
            }
            catch (Exception ex)                                // ...but if something goes wrong...
            {
                Console.WriteLine(ex.Message);                  // Log the Exception message to Console
            }
        }

        /// <summary>
        /// Method for executing commands on database
        /// </summary>
        /// <param name="query">SQL query to send to the database</param>
        public void SendNonQuery(string query)
        {
            try                                                 // Try to...
            {
                dbCmd = new OleDbCommand(query, dbConn);        // Make new dbCmd using supplied query
                myDebug.WriteLine(query);

                dbConn.Open();                                  // Open connection to database
                dbCmd.ExecuteNonQuery();                        // Send the query to database
                System.Threading.Thread.Sleep(500);             // A quick sleep to ensure database is finished before closing connection
                dbConn.Close();                                 // Close connection to database
            }
            catch (Exception ex)                                // ...but if something goes wrong...
            {
                Console.WriteLine(ex.Message);                  // Log the Exception message to Console
            }
        }

        /// <summary>
        /// Method to manually close connection to database
        /// Needed for GetDataReader()...
        /// </summary>
        public void CloseConnection()
        {
            try                                                 // Try to...
            {
                dbConn.Close();                                 // Close connection to database
            }
            catch (Exception ex)                                // ...but if something goes wrong...
            {
                Console.WriteLine(ex.Message);                  // Log the Exception message to Console
            }
        }

        #endregion methods
    }
}