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
                ValidatorBuilderExtensions.EmptyFor<string>(null, null);
            });
        }

        [Fact]
        public void EmptyFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.EmptyFor<string>(new ValidatorBuilder<string>(), null);
            });
        }
        
        [Fact]
        public void EmptyFor_ShouldAddBrokenRule_WhenValueIsNotEmpty()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EmptyFor(e => e.Employer.Name)
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
                .EmptyFor(e => e.Employer.Name)
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
                .EmptyFor(e => e.Employer.Name)
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
                .EmptyFor(e => e.Employer.Name)
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
                .EmptyFor(e => e.Employer.Name, key: "test")
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
                .EmptyFor(e => e.Employer.Name, message: "test")
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
                ValidatorBuilderExtensions.RegexFor<string>(null, null, null);
            });
        }

        [Fact]
        public void RegexFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.RegexFor<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void RegexFor_ThrowsException_WhenPatternIsNull()
        {
            Assert.Throws<ArgumentException>("pattern", () =>
            {
                ValidatorBuilderExtensions.RegexFor<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void RegexFor_ThrowsException_WhenPatternIsEmpty()
        {
            Assert.Throws<ArgumentException>("pattern", () =>
            {
                ValidatorBuilderExtensions.RegexFor<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, string.Empty);
            });
        }

        [Fact]
        public void RegexFor_ShouldAddBrokenRule_WhenValueDoesNotMatchPattern()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .RegexFor(e => e.FirstName, "a{1,4}")
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
                .RegexFor(e => e.FirstName, "a{1,4}")
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
                .RegexFor(e => e.FirstName, "a{1,4}")
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
                .RegexFor(e => e.FirstName, "a{1,4}", "test")
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
                .RegexFor(e => e.FirstName, "a{1,4}", message: "test")
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
                .RegexFor(e => e.FirstName, "a{1,4}", RegexOptions.IgnoreCase)
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
                ValidatorBuilderExtensions.StartsWithFor<string>(null, null, null);
            });
        }

        [Fact]
        public void StartsWithFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.StartsWithFor<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void StartsWithFor_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.StartsWithFor<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void StartsWithFor_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.StartsWithFor<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, string.Empty);
            });
        }

        [Fact]
        public void StartsWithFor_ShouldAddBrokenRule_WhenValueDoesNotStartWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .StartsWithFor(e => e.LastName, "Data")
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
                .StartsWithFor(e => e.LastName, "Data")
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
                .StartsWithFor(e => e.LastName, "Data")
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
                .StartsWithFor(e => e.LastName, "Pica", "test")
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
                .StartsWithFor(e => e.LastName, "Pica", message: "test")
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
                ValidatorBuilderExtensions.EndsWithFor<string>(null, null, null);
            });
        }

        [Fact]
        public void EndsWithFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.EndsWithFor<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void EndsWithFor_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.EndsWithFor<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void EndsWithFor_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.EndsWithFor<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, string.Empty);
            });
        }

        [Fact]
        public void EndsWithFor_ShouldAddBrokenRule_WhenValueDoesNotEndWithValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .EndsWithFor(e => e.LastName, "Data")
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
                .EndsWithFor(e => e.LastName, "Data")
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
                .EndsWithFor(e => e.LastName, "man")
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
                .EndsWithFor(e => e.LastName, "Pica", "test")
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
                .EndsWithFor(e => e.LastName, "Pica", message: "test")
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
                ValidatorBuilderExtensions.ContainsFor<string>(null, null, null);
            });
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.ContainsFor<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenValueIsNull()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.ContainsFor<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, null);
            });
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () =>
            {
                ValidatorBuilderExtensions.ContainsFor<Employee>(new ValidatorBuilder<Employee>(), e => e.FirstName, string.Empty);
            });
        }

        [Fact]
        public void ContainsFor_ShouldAddBrokenRule_WhenValueDoesNotContainValue()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .ContainsFor(e => e.LastName, "Data")
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
                .ContainsFor(e => e.LastName, "Data")
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
                .ContainsFor(e => e.LastName, "man")
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
                .ContainsFor(e => e.LastName, "Pica", "test")
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
                .ContainsFor(e => e.LastName, "Pica", message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
    }
}