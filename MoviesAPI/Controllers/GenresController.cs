using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MoviesAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.Entities;
using Microsoft.Extensions.Logging;
using MoviesAPI.Filters;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly ILogger<GenresController> logger;

        public GenresController(IRepository repository, ILogger<GenresController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        //[ResponseCache(Duration = 60)]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("Getting all Genres!");
            var genres = await repository.GetAllGenres();
            return Ok(genres);
        }

        [HttpGet("{id:int}", Name = "getGenre")]
        public IActionResult Get(int id)
        {
            var genre = repository.GetGenreById(id);
            if (genre == null)
            {
                return NotFound($"Ther is No Genre with the Id:{id}");
            }
            return Ok(genre);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Genre genre)
        {           
            return new CreatedAtRouteResult("getGenre",new {id = genre.Id }, genre);
        }

        [HttpPut]
        public void Put()
        {

        }

        [HttpDelete]
        public void Delete()
        {

        }


    }
}
