using System;
using System.Linq;
using Xunit;

namespace Validatum.Tests
{
    public class ValidatorBuilderTests
    {
        [Fact]
        public void Build_ReturnsValidatorInstance()
        {
            // arrange
            var builder = new ValidatorBuilder<Employee>();

            // act
            var validator = builder.Build();

            // assert
            Assert.NotNull(validator);
        }

        [Fact]
        public void Build_ShouldSetLabelToTypeName_WhenLabelNotProvided()
        {
            // arrange
            var builder = new ValidatorBuilder<Employee>();

            // act
            var validator = builder.Build();

            // assert
            Assert.Equal(typeof(Employee).Name, validator.Label);
        }

        [Fact]
        public void Build_ShouldSetLabelOnValidator_WhenLabelProvided()
        {
            // arrange
            var builder = new ValidatorBuilder<Employee>();

            // act
            var validator = builder.Build("test");

            // assert
            Assert.Equal("test", validator.Label);
        }

        [Fact]
        public void Build_ShouldOnlyBuildTheDelegateChainOnce()
        {
            // arrange
            var builder = new ValidatorBuilder<Employee>();

            // act
            var validator1 = builder.Build("test1");
            var validator2 = builder.Build("test2");

            // assert
            Assert.Same(validator1, validator2);
            Assert.Equal("test1", validator2.Label);
            Assert.Equal(validator1.Label, validator2.Label);
        }

        [Fact]
        public void With_ShouldThrowException_WhenValidatorDelegateIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                new ValidatorBuilder<string>()
                    .With(null)
                    .Build();
            });
        }

        [Fact]
        public void With_ShouldAddDelegateToChain()
        {
            // arrange
            int callCount = 0;
            var builder = new ValidatorBuilder<string>()
                .With(ctx =>
                {
                    callCount++;
                })
                .With(ctx =>
                {
                    callCount++;
                });
            var validator = builder.Build();

            // act
            var result = validator.Validate("test");

            // assert
            Assert.Equal(2, callCount);
        }

        [Fact]
        public void With_ShouldThrowValidationException_WhenThrowWhenInvalidIsTrue()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .With(ctx =>
                {
                    ctx.AddBrokenRule("test1", "test1", "test1");
                })
                .With(ctx =>
                {
                    ctx.AddBrokenRule("test2", "test2", "test2");
                })
                .Build();

            // assert
            Assert.Throws<ValidationException>(() =>
            {
                try
                {
                    // act
                    validator.Validate("test", new ValidationOptions { ThrowWhenInvalid = true });
                }
                catch (ValidationException ex)
                {
                    Assert.Equal(2, ex.BrokenRules.Count());
                    throw ex;
                }
            });
        }

        [Fact]
        public void With_ShouldHaltExecution_WhenStopWhenInvalidIsTrue()
        {
            var secondFuncCalled = false;

            // arrange
            var validator = new ValidatorBuilder<string>()
                .With(ctx =>
                {
                    ctx.AddBrokenRule("test1", "test1", "test1");
                })
                .With(ctx =>
                {
                    secondFuncCalled = true;
                    ctx.AddBrokenRule("test2", "test2", "test2");
                })
                .Build();

            // assert
            var result = validator.Validate("test", new ValidationOptions { StopWhenInvalid = true });

            // act
            Assert.False(secondFuncCalled);
            Assert.Single(result.BrokenRules);
        }

        [Fact]
        public void With_ShouldHaltExecutionAndThrowException_WhenStopWhenInvalidIsTrue_AndThrowWhenInvalidIsTrue()
        {
            var secondFuncCalled = false;

            // arrange
            var validator = new ValidatorBuilder<string>()
                .With(ctx =>
                {
                    ctx.AddBrokenRule("test1", "test1", "test1");
                })
                .With(ctx =>
                {
                    secondFuncCalled = true;
                    ctx.AddBrokenRule("test2", "test2", "test2");
                })
                .Build();

            // assert
            Assert.Throws<ValidationException>(() =>
            {
                try
                {
                    // act
                    validator.Validate("test", new ValidationOptions { StopWhenInvalid = true, ThrowWhenInvalid = true });
                }
                catch (ValidationException ex)
                {
                    Assert.Single(ex.BrokenRules);
                    throw ex;
                }
            });

            Assert.False(secondFuncCalled);
        }

        [Fact]
        public void With_ShouldNotThrowException_WhenAddBrokenRuleForExceptionIsTrue_AndThrowWhenInvalidIsFalse()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .With(ctx =>
                {
                    throw new FormatException("FormatException test");
                })
                .With(ctx =>
                {
                    throw new InvalidOperationException("InvalidOperationException test");
                })
                .Build();

            // act
            var result = validator.Validate("test", new ValidationOptions { AddBrokenRuleForException = true, ThrowWhenInvalid = false });
            var brokenRule1 = result.BrokenRules.FirstOrDefault();
            var brokenRule2 = result.BrokenRules.LastOrDefault();

            // assert
            Assert.Equal(2, result.BrokenRules.Count());
            Assert.NotNull(brokenRule1);
            Assert.NotNull(brokenRule2);
            Assert.Equal("FormatException", brokenRule1.Rule);
            Assert.Equal("String", brokenRule1.Key);
            Assert.Equal("FormatException test", brokenRule1.Message);
            Assert.Equal("InvalidOperationException", brokenRule2.Rule);
            Assert.Equal("String", brokenRule2.Key);
            Assert.Equal("InvalidOperationException test", brokenRule2.Message);
        }

        [Fact]
        public void With_ShouldThrowValidationException_WhenUnhandledExceptionThrown_AndAddBrokenRuleForExceptionIsFalse()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .With(ctx =>
                {
                    ctx.AddBrokenRule("test", "test", "test");
                })
                .With(ctx =>
                {
                    throw new InvalidOperationException();
                })
                .Build();

            // assert
            Assert.Throws<ValidationException>(() =>
            {
                try
                {
                    // act
                    validator.Validate("test", new ValidationOptions { AddBrokenRuleForException = false });
                }
                catch (ValidationException ex)
                {
                    Assert.IsType<InvalidOperationException>(ex.InnerException);
                    throw ex;
                }
            });
        }
    }
}
