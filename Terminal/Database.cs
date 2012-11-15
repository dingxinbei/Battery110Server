using System;
using System.Collections.Generic;
using System.Text;

namespace Terminal
{
    public class Database
    {
        public delegate void error(Exception e);
        public event error Error;
        private static string sConnectDB = @"Data Source=db.battery110.com,3200;Initial Catalog=Battery110;User ID=sa;Password=sasa";
        private void AttachError(Exception e)
        {
            if (Error != null)
                Error(e);
        }

    }
}
