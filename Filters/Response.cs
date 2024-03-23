namespace FileUpload.API.Filters
{
    public sealed class Response
    {
        public Response()
        {
            IsError = false;
        }

        public int StatusCode { get; set; }
        public string InternalMessage { get; set; }
        public string UserMessage { get; set; }
        public bool IsError { get; set; }
        public object Result { get; set; }

        public void SetError()
        {
            this.SetError(null, null);
        }

        public void SetError(string message)
        {
            this.SetError(message, null);
        }

        public void SetError(string message, string internalMessage)
        {
            IsError = true;
            UserMessage = message;
            InternalMessage = internalMessage;
        }

        public void SetSuccess(string message)
        {
            IsError = false;
            UserMessage = message;
        }
    }
}
