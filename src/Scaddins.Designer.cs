﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCaddins {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ICSharpCode.SettingsEditor.SettingsCodeGeneratorTool", "5.0.0.4755")]
    internal sealed partial class Scaddins : global::System.Configuration.ApplicationSettingsBase {
        
        private static Scaddins defaultInstance = ((Scaddins)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Scaddins())));
        
        public static Scaddins Default {
            get {
                return defaultInstance;
            }
        }
        
        /// <summary>
        /// test
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("test")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public global::System.Collections.Specialized.StringCollection DisplayOrder {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["DisplayOrder"]));
            }
            set {
                this["DisplayOrder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DisplaySCexport {
            get {
                return ((bool)(this["DisplaySCexport"]));
            }
            set {
                this["DisplaySCexport"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DisplayScoord {
            get {
                return ((bool)(this["DisplayScoord"]));
            }
            set {
                this["DisplayScoord"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DisplaySCulcase {
            get {
                return ((bool)(this["DisplaySCulcase"]));
            }
            set {
                this["DisplaySCulcase"] = value;
            }
        }
    }
}
