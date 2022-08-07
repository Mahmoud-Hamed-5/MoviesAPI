using Microsoft.AspNetCore.Mvc;
using System;
using MoviesAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.Entities;
using Microsoft.Extensions.Logging;
using MoviesAPI.Filters;
using AutoMapper;
using MoviesAPI.DTOs;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : CustomBaseController
    {
        private readonly ApplicationDBContext context;
        private readonly ILogger<GenresController> logger;
        private readonly IMapper mapper;

        //private readonly IUnitOfWork unitOfWork;

        public GenresController(ApplicationDBContext context, 
            ILogger<GenresController> logger,
            IMapper mapper)
            : base(context, mapper)
        {
            this.context = context;
            // this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        //[ResponseCache(Duration = 60)]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<IActionResult> GetGenres([FromQuery] PaginationDTO paginationDTO)
        {
            logger.LogInformation("Getting all Genres!");

            var genres = await GetAll<Genre, GenreDTO>(paginationDTO);
            return Ok(genres);
        }


        [HttpGet("{id:int}", Name = "getGenre")]
        public async Task<ActionResult<GenreDTO>> GetGenreById(int id)
        {
            var genre = await GetById<Genre, GenreDTO>(id);

            return genre;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenreCreateDTO genreCreateDTO)
        {
            var result = await Create<GenreCreateDTO, Genre, GenreDTO>(genreCreateDTO, "getGenre");

            return result;
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GenreUpdateDTO genreDTO)
        {
            var result = await Update<GenreUpdateDTO, Genre>(id, genreDTO);

            return result;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Delete<Genre>(id);
            return result;
        }


    }
}
