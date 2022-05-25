using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeHub.Infrastructure.Migrations
{
    public partial class Ingredients_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"INSERT INTO dbo.Ingredients(Id, Name, MeasureUnit, CaloriesPerUnit) VALUES 
                ('175dabcf-8e55-47a9-84ac-8f832646bdc7', 'Hleb', 0, 100), 
                ('7c06be40-80c6-4a53-87f5-bfd547170fab', 'Jaja', 2, 50),
                ('17f7fd38-2444-4165-859b-592206e2fea2', 'Mleko', 1, 70),
                ('42e35bc4-5978-47eb-849c-abc4af4f6223', 'Paradajz', 2, 30),
                ('fd0f2e2b-f4b4-49f3-924d-1288e062d3ed', 'Visnje', 0, 10)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM dbo.Ingredients");
        }
    }
}
