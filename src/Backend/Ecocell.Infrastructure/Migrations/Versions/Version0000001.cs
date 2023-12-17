using FluentMigrator;

namespace Ecocell.Infrastructure.Migrations.Versions;

[Migration((long)VersionsNumber.CreateUserTable, "Create Users table")]
public class Version0000001 : Migration
{
    public override void Down()
    {
        Delete.Table("Users");
        Delete.Table("NaturalPerson");
        Delete.Table("LegalPerson");
    }

    public override void Up()
    {
        var table = BaseVersion.InsertDefaultColumns(Create.Table("Users"));

        table
            .WithColumn("ExternalId").AsGuid().NotNullable().Unique()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(50).NotNullable().Unique()
            .WithColumn("Document").AsString(50).NotNullable().Unique()
            .WithColumn("Password").AsString(512).NotNullable()
            .WithColumn("Role").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("Type").AsString(1).NotNullable().WithDefaultValue('N')
            .WithColumn("Cellphone").AsString(20).NotNullable()
            .WithColumn("IsActive").AsBoolean().NotNullable();

        Create.Table("LegalPersons")
            .WithColumn("Id").AsInt64().ForeignKey("Users", "Id").NotNullable().PrimaryKey()
            .WithColumn("CompanyName").AsString(100).NotNullable()
            .WithColumn("IsDiscarding").AsBoolean().NotNullable()
            .WithColumn("IsCollectPoint").AsBoolean().NotNullable()
            .WithColumn("IsCollector").AsBoolean().NotNullable();

        Create.Table("NaturalPersons")
            .WithColumn("Id").AsInt64().ForeignKey("Users", "Id").NotNullable().PrimaryKey()    
            .WithColumn("IsDiscarding").AsBoolean().NotNullable()            
            .WithColumn("BirthDate").AsDateTime().NotNullable();
    }
}