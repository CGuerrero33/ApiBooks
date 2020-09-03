using ApiBooks.Models.Book;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiBooks.Controllers
{
    public class BooksController : ApiController
    {
        [HttpGet]
        public IEnumerable<Book> Books()
        {
            try
            {
                List<Book> books = GetBooks();

                return books;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        public Book Books(int id)
        {
            try
            {
                Book book = GetBook(id);

                return book;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, Book book)
        {
            try
            {
                if (book == null)
                {

                    return BadRequest();
                }

                EditBook(id, book);

                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

        [HttpPost]
        public IHttpActionResult Post(Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest();
                }


                CreateBook(book);

                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                bool result = DeleteBook(id);

                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
            }
        }

        #region services

        private List<Book> GetBooks()
        {
            try
            {
                List<Book> result = new List<Book>();

                HttpClient client = new HttpClient();
                var data = client.GetAsync("https://fakerestapi.azurewebsites.net/api/Books").ContinueWith(d =>
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
                var data = client.GetAsync($"https://fakerestapi.azurewebsites.net/api/Books/{id}").ContinueWith(d =>
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
                var data = client.DeleteAsync($"https://fakerestapi.azurewebsites.net/api/Books/{id}").ContinueWith(d =>
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


                var response = client.PostAsync($"https://fakerestapi.azurewebsites.net/api/Books", data).ContinueWith(d =>
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


                var response = client.PutAsync($"https://fakerestapi.azurewebsites.net/api/Books/{id}", data).ContinueWith(d =>
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
