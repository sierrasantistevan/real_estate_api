using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models;

namespace RealEstate.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("properties")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public PropertiesController(RealEstateContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProperties([FromQuery] PropertyQueryParameters queryParameters)
        {
            IQueryable<Property> properties = _context.Properties;

            if(queryParameters.Category != null)
            {
                properties = properties.Where(p => p.CategoryId == queryParameters.Category.Value);
            }

            if(queryParameters.MinPrice != null)
            {
                properties = properties.Where(p => p.Price >= queryParameters.MinPrice.Value);
            }

            if(queryParameters.MaxPrice != null)
            {
                properties = properties.Where(p => p.Price <= queryParameters.MaxPrice.Value);
            }

            if(!string.IsNullOrEmpty(queryParameters.City))
            {
                properties = properties.Where(p => p.City == queryParameters.City);
            }

             if(!string.IsNullOrEmpty(queryParameters.State))
            {
                properties = properties.Where(p => p.State == queryParameters.State);
            }

            if(!string.IsNullOrEmpty(queryParameters.SortBy)){
                if(typeof(Property).GetProperty(queryParameters.SortBy) != null)
                {
                    properties = properties.OrderByCustom(
                        queryParameters.SortBy,
                        queryParameters.SortOrder
                    );
                }
            }
            properties = properties
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);
            return Ok(await properties.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if(property == null){
                return NotFound();
            }

            return Ok(property);
        }

        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return CreatedAtAction
            (
                "GetProperty",
                new { id = property.Id },
                property
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProperty(int id, Property property)
        {
            if(id != property.Id)
            {
                return BadRequest();
            }

            _context.Entry(property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!_context.Properties.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Property>> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);

            if(property == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return property;
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult<List<Property>>> DeleteMultipleProperties([FromQuery] int[] ids)
        {
            var properties = new List<Property>();
            foreach(var id in ids)
            {
                var property = await _context.Properties.FindAsync(id);
                if(property == null)
                {
                    return NotFound();
                }
                properties.Add(property);
            }

            _context.Properties.RemoveRange(properties);
            await _context.SaveChangesAsync();

            return Ok(properties);
        }
    }
}