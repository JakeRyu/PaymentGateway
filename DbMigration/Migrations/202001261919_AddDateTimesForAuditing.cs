using FluentMigrator;

namespace DbMigration.Migrations
{
    [Migration((202001261919))]
    public class AddDateTimesForAuditing : Migration
    {
        public override void Up()
        {
            Create.Column("CreatedOn").OnTable("Payments").AsDateTime().Nullable();
            Create.Column("LastModifiedOn").OnTable("Payments").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Column("CreatedOn").FromTable("Payments");
            Delete.Column("LastModifiedOn").FromTable("Payments");
        }
    }
}