namespace UPBank.Address.Domain.DTOs
{
    public class ResponseDTO
    {
        public IEnumerable<string> Errors { get; set; }

        public void ErrorsResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}