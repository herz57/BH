using Microsoft.EntityFrameworkCore.Migrations;

namespace BH.Domain.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Domains_DomainType1",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_DomainType1",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "DomainType1",
                table: "Machines");

            migrationBuilder.AlterColumn<bool>(
                name: "IsLocked",
                table: "Machines",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_DomainType",
                table: "Machines",
                column: "DomainType");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Domains_DomainType",
                table: "Machines",
                column: "DomainType",
                principalTable: "Domains",
                principalColumn: "DomainType",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Domains_DomainType",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_DomainType",
                table: "Machines");

            migrationBuilder.AlterColumn<bool>(
                name: "IsLocked",
                table: "Machines",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");

            migrationBuilder.AddColumn<int>(
                name: "DomainType1",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_DomainType1",
                table: "Machines",
                column: "DomainType1");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Domains_DomainType1",
                table: "Machines",
                column: "DomainType1",
                principalTable: "Domains",
                principalColumn: "DomainType",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
