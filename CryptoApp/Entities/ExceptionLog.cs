using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApp.Entities
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public int HttpStatusCode { get; set; }
        public Guid RequestId { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
