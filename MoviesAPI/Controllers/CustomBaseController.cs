using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDBContext context;

        public CustomBaseController(ApplicationDBContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        protected async Task<List<TDTO>> GetAll<TEntity, TDTO>() where TEntity : class
        {
            var entities = await context.Set<TEntity>().AsNoTracking().ToListAsync();

              var results = mapper.Map<List<TDTO>>(entities);
            return results;
        }


        protected async Task<ActionResult<TDTO>> GetById<TEntity, TDTO>(int id) where TEntity : class , IId 
        {
            var entity = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return NotFound($"Ther is No {typeof(TEntity).Name} with the Id:{id}");
            }
            var result = mapper.Map<TDTO>(entity);
            return result;
        }


        protected async Task<ActionResult> Create<TCreate, TEntity, TRead>(TCreate createDTO, string routeName) 
            where TEntity : class, IId
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = mapper.Map<TEntity>(createDTO);
            context.Add(entity);
            await context.SaveChangesAsync();
            var readDTO = mapper.Map<TRead>(entity);

            return new CreatedAtRouteResult(routeName, new { id = entity.Id }, readDTO);
        }


        protected async Task<ActionResult> Update<TUpdate, TEntity>(int id, TUpdate updateDTO)
            where TEntity : class, IId
        {        
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            var entity = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return NotFound($"Ther is No {typeof(TEntity).Name} with the Id:{id}");
            }

            mapper.Map(updateDTO, entity);
            context.Entry(entity).State = EntityState.Modified;
            
            await context.SaveChangesAsync();

            return NoContent();
        }


        protected async Task<ActionResult> Delete<TEntity>(int id) where TEntity : class, IId
        {
            if (id < 1)
            {
                return BadRequest(ModelState);
            }

            var entity = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return NotFound($"Ther is No {typeof(TEntity).Name} with the Id:{id}");
            }

            context.Remove(entity);
            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
