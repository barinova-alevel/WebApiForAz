using SFMB.BL.Dtos;
using Xunit;

namespace SFMB.Tests
{
    public class AuthResponseDtoTests
    {
        [Fact]
        public void ToString_HidesSensitiveData()
        {
            // Arrange
            var authResponse = new AuthResponseDto
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.sensitive_token_data",
                Email = "admin@sfmb.com",
                UserId = "986e3115-4cd9-4835-b7e1-623c1c559da6",
                Role = "Admin"
            };

            // Act
            var stringRepresentation = authResponse.ToString();

            // Assert - sensitive data should NOT be in the string representation
            Assert.DoesNotContain("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", stringRepresentation);
            Assert.DoesNotContain("admin@sfmb.com", stringRepresentation);
            Assert.DoesNotContain("986e3115-4cd9-4835-b7e1-623c1c559da6", stringRepresentation);
            Assert.DoesNotContain("Admin", stringRepresentation);
            
            // Assert - should indicate that sensitive data is hidden
            Assert.Contains("sensitive data hidden", stringRepresentation);
        }

        [Fact]
        public void ToString_ReturnsConsistentMessage()
        {
            // Arrange
            var authResponse1 = new AuthResponseDto
            {
                Token = "different_token",
                Email = "user1@example.com",
                UserId = "user-id-1",
                Role = "User"
            };

            var authResponse2 = new AuthResponseDto
            {
                Token = "another_token",
                Email = "user2@example.com",
                UserId = "user-id-2",
                Role = "Admin"
            };

            // Act
            var string1 = authResponse1.ToString();
            var string2 = authResponse2.ToString();

            // Assert - both should return the same sanitized message
            Assert.Equal("AuthResponse [sensitive data hidden]", string1);
            Assert.Equal("AuthResponse [sensitive data hidden]", string2);
        }
    }
}
