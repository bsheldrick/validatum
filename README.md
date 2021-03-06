# Validatum

Validatum is an open-source library for building fluent validation functions for .NET.

## Install

**.NET CLI**

```cmd
dotnet add package Validatum --version 1.1.0
```

**Package Manager**

```cmd
Install-Package Validatum -Version 1.1.0
```

## Platform Support

- .NET Standard 2.0+
- .NET Core 2.0+
- .NET Framework 4.6.1+

## Example

```csharp
// build a validator
var validator = new ValidatorBuilder<Employee>()
    .Required(e => e.FirstName)
    .Email(e => e.Email)
    .For(e => e.LastName, name =>
    {
        name.MinLength(5)
            .Equal("Smithers");
    })
    .Build();

// validate
var result = validator.Validate(
    new Employee
    {
        LastName = "Simpson",
        Email = "homer[at]springfieldnuclear.com"
    }
);

foreach (var rule in result.BrokenRules)
{
    Console.WriteLine($"[{rule.Rule}] {rule.Key}: {rule.Message}");
}
```

Output

```sh
[Required] FirstName: Value is required.
[Email] Email: Value must be a valid email.
[Equal] LastName: Value must equal 'Smithers'.
```

## Why use Validatum?

- Does not require inheritance of a base type.
- Define and build functions in the scope that validation will be executed.
- Define very complex validation logic in a single statement.
- Easily add custom validators as inline functions.
- Very small package size ~33KB.
- Simple and easily extensible API.

## Documentation

Please visit https://bsheldrick.github.io/validatum
