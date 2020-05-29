(window.webpackJsonp=window.webpackJsonp||[]).push([[0],{"8+s/":function(e,t,n){"use strict";n("V+eJ"),n("bWfx"),n("f3/d"),n("hHhE"),n("HAE/");var a,r=n("q1tI"),i=(a=r)&&"object"==typeof a&&"default"in a?a.default:a;function o(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}var l=!("undefined"==typeof window||!window.document||!window.document.createElement);e.exports=function(e,t,n){if("function"!=typeof e)throw new Error("Expected reducePropsToState to be a function.");if("function"!=typeof t)throw new Error("Expected handleStateChangeOnClient to be a function.");if(void 0!==n&&"function"!=typeof n)throw new Error("Expected mapStateOnServer to either be undefined or a function.");return function(a){if("function"!=typeof a)throw new Error("Expected WrappedComponent to be a React component.");var c,s=[];function u(){c=e(s.map((function(e){return e.props}))),d.canUseDOM?t(c):n&&(c=n(c))}var d=function(e){var t,n;function r(){return e.apply(this,arguments)||this}n=e,(t=r).prototype=Object.create(n.prototype),t.prototype.constructor=t,t.__proto__=n,r.peek=function(){return c},r.rewind=function(){if(r.canUseDOM)throw new Error("You may only call rewind() on the server. Call peek() to read the current state.");var e=c;return c=void 0,s=[],e};var o=r.prototype;return o.UNSAFE_componentWillMount=function(){s.push(this),u()},o.componentDidUpdate=function(){u()},o.componentWillUnmount=function(){var e=s.indexOf(this);s.splice(e,1),u()},o.render=function(){return i.createElement(a,this.props)},r}(r.PureComponent);return o(d,"displayName","SideEffect("+function(e){return e.displayName||e.name||"Component"}(a)+")"),o(d,"canUseDOM",l),d}}},"9Dj+":function(e,t,n){"use strict";var a=n("q1tI"),r=n.n(a),i=n("qhky"),o=n("Wbzz");var l=function(){return r.a.createElement("div",{className:"mt-2"},r.a.createElement("div",{className:"mb-8"},r.a.createElement("h5",{className:"side-nav-heading"},"Getting Started"),r.a.createElement("ul",null,r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/getting-started/installation/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Installation")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/getting-started/first-validator/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Create your first Validator")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/getting-started/release-notes/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Release Notes")))),r.a.createElement("div",{className:"mb-8"},r.a.createElement("h5",{className:"side-nav-heading"},"Building Validators"),r.a.createElement("ul",null,r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/building/validator-builder/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"The ValidatorBuilder<T> Class")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/building/chaining-functions/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Chaining Functions")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/building/custom-functions/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Custom Functions")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/building/conditional-functions/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Conditional Functions")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/building/including-validators/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Including Validators")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/building/for-expressions/",className:"side-nav-link",activeClassName:"side-nav-link-active"},r.a.createElement("em",null,"For")," Expressions")))),r.a.createElement("div",{className:"mb-8"},r.a.createElement("h5",{className:"side-nav-heading"},"Functions"),r.a.createElement("ul",null,r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/functions/common/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Common")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/functions/comparison/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Comparison")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/functions/string/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"String")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/functions/boolean/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Boolean")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/functions/collection/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Collection")))),r.a.createElement("div",{className:"mb-8"},r.a.createElement("h5",{className:"side-nav-heading"},"Validation"),r.a.createElement("ul",null,r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/validation/execution/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Executing a Validator")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/validation/options/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Validation Options")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/validation/results/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Validation Results")),r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/validation/exception-handling/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Exception Handling")))),r.a.createElement("div",{className:"mb-8"},r.a.createElement("h5",{className:"side-nav-heading"},"Advanced"),r.a.createElement("ul",null,r.a.createElement("li",null,r.a.createElement(o.Link,{to:"/advanced/extension-methods/",className:"side-nav-link",activeClassName:"side-nav-link-active"},"Extending IValidatorBuilder<T>")))))},c=n("s8M5");var s=function(){var e=Object(a.useState)(!1),t=e[0],n=e[1],i=Object(c.a)().nugetVersion;return r.a.createElement(r.a.Fragment,null,r.a.createElement("div",{className:"flex justify-between items-center p-4 border-b"},r.a.createElement("div",{className:"text-2xl text-gray-800 font-semibold"},r.a.createElement(o.Link,{to:"/"},r.a.createElement("span",{className:"border border-green-400 bg-green-200 text-green-400 text-base rounded mr-2 px-2 py-1"},"✓"),"Validatum")),r.a.createElement("div",{className:"flex items-center"},r.a.createElement("span",{className:"px-2 py-1 bg-teal-200 font-bold text-xs text-teal-600 rounded border border-teal-400"},i),r.a.createElement("a",{href:"https://github.com/bsheldrick/validatum",className:"text-gray-700 hover:text-gray-500 ml-4"},r.a.createElement("svg",{className:"fill-current h-6 w-6",role:"img",viewBox:"0 0 24 24",xmlns:"http://www.w3.org/2000/svg"},r.a.createElement("title",null,"GitHub"),r.a.createElement("path",{d:"M12 .297c-6.63 0-12 5.373-12 12 0 5.303 3.438 9.8 8.205 11.385.6.113.82-.258.82-.577 0-.285-.01-1.04-.015-2.04-3.338.724-4.042-1.61-4.042-1.61C4.422 18.07 3.633 17.7 3.633 17.7c-1.087-.744.084-.729.084-.729 1.205.084 1.838 1.236 1.838 1.236 1.07 1.835 2.809 1.305 3.495.998.108-.776.417-1.305.76-1.605-2.665-.3-5.466-1.332-5.466-5.93 0-1.31.465-2.38 1.235-3.22-.135-.303-.54-1.523.105-3.176 0 0 1.005-.322 3.3 1.23.96-.267 1.98-.399 3-.405 1.02.006 2.04.138 3 .405 2.28-1.552 3.285-1.23 3.285-1.23.645 1.653.24 2.873.12 3.176.765.84 1.23 1.91 1.23 3.22 0 4.61-2.805 5.625-5.475 5.92.42.36.81 1.096.81 2.22 0 1.606-.015 2.896-.015 3.286 0 .315.21.69.825.57C20.565 22.092 24 17.592 24 12.297c0-6.627-5.373-12-12-12"}))),r.a.createElement("div",{className:"md:hidden ml-4 h-5 w-5 flex items-center"},!t&&r.a.createElement("button",{onClick:function(e){return n(!t)},className:"text-gray-700 hover:text-gray-500"},r.a.createElement("svg",{viewBox:"0 0 64 64",className:"fill-current"},r.a.createElement("rect",{y:"10",width:"80",height:"8",rx:"4"}),r.a.createElement("rect",{y:"30",width:"80",height:"8",rx:"4"}),r.a.createElement("rect",{y:"50",width:"80",height:"8",rx:"4"}))),t&&r.a.createElement("button",{onClick:function(e){return n(!t)},className:"text-gray-700 hover:text-gray-500"},r.a.createElement("svg",{className:"fill-current stroke-current stroke-2 w-4 h-4",viewBox:"0 0 20 20"},r.a.createElement("path",{d:"M10 8.586L2.929 1.515 1.515 2.929 8.586 10l-7.071 7.071 1.414 1.414L10 11.414l7.071 7.071 1.414-1.414L11.414 10l7.071-7.071-1.414-1.414L10 8.586z"})))))),t&&r.a.createElement("div",{className:"absolute w-full h-full p-4 bg-gray-100 z-100"},r.a.createElement(l,null)))};t.a=function(e){var t=e.children,n=e.title,a=e.description,o=Object(c.a)(),u=o.title,d=o.description;return u=n?n+" - "+u:u,r.a.createElement(r.a.Fragment,null,r.a.createElement(i.a,null,r.a.createElement("title",null,u),r.a.createElement("meta",{name:"description",content:a||d}),r.a.createElement("meta",{name:"keywords",content:"validation dotnet csharp c# builder"})),r.a.createElement("div",{className:"container mx-auto"},r.a.createElement(s,null),r.a.createElement("div",{className:"grid sm:grid-cols-none md:grid-cols-12 gap-6 p-4"},r.a.createElement("div",{className:"col-span-3 md:col-span-4 lg:col-span-3 hidden md:block"},r.a.createElement(l,null)),r.a.createElement("div",{className:"col-span-9 md:col-span-8 lg:col-span-9"},t))))}},"HAE/":function(e,t,n){var a=n("XKFU");a(a.S+a.F*!n("nh4g"),"Object",{defineProperty:n("hswa").f})},MfPe:function(e){e.exports=JSON.parse('{"data":{"site":{"siteMetadata":{"title":"Validatum Docs","description":"An open-source library for building fluent validation functions for .NET.","author":"Brad Sheldrick","nugetVersion":"1.0.0-rc.3"}}}}')},Oyvg:function(e,t,n){var a=n("dyZX"),r=n("Xbzi"),i=n("hswa").f,o=n("kJMx").f,l=n("quPj"),c=n("C/va"),s=a.RegExp,u=s,d=s.prototype,m=/a/g,f=/a/g,p=new s(m)!==m;if(n("nh4g")&&(!p||n("eeVq")((function(){return f[n("K0xU")("match")]=!1,s(m)!=m||s(f)==f||"/a/i"!=s(m,"i")})))){s=function(e,t){var n=this instanceof s,a=l(e),i=void 0===t;return!n&&a&&e.constructor===s&&i?e:r(p?new u(a&&!i?e.source:e,t):u((a=e instanceof s)?e.source:e,a&&i?c.call(e):t),n?this:d,s)};for(var v=function(e){e in s||i(s,e,{configurable:!0,get:function(){return u[e]},set:function(t){u[e]=t}})},h=o(u),y=0;h.length>y;)v(h[y++]);d.constructor=s,s.prototype=d,n("KroJ")(a,"RegExp",s)}n("elZq")("RegExp")},bmMU:function(e,t,n){"use strict";n("f3/d"),n("SRfc"),n("a1Th"),n("h7Nl"),n("Oyvg"),n("rGqo"),n("yt8O"),n("Btvt"),n("RW0V"),n("LK8F");var a=Array.isArray,r=Object.keys,i=Object.prototype.hasOwnProperty,o="undefined"!=typeof Element;e.exports=function(e,t){try{return function e(t,n){if(t===n)return!0;if(t&&n&&"object"==typeof t&&"object"==typeof n){var l,c,s,u=a(t),d=a(n);if(u&&d){if((c=t.length)!=n.length)return!1;for(l=c;0!=l--;)if(!e(t[l],n[l]))return!1;return!0}if(u!=d)return!1;var m=t instanceof Date,f=n instanceof Date;if(m!=f)return!1;if(m&&f)return t.getTime()==n.getTime();var p=t instanceof RegExp,v=n instanceof RegExp;if(p!=v)return!1;if(p&&v)return t.toString()==n.toString();var h=r(t);if((c=h.length)!==r(n).length)return!1;for(l=c;0!=l--;)if(!i.call(n,h[l]))return!1;if(o&&t instanceof Element&&n instanceof Element)return t===n;for(l=c;0!=l--;)if(!("_owner"===(s=h[l])&&t.$$typeof||e(t[s],n[s])))return!1;return!0}return t!=t&&n!=n}(e,t)}catch(n){if(n.message&&n.message.match(/stack|recursion/i)||-2146828260===n.number)return console.warn("Warning: react-fast-compare does not handle circular references.",n.name,n.message),!1;throw n}}},h7Nl:function(e,t,n){var a=Date.prototype,r=a.toString,i=a.getTime;new Date(NaN)+""!="Invalid Date"&&n("KroJ")(a,"toString",(function(){var e=i.call(this);return e==e?r.call(this):"Invalid Date"}))},qhky:function(e,t,n){"use strict";(function(e){n.d(t,"a",(function(){return ve}));n("dZ+Y"),n("KKXr"),n("2Spj"),n("eM6i"),n("8+KV"),n("0l/t"),n("LK8F"),n("pIFo"),n("V+eJ"),n("/SS/"),n("hHhE"),n("91GP"),n("HAE/"),n("rE2o"),n("ioFf"),n("DNiP"),n("rGqo"),n("yt8O"),n("Btvt"),n("RW0V"),n("bWfx");var a,r,i,o,l=n("17x9"),c=n.n(l),s=n("8+s/"),u=n.n(s),d=n("bmMU"),m=n.n(d),f=n("q1tI"),p=n.n(f),v=n("MgzW"),h=n.n(v),y="bodyAttributes",g="htmlAttributes",b="titleAttributes",E={BASE:"base",BODY:"body",HEAD:"head",HTML:"html",LINK:"link",META:"meta",NOSCRIPT:"noscript",SCRIPT:"script",STYLE:"style",TITLE:"title"},T=(Object.keys(E).map((function(e){return E[e]})),"charset"),w="cssText",k="href",C="http-equiv",N="innerHTML",A="itemprop",O="name",x="property",S="rel",L="src",j="target",P={accesskey:"accessKey",charset:"charSet",class:"className",contenteditable:"contentEditable",contextmenu:"contextMenu","http-equiv":"httpEquiv",itemprop:"itemProp",tabindex:"tabIndex"},I="defaultTitle",M="defer",R="encodeSpecialCharacters",F="onChangeClientState",q="titleTemplate",D=Object.keys(P).reduce((function(e,t){return e[P[t]]=t,e}),{}),H=[E.NOSCRIPT,E.SCRIPT,E.STYLE],B="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},V=function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")},K=function(){function e(e,t){for(var n=0;n<t.length;n++){var a=t[n];a.enumerable=a.enumerable||!1,a.configurable=!0,"value"in a&&(a.writable=!0),Object.defineProperty(e,a.key,a)}}return function(t,n,a){return n&&e(t.prototype,n),a&&e(t,a),t}}(),U=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var a in n)Object.prototype.hasOwnProperty.call(n,a)&&(e[a]=n[a])}return e},Y=function(e,t){var n={};for(var a in e)t.indexOf(a)>=0||Object.prototype.hasOwnProperty.call(e,a)&&(n[a]=e[a]);return n},_=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t},W=function(e){var t=!(arguments.length>1&&void 0!==arguments[1])||arguments[1];return!1===t?String(e):String(e).replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/"/g,"&quot;").replace(/'/g,"&#x27;")},z=function(e){var t=$(e,E.TITLE),n=$(e,q);if(n&&t)return n.replace(/%s/g,(function(){return Array.isArray(t)?t.join(""):t}));var a=$(e,I);return t||a||void 0},J=function(e){return $(e,F)||function(){}},G=function(e,t){return t.filter((function(t){return void 0!==t[e]})).map((function(t){return t[e]})).reduce((function(e,t){return U({},e,t)}),{})},X=function(e,t){return t.filter((function(e){return void 0!==e[E.BASE]})).map((function(e){return e[E.BASE]})).reverse().reduce((function(t,n){if(!t.length)for(var a=Object.keys(n),r=0;r<a.length;r++){var i=a[r].toLowerCase();if(-1!==e.indexOf(i)&&n[i])return t.concat(n)}return t}),[])},Z=function(e,t,n){var a={};return n.filter((function(t){return!!Array.isArray(t[e])||(void 0!==t[e]&&ae("Helmet: "+e+' should be of type "Array". Instead found type "'+B(t[e])+'"'),!1)})).map((function(t){return t[e]})).reverse().reduce((function(e,n){var r={};n.filter((function(e){for(var n=void 0,i=Object.keys(e),o=0;o<i.length;o++){var l=i[o],c=l.toLowerCase();-1===t.indexOf(c)||n===S&&"canonical"===e[n].toLowerCase()||c===S&&"stylesheet"===e[c].toLowerCase()||(n=c),-1===t.indexOf(l)||l!==N&&l!==w&&l!==A||(n=l)}if(!n||!e[n])return!1;var s=e[n].toLowerCase();return a[n]||(a[n]={}),r[n]||(r[n]={}),!a[n][s]&&(r[n][s]=!0,!0)})).reverse().forEach((function(t){return e.push(t)}));for(var i=Object.keys(r),o=0;o<i.length;o++){var l=i[o],c=h()({},a[l],r[l]);a[l]=c}return e}),[]).reverse()},$=function(e,t){for(var n=e.length-1;n>=0;n--){var a=e[n];if(a.hasOwnProperty(t))return a[t]}return null},Q=(a=Date.now(),function(e){var t=Date.now();t-a>16?(a=t,e(t)):setTimeout((function(){Q(e)}),0)}),ee=function(e){return clearTimeout(e)},te="undefined"!=typeof window?window.requestAnimationFrame&&window.requestAnimationFrame.bind(window)||window.webkitRequestAnimationFrame||window.mozRequestAnimationFrame||Q:e.requestAnimationFrame||Q,ne="undefined"!=typeof window?window.cancelAnimationFrame||window.webkitCancelAnimationFrame||window.mozCancelAnimationFrame||ee:e.cancelAnimationFrame||ee,ae=function(e){return console&&"function"==typeof console.warn&&console.warn(e)},re=null,ie=function(e,t){var n=e.baseTag,a=e.bodyAttributes,r=e.htmlAttributes,i=e.linkTags,o=e.metaTags,l=e.noscriptTags,c=e.onChangeClientState,s=e.scriptTags,u=e.styleTags,d=e.title,m=e.titleAttributes;ce(E.BODY,a),ce(E.HTML,r),le(d,m);var f={baseTag:se(E.BASE,n),linkTags:se(E.LINK,i),metaTags:se(E.META,o),noscriptTags:se(E.NOSCRIPT,l),scriptTags:se(E.SCRIPT,s),styleTags:se(E.STYLE,u)},p={},v={};Object.keys(f).forEach((function(e){var t=f[e],n=t.newTags,a=t.oldTags;n.length&&(p[e]=n),a.length&&(v[e]=f[e].oldTags)})),t&&t(),c(e,p,v)},oe=function(e){return Array.isArray(e)?e.join(""):e},le=function(e,t){void 0!==e&&document.title!==e&&(document.title=oe(e)),ce(E.TITLE,t)},ce=function(e,t){var n=document.getElementsByTagName(e)[0];if(n){for(var a=n.getAttribute("data-react-helmet"),r=a?a.split(","):[],i=[].concat(r),o=Object.keys(t),l=0;l<o.length;l++){var c=o[l],s=t[c]||"";n.getAttribute(c)!==s&&n.setAttribute(c,s),-1===r.indexOf(c)&&r.push(c);var u=i.indexOf(c);-1!==u&&i.splice(u,1)}for(var d=i.length-1;d>=0;d--)n.removeAttribute(i[d]);r.length===i.length?n.removeAttribute("data-react-helmet"):n.getAttribute("data-react-helmet")!==o.join(",")&&n.setAttribute("data-react-helmet",o.join(","))}},se=function(e,t){var n=document.head||document.querySelector(E.HEAD),a=n.querySelectorAll(e+"[data-react-helmet]"),r=Array.prototype.slice.call(a),i=[],o=void 0;return t&&t.length&&t.forEach((function(t){var n=document.createElement(e);for(var a in t)if(t.hasOwnProperty(a))if(a===N)n.innerHTML=t.innerHTML;else if(a===w)n.styleSheet?n.styleSheet.cssText=t.cssText:n.appendChild(document.createTextNode(t.cssText));else{var l=void 0===t[a]?"":t[a];n.setAttribute(a,l)}n.setAttribute("data-react-helmet","true"),r.some((function(e,t){return o=t,n.isEqualNode(e)}))?r.splice(o,1):i.push(n)})),r.forEach((function(e){return e.parentNode.removeChild(e)})),i.forEach((function(e){return n.appendChild(e)})),{oldTags:r,newTags:i}},ue=function(e){return Object.keys(e).reduce((function(t,n){var a=void 0!==e[n]?n+'="'+e[n]+'"':""+n;return t?t+" "+a:a}),"")},de=function(e){var t=arguments.length>1&&void 0!==arguments[1]?arguments[1]:{};return Object.keys(e).reduce((function(t,n){return t[P[n]||n]=e[n],t}),t)},me=function(e,t,n){switch(e){case E.TITLE:return{toComponent:function(){return e=t.title,n=t.titleAttributes,(a={key:e})["data-react-helmet"]=!0,r=de(n,a),[p.a.createElement(E.TITLE,r,e)];var e,n,a,r},toString:function(){return function(e,t,n,a){var r=ue(n),i=oe(t);return r?"<"+e+' data-react-helmet="true" '+r+">"+W(i,a)+"</"+e+">":"<"+e+' data-react-helmet="true">'+W(i,a)+"</"+e+">"}(e,t.title,t.titleAttributes,n)}};case y:case g:return{toComponent:function(){return de(t)},toString:function(){return ue(t)}};default:return{toComponent:function(){return function(e,t){return t.map((function(t,n){var a,r=((a={key:n})["data-react-helmet"]=!0,a);return Object.keys(t).forEach((function(e){var n=P[e]||e;if(n===N||n===w){var a=t.innerHTML||t.cssText;r.dangerouslySetInnerHTML={__html:a}}else r[n]=t[e]})),p.a.createElement(e,r)}))}(e,t)},toString:function(){return function(e,t,n){return t.reduce((function(t,a){var r=Object.keys(a).filter((function(e){return!(e===N||e===w)})).reduce((function(e,t){var r=void 0===a[t]?t:t+'="'+W(a[t],n)+'"';return e?e+" "+r:r}),""),i=a.innerHTML||a.cssText||"",o=-1===H.indexOf(e);return t+"<"+e+' data-react-helmet="true" '+r+(o?"/>":">"+i+"</"+e+">")}),"")}(e,t,n)}}}},fe=function(e){var t=e.baseTag,n=e.bodyAttributes,a=e.encode,r=e.htmlAttributes,i=e.linkTags,o=e.metaTags,l=e.noscriptTags,c=e.scriptTags,s=e.styleTags,u=e.title,d=void 0===u?"":u,m=e.titleAttributes;return{base:me(E.BASE,t,a),bodyAttributes:me(y,n,a),htmlAttributes:me(g,r,a),link:me(E.LINK,i,a),meta:me(E.META,o,a),noscript:me(E.NOSCRIPT,l,a),script:me(E.SCRIPT,c,a),style:me(E.STYLE,s,a),title:me(E.TITLE,{title:d,titleAttributes:m},a)}},pe=u()((function(e){return{baseTag:X([k,j],e),bodyAttributes:G(y,e),defer:$(e,M),encode:$(e,R),htmlAttributes:G(g,e),linkTags:Z(E.LINK,[S,k],e),metaTags:Z(E.META,[O,T,C,x,A],e),noscriptTags:Z(E.NOSCRIPT,[N],e),onChangeClientState:J(e),scriptTags:Z(E.SCRIPT,[L,N],e),styleTags:Z(E.STYLE,[w],e),title:z(e),titleAttributes:G(b,e)}}),(function(e){re&&ne(re),e.defer?re=te((function(){ie(e,(function(){re=null}))})):(ie(e),re=null)}),fe)((function(){return null})),ve=(r=pe,o=i=function(e){function t(){return V(this,t),_(this,e.apply(this,arguments))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,e),t.prototype.shouldComponentUpdate=function(e){return!m()(this.props,e)},t.prototype.mapNestedChildrenToProps=function(e,t){if(!t)return null;switch(e.type){case E.SCRIPT:case E.NOSCRIPT:return{innerHTML:t};case E.STYLE:return{cssText:t}}throw new Error("<"+e.type+" /> elements are self-closing and can not contain children. Refer to our API for more information.")},t.prototype.flattenArrayTypeChildren=function(e){var t,n=e.child,a=e.arrayTypeChildren,r=e.newChildProps,i=e.nestedChildren;return U({},a,((t={})[n.type]=[].concat(a[n.type]||[],[U({},r,this.mapNestedChildrenToProps(n,i))]),t))},t.prototype.mapObjectTypeChildren=function(e){var t,n,a=e.child,r=e.newProps,i=e.newChildProps,o=e.nestedChildren;switch(a.type){case E.TITLE:return U({},r,((t={})[a.type]=o,t.titleAttributes=U({},i),t));case E.BODY:return U({},r,{bodyAttributes:U({},i)});case E.HTML:return U({},r,{htmlAttributes:U({},i)})}return U({},r,((n={})[a.type]=U({},i),n))},t.prototype.mapArrayTypeChildrenToProps=function(e,t){var n=U({},t);return Object.keys(e).forEach((function(t){var a;n=U({},n,((a={})[t]=e[t],a))})),n},t.prototype.warnOnInvalidChildren=function(e,t){return!0},t.prototype.mapChildrenToProps=function(e,t){var n=this,a={};return p.a.Children.forEach(e,(function(e){if(e&&e.props){var r=e.props,i=r.children,o=function(e){var t=arguments.length>1&&void 0!==arguments[1]?arguments[1]:{};return Object.keys(e).reduce((function(t,n){return t[D[n]||n]=e[n],t}),t)}(Y(r,["children"]));switch(n.warnOnInvalidChildren(e,i),e.type){case E.LINK:case E.META:case E.NOSCRIPT:case E.SCRIPT:case E.STYLE:a=n.flattenArrayTypeChildren({child:e,arrayTypeChildren:a,newChildProps:o,nestedChildren:i});break;default:t=n.mapObjectTypeChildren({child:e,newProps:t,newChildProps:o,nestedChildren:i})}}})),t=this.mapArrayTypeChildrenToProps(a,t)},t.prototype.render=function(){var e=this.props,t=e.children,n=Y(e,["children"]),a=U({},n);return t&&(a=this.mapChildrenToProps(t,a)),p.a.createElement(r,a)},K(t,null,[{key:"canUseDOM",set:function(e){r.canUseDOM=e}}]),t}(p.a.Component),i.propTypes={base:c.a.object,bodyAttributes:c.a.object,children:c.a.oneOfType([c.a.arrayOf(c.a.node),c.a.node]),defaultTitle:c.a.string,defer:c.a.bool,encodeSpecialCharacters:c.a.bool,htmlAttributes:c.a.object,link:c.a.arrayOf(c.a.object),meta:c.a.arrayOf(c.a.object),noscript:c.a.arrayOf(c.a.object),onChangeClientState:c.a.func,script:c.a.arrayOf(c.a.object),style:c.a.arrayOf(c.a.object),title:c.a.string,titleAttributes:c.a.object,titleTemplate:c.a.string},i.defaultProps={defer:!0,encodeSpecialCharacters:!0},i.peek=r.peek,i.rewind=function(){var e=r.rewind();return e||(e=fe({baseTag:[],bodyAttributes:{},encodeSpecialCharacters:!0,htmlAttributes:{},linkTags:[],metaTags:[],noscriptTags:[],scriptTags:[],styleTags:[],title:"",titleAttributes:{}})),e},o);ve.renderStatic=ve.rewind}).call(this,n("yLpj"))},s8M5:function(e,t,n){"use strict";var a=n("MfPe");t.a=function(){return a.data.site.siteMetadata}},yLpj:function(e,t){var n;n=function(){return this}();try{n=n||new Function("return this")()}catch(a){"object"==typeof window&&(n=window)}e.exports=n}}]);
//# sourceMappingURL=commons-31d9b819d0a922b9ddde.js.map