﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCaddins.ParameterUtilities {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")]
    internal sealed partial class IncrementSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static IncrementSettings defaultInstance = ((IncrementSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new IncrementSettings())));
        
        public static IncrementSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Window Instance Number")]
        public string CustomParameterName {
            get {
                return ((string)(this["CustomParameterName"]));
            }
            set {
                this["CustomParameterName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#VAL#$2")]
        public string DestinationReplacePattern {
            get {
                return ((string)(this["DestinationReplacePattern"]));
            }
            set {
                this["DestinationReplacePattern"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(^\\d*)(.*$)")]
        public string DestinationSearchPattern {
            get {
                return ((string)(this["DestinationSearchPattern"]));
            }
            set {
                this["DestinationSearchPattern"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int IncrementValue {
            get {
                return ((int)(this["IncrementValue"]));
            }
            set {
                this["IncrementValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int OffsetValue {
            get {
                return ((int)(this["OffsetValue"]));
            }
            set {
                this["OffsetValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("$1")]
        public string SourceReplacePattern {
            get {
                return ((string)(this["SourceReplacePattern"]));
            }
            set {
                this["SourceReplacePattern"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(^\\d*).*$")]
        public string SourceSearchPattern {
            get {
                return ((string)(this["SourceSearchPattern"]));
            }
            set {
                this["SourceSearchPattern"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UseCustomParameterName {
            get {
                return ((bool)(this["UseCustomParameterName"]));
            }
            set {
                this["UseCustomParameterName"] = value;
            }
        }
    }
}
