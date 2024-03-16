using System.Collections;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Requests.ValueObjects;

public class AttendanceReason
{
    public string Reason { get; set; }

    private AttendanceReason(string reason)
    {
        this.Reason = reason;
    }

    public static Result<AttendanceReason> Create(string reason)
    {
        var validate = Validate(reason);
        return validate.IsSuccess
            ? Result<AttendanceReason>.Success(new AttendanceReason(reason))
            : Result<AttendanceReason>.Failure(validate.Errors.ToArray());
    }

    private static Result Validate(string reason)
    {
        var errors = new List<string>();
        if (string.IsNullOrEmpty(reason))
        {
            errors.Add("Reason cannot be null or Empty.");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                errors.Add("Reason cannot be Whitespace.");
            }

            if (reason.Length < 3)
            {
                errors.Add("AttendanceReason cannot be less than 3 Characters.");
            }

            if (reason.Length > 200)
            {
                errors.Add("AttendanceReason is too Long.");
            }
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();

    }

}