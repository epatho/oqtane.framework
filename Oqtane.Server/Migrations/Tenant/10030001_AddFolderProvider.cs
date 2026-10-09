using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations.EntityBuilders;
using Oqtane.Repository;
using Oqtane.Shared;

namespace Oqtane.Migrations.Tenant
{
    [DbContext(typeof(TenantDBContext))]
    [Migration("Tenant.10.03.00.01")]
    public class AddFolderProvider : MultiDatabaseMigration
    {
        public AddFolderProvider(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var folderProviderEntityBuilder = new FolderConfigEntityBuilder(migrationBuilder, ActiveDatabase);
            folderProviderEntityBuilder.Create();

            object utcDate = DateTime.UtcNow;
            if (ActiveDatabase.Name == "PostgreSQL" || ActiveDatabase.Name == "SqlServer")
            {
                utcDate = $"'{DateTime.UtcNow.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fffffffK")}'";
            }
                
            folderProviderEntityBuilder.InsertData(new[] { "Name", "Provider", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" }, new object[] { $"'Public'", $"'{Constants.PublicFolderProvider}'", "''", utcDate, "''", utcDate }, string.Empty);
            folderProviderEntityBuilder.InsertData(new[] { "Name", "Provider", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" }, new object[] { $"'Private'", $"'{Constants.PrivateFolderProvider}'", "''", utcDate, "''", utcDate }, string.Empty);

            var folderEntityBuilder = new FolderEntityBuilder(migrationBuilder, ActiveDatabase);
            folderEntityBuilder.AddIntegerColumn("FolderConfigId", false, 1);
            folderEntityBuilder.AddStringColumn("MappedPath", 512, true);
            folderEntityBuilder.UpdateData("FolderConfigId", 1, "Type = 'Public'");
            folderEntityBuilder.UpdateData("FolderConfigId", 2, "Type = 'Private'");
            folderEntityBuilder.AddForeignKey("FK_Folder_FolderConfig", "FolderConfigId", "FolderConfig", "FolderConfigId", ReferentialAction.NoAction);
            folderEntityBuilder.DropColumn("Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // not implemented
        }
    }
}
