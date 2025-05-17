

using Asp.Versioning;
using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eStoreCA.API.Infrastructure;
using eStoreCA.Application.Features.Commands;
using eStoreCA.Application.Features.Queries;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;




namespace eStoreCA.API.Controllers
{
     [ApiVersion("1.0")]
    public class RoleController : BaseApiController
    {
         private readonly IMediator _mediator;



     
 public RoleController(IMediator mediator)
        {
           _mediator = mediator;
        }




       
       [HttpGet]
        [ProducesResponseType(200, Type = typeof(MyAppResponse<List<GetAllRoleDto>>))]
        public async Task<IActionResult> GetAll(string searchValue ="", string orderBy ="", bool orderAscendingDirection=true)
        {

            var response = await _mediator.Send(new GetAllRoleQuery()
            {
                Search = searchValue,
                Sort = orderBy,
                AscendingOrder = orderAscendingDirection

            }
            );

             return ActionResult(response);

     //      if (response != null)
     //      {
     //          if (response.Succeeded)
     //          {
					//return Ok(response.Data);
     //          }
     //          else
     //          {
     //              if (response.Errors != null && response.Errors.Any())
     //              {
     //                  return BadRequest(response.Errors);
     //              }
     //              else
     //              {
     //                  return BadRequest(response.Message);
     //              }
     //          }
     //      }
     //      else
     //      {
     //         return BadRequest(SD.ErrorOccurred);
     //      }


        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(MyAppResponse<List<GetAllRoleWithoutClaimsDto>>))]
        public async Task<IActionResult> GetAllWithoutClaims()
        {
            var response = await _mediator.Send(new GetAllRoleWithoutClaimsQuery());

             return ActionResult(response);

     //      if (response != null)
     //      {
     //          if (response.Succeeded)
     //          {
					//return Ok(response.Data);
     //          }
     //          else
     //          {
     //              if (response.Errors != null && response.Errors.Any())
     //              {
     //                  return BadRequest(response.Errors);
     //              }
     //              else
     //              {
     //                  return BadRequest(response.Message);
     //              }
     //          }
     //      }
     //      else
     //      {
     //         return BadRequest(SD.ErrorOccurred);
     //      }

        }


        [HttpGet("{id:Guid}", Name = "GetByIdRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyAppResponse<GetByIdRoleDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetByIdRoleQuery() { Id = id });

              return ActionResult(response);

     //       if (response != null)
     //      {
     //          if (response.Succeeded)
     //          {
					//return Ok(response.Data);
     //          }
     //          else
     //          {
     //              if (response.Errors != null && response.Errors.Any())
     //              {
     //                  return BadRequest(response.Errors);
     //              }
     //              else
     //              {
     //                  return BadRequest(response.Message);
     //              }
     //          }
     //      }
     //      else
     //      {
     //         return BadRequest(SD.ErrorOccurred);
     //      }

        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateRoleCommand model)
        {
            if (model == null)
            {
                return BadRequest(new MyAppResponse<GetByIdRoleDto>("Invalid object"));
            }

            var response = await _mediator.Send(model);

             return ActionResult(response);


     //      if (response != null)
     //      {
     //          if (response.Succeeded)
     //          {
					//return Ok(response.Data);
     //          }
     //          else
     //          {
     //              if (response.Errors != null && response.Errors.Any())
     //              {
     //                  return BadRequest(response.Errors);
     //              }
     //              else
     //              {
     //                  return BadRequest(response.Message);
     //              }
     //          }
     //      }
     //      else
     //      {
     //         return BadRequest(SD.ErrorOccurred);
     //      }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateRoleCommand model)
        {

            if (model == null)
            {
                return BadRequest(new MyAppResponse<bool>("Invalid object"));
            }

            var response = await _mediator.Send(model);


              return ActionResult(response);

     //     if (response != null)
     //      {
     //          if (response.Succeeded)
     //          {
					//return Ok(response.Data);
     //          }
     //          else
     //          {
     //              if (response.Errors != null && response.Errors.Any())
     //              {
     //                  return BadRequest(response.Errors);
     //              }
     //              else
     //              {
     //                  return BadRequest(response.Message);
     //              }
     //          }
     //      }
     //      else
     //      {
     //         return BadRequest(SD.ErrorOccurred);
     //      }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteRoleCommand() { Id = id });

             return ActionResult(response);

     //       if (response != null)
     //      {
     //          if (response.Succeeded)
     //          {
					//return Ok(response.Data);
     //          }
     //          else
     //          {
     //              if (response.Errors != null && response.Errors.Any())
     //              {
     //                  return BadRequest(response.Errors);
     //              }
     //              else
     //              {
     //                  return BadRequest(response.Message);
     //              }
     //          }
     //      }
     //      else
     //      {
     //         return BadRequest(SD.ErrorOccurred);
     //      }


        }

       
            
#region Custom
#endregion Custom

}
}
