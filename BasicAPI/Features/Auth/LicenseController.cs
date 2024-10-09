using BasicAPI.Features.Infra.Data;
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
            _context.Database.Migrate();
            _context.Licenses.Add(model);
            _context.SaveChanges();
            return model;
            //return 0;
        }



    }
}
