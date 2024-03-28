namespace UnitTests.Common.Values.GuestsValues;


using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventAssociation.Core.Tools.OperationResult;
using Xunit;


public class LastNameTests
{
     [Fact]
        public void Create_ValidName_ReturnsSuccessResult()
        {
            // Arrange
            Result<LastName> result = LastName.Create("john");
            
            Assert.True(result.IsSuccess);
            Assert.Equal("John",result.Data.Name);
        }

        [Fact]
        public void Create_EmptyName_ReturnsFailureResult()
        {
            // Arrange
            var result = LastName.Create("");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Last name cannot be NULL or Empty.", result.Errors);
        }

        [Fact]
        public void Create_NameWithSpace_ReturnsFailureResult()
        {
            // Arrange
            var result = LastName.Create(" ");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Last name cannot bee a white space.", result.Errors);
        }

        [Fact]
        public void Create_NameTooShort_ReturnsFailureResult()
        {
            // Arrange
            var result = LastName.Create("J");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Last name cannot be less than 2 characters.", result.Errors);
        }

        [Fact]
        public void Create_NameTooLong_ReturnsFailureResult()
        {
            // Arrange
            var result = LastName.Create("ThisIsALongFirstNameThisIsALongFirstName");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Last name cannot be more than 25 characters.", result.Errors);
        }
        
        [Fact]
        public void Create_containsSpecialChar_ReturnsFailureResult()
        {
            // Arrange
            var result = LastName.Create("Jhon@");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Last name cannot contains Special characters.", result.Errors);
        }
        
        [Fact]
        public void Create_containsNumbers_ReturnsFailureResult()
        {
            // Arrange
            var result = LastName.Create("Jhon1");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Last name cannot contain numbers.", result.Errors);
        }
}