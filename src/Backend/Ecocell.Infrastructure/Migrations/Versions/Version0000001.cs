using FluentMigrator;

namespace Ecocell.Infrastructure.Migrations.Versions;

[Migration((long)VersionsNumber.CreateUserTable, "Create User table")]
public class Version0000001 : Migration
{
    public override void Down()
    {
        Delete.Table("User");
    }

    public override void Up()
    {
        var table = BaseVersion.InsertDefaultColumns(Create.Table("User"));

        table
            .WithColumn("ExternalId").AsGuid().NotNullable().Unique()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(50).NotNullable()
            .WithColumn("Document").AsString(50).NotNullable()
            .WithColumn("Password").AsString(512).NotNullable()
            .WithColumn("Role").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("Cellphone").AsString(20).NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable();

        Create.Table("LegalPerson")
            .WithColumn("UserId").AsInt64().ForeignKey("User", "Id").NotNullable().PrimaryKey().Identity()
            .WithColumn("CompanyName").AsString(100).NotNullable()
            .WithColumn("IsDiscarding").AsBoolean().NotNullable()
            .WithColumn("IsCollectPoint").AsBoolean().NotNullable()
            .WithColumn("IsCollector").AsBoolean().NotNullable();

        Create.Table("NaturalPerson")
            .WithColumn("UserId").AsInt64().ForeignKey("User", "Id").NotNullable().PrimaryKey().Identity()    
            .WithColumn("IsDiscarding").AsBoolean().NotNullable()            
            .WithColumn("BirthDate").AsDateTime().NotNullable();
    }
}