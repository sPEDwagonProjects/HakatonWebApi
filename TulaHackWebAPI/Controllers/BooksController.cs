using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TulaHackWebAPI.Context;
using TulaHackWebAPI.Model.Books;

namespace TulaHackWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
  
    public class BooksController:ControllerBase
    {

        [HttpGet("/books.getBookById")]
        public async Task<IActionResult> GetBookByid(int id, bool fullInfo = false)
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    if (fullInfo == false)
                    {
                        var res = await bookDBAppContext.GetBookById(id);
                        return Ok(res);
                    }
                    else
                    {
                        var data = await bookDBAppContext.GetFullBookById(id);
                        return Ok(data);
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/books.getBooks")]
        public async Task<IActionResult> GetBooks(int count = 100, int offset = 0, bool fullInfo = false)
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    if (fullInfo == false)
                    {
                        var res = await bookDBAppContext.GetBooks(count, offset);
                        return Ok(res);
                    }
                    else { 

                        var data = await bookDBAppContext.GetBooks(count, offset);
                        List<BookFull> fullData = new List<BookFull>();
                        foreach(var item in data)
                        {
                            var res = await  bookDBAppContext.GetFullBookById(item.Id);
                            fullData.Add(res);
                        }


                        return Ok(fullData);
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/books.getByPublisher")]
        public async Task<IActionResult> GetBookByPublisherAsync(int id = -1)
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    var res = await bookDBAppContext.GetBooksByPublisher(id);

                    return Ok(res);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("/books.getByCharpter")]
        public async Task<IActionResult> GetBookByCharpter(int id)
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    var res = await bookDBAppContext.GetBooksByCharpter(id);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("/books.getCharpters")]
        public async Task<IActionResult> GetCharpters()
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    var res = await bookDBAppContext.GetCharpters();
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpGet("/books.getPublishers")]
        public async Task<IActionResult> GetPublishers()
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    var res = await bookDBAppContext.GetCharpters();
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("/books.getTypes")]
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    var res = await bookDBAppContext.GetTypes();
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("/books.getByType")]
        public async Task<IActionResult> GetByType(int id)
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    var res = await bookDBAppContext.GetBooksByType(id);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
