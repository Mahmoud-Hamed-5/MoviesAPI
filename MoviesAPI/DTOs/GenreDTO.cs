

using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{

    public class GenreCreateDTO
    {
        [Required]
        [StringLength(10)]
        [FirstLetterUpperCase]
        public string Name { get; set; }
    }

    public class GenreUpdateDTO : GenreCreateDTO
    {
    }


    public class GenreDTO : GenreCreateDTO
    {
        public int Id { get; set; }

    }
}
