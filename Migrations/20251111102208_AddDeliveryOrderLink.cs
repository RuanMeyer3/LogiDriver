using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogiDriver_Web_Portal_.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryOrderLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviationAlerts_RoutePlans_RoutePlanId",
                table: "DeviationAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_PanicEvents_RoutePlans_RoutePlanId",
                table: "PanicEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlans_Drivers_DriverId",
                table: "RoutePlans");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlans_Vehicles_VehicleId",
                table: "RoutePlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutePlans",
                table: "RoutePlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PanicEvents",
                table: "PanicEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverLocations",
                table: "DriverLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviationAlerts",
                table: "DeviationAlerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "RouteDescription",
                table: "RoutePlans");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DriverLocations");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "vehicles");

            migrationBuilder.RenameTable(
                name: "RoutePlans",
                newName: "routeplans");

            migrationBuilder.RenameTable(
                name: "PanicEvents",
                newName: "panicevents");

            migrationBuilder.RenameTable(
                name: "Drivers",
                newName: "drivers");

            migrationBuilder.RenameTable(
                name: "DriverLocations",
                newName: "driverlocations");

            migrationBuilder.RenameTable(
                name: "DeviationAlerts",
                newName: "deviationalerts");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "aspnetusertokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "aspnetusers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "aspnetuserroles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "aspnetuserlogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "aspnetuserclaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "aspnetroles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "aspnetroleclaims");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "vehicles",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "vehicles",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "vehicles",
                newName: "registrationnumber");

            migrationBuilder.RenameColumn(
                name: "NextService",
                table: "vehicles",
                newName: "nextservice");

            migrationBuilder.RenameColumn(
                name: "Mileage",
                table: "vehicles",
                newName: "mileage");

            migrationBuilder.RenameColumn(
                name: "MakeModel",
                table: "vehicles",
                newName: "makemodel");

            migrationBuilder.RenameColumn(
                name: "LastService",
                table: "vehicles",
                newName: "lastservice");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "vehicles",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "AssignedDriver",
                table: "vehicles",
                newName: "assigneddriver");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "vehicles",
                newName: "vehicleid");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "routeplans",
                newName: "vehicleid");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "routeplans",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "routeplans",
                newName: "starttime");

            migrationBuilder.RenameColumn(
                name: "RouteCode",
                table: "routeplans",
                newName: "routecode");

            migrationBuilder.RenameColumn(
                name: "Progress",
                table: "routeplans",
                newName: "progress");

            migrationBuilder.RenameColumn(
                name: "EstimatedArrival",
                table: "routeplans",
                newName: "estimatedarrival");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "routeplans",
                newName: "endtime");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "routeplans",
                newName: "driverid");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "routeplans",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "RoutePlanId",
                table: "routeplans",
                newName: "routeplanid");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePlans_VehicleId",
                table: "routeplans",
                newName: "ix_routeplans_vehicleid");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePlans_DriverId",
                table: "routeplans",
                newName: "ix_routeplans_driverid");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "panicevents",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Severity",
                table: "panicevents",
                newName: "severity");

            migrationBuilder.RenameColumn(
                name: "RoutePlanId",
                table: "panicevents",
                newName: "routeplanid");

            migrationBuilder.RenameColumn(
                name: "ResponseTime",
                table: "panicevents",
                newName: "responsetime");

            migrationBuilder.RenameColumn(
                name: "OccurredAt",
                table: "panicevents",
                newName: "occurredat");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "panicevents",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "PanicEventId",
                table: "panicevents",
                newName: "paniceventid");

            migrationBuilder.RenameIndex(
                name: "IX_PanicEvents_RoutePlanId",
                table: "panicevents",
                newName: "ix_panicevents_routeplanid");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "drivers",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "drivers",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "LastAlertTime",
                table: "drivers",
                newName: "lastalerttime");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "drivers",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "FatigueLevel",
                table: "drivers",
                newName: "fatiguelevel");

            migrationBuilder.RenameColumn(
                name: "DriverCode",
                table: "drivers",
                newName: "drivercode");

            migrationBuilder.RenameColumn(
                name: "CurrentLocation",
                table: "drivers",
                newName: "currentlocation");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "drivers",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "AssignedVehicle",
                table: "drivers",
                newName: "assignedvehicle");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "drivers",
                newName: "driverid");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "driverlocations",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "driverlocations",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "driverlocations",
                newName: "latitude");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "driverlocations",
                newName: "driverid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "driverlocations",
                newName: "locationid");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "deviationalerts",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "RoutePlanId",
                table: "deviationalerts",
                newName: "routeplanid");

            migrationBuilder.RenameColumn(
                name: "ResolvedAt",
                table: "deviationalerts",
                newName: "resolvedat");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "deviationalerts",
                newName: "reason");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "deviationalerts",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "DetectedAt",
                table: "deviationalerts",
                newName: "detectedat");

            migrationBuilder.RenameColumn(
                name: "DeviationAlertId",
                table: "deviationalerts",
                newName: "deviationalertid");

            migrationBuilder.RenameIndex(
                name: "IX_DeviationAlerts_RoutePlanId",
                table: "deviationalerts",
                newName: "ix_deviationalerts_routeplanid");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "aspnetusertokens",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "aspnetusertokens",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "aspnetusertokens",
                newName: "loginprovider");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "aspnetusertokens",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "aspnetusers",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "aspnetusers",
                newName: "twofactorenabled");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "aspnetusers",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "aspnetusers",
                newName: "securitystamp");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "aspnetusers",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "aspnetusers",
                newName: "phonenumberconfirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "aspnetusers",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "aspnetusers",
                newName: "passwordhash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "aspnetusers",
                newName: "normalizedusername");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "aspnetusers",
                newName: "normalizedemail");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "aspnetusers",
                newName: "lockoutend");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "aspnetusers",
                newName: "lockoutenabled");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "aspnetusers",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "aspnetusers",
                newName: "emailconfirmed");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "aspnetusers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "aspnetusers",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "aspnetusers",
                newName: "concurrencystamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "aspnetusers",
                newName: "accessfailedcount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aspnetusers",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "UserNameIndex",
                table: "aspnetusers",
                newName: "usernameindex");

            migrationBuilder.RenameIndex(
                name: "EmailIndex",
                table: "aspnetusers",
                newName: "emailindex");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "aspnetuserroles",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "aspnetuserroles",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "aspnetuserroles",
                newName: "ix_aspnetuserroles_roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "aspnetuserlogins",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "aspnetuserlogins",
                newName: "providerdisplayname");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "aspnetuserlogins",
                newName: "providerkey");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "aspnetuserlogins",
                newName: "loginprovider");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "aspnetuserlogins",
                newName: "ix_aspnetuserlogins_userid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "aspnetuserclaims",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "aspnetuserclaims",
                newName: "claimvalue");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "aspnetuserclaims",
                newName: "claimtype");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aspnetuserclaims",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "aspnetuserclaims",
                newName: "ix_aspnetuserclaims_userid");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "aspnetroles",
                newName: "normalizedname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "aspnetroles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "aspnetroles",
                newName: "concurrencystamp");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aspnetroles",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "RoleNameIndex",
                table: "aspnetroles",
                newName: "rolenameindex");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "aspnetroleclaims",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "aspnetroleclaims",
                newName: "claimvalue");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "aspnetroleclaims",
                newName: "claimtype");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "aspnetroleclaims",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "aspnetroleclaims",
                newName: "ix_aspnetroleclaims_roleid");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "routeplans",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "routecode",
                table: "routeplans",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "deliveryorderid",
                table: "routeplans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "distancekm",
                table: "routeplans",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "startlocation",
                table: "routeplans",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "waypoints",
                table: "routeplans",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "heading",
                table: "driverlocations",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "speed",
                table: "driverlocations",
                type: "double",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_vehicles",
                table: "vehicles",
                column: "vehicleid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_routeplans",
                table: "routeplans",
                column: "routeplanid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_panicevents",
                table: "panicevents",
                column: "paniceventid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_drivers",
                table: "drivers",
                column: "driverid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_driverlocations",
                table: "driverlocations",
                column: "locationid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_deviationalerts",
                table: "deviationalerts",
                column: "deviationalertid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetusertokens",
                table: "aspnetusertokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetusers",
                table: "aspnetusers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserroles",
                table: "aspnetuserroles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserlogins",
                table: "aspnetuserlogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserclaims",
                table: "aspnetuserclaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetroles",
                table: "aspnetroles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetroleclaims",
                table: "aspnetroleclaims",
                column: "id");

            migrationBuilder.CreateTable(
                name: "deliveryorders",
                columns: table => new
                {
                    do_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    route_plan_id = table.Column<int>(type: "int", nullable: true),
                    customer_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliveryorders", x => x.do_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_routeplans_deliveryorderid",
                table: "routeplans",
                column: "deliveryorderid");

            migrationBuilder.CreateIndex(
                name: "ix_driverlocations_driverid",
                table: "driverlocations",
                column: "driverid");

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetroleclaims_aspnetroles_roleid",
                table: "aspnetroleclaims",
                column: "roleid",
                principalTable: "aspnetroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserclaims_aspnetusers_userid",
                table: "aspnetuserclaims",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserlogins_aspnetusers_userid",
                table: "aspnetuserlogins",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserroles_aspnetroles_roleid",
                table: "aspnetuserroles",
                column: "roleid",
                principalTable: "aspnetroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserroles_aspnetusers_userid",
                table: "aspnetuserroles",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetusertokens_aspnetusers_userid",
                table: "aspnetusertokens",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_deviationalerts_routeplans_routeplanid",
                table: "deviationalerts",
                column: "routeplanid",
                principalTable: "routeplans",
                principalColumn: "routeplanid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_driverlocations_drivers_driverid",
                table: "driverlocations",
                column: "driverid",
                principalTable: "drivers",
                principalColumn: "driverid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_panicevents_routeplans_routeplanid",
                table: "panicevents",
                column: "routeplanid",
                principalTable: "routeplans",
                principalColumn: "routeplanid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_routeplans_deliveryorders_deliveryorderid",
                table: "routeplans",
                column: "deliveryorderid",
                principalTable: "deliveryorders",
                principalColumn: "do_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_routeplans_drivers_driverid",
                table: "routeplans",
                column: "driverid",
                principalTable: "drivers",
                principalColumn: "driverid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_routeplans_vehicles_vehicleid",
                table: "routeplans",
                column: "vehicleid",
                principalTable: "vehicles",
                principalColumn: "vehicleid",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_aspnetroleclaims_aspnetroles_roleid",
                table: "aspnetroleclaims");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserclaims_aspnetusers_userid",
                table: "aspnetuserclaims");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserlogins_aspnetusers_userid",
                table: "aspnetuserlogins");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserroles_aspnetroles_roleid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserroles_aspnetusers_userid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetusertokens_aspnetusers_userid",
                table: "aspnetusertokens");

            migrationBuilder.DropForeignKey(
                name: "fk_deviationalerts_routeplans_routeplanid",
                table: "deviationalerts");

            migrationBuilder.DropForeignKey(
                name: "fk_driverlocations_drivers_driverid",
                table: "driverlocations");

            migrationBuilder.DropForeignKey(
                name: "fk_panicevents_routeplans_routeplanid",
                table: "panicevents");

            migrationBuilder.DropForeignKey(
                name: "fk_routeplans_deliveryorders_deliveryorderid",
                table: "routeplans");

            migrationBuilder.DropForeignKey(
                name: "fk_routeplans_drivers_driverid",
                table: "routeplans");

            migrationBuilder.DropForeignKey(
                name: "fk_routeplans_vehicles_vehicleid",
                table: "routeplans");

            migrationBuilder.DropTable(
                name: "deliveryorders");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vehicles",
                table: "vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_routeplans",
                table: "routeplans");

            migrationBuilder.DropIndex(
                name: "ix_routeplans_deliveryorderid",
                table: "routeplans");

            migrationBuilder.DropPrimaryKey(
                name: "pk_panicevents",
                table: "panicevents");

            migrationBuilder.DropPrimaryKey(
                name: "pk_drivers",
                table: "drivers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_driverlocations",
                table: "driverlocations");

            migrationBuilder.DropIndex(
                name: "ix_driverlocations_driverid",
                table: "driverlocations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_deviationalerts",
                table: "deviationalerts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetusertokens",
                table: "aspnetusertokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetusers",
                table: "aspnetusers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserroles",
                table: "aspnetuserroles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserlogins",
                table: "aspnetuserlogins");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserclaims",
                table: "aspnetuserclaims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetroles",
                table: "aspnetroles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetroleclaims",
                table: "aspnetroleclaims");

            migrationBuilder.DropColumn(
                name: "deliveryorderid",
                table: "routeplans");

            migrationBuilder.DropColumn(
                name: "distancekm",
                table: "routeplans");

            migrationBuilder.DropColumn(
                name: "startlocation",
                table: "routeplans");

            migrationBuilder.DropColumn(
                name: "waypoints",
                table: "routeplans");

            migrationBuilder.DropColumn(
                name: "heading",
                table: "driverlocations");

            migrationBuilder.DropColumn(
                name: "speed",
                table: "driverlocations");

            migrationBuilder.RenameTable(
                name: "vehicles",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "routeplans",
                newName: "RoutePlans");

            migrationBuilder.RenameTable(
                name: "panicevents",
                newName: "PanicEvents");

            migrationBuilder.RenameTable(
                name: "drivers",
                newName: "Drivers");

            migrationBuilder.RenameTable(
                name: "driverlocations",
                newName: "DriverLocations");

            migrationBuilder.RenameTable(
                name: "deviationalerts",
                newName: "DeviationAlerts");

            migrationBuilder.RenameTable(
                name: "aspnetusertokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "aspnetusers",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "aspnetuserroles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "aspnetuserlogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "aspnetuserclaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "aspnetroles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "aspnetroleclaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "Vehicles",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Vehicles",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "registrationnumber",
                table: "Vehicles",
                newName: "RegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "nextservice",
                table: "Vehicles",
                newName: "NextService");

            migrationBuilder.RenameColumn(
                name: "mileage",
                table: "Vehicles",
                newName: "Mileage");

            migrationBuilder.RenameColumn(
                name: "makemodel",
                table: "Vehicles",
                newName: "MakeModel");

            migrationBuilder.RenameColumn(
                name: "lastservice",
                table: "Vehicles",
                newName: "LastService");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "Vehicles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "assigneddriver",
                table: "Vehicles",
                newName: "AssignedDriver");

            migrationBuilder.RenameColumn(
                name: "vehicleid",
                table: "Vehicles",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "vehicleid",
                table: "RoutePlans",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "RoutePlans",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "starttime",
                table: "RoutePlans",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "routecode",
                table: "RoutePlans",
                newName: "RouteCode");

            migrationBuilder.RenameColumn(
                name: "progress",
                table: "RoutePlans",
                newName: "Progress");

            migrationBuilder.RenameColumn(
                name: "estimatedarrival",
                table: "RoutePlans",
                newName: "EstimatedArrival");

            migrationBuilder.RenameColumn(
                name: "endtime",
                table: "RoutePlans",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "driverid",
                table: "RoutePlans",
                newName: "DriverId");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "RoutePlans",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "routeplanid",
                table: "RoutePlans",
                newName: "RoutePlanId");

            migrationBuilder.RenameIndex(
                name: "ix_routeplans_vehicleid",
                table: "RoutePlans",
                newName: "IX_RoutePlans_VehicleId");

            migrationBuilder.RenameIndex(
                name: "ix_routeplans_driverid",
                table: "RoutePlans",
                newName: "IX_RoutePlans_DriverId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "PanicEvents",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "severity",
                table: "PanicEvents",
                newName: "Severity");

            migrationBuilder.RenameColumn(
                name: "routeplanid",
                table: "PanicEvents",
                newName: "RoutePlanId");

            migrationBuilder.RenameColumn(
                name: "responsetime",
                table: "PanicEvents",
                newName: "ResponseTime");

            migrationBuilder.RenameColumn(
                name: "occurredat",
                table: "PanicEvents",
                newName: "OccurredAt");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "PanicEvents",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "paniceventid",
                table: "PanicEvents",
                newName: "PanicEventId");

            migrationBuilder.RenameIndex(
                name: "ix_panicevents_routeplanid",
                table: "PanicEvents",
                newName: "IX_PanicEvents_RoutePlanId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Drivers",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Drivers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "lastalerttime",
                table: "Drivers",
                newName: "LastAlertTime");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "Drivers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "fatiguelevel",
                table: "Drivers",
                newName: "FatigueLevel");

            migrationBuilder.RenameColumn(
                name: "drivercode",
                table: "Drivers",
                newName: "DriverCode");

            migrationBuilder.RenameColumn(
                name: "currentlocation",
                table: "Drivers",
                newName: "CurrentLocation");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "Drivers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "assignedvehicle",
                table: "Drivers",
                newName: "AssignedVehicle");

            migrationBuilder.RenameColumn(
                name: "driverid",
                table: "Drivers",
                newName: "DriverId");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "DriverLocations",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "DriverLocations",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "DriverLocations",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "driverid",
                table: "DriverLocations",
                newName: "DriverId");

            migrationBuilder.RenameColumn(
                name: "locationid",
                table: "DriverLocations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "DeviationAlerts",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "routeplanid",
                table: "DeviationAlerts",
                newName: "RoutePlanId");

            migrationBuilder.RenameColumn(
                name: "resolvedat",
                table: "DeviationAlerts",
                newName: "ResolvedAt");

            migrationBuilder.RenameColumn(
                name: "reason",
                table: "DeviationAlerts",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "DeviationAlerts",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "detectedat",
                table: "DeviationAlerts",
                newName: "DetectedAt");

            migrationBuilder.RenameColumn(
                name: "deviationalertid",
                table: "DeviationAlerts",
                newName: "DeviationAlertId");

            migrationBuilder.RenameIndex(
                name: "ix_deviationalerts_routeplanid",
                table: "DeviationAlerts",
                newName: "IX_DeviationAlerts_RoutePlanId");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "AspNetUserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetUserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "loginprovider",
                table: "AspNetUserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "twofactorenabled",
                table: "AspNetUsers",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "AspNetUsers",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "securitystamp",
                table: "AspNetUsers",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "AspNetUsers",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "phonenumberconfirmed",
                table: "AspNetUsers",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phonenumber",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "passwordhash",
                table: "AspNetUsers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalizedusername",
                table: "AspNetUsers",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalizedemail",
                table: "AspNetUsers",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockoutend",
                table: "AspNetUsers",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockoutenabled",
                table: "AspNetUsers",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "emailconfirmed",
                table: "AspNetUsers",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "AspNetUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "AspNetUsers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "concurrencystamp",
                table: "AspNetUsers",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "accessfailedcount",
                table: "AspNetUsers",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetUsers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "usernameindex",
                table: "AspNetUsers",
                newName: "UserNameIndex");

            migrationBuilder.RenameIndex(
                name: "emailindex",
                table: "AspNetUsers",
                newName: "EmailIndex");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "AspNetUserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "ix_aspnetuserroles_roleid",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "providerdisplayname",
                table: "AspNetUserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "providerkey",
                table: "AspNetUserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "loginprovider",
                table: "AspNetUserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "ix_aspnetuserlogins_userid",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "claimvalue",
                table: "AspNetUserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claimtype",
                table: "AspNetUserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetUserClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_aspnetuserclaims_userid",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "normalizedname",
                table: "AspNetRoles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetRoles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "concurrencystamp",
                table: "AspNetRoles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetRoles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "rolenameindex",
                table: "AspNetRoles",
                newName: "RoleNameIndex");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "AspNetRoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "claimvalue",
                table: "AspNetRoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claimtype",
                table: "AspNetRoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetRoleClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_aspnetroleclaims_roleid",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.UpdateData(
                table: "RoutePlans",
                keyColumn: "Status",
                keyValue: null,
                column: "Status",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "RoutePlans",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "RoutePlans",
                keyColumn: "RouteCode",
                keyValue: null,
                column: "RouteCode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RouteCode",
                table: "RoutePlans",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RouteDescription",
                table: "RoutePlans",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DriverLocations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutePlans",
                table: "RoutePlans",
                column: "RoutePlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PanicEvents",
                table: "PanicEvents",
                column: "PanicEventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                column: "DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverLocations",
                table: "DriverLocations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviationAlerts",
                table: "DeviationAlerts",
                column: "DeviationAlertId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviationAlerts_RoutePlans_RoutePlanId",
                table: "DeviationAlerts",
                column: "RoutePlanId",
                principalTable: "RoutePlans",
                principalColumn: "RoutePlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PanicEvents_RoutePlans_RoutePlanId",
                table: "PanicEvents",
                column: "RoutePlanId",
                principalTable: "RoutePlans",
                principalColumn: "RoutePlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlans_Drivers_DriverId",
                table: "RoutePlans",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlans_Vehicles_VehicleId",
                table: "RoutePlans",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
