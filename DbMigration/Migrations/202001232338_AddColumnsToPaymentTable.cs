using FluentMigrator;

namespace DbMigration.Migrations
{
    [Migration(202001232338)]
    public class AddColumnsToPaymentTable : Migration
    {
        public override void Up()
        {
            Create.Column("ExpiryMonth").OnTable("Payments").AsString(2).Nullable();
            Create.Column("ExpiryYear").OnTable("Payments").AsString(4).Nullable();
            Create.Column("Cvv").OnTable("Payments").AsString(3).Nullable();
        }

        public override void Down()
        {
            Delete.Column("ExpiryMonth").FromTable("Payments");
            Delete.Column("ExpiryYear").FromTable("Payments");
            Delete.Column("Cvv").FromTable("Payments");
        }
    }
}