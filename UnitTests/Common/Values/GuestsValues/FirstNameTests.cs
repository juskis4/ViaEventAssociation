namespace UnitTests.Common.Values.GuestsValues;


using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using Xunit;


public class FirstNameTests
{
     [Fact]
        public void Create_ValidName_ReturnsSuccessResult()
        {
            // Arrange
            var firstName = FirstName.Create("john");
            
            Assert.True(firstName.IsSuccess);
            Assert.Equal("John",firstName.Data.Name);
        }

        [Fact]
        public void Create_EmptyName_ReturnsFailureResult()
        {
            // Arrange
            var result = FirstName.Create("");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot be NULL or Empty.", result.Errors);
        }

        [Fact]
        public void Create_NameWithSpace_ReturnsFailureResult()
        {
            // Arrange
            var result = FirstName.Create(" ");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot bee a white space.", result.Errors);
        }

        [Fact]
        public void Create_NameTooShort_ReturnsFailureResult()
        {
            // Arrange
            var result = FirstName.Create("J");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot be less than 2 characters.", result.Errors);
        }

        [Fact]
        public void Create_NameTooLong_ReturnsFailureResult()
        {
            // Arrange
            var result = FirstName.Create("ThisIsALongFirstNameThisIsALongFirstNa");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot be more than 25 characters.", result.Errors);
        }
        
        [Fact]
        public void Create_containsSpecialChar_ReturnsFailureResult()
        {
            // Arrange
            var result = FirstName.Create("Jhon@");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot contains Special characters.", result.Errors);
        }
        
        [Fact]
        public void Create_containsNumbers_ReturnsFailureResult()
        {
            // Arrange
            var result = FirstName.Create("Jhon2");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot contain numbers.", result.Errors);
        }
}