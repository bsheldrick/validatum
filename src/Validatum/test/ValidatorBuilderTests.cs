using System;
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
        public void Build_ShouldPopulateLabelOnValidator()
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
    }
}
