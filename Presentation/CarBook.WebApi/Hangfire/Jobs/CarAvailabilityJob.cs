using CarBook.WebApi.Hangfire.Managers;
using Hangfire;

namespace CarBook.WebApi.Hangfire.Jobs
{
    public static class CarAvailabilityJob
    {
        public static void UpdateCarAvailability()
        {
            RecurringJob.RemoveIfExists(nameof(CarAvailabilityJobManager));
            RecurringJob.AddOrUpdate<CarAvailabilityJobManager>(
                nameof(CarAvailabilityJobManager),
                job => job.Process(),
                "20 16 * * *",
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local,
                }
            );
        }
    }
}
