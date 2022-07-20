using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;


namespace MoviesAPI.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [StringLength(10)]
        [FirstLetterUpperCase]
        public string Name { get; set; }
    }
}
