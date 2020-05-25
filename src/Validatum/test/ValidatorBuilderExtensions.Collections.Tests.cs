using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Validatum.Tests
{
    public class ValidatorBuilderCollectionExtensionsTests
    {
        [Fact]
        public void Count_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Count<string>(null, 0);
            });
        }

        [Fact]
        public void Count_ThrowsException_WhenCountLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("count", () =>
            {
                ValidatorBuilderExtensions.Count<string>(new ValidatorBuilder<IEnumerable<string>>(), -1);
            });
        }

        [Fact]
        public void Count_ShouldAddBrokenRule_WhenCollectionCount_LessThanCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .Count(1)
                .Build();

            // act
            var result = validator.Validate(new List<string>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Count", brokenRule.Rule);
            Assert.Equal("IEnumerable<String>", brokenRule.Key);
            Assert.Equal("Collection count must equal 1.", brokenRule.Message);
        }

        [Fact]
        public void Count_ShouldAddBrokenRule_WhenCollectionCount_GreaterThanCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .Count(1)
                .Build();

            // act
            var result = validator.Validate(new List<string>(new[] { "test", "hello" }));
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Count", brokenRule.Rule);
            Assert.Equal("IEnumerable<String>", brokenRule.Key);
            Assert.Equal("Collection count must equal 1.", brokenRule.Message);
        }

        [Fact]
        public void Count_ShouldNotAddBrokenRule_WhenCollectionCount_EqualToCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .Count(1)
                .Build();

            // act
            var result = validator.Validate(new[] { "hello" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Count_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .Count(1, "test")
                .Build();

            // act
            var result = validator.Validate(new List<string>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Count_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .Count(1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new List<string>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void CountFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Count<string, string>(null, null, 0);
            });
        }

        [Fact]
        public void CountFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Count<string, string>(new ValidatorBuilder<string>(), null, 0);
            });
        }

        [Fact]
        public void CountFor_ThrowsException_WhenCountLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("count", () =>
            {
                ValidatorBuilderExtensions.Count<Employee, string>(new ValidatorBuilder<Employee>(), e => e.Skills, -1);
            });
        }

        [Fact]
        public void CountFor_ShouldAddBrokenRule_WhenCollectionCount_LessThanCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Count(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = Array.Empty<string>() });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Count", brokenRule.Rule);
            Assert.Equal("Skills", brokenRule.Key);
            Assert.Equal("Collection count must equal 1.", brokenRule.Message);
        }

        [Fact]
        public void CountFor_ShouldAddBrokenRule_WhenCollectionCount_GreaterThanCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Count(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "test", "hello" } });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Count", brokenRule.Rule);
            Assert.Equal("Skills", brokenRule.Key);
            Assert.Equal("Collection count must equal 1.", brokenRule.Message);
        }

        [Fact]
        public void CountFor_ShouldNotAddBrokenRule_WhenCollectionCount_EqualToCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Count(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "hello" } });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void CountFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Count(e => e.Skills, 1, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = Array.Empty<string>() });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void CountFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Count(e => e.Skills, 1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = Array.Empty<string>() });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void MinCount_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.MinCount<string>(null, 0);
            });
        }

        [Fact]
        public void MinCount_ThrowsException_WhenCountLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("count", () =>
            {
                ValidatorBuilderExtensions.MinCount<string>(new ValidatorBuilder<IEnumerable<string>>(), -1);
            });
        }

        [Fact]
        public void MinCount_ShouldAddBrokenRule_WhenCollectionCount_LessThanCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MinCount(1)
                .Build();

            // act
            var result = validator.Validate(new List<string>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MinCount", brokenRule.Rule);
            Assert.Equal("IEnumerable<String>", brokenRule.Key);
            Assert.Equal("Collection count must be at least 1.", brokenRule.Message);
        }

        [Fact]
        public void MinCount_ShouldNotAddBrokenRule_WhenCollectionCount_EqualToMinCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MinCount(1)
                .Build();

            // act
            var result = validator.Validate(new[] { "hello" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MinCount_ShouldNotAddBrokenRule_WhenCollectionCount_GreaterThanMinCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MinCount(1)
                .Build();

            // act
            var result = validator.Validate(new[] { "hello", "test" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MinCount_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MinCount(1, "test")
                .Build();

            // act
            var result = validator.Validate(new List<string>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void MinCount_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MinCount(1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new List<string>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void MinCountFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.MinCount<string, string>(null, null, 0);
            });
        }

        [Fact]
        public void MinCountFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.MinCount<string, string>(new ValidatorBuilder<string>(), null, 0);
            });
        }

        [Fact]
        public void MinCountFor_ThrowsException_WhenCountLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("count", () =>
            {
                ValidatorBuilderExtensions.MinCount<Employee, string>(new ValidatorBuilder<Employee>(), e => e.Skills, -1);
            });
        }

        [Fact]
        public void MinCountFor_ShouldAddBrokenRule_WhenCollectionCount_LessThanMinCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinCount(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = Array.Empty<string>() });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MinCount", brokenRule.Rule);
            Assert.Equal("Skills", brokenRule.Key);
            Assert.Equal("Collection count must be at least 1.", brokenRule.Message);
        }

        [Fact]
        public void MinCountFor_ShouldNotAddBrokenRule_WhenCollectionCount_EqualToMinCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinCount(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "hello" } });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MinCountFor_ShouldNotAddBrokenRule_WhenCollectionCount_GreaterThanMinCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinCount(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "hello", "test" } });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MinCountFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinCount(e => e.Skills, 1, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = Array.Empty<string>() });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void MinCountFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MinCount(e => e.Skills, 1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = Array.Empty<string>() });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void MaxCount_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.MaxCount<string>(null, 0);
            });
        }

        [Fact]
        public void MaxCount_ThrowsException_WhenCountLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("count", () =>
            {
                ValidatorBuilderExtensions.MaxCount<string>(new ValidatorBuilder<IEnumerable<string>>(), -1);
            });
        }

        [Fact]
        public void MaxCount_ShouldAddBrokenRule_WhenCollectionCount_GreaterThanCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MaxCount(1)
                .Build();

            // act
            var result = validator.Validate(new List<string>(new[] { "test", "hello" }));
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MaxCount", brokenRule.Rule);
            Assert.Equal("IEnumerable<String>", brokenRule.Key);
            Assert.Equal("Collection count cannot be greater than 1.", brokenRule.Message);
        }

        [Fact]
        public void MaxCount_ShouldNotAddBrokenRule_WhenCollectionCount_EqualToMaxCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MaxCount(1)
                .Build();

            // act
            var result = validator.Validate(new[] { "hello" });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MaxCount_ShouldNotAddBrokenRule_WhenCollectionCount_LessThanMaxCount()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MaxCount(1)
                .Build();

            // act
            var result = validator.Validate(Array.Empty<string>());

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MaxCount_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MaxCount(1, "test")
                .Build();

            // act
            var result = validator.Validate(new List<string>(new[] { "test", "hello" }));
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void MaxCount_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<string>>()
                .MaxCount(1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new List<string>(new[] { "test", "hello" }));
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void MaxCountFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.MaxCount<string, string>(null, null, 0);
            });
        }

        [Fact]
        public void MaxCountFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.MaxCount<string, string>(new ValidatorBuilder<string>(), null, 0);
            });
        }

        [Fact]
        public void MaxCountFor_ThrowsException_WhenCountLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>("count", () =>
            {
                ValidatorBuilderExtensions.MaxCount<Employee, string>(new ValidatorBuilder<Employee>(), e => e.Skills, -1);
            });
        }

        [Fact]
        public void MaxCountFor_ShouldAddBrokenRule_WhenCollectionCount_GreaterThanMaxCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxCount(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "test", "hello" } });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("MaxCount", brokenRule.Rule);
            Assert.Equal("Skills", brokenRule.Key);
            Assert.Equal("Collection count cannot be greater than 1.", brokenRule.Message);
        }

        [Fact]
        public void MaxCountFor_ShouldNotAddBrokenRule_WhenCollectionCount_EqualToMaxCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxCount(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "hello" } });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MaxCountFor_ShouldNotAddBrokenRule_WhenCollectionCount_LessThanMaxCount()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxCount(e => e.Skills, 1)
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = Array.Empty<string>() });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MaxCountFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxCount(e => e.Skills, 1, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "test", "hello" } });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void MaxCountFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .MaxCount(e => e.Skills, 1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "test", "hello" } });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void Contains_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Contains<int>(null, 0);
            });
        }

        [Fact]
        public void Contains_ThrowsException_WhenItemIsNull()
        {
            Assert.Throws<ArgumentNullException>("item", () =>
            {
                ValidatorBuilderExtensions.Contains<string>(new ValidatorBuilder<IEnumerable<string>>(), null);
            });
        }

        [Fact]
        public void Contains_ShouldAddBrokenRule_WhenCollection_DoesNotContainItem()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<int>>()
                .Contains(1)
                .Build();

            // act
            var result = validator.Validate(new List<int>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Contains", brokenRule.Rule);
            Assert.Equal("IEnumerable<Int32>", brokenRule.Key);
            Assert.Equal("Collection must contain item '1'.", brokenRule.Message);
        }

        [Fact]
        public void Contains_ShouldNotAddBrokenRule_WhenCollection_ContainsItem()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<int>>()
                .Contains(2)
                .Build();

            // act
            var result = validator.Validate(new[] { 1, 2, 3 });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void Contains_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<int>>()
                .Contains(1, "test")
                .Build();

            // act
            var result = validator.Validate(new List<int>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void Contains_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<IEnumerable<int>>()
                .Contains(1, message: "test")
                .Build();

            // act
            var result = validator.Validate(new List<int>());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Contains<Employee, string>(null, null, null);
            });
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Contains<Employee, string>(new ValidatorBuilder<Employee>(), null, null);
            });
        }

        [Fact]
        public void ContainsFor_ThrowsException_WhenItemIsNull()
        {
            Assert.Throws<ArgumentNullException>("item", () =>
            {
                ValidatorBuilderExtensions.Contains<Employee, string>(new ValidatorBuilder<Employee>(), e => e.Skills, null);
            });
        }

        [Fact]
        public void ContainsFor_ShouldAddBrokenRule_WhenCollection_DoesNotContainItem()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.Skills, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Contains", brokenRule.Rule);
            Assert.Equal("Skills", brokenRule.Key);
            Assert.Equal("Collection must contain item 'test'.", brokenRule.Message);
        }

        [Fact]
        public void ContainsFor_ShouldNotAddBrokenRule_WhenCollection_ContainsItem()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.Skills, "test")
                .Build();

            // act
            var result = validator.Validate(new Employee { Skills = new[] { "test" } });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void ContainsFor_ShouldPassKeyToBrokenRule_WhenKeyProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.Skills, "test", "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Key);
        }

        [Fact]
        public void ContainsFor_ShouldPassMessageToBrokenRule_WhenMessageProvided()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Contains(e => e.Skills, "test", message: "test")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal("test", brokenRule.Message);
        }
    }
}