using System;
using System.Linq;
using Xunit;

namespace Validatum.Tests
{
    public class ValidatorBuilderBooleanExtensionsTests
    {
        [Fact]
        public void True_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.True(null);
            });
        }

        [Fact]
        public void True_ShouldAddBrokenRule_WhenValueIsFalse()
        {
            // arrange
            var validator = new ValidatorBuilder<bool>()
                .True()
                .Build();

            // act
            var result = validator.Validate(false);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("True", brokenRule.Rule);
            Assert.Equal("Boolean", brokenRule.Key);
            Assert.Equal("Value must be true.", brokenRule.Message);
        }

        [Fact]
        public void True_ShouldNotAddBrokenRule_WhenValueIsTrue()
        {
            // arrange
            var validator = new ValidatorBuilder<bool>()
                .True()
                .Build();

            // act
            var result = validator.Validate(true);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void True_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<bool>()
                .True("test")
                .Build();

            // act
            var result = validator.Validate(false);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void True_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<bool>()
                .True(message: "test")
                .Build();

            // act
            var result = validator.Validate(false);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void TrueFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.True<string>(null, null);
            });
        }

        [Fact]
        public void TrueFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.True<string>(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void TrueFor_ShouldAddBrokenRule_WhenValueIsFalse()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .True(e => e.Active)
                .Build();

            // act
            var result = validator.Validate(new Employee { Active = false });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("True", brokenRule.Rule);
            Assert.Equal("Active", brokenRule.Key);
            Assert.Equal("Value must be true.", brokenRule.Message);
        }

        [Fact]
        public void TrueFor_ShouldNotAddBrokenRule_WhenValueIsTrue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .True(e => e.Active)
                .Build();

            // act
            var result = validator.Validate(new Employee { Active = true });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void TrueFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .True(e => e.Active, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void TrueFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .True(e => e.Active, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void False_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.False(null);
            });
        }

        [Fact]
        public void False_ShouldAddBrokenRule_WhenValueIsTrue()
        {
            // arrange
            var validator = new ValidatorBuilder<bool>()
                .False()
                .Build();

            // act
            var result = validator.Validate(true);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("False", brokenRule.Rule);
            Assert.Equal("Boolean", brokenRule.Key);
            Assert.Equal("Value must be false.", brokenRule.Message);
        }

        [Fact]
        public void False_ShouldNotAddBrokenRule_WhenValueIsFalse()
        {
            // arrange
            var validator = new ValidatorBuilder<bool>()
                .False()
                .Build();

            // act
            var result = validator.Validate(false);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void False_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<bool>()
                .False("test")
                .Build();

            // act
            var result = validator.Validate(true);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void False_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<bool>()
                .False(message: "test")
                .Build();

            // act
            var result = validator.Validate(true);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void FalseFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.False<string>(null, null);
            });
        }

        [Fact]
        public void FalseFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.False<string>(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void FalseFor_ShouldAddBrokenRule_WhenValueIsTrue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .False(e => e.Active)
                .Build();

            // act
            var result = validator.Validate(new Employee { Active = true });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("False", brokenRule.Rule);
            Assert.Equal("Active", brokenRule.Key);
            Assert.Equal("Value must be false.", brokenRule.Message);
        }

        [Fact]
        public void FalseFor_ShouldNotAddBrokenRule_WhenValueIsFalse()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .False(e => e.Active)
                .Build();

            // act
            var result = validator.Validate(new Employee { Active = false });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void FalseFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .False(e => e.Active, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Active = true });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void FalseFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .False(e => e.Active, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Active = true });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
    }
}
