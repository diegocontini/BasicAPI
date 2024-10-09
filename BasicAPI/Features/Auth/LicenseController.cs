using BasicAPI.Features.Infra.Data;
using BasicAPI.Features.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicAPI.Features.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class LicenseController : ControllerBase
    {

        private readonly DataContext _context;

        public LicenseController(DataContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "CreateLicense")]
        public License Post([FromBody]License model)
        {

            _context.Licenses.Add(model);
            _context.SaveChanges();
            return model;
            //return 0;
        }

        [HttpGet]
        public PaginatedResult<License> GetAll()
        {
            var res = new PaginatedResult<License>();
            
            res.Data = _context.Licenses;
            return res;
        }



    }
}
