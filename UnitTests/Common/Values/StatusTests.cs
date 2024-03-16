using ViaEventAssociation.Core.Domain.Common.Values;
using Xunit;

namespace UnitTests.Common.Values;

public class StatusTests
{
    
    [Fact]
    public void Create_Status_Pending_Successfully()
    {
        var result = Status.Create(RequestStatus.Pending);

        Assert.True(result.IsSuccess);
        Assert.Equal(RequestStatus.Pending, result.Data.RequestStatus);
    }

    [Fact]
    public void Create_Status_Accepted_Successfully()
    {
        var result = Status.Create(RequestStatus.Accepted);

        Assert.True(result.IsSuccess);
        Assert.Equal(RequestStatus.Accepted, result.Data.RequestStatus);
    }

    [Fact]
    public void Create_Status_Rejected_Successfully()
    {
        var result = Status.Create(RequestStatus.Rejected);

        Assert.True(result.IsSuccess);
        Assert.Equal(RequestStatus.Rejected, result.Data.RequestStatus);
    }
}
