using BlindDate.Api.Data;
using BlindDate.Api.Data.Models;
using BlindDate.Api.Helpers;
using BlindDate.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlindDate.Api.Controllers
{
    public class ValuesController : ApiController
    {
        //[Dependency]
        //public ApplicationDbContext DbContext { get; set; }
        ApplicationDbContext DbContext = new ApplicationDbContext();

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public async Task<ResponseBase> RegisterUser([FromBody]RegistrationRequest registrationRequest)
        {
            var res = new ResponseBase() { Success = false };
            try
            {
                var profile = DbContext.ApplicationUserProfiles.Add(new ApplicationUserProfile()
                {
                    DisplayUsername = "Anonymous",
                    Id = SequentialGuidHelper.CreateSequentialGuid().ToString(),
                    DateCreated = DateTimeOffset.Now,
                    UserType = ProfileType.Trial
                });

                await DbContext.SaveChangesAsync();

                res.Success = true;
                res.Message = $"User created with id: {profile.Id}";
            }
            catch (Exception)
            {

                throw;
            }
            return res;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
