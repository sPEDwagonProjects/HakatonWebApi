using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TulaHackWebAPI.Context;

namespace TulaHackWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class BookingController:ControllerBase
    {

        [HttpGet("/Booking.get")]
        public async Task<IActionResult> BookingGet(string login)
        {

                try { 
                    
                    using (UserDBAppContext userDB = new UserDBAppContext())
                    {
                    var user = await userDB.GetUserByLogin(login);
                    using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                        {
                            return Ok((await bookDBAppContext.GetBooking(user)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
        }


        [HttpGet("/Booking.add")]
        public async Task<IActionResult> BookingAdd(int bookId, string userLogin)
        {
            try
            {
                using (UserDBAppContext userDB = new UserDBAppContext())
                {
                    var user = await userDB.GetUserByLogin(userLogin);

                    using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                    {
                        var res = await bookDBAppContext.AddBooking(bookId,user.Id);
                      
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Booking.delete")]
        public async Task<IActionResult> BookingRemove(int id)
        {
            try
            {
                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    var res = await bookDBAppContext.DeleteBooking(id);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
