# Validatum

Validatum is an open-source library for building pure fluent validation functions for .NET.


## Install

**.NET CLI**
```cmd
dotnet add package Validatum --version 1.0.0-rc.1
```

**Package Manager**
```cmd
Install-Package Validatum -Version 1.0.0-rc.1
```


## Example

```csharp
// build a validator
var validator = new ValidatorBuilder<Employee>()
    .Required(e => e.FirstName)
    .Email(e => e.Email)
    .For(e => e.LastName, name =>
    {
        name.MinLength(5)
            .Equal("Flanders");
    })
    .Build();

// validate
var result = validator.Validate(
    new Employee 
    { 
        LastName = "Simpson",
        Email = "homer@springfieldnuclear..com"
    }
);

// inspect the result
if (!result.IsValid)
{
    foreach (var rule in result.BrokenRules)
    {
        Console.WriteLine($"[{rule.Rule}] {rule.Key}: {rule.Message}");
    }

    /* OUTPUT
    [Required] FirstName: Value is required.
    [Email] Email: Value must be a valid email.
    [Equal] LastName: Value must equal 'Flanders'.
    */
}

```

## Documentation

Please visit https://bsheldrick.github.io/validatum