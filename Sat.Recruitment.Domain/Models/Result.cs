namespace Sat.Recruitment.Domain.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ResultValue { get; set; }
        public string Errors { get; set; }
    }
}
