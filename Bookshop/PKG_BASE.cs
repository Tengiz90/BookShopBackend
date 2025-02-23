﻿namespace Bookshop
{
    internal class PKG_BASE
    {
        string? connStr;
        IConfiguration configuration;

        public PKG_BASE(IConfiguration configuration)
        {
            this.configuration = configuration;
            connStr = this.configuration.GetConnectionString("OracleConnStr");
        }
        protected string ConnStr
        {
            get { return connStr; }
        }
    }
}
