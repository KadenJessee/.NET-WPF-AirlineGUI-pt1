using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6Flight
{
    internal class clsPassengerManager
    {

        /// <summary>
        /// object to connect to the data access
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// creates object for the data access
        /// </summary>
        public clsPassengerManager()
        {
            try
            {
                db = new clsDataAccess ();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// gets the passengers from SQL Queries and returnss a list of clsPassengers
        /// </summary>
        /// <returns></returns>
        public List<clsPassenger> GetPassengers(string sFlightID)
        {
            try
            {
                //create a new lislt
                List<clsPassenger> passengers = new List<clsPassenger> ();
                int i = 0;

                //get the sql statement
                string sSQL = clsSQL.GetPassengers(sFlightID);

                //execute the sql statement
                DataSet dsPassengers = new DataSet ();

                dsPassengers = db.ExecuteSQLStatement(sSQL, ref i);

                //loop through
                foreach(DataRow dr in dsPassengers.Tables[0].Rows)
                {
                    //create new passenger
                    clsPassenger clsMyPassenger = new clsPassenger ();
                    //get the data
                    clsMyPassenger.PassengerID = int.Parse(dr["Passenger_ID"].ToString());
                    clsMyPassenger.FirstName = (string)dr["First_Name"];
                    clsMyPassenger.LastName = (string)dr["Last_Name"];
                    //add to list of flights
                    passengers.Add(clsMyPassenger);
                }
                return passengers;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
