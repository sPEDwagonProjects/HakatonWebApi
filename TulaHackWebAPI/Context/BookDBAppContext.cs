using Microsoft.EntityFrameworkCore;
using TulaHackWebAPI.Model;
using TulaHackWebAPI.Model.Books;

namespace TulaHackWebAPI.Context
{
    public class BookDBAppContext : DBContextBase
    {

        private DbSet<Publisher> Publishers { get; set; }
        private DbSet<Lang> Langs { get; set; }
        private DbSet<Author> Authors { get; set; }
        private DbSet<Charpter> Charpters { get; set; }
        private DbSet<BookMinimal> Books { get; set; }
        private DbSet<BookType> Types { get; set; }
        private DbSet<Booking> Booking { get; set; }
        private DbSet<Favorites> Favorites { get; set; }

        public async Task<bool> EditBook(int id,string title,string author, string publisher)
        {
            try
            {
              var book =  new BookMinimal()
                {
                    Id = id,
                    AuthorId = await GetAuthorIdByName(author),
                    PublisherId= await GetPublisherIdByName(publisher),
                    
                };
                Books.Update(book);
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public async Task<int> GetAuthorIdByName(string name)
        {
            try
            {
                return   Authors.First(x => x.Name == name).Id;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> GetPublisherIdByName(string title)
        {
            try
            {
                return Publishers.First(x => x.Title == title).Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<bool> DeleteBooking(int bookingId)
        {
            try
            {
                Booking.Remove(Booking.First(x => x.Id == bookingId));
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public async Task<bool> DeleteFavorite(int favoritesId)
        {
            try
            {
                Favorites.Remove(Favorites.First(x => x.Id == favoritesId));
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public async Task<bool> AddBooking(int bookId, int userId)
        {
            try
            {
                Booking booking = new Booking();
                booking.Idbook = bookId;
                booking.Iduser = userId;

                Booking.Add(booking);
                
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public async Task<bool> AddFavorite(int bookid, int userId)
        {
            try
            {
                Favorites favorite = new Favorites();
                favorite.BookId = bookid;
                favorite.UserId = userId;

                Favorites.Add(favorite);

                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { return false; }
        }




        #region Search
        public async Task<List<BookMinimal>> SearchByCharpter(string text)
        {
            List<BookMinimal> results = new List<BookMinimal>();
            var charptersId = await  Charpters.Where(x => x.Title.Contains(text)).Select(x=>x.Id).ToListAsync();
            foreach(var id in charptersId)
            {
                var books = await Books.Where(x => x.CharpterId == id).ToListAsync();
                foreach(var book in books)
                {
                    results.Add(book);
                }
                
            }
            return results;
            
        }
        public async Task<List<BookMinimal>> SearchByPublisher(string text)
        {
            List<BookMinimal> results = new List<BookMinimal>();
            var publishersId = await Publishers.Where(x => x.Title.Contains(text)).Select(x => x.Id).ToListAsync();
            foreach (var id in publishersId)
            {
                var books = await Books.Where(x => x.PublisherId == id).ToListAsync();
                foreach (var book in books)
                {
                    results.Add(book);
                }

            }
            return results;
        }
        #endregion

        #region Get
       
        public async Task<BookMinimal> GetBookById(int id)
        {
            return  await Books.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<BookFull> GetFullBookById(int id)
        {
          
                BookFull bookFull = new BookFull();

                var res = await GetBookById(id);
                bookFull.Id = res.Id;
                bookFull.Title = res.Title;

                bookFull.Author = await GetAuthorById((int)res.AuthorId);
                bookFull.Publisher = await GetPublisherById((int)res.PublisherId);
                bookFull.Charpter = await GetCharpterById((int)res.CharpterId);
                bookFull.Lang = await GetLangById((int)res.LangId);
                bookFull.Type = await GetBookTypeById((int)res.Type);
            
                return bookFull;
            
        }
        public async Task<List<BookMinimal>> GetBooks(int count,int offset)
        {
            return await Books.Skip(offset).Take(count).ToListAsync();
        }
        public async Task<List<BookMinimal>> GetBooksByCharpter(int id)
        {
            return await Books.Where(x => x.CharpterId == id).ToListAsync();
        }
        public async Task<List<BookMinimal>> GetBooksByPublisher(int id)
        {
            return await Books.Where(x => x.PublisherId == id).ToListAsync();
        }
        public async Task<List<BookMinimal>> GetBooksByLangs(int id)
        {
            return await Books.Where(x => x.LangId == id).ToListAsync();
        }
        public async Task<List<BookMinimal>> GetBooksByType(int id)
        {
            return await Books.Where(x => x.Type == id).ToListAsync();
        }


        public async Task<Lang?> GetLangById(int id)
        {
            return await Langs.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Author?> GetAuthorById(int id)
        {

            return await Authors.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Publisher?> GetPublisherById(int id)
        {
            return await Publishers.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Charpter?> GetCharpterById(int id)
        {
            return await Charpters.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<BookType?> GetBookTypeById(int id)
        {
           return  await Types.FirstOrDefaultAsync(x => x.Id == id);
        } 
        
        public async Task<List<Publisher>> GetPublishers()
        {
            return await Publishers.ToListAsync();
        }
        public async Task<List<Author> > GetAuthors()
        {
           return await  Authors.ToListAsync();
        }
        public async Task<List<Charpter>> GetCharpters()
        {
            return await Charpters.ToListAsync();
        }
        public async Task<List<Lang>> GetLangs()
        {
            return await Langs.ToListAsync();
        }

        public async Task<List<BookType>> GetTypes()
        {
            return await  Types.ToListAsync();
        }
        
        public async Task<List<Booking>> GetBooking(User user)
        {
            return await Booking.Where(x=>x.Iduser == user.Id).ToListAsync();
        }

        public async Task<List<Favorites>> GetFavorites(User user)
        {
            return await Favorites.Where(x => x.UserId == user.Id).ToListAsync();
        }

        #endregion


    }
}
