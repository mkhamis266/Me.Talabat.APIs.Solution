namespace Me.Talabat.APIs.Errors
{
	public class ValidationErrorApiResponse: ApiResponse
	{
        public IEnumerable<string?> Errors { get; set; }

        public ValidationErrorApiResponse():base(400)
        {
            
        }
    }
}
