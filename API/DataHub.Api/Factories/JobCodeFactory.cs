using DataHub.Domain.Entities;

namespace DataHub.Api.Factories
{
    public class JobCodeFactory
    {
        public Dtos.JobCode CreateJobCode(JobCode jobCode)
        {
            decimal? minimum = null;
            decimal tryMinimum;

            if (decimal.TryParse(jobCode.Minimum, out tryMinimum))
            {
                minimum = tryMinimum;
            }

            decimal? maximum = null;
            decimal tryMaximum;

            if (decimal.TryParse(jobCode.Maximum, out tryMaximum))
            {
                maximum = tryMaximum;
            }

            var dtoJobCode = new Dtos.JobCode
            {
                Id = (int)jobCode.Id,
                Href = null,
                WorkCode = jobCode.WorkCode,
                Title = jobCode.Title,
                Band = jobCode.Band,
                Minimum = minimum,
                Maximum = maximum
            };

            return dtoJobCode;
        }
    }
}
