using FluentMigrator;

namespace FootballMatchCrudApi.Data.Migrations
{
    [Migration(24032024)]
    public class CreateSchema:Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Create.Table("footballmatch")
                 .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("stadium").AsString(128).NotNullable()
                   .WithColumn("score").AsString(128).NotNullable()
                    .WithColumn("country").AsString(128).NotNullable();
        }

    }
}
