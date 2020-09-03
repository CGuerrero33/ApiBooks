using ApiBooksClient.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ApiBooksClient.Controllers
{
    public class BooksController : Controller
    {
        public BooksController()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        public ActionResult Index(int? id)
        {
            List<Book> books = new List<Book>();

            if (id != null)
            {
                Book book = GetBook((int)id);

                if (book != null) books.Add(book);
            }
            else
            {
                books = GetBooks();
            }

            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Book());
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            bool result = false;

            if (book != null)
            {
                result = CreateBook(book);
            }
            else
            {
                return HttpNotFound();
            }

            if (!result)
            {
                ViewBag.Error = "Ha ocurrido un error!";

                return View(book);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book book = GetBook(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(int id, Book book)
        {
            if (book == null)
            {
                return HttpNotFound();
            }

            bool result = EditBook(id, book);

            if (!result)
            {
                ViewBag.Error = "Ha ocurrido un error!";

                return View(book);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            Book book = GetBook(id);

            return View(book);
        }

        public ActionResult Delete(int id)
        {
            bool result = DeleteBook(id);

            if (!result)
            {
                ViewBag.Error = "Ha ocurrido un error!";
            }

            List<Book> books = GetBooks();

            return View("Index",books);
        }


        #region services

        private List<Book> GetBooks()
        {
            try
            {
                List<Book> result = new List<Book>();

                HttpClient client = new HttpClient();
                var data = client.GetAsync("https://localhost:44380/api/Books").ContinueWith(d =>
                {
                    if (d.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var content = d.Result.Content.ReadAsAsync<List<Book>>();
                        content.Wait();


                        result = content.Result;
                    }
                    else
                    {
                        result = null;
                    }
                });

                data.Wait();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private Book GetBook(int id)
        {
            try
            {
                Book result = new Book();

                HttpClient client = new HttpClient();
                var data = client.GetAsync($"https://localhost:44380/api/Books/{id}").ContinueWith(d =>
                {
                    if (d.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var content = d.Result.Content.ReadAsAsync<Book>();
                        content.Wait();

                        result = content.Result;
                    }
                    else
                    {
                        result = null;
                    }
                });

                data.Wait();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool DeleteBook(int id)
        {
            try
            {
                bool result = false;

                HttpClient client = new HttpClient();
                var data = client.DeleteAsync($"https://localhost:44380/api/Books/{id}").ContinueWith(d =>
                {
                    if (d.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                });

                data.Wait();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateBook(Book book)
        {
            try
            {
                bool result = false;


                var bookJson = JsonConvert.SerializeObject(book);

                var data = new StringContent(bookJson, Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();


                var response = client.PostAsync($"https://localhost:44380/api/Books", data).ContinueWith(d =>
                  {
                      if (d.Result.StatusCode == System.Net.HttpStatusCode.OK)
                      {
                          result = true;
                      }
                      else
                      {
                          result = false;
                      }
                  });

                response.Wait();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool EditBook(int id, Book book)
        {
            try
            {
                bool result = false;


                var bookJson = JsonConvert.SerializeObject(book);

                var data = new StringContent(bookJson, Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();


                var response = client.PutAsync($"https://localhost:44380/api/Books/{id}", data).ContinueWith(d =>
                {
                    if (d.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                });

                response.Wait();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}