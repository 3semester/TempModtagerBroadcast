using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public int PostTempToDB(string temp)
        {
            throw new NotImplementedException();
        }
    }
}
