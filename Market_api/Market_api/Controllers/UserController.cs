using Microsoft.AspNetCore.Mvc;
using Market_api.Models;
using System.Data.SqlClient;
using System.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly string _conString;
        public UserController()
        {
            _conString = ConnectionString.getConnectionString();
        }

        // GET: api/<UserController>
        [HttpPost("LoginUser")]
        public IActionResult Get(User userin)
        {
            try
            {
                string txt = string.Format("select * from Table_Users where Account_User='{0}' and Pass_User='{1}'", userin.Account_User , userin.Pass_User);
                using(SqlConnection cn=new SqlConnection(_conString))
                {
                    cn.Open();
                    using(SqlCommand cmd=new SqlCommand(txt, cn))
                    {
                        using(SqlDataReader rdr=cmd.ExecuteReader())
                        {
                            rdr.Read();
                            if (rdr.HasRows)
                            {
                                User userout = new User()
                                {
                                    Id_User = rdr.GetInt32("Id_User"),
                                    Account_User = rdr.GetString("Account_User"),
                                    Name_User = rdr.GetString("Name_User"),
                                    Pass_User = rdr.GetString("Pass_User"),
                                    State_User = rdr.GetInt32("State_User"),
                                    Date_User = rdr.GetDateTime("Date_User")
                                };
                                return Ok(userout);
                            }
                            else
                                return NotFound("المستخدم مهلوش");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

       [HttpGet("AllUser")]
       public IActionResult Get()
        {
            try
            {
                List<User> users = new List<User>();
                using(SqlConnection cn=new SqlConnection(_conString))
                {
                    cn.Open();
                    using(SqlCommand cmd=new SqlCommand("select * from Table_Users", cn))
                    {
                        using(SqlDataReader rdr=cmd.ExecuteReader())
                        {
                            while(rdr.Read())
                            {
                                User user = new User()
                                {
                                    Id_User = rdr.GetInt32("Id_User"),
                                    Account_User=rdr.GetString("Account_User"),
                                    Name_User = rdr.GetString("Name_User"),
                                    Pass_User = rdr.GetString("Pass_User"),
                                    Date_User = rdr.GetDateTime("Date_User"),
                                    State_User = rdr.GetInt32("State_User"),
                                };
                                users.Add(user);
                            }
                            return Ok(users);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpGet("SearchUser")]
        public IActionResult Get(int id_User)
        {
            try
            {
                string txt = string.Format("select * from Table_Users where  Id_User={0}", id_User);
                using (SqlConnection cn = new SqlConnection(_conString))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(txt, cn))
                    {
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            rdr.Read();
                            if (rdr.HasRows)
                            {
                                User user = new User()
                                {
                                    Id_User = rdr.GetInt32("Id_User"),
                                    Account_User = rdr.GetString("Account_User"),
                                    Name_User = rdr.GetString("Name_User"),
                                    Pass_User = rdr.GetString("Pass_User"),
                                    Date_User = rdr.GetDateTime("Date_User"),
                                    State_User = rdr.GetInt32("State_User"),

                                };

                                return Ok(user);
                            }
                            else
                                return NotFound("Not Found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("AddUser")]
        public IActionResult post(User user)
        {
            try
            {
                string txt = string.Format("insert into Table_Users(Id_User,Account_User,Name_User,Pass_User, State_User) values({0},'{1}','{2}','{3}', '{4}')", user.Id_User,user.Account_User,user.Name_User,user.Pass_User, user.State_User);
                using(SqlConnection con=new SqlConnection(_conString))
                {
                    con.Open();
                    using(SqlCommand cmd=new SqlCommand(txt,con))
                    {
                      int result=  cmd.ExecuteNonQuery();
                        if (result > 0)
                            return Ok("تمت الاضافة");
                        else
                            return NotFound("لم تتم الاضافة");
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpPut("UpdateUser")]
        public IActionResult put(User user)
        {
            try
            {
                string txt = string.Format("update Table_Users set Account_User='{1}',Name_User='{2}',Pass_User='{3}' ,State_User='{4}' where Id_User={0}", user.Id_User, user.Account_User, user.Name_User, user.Pass_User, user.State_User);
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(txt, con))
                    {
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                            return Ok("تم التعديل");
                        else
                            return NotFound("لم يتم التعديل");
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("DeleteUser")]
        public IActionResult delete(int id_User)
        {
            try
            {
                string txt = string.Format("delete from Table_Users where Id_User={0}",id_User);
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(txt, con))
                    {
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                            return Ok("تم الحذف");
                        else
                            return NotFound("لم يتم الحذف");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
