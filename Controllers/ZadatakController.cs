using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Constants;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZadatakController : ControllerBase
    {

        //private static List<Zadatak> zadaci = new List<Zadatak>
        //{

        //        new Zadatak
        //        {
        //            TenantId=Guid.NewGuid(),
        //            Context="fadfdsa",
        //            Content=new Dictionary<string, string>
        //            {
        //                {ContentKeys.ApiKey, "asdasdasd" },
        //                {ContentKeys.ApiSecret, "basdn" },
        //                {ContentKeys.BaseUrl, "https.sadasd" }
        //            }
        //        }

        //};
        private readonly DataContext _context;

        public ZadatakController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mylist = await _context.Zadaci.ToListAsync();
            return Ok(mylist);
        }
        
        [HttpGet("/id")]
        public async Task<IActionResult> GetById([FromQuery] Guid tenantId, [FromQuery] string context)
        {
            var zadatak = await _context.Zadaci.Where(x => x.TenantId==tenantId && x.Context==context).FirstOrDefaultAsync();

            if (zadatak == null)
            {
                return NotFound("Zadatak not found.");
            }
            
            return Ok(zadatak); 
        }
        [HttpPost]
        public async Task<IActionResult> AddZadatak([FromBody] ZadatakCreateRequest request)
        {
            if (!ModelState.IsValid) // ako korisnik ne upiše potrebne parametre
            { 
                return BadRequest("Invalid model supplied");
            }
            var zadatak = new Zadatak
            {
                TenantId = request.TenantId,
                Context = request.Context,
                Content=request.Content
            };
            _context.Zadaci.Add(zadatak);
            await _context.SaveChangesAsync();
            return Ok(await _context.Zadaci.ToListAsync());
           
            
        }
        [HttpPut]
        public async Task<IActionResult> UpdateZadatak(Zadatak request)
        {
            var dbzad = await _context.Zadaci.FindAsync(request.TenantId);
            if(dbzad == null)
                return BadRequest("Zadatak not found.");
            dbzad.Context = request.Context;
            dbzad.Content = request.Content;
            
            await _context.SaveChangesAsync();

            return Ok(await _context.Zadaci.ToListAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteZadatak(Guid tenantId)
        {
            var zadatak = await _context.Zadaci.FindAsync(tenantId);

            if (zadatak == null)
            {
                return NotFound();
            }
            _context.Zadaci.Remove(zadatak);
            await _context.SaveChangesAsync();
            return Ok(await _context.Zadaci.ToListAsync());
        }
    }
}
