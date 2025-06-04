using MediatR;
using Microsoft.AspNetCore.Mvc;
using Surveys.Application.Features.DTOs.Surveys;
using Surveys.Application.Features.Exceptions;
using Surveys.Application.Features.Surveys.Commands.CreateSurvey;
using Surveys.Application.Features.Surveys.Commands.RemoveSurvey;
using Surveys.Application.Features.Surveys.Queries.GetSurveyById;
using Surveys.Application.Features.Surveys.Queries.ListSurveys;
using Surveys.Application.UnitOfWork;
using Surveys.Application.UnitOfWork.Exceptions;

namespace Surveys.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _sender;

        public SurveysController(IUnitOfWork unitOfWork, ISender sender)
        {
            _unitOfWork = unitOfWork;
            _sender = sender;
        }

        [HttpGet("helloworld")]
        public async Task<ActionResult<string>> HelloWorld()
        {
            return await Task.FromResult("hello world 1");
        }

        [HttpGet]
        public async Task<ActionResult<List<SurveyForPreviewDto>>> ListSurveys(
            string? query, bool isArhieved = false, int pageNum = 1, int pageSize = 10)
        {
            var request = new ListSurveysQuery(
                query, isArhieved, pageNum, pageSize);

            var surveys = await _sender.Send(request);

            return Ok(surveys);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyDto>> GetSurveyById(Guid id)
        {
            try
            {
                var query = new GetSurveyByIdQuery(id);

                var survey = await _sender.Send(query);

                return Ok(survey);
            }
            catch (SurveyNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSurvey(
            SurveyForCreateDto surveyForCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var command = new CreateSurveyCommand(surveyForCreateDto);

                var createdSurveyId = await _sender
                    .Send(command, cancellationToken);
                return Ok(createdSurveyId);
            }
            catch (InvalidScale ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveSurvey(
            Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new RemoveSurveyCommand(id);

                await _sender.Send(command, cancellationToken);

                return NoContent();
            }
            catch (SurveyNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        // TODO: Write form intances contoller
    }
}
