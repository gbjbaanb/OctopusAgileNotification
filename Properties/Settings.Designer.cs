﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OctopusAgileNotification.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.8.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.octopus.energy/v1/products/")]
        public string OctopusBaseURL {
            get {
                return ((string)(this["OctopusBaseURL"]));
            }
            set {
                this["OctopusBaseURL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AGILE-18-02-21")]
        public string ProductCode {
            get {
                return ((string)(this["ProductCode"]));
            }
            set {
                this["ProductCode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("E-1R-AGILE-18-02-21-C")]
        public string TariffCode {
            get {
                return ((string)(this["TariffCode"]));
            }
            set {
                this["TariffCode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("[{\"textColour\":\"-1\",\"backColour\":\"-16776961\",\"threshold\":0},{\"textColour\":\"-16744" +
            "448\",\"backColour\":\"0\",\"threshold\":15},{\"textColour\":\"-16777216\",\"backColour\":\"-3" +
            "2768\",\"threshold\":24},{\"textColour\":\"-1\",\"backColour\":\"-65536\",\"threshold\":999}]" +
            "")]
        public string Thresholds {
            get {
                return ((string)(this["Thresholds"]));
            }
            set {
                this["Thresholds"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\"[FontFamily: Name=Segoe UI]\",18,0}")]
        public string Font {
            get {
                return ((string)(this["Font"]));
            }
            set {
                this["Font"] = value;
            }
        }
    }
}
