using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            var thing = context.Products.Find(42);
            if (thing == null) return NotFound(new ApiErrorResponse(404));
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = context.Products.Find(42);
            var thingtoreturn = thing.ToString();
            return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetSBadRequest()
        {
            
            return BadRequest(new ApiErrorResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetSBadRequest(int id)
        {

            return BadRequest();
        }

    }
}
