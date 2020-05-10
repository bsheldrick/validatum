using System;
using System.Linq;
using Xunit;

namespace Validatum.Tests
{
    public class ValidatorBuilderExtensionsTests
    {
        [Fact]
        public void With_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.With<string>(null, null);
            });
        }

        [Fact]
        public void With_ThrowsException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                ValidatorBuilderExtensions.With<string>(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void With_ShouldAddToDelegateChain()
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
        public void For_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.For<string, int>(null, null, null);
            });
        }

        [Fact]
        public void For_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.For<string, int>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void For_ThrowsException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                ValidatorBuilderExtensions.For<Employee, int>(new ValidatorBuilder<Employee>(), e => e.Id, null);
            });
        }

        [Fact]
        public void For_ShouldSetLabel_ToNameOfSelectorTarget()
        {
            // arrange
            string label = null;
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var builder = new ValidatorBuilder<Employee>();
            builder.For(e => e.Manager, v => 
            {
                v.With(ctx => { label = ctx.Label; });
            });

            // act
            var validator = builder.Build();
            validator.Validate(employee);

            // assert
            Assert.Equal("Manager", label);
        }

        [Fact]
        public void For_TargetValidatorValue_ShouldBeValueOfTargetExpression()
        {
            // arrange
            Employee toValidate = null;
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var builder = new ValidatorBuilder<Employee>();
            builder.For(e => e.Manager, v => 
            {
                v.With(ctx => { toValidate = ctx.Value; });
            });

            // act
            var validator = builder.Build();
            validator.Validate(employee);

            // assert
            Assert.Same(employee.Manager, toValidate);
        }

        [Fact]
        public void For_TargetValidatorOptions_ShouldBePassedFromSourceContext()
        {
            // arrange
            ValidationOptions valOptions = null;
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var builder = new ValidatorBuilder<Employee>();
            builder.For(e => e.Manager, v => 
            {
                v.With(ctx => { valOptions = ctx.Options; });
            });

            // act
            var validator = builder.Build();
            var options = new ValidationOptions { StopWhenInvalid = true };
            validator.Validate(employee, options);

            // assert
            Assert.Same(options, valOptions);
        }

        [Fact]
        public void For_ShouldAddBrokenRule_WhenTargetExpression_CannotBeResolved()
        {
            // arrange
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var builder = new ValidatorBuilder<Employee>();
            builder.For(e => e.Employer.Name, v => {});
            var validator = builder.Build();

            // act
            var result = validator.Validate(employee);
            var brokenRule = result.BrokenRules.FirstOrDefault();
            
            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NullReferenceException", brokenRule.Rule);
            Assert.Equal("Employer.Name", brokenRule.Key);
            Assert.Equal("Object reference not set to an instance of an object.", brokenRule.Message);
        }

        [Fact]
        public void For_ShouldNotAddBrokenRule_WhenTargetExpression_CannotBeResolved_AndAddBrokenRuleOnException_IsFalse()
        {
            // arrange
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var builder = new ValidatorBuilder<Employee>();
            builder.For(e => e.Employer.Name, v => {});
            var validator = builder.Build();

            // act
            var options = new ValidationOptions { AddBrokenRuleForException = false };
            var result = validator.Validate(employee, options);
            
            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void For_ValueInContext_ShouldBeResolvedFromExpression()
        {
            // arrange
            string managerName = null;
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var builder = new ValidatorBuilder<Employee>();
            builder.For(e => e.Manager.FirstName, val => 
            {
                val.With(ctx => { managerName = ctx.Value; });
            });
            var validator = builder.Build();

            // act
            var result = validator.Validate(employee);
            
            // assert
            Assert.NotNull(managerName);
            Assert.Equal("Jane", managerName);
        }

        [Fact]
        public void For_ShouldAddBrokenRules_ToResult()
        {
            // arrange
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var validator = new ValidatorBuilder<Employee>()
                .For(e => e.Manager.FirstName, val => 
                {
                    val.With(ctx => 
                    {  
                        ctx.AddBrokenRule("Test", "test", "Test error");
                    });
                })
                .Build();

            // act
            var result = validator.Validate(employee);
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("Test", brokenRule.Rule);
            Assert.Equal("test", brokenRule.Key);
            Assert.Equal("Test error", brokenRule.Message);
        }

        [Fact]
        public void ForEach_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.ForEach<string, string>(null, null, null);
            });
        }

        [Fact]
        public void ForEach_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.ForEach<string, string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void ForEach_ThrowsException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                ValidatorBuilderExtensions.ForEach<string, char>(new ValidatorBuilder<string>(), s => s.ToArray(), null);
            });
        }

        [Fact]
        public void ForEach_ShouldAddBrokenRule_WhenItemsHasInvalidItem()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .ForEach(e => e.Skills, val => val.Contains("p").Length(5))
                .Build();

            // act
            var result = validator.Validate(new Employee
            {
                FirstName = "John",
                Skills = new[] { "csharp", "javascript", "html" }
            });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal(2, result.BrokenRules.Count);
            Assert.Equal("Contains", brokenRule.Rule);
            Assert.Equal("Skills[2]", brokenRule.Key);
            Assert.Equal("Value must contain 'p'.", brokenRule.Message);
        }

        [Fact]
        public void ForEach_ShouldNotAddBrokenRule_WhenAllItemsAreValid()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .ForEach(e => e.Skills, val => val.Contains("p").Length(5))
                .Build();

            // act
            var result = validator.Validate(new Employee
            {
                FirstName = "John",
                Skills = new[] { "csharp", "javascript", "typescript" }
            });

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void ForEach_ShouldAddBrokenRule_WhenTargetExpression_CannotBeResolved()
        {
            // arrange
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var builder = new ValidatorBuilder<Employee>();
            builder.ForEach(e => e.Employer.Employees, v => {});
            var validator = builder.Build();

            // act
            var result = validator.Validate(employee);
            var brokenRule = result.BrokenRules.FirstOrDefault();
            
            // assert
            Assert.NotNull(brokenRule);
            Assert.Equal("NullReferenceException", brokenRule.Rule);
            Assert.Equal("Employer.Employees", brokenRule.Key);
            Assert.Equal("Object reference not set to an instance of an object.", brokenRule.Message);
        }

        [Fact]
        public void ForEach_ShouldNotAddBrokenRule_WhenTargetExpression_CannotBeResolved_AndAddBrokenRuleOnException_IsFalse()
        {
            // arrange
            var employee = new Employee
            {
                FirstName = "John",
                Manager = new Employee { FirstName = "Jane" }
            };

            var builder = new ValidatorBuilder<Employee>();
            builder.ForEach(e => e.Skills, v => {});
            var validator = builder.Build();

            // act
            var options = new ValidationOptions { AddBrokenRuleForException = false };
            var result = validator.Validate(employee, options);
            
            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void When_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.When<string>(null, null, null);
            });
        }

        [Fact]
        public void When_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("predicate", () =>
            {
                ValidatorBuilderExtensions.When<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void When_ThrowsException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                ValidatorBuilderExtensions.When<Employee>(new ValidatorBuilder<Employee>(), e => true, null);
            });
        }

        [Fact]
        public void When_ShouldExecuteFunction_WhenPredicateIsTrue()
        {
            // arrange
            bool funcCalled = false;
            var employee = new Employee { FirstName = "Ken" };

            // act
            var validator = new ValidatorBuilder<Employee>()
                .When(ctx => ctx.Value.FirstName == "Ken", ctx => 
                {
                    funcCalled = true;
                })
                .Build();
            
            validator.Validate(employee);

            // assert
            Assert.True(funcCalled);
        }

        [Fact]
        public void When_ShouldNotExecuteFunction_WhenPredicateIsFalse()
        {
            // arrange
            bool funcCalled = false;
            var employee = new Employee { FirstName = "Ken" };

            // act
            var validator = new ValidatorBuilder<Employee>()
                .When(ctx => ctx.Value.FirstName == "Kenny", ctx => 
                {
                    funcCalled = true;
                })
                .Build();
            
            validator.Validate(employee);

            // assert
            Assert.False(funcCalled);
        }

        [Fact]
        public void WhenNot_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.WhenNot<string>(null, null, null);
            });
        }

        [Fact]
        public void WhenNot_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("predicate", () =>
            {
                ValidatorBuilderExtensions.WhenNot<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void WhenNot_ThrowsException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                ValidatorBuilderExtensions.WhenNot<Employee>(new ValidatorBuilder<Employee>(), e => true, null);
            });
        }

        [Fact]
        public void WhenNot_ShouldExecuteFunction_WhenPredicateIsFalse()
        {
            // arrange
            bool funcCalled = false;
            var validator = new ValidatorBuilder<Employee>()
                .WhenNot(ctx => ctx.Value.FirstName == "Ken", ctx => 
                {
                    funcCalled = true;
                })
                .Build();
            
            // act
            validator.Validate(new Employee { FirstName = "Kenny" });

            // assert
            Assert.True(funcCalled);
        }

        [Fact]
        public void WhenNot_ShouldNotExecuteFunction_WhenPredicateIsTrue()
        {
            // arrange
            bool funcCalled = false;
            var validator = new ValidatorBuilder<Employee>()
                .WhenNot(ctx => ctx.Value.FirstName == "Ken", ctx => 
                {
                    funcCalled = true;
                })
                .Build();
            
            // act
            validator.Validate(new Employee { FirstName = "Ken" });

            // assert
            Assert.False(funcCalled);
        }

        [Fact]
        public void If_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.If<string>(null, null, null);
            });
        }

        [Fact]
        public void If_ThrowsException_WhenPredicateIsNull()
        {
            Assert.Throws<ArgumentNullException>("predicate", () =>
            {
                ValidatorBuilderExtensions.If<string>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void If_ThrowsException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                ValidatorBuilderExtensions.If<string>(new ValidatorBuilder<string>(), ctx => true, null);
            });
        }

        [Fact]
        public void If_ShouldExecuteBuilderFunction_WhenPredicateIsTrue()
        {
            // arrange
            bool funcCalled = false;
            var validator = new ValidatorBuilder<bool>()
                .If(ctx => ctx.Value, v =>
                {
                    v.With(ctx => { funcCalled = true; });
                })
                .Build();
            
            // act
            validator.Validate(true);
            
            // assert
            Assert.True(funcCalled);
        }

        [Fact]
        public void If_ShouldExecuteBuilderFunction_WhenPredicateIsFalse()
        {
            // arrange
            bool funcCalled = false;
            var validator = new ValidatorBuilder<bool>()
                .If(ctx => ctx.Value, v =>
                {
                    v.With(ctx => { funcCalled = true; });
                })
                .Build();
            
            // act
            validator.Validate(false);

            // assert
            Assert.False(funcCalled);
        }

        [Fact]
        public void Continue_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Continue<string>(null, null);
            });
        }

        [Fact]
        public void Continue_ThrowsException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                ValidatorBuilderExtensions.Continue<string>(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void Continue_ShouldExecuteBuilderFunction_WhenContextIsValid()
        {
            // arrange
            bool funcCalled = false;
            var validator = new ValidatorBuilder<string>()
                .Continue(v =>
                {
                    v.With(ctx => { funcCalled = true; });
                })
                .Build();
            
            // act
            validator.Validate("test");

            // assert
            Assert.True(funcCalled);
        }

        [Fact]
        public void Continue_ShouldNotExecuteBuilderFunction_WhenContextIsInvalid()
        {
            // arrange
            bool funcCalled = false;
            var validator = new ValidatorBuilder<string>()
                .With(ctx => ctx.AddBrokenRule("test", "test", "test"))
                .Continue(v =>
                {
                    v.With(ctx => { funcCalled = true; });
                })
                .Build();
            
            // act
            validator.Validate("test");

            // assert
            Assert.False(funcCalled);
        }

        [Fact]
        public void Validator_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Validator<string>(null, null);
            });
        }

        [Fact]
        public void Validator_ThrowsException_WhenValidatorIsNull()
        {
            Assert.Throws<ArgumentNullException>("validator", () =>
            {
                ValidatorBuilderExtensions.Validator<string>(new ValidatorBuilder<string>(), null);
            });
        }

        [Fact]
        public void Validator_ShouldExecute_ValidateMethod()
        {
            // arrange
            var validator1Called = false;
            var validator2Called = false;
            var validator1 = new ValidatorBuilder<string>()
                .With(ctx => { validator1Called = true; })
                .Build();
            var validator2 = new ValidatorBuilder<string>()
                .With(ctx => { validator2Called = true; })
                .Validator(validator1)
                .Build();

            // act
            validator2.Validate("test");

            // assert
            Assert.True(validator1Called);
            Assert.True(validator2Called);
        }

        [Fact]
        public void Validator_ValueInContext_ShouldBePassedFromCallingContext()
        {
            // arrange
            string value = null;
            var validator1 = new ValidatorBuilder<string>()
                .With(ctx => 
                { 
                    value = ctx.Value;
                })
                .Build();
            var validator2 = new ValidatorBuilder<string>()
                .Validator(validator1)
                .Build();

            // act
            validator2.Validate("test");

            // assert
            Assert.NotNull(value);
            Assert.Equal("test", value);
        }

        [Fact]
        public void Validator_ValidationOptionsInContext_ShouldBePassedFromCallingContext()
        {
            // arrange
            ValidationOptions valOptions = null;
            var validator1 = new ValidatorBuilder<string>()
                .With(ctx => 
                { 
                    valOptions = ctx.Options;
                })
                .Build();
            var validator2 = new ValidatorBuilder<string>()
                .Validator(validator1)
                .Build();

            // act
            var options = new ValidationOptions
            {
                AddBrokenRuleForException = true,
                StopWhenInvalid = true
            };
            validator2.Validate("test", options);

            // assert
            Assert.NotNull(valOptions);
            Assert.Same(valOptions, options);
        }

        [Fact]
        public void Validator_ShouldAddBrokenRulesToResult()
        {
            // arrange
            var validator1 = new ValidatorBuilder<string>()
                .With(ctx => 
                { 
                    ctx.AddBrokenRule("test", "testVal1", "testVal1");
                })
                .Build();
            var validator2 = new ValidatorBuilder<string>()
                .With(ctx => 
                { 
                    ctx.AddBrokenRule("test", "testVal2", "testVal2");
                })
                .Validator(validator1)
                .Build();

            // act
            var result = validator2.Validate("test");
            var brokenRule1 = result.BrokenRules.FirstOrDefault();
            var brokenRule2 = result.BrokenRules.LastOrDefault();

            // assert
            Assert.False(result.IsValid);
            Assert.Equal(2, result.BrokenRules.Count);
            Assert.Equal("testVal2", brokenRule1.Key);
            Assert.Equal("testVal1", brokenRule2.Key);
        }

        [Fact]
        public void ValidatorFor_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Validator<string, int>(null, null, null);
            });
        }

        [Fact]
        public void ValidatorFor_ThrowsException_WhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>("selector", () =>
            {
                ValidatorBuilderExtensions.Validator<string, int>(new ValidatorBuilder<string>(), null, null);
            });
        }

        [Fact]
        public void ValidatorFor_ThrowsException_WhenValidatorIsNull()
        {
            Assert.Throws<ArgumentNullException>("validator", () =>
            {
                ValidatorBuilderExtensions.Validator<Employee, int>(new ValidatorBuilder<Employee>(), e => e.Id, null);
            });
        }

        [Fact]
        public void ValidatorFor_ShouldExecute_ValidateMethod()
        {
            // arrange
            var validator1Called = false;
            var validator2Called = false;
            var validator1 = new ValidatorBuilder<string>()
                .With(ctx => { validator1Called = true; })
                .Build();
            var validator2 = new ValidatorBuilder<Employee>()
                .With(ctx => { validator2Called = true; })
                .Validator(e => e.LastName, validator1)
                .Build();

            // act
            validator2.Validate(new Employee());

            // assert
            Assert.True(validator1Called);
            Assert.True(validator2Called);
        }

        [Fact]
        public void Message_ThrowsException_WhenBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>("builder", () =>
            {
                ValidatorBuilderExtensions.Message<string>(null, "");
            });
        }

        [Fact]
        public void Message_ThrowsException_WhenMessageIsNull()
        {
            Assert.Throws<ArgumentException>("message", () =>
            {
                ValidatorBuilderExtensions.Message(new ValidatorBuilder<string>(), (string)null);
            });
        }

        [Fact]
        public void Message_ThrowsException_WhenMessageIsEmpty()
        {
            Assert.Throws<ArgumentException>("message", () =>
            {
                ValidatorBuilderExtensions.Message(new ValidatorBuilder<string>(), "");
            });
        }

        [Fact]
        public void Message_ThrowsException_WhenFuncIsNull()
        {
            Assert.Throws<ArgumentNullException>("func", () =>
            {
                ValidatorBuilderExtensions.Message(new ValidatorBuilder<string>(), (Func<ValidationContext<string>, string>)null);
            });
        }

        [Fact]
        public void Message_ShouldCreateOneBrokenRule_WithRulesJoined_LabelAsKey_AndProvidedMessage()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .With(ctx => ctx.AddBrokenRule("R1", "Key1", "Message1"))
                .With(ctx => ctx.AddBrokenRule("R2", "Key2", "Message2"))
                .Message("Test message.")
                .Build();

            // act
            var result = validator.Validate(new Employee());
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal(1, result.BrokenRules.Count);
            Assert.NotNull(brokenRule);
            Assert.Equal("R1,R2", brokenRule.Rule);
            Assert.Equal("Employee", brokenRule.Key);
            Assert.Equal("Test message.", brokenRule.Message);
        }

        [Fact]
        public void Message_ShouldNotCreateBrokenRule_WhenNoBrokenRulesInContext()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .Message("Test message.")
                .Build();

            // act
            var result = validator.Validate(new Employee());

            // assert
            Assert.Empty(result.BrokenRules);
        }

        [Fact]
        public void MessageFunc_ShouldCreateOneBrokenRule_WithRulesJoined_LabelAsKey_AndProvidedMessage()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .With(ctx => ctx.AddBrokenRule("R1", "Key1", "Message1"))
                .With(ctx => ctx.AddBrokenRule("R2", "Key2", "Message2"))
                .Message(ctx => $"Test message {ctx.Value.FirstName}.")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Bob" });
            var brokenRule = result.BrokenRules.FirstOrDefault();

            // assert
            Assert.Equal(1, result.BrokenRules.Count);
            Assert.NotNull(brokenRule);
            Assert.Equal("R1,R2", brokenRule.Rule);
            Assert.Equal("Employee", brokenRule.Key);
            Assert.Equal("Test message Bob.", brokenRule.Message);
        }

        [Fact]
        public void MessageFunc_ShouldNotCreateBrokenRule_WhenNoBrokenRulesInContext()
        {
            // arrange
            var validator = new ValidatorBuilder<Employee>()
                .For(e => e.FirstName, p => p.Equal("Bob"))
                .Message(ctx => $"Test message {ctx.Value.FirstName}.")
                .Build();

            // act
            var result = validator.Validate(new Employee { FirstName = "Bob" });

            // assert
            Assert.Empty(result.BrokenRules);
        }
    }
}