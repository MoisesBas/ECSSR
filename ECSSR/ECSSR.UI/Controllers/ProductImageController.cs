using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECSSR.COMMON.Commands;
using ECSSR.COMMON.Product.Dto;
using ECSSR.COMMON.ProductImage.Dto;
using ECSSR.UTILITY.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECSSR.UI.Controllers
{
    [Route("api/productimage")]
    [ApiController]
    public class ProductImageController : BaseController
    {
        public ProductImageController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost("Insert")]
        [ProducesResponseType(typeof(EntityResponseModel<ProductImageReadDto>), 200)]
        public async Task<IActionResult> Insert(CancellationToken cancellationToken,
           [FromForm] ProductImageCreateDto model)
        {
            var returnResponse = new EntityResponseModel<ProductImageReadDto>();
            try
            {
                var command = new EntityCreateCommand<ProductImageCreateDto, EntityResponseModel<ProductImageReadDto>>(model);
                var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
                if (result.ReturnStatus == false)
                    return BadRequest(result);
                return Ok(result);

            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }

        }
    }
}