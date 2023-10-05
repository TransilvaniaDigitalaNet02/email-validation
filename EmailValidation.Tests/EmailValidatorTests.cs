namespace EmailValidation.Tests
{
    public class EmailValidatorTests
    {
        [Fact]
        public void IsEmailValidManual_WhenEmailIsNull_ReturnsFalse()
        {
            // arrange
            string email = null;

            // act
            bool isValid = EmailValidator.IsEmailValidManual(email);

            // assert
            Assert.False(isValid);
        }

        [InlineData("user@gmail.com", true)]
        [InlineData("user@gmail", false)]
        [InlineData("userABC", false)]
        [Theory]
        public void IsEmailValidManual_WhenEmailIsSpecified_ExpectedOutcome(string email, bool expectedResult)
        {
            // act
            bool isValid = EmailValidator.IsEmailValidManual(email);

            // assert
            Assert.Equal(expectedResult, isValid);
        }
    }
}