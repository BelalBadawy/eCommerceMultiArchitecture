
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using eStoreCA.API.Infrastructure;
using eStoreCA.Application.Features.Commands;
using eStoreCA.Application.Features.Queries;
using eStoreCA.Domain.Events;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;






using System.Globalization;

namespace eStoreCA.API.Controllers
{
    [ApiVersion("1.0")]
    public class CategoryController : BaseApiController
    {
       


 

        public CategoryController()
        {
          
        }


       

        [HttpGet]
[ProducesResponseType(200, Type = typeof(MyAppResponse<List<GetAllCategoryDto>>))]
public async Task<IActionResult> GetAll(string searchValue = "", string orderBy = "", bool orderAscendingDirection = true)
{
    try
    {
        bool byPassCache = true;

        if (string.IsNullOrEmpty(searchValue))
        {
            byPassCache = false;
        }

        var result = await _Mediator.Send(new GetAllCategoryQuery()
        {
            Search = searchValue,
            SortColumnName = orderBy,
            AscendingOrder = orderAscendingDirection,
            BypassCache = byPassCache
        }
        );

        return ActionResult(result);
    }
    catch (Exception ex)
    {
       return ActionResult<string>(null, ex);
    }
}

[HttpGet]
[ProducesResponseType(200, Type = typeof(MyAppResponse<PagedResult<GetAllByPageCategoryDto>>))]
public async Task<IActionResult> GetAllPagedList(string searchValue = "", string orderBy = "", bool orderAscendingDirection = true, int pageIndex = 1, int pageSize = 10)
{
    try
    {
        var result = await _Mediator.Send(new GetAllByPageCategoryQuery()
        {
            Search = searchValue,
            SortColumnName = orderBy,
            AscendingOrder = orderAscendingDirection,
            PageIndex = pageIndex,
            PageSize = pageSize
        });

        return ActionResult(result);
    }
    catch (Exception ex)
    {
       return ActionResult<string>(null, ex);
    }
}

[HttpGet("{id:Guid}", Name = "GetByIdCategory")]
[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyAppResponse<GetByIdCategoryDto>))]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesDefaultResponseType]
public async Task<IActionResult> GetById(Guid id)
{
    try
    {

   
var result = await _Mediator.Send(new GetByIdCategoryQuery() { Id  = id});


        

        return ActionResult(result);
    }
    catch (Exception ex)
    {
       return ActionResult<string>(null, ex);
    }
}


[HttpPost]
[ProducesResponseType(201, Type = typeof(Guid))]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<IActionResult> Create(CreateCategoryDto model)
{
    try
    {
        if (model == null)
        {
            return BadRequest(new MyAppResponse<GetByIdCategoryDto>("Invalid object"));
        }

        var dtoValidator = new CreateCategoryDtoValidator();

        var validationResult = dtoValidator.Validate(model);

        if (validationResult != null && validationResult.IsValid == false)
        {
            return ReturnActionResult("", false, validationResult.Errors.Select(modelError => modelError.ErrorMessage).ToList(), "", "");
        }

        var request = _Mapper.Map<CreateCategoryCommand>(model);


   


        


        var result = await _Mediator.Send(request);

        if (result.Succeeded)
        {
            // Fire-and-forget for the notification publishing
            _ = Task.Run(() => _Mediator.Publish(new CategoryCreatedEvent(model)));
        }

        return ActionResult(result);
    }
    catch (Exception ex)
    {
       return ActionResult<string>(null, ex);
    }
}

[HttpPut]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<IActionResult> Update(UpdateCategoryDto model)
{
    try
    {
        if (model == null)
        {
            return BadRequest(new MyAppResponse<bool>("Invalid object"));
        }
        var dtoValidator = new UpdateCategoryDtoValidator();

        var validationResult = dtoValidator.Validate(model);

        if (validationResult != null && validationResult.IsValid == false)
        {
            return ReturnActionResult("", false, validationResult.Errors.Select(modelError => modelError.ErrorMessage).ToList(), "", "");
        }

        var request = _Mapper.Map<UpdateCategoryCommand>(model);

       



        var result = await _Mediator.Send(request);

        if (result.Succeeded)
        {
            // Fire-and-forget for the notification publishing
            _ = Task.Run(() => _Mediator.Publish(new CategoryUpdatedEvent(model)));
        }

        return ActionResult(result);
    }
    catch (Exception ex)
    {
       return ActionResult<string>(null, ex);
    }
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<IActionResult> Delete(DeleteCategoryDto model)
{
    try
    {
   
var result = await _Mediator.Send(new DeleteCategoryCommand() { Id = model.Id});


        

        if (result.Succeeded)
        {
            // Fire-and-forget for the notification publishing
            _ = Task.Run(() => _Mediator.Publish(new CategoryDeletedEvent(model)));
        }

        return ActionResult(result);
    }
    catch (Exception ex)
    {
      return ActionResult<string>(null, ex);
    }
}
 

#region Custom
#endregion Custom

	}
}
