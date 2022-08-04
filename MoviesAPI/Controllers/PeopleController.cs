using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : CustomBaseController
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly ILogger<PeopleController> logger;
        private readonly string containerName = "People";

        public PeopleController(ApplicationDBContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<PeopleController> logger) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await GetAll<Person, PersonDTO>();

            return Ok(people);
        }

        [HttpGet("{id:int}", Name = "getPerson")]
        public async Task<ActionResult<PersonDTO>> GetPersonById(int id)
        {
            var person = await GetById<Person, PersonDTO>(id);
            return person;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PersonCreateDTO personCreateDTO)
        {         

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = mapper.Map<Person>(personCreateDTO);

            if (personCreateDTO.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await personCreateDTO.Picture.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(personCreateDTO.Picture.FileName);
                    var contentType = personCreateDTO.Picture.ContentType;

                person.Picture = await fileStorageService.SaveFile(content, extension, containerName, contentType);
                }
            }

            context.Add(person);
            await context.SaveChangesAsync();
            var personDTO = mapper.Map<PersonDTO>(person);

            return new CreatedAtRouteResult("getPerson", new { id = person.Id }, personDTO);          
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] PersonUpdateDTO personUpdateDTO)
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return NotFound($"Ther is No Person with the Id:{id}");
            }

            mapper.Map(personUpdateDTO, person);

            if (personUpdateDTO.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await personUpdateDTO.Picture.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(personUpdateDTO.Picture.FileName);
                    var contentType = personUpdateDTO.Picture.ContentType;

                    person.Picture = await fileStorageService.EditFile(content, extension, containerName, person.Picture, contentType);
                }
            }

            //context.Entry(entity).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var result = await Delete<Person>(id);

            //fileStorageService.DeleteFile()

            //return result;
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            var person = await context.People.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return NotFound($"Ther is No Person with the Id:{id}");
            }

            context.Remove(person);
            await fileStorageService.DeleteFile(person.Picture, containerName);
            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
