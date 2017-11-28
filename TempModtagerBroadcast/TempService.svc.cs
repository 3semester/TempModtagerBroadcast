using System;
using System.Collections.Generic;
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
            const string selectAllTemp = "SELECT * FROM Temperaturmaaling";

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
                            float temp = dReader.GetFloat(1);

                            Temp t1 = new Temp()
                            {
                                Id = id,
                                Temps = temp
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
            throw new NotImplementedException();
        }
    }
}
