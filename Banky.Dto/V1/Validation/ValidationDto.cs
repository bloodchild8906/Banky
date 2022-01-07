using System.Collections.Generic;

namespace Banky.Dto.V1.Validation
{
    public class ValidationDto:BaseResponseDto
    {
        public List<Error> Errors { get; set; } = new List<Error>();
    }
    public class Error
    {
        public string Message { get; set; }
    }
}
