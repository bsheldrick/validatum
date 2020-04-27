using System;
using System.Collections.Generic;

namespace Validatum.Tests
{
    public class Employee
    {
        public int Id { get; set; }
        public Company Employer { get; set; }
        public Employee Manager { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string[] Skills { get; set; }
        public decimal Salary { get; set; }
        public DateTime Commenced { get; set; }
        public bool Active { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}