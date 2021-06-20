(window.webpackJsonp=window.webpackJsonp||[]).push([[4],{"8Ik1":function(e,n,t){"use strict";t("rGqo"),t("yt8O"),t("Btvt"),t("RW0V"),t("91GP");var a=t("q1tI"),l=t.n(a);n.a=function(e){var n=e.code,t=e.className,a=e.children,r=function(e,n){if(null==e)return{};var t,a,l={},r=Object.keys(e);for(a=0;a<r.length;a++)t=r[a],n.indexOf(t)>=0||(l[t]=e[t]);return l}(e,["code","className","children"]),i="inline-code";return t&&(i=i+" "+t),l.a.createElement("span",Object.assign({className:i},r),n||a)}},C6iJ:function(e,n,t){"use strict";t.r(n);var a=t("Wbzz"),l=t("q1tI"),r=t.n(l),i=t("8Ik1"),o=t("9Dj+"),s=t("wxWl");n.default=function(){return r.a.createElement(o.a,null,r.a.createElement("h1",null,"Extending IValidatorBuilder"),r.a.createElement("p",null,"Validatum is easy to extend by creating your own extension methods targeting the ",r.a.createElement(i.a,{code:"IValidatorBuilder<T>"})," interface."),r.a.createElement("hr",{className:"my-6"}),r.a.createElement("h2",null,"Writing an extension method"),r.a.createElement("p",null,"There are some golden rules to follow to ensure you build high quality extension methods."),r.a.createElement("ul",{className:"list-disc list-inside ml-4 my-4 space-y-2"},r.a.createElement("li",null,"Use the extension method name as the rule name for the broken rule."),r.a.createElement("li",null,"Provide a"," ",r.a.createElement("em",null,r.a.createElement("strong",null,"For"))," ","function overload method that has a selector expression."),r.a.createElement("li",null,"Provide optional"," ",r.a.createElement("em",null,r.a.createElement("strong",null,"key"))," ","and"," ",r.a.createElement("em",null,r.a.createElement("strong",null,"message"))," ","parameters."),r.a.createElement("li",null,"Provide a default"," ",r.a.createElement("em",null,r.a.createElement("strong",null,"message"))," ","to the broken rule."),r.a.createElement("li",null,"Don't use other validation functions in your validation function (don't chain functions)."," ",r.a.createElement(a.Link,{to:"/building/conditional-functions/"},"Conditional When and WhenNot")," ","and the ",r.a.createElement(a.Link,{to:"/building/for-function/"},"For")," function can be used."),r.a.createElement("li",null,r.a.createElement("strong",null,"DON'T")," call the ",r.a.createElement(i.a,{code:"Build()"})," ","function.")),r.a.createElement("p",null,"It's also important to understand the extension method is executed in two phases, the"," ",r.a.createElement("em",null,r.a.createElement("strong",null,"building phase"))," ","and the"," ",r.a.createElement("em",null,r.a.createElement("strong",null,"validation phase")),". See comments in example below."),r.a.createElement("h3",{className:"my-4 font-bold text-base"},"Example"),r.a.createElement(s.a,{code:"using System;\nusing System.Linq.Expressions;\nusing Validatum; // namespace of IValidatorBuilder<T>\n\nnamespace MyProject\n{\n    public static class ValidatorBuilderExtensions\n    {\n        /// <summary>\n        /// Adds a validator to ensure the integer value is not thirteen.\n        /// </summary>\n        public static IValidatorBuilder<int> NotThirteen(this IValidatorBuilder<int> builder,\n            string key = null,\n            string message = null)\n        {\n            if (builder is null)\n            {\n                throw new ArgumentNullException(nameof(builder));\n            }\n\n            // ============================================================ //\n            // Anything outside the 'builder' is the building phase.        //\n            // Code here will execute when the Build() method is called on  //\n            // the builder.                                                 //\n            // ============================================================ //\n\n            return builder\n                .When(\n                    ctx => ctx.Value == 13,\n                    ctx => \n                    {\n                        // ============================================================ //\n                        // Anything inside the 'builder' is the validation phase.       //\n                        // Code here will execute when the Validate() method is called  //\n                        // on the validator.                                            //\n                        // ============================================================ //\n\n                        // use the name of the extension method as broken rule name\n                        // key and message passed to broken rule with default message if null\n                        ctx.AddBrokenRule(\n                            nameof(NotThirteen), \n                            key, \n                            message ?? \"Value cannot equal 13.\");\n                    });\n        }\n\n        /// <summary>\n        /// Adds a validator to ensure the integer value is not thirteen \n        /// for the target of the selector expression.\n        /// </summary>\n        public static IValidatorBuilder<T> NotThirteen<T>(this IValidatorBuilder<T> builder, \n            Expression<Func<T, int>> selector, // make sure your expression targets the correct type\n            string key = null,\n            string message = null)\n        {\n            if (builder is null)\n            {\n                throw new ArgumentNullException(nameof(builder));\n            }\n\n            if (selector is null)\n            {\n                throw new ArgumentNullException(nameof(selector));\n            }\n\n            // set the key to use the path of the selected property if a key has not been provided\n            // e.g. with selector expression 't => t.Address.AddressLine1'\n            //      key will be 'Address.AddressLine1'\n            key = key ?? selector.GetPropertyPath();\n\n            // use the For function to call your validation function\n            return builder.For<T, int>(selector, p => p.NotThirteen(key, message));\n        }\n    }\n}\n"}),r.a.createElement(s.a,{code:'var validator = new ValidatorBuilder<int>()\n    .GreaterThan(10)\n    .NotThirteen()\n    .LessThan(20)\n    .Build();\n\nvar result = validator.Validate(13);\nvar rule = result.BrokenRules.First();\n\nConsole.WriteLine($"[{rule.Rule}] {rule.Key}: {rule.Message}");\n\n// output\n// [NotThirteen] Int32: Value cannot equal 13.\n',label:"Using the extension method"}),r.a.createElement(s.a,{code:'var validator = new ValidatorBuilder<Person>()\n    .GreaterThan(p => p.Age, 10)\n    .NotThirteen(p => p.Age)\n    .LessThan(p => p.Age, 20)\n    .Build();\n\nvar result = validator.Validate(new Person { Name = "Steve", Age = 13 });\nvar rule = result.BrokenRules.First();\n\nConsole.WriteLine($"[{rule.Rule}] {rule.Key}: {rule.Message}");\n\n// output\n// [NotThirteen] Age: Value cannot equal 13.\n',label:"Using selector expression"}),r.a.createElement(s.a,{code:'var validator = new ValidatorBuilder<Person>()\n    .GreaterThan(p => p.Age, 10)\n    .For(p => p.Age, a => a.NotThirteen())\n    .LessThan(p => p.Age, 20)\n    .Build();\n\nvar result = validator.Validate(new Person { Name = "Steve", Age = 13 });\nvar rule = result.BrokenRules.First();\n\nConsole.WriteLine($"[{rule.Rule}] {rule.Key}: {rule.Message}");\n\n// output\n// [NotThirteen] Age: Value cannot equal 13.\n',label:r.a.createElement(r.a.Fragment,null,"Using ",r.a.createElement(i.a,null,"For")," function.")}))}},wxWl:function(e,n,t){"use strict";t.d(n,"b",(function(){return o})),t.d(n,"a",(function(){return s}));t("f3/d");var a=t("q1tI"),l=t.n(a),r=t("8Ik1"),i=t("N8Nn"),o=function(e){var n=e.name,t=e.description,a=e.methods,i=e.brokenRule,o=e.children;return l.a.createElement("div",{className:"mb-4",id:n},l.a.createElement("h2",{className:"font-semibold"},n),l.a.createElement("p",null,t),l.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Methods"),a.map((function(e){return l.a.createElement(l.a.Fragment,null,l.a.createElement(r.a,{className:"text-sm font-semibold text-blue-600 bg-blue-100"},e),l.a.createElement("br",null))})),l.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Broken Rule"),l.a.createElement("p",{className:"font-semibold font-mono text-sm text-red-600 inline px-1"},i),l.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm mt-4"},"Example Usage"),o)},s=function(e){var n=e.label,t=e.code;return l.a.createElement(l.a.Fragment,null,n&&l.a.createElement("p",{className:"text-gray-700 mb-0 pb-0 mt-4"},n),l.a.createElement(i.a,{code:t}))}}}]);
//# sourceMappingURL=component---src-pages-advanced-extension-methods-tsx-7c46cd3b59eba076352e.js.map