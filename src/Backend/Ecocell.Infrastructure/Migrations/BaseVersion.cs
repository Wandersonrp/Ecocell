using FluentMigrator.Builders.Create.Table;

namespace Ecocell.Infrastructure.Migrations;

public static class BaseVersion
{
    public static ICreateTableColumnOptionOrWithColumnSyntax InsertDefaultColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
    {
        return table.WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime();
    }
}