namespace UnitTests.Common.Values.GuestsValues;


using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using ViaEventAssociation.Core.Tools.OperationResult;
using Xunit;


public class LastNameTests
{
     [Fact]
        public void Create_ValidName_ReturnsSuccessResult()
        {
            // Arrange
            Result<LastName> result = LastName.Create("John");
            
            Assert.True(result.IsSuccess);
            Assert.Equal("John",result.Data.Name);
        }
        //TODO: find a way to null values.
        /*
        [Fact]
        public void Create_NullName_ReturnsFailureResult()
        {
            // Arrange
            string name = null ;
            
            //Act
            var result = LastName.Create(name);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot be NULL or Empty.", result.Errors);

        }
        */

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
            var result = LastName.Create("ThisIsALongFirstName");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Last name cannot be more than 16 characters.", result.Errors);
        }
        
        [Fact]
        public void Create_containsSpecialChar_ReturnsFailureResult()
        {
            // Arrange
            var result = LastName.Create("Jhon@");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Last name cannot contains Special char.", result.Errors);
        }
}