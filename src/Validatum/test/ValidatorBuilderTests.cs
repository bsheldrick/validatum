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
        public void With_ShouldThrowException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                new ValidatorBuilder<string>()
                    .With(null)
                    .Build();
            });
        }

        [Fact]
        public void With_ShouldAddToDelegateChain()
        {
            // arrange
            int callCount = 0;
            var builder = new ValidatorBuilder<string>()
                .With((ctx, next) => 
                {
                    callCount++;
                    next(ctx);
                })
                .With((ctx, next) => 
                {
                    callCount++;
                    next(ctx);
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
                .With((ctx, next) => 
                {
                    ctx.AddBrokenRule("test1", "test1", "test1");
                    next(ctx);
                })
                .With((ctx, next) => 
                {
                    ctx.AddBrokenRule("test2", "test2", "test2");
                    next(ctx);
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
        public void With_ShouldThrowValidationException_WhenUnhandledExceptionThrown()
        {
            // arrange
            var validator = new ValidatorBuilder<string>()
                .With((ctx, next) => 
                {
                    ctx.AddBrokenRule("test", "test", "test");
                    next(ctx);
                })
                .With((ctx, next) => 
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
                    validator.Validate("test");
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
