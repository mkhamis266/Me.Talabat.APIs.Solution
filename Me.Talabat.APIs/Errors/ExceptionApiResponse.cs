namespace Me.Talabat.APIs.Errors
{
	public class ExceptionApiResponse : ApiResponse
	{
        public string? Detailes { get; set; }

        public ExceptionApiResponse(string? message = null, string? detailes = null):base(500, message)
        {
            Detailes = detailes;
        }
    }
}
