using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TempModtagerBroadcast
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TempService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TempService.svc or TempService.svc.cs at the Solution Explorer and start debugging.
    public class TempService : ITempService
    {
        private const string ConnString =
            "Server=tcp:skole.database.windows.net,1433;Initial Catalog=General_DB;Persist Security Info=False;User ID=lisa;Password=Secret1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public IList<Temp> GetAllTemp()
        {
            const string selectAllTemp = "SELECT * FROM Temperaturmaaling ORDER BY Id";

            using(SqlConnection databaseConnection = new SqlConnection(ConnString))
            {
                databaseConnection.Open();
                using(SqlCommand selectedCommand = new SqlCommand(selectAllTemp, databaseConnection))
                {
                    using(SqlDataReader dReader = selectedCommand.ExecuteReader())
                    {
                        List<Temp> tempList = new List<Temp>();
                        while(dReader.Read())
                        {
                            int id = dReader.GetInt32(0);
                            string temp = dReader.GetString(1);
                            DateTime date = dReader.GetDateTime(2);

                            Temp t1 = new Temp()
                            {
                                Id = id,
                                Temps = temp,
                                Date = date
                            };
                            tempList.Add(t1);
                        }
                        return tempList;
                    }
                }
            }
        }

        public int PostTempToDB(string temp)
        {
            const string postTemps = "INSERT INTO Temperaturmaaling (Temps) values (@temp)";
            using (SqlConnection databaseConn = new SqlConnection(ConnString))
            {
                databaseConn.Open();
                using (SqlCommand insertCommand = new SqlCommand(postTemps, databaseConn))
                {
                    insertCommand.Parameters.AddWithValue("@temp", temp);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        public bool TjekStatus()
        {
            const string gemmeSwitch = "SELECT TOP(1) OnOff from Switch";
            using (SqlConnection databaseConnection = new SqlConnection(ConnString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(gemmeSwitch, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            bool kontakt = (bool)reader["OnOff"];
                            if (kontakt == true)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void UpdateDb()
        {
            bool status = TjekStatus();
            if (status == true)
            {
                bool value = false;
                const string update = "UPDATE Switch SET OnOff = @Value WHERE Id = 1";

                SqlConnection conn = new SqlConnection(ConnString);
                SqlCommand UpdateCommand = new SqlCommand(update, conn);
                UpdateCommand.Parameters.AddWithValue("@Value", SqlDbType.Bit).Value = 0;
                conn.Open();
                UpdateCommand.ExecuteNonQuery();
                conn.Close();
            }
            else if (status == false)
            {
                bool value = true;
                const string update = "UPDATE Switch SET OnOff = @Value WHERE Id = 1";

                SqlConnection conn = new SqlConnection(ConnString);
                SqlCommand UpdateCommand = new SqlCommand(update, conn);
                UpdateCommand.Parameters.AddWithValue("@Value", SqlDbType.Bit).Value = 1;
                conn.Open();
                UpdateCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        //private static Temp ReadSensorData(IDataRecord reader)
        //{
        //    int Id = reader.GetInt32(0);
        //    string Temps = reader.GetString(1);
        //    Nullable<System.DateTime> Date = reader.GetDateTime(2);
        //    Temp temps = new Temp
        //    {
        //        Id = Id,
        //        Temps = Temps,
        //        Date = Date
        //    };

        //}
    }
}
