using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECSSR.COMMON.Commands;
using ECSSR.COMMON.Product.Dto;
using ECSSR.UTILITY.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECSSR.UI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPut("Insert")]
        [ProducesResponseType(typeof(EntityResponseModel<ProductReadDto>), 200)]
        public async Task<IActionResult> Insert(CancellationToken cancellationToken,
            ProductCreateDto model)
        {
            var returnResponse = new EntityResponseModel<ProductReadDto>();
            try
            {
                var command = new EntityCreateCommand<ProductCreateDto, EntityResponseModel<ProductReadDto>>(model);
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
        [HttpPut("Update")]
        [ProducesResponseType(typeof(EntityResponseModel<ProductReadDto>), 200)]
        public async Task<IActionResult> Update(CancellationToken cancellationToken,
           ProductUpdateDto model,int id)
        {
            var returnResponse = new EntityResponseModel<ProductReadDto>();
            try
            {
                var command = new EntityUpdateCommand<int,ProductUpdateDto, EntityResponseModel<ProductReadDto>>(id,model);
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

        [HttpPut("Delete")]
        [ProducesResponseType(typeof(EntityResponseModel<ProductReadDto>), 200)]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken,
           ProductUpdateDto model, int id)
        {
            var returnResponse = new EntityResponseModel<ProductReadDto>();
            try
            {
                var command = new EntityUpdateCommand<int, ProductUpdateDto, EntityResponseModel<ProductReadDto>>(id, model);
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
        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(EntityResponseModel<ProductReadDto>), 200)]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken, int id)
        {
            var returnResponse = new EntityResponseModel<ProductReadDto>();
            try
            {
                var command = new EntityDeleteCommand<int, EntityResponseModel<ProductReadDto>>(User, id, "Addresses, AgentContacts, AgentModules, AgentModules.AgentQuotas, AgentDocuments, AgentIpAddresses, AgentCountryOperations,CompanyAgentBranches, SendingTransactions");
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