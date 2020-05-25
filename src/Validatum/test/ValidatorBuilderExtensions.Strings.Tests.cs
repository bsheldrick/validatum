using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Validatum.Tests
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
        public void NotEmpty_ShouldAddBrokenRule_WhenValueIsEmpty()
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
        public void NotEmpty_ShouldNotAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .NotEmpty()
                .Build();

            // act
            var result = validator.Validate(null);

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void NotEmpty_ShouldNotAddBrokenRule_WhenValueIsNotEmpty()
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
                ValidatorBuilderExtensions.NotEmpty<string>(null, null);
            });
        }

        [Fact]
        public void NotEmptyFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.NotEmpty<string>(new ValidatorBuilder<string>(), null);
            });
        }
        
        [Fact]
        public void NotEmptyFor_ShouldAddBrokenRule_WhenValueIsEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmpty(e => e.Employer.Name)
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
        public void NotEmptyFor_ShouldNotAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmpty(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5 }
            });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void NotEmptyFor_ShouldNotAddBrokenRule_WhenTargetIsNotNullOrEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmpty(e => e.Employer.Name)
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
        public void NotEmptyFor_ShouldUseExpressionMemberName_AsKeyInBrokenRule_WhenKeyNotProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .NotEmpty(e => e.Employer.Name)
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
                .NotEmpty(e => e.Employer.Name, key: "test")
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
                .NotEmpty(e => e.Employer.Name, message: "test")
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
        public void Empty_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Empty(null);
            });
        }

        [Fact]
        public void Empty_ShouldAddBrokenRule_WhenValueIsNotEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Empty()
                .Build();

            // act
            var result = validator.Validate("test");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Empty", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be empty.", brokenRule.Message);
        }

        [Fact]
        public void Empty_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Empty()
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Empty", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be empty.", brokenRule.Message);
        }

        [Fact]
        public void Empty_ShouldNotAddBrokenRule_WhenValueIsEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Empty()
                .Build();

            // act
            var result = validator.Validate("");

            // assert
            Assert.Empty(result.BrokenRules);
        }
        
        [Fact]
        public void Empty_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Empty("test")
                .Build();

            // act
            var result = validator.Validate("test");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Empty_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Empty(message: "test")
                .Build();

            // act
            var result = validator.Validate("test");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void EmptyFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Empty<string>(null, null);
            });
        }

        [Fact]
        public void EmptyFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Empty<string>(new ValidatorBuilder<string>(), null);
            });
        }
        
        [Fact]
        public void EmptyFor_ShouldAddBrokenRule_WhenValueIsNotEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Empty(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "test" }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Empty", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
        }

        [Fact]
        public void EmptyFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Empty(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = null }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Empty", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
        }
        
        [Fact]
        public void EmptyFor_ShouldNotAddBrokenRule_WhenTargetIsEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Empty(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "" }
            });

            // assert
            Assert.True(result.IsValid);
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void EmptyFor_ShouldUseExpressionMemberName_AsKeyInBrokenRule_WhenKeyNotProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Empty(e => e.Employer.Name)
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "test" }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Empty", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
        }

        [Fact]
        public void EmptyFor_ShouldUseKey_AsKeyInBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Empty(e => e.Employer.Name, key: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "test" }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void EmptyFor_ShouldUseMessage_AsMessageInBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Empty(e => e.Employer.Name, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee 
            {
                FirstName = "John",
                Employer = new Company { Id = 5, Name = "test" }
            });
            var brokenRule = result.BrokenRules.LastOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Regex_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Regex(null, null);
            });
        }

        [Fact]
        public void Regex_ThrowsException_WhenPatternIsNull()
        {
            Assert.Throws<ArgumentException>("pattern", () =>
            {
                ValidatorBuilderExtensions.Regex(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void Regex_ThrowsException_WhenPatternIsEmpty()
        {
            Assert.Throws<ArgumentException>("pattern", () =>
            {
                ValidatorBuilderExtensions.Regex(new ValidatorBuilder<string>(), string.Empty);
            });
        }

        [Fact]
        public void Regex_ShouldAddBrokenRule_WhenValueDoesNotMatchPattern()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Regex("a{1,4}")
                .Build();

            // act
            var result = validator.Validate("1234");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Regex", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must match pattern.", brokenRule.Message);
        }

        [Fact]
        public void Regex_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Regex("a{1,4}")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Regex", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must match pattern.", brokenRule.Message);
        }

        [Fact]
        public void Regex_ShouldNotAddBrokenRule_WhenValueDoesMatchPattern()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Regex("a{1,4}")
                .Build();

            // act
            var result = validator.Validate("abcd");

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Regex_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Regex("a{1,4}", "test")
                .Build();

            // act
            var result = validator.Validate("test");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Regex_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Regex("a{1,4}", message: "test")
                .Build();

            // act
            var result = validator.Validate("test");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Regex_ShouldBeCallable_WithRegexOptions()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Regex("a{1,4}", RegexOptions.IgnoreCase)
                .Build();

            // act
            var result = validator.Validate("Absolutely.");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void RegexFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Regex<string>(null, null, null);
            });
        }

        [Fact]
        public void RegexFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Regex<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void RegexFor_ThrowsException_WhenPatternIsNull()
        {
            Assert.Throws<ArgumentException>("pattern", () =>
            {
                ValidatorBuilderExtensions.Regex<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void RegexFor_ThrowsException_WhenPatternIsEmpty()
        {
            Assert.Throws<ArgumentException>("pattern", () =>
            {
                ValidatorBuilderExtensions.Regex<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, string.Empty);
            });
        }

        [Fact]
        public void RegexFor_ShouldAddBrokenRule_WhenValueDoesNotMatchPattern()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Regex(e => e.FirstName, "a{1,4}")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "John" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Regex", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must match pattern.", brokenRule.Message);
        }

        [Fact]
        public void RegexFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Regex(e => e.FirstName, "a{1,4}")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Simpson" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Regex", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must match pattern.", brokenRule.Message);
        }

        [Fact]
        public void RegexFor_ShouldNotAddBrokenRule_WhenValueDoesMatchPattern()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Regex(e => e.FirstName, "a{1,4}")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Anna" });

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void RegexFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Regex(e => e.FirstName, "a{1,4}", "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "John" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void RegexFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Regex(e => e.FirstName, "a{1,4}", message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "John" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void RegexFor_ShouldBeCallable_WithRegexOptions()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Regex(e => e.FirstName, "a{1,4}", RegexOptions.IgnoreCase)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Alex" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void StartsWith_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.StartsWith(null, null);
            });
        }

        [Fact]
        public void StartsWith_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.StartsWith(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void StartsWith_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.StartsWith(new ValidatorBuilder<string>(), string.Empty);
            });
        }

        [Fact]
        public void StartsWith_ShouldAddBrokenRule_WhenValueDoesNotStartWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .StartsWith("star")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("StartsWith", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must start with 'star'.", brokenRule.Message);
        }

        [Fact]
        public void StartsWith_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .StartsWith("star")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("StartsWith", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must start with 'star'.", brokenRule.Message);
        }

        [Fact]
        public void StartsWith_ShouldNotAddBrokenRule_WhenValueDoesStartWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .StartsWith("star")
                .Build();

            // act
            var result = validator.Validate("start");

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void StartsWith_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .StartsWith("star", "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void StartsWith_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .StartsWith("star", message: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void StartsWithFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.StartsWith<string>(null, null, null);
            });
        }

        [Fact]
        public void StartsWithFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.StartsWith<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void StartsWithFor_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.StartsWith<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void StartsWithFor_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.StartsWith<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, string.Empty);
            });
        }

        [Fact]
        public void StartsWithFor_ShouldAddBrokenRule_WhenValueDoesNotStartWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .StartsWith(e => e.LastName, "Data")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Picard" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("StartsWith", brokenRule.Rule);
            Assert.Equal("LastName", brokenRule.Key);
            Assert.Equal("Value must start with 'Data'.", brokenRule.Message);
        }

        [Fact]
        public void StartsWithFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .StartsWith(e => e.LastName, "Data")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("StartsWith", brokenRule.Rule);
            Assert.Equal("LastName", brokenRule.Key);
            Assert.Equal("Value must start with 'Data'.", brokenRule.Message);
        }

        [Fact]
        public void StartsWithFor_ShouldNotAddBrokenRule_WhenValueDoesStartWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .StartsWith(e => e.LastName, "Data")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Dataman" });

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void StartsWithFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .StartsWith(e => e.LastName, "Pica", "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void StartsWithFor_ShouldPassMessageToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .StartsWith(e => e.LastName, "Pica", message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void EndsWith_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.EndsWith(null, null);
            });
        }

        [Fact]
        public void EndsWith_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.EndsWith(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void EndsWith_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.EndsWith(new ValidatorBuilder<string>(), string.Empty);
            });
        }

        [Fact]
        public void EndsWith_ShouldAddBrokenRule_WhenValueDoesNotEndWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .EndsWith("star")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("EndsWith", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must end with 'star'.", brokenRule.Message);
        }

        [Fact]
        public void EndsWith_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .EndsWith("star")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("EndsWith", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must end with 'star'.", brokenRule.Message);
        }

        [Fact]
        public void EndsWith_ShouldNotAddBrokenRule_WhenValueDoesEndWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .EndsWith("tar")
                .Build();

            // act
            var result = validator.Validate("star");

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void EndsWith_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .EndsWith("star", "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void EndsWith_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .EndsWith("star", message: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void EndsWithFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.EndsWith<string>(null, null, null);
            });
        }

        [Fact]
        public void EndsWithFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.EndsWith<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void EndsWithFor_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.EndsWith<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void EndsWithFor_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.EndsWith<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, string.Empty);
            });
        }

        [Fact]
        public void EndsWithFor_ShouldAddBrokenRule_WhenValueDoesNotEndWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EndsWith(e => e.LastName, "Data")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Picard" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("EndsWith", brokenRule.Rule);
            Assert.Equal("LastName", brokenRule.Key);
            Assert.Equal("Value must end with 'Data'.", brokenRule.Message);
        }

        [Fact]
        public void EndsWithFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EndsWith(e => e.LastName, "Data")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("EndsWith", brokenRule.Rule);
            Assert.Equal("LastName", brokenRule.Key);
            Assert.Equal("Value must end with 'Data'.", brokenRule.Message);
        }

        [Fact]
        public void EndsWithFor_ShouldNotAddBrokenRule_WhenValueDoesEndWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EndsWith(e => e.LastName, "man")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Dataman" });

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void EndsWithFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EndsWith(e => e.LastName, "Pica", "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void EndsWithFor_ShouldPassMessageToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EndsWith(e => e.LastName, "Pica", message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Contains_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Contains(null, null);
            });
        }

        [Fact]
        public void Contains_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.Contains(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void Contains_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.Contains(new ValidatorBuilder<string>(), string.Empty);
            });
        }

        [Fact]
        public void Contains_ShouldAddBrokenRule_WhenValueDoesNotContainValue()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Contains("star")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Contains", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must contain 'star'.", brokenRule.Message);
        }

        [Fact]
        public void Contains_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Contains("star")
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Contains", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must contain 'star'.", brokenRule.Message);
        }

        [Fact]
        public void Contains_ShouldNotAddBrokenRule_WhenValueDoesContainValue()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Contains("gaze")
                .Build();

            // act
            var result = validator.Validate("stargazer");

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Contains_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Contains("star", "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Contains_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Contains("star", message: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Contains<string>(null, "test", null);
            });
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Contains<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.Contains<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.Contains<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, string.Empty);
            });
        }

        [Fact]
        public void ContainsFor_ShouldAddBrokenRule_WhenValueDoesNotContainValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.LastName, "Data")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "Picard" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Contains", brokenRule.Rule);
            Assert.Equal("LastName", brokenRule.Key);
            Assert.Equal("Value must contain 'Data'.", brokenRule.Message);
        }

        [Fact]
        public void ContainsFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.LastName, "Data")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Contains", brokenRule.Rule);
            Assert.Equal("LastName", brokenRule.Key);
            Assert.Equal("Value must contain 'Data'.", brokenRule.Message);
        }

        [Fact]
        public void ContainsFor_ShouldNotAddBrokenRule_WhenValueDoesContainValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.LastName, "man")
                .Build();

            // act
            var result = validator.Validate(new Employee { LastName = "salamander" });

            // assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ContainsFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.LastName, "Pica", "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void ContainsFor_ShouldPassMessageToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.LastName, "Pica", message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Length_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Length(null, 0, 3);
            });
        }

        [Fact]
        public void Length_ThrowsException_WhenMinLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("min", () =>
            {
                ValidatorBuilderExtensions.Length(new ValidatorBuilder<string>(), -1, 3);
            });
        }

        [Fact]
        public void Length_ThrowsException_WhenMinGreaterThanMax()
        {
            Assert.Throws<ArgumentOutOfRangeException>("min", () =>
            {
                ValidatorBuilderExtensions.Length(new ValidatorBuilder<string>(), 3, 2);
            });
        }

        [Fact]
        public void Length_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Length(5, 8)
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Length", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be 5 to 8 characters in length.", brokenRule.Message);
        }
        
        [Fact]
        public void Length_ShouldAddBrokenRule_WhenValueLengthIsLessThanMin()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Length(5, 8)
                .Build();

            // act
            var result = validator.Validate("plan");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Length", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be 5 to 8 characters in length.", brokenRule.Message);
        }

        [Fact]
        public void Length_ShouldAddBrokenRule_WhenValueLengthIsGreaterThanMax()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Length(5, 7)
                .Build();

            // act
            var result = validator.Validate("planetary");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Length", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be 5 to 7 characters in length.", brokenRule.Message);
        }

        [Fact]
        public void Length_ShouldNotAddBrokenRule_WhenValueLengthEqualsMin()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Length(5, 8)
                .Build();

            // act
            var result = validator.Validate("plant");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Length_ShouldNotAddBrokenRule_WhenValueLengthEqualsMax()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Length(5, 7)
                .Build();

            // act
            var result = validator.Validate("planets");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Length_ShouldNotAddBrokenRule_WhenValueLengthBetweenMinAndMax()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Length(5, 7)
                .Build();

            // act
            var result = validator.Validate("planet");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Length_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Length(8, 12, key: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Length_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Length(8, 12, message: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void LengthFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Length<string>(null, null, 0, 3);
            });
        }

        [Fact]
        public void LengthFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Length<string>(new ValidatorBuilder<string>(), null, 0, 3);
            });
        }

        [Fact]
        public void LengthFor_ThrowsException_WhenMinLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("min", () =>
            {
                ValidatorBuilderExtensions.Length(new ValidatorBuilder<Employee>(), e => e.FirstName, -1, 3);
            });
        }

        [Fact]
        public void LengthFor_ThrowsException_WhenMinGreaterThanMax()
        {
            Assert.Throws<ArgumentOutOfRangeException>("min", () =>
            {
                ValidatorBuilderExtensions.Length(new ValidatorBuilder<Employee>(), e => e.FirstName, 3, 2);
            });
        }

        [Fact]
        public void LengthFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Length(e => e.FirstName, 5, 8)
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Length", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be 5 to 8 characters in length.", brokenRule.Message);
        }

        [Fact]
        public void LengthFor_ShouldAddBrokenRule_WhenValueLengthIsLessThanMin()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Length(e => e.FirstName, 5, 8)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "John" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Length", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be 5 to 8 characters in length.", brokenRule.Message);
        }

        [Fact]
        public void LengthFor_ShouldAddBrokenRule_WhenValueLengthIsGreaterThanMax()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Length(e => e.FirstName, 5, 7)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Jonathon" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Length", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must be 5 to 7 characters in length.", brokenRule.Message);
        }

        [Fact]
        public void LengthFor_ShouldNotAddBrokenRule_WhenValueLengthEqualsMin()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Length(e => e.FirstName, 4, 7)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Karl" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LengthFor_ShouldNotAddBrokenRule_WhenValueLengthEqualsMax()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Length(e => e.FirstName, 3, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Lenny" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LengthFor_ShouldNotAddBrokenRule_WhenValueLengthBetweenMinAndMax()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Length(e => e.FirstName, 5, 8)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "William" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void LengthFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Length(e => e.FirstName, 8, 12, key: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "William" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void LengthFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Length(e => e.FirstName, 8, 12, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "William" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void MinLength_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.MinLength(null, 0);
            });
        }

        [Fact]
        public void MinLength_ThrowsException_WhenMinLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("min", () =>
            {
                ValidatorBuilderExtensions.MinLength(new ValidatorBuilder<string>(), -1);
            });
        }

        [Fact]
        public void MinLength_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MinLength(5)
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MinLength", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must have minimum length of 5.", brokenRule.Message);
        }
        
        [Fact]
        public void MinLength_ShouldAddBrokenRule_WhenValueLengthIsLessThanMin()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MinLength(5)
                .Build();

            // act
            var result = validator.Validate("plan");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MinLength", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must have minimum length of 5.", brokenRule.Message);
        }

        [Fact]
        public void MinLength_ShouldNotAddBrokenRule_WhenValueLengthEqualsMin()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MinLength(5)
                .Build();

            // act
            var result = validator.Validate("plant");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MinLength_ShouldNotAddBrokenRule_WhenValueLengthGreaterThanMin()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MinLength(5)
                .Build();

            // act
            var result = validator.Validate("planet");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MinLength_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MinLength(8, key: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void MinLength_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MinLength(8, message: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void MinLengthFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.MinLength<Employee>(null, null, 0);
            });
        }

        [Fact]
        public void MinLengthFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.MinLength(new ValidatorBuilder<Employee>(), null, 0);
            });
        }

        [Fact]
        public void MinLengthFor_ThrowsException_WhenMinLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("min", () =>
            {
                ValidatorBuilderExtensions.MinLength(new ValidatorBuilder<Employee>(), e => e.FirstName, -1);
            });
        }

        [Fact]
        public void MinLengthFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinLength(e => e.FirstName, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MinLength", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must have minimum length of 5.", brokenRule.Message);
        }
        
        [Fact]
        public void MinLengthFor_ShouldAddBrokenRule_WhenValueLengthIsLessThanMin()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinLength(e => e.FirstName, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Bill" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MinLength", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must have minimum length of 5.", brokenRule.Message);
        }

        [Fact]
        public void MinLengthFor_ShouldNotAddBrokenRule_WhenValueLengthEqualsMin()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinLength(e => e.FirstName, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Marge" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MinLengthFor_ShouldNotAddBrokenRule_WhenValueLengthGreaterThanMin()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinLength(e => e.FirstName, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Margory" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MinLengthFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinLength(e => e.FirstName, 8, key: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void MinLengthFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinLength(e => e.FirstName, 8, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void MaxLength_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.MaxLength(null, 0);
            });
        }

        [Fact]
        public void MaxLength_ThrowsException_WhenMaxLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("max", () =>
            {
                ValidatorBuilderExtensions.MaxLength(new ValidatorBuilder<string>(), -1);
            });
        }

        [Fact]
        public void MaxLength_ShouldNotAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MaxLength(5)
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Null(brokenRule);
        }
        
        [Fact]
        public void MaxLength_ShouldAddBrokenRule_WhenValueLengthIsGreaterThanMax()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MaxLength(5)
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MaxLength", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must have maximum length of 5.", brokenRule.Message);
        }

        [Fact]
        public void MaxLength_ShouldNotAddBrokenRule_WhenValueLengthEqualsMax()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MaxLength(5)
                .Build();

            // act
            var result = validator.Validate("plant");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MaxLength_ShouldNotAddBrokenRule_WhenValueLengthLessThanMax()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MaxLength(5)
                .Build();

            // act
            var result = validator.Validate("plan");

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MaxLength_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MaxLength(5, key: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void MaxLength_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .MaxLength(5, message: "test")
                .Build();

            // act
            var result = validator.Validate("planet");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void MaxLengthFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.MaxLength<Employee>(null, null, 0);
            });
        }

        [Fact]
        public void MaxLengthFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.MaxLength(new ValidatorBuilder<Employee>(), null, 0);
            });
        }

        [Fact]
        public void MaxLengthFor_ThrowsException_WhenMaxLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("max", () =>
            {
                ValidatorBuilderExtensions.MaxLength(new ValidatorBuilder<Employee>(), e => e.FirstName, -1);
            });
        }

        [Fact]
        public void MaxLengthFor_ShouldNotAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxLength(e => e.FirstName, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Null(brokenRule);
        }
        
        [Fact]
        public void MaxLengthFor_ShouldAddBrokenRule_WhenValueLengthIsGreaterThanMax()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxLength(e => e.FirstName, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "William" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MaxLength", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value must have maximum length of 5.", brokenRule.Message);
        }

        [Fact]
        public void MaxLengthFor_ShouldNotAddBrokenRule_WhenValueLengthEqualsMax()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxLength(e => e.FirstName, 5)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Marge" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MaxLengthFor_ShouldNotAddBrokenRule_WhenValueLengthLessThanMax()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxLength(e => e.FirstName, 8)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Marge" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MaxLengthFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxLength(e => e.FirstName, 8, key: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "morethan8" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void MaxLengthFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxLength(e => e.FirstName, 8, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "morethan8" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Required_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Required(null);
            });
        }
        
        [Fact]
        public void Required_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Required()
                .Build();

            // act
            var result = validator.Validate(null);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Required", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value is required.", brokenRule.Message);
        }

        [Fact]
        public void Required_ShouldAddBrokenRule_WhenValueIsEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Required()
                .Build();

            // act
            var result = validator.Validate(string.Empty);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Required", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value is required.", brokenRule.Message);
        }

        [Fact]
        public void Required_ShouldAddBrokenRule_WhenValueIsWhiteSpace()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Required()
                .Build();

            // act
            var result = validator.Validate("   ");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Required", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value is required.", brokenRule.Message);
        }

        [Fact]
        public void Required_ShouldNotAddBrokenRule_WhenValueIsProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Required()
                .Build();

            // act
            var result = validator.Validate("test");

            // assert
            Assert.Empty(result.BrokenRules);
        }
        
        [Fact]
        public void Required_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Required("test")
                .Build();

            // act
            var result = validator.Validate(string.Empty);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Required_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Required(message: "test")
                .Build();

            // act
            var result = validator.Validate(string.Empty);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void RequiredFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Required<string>(null, null);
            });
        }

        [Fact]
        public void RequiredFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Required<string>(new ValidatorBuilder<string>(), null);
            });
        }
        
        [Fact]
        public void RequiredFor_ShouldAddBrokenRule_WhenValueIsNull()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Required(e => e.FirstName)
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Required", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value is required.", brokenRule.Message);
        }

        [Fact]
        public void RequiredFor_ShouldAddBrokenRule_WhenValueIsEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Required(e => e.FirstName)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Required", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value is required.", brokenRule.Message);
        }

        [Fact]
        public void RequiredFor_ShouldAddBrokenRule_WhenValueIsWhiteSpace()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Required(e => e.FirstName)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "  " });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Required", brokenRule.Rule);
            Assert.Equal("FirstName", brokenRule.Key);
            Assert.Equal("Value is required.", brokenRule.Message);
        }

        [Fact]
        public void RequiredFor_ShouldNotAddBrokenRule_WhenValueIsProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Required(e => e.FirstName)
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Bob" });

            // assert
            Assert.Empty(result.BrokenRules);
        }
        
        [Fact]
        public void RequiredFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Required(e => e.FirstName, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void RequiredFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Required(e => e.FirstName, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Email_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Email(null);
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("@")]
        [InlineData("b@")]
        [InlineData("@x")]
        [InlineData("notanemail.com")]
        [InlineData("@nocharsbeforeat.com")]
        [InlineData("has@two@ats.com")]
        [InlineData("double@periods..com")]
        [InlineData("no@white space.com")]
        public void Email_ShouldAddBrokenRule_WhenValueIsInvalidEmail(string email)
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Email()
                .Build();

            // act
            var result = validator.Validate(email);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Email", brokenRule.Rule);
            Assert.Equal("String", brokenRule.Key);
            Assert.Equal("Value must be a valid email.", brokenRule.Message);
        }

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("test@example.com.au")]
        [InlineData("test.1@example.com")]
        [InlineData("test+name@example.com")]
        public void Email_ShouldNotAddBrokenRule_WhenValueIsValidEmail(string email)
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Email()
                .Build();

            // act
            var result = validator.Validate(email);

            // assert
            Assert.Empty(result.BrokenRules);
        }
        
        [Fact]
        public void Email_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Email("test")
                .Build();

            // act
            var result = validator.Validate("nope");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Email_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .Email(message: "test")
                .Build();

            // act
            var result = validator.Validate("nope");
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void EmailFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Email<string>(null, null);
            });
        }

        [Fact]
        public void EmailFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Email<string>(new ValidatorBuilder<string>(), null);
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("@")]
        [InlineData("b@")]
        [InlineData("@x")]
        [InlineData("notanemail.com")]
        [InlineData("@nocharsbeforeat.com")]
        [InlineData("has@two@ats.com")]
        [InlineData("double@periods..com")]
        [InlineData("no@white space.com")]
        public void EmailFor_ShouldAddBrokenRule_WhenValueIsInvalidEmail(string email)
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Email(e => e.Email)
                .Build();

            // act
            var result = validator.Validate(new Employee { Email = email });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Email", brokenRule.Rule);
            Assert.Equal("Email", brokenRule.Key);
            Assert.Equal("Value must be a valid email.", brokenRule.Message);
        }

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("test@example.com.au")]
        [InlineData("test.1@example.com")]
        [InlineData("test+name@example.com")]
        public void EmailFor_ShouldNotAddBrokenRule_WhenValueIsValidEmail(string email)
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Email(e => e.Email)
                .Build();

            // act
            var result = validator.Validate(new Employee { Email = email });

            // assert
            Assert.Empty(result.BrokenRules);
        }
        
        [Fact]
        public void EmailFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Email(e => e.Email, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void EmailFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Email(e => e.Email, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
    }
}