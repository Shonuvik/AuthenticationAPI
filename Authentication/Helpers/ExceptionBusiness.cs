namespace Authentication.Helpers
{
    public class ExceptionBusiness : Exception
	{
		public ExceptionBusiness(string message) : base(message) { }

		public ExceptionBusiness(string message, Exception innerException) : base(message, innerException) { }
	}
}

