using System;
using System.Linq;
using Xunit;

namespace Validatum.Tests
{
    public class ValidatorBuilderComparisonExtensionsTests
    {
        [Fact]
        public void GreatreThan_ThrowsException_WhenBuilderIsNull()
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
                ValidatorBuilderExtensions.GreaterThan<string, string>(null, null, null);
            });
        }

        [Fact]
        public void GreaterThanFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.GreaterThan<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void GreaterThanFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThan(e => e.FirstName, "a")
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
                .GreaterThan(e => e.Id, 5)
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
                .GreaterThan(e => e.Id, 5)
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
                .GreaterThan(e => e.Id, 5)
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
                .GreaterThan(e => e.Id, 5, "test")
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
                .GreaterThan(e => e.Id, 5, message: "test")
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
                ValidatorBuilderExtensions.GreaterThanOrEqual<string, string>(null, null, null);
            });
        }

        [Fact]
        public void GreaterThanOrEqualFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.GreaterThanOrEqual<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void GreaterThanOrEqualFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .GreaterThanOrEqual(e => e.FirstName, "a")
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
                .GreaterThanOrEqual(e => e.Id, 5)
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
                .GreaterThanOrEqual(e => e.Id, 5)
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
                .GreaterThanOrEqual(e => e.Id, 5)
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
                .GreaterThanOrEqual(e => e.Id, 5, "test")
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
                .GreaterThanOrEqual(e => e.Id, 5, message: "test")
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
                ValidatorBuilderExtensions.LessThan<string, string>(null, null, null);
            });
        }

        [Fact]
        public void LessThanFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.LessThan<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void LessThanFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThan(e => e.FirstName, "a")
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
                .LessThan(e => e.Id, 5)
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
                .LessThan(e => e.Id, 5)
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
                .LessThan(e => e.Id, 5)
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
                .LessThan(e => e.Id, -1, "test")
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
                .LessThan(e => e.Id, -1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void LessThanOrEqual_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.LessThanOrEqual<string>(null, null);
            });
        }

        [Fact]
        public void LessThanOrEqual_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .LessThanOrEqual("a")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThanOrEqual", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be less than or equal to 'a'.", brokenRule.Message);
        }

        [Fact]
        public void LessThanOrEqual_ShouldAddBrokenRule_WhenValueIsNotLessThanOrEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThanOrEqual(4)
                .Build();

            // act
            var result = validator.Validate(5);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThanOrEqual", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be less than or equal to '4'.", brokenRule.Message);
        }

        [Fact]
        public void LessThanOrEqual_ShouldNotAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThanOrEqual(5)
                .Build();

            // act
            var result = validator.Validate(5);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LessThanOrEqual_ShouldNotAddBrokenRule_WhenValueIsLessThan()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThanOrEqual(6)
                .Build();

            // act
            var result = validator.Validate(5);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LessThanOrEqual_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThanOrEqual(4, "test")
                .Build();

            // act
            var result = validator.Validate(5);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void LessThanOrEqual_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .LessThanOrEqual(4, message: "test")
                .Build();

            // act
            var result = validator.Validate(5);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void LessThanOrEqualFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.LessThanOrEqual<string, string>(null, null, null);
            });
        }

        [Fact]
        public void LessThanOrEqualFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.LessThanOrEqual<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void LessThanOrEqualFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanOrEqual(e => e.FirstName, "a")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThanOrEqual", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be less than or equal to 'a'.", brokenRule.Message);
        }

        [Fact]
        public void LessThanOrEqualFor_ShouldAddBrokenRule_WhenValueIsNotLessThanOrEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanOrEqual(e => e.Id, 4)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 5 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("LessThanOrEqual", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be less than or equal to '4'.", brokenRule.Message);
        }

        [Fact]
        public void LessThanOrEqualFor_ShouldNotAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanOrEqual(e => e.Id, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 5 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LessThanOrEqualFor_ShouldNotAddBrokenRule_WhenValueIsLessThan()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanOrEqual(e => e.Id, 6)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 5 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LessThanOrEqualFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanOrEqual(e => e.Id, -1, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void LessThanOrEqualFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .LessThanOrEqual(e => e.Id, -1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Range_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Range<string>(null, null, null);
            });
        }

        [Fact]
        public void Range_ThrowsException_WhenLowerIsNull()
        {
            Assert.Throws<ArgumentNullException>("lower", () =>
            {
                ValidatorBuilderExtensions.Range(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void Range_ThrowsException_WhenUpperIsNull()
        {
            Assert.Throws<ArgumentNullException>("upper", () =>
            {
                ValidatorBuilderExtensions.Range(new ValidatorBuilder<string>(), "", null);
            });
        }

        [Fact]
        public void Range_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Range("a", "d")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Range", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be in range 'a' to 'd'.", brokenRule.Message);
        }

        [Fact]
        public void Range_ShouldAddBrokenRule_WhenValueLessThanLower()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .Range(2, 5)
                .Build();

            // act
            var result = validator.Validate(1);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Range", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be in range '2' to '5'.", brokenRule.Message);
        }

        [Fact]
        public void Range_ShouldAddBrokenRule_WhenValueGreaterThanUpper()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .Range(2, 5)
                .Build();

            // act
            var result = validator.Validate(6);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Range", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be in range '2' to '5'.", brokenRule.Message);
        }

        [Fact]
        public void Range_ShouldAddBrokenRule_WhenLowerGreaterThanUpper()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .Range(5, 2)
                .Build();

            // act
            var result = validator.Validate(3);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Range", brokenRule.Rule);
            Assert.Equal("Int32", brokenRule.Key);
            Assert.Equal("Value must be in range '5' to '2'.", brokenRule.Message);
        }

        [Fact]
        public void Range_ShouldNotAddBrokenRule_WhenValueEqualsLower()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .Range(2, 5)
                .Build();

            // act
            var result = validator.Validate(2);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Range_ShouldNotAddBrokenRule_WhenValueEqualsUpper()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .Range(2, 5)
                .Build();

            // act
            var result = validator.Validate(5);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Range_ShouldNotAddBrokenRule_WhenValueInRange()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .Range(2, 5)
                .Build();

            // act
            var result = validator.Validate(3);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Range_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .Range(1, 3, "test")
                .Build();

            // act
            var result = validator.Validate(0);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Range_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<int>()
                .Range(1, 3, message: "test")
                .Build();

            // act
            var result = validator.Validate(0);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void RangeFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Range<string, string>(null, null, null, null);
            });
        }

        [Fact]
        public void RangeFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Range<string, string>(new ValidatorBuilder<string>(), null, null, null);
            });
        }

        [Fact]
        public void RangeFor_ThrowsException_WhenLowerIsNull()
        {
            Assert.Throws<ArgumentNullException>("lower", () =>
            {
                ValidatorBuilderExtensions.Range<Employee, string>(new ValidatorBuilder<Employee>(), e => e.FirstName, null, null);
            });
        }

        [Fact]
        public void RangeFor_ThrowsException_WhenUpperIsNull()
        {
            Assert.Throws<ArgumentNullException>("upper", () =>
            {
                ValidatorBuilderExtensions.Range<Employee, string>(new ValidatorBuilder<Employee>(), e => e.FirstName, "a", null);
            });
        }

        [Fact]
        public void RangeFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.FirstName, "a", "d")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Range", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be in range 'a' to 'd'.", brokenRule.Message);
        }

        [Fact]
        public void RangeFor_ShouldAddBrokenRule_WhenValueLessThanLower()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.Id, 2, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 1 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Range", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be in range '2' to '5'.", brokenRule.Message);
        }

        [Fact]
        public void RangeFor_ShouldAddBrokenRule_WhenValueGreaterThanUpper()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.Id, 2, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 6 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Range", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be in range '2' to '5'.", brokenRule.Message);
        }

        [Fact]
        public void RangeFor_ShouldAddBrokenRule_WhenLowerGreaterThanUpper()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.Id, 5, 2)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 3 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Range", brokenRule.Rule);
            Assert.Equal("Id", brokenRule.Key);
            Assert.Equal("Value must be in range '5' to '2'.", brokenRule.Message);
        }

        [Fact]
        public void RangeFor_ShouldNotAddBrokenRule_WhenValueEqualsLower()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.Id, 2, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 2 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void RangeFor_ShouldNotAddBrokenRule_WhenValueEqualsUpper()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.Id, 2, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 5 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void RangeFor_ShouldNotAddBrokenRule_WhenValueInRange()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.Id, 2, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 3 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void RangeFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.Id, 1, 3, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 0 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void RangeFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Range(e => e.Id, 1, 3, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Id = 0 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Compare_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Compare<string, string>(null, null, null);
            });
        }

        [Fact]
        public void Compare_ThrowsException_WhenLeftSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("leftSelector", () =>
            {
                ValidatorBuilderExtensions.Compare<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void Compare_ThrowsException_WhenRightSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("rightSelector", () =>
            {
                ValidatorBuilderExtensions.Compare<Employee, string>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void Compare_ShouldAddBrokenRule_WhenValuesAreNotEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Compare(e => e.FirstName, e => e.LastName)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            { 
                FirstName = "William",
                LastName = "Riker"
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Compare", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be equal to value of LastName", brokenRule.Message);
        }

        [Fact]
        public void Compare_ShouldNotAddBrokenRule_WhenValuesAreEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Compare(e => e.FirstName, e => e.LastName)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            { 
                FirstName = "test",
                LastName = "test"
            });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Compare_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Compare(e => e.FirstName, e => e.LastName, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            { 
                FirstName = "William",
                LastName = "Riker"
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Compare_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Compare(e => e.FirstName, e => e.LastName, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            { 
                FirstName = "William",
                LastName = "Riker"
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }       
    }
}