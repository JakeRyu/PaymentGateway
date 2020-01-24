using FluentMigrator;

namespace DbMigration.Migrations
{
    [Migration(202001240214)]
    public class AlterPaymentIdType : Migration
    {
        public override void Up()
        {
            Alter.Column("Id").OnTable("Payments").AsInt32().Identity();
        }

        public override void Down()
        {
            
        }
    }
}