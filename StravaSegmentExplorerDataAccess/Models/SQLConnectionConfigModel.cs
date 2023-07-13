using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.Models
{
    public class SQLConnectionConfigModel
    {
        public string IdentityDbConnection { get; set; }
        public string StravaDbConnection { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

    }
}
