using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricityDataApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegionElectricityConsumptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChainRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SumPPlus = table.Column<double>(type: "float", nullable: false),
                    SumPMinus = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionElectricityConsumptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionElectricityConsumptions");
        }
    }
}
