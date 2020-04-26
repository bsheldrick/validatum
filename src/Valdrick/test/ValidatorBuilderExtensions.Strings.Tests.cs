using System;
using System.Linq;
using Xunit;

namespace Valdrick.Tests
{
    public class ValidatorBuilderStringExtensionsTests
    {
        [Fact]
        public void NotEmpty_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.NotEmpty(null);
            });
        }

        [Fact]
        public void NotEmpty_ShouldAddBrokenRuleToContext_WhenValueIsEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEmpty()
                .Build();

            // act
            var result = validator.Validate(string.Empty);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotEmpty", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value cannot be empty.", brokenRule.Message);
        }

        [Fact]
        public void NotEmpty_ShouldAddBrokenRuleToContext_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEmpty()
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotEmpty", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value cannot be empty.", brokenRule.Message);
        }

        [Fact]
        public void NotEmpty_ShouldNotAddBrokenRuleToContext_WhenValueIsNotEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEmpty()
                .Build();

            // act
            var result = validator.Validate("test");

            // assert
            Assert.Empty(result.BrokenRules);
        }
        
        [Fact]
        public void NotEmpty_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEmpty("test")
                .Build();

            // act
            var result = validator.Validate(string.Empty);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void NotEmpty_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEmpty(message: "test")
                .Build();

            // act
            var result = validator.Validate(string.Empty);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
        
        [Fact]
        public void NotEmptyFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.NotEmptyFor<string>(null, null);
            });
        }

        [Fact]
        public void NotEmptyFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.NotEmptyFor<string>(new ValidatorBuilder<string>(), null);
            });
        }
        
        [Fact]
        public void NotEmptyFor_ShouldAddBrokenRule_WhenValueIsEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmptyFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "" }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotEmpty", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
        }

        [Fact]
        public void NotEmptyFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmptyFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5 }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotEmpty", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
        }

        [Fact]
        public void NotEmptyFor_ShouldUseExpressionMemberName_AsKeyInBrokenRule_WhenKeyNotProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmptyFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "" }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotEmpty", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
        }

        [Fact]
        public void NotEmptyFor_ShouldUseKey_AsKeyInBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmptyFor(e => e.Employer.Name, key: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "" }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void NotEmptyFor_ShouldUseMessage_AsMessageInBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmptyFor(e => e.Employer.Name, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "" }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
        
        [Fact]
        public void NotEmptyFor_ShouldNotAddBrokenRule_WhenTargetIsNotNullOrEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmptyFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "Acme Inc." }
            });

            // assert
            Assert.True(result.IsValid);
            Assert.Empty(result.BrokenRules);
        }
    }
}