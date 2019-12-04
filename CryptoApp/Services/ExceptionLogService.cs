using CryptoApp.Data;
using CryptoApp.Entities;
using CryptoApp.Models;


namespace CryptoApp.Services
{
    public class ExceptionLogService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ExceptionLogService(ApplicationDbContext demoContext)
        {
            _applicationDbContext = demoContext;
        }
        public void CreateExceptionRecord(CreateExceptionLogCommand cmd)
        {
            var exceptionLogRecord = new ExceptionLog
            {
                LogTime = cmd.LogTime,
                HttpStatusCode = cmd.HttpStatusCode,
                RequestId = cmd.RequestId,
                ExceptionMessage = cmd.ExceptionMessage,
                ExceptionStackTrace = cmd.ExceptionStackTrace
            };

            _applicationDbContext.Add(exceptionLogRecord);
            _applicationDbContext.SaveChanges();
        }
    }
}
