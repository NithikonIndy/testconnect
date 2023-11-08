using Renci.SshNet;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
string sshHost = "20.55.66.62";
string sshUsername = "Admin492";
string sshPassword = "ProjectCPE492";
int sshPort = 22;

// ��˹������š���������� MySQL
string mysqlHost = "localhost"; // ���ͧ�ҡ����������� MySQL ��ҹ��� SSH
int mysqlPort = 3306; // �����Ţ���� MySQL
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

// ���ҧ SSH Client
/*using (var sshClient = new SshClient(sshHost, sshPort, sshUsername, sshPassword))
{
    sshClient.Connect();

    // ���ҧ SSH Tunnel ������������ MySQL ��ҹ��� SSH
    var tunnel = new ForwardedPortLocal("localhost", (uint)mysqlPort, mysqlHost, (uint)mysqlPort);
    sshClient.AddForwardedPort(tunnel);
    tunnel.Start();

    // ��˹������������ MySQL
    string mysqlConnectionString = $"Server={mysqlHost};Port={tunnel.BoundPort};Database={mysqlDatabase};User={mysqlUsername};Password={mysqlPassword}";

    // �������� MySQL ��ҹ��� SSH
    using (var mysqlConnection = new MySqlConnection(mysqlConnectionString))
    {
        mysqlConnection.Open();

        // �ӧҹ�Ѻ MySQL ������

        mysqlConnection.Close();
    }

    // ��ش SSH Tunnel
    tunnel.Stop();
    sshClient.Disconnect();
}
*/