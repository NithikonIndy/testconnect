using Renci.SshNet;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
string sshHost = "20.55.66.62";
string sshUsername = "Admin492";
string sshPassword = "ProjectCPE492";
int sshPort = 22;

// กำหนดข้อมูลการเชื่อมต่อ MySQL
string mysqlHost = "localhost"; // เนื่องจากที่เชื่อมต่อ MySQL ผ่านท่อ SSH
int mysqlPort = 3306; // หมายเลขพอร์ต MySQL
string mysqlUsername = "Admintestdb";
string mysqlPassword = "ProjectCPE492?";
string mysqlDatabase = "testdb";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// สร้าง SSH Client
/*using (var sshClient = new SshClient(sshHost, sshPort, sshUsername, sshPassword))
{
    sshClient.Connect();

    // สร้าง SSH Tunnel เพื่อเชื่อมต่อ MySQL ผ่านท่อ SSH
    var tunnel = new ForwardedPortLocal("localhost", (uint)mysqlPort, mysqlHost, (uint)mysqlPort);
    sshClient.AddForwardedPort(tunnel);
    tunnel.Start();

    // กำหนดการเชื่อมต่อ MySQL
    string mysqlConnectionString = $"Server={mysqlHost};Port={tunnel.BoundPort};Database={mysqlDatabase};User={mysqlUsername};Password={mysqlPassword}";

    // เชื่อมต่อ MySQL ผ่านท่อ SSH
    using (var mysqlConnection = new MySqlConnection(mysqlConnectionString))
    {
        mysqlConnection.Open();

        // ทำงานกับ MySQL ได้ที่นี่

        mysqlConnection.Close();
    }

    // หยุด SSH Tunnel
    tunnel.Stop();
    sshClient.Disconnect();
}
*/