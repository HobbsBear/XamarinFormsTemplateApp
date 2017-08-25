using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using PassTime.MobileApp.DataObjects;

namespace PassTime.MobileApp.Models
{
	public class MobileServiceContext : DbContext
	{
		//https://github.com/Microsoft/HealthClinic.biz/wiki/Create-and-deploy-a-mobile-app-in-Azure-App-Service
		//Followed the tutorial above to get the backend setup.

		private const string connectionStringName = "Name=MS_TableConnectionString";

		public MobileServiceContext() : base(connectionStringName)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Event> Events { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("dbo");
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Add(
			    new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
				"ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
		}
	}
}
