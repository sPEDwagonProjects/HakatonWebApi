namespace TulaHackWebAPI.Model.Books
{
    public class BookFull
    {

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public Author? Author { get; set; }
        public Publisher? Publisher { get; set; }
        public Lang? Lang { get; set; }
        public Charpter? Charpter { get; set; }
        public bool Availability { get; set; }
    }
}
