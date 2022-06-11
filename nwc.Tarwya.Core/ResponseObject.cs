namespace nwc.Tarwya.Infra.Core
{
	public class Response<T>
	{
		public bool IsSucess { get; set; }
		public string ErrorCode { get; set; }
		public string Error { get; set; }
		public T Data { get; set; }
		public Response()
		{

		}
		public Response(string errorCode, string ErrorMessage)
		{
			this.IsSucess = false;
			this.ErrorCode = string.IsNullOrEmpty(errorCode) ? "ERROR" : errorCode;
			this.Error = ErrorMessage;
		}
		public Response(T obj)
		{
			this.IsSucess = true;
			this.Data = obj;
		}
		public bool IsValied()
		{
			return (Data != null && Data.Equals(default(T)));
		}
	}
}
