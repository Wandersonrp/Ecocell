using FluentMigrator;

namespace Ecocell.Infrastructure.Migrations.Versions;

[Migration((long)VersionsNumber.CreateEletronicMaterials, "Create eletronic materials table")]
public class Version0000002 : Migration
{
    public override void Down()
    {        
    }

    public override void Up()
    {
        var table = BaseVersion.InsertDefaultColumns(Create.Table("EletronicMaterials"));

        table
            .WithColumn("ExternalId").AsGuid().NotNullable().Unique()
            .WithColumn("Description").AsString(50).NotNullable()
            .WithColumn("Type").AsFixedLengthString(1).NotNullable()
            .WithColumn("Weight").AsDouble().Nullable()
            .WithColumn("Quantity").AsInt32().Nullable();                     
    }
}