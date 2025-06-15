using Microsoft.EntityFrameworkCore;
using Surveys.Application.UnitOfWork.Repositories;
using Surveys.Domain.Survey;

namespace Surveys.Infrastructure.Persistence.UnitOfWork.Repositories
{
    public class FormInstanceRepository : IFormInstanceRepository
    {
        private readonly SurveysDbContext _context;

        public FormInstanceRepository(SurveysDbContext context)
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

        public async Task<FormInstance?> GetFormInstanceByIdAsync(Guid id)
        {
            return await _context.FormInstances

                .Include(formInstance => formInstance.UserAnswers)
                .ThenInclude(userAnswer => userAnswer.Answer)

                .FirstOrDefaultAsync(formInstance => formInstance.Id == id);
        }

        public async Task<List<FormInstance>> ListFormInstancesOfUser(Guid userId)
        {
            return await _context.FormInstances
                .Where(formInstance => formInstance.UserId == userId)
                .ToListAsync();
        }
    }
}
