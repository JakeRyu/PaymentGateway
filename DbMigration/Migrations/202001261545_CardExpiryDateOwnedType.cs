using FluentMigrator;

namespace DbMigration.Migrations
{
    [Migration(202001261545)]
    public class CardExpiryDateOwnedType : Migration
    {
        public override void Up()
        {
            // EF Core Owned Type (Value Object)
            Create.Column("CardExpiryDate").OnTable("Payments").AsDate().Nullable();
        }

        public override void Down()
        {
            Delete.Column("CardExpiryDate").FromTable("Payments");
        }
    }
}