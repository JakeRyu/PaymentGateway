using FluentMigrator;

namespace DbMigration.Migrations
{
    [Migration(202001192332)]
    public class AddPaymentTable : Migration
    {
        public override void Up()
        {
            Create.Table("Payments")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("MerchantId").AsInt32()
                .WithColumn("CardHolderName").AsString(60)
                .WithColumn("CardNumber").AsString(20)
                .WithColumn("Amount").AsDecimal(20, 2)
                .WithColumn("Currency").AsString(3);
        }

        public override void Down()
        {
            Delete.Table("Payments");
        }
    }
}