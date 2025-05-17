

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
    public class UserController : BaseApiController
    {
         private readonly IMediator _mediator;



     
 public UserController(IMediator mediator)
        {
           _mediator = mediator;
        }




       
       [HttpGet]
        [ProducesResponseType(200, Type = typeof(MyAppResponse<List<GetAllUserDto>>))]
        public async Task<IActionResult> GetAll(string searchValue ="", string orderBy ="", bool orderAscendingDirection=true)
        {

            var result = await _mediator.Send(new GetAllUserQuery()
            {
                Search = searchValue,
                Sort = orderBy,
                AscendingOrder = orderAscendingDirection

            }
            );

               if (result != null)
            {
                if (result.Succeeded)
                {

                    return Ok(result.Data);
                }
                else
                {
                    if (result.Errors != null && result.Errors.Any())
                    {
                        return BadRequest(result.Errors);
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }

                }
            }
            else
            {
                return BadRequest(new MyAppResponse<int>(SD.ErrorOccurred));
            }
        }


        [HttpGet("{id:Guid}", Name = "GetByIdUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyAppResponse<GetByIdUserDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetByIdUserQuery() { Id = id });

                if (result != null)
            {
                if (result.Succeeded)
                {

                    return Ok(result.Data);
                }
                else
                {
                    if (result.Errors != null && result.Errors.Any())
                    {
                        return BadRequest(result.Errors);
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }

                }
            }
            else
            {
                return BadRequest(new MyAppResponse<int>(SD.ErrorOccurred));
            }
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateUserCommand model)
        {
            if (model == null)
            {
                return BadRequest(new MyAppResponse<GetByIdUserDto>("Invalid object"));
            }

            var result = await _mediator.Send(model);


             if (result != null)
            {
                if (result.Succeeded)
                {

                    return Ok(result.Data);
                }
                else
                {
                    if (result.Errors != null && result.Errors.Any())
                    {
                        return BadRequest(result.Errors);
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }

                }
            }
            else
            {
                return BadRequest(new MyAppResponse<int>(SD.ErrorOccurred));
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateUserCommand model)
        {

            if (model == null)
            {
                return BadRequest(new MyAppResponse<bool>("Invalid object"));
            }

            var result = await _mediator.Send(model);


              if (result != null)
            {
                if (result.Succeeded)
                {

                    return Ok(result.Data);
                }
                else
                {
                    if (result.Errors != null && result.Errors.Any())
                    {
                        return BadRequest(result.Errors);
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }

                }
            }
            else
            {
                return BadRequest(new MyAppResponse<int>(SD.ErrorOccurred));
            }
        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetUserPassword(SetUserPasswordCommand model)
        {

            if (model == null)
            {
                return BadRequest(new MyAppResponse<bool>("Invalid object"));
            }

            var result = await _mediator.Send(model);


              if (result != null)
            {
                if (result.Succeeded)
                {

                    return Ok(result.Data);
                }
                else
                {
                    if (result.Errors != null && result.Errors.Any())
                    {
                        return BadRequest(result.Errors);
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }

                }
            }
            else
            {
                return BadRequest(new MyAppResponse<bool>(SD.ErrorOccurred));
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = id });

              if (result != null)
            {
                if (result.Succeeded)
                {

                    return Ok(result.Data);
                }
                else
                {
                    if (result.Errors != null && result.Errors.Any())
                    {
                        return BadRequest(result.Errors);
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }

                }
            }
            else
            {
                return BadRequest(new MyAppResponse<bool>(SD.ErrorOccurred));
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand model)
        {
            if (model == null)
            {
                return BadRequest(new MyAppResponse<bool>("Invalid object"));
            }

            var result = await _mediator.Send(model);


              if (result != null)
            {
                if (result.Succeeded)
                {

                    return Ok(result.Data);
                }
                else
                {
                    if (result.Errors != null && result.Errors.Any())
                    {
                        return BadRequest(result.Errors);
                    }
                    else
                    {
                        return BadRequest(result.Message);
                    }

                }
            }
            else
            {
                return BadRequest(new MyAppResponse<bool>(SD.ErrorOccurred));
            }
        }
       
            
#region Custom
#endregion Custom

}
}
