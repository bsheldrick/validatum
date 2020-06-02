(window.webpackJsonp=window.webpackJsonp||[]).push([[11,12],{"0tVi":function(e,a,l){"use strict";l.r(a);a.default={Common:{NotNull:{Example1:"var validator = new ValidatorBuilder<string>()\n    .NotNull()\n    .Build();\n\nvar result = validator.Validate(null);",Example2:'var validator = new ValidatorBuilder<Employee>()\n    .NotNull(e => e.FirstName)\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Homer" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.NotNull())\n    .Build();\n\nvar result = validator.Validate(new Employee { LastName = "Simpson" });'},Null:{Example1:"var validator = new ValidatorBuilder<string>()\n    .Null()\n    .Build();\n\nvar result = validator.Validate(null);",Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Null(e => e.FirstName)\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Homer" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.Null())\n    .Build();\n\nvar result = validator.Validate(new Employee { LastName = "Simpson" });'},Equal:{Example1:'var validator = new ValidatorBuilder<string>()\n    .Equal("Smithers")\n    .Build();\n\nvar result = validator.Validate("Smithers");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Equal(e => e.FirstName, "Homer")\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Homer" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.LastName, v => v.Equal("Smithers"))\n    .Build();\n    \nvar result = validator.Validate(new Employee { LastName = "Simpson" });'},NotEqual:{Example1:"var validator = new ValidatorBuilder<int>()\n    .NotEqual(17)\n    .Build();\n\nvar result = validator.Validate(18);",Example2:"var validator = new ValidatorBuilder<Employee>()\n    .NotEqual(e => e.Age, 17)\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 18 });",Example3:"var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Age, v => v.NotEqual(17))\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 18 });"}},Comparison:{GreaterThan:{Example1:"var validator = new ValidatorBuilder<int>()\n    .GreaterThan(18)\n    .Build();\n  \nvar result = validator.Validate(16);",Example2:"var validator = new ValidatorBuilder<Employee>()\n    .GreaterThan(e => e.Age, 18)\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 20 });",Example3:"var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Age, v => v.GreaterThan(18))\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 17 });"},GreaterThanOrEqual:{Example1:"var validator = new ValidatorBuilder<int>()\n    .GreaterThanOrEqual(18)\n    .Build();\n  \nvar result = validator.Validate(16);",Example2:"var validator = new ValidatorBuilder<Employee>()\n    .GreaterThanOrEqual(e => e.Age, 18)\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 20 });",Example3:"var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Age, v => v.GreaterThanOrEqual(18))\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 17 });"},LessThan:{Example1:"var validator = new ValidatorBuilder<int>()\n    .LessThan(18)\n    .Build();\n  \nvar result = validator.Validate(16);",Example2:"var validator = new ValidatorBuilder<Employee>()\n    .LessThan(e => e.Age, 18)\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 20 });",Example3:"var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Age, v => v.LessThan(18))\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 17 });"},LessThanOrEqual:{Example1:"var validator = new ValidatorBuilder<int>()\n    .LessThanOrEqual(18)\n    .Build();\n  \nvar result = validator.Validate(16);",Example2:"var validator = new ValidatorBuilder<Employee>()\n    .LessThanOrEqual(e => e.Age, 18)\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 20 });",Example3:"var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Age, v => v.LessThanOrEqual(18))\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 17 });"},Range:{Example1:"var validator = new ValidatorBuilder<int>()\n    .Range(18, 30)\n    .Build();\n  \nvar result = validator.Validate(24);",Example2:"var validator = new ValidatorBuilder<Employee>()\n    .Range(e => e.Age, 18, 30)\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 20 });",Example3:"var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Age, v => v.Range(18, 30))\n    .Build();\n\nvar result = validator.Validate(new Employee { Age = 17 });"},Compare:{Example1:'var validator = new ValidatorBuilder<LoginModel>()\n    .Compare(m => m.Password, m => m.PasswordConfirm)\n    .Build();\n\nvar result = validator.Validate(new LoginModel \n    { \n        Password = "StrongP@ssw0rd",\n        PasswordConfirm = "StrongPassw0rd"\n    });'}},String:{NotEmpty:{Example1:"var validator = new ValidatorBuilder<string>()\n    .NotEmpty()\n    .Build();\n\nvar result = validator.Validate(null);",Example2:'var validator = new ValidatorBuilder<Employee>()\n    .NotEmpty(e => e.FirstName)\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Homer" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.NotEmpty())\n    .Build();\n\nvar result = validator.Validate(new Employee { LastName = "Simpson" });'},Empty:{Example1:'var validator = new ValidatorBuilder<string>()\n    .Empty()\n    .Build();\n\nvar result = validator.Validate("");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Empty(e => e.FirstName)\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Homer" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.Empty())\n    .Build();\n\nvar result = validator.Validate(new Employee { LastName = "Simpson" });'},Regex:{Example1:'var validator = new ValidatorBuilder<string>()\n    .Regex("^[0-9]*$")\n    .Build();\n\nvar result = validator.Validate("12345");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Regex(e => e.FirstName, "^[0-9]*$")\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Six" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.Regex("^[0-9]*$"))\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "7 of 9" });',Example4:'var validator = new ValidatorBuilder<string>()\n    .Regex("^[a-z]*$", RegexOptions.IgnoreCase)\n    .Build();\n\nvar result = validator.Validate("ABC");'},StartsWith:{Example1:'var validator = new ValidatorBuilder<string>()\n    .StartsWith("fun")\n    .Build();\n\nvar result = validator.Validate("function");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .StartsWith(e => e.FirstName, "Jo")\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Homer" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.StartsWith("Pet"))\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Peter" });'},EndsWith:{Example1:'var validator = new ValidatorBuilder<string>()\n    .EndsWith("on")\n    .Build();\n\nvar result = validator.Validate("function");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .EndsWith(e => e.FirstName, "son")\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Jason" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.EndsWith("art"))\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Lisa" });'},Contains:{Example1:'var validator = new ValidatorBuilder<string>()\n    .Contains("tio")\n    .Build();\n\nvar result = validator.Validate("function");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Contains(e => e.FirstName, "aso")\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Jason" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.Contains("holo"))\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Bartholomew" });'},Length:{Example1:'var validator = new ValidatorBuilder<string>()\n    .Length(3, 8)\n    .Build();\n\nvar result = validator.Validate("function");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Length(e => e.FirstName, 1, 15)\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Jason" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.Length(1, 10))\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Bartholomew" });'},MinLength:{Example1:'var validator = new ValidatorBuilder<string>()\n    .MinLength(3)\n    .Build();\n\nvar result = validator.Validate("stop");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .MinLength(e => e.Role, 5)\n    .Build();\n\nvar result = validator.Validate(new Employee { Role = "Admin" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.MinLength(4))\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "Bart" });'},MaxLength:{Example1:'var validator = new ValidatorBuilder<string>()\n    .MaxLength(6)\n    .Build();\n\nvar result = validator.Validate("validate");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .MaxLength(e => e.Lastname, 12)\n    .Build();\n\nvar result = validator.Validate(new Employee { LastName = "Smithers" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.MaxLength(4))\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "El Barto" });'},Required:{Example1:"var validator = new ValidatorBuilder<string>()\n    .Required()\n    .Build();\n\nvar result = validator.Validate(null);",Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Required(e => e.FirstName)\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.FirstName, v => v.Required())\n    .Build();\n\nvar result = validator.Validate(new Employee { FirstName = "   " });'},Email:{Example1:'var validator = new ValidatorBuilder<string>()\n    .Email()\n    .Build();\n\nvar result = validator.Validate("bart@example.com");',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Email(e => e.Email)\n    .Build();\n\nvar result = validator.Validate(new Employee { Email = "invalid[at]email.com" });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Email, v => v.Email())\n    .Build();\n\nvar result = validator.Validate(new Employee { Email = "homer@gmail.com" });'}},Boolean:{True:{Example1:"var validator = new ValidatorBuilder<bool>()\n    .True()\n    .Build();\n\nvar result = validator.Validate(1 == 1);",Example2:"var validator = new ValidatorBuilder<Employee>()\n    .True(e => e.Active)\n    .Build();\n\nvar result = validator.Validate(new Employee { Active = false });",Example3:"var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Active, v => v.True())\n    .Build();\n\nvar result = validator.Validate(new Employee { Active = true });"},False:{Example1:"var validator = new ValidatorBuilder<bool>()\n    .False()\n    .Build();\n\nvar result = validator.Validate(1 == 1);",Example2:"var validator = new ValidatorBuilder<Employee>()\n    .False(e => e.Active)\n    .Build();\n\nvar result = validator.Validate(new Employee { Active = false });",Example3:"var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Active, v => v.False())\n    .Build();\n\nvar result = validator.Validate(new Employee { Active = true });"}},Collection:{Count:{Example1:'var validator = new ValidatorBuilder<IEnumerable<string>>()\n    .Count(2)\n    .Build();\n\nvar result = validator.Validate(new[] { "One", "Two" });',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Count(e => e.Skills, 2)\n    .Build();\n\nvar result = validator.Validate(new Employee { Skills = new[] { "Cromulent" } });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Skills, v => v.Count(1))\n    .Build();\n\nvar result = validator.Validate(new Employee { Skills = new[] { "Vandalism" } });'},MinCount:{Example1:'var validator = new ValidatorBuilder<IEnumerable<string>>()\n    .MinCount(2)\n    .Build();\n\nvar result = validator.Validate(new[] { "One" });',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .MinCount(e => e.Skills, 2)\n    .Build();\n\nvar result = validator.Validate(new Employee { Skills = new[] { "Cromulent" } });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Skills, v => v.MinCount(1))\n    .Build();\n\nvar result = validator.Validate(new Employee { Skills = new[] { "Vandalism" } });'},MaxCount:{Example1:'var validator = new ValidatorBuilder<IEnumerable<string>>()\n    .MaxCount(2)\n    .Build();\n\nvar result = validator.Validate(new[] { "One" });',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .MaxCount(e => e.Skills, 2)\n    .Build();\n\nvar result = validator.Validate(new Employee { Skills = new[] { "Cromulent" } });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Skills, v => v.MaxCount(1))\n    .Build();\n\nvar result = validator.Validate(new Employee { Skills = new[] { "Vandalism" } });'},Contains:{Example1:'var validator = new ValidatorBuilder<IEnumerable<string>>()\n    .Contains("Two")\n    .Build();\n\nvar result = validator.Validate(new[] { "One", "Two", "Three" });',Example2:'var validator = new ValidatorBuilder<Employee>()\n    .Contains(e => e.Skills, "Cromulent")\n    .Build();\n\nvar result = validator.Validate(new Employee { Skills = new[] { "Cromulent" } });',Example3:'var validator = new ValidatorBuilder<Employee>()\n    .For(e => e.Skills, v => v.Contains("Chill"))\n    .Build();\n\nvar result = validator.Validate(new Employee { Skills = new[] { "Wisdom" } });'}}}},"8Ik1":function(e,a,l){"use strict";var r=l("q1tI"),n=l.n(r);a.a=function(e){var a=e.children,l=e.className,r="inline-code";return l&&(r=r+" "+l),n.a.createElement("span",{className:r},a)}},AV3H:function(e,a,l){"use strict";l.r(a);var r=l("q1tI"),n=l.n(r),t=l("Wbzz"),i=l("9Dj+"),o=l("8Ik1"),d=l("wxWl"),v=l("0tVi");a.default=function(){return n.a.createElement(i.a,{title:"Boolean functions",description:"Validation functions for use on booleans"},n.a.createElement("h1",null,"Boolean Functions"),n.a.createElement("p",null,n.a.createElement("ul",{className:"list"},["True","False"].map((function(e){return n.a.createElement("li",null,n.a.createElement(t.Link,{to:"/functions/boolean/#"+e},e))})))),n.a.createElement("hr",{className:"my-6"}),n.a.createElement(d.b,{name:"True",description:"Ensures the boolean value being validated is true.",methods:["True()","True<T>(Expression<Func<T, bool>> selector)"],brokenRule:"Value must be true."},n.a.createElement(d.a,{label:"Using type instance.",code:v.default.Boolean.True.Example1}),n.a.createElement(d.a,{label:"Using selector expression.",code:v.default.Boolean.True.Example2}),n.a.createElement(d.a,{label:n.a.createElement(n.a.Fragment,null,"Using ",n.a.createElement(o.a,null,"For")," function."),code:v.default.Boolean.True.Example3})),n.a.createElement("hr",{className:"my-6"}),n.a.createElement(d.b,{name:"False",description:"Ensures the boolean value being validated is false.",methods:["False()","False<T>(Expression<Func<T, bool>> selector)"],brokenRule:"Value must be false."},n.a.createElement(d.a,{label:"Using type instance.",code:v.default.Boolean.False.Example1}),n.a.createElement(d.a,{label:"Using selector expression.",code:v.default.Boolean.False.Example2}),n.a.createElement(d.a,{label:n.a.createElement(n.a.Fragment,null,"Using ",n.a.createElement(o.a,null,"For")," function."),code:v.default.Boolean.False.Example3})))}},wxWl:function(e,a,l){"use strict";l.d(a,"b",(function(){return o})),l.d(a,"a",(function(){return d}));l("f3/d");var r=l("q1tI"),n=l.n(r),t=l("8Ik1"),i=l("N8Nn"),o=function(e){var a=e.name,l=e.description,r=e.methods,i=e.brokenRule,o=e.children;return n.a.createElement("div",{className:"mb-4",id:a},n.a.createElement("h2",{className:"font-semibold"},a),n.a.createElement("p",null,l),n.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Methods"),r.map((function(e){return n.a.createElement(n.a.Fragment,null,n.a.createElement(t.a,{className:"text-sm"},e),n.a.createElement("br",null))})),n.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Broken Rule"),n.a.createElement("p",{className:"font-semibold font-mono text-sm text-red-800 bg-red-100 inline px-1"},i),n.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm mt-4"},"Example Usage"),o)},d=function(e){var a=e.label,l=e.code;return n.a.createElement(n.a.Fragment,null,n.a.createElement("p",{className:"text-gray-700 mb-0 pb-0 mt-4"},a),n.a.createElement(i.a,{code:l}))}}}]);
//# sourceMappingURL=component---src-pages-functions-boolean-tsx-903050be2856634039a2.js.map