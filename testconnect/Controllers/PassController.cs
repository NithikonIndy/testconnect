using Renci.SshNet;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using testconnect.Models;

namespace testconnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassController : Controller
    {

        private readonly IConfiguration _configuration;

        public PassController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetStorePasswords()
        {
            try
            {
                string sshHost = "20.55.66.62";
                string sshUsername = "Admin492";
                string sshPassword = "ProjectCPE492";
                int sshPort = 22;
                string mysqlHost = "localhost"; ;
                int mysqlPort = 3306;
                string mysqlUsername = "Admintestdb";
                string mysqlPassword = "ProjectCPE492?";
                string mysqlDatabase = "testdb";

                using (var sshClient = new SshClient(sshHost, sshPort, sshUsername, sshPassword))
                {
                    sshClient.Connect();

                    var tunnel = new ForwardedPortLocal("localhost", (uint)mysqlPort, mysqlHost, (uint)mysqlPort);
                    sshClient.AddForwardedPort(tunnel);
                    tunnel.Start();

                    string mysqlConnectionString = "Server=localhost;Port=3306;Database=testdb;Uid=root;Pwd=ProjectCPE492;";

                    
                    using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;Uid=root;Pwd=ProjectCPE492;"))
                    {
                        connection.Open();
                        Console.WriteLine("SSH tunnel is established.");
                        // สร้างคำสั่ง SQL สำหรับการดึงข้อมูล
                        string sqlQuery = "SELECT * FROM storepassword";
                        MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                        var storePasswords = new List<storepassword>();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                while (reader.Read())
                                {
                                    storePasswords.Add(new storepassword
                                    {
                                        UId = reader.GetInt32(0), 
                                        Username = reader.GetString(1), 
                                        Password = reader.GetString(2) 
                                    });
                                }
                            }
                        }

                        return Ok(storePasswords);
                    }

                    // หยุด SSH Tunnel
                    tunnel.Stop();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"เกิดข้อผิดพลาด: {ex.Message}");
            }
        }
    }
}
