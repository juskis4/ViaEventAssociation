
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Common.Bases;

public class GuestId
{
    public int id { get; set; }

    private GuestId(int id)
    {
        this.id = id;
    }

    public static Result<GuestId> Create(int id)
    {
        var result = Validate(id);
        return !result.IsSuccess ? Result<GuestId>.Failure(result.Errors.ToArray()) : Result<GuestId>.Success(new GuestId(id));
    }

    private static Result Validate(int id)
    {
        //Coming Validations for ID
        return Result.Success();
    }
}