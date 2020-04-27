using System;
using System.Linq;
using Xunit;

namespace Validatum.Tests
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
        public void Null_ShouldAddBrokenRule_WhenValueIsNotNull()
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
        public void Null_ShouldNotAddBrokenRule_WhenValueIsNull()
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
        public void NullFor_ShouldAddBrokenRule_WhenValueIsNotNull()
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

        [Fact]
        public void Equal_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Equal<string>(null, null);
            });
        }

        [Fact]
        public void Equal_ShouldAddBrokenRule_WhenValueIsNotEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Equal("test")
                .Build();

            // act
            var result = validator.Validate("hello");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Equal", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must equal 'test'.", brokenRule.Message);
        }

        [Fact]
        public void Equal_ShouldAddBrokenRule_WhenValueIsNotNullAndOtherIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Equal(null)
                .Build();

            // act
            var result = validator.Validate("hello");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Equal", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must equal 'null'.", brokenRule.Message);
        }

        [Fact]
        public void Equal_ShouldNotAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Equal("test")
                .Build();

            // act
            var result = validator.Validate("test");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Equal_ShouldNotAddBrokenRule_WhenBothValuesAreNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Equal(null)
                .Build();

            // act
            var result = validator.Validate(null);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Equal_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Equal("test", "test")
                .Build();

            // act
            var result = validator.Validate("hello");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Equal_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Equal("test", message: "test")
                .Build();

            // act
            var result = validator.Validate("hello");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void EqualFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.EqualFor<string, string>(null, null, null);
            });
        }

        [Fact]
        public void EqualFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.EqualFor<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void EqualFor_ShouldAddBrokenRule_WhenValueIsNotEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EqualFor(e => e.LastName, "Simpson")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Cruz" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Equal", brokenRule.Rule);
            Assert.Equal("LastName", brokenRule.Key);
            Assert.Equal("Value must equal 'Simpson'.", brokenRule.Message);
        }

        [Fact]
        public void EqualFor_ShouldAddBrokenRule_WhenValueIsNotNullAndOtherIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EqualFor(e => e.LastName, null)
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Cruz" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Equal", brokenRule.Rule);
            Assert.Equal("LastName", brokenRule.Key);
            Assert.Equal("Value must equal 'null'.", brokenRule.Message);
        }

        [Fact]
        public void EqualFor_ShouldNotAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EqualFor(e => e.LastName, "Smith")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Smith" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void EqualFor_ShouldNotAddBrokenRule_WhenBothValuesAreNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EqualFor(e => e.LastName, null)
                .Build();

            // act
            var result = validator.Validate(new Employee());

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void EqualFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EqualFor(e => e.LastName, "test", "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void EqualFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EqualFor(e => e.LastName, "test", message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void NotEqual_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.NotEqual<string>(null, null);
            });
        }

        [Fact]
        public void NotEqual_ShouldAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEqual("test")
                .Build();

            // act
            var result = validator.Validate("test");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotEqual", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must not equal 'test'.", brokenRule.Message);
        }

        [Fact]
        public void NotEqual_ShouldNotAddBrokenRule_WhenValueIsNotEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEqual("test")
                .Build();

            // act
            var result = validator.Validate("hello");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void NotEqual_ShouldAddBrokenRule_WhenBothValuesAreNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEqual(null)
                .Build();

            // act
            var result = validator.Validate(null);

            // assert
            Assert.NotEmpty(result.BrokenRules);
        }

        [Fact]
        public void NotEqual_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEqual("test", "test")
                .Build();

            // act
            var result = validator.Validate("test");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void NotEqual_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEqual("test", message: "test")
                .Build();

            // act
            var result = validator.Validate("test");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void NotEqualFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.NotEqualFor<string, string>(null, null, null);
            });
        }

        [Fact]
        public void NotEqualFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.NotEqualFor<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void NotEqualFor_ShouldAddBrokenRule_WhenValueIsEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEqualFor(e => e.Salary, 50000)
                .Build();

            // act
            var result = validator.Validate(new Employee { Salary = 50000 });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NotEqual", brokenRule.Rule);
            Assert.Equal("Salary", brokenRule.Key);
            Assert.Equal("Value must not equal '50000'.", brokenRule.Message);
        }

        [Fact]
        public void NotEqualFor_ShouldNotAddBrokenRule_WhenValueIsNotEqual()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEqualFor(e => e.Salary, 50000)
                .Build();

            // act
            var result = validator.Validate(new Employee { Salary = 60000 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void NotEqualFor_ShouldAddBrokenRule_WhenBothValuesAreNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEqualFor(e => e.FirstName, null)
                .Build();

            // act
            var result = validator.Validate(new Employee());

            // assert
            Assert.NotEmpty(result.BrokenRules);
        }

        [Fact]
        public void NotEqualFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEqualFor(e => e.LastName, "Simpson", "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Simpson" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void NotEqualFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEqualFor(e => e.LastName, "Simpson", message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Simpson" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
    }
}