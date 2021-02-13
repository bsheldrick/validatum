(window.webpackJsonp=window.webpackJsonp||[]).push([[11],{"8Ik1":function(e,t,a){"use strict";a("rGqo"),a("yt8O"),a("Btvt"),a("RW0V"),a("91GP");var l=a("q1tI"),n=a.n(l);t.a=function(e){var t=e.code,a=e.className,l=e.children,r=function(e,t){if(null==e)return{};var a,l,n={},r=Object.keys(e);for(l=0;l<r.length;l++)a=r[l],t.indexOf(a)>=0||(n[a]=e[a]);return n}(e,["code","className","children"]),s="inline-code";return a&&(s=s+" "+a),n.a.createElement("span",Object.assign({className:s},r),t||l)}},roPW:function(e,t,a){"use strict";a.r(t);var l=a("Wbzz"),n=a("q1tI"),r=a.n(n),s=a("8Ik1"),m=a("9Dj+"),c=a("wxWl");t.default=function(){return r.a.createElement(m.a,{title:"The ValidatorBuilder<T> class",description:"If you build it they will validate."},r.a.createElement("h1",null,"The ValidatorBuilder<T> Class"),r.a.createElement("p",null,"In Validatum all validation begins with the"," ",r.a.createElement(s.a,{code:"ValidatorBuilder<T>"})," class. You must create an instance of this class in order to build a"," ",r.a.createElement(s.a,{code:"Validator<T>"})," to validate values. Any type can be used to create a ValidatorBuilder."),r.a.createElement("hr",{className:"my-6"}),r.a.createElement("p",null,"The ",r.a.createElement(s.a,{code:"ValidatorBuilder<T>"})," class provides a default implementation of the ",r.a.createElement(s.a,{code:"IValidatorBuilder<T>"})," ","interface."),r.a.createElement("p",{className:"mt-4"},"This class provides two important functions:",r.a.createElement("ol",{className:"list-decimal list-inside mt-2 ml-2"},r.a.createElement("li",null,"Adding validation functions using the"," ",r.a.createElement(s.a,null,"With()")," method."),r.a.createElement("li",null,"Create a ",r.a.createElement(s.a,{code:"Validator<T>"})," instance using the"," ",r.a.createElement(s.a,null,"Build()")," method."))),r.a.createElement("hr",{className:"my-6"}),r.a.createElement("h2",{className:"my-8"},"Methods"),r.a.createElement(i,{methods:["With<T>(ValidatorDelegate<T> func) (extension)"],returns:"IValidatorBuilder<T>"},r.a.createElement("p",{className:"mt-4"},"This method is used to add validation functions to the builder."),r.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm mt-4"},"Example Usage"),r.a.createElement(c.a,{code:'var builder = new ValidatorBuilder<int>()\n    .With(ctx => \n    {\n        // perform validation\n        if (context.Value == 13)\n        {\n            context.AddBrokenRule("NotThirteen", "Unlucky", "Value cannot equal 13");\n        }\n    });'})),r.a.createElement("hr",{className:"my-6"}),r.a.createElement(i,{methods:["Build(string label = null)"],returns:"Validator<T>"},r.a.createElement("p",{className:"mt-4"},"This method is used to create a ",r.a.createElement(s.a,{code:"Validator<T>"})," ","instance."),r.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm mt-4"},"Example Usage"),r.a.createElement(c.a,{code:'var validator = new ValidatorBuilder<string>()\n    .Required()\n    .Build(); // builds a Validator<string> instance\n    \n// use the validator\nvar result = validator.Validate("hello");'})),r.a.createElement("hr",{className:"my-6"}),r.a.createElement("h2",{className:"mt-8"},"See also"),r.a.createElement("ul",{className:"list"},r.a.createElement("li",null,r.a.createElement(l.Link,{to:"/validation/execution/"},"Executing a Validator"))))};var i=function(e){var t=e.methods,a=e.returns,l=e.children;return r.a.createElement("div",{className:"mb-4"},r.a.createElement("h3",{className:"font-semibold"},t.map((function(e){return r.a.createElement(r.a.Fragment,null,r.a.createElement(s.a,{className:"text-blue-600 bg-blue-100"},e),r.a.createElement("br",null))}))),r.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Returns"),r.a.createElement(s.a,{className:"text-sm text-blue-600 bg-blue-100 font-semibold"},a),l)}},wxWl:function(e,t,a){"use strict";a.d(t,"b",(function(){return m})),a.d(t,"a",(function(){return c}));a("f3/d");var l=a("q1tI"),n=a.n(l),r=a("8Ik1"),s=a("N8Nn"),m=function(e){var t=e.name,a=e.description,l=e.methods,s=e.brokenRule,m=e.children;return n.a.createElement("div",{className:"mb-4",id:t},n.a.createElement("h2",{className:"font-semibold"},t),n.a.createElement("p",null,a),n.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Methods"),l.map((function(e){return n.a.createElement(n.a.Fragment,null,n.a.createElement(r.a,{className:"text-sm font-semibold text-blue-600 bg-blue-100"},e),n.a.createElement("br",null))})),n.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm my-4"},"Broken Rule"),n.a.createElement("p",{className:"font-semibold font-mono text-sm text-red-600 inline px-1"},s),n.a.createElement("h5",{className:"font-semibold text-gray-800 text-sm mt-4"},"Example Usage"),m)},c=function(e){var t=e.label,a=e.code;return n.a.createElement(n.a.Fragment,null,t&&n.a.createElement("p",{className:"text-gray-700 mb-0 pb-0 mt-4"},t),n.a.createElement(s.a,{code:a}))}}}]);
//# sourceMappingURL=component---src-pages-building-validator-builder-tsx-205fd056dc8c5ec575ec.js.map