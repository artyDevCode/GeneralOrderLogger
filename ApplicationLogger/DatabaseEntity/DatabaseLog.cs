namespace ApplicationLogger
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    //[DbConfigurationType(typeof(DatabaseEntity))]
    public partial class DatabaseLog : DbContext
    {
        public DatabaseLog()
            : base(AppConfig.CreateConnectionString(), true) // : base("name=DatabaseLog")
        {
           
        }



        public virtual DbSet<AppList> AppLists { get; set; }
        public virtual DbSet<AppLog> AppLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        //Use this instead of 'public class DatabaseEntity : DbConfiguration'. This will automatically solve the
        //'no entity framework provider found for the ado.net provider with invariant name 'system.data.sqlclient' error. If I include the DBconfiguration class it will
        //cause an error when trying to access this assembly via another class. 
        //This will enable to execute as either a standalone assembly or as part of multiple Entity Framework assemblies being called by an exe (for example)
        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
