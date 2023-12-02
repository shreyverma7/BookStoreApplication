using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookBusiness bookBusiness;
        public BookController(IBookBusiness bookBusiness)
        {
            this.bookBusiness = bookBusiness;
        }
        NlogOperation nlog = new NlogOperation();

        [HttpPost]
        [Route("AddingBook")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddBook(Books book)
        {
            try
            {
                var result = await this.bookBusiness.AddBook(book);
                if (result != 0)
                {
                    nlog.LogInfo("Book Added Successfully");
                    return this.Ok(new { Status = true, Message = "Book Added Successfully", Data = book });
                }
                return this.BadRequest(new { Status = false, Message = "Book Added Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<ActionResult> GetAllBooks()
        {
            try
            {
                var result = this.bookBusiness.GetAllBooks();
                if (result != null)
                {
                    nlog.LogInfo("All Books Found");
                    return this.Ok(new { Status = true, Message = "All Books Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Books Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateABook")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateBook(Books book)
        {
            try
            {
                var result = this.bookBusiness.UpdateBook(book);
                if (result)
                {
                    nlog.LogInfo("Book Updated Successfully");
                    return this.Ok(new { Status = true, Message = "Book Updated Successfully", Data = book });
                }
                return this.BadRequest(new { Status = false, Message = "Book Updated Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteABook")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookBusiness.DeleteBook(BookId);
                if (result)
                {
                    nlog.LogInfo("Book Deleted Successfully");
                    return this.Ok(new { Status = true, Message = "Book Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Book Deleted Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("UploadImage")]
        public ActionResult Image(IFormFile file, int BookId)
        {
            try
            {
                var result = this.bookBusiness.Image(file, BookId);
                if (result)
                {
                    nlog.LogInfo("Image Uploaded Successfully");
                    return this.Ok(new { Status = true, Message = "Image Uploaded Successfully", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Uploading Image Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetBookById")]
        public async Task<ActionResult> GetBookById(int BookId)
        {
            try
            {
                var result = this.bookBusiness.GetBookById(BookId);
                if (result != null)
                {
                    nlog.LogInfo("Book by Id Found");
                    return this.Ok(new { Status = true, Message = "Book by Id Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Book Found by id" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
