namespace Me.Talabat.APIs.Errors
{
	public class ValidationErrorApiResponse: ApiResponse
	{
        public IEnumerable<string?> Errors { get; set; }

        public ValidationErrorApiResponse(string? message = null):base(400,message)
        {
            
        }
    }
}
