(window.webpackJsonp=window.webpackJsonp||[]).push([[8],{"8Ik1":function(e,t,a){"use strict";a("rGqo"),a("yt8O"),a("Btvt"),a("RW0V"),a("91GP");var n=a("q1tI"),r=a.n(n);t.a=function(e){var t=e.code,a=e.className,n=e.children,l=function(e,t){if(null==e)return{};var a,n,r={},l=Object.keys(e);for(n=0;n<l.length;n++)a=l[n],t.indexOf(a)>=0||(r[a]=e[a]);return r}(e,["code","className","children"]),s="inline-code";return a&&(s=s+" "+a),r.a.createElement("span",Object.assign({className:s},l),t||n)}},gY7D:function(e,t,a){"use strict";a.r(t);var n=a("q1tI"),r=a.n(n),l=a("8Ik1"),s=a("9Dj+"),o=a("wxWl");t.default=function(){return r.a.createElement(s.a,null,r.a.createElement("h1",null,r.a.createElement("em",null,"For")," Function"),r.a.createElement("p",null,"In Validatum, the ",r.a.createElement(l.a,null,"For")," function is an extension method used to build a validator that targets the type returned from a selector expression."),r.a.createElement("p",null,"The ",r.a.createElement(l.a,null,"For")," function can be used to create very complex nested validation functions against the properties (and nested properties) of complex types."),r.a.createElement("p",null,"The selector expression makes it easy to target a property, retrieve its value at runtime, and build a nested inline validator against the property's type."),r.a.createElement("hr",{className:"my-6"}),r.a.createElement("h5",{className:"font-semibold text-sm"},"Method"),r.a.createElement("div",{className:"my-4"},r.a.createElement(l.a,{className:"text-sm text-blue-600 bg-blue-100 font-semibold",code:"For<T, P>(Expression<Func<T, P>> selector, Action<IValidatorBuilder<P>> func)"})),r.a.createElement("p",{className:"mt-4"},"The first parameter is the selector expression (a type of lambda expression)"),r.a.createElement("p",null,"The second parameter is a function that provides an"," ",r.a.createElement(l.a,{code:"IValidatorBuilder<P>"})," that will target the return type ",r.a.createElement(l.a,null,"P")," (",r.a.createElement("em",null,"Note the second type parameter"),")."),r.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Example Usage"),r.a.createElement(o.a,{code:'var validator = new ValidatorBuilder<Employee>()\n    // FirstName is required with min length of 2 and must contain the letter \'p\'\n    .For(e => e.FirstName, name => \n        {\n            name.Required()\n                .MinLength(2)\n                .Contains("p");\n        })\n    // Address must be set with AddressLine1, Postcode and Region required and\n    // the Country must equal Australia\n    .For(e => e.Address, addr => \n        {\n            addr.NotNull()\n                .Required(a => a.AddressLine1)\n                .Required(a => a.PostCode)\n                .Required(a => a.Region)\n                .Equal(a => a.Country, "Australia");\n        })\n    .Build();\n\n// this will not validate\nvar result = validator.Validate(\n    new Employee \n    {\n        FirstName = "Steve",\n        Address = new Address\n        {\n            AddressLine1 = "742 Evergreen Terrace",\n            PostCode = "3500",\n            Region = "VIC",\n            Country = "Australia"\n        }\n    });'}))}},wxWl:function(e,t,a){"use strict";a.d(t,"b",(function(){return o})),a.d(t,"a",(function(){return i}));a("f3/d");var n=a("q1tI"),r=a.n(n),l=a("8Ik1"),s=a("N8Nn"),o=function(e){var t=e.name,a=e.description,n=e.methods,s=e.brokenRule,o=e.children;return r.a.createElement("div",{className:"mb-4",id:t},r.a.createElement("h2",{className:"font-semibold"},t),r.a.createElement("p",null,a),r.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Methods"),n.map((function(e){return r.a.createElement(r.a.Fragment,null,r.a.createElement(l.a,{className:"text-sm font-semibold text-blue-600 bg-blue-100"},e),r.a.createElement("br",null))})),r.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Broken Rule"),r.a.createElement("p",{className:"font-semibold font-mono text-sm text-red-600 inline px-1"},s),r.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm mt-4"},"Example Usage"),o)},i=function(e){var t=e.label,a=e.code;return r.a.createElement(r.a.Fragment,null,t&&r.a.createElement("p",{className:"text-gray-700 mb-0 pb-0 mt-4"},t),r.a.createElement(s.a,{code:a}))}}}]);
//# sourceMappingURL=component---src-pages-building-for-function-tsx-ccd7bd72c27f5ebcf522.js.map