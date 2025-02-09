using Microsoft.AspNetCore.Mvc;
using Market_api.Models;
using System.Data.SqlClient;
using System.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {


        private readonly string _conString;
        public ItemController()
        {
            _conString = ConnectionString.getConnectionString();
        }

        // GET: api/<ItemController>
     

       [HttpGet("AllItem")]
       public IActionResult Get()
        {
            try
            {
                List<Item> Items = new List<Item>();
                using(SqlConnection cn=new SqlConnection(_conString))
                {
                    cn.Open();
                    using(SqlCommand cmd=new SqlCommand("select * from Table_Items", cn))
                    {
                        using(SqlDataReader rdr=cmd.ExecuteReader())
                        {
                            while(rdr.Read())
                            {
                                Item Item = new Item()
                                {
                                    Id_Item = rdr.GetInt32("Id_Item"),
                                    Com_Item=rdr.GetString("Com_Item"),
                                    Name_Item = rdr.GetString("Name_Item"),
                                };
                                Items.Add(Item);
                            }
                            return Ok(Items);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpGet("SearchItem")]
        public IActionResult Get(int id_Item)
        {
            try
            {
                string txt = string.Format("select * from Table_Items where  Id_Item={0}", id_Item);
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
                                Item Item = new Item()
                                {
                                    Id_Item = rdr.GetInt32("Id_Item"),
                                    Name_Item = rdr.GetString("Name_Item"),
                                    Com_Item = rdr.GetString("Com_Item"),

                                };

                                return Ok(Item);
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

        [HttpPost("AddItem")]
        public IActionResult post(Item Item)
        {
            try
            {
                string txt = string.Format("insert into Table_Items(Id_Item,Name_Item,Com_Item) values({0},'{1}','{2}')",Item.Id_Item,Item.Name_Item,Item.Com_Item);
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



        [HttpPut("UpdateItem")]
        public IActionResult put(Item Item)
        {
            try
            {
                string txt = string.Format("update Table_Items set Name_Item='{1}',Com_Item='{2}' where Id_Item={0}", Item.Id_Item, Item.Name_Item, Item.Com_Item);
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



        [HttpDelete("DeleteItem")]
        public IActionResult delete(int id_Item)
        {
            try
            {
                string txt = string.Format("delete from Table_Items where Id_Item={0}",id_Item);
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
