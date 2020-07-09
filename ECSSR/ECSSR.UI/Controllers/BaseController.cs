using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECSSR.UI.Controllers
{
    
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
        public IMediator Mediator { get; }
    }
}