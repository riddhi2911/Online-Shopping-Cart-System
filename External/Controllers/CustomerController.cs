using Microsoft.AspNetCore.Mvc;
using External.Models;
using System.Data.SqlClient;
using System.Data;
namespace External.Controllers
{
    public class CustomerController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-M4SMM1S\\SQLEXPRESS;Initial Catalog=Product;Integrated Security=True;Pooling=False;");
        SqlCommand cmd;
        SqlDataAdapter adp;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(string em , string pass)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Customer where Cemail = @e and CPassword = @p", con);
            cmd.Parameters.AddWithValue("@e", em);
            cmd.Parameters.AddWithValue("@p", pass);
            SqlDataReader read = cmd.ExecuteReader();

            if(read.Read())
            {
                HttpContext.Session.SetString("id", read["Cid"].ToString());
                con.Close();
                return RedirectToAction("Dashboard");
            } else
            {
                con.Close();
                ViewBag.msg = "Invalid";
                return View();
            }          
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Customer c)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Customer values(@name,@em,@pass,@add,@gen)", con);
            cmd.Parameters.AddWithValue("@name", c.Cname);
            cmd.Parameters.AddWithValue("@em", c.Cemail);
            cmd.Parameters.AddWithValue("@pass", c.CPassword);
            cmd.Parameters.AddWithValue("@add", c.Cadd);
            cmd.Parameters.AddWithValue("@gen", c.Cgender);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Login");
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult MyProfile()
        {
            con.Open();
            string id = HttpContext.Session.GetString("id");
            SqlDataAdapter adp = new SqlDataAdapter("select * from Customer where Cid=@id", con);
            adp.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();

            return View(dt);
        }

        public IActionResult UpdateProf()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateProf(Customer c)
        {
            int id =Convert.ToInt32(HttpContext.Session.GetString("id"));
            con.Open();
            SqlCommand cmd = new SqlCommand("update Customer set Cname=@name,Cemail=@em,CPassword=@pass,Cadd=@add,Cgender=@gen where Cid=@id", con);
            cmd.Parameters.AddWithValue("@name", c.Cname);
            cmd.Parameters.AddWithValue("@em", c.Cemail);
            cmd.Parameters.AddWithValue("@pass", c.CPassword);
            cmd.Parameters.AddWithValue("@add", c.Cadd);
            cmd.Parameters.AddWithValue("@gen", c.Cgender);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("MyProfile");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
