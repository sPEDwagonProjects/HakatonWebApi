namespace TulaHackWebAPI.Model
{
    public class BookMinimal
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public int? Type { get; set; }
        public int? PublisherId { get; set; }
        public int? LangId { get; set; }
        public int? CharpterId { get; set; }
        public bool Taken { get; set; }
    }
}
