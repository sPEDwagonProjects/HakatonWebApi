using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TulaHackWebAPI.Context;

namespace TulaHackWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FavoritesController: ControllerBase
    {
      

        [HttpGet("/favorites.get")]
        public async Task<IActionResult> FavoritesGet(string login)
        {

            try
            {

                using (UserDBAppContext userDB = new UserDBAppContext())
                {
                    var user = await userDB.GetUserByLogin(login);
                    using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                    {
                        return Ok((await bookDBAppContext.GetFavorites(user)));
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("/Favorites.add")]
        public async Task<IActionResult> FavoritesAdd(int bookId, string userLogin)
        {
            try
            {
                using (UserDBAppContext userDB = new UserDBAppContext())
                {
                    var user = await userDB.GetUserByLogin(userLogin);

                    using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                    {
                        var res = await bookDBAppContext.AddFavorite(bookId,user.Id);

                        return Ok(res);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Favorites.delete")]
        public async Task<IActionResult> FavoritesRemove(int Id)
        {
            try
            {

                using (BookDBAppContext bookDBAppContext = new BookDBAppContext())
                {
                    var res = await bookDBAppContext.DeleteFavorite(Id);

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
