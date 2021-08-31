using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DemoClient.Models;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DemoClient.Controllers
{
    public class HomeController : Controller
    {
        private string BaseUrl = "https://localhost:44337/";
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(FormCollection f)
        {
            string name = f.Get("name").ToString();
            string pass = f.Get("pass").ToString();
            User user = new User();
            user.name = name;
            user.password = pass;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.PostAsJsonAsync("/api/users/checkuser", user);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("ManagerUser", "Home");
                }
            }
            return View();
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(FormCollection f, HttpPostedFileBase file)
        {
            var rand = new Random();
            string ID = "Kh9jas" + rand.Next(10, 99);
            string name = f.Get("name").ToString();
            string pass = f.Get("pass").ToString();
            string email = f.Get("email").ToString();
            string phone = f.Get("phone").ToString();
            string addre = f.Get("address").ToString();
            string path = Server.MapPath("/img/");
            file.SaveAs(path + Path.GetFileName(file.FileName));
            string emoij = file.FileName;

            User us = new User();
            us.id = ID;
            us.name = name;
            us.password = pass;
            us.email = email;
            us.phone = phone;
            us.address = addre;
            us.emoij = emoij;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicaiton/json"));
                HttpResponseMessage res = await client.PostAsJsonAsync("/api/users/postusers", us);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public async Task<ActionResult> ManagerUser()
        {
            var list = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("/api/users");
                if (res.IsSuccessStatusCode)
                {
                    var lsdm = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<User>>(lsdm);
                }
            }
            return View(list);
        }

        public async Task<ActionResult> DeleteUser(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.DeleteAsync("/api/users/deleteuser?id="+id);
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("ManagerUser", "Home");
                }
            }
            return RedirectToAction("ManagerUser", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Add(FormCollection f, HttpPostedFileBase file)
        {
            var rand = new Random();
            string ID = f.Get("id").ToString();
            string name = f.Get("name").ToString();
            string pass = f.Get("pass").ToString();
            string email = f.Get("email").ToString();
            string phone = f.Get("phone").ToString();
            string addre = f.Get("address").ToString();
            string path = Server.MapPath("/img/");
            file.SaveAs(path + Path.GetFileName(file.FileName));
            string emoij = file.FileName;

            User us = new User();
            us.id = ID;
            us.name = name;
            us.password = pass;
            us.email = email;
            us.phone = phone;
            us.address = addre;
            us.emoij = emoij;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicaiton/json"));
                HttpResponseMessage res = await client.PostAsJsonAsync("/api/users/postusers", us);
                if (res.IsSuccessStatusCode)
                {
                    return View();
                }
            }

            return View();
        }
    }
}