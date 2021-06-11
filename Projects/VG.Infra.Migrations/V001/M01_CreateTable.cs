using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace VG.Infra.Migrations.V001
{
    [Migration(1)]
    public class M01_CreateTable : Migration
    {
        public override void Up()
        {
            Create.Table("model")
                .WithColumn("id").AsInt16().NotNullable().Identity().PrimaryKey()
                .WithColumn("name").AsAnsiString(255).NotNullable().Unique();

            Create.Table("truck")
                .WithColumn("id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("model_id").AsInt16().NotNullable().ForeignKey("fk_model_truck", "model", "id")
                .WithColumn("manufacture_year").AsInt16().NotNullable()
                .WithColumn("manufacture_model").AsInt16().NotNullable()
                .WithColumn("color").AsAnsiString(15).Nullable()
                .WithColumn("price").AsDecimal().Nullable();

            Insert.IntoTable("model").Row(new { name = "FH" });
            Insert.IntoTable("model").Row(new { name = "FM" });
        }

        public override void Down()
        {
            Delete.ForeignKey("fk_model_truck");

            Delete.Table("truck");

            Delete.Table("model");
        }
    }
}
