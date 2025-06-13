using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using eStoreCA.API.Infrastructure;
using eStoreCA.Application.Features.Country.Queries.GetAll;
using eStoreCA.Application.Features.Country.Queries.GetAllByPage;
using eStoreCA.Application.Features.Country.Queries.GetById;
using eStoreCA.Application.Features.Country.Commands.Create;
using eStoreCA.Application.Features.Country.Commands.Update;
using eStoreCA.Application.Features.Country.Commands.Delete;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos.Country;
using Microsoft.AspNetCore.Authorization;

namespace eStoreCA.API.Controllers
{
    [ApiVersion("1.0")]
    public class CountryController : BaseApiController
    {
        public CountryController() { }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(MyAppResponse<List<GetCountryDto>>))]
        public async Task<IActionResult> GetAll(string searchValue = "", string orderBy = "", bool orderAscendingDirection = true)
        {
            try
            {
                bool byPassCache = true;
                if (string.IsNullOrEmpty(searchValue))
                {
                    byPassCache = false;
                }
                var result = await _Mediator.Send(new GetAllCountryQuery()
                {
                    Search = searchValue,
                    SortColumnName = orderBy,
                    AscendingOrder = orderAscendingDirection,
                    BypassCache = byPassCache
                });
                return ActionResult(result);
            }
            catch (Exception ex)
            {
                return ActionResult<string>(null, ex);
            }
        }

        [AllowAnonymous]
        [HttpGet("paged")]
        [ProducesResponseType(200, Type = typeof(MyAppResponse<PagedResult<GetAllByPageCountryDto>>))]
        public async Task<IActionResult> GetAllPagedList(string searchValue = "", string orderBy = "", bool orderAscendingDirection = true, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var result = await _Mediator.Send(new GetAllByPageCountryQuery()
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

        [HttpGet("{id:Guid}", Name = "GetByIdCountry")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyAppResponse<GetCountryDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _Mediator.Send(new GetByIdCountryQuery() { Id = id });
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
        public async Task<IActionResult> Create(CreateCountryDto model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new MyAppResponse<GetCountryDto>("Invalid object"));
                }
                var dtoValidator = new CreateCountryDtoValidator();
                var validationResult = dtoValidator.Validate(model);
                if (validationResult != null && validationResult.IsValid == false)
                {
                    return ReturnActionResult("", false, validationResult.Errors.Select(modelError => modelError.ErrorMessage).ToList(), "", "");
                }
                var request = _Mapper.Map<CreateCountryCommand>(model);
                var result = await _Mediator.Send(request);
                // Optionally publish event here
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
        public async Task<IActionResult> Update(UpdateCountryDto model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new MyAppResponse<bool>("Invalid object"));
                }
                var dtoValidator = new UpdateCountryDtoValidator();
                var validationResult = dtoValidator.Validate(model);
                if (validationResult != null && validationResult.IsValid == false)
                {
                    return ReturnActionResult("", false, validationResult.Errors.Select(modelError => modelError.ErrorMessage).ToList(), "", "");
                }
                var request = _Mapper.Map<UpdateCountryCommand>(model);
                var result = await _Mediator.Send(request);
                // Optionally publish event here
                return ActionResult(result);
            }
            catch (Exception ex)
            {
                return ActionResult<string>(null, ex);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteCountryCommand model)
        {
            try
            {
                var result = await _Mediator.Send(model);
                // Optionally publish event here
                return ActionResult(result);
            }
            catch (Exception ex)
            {
                return ActionResult<string>(null, ex);
            }
        }
    }
}