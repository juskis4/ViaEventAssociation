using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using ViaEventAssociation.Core.Tools.OperationResult;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Common.Values.GuestsValues
{
    public class ViaEmailTests
    {

        [Fact]
        public void Create_ValidEmail_ReturnsSuccessResult()
        {
            // Arrange
            const string validEmail = "john.doe@via.dk";

            // Act
            var result = ViaEmail.Create(validEmail);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(validEmail, result.Data.Email);
        }

        [Fact]
        public void Create_InvalidWhitespaceEmail_ReturnsFailureResult()
        {
            // Act
            var result = ViaEmail.Create(" ");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email cannot bee a white space.", result.Errors);
        }
        
        [Fact]
        public void Create_InvalidEmptyEmail_ReturnsFailureResult()
        {
            // Act
            var result = ViaEmail.Create("");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email cannot bee Empty.", result.Errors);
        }

        [Fact]
        public void Create_InvalidEmailWithoutAtSymbol_ReturnsFailureResult()
        {
            // Arrange
            const string invalidEmail = "john.doe.example.dk";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email should contain one @.", result.Errors);
        }

        [Fact]
        public void Create_InvalidEmailWithInvalidDomain_ReturnsFailureResult()
        {
            // Arrange
            const string invalidEmail = "john.doe@invalid.com";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email domain must be 'via' or 'viauc'.", result.Errors);
        }

        [Fact]
        public void Create_InvalidEmailWithoutDKDomain_ReturnsFailureResult()
        {
            // Arrange
            const string invalidEmail = "john.doe@via.com";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email must end with '.dk'.", result.Errors);
        }
    }
}
