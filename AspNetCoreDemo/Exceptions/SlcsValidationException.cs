using System;

using SO = AspNetCoreDemo.Model.SlcsOutbound;

namespace AspNetCoreDemo.Exceptions
{
    public class SlcsValidationException : ApplicationException
    {
        public SlcsValidationException(ValidationExceptionSeverity exceptionSeverity, string messageForLogs, SO.SlcsErrors slcsErrors)
        {
            SlcsErrors = slcsErrors ?? throw new ArgumentNullException("slcsErrors");

            if (String.IsNullOrWhiteSpace(messageForLogs)) throw new ArgumentNullException("messageForLogs");
            MessageForLogs = messageForLogs;

            ExceptionSeverity = exceptionSeverity;
        }

        public SlcsValidationException(ValidationExceptionSeverity exceptionSeverity, string messageForLogs, SO.SlcsError slcsError)
            : this(exceptionSeverity, messageForLogs, SO.SlcsErrors.WrapError(slcsError))
        {
        }

        public SO.SlcsErrors SlcsErrors { get; }

        public ValidationExceptionSeverity ExceptionSeverity { get; }

        public string MessageForLogs { get; }
    }

    public enum ValidationExceptionSeverity
    {
        Info,
        Warning,
        Error
    }
}