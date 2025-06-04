namespace Surveys.Domain.Survey.Extensions
{
    public static class SurveyExtensions
    {
        public static IQueryable<Survey> FilterSurveys(
            this IQueryable<Survey> surveys, string? query, bool isArchieved)
        {
            return surveys.Where(survey =>
                query == null || 
                    survey.Name.ToLower().Contains(query.ToLower())
                    || survey.Description.ToLower().Contains(query.ToLower()))
                .Where(survey => survey.IsArchived == isArchieved);
        }
    }
}
