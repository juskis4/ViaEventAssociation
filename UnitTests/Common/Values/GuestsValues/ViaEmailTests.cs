using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventAssociation.Core.Tools.OperationResult;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Common.Values.GuestsValues;

    public class ViaEmailTests
    {

        [Fact]
        public void Create_ValidEmail_ReturnsSuccessResult()
        {
            // Arrange
            const string validEmail = "john@via.dk";

            // Act
            var result = ViaEmail.Create(validEmail);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(validEmail, result.Data.Email);
        }
        
        [Fact]
        public void Create_ValidEmailNumbers_ReturnsSuccessResult()
        {
            // Arrange
            const string validEmail = "123456@via.dk";

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
            Assert.Contains("Email cannot bee null er empty.", result.Errors);
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
            Assert.Contains("Email must end with '@via.dk'.", result.Errors);
        }

        [Fact]
        public void Create_InvalidEmailWithInvalidDomain_ReturnsFailureResult()
        {
            // Arrange
            const string invalidEmail = "john@invalid.com";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email must end with '@via.dk'.", result.Errors);
        }

        [Fact]
        public void Create_EmailWithLettersAndNumbers_ReturnsFailure()
        {
            // Arrange
            const string invalidEmail = "john123@via.dk";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email name must be between 3 and 6 characters long.", result.Errors); 
        }
        
        [Fact]
        public void Create_EmailWithLetters3_ReturnsSuccess()
        {
            // Arrange
            const string invalidEmail = "joh@via.dk";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.True(result.IsSuccess);
        }
        
        [Fact]
        public void Create_EmailWithLetters2_ReturnsFailure()
        {
            // Arrange
            const string invalidEmail = "jo@via.dk";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email name must be between 3 and 6 characters long.", result.Errors); 
        }
        
        [Fact]
        public void Create_EmailWithLetters5_ReturnsFailure()
        {
            // Arrange
            const string invalidEmail = "johnw@via.dk";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email name must be either 3 or 4  English letters, or 6 digits from 0 to 9.", result.Errors); 
        }
        
        [Fact]
        public void Create_EmailWithNonEnglishLetters_ReturnsFailure()
        {
            // Arrange
            const string invalidEmail = "Sørn@via.dk";

            // Act
            var result = ViaEmail.Create(invalidEmail);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Email format is incorrect Consider using English Letters.", result.Errors); 
        }
    }

