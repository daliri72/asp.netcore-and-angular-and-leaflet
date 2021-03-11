using System.Collections.Generic;

namespace LocationMap.DomainClasses
{
    public enum ErrorHandling
    {
        userpaswrong,
        add,
        edit,
        server,
        upload,
        validation,
        success,
        duplicate,
        nofile,
        successEdite,
        notfound,
        nullValue,
        duplicateCodeMeli,
        sendMessageError,
        maxRequest,
        notAbleAccess,
        errorCalProjectPrice
    }

    public class ResultShow<T>
    {
        public ResultShow()
        {
            Errors = new List<string>();
        }

        public string DataValue { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public T DataValueObject { get; set; }
        public List<string> Errors { get; set; }
    }
}