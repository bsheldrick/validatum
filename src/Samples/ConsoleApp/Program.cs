using System;
using Validatum;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new ValidatorBuilder<Employee>()
                .Required(e => e.FirstName)
                .Email(e => e.Email)
                .For(e => e.LastName, name =>
                {
                    name.MinLength(5)
                        .Equal("Flanders");
                })
                .Build();

            var result = validator.Validate(
                new Employee 
                { 
                    LastName = "Simpson",
                    Email = "homer@springfieldnuclear..com"
                }
            );

            if (!result.IsValid)
            {
                foreach (var rule in result.BrokenRules)
                {
                    Console.WriteLine($"[{rule.Rule}] {rule.Key}: {rule.Message}");
                }
            }
        }

        private class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }
    }
}
