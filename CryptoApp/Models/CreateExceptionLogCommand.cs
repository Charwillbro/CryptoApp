using System;

namespace CryptoApp.Models
{
    public class CreateExceptionLogCommand
    {
        public DateTime LogTime { get; set; }
        public int HttpStatusCode { get; set; }
        public Guid RequestId { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
