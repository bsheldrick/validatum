(window.webpackJsonp=window.webpackJsonp||[]).push([[16],{"8Ik1":function(e,a,l){"use strict";var t=l("q1tI"),n=l.n(t);a.a=function(e){var a=e.children;return n.a.createElement("span",{className:"inline-code"},a)}},"9U4S":function(e,a,l){"use strict";l.r(a);l("FLlr"),l("pIFo");var t=l("q1tI"),n=l.n(t),r=l("Wbzz"),i=l("9Dj+"),u=l("N8Nn"),s=l("8Ik1"),c=l("DwpA");function m(e,a){return e.replace(/^(?!\s*$)/gm," ".repeat(a))}var o="public class Employee\n{\n    public string FirstName { get; set; }\n    public string LastName { get; set; }\n    public string Email { get; set; }\n    public string Phone { get; set; }\n    public string[] Skills { get; set; }\n    public decimal Salary { get; set; }\n    public DateTime Commenced { get; set; }\n    public bool Active { get; set; }\n}",d='var validator = new ValidatorBuilder<Employee>()\n    .Required(e => e.FirstName)\n    .For(e => e.LastName, name => \n    {\n        name.Required()\n            .MaxLength(30);\n    })\n    .Email(e => e.Email)\n    .Contains(e => e.Skills, "Cromulent")\n    .Range(e => e.Salary, 50000, 100000)\n    .LessThanOrEqual(e => e.Commenced, DateTime.Today)\n    .Continue(v =>\n    {\n        v.True(e => e.Active);\n    })\n    .Build();',E='var employee = new Employee\n{\n    FirstName = "Homer",\n    Email = "homer[at]springfieldnuclear.com",\n    Salary = 45000,\n    Skills = new[] { "Embiggening" }\n};\n\nvar result = validator.Validate(employee);',p='if (!result.IsValid)\n{\n    foreach (var rule in result.BrokenRules)\n    {\n        Console.WriteLine($"[{rule.Rule}] {rule.Key}: {rule.Message}");\n    }\n};',g="using System;\nusing Validatum;\n\nnamespace ConsoleApp\n{\n    public class Program\n    {\n        public static void Main(string[] args)\n        {\n"+m(d,12)+"\n\n"+m(E,12)+"\n\n"+m(p,12)+"\n        }\n\n"+m(o,8)+"\n    }\n}";a.default=function(){return n.a.createElement(i.a,{title:"Create your first Validator",description:"Create your first validator using Validatum"},n.a.createElement("h1",null,"Create your first Validator"),n.a.createElement("p",null,"Building a validation function is accomplished with the"," ",n.a.createElement(s.a,null,"ValidatorBuilder<T>")," class. Where"," ",n.a.createElement(s.a,null,"T")," is the type you want to validate. See"," ",n.a.createElement(r.Link,{to:"/building/validator-builder/",className:"text-blue-500"},"The ValidatorBuilder<T> Class"),"."),n.a.createElement("hr",{className:"my-6"}),n.a.createElement("p",null,"For your first validator we will use the following"," ",n.a.createElement(s.a,null,"Employee")," model."),n.a.createElement("div",{className:"lg:w-2/3 mb-4"},n.a.createElement(u.a,{code:o})),n.a.createElement("p",null,"Our validation function must satisfy the following rules:"),n.a.createElement("ul",{className:"list mb-4"},n.a.createElement("li",null,n.a.createElement(s.a,null,"FirstName")," is required."),n.a.createElement("li",null,n.a.createElement(s.a,null,"LastName")," is required and have no more than 30 characters."),n.a.createElement("li",null,n.a.createElement(s.a,null,"Email")," must be a valid email."),n.a.createElement("li",null,n.a.createElement(s.a,null,"Skills")," must contain"," ",n.a.createElement("strong",null,"Cromulent"),"."),n.a.createElement("li",null,n.a.createElement(s.a,null,"Salary")," between ",n.a.createElement("strong",null,"50,000")," and"," ",n.a.createElement("strong",null,"100,000"),"."),n.a.createElement("li",null,n.a.createElement(s.a,null,"Commenced")," cannot be in the future."),n.a.createElement("li",null,n.a.createElement(s.a,null,"Active")," must be true only if previous rules pass.")),n.a.createElement("p",null,"We can build a validator that will enforce the above rules like this:"),n.a.createElement("div",{className:"lg:w-2/3 mb-4"},n.a.createElement(u.a,{code:d}),n.a.createElement("p",null,"The ",n.a.createElement(s.a,null,"Build()")," method will return a",n.a.createElement(s.a,null,"Validator<Employee>")," which can be used to validate ",n.a.createElement(s.a,null,"Employee")," instances.")),n.a.createElement("div",{className:"lg:w-2/3 mb-4"},n.a.createElement(u.a,{code:E}),n.a.createElement("p",null,"The ",n.a.createElement(s.a,null,"Validate()")," method returns a"," ",n.a.createElement(s.a,null,"ValidationResult")," instance containing a collection of broken validation rules. See"," ",n.a.createElement(r.Link,{to:"/validation/results/",className:"text-blue-500"},"Validation Results"),".")),n.a.createElement("div",{className:"lg:w-2/3 mb-4"},n.a.createElement(u.a,{code:p})),n.a.createElement("p",null,"Putting it all together into an executable console application."),n.a.createElement("div",{className:"lg:w-2/3 mb-4"},n.a.createElement(u.a,{code:g})),n.a.createElement("p",null,"Will produce the following output:"),n.a.createElement("div",{className:"lg:w-2/3 mb-4"},n.a.createElement(c.a,{code:"[Required] LastName: Value is required.\n[Email] Email: Value must be a valid email.\n[Contains] Skills: Must contain item 'Cromulent'.\n[Range] Salary: Value must be in range '50000' to '100000'."})))}},FLlr:function(e,a,l){var t=l("XKFU");t(t.P,"String",{repeat:l("l0Rn")})},l0Rn:function(e,a,l){"use strict";var t=l("RYi7"),n=l("vhPU");e.exports=function(e){var a=String(n(this)),l="",r=t(e);if(r<0||r==1/0)throw RangeError("Count can't be negative");for(;r>0;(r>>>=1)&&(a+=a))1&r&&(l+=a);return l}}}]);
//# sourceMappingURL=component---src-pages-getting-started-first-validator-tsx-6bf1571474a18673aef4.js.map