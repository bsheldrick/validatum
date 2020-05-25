using System;
using Validatum;

namespace ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var validator = new ValidatorBuilder<Employee>()
                .Required(e => e.FirstName)
                .For(e => e.LastName, name => 
                {
                    name.Required()
                        .MaxLength(30);
                })
                .Email(e => e.Email)
                .Contains(e => e.Skills, "Cromulent")
                .Range(e => e.Salary, 50000, 100000)
                .LessThanOrEqual(e => e.Commenced, DateTime.Today)
                .Continue(v =>
                {
                    v.True(e => e.Active);
                })
                .Build();

            var employee = new Employee
            {
                FirstName = "Homer",
                Email = "homer[at]springfieldnuclear.com",
                Salary = 45000,
                Skills = new[] { "Embiggening" }
            };

            var result = validator.Validate(employee);

            if (!result.IsValid)
            {
                foreach (var rule in result.BrokenRules)
                {
                    Console.WriteLine($"[{rule.Rule}] {rule.Key}: {rule.Message}");
                }
            };
        }

        public class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string[] Skills { get; set; }
            public decimal Salary { get; set; }
            public DateTime Commenced { get; set; }
            public bool Active { get; set; }
        }
    }
}
