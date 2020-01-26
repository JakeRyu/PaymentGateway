using FluentMigrator;

namespace DbMigration.Migrations
{
    [Migration(202001232338)]
    public class AddColumnsToPaymentTable : Migration
    {
        public override void Up()
        {
            Create.Column("Cvv").OnTable("Payments").AsString(3).Nullable();
        }

        public override void Down()
        {
            Delete.Column("Cvv").FromTable("Payments");
        }
    }
}