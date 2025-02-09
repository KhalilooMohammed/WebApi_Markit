using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Market_api.Models;
using System.Data.SqlClient;
using System.Data;
namespace Market_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly string _conString;
        public ConnectionController()
        {
            _conString = ConnectionString.getConnectionString();
        }

        [HttpGet("Connect")]
        public IActionResult Get()
        {
            try
            {
                SqlConnection cn = new SqlConnection(_conString);
                cn.Open();
                if (cn.State == ConnectionState.Open)
                    return Ok("متصل");
                else
                    return NotFound("غير متصل");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
                
            }
        }

    }
}
