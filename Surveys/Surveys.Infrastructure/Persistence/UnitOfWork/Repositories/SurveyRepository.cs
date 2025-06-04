using Microsoft.EntityFrameworkCore;
using Surveys.Application.UnitOfWork.Exceptions;
using Surveys.Application.UnitOfWork.Repositories;
using Surveys.Domain.Survey;
using Surveys.Domain.Survey.Extensions;

namespace Surveys.Infrastructure.Persistence.UnitOfWork.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly SurveysDbContext _context;

        public SurveyRepository(SurveysDbContext context)
        {
            _context = context;
        }

        // Defining result
        public async Task AddFormInstance(FormInstance formInstance)
        {
            formInstance.Points = formInstance.UserAnswers
                .Select(userAnswer => userAnswer.Answer.Value)
                .Sum();

            formInstance.Result = await _context.Scales
                .FirstOrDefaultAsync(scale =>
                    scale.From >= formInstance.Points
                    && scale.To < formInstance.Points);

            _context.Add(formInstance);
        }

        public void AddSurvey(Survey survey)
        {
            var scales = survey.Scales.ToList();

            scales = scales.OrderBy(scale => scale.From).ToList();

            for (int i = 1; i < scales.Count; i++)
            {
                var prev = scales[i - 1];
                var curr = scales[i];

                // Validate that scale don't have spaces and that one scale not continains values of other
                if (prev.To - curr.From != 0)
                {
                    throw new InvalidScale();
                }
            }

            _context.Add(survey);
        }

        public async Task<FormInstance?> GetFormInstanceByIdAsync(Guid id)
        {
            return await _context.FormInstances
                
                .Include(formInstance => formInstance.UserAnswers)
                .ThenInclude(userAnswer => userAnswer.Answer)

                .FirstOrDefaultAsync(formInstance => formInstance.Id == id);
        }

        public async Task<Survey?> GetSurveyByIdAsync(Guid id)
        {
            return await _context.Surveys

                .Include(survey => survey.Questions)
                .ThenInclude(question => question.Answers)

                .FirstOrDefaultAsync(survey => survey.Id == id);
        }

        public async Task<List<FormInstance>> ListFormInstancesOfUser(Guid userId)
        {
            return await _context.FormInstances
                .Where(formInstance => formInstance.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Survey>> ListSurveys(
            int pageSize, int pageNum, string? query, bool isArhieved)
        {
            return await _context.Surveys
                .FilterSurveys(query, isArhieved)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void RemoveSurvey(Survey survey)
        {
            _context.Surveys.Remove(survey);
        }

        // TODO: Divide survey and form instance repositories
    }
}
