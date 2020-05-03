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

        [Fact]
        public void GreaterThanOrEqual_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.GreaterThanOrEqual<string>(null, null);
            });
        }

        [Fact]
        public void GreaterThanOrEqual_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .GreaterThanOrEqual("a")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThanOrEqual", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be greater than or equal to 'a'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanOrEqual_ShouldAddBrokenRule_WhenValueIsNotGreaterThanOrEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThanOrEqual(5)
                .Build();

            // act
            var result = validator.Validate(4);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThanOrEqual", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be greater than or equal to '5'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanOrEqual_ShouldNotAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThanOrEqual(5)
                .Build();

            // act
            var result = validator.Validate(5);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void GreaterThanOrEqual_ShouldNotAddBrokenRule_WhenValueIsGreaterThan()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThanOrEqual(5)
                .Build();

            // act
            var result = validator.Validate(6);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void GreaterThanOrEqual_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThanOrEqual(5, "test")
                .Build();

            // act
            var result = validator.Validate(4);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void GreaterThanOrEqual_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .GreaterThanOrEqual(5, message: "test")
                .Build();

            // act
            var result = validator.Validate(4);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanOrEqualFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.GreaterThanOrEqualFor<string, string>(null, null, null);
            });
        }

        [Fact]
        public void GreaterThanOrEqualFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.GreaterThanOrEqualFor<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void GreaterThanOrEqualFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanOrEqualFor(e => e.FirstName, "a")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThanOrEqual", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be greater than or equal to 'a'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanOrEqualFor_ShouldAddBrokenRule_WhenValueIsNotGreaterThanOrEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanOrEqualFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 4 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("GreaterThanOrEqual", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be greater than or equal to '5'.", brokenRule.Message);
        }

        [Fact]
        public void GreaterThanOrEqualFor_ShouldNotAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanOrEqualFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 5 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void GreaterThanOrEqualFor_ShouldNotAddBrokenRule_WhenValueIsGreaterThan()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanOrEqualFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 6 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void GreaterThanOrEqualFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanOrEqualFor(e => e.Id, 5, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void GreaterThanOrEqualFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanOrEqualFor(e => e.Id, 5, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void LessThan_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.LessThan<string>(null, null);
            });
        }

        [Fact]
        public void LessThan_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .LessThan("a")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThan", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be less than 'a'.", brokenRule.Message);
        }

        [Fact]
        public void LessThan_ShouldAddBrokenRule_WhenValueIsNotLessThan()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThan(5)
                .Build();

            // act
            var result = validator.Validate(6);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThan", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be less than '5'.", brokenRule.Message);
        }

        [Fact]
        public void LessThan_ShouldAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThan(5)
                .Build();

            // act
            var result = validator.Validate(5);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThan", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be less than '5'.", brokenRule.Message);
        }

        [Fact]
        public void LessThan_ShouldNotAddBrokenRule_WhenValueIsLessThan()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThan(5)
                .Build();

            // act
            var result = validator.Validate(4);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LessThan_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThan(5, "test")
                .Build();

            // act
            var result = validator.Validate(6);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void LessThan_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThan(5, message: "test")
                .Build();

            // act
            var result = validator.Validate(6);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void LessThanFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.LessThanFor<string, string>(null, null, null);
            });
        }

        [Fact]
        public void LessThanFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.LessThanFor<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void LessThanFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanFor(e => e.FirstName, "a")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThan", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be less than 'a'.", brokenRule.Message);
        }

        [Fact]
        public void LessThanFor_ShouldAddBrokenRule_WhenValueIsNotLessThan()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 6 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThan", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be less than '5'.", brokenRule.Message);
        }

        [Fact]
        public void LessThanFor_ShouldAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 5 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThan", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be less than '5'.", brokenRule.Message);
        }

        [Fact]
        public void LessThanFor_ShouldNotAddBrokenRule_WhenValueIsLessThan()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanFor(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 4 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LessThanFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanFor(e => e.Id, -1, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void LessThanFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanFor(e => e.Id, -1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
    }
}