﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int ExposureTime {
            get {
                return ((int)(this["ExposureTime"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LeftCameraXMirror {
            get {
                return ((bool)(this["LeftCameraXMirror"]));
            }
            set {
                this["LeftCameraXMirror"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LeftCameraYMirror {
            get {
                return ((bool)(this["LeftCameraYMirror"]));
            }
            set {
                this["LeftCameraYMirror"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool RightCameraXMirror {
            get {
                return ((bool)(this["RightCameraXMirror"]));
            }
            set {
                this["RightCameraXMirror"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool RightCameraYMirror {
            get {
                return ((bool)(this["RightCameraYMirror"]));
            }
            set {
                this["RightCameraYMirror"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LeftCamToRightChanged {
            get {
                return ((bool)(this["LeftCamToRightChanged"]));
            }
            set {
                this["LeftCamToRightChanged"] = value;
            }
        }
    }
}
