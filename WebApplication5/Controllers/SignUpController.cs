using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SignUpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select * from dbo.Account";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("E-learning");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] account user)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("E-learning");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO dbo.Account (username, email, password, role, photo) 
                                 VALUES (@Username, @Email, @Password, @Role, @Photo)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.username);
                        cmd.Parameters.AddWithValue("@Email", user.email);
                        cmd.Parameters.AddWithValue("@Password", user.coverttoencrypt(user.password));
                        cmd.Parameters.AddWithValue("@Role", user.role);
                        cmd.Parameters.AddWithValue("@Photo", user.photo);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                return Ok(new { message = "Added successfully!!" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Failed to add!!" });
            }
        }
    }



}
