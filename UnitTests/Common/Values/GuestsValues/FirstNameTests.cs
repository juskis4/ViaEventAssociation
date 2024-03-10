namespace UnitTests.Common.Values.GuestsValues;


using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using Xunit;


public class FirstNameTests
{
     [Fact]
        public void Create_ValidName_ReturnsSuccessResult()
        {
            // Arrange
            var firstName = FirstName.Create("John");
            
            Assert.True(firstName.IsSuccess);
            Assert.Equal("John",firstName.Data.Name);
        }
        //TODO: find a way to null values.
        /*
        [Fact]
        public void Create_NullName_ReturnsFailureResult()
        {
            // Arrange
            string name = null ;
            
            //Act
            var result = FirstName.Create(name);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot be NULL or Empty.", result.Errors);

        }
        */

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
            var result = FirstName.Create("Jo");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot be less than 3 characters.", result.Errors);
        }

        [Fact]
        public void Create_NameTooLong_ReturnsFailureResult()
        {
            // Arrange
            var result = FirstName.Create("ThisIsALongFirstName");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot be more than 16 characters.", result.Errors);
        }
        
        [Fact]
        public void Create_containsSpecialChar_ReturnsFailureResult()
        {
            // Arrange
            var result = FirstName.Create("Jhon@");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("First name cannot contains Special char.", result.Errors);
        }
}