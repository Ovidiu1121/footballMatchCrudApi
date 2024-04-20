using FluentMigrator;

namespace FootballMatchCrudApi.Data.Migrations
{
    [Migration(24032024100)]
    public class TestMigration:Migration
    {
        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
