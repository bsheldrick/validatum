using System;
using System.Linq;
using Xunit;

namespace Valdrick.Tests
{
    public class ValidatorBuilderCommonExtensionsTests
    {
        [Fact]
        public void NotNull_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.NotNull<string>(null);
            });
        }

        [Fact]
        public void NotNull_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNull()
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotNull", brokenRule.Rule);
            Assert.Equal("Employee", brokenRule.Key);
            Assert.Equal("Value cannot be null.", brokenRule.Message);
        }

        [Fact]
        public void NotNull_ShouldNotAddBrokenRule_WhenValueIsNotNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNull()
                .Build();

            // act
            var result = validator.Validate(new Employee());

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void NotNull_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNull("test")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void NotNull_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNull(message: "test")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void NotNullFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.NotNullFor<string, string>(null, null);
            });
        }

        [Fact]
        public void NotNullFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.NotNullFor<string, string>(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void NotNullFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNullFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5 }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotNull", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
            Assert.Equal("Value cannot be null.", brokenRule.Message);
        }

        [Fact]
        public void NotNullFor_ShouldNotAddBrokenRule_WhenValueIsNotNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNullFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "Acme Inc." }
            });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void NotNullFor_ShouldUseExpressionMemberName_AsKeyInBrokenRule_WhenKeyNotProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNullFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5 }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Employer.Name", brokenRule.Key);
        }

        [Fact]
        public void NotNullFor_ShouldUseKey_AsKeyInBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNullFor(e => e.Employer.Name, key: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5 }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void NotNullFor_ShouldUseMessage_AsErrorInBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNullFor(e => e.Employer.Name, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5 }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void NotNullFor_ShouldNotAddBrokenRule_WhenTargetIsNotNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotNullFor(e => e.Employer.Name)
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

        [Fact]
        public void Null_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Null<string>(null);
            });
        }

        [Fact]
        public void Null_ShouldAddBrokenRuleToContext_WhenValueIsNotNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Null()
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Null", brokenRule.Rule);
            Assert.Equal("Employee", brokenRule.Key);
            Assert.Equal("Value must be null.", brokenRule.Message);
        }

        [Fact]
        public void Null_ShouldNotAddBrokenRuleToContext_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Null()
                .Build();

            // act
            var result = validator.Validate(null);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Null_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Null("test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Null_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Null(message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void NullFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.NullFor<string, string>(null, null);
            });
        }

        [Fact]
        public void NullFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.NullFor<string, string>(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void NullFor_ShouldAddBrokenRuleToContext_WhenValueIsNotNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NullFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                Employer = new Company { Name = "Acme Inc." }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Null", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
            Assert.Equal("Value must be null.", brokenRule.Message);
        }

        [Fact]
        public void NullFor_ShouldNotAddBrokenRule_WhenTargetIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NullFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5 }
            });

            // assert
            Assert.True(result.IsValid);
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void NullFor_ShouldUseExpressionMemberName_AsKeyInBrokenRule_WhenKeyNotProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NullFor(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "Acme Inc." }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Employer.Name", brokenRule.Key);
        }

        [Fact]
        public void NullFor_ShouldUseKey_AsKeyInBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NullFor(e => e.Employer.Name, key: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "Acme Inc." }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void NullFor_ShouldUseMessage_InBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NullFor(e => e.Employer.Name, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "Acme Inc." }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("test", brokenRule.Message);
        }       
    }
}