using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using External.Models;
using System.Data;
namespace External.Controllers
{
    public class ProdController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4SMM1S\\SQLEXPRESS;Initial Catalog=Product;Integrated Security=True;Pooling=False;");
        SqlCommand cmd;
        SqlDataAdapter adp;
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ViewProd()
        {
            con.Open();
            string id = HttpContext.Session.GetString("id");
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Prod", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            return View(dt);
        }

        [HttpGet]
        public IActionResult AddProd(int id,int pri)
        {
            HttpContext.Session.SetString("pid", id.ToString());
            HttpContext.Session.SetString("price", pri.ToString());
            return View();
        }

        [HttpPost]
        public IActionResult AddProd(int que)
        {
            int cid =Convert.ToInt32(HttpContext.Session.GetString("id"));
            int pid =Convert.ToInt32(HttpContext.Session.GetString("pid"));
            int price =Convert.ToInt32(HttpContext.Session.GetString("price"));

            DateTime date = DateTime.Now.Date;
            int total = price * que;
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Card values(@cid,@pid,@date,@que,@price,@total)", con);
            cmd.Parameters.AddWithValue("@cid", cid);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@que", que);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ViewProd");
        }

        public IActionResult MyProd()
        {
            int cid = Convert.ToInt32(HttpContext.Session.GetString("id"));
            con.Open();
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Card where Cid=@cid", con);
            adp.SelectCommand.Parameters.AddWithValue("@cid", cid);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            return View(dt);
        }
        [HttpGet]
        public IActionResult DeleteProd(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Card where carid=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("MyProd");
        }
    }
}
