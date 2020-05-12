Validatum
=========

Example

```csharp
// build a validator
var validator = new ValidatorBuilder<Employee>()
    // FirstName cannot be null, empty or whitespace
    .Required(e => e.FirstName) 
    // Email must be valid email address
    .Email(e => e.Email) 
    // Validator targeting LastName
    .For(e => e.LastName, val => 
    {
        // must be at least 5 characters
        val.MinLength(5);
        // must be 'Flanders'
        val.Equal("Flanders"); 
    })
    // Active must be true
    .True(e => e.Active) 
    // Continue validating only if valid
    .Continue(val => 
    {
        // Phone cannot be null
        val.NotNull(e => e.Phone, message: "We need your phone number");
    })
    // Custom validation function
    .With(ctx =>
    {
        if (ctx.Value.LastName == "Simpson")
        {
            // broken rule (Rule, Key, Message)
            ctx.AddBrokenRule("NoHomers", "LastName", "Nice try, Homer.");
        }
    })
    // create the validator
    .Build(); 

// validate
var result = validator.Validate(new Employee 
{ 
    FirstName = "Homer",
    LastName = "Simpson" 
});

if (!result.IsValid)
{
    // inspect the broken rules

}
```