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
        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                Books.Remove(await GetBookById(id));
                await SaveChangesAsync();
                return true;
            }
            catch(Exception ex) { return false; }
        }
        
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

        #endregion


    }
}
