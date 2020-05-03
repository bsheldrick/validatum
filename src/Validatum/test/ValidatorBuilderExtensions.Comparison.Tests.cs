using System;
using System.Linq;
using Xunit;

namespace Validatum.Tests
{
    public class ValidatorBuilderComparisonExtensionsTests
    {
        [Fact]
        public void GreaterThan_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.GreaterThan<string>(null, null);
            });
        }

        [Fact]
        public void GreaterThan_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .GreaterThan("a")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThan", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be greater than 'a'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThan_ShouldAddBrokenRule_WhenValueIsNotGreaterThan()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThan(5)
                .Build();

            // act
            var result = validator.Validate(4);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThan", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be greater than '5'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThan_ShouldAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThan(5)
                .Build();

            // act
            var result = validator.Validate(5);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThan", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be greater than '5'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThan_ShouldNotAddBrokenRule_WhenValueIsGreaterThan()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThan(5)
                .Build();

            // act
            var result = validator.Validate(6);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void GreaterThan_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThan(5, "test")
                .Build();

            // act
            var result = validator.Validate(4);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void GreaterThan_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThan(5, message: "test")
                .Build();

            // act
            var result = validator.Validate(4);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.GreaterThanFor<string, string>(null, null, null);
            });
        }

        [Fact]
        public void GreaterThanFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.GreaterThanFor<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void GreaterThanFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanFor(e => e.FirstName, "a")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThan", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be greater than 'a'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanFor_ShouldAddBrokenRule_WhenValueIsNotGreaterThan()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 4 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThan", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be greater than '5'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanFor_ShouldAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 5 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThan", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be greater than '5'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanFor_ShouldNotAddBrokenRule_WhenValueIsGreaterThan()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 6 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void GreaterThanFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanFor(e => e.Id, 5, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void GreaterThanFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanFor(e => e.Id, 5, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
    }
}