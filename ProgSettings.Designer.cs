﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExposureMachine {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
    internal sealed partial class ProgSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static ProgSettings defaultInstance = ((ProgSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ProgSettings())));
        
        public static ProgSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("..\\LeftCameraSettings.json")]
        public string LeftCameraSettings {
            get {
                return ((string)(this["LeftCameraSettings"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("..\\RightCameraSettings.json")]
        public string RightCameraSettings {
            get {
                return ((string)(this["RightCameraSettings"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("..\\ValveSettings.json")]
        public string ValvesSettings {
            get {
                return ((string)(this["ValvesSettings"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int ExposureTime {
            get {
                return ((int)(this["ExposureTime"]));
            }
            set {
                this["ExposureTime"] = value;
            }
        }
    }
}
