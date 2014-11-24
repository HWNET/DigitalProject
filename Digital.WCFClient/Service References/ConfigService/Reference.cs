﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Digital.WCFClient.ConfigService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MenuModel", Namespace="http://schemas.datacontract.org/2004/07/Digital.Contact.Models")]
    [System.SerializableAttribute()]
    public partial class MenuModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MenuNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OtherNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ParentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StyleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UrlField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MenuName {
            get {
                return this.MenuNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MenuNameField, value) != true)) {
                    this.MenuNameField = value;
                    this.RaisePropertyChanged("MenuName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OtherName {
            get {
                return this.OtherNameField;
            }
            set {
                if ((object.ReferenceEquals(this.OtherNameField, value) != true)) {
                    this.OtherNameField = value;
                    this.RaisePropertyChanged("OtherName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ParentId {
            get {
                return this.ParentIdField;
            }
            set {
                if ((this.ParentIdField.Equals(value) != true)) {
                    this.ParentIdField = value;
                    this.RaisePropertyChanged("ParentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Style {
            get {
                return this.StyleField;
            }
            set {
                if ((object.ReferenceEquals(this.StyleField, value) != true)) {
                    this.StyleField = value;
                    this.RaisePropertyChanged("Style");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Url {
            get {
                return this.UrlField;
            }
            set {
                if ((object.ReferenceEquals(this.UrlField, value) != true)) {
                    this.UrlField = value;
                    this.RaisePropertyChanged("Url");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ConfigService.IConfigService", SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IConfigService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IConfigService/GetMenuList", ReplyAction="http://tempuri.org/IConfigService/GetMenuListResponse")]
        System.Collections.ObjectModel.ObservableCollection<Digital.WCFClient.ConfigService.MenuModel> GetMenuList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IConfigService/GetMenuList", ReplyAction="http://tempuri.org/IConfigService/GetMenuListResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Digital.WCFClient.ConfigService.MenuModel>> GetMenuListAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IConfigServiceChannel : Digital.WCFClient.ConfigService.IConfigService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConfigServiceClient : System.ServiceModel.ClientBase<Digital.WCFClient.ConfigService.IConfigService>, Digital.WCFClient.ConfigService.IConfigService {
        
        public ConfigServiceClient() {
        }
        
        public ConfigServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ConfigServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConfigServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConfigServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.ObjectModel.ObservableCollection<Digital.WCFClient.ConfigService.MenuModel> GetMenuList() {
            return base.Channel.GetMenuList();
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<Digital.WCFClient.ConfigService.MenuModel>> GetMenuListAsync() {
            return base.Channel.GetMenuListAsync();
        }
    }
}
