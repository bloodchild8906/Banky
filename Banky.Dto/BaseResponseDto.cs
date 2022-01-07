namespace Banky.Dto
{
    public abstract class BaseResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
