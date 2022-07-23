using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;


namespace MoviesAPI.Entities
{
    public class Genre : IId
    {
        public int Id { get; set; }   
        public string Name { get; set; }
    }
}
