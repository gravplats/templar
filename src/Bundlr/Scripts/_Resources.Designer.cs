﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bundlr.Scripts {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class _Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal _Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Bundlr.Scripts._Resources", typeof(_Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*
        /// *  Copyright 2011 Twitter, Inc.
        /// *  Licensed under the Apache License, Version 2.0 (the &quot;License&quot;);
        /// *  you may not use this file except in compliance with the License.
        /// *  You may obtain a copy of the License at
        /// *
        /// *  http://www.apache.org/licenses/LICENSE-2.0
        /// *
        /// *  Unless required by applicable law or agreed to in writing, software
        /// *  distributed under the License is distributed on an &quot;AS IS&quot; BASIS,
        /// *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
        /// *  See the  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string hogan {
            get {
                return ResourceManager.GetString("hogan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to var compile = function(template) {
        ///    return Hogan.compile(template, { asString: true });
        ///};.
        /// </summary>
        internal static string hogan_compiler {
            get {
                return ResourceManager.GetString("hogan_compiler", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to //     Underscore.js 1.5.2
        /////     http://underscorejs.org
        /////     (c) 2009-2013 Jeremy Ashkenas, DocumentCloud and Investigative Reporters &amp; Editors
        /////     Underscore may be freely distributed under the MIT license.
        ///
        ///(function () {
        ///
        ///    // Baseline setup
        ///    // --------------
        ///
        ///    // Establish the root object, `window` in the browser, or `exports` on the server.
        ///    var root = this;
        ///
        ///    // Save the previous value of the `_` variable.
        ///    var previousUnderscore = root._;
        ///
        ///    // Establish the [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string underscore {
            get {
                return ResourceManager.GetString("underscore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to var compile = function(template) {
        ///    var tmpl = _.template(template);
        ///    return tmpl.source;
        ///};.
        /// </summary>
        internal static string underscore_compiler {
            get {
                return ResourceManager.GetString("underscore_compiler", resourceCulture);
            }
        }
    }
}
