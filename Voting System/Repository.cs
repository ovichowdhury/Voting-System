using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlClientRepository;
using System.Configuration;

namespace Voting_System
{
    public class Repository
    {
        public ISqlClientWrapper Db;

        public string ConnString;
        
        public Repository()
        {
            ConnString = ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString;
            Db = new SqlClientWrapper(ConnString);
        }
    }
}