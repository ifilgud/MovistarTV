﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.DvbIpTv.ChannelList.Properties {
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
    internal class Texts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Texts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Project.DvbIpTv.ChannelList.Properties.Texts", typeof(Texts).Assembly);
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
        ///   Looks up a localized string similar to movistar+ channels and services - DVB-IPTV (1.5“Kruger 60” Alpha 4).
        /// </summary>
        internal static string AppCaption {
            get {
                return ResourceManager.GetString("AppCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DVB-IPTV: movistar+ channels and services.
        /// </summary>
        internal static string AppName {
            get {
                return ResourceManager.GetString("AppName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Alpha (experimental).
        /// </summary>
        internal static string AppStatus {
            get {
                return ResourceManager.GetString("AppStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 1.5 “Kruger 60” Alpha 4.
        /// </summary>
        internal static string AppVersion {
            get {
                return ResourceManager.GetString("AppVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to obtain the list of TV channels/services.
        /// </summary>
        internal static string BroadcastListUnableRefresh {
            get {
                return ResourceManager.GetString("BroadcastListUnableRefresh", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Obtaining list of TV channels/services....
        /// </summary>
        internal static string BroadcastObtainingList {
            get {
                return ResourceManager.GetString("BroadcastObtainingList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parsing and extracting the list of TV channels/services....
        /// </summary>
        internal static string BroadcastParsingList {
            get {
                return ResourceManager.GetString("BroadcastParsingList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Properties of the TV channel/service.
        /// </summary>
        internal static string BroadcastServiceProperties {
            get {
                return ResourceManager.GetString("BroadcastServiceProperties", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The verification of available TV channels and services is in progress.
        ///The verification has to be canceled before you can perform the requested action..
        /// </summary>
        internal static string ChannelFormActiveScan {
            get {
                return ResourceManager.GetString("ChannelFormActiveScan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Services verification is in progress.
        /// </summary>
        internal static string ChannelFormActiveScanCaption {
            get {
                return ResourceManager.GetString("ChannelFormActiveScanCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The channel list can be outdated. It is over {0} days old..
        /// </summary>
        internal static string ChannelListAgeObsolete {
            get {
                return ResourceManager.GetString("ChannelListAgeObsolete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The channel list is {0} days old..
        /// </summary>
        internal static string ChannelListAgeOld {
            get {
                return ResourceManager.GetString("ChannelListAgeOld", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The channel list has been obtained from the cache, but it is empty..
        /// </summary>
        internal static string ChannelListCacheEmpty {
            get {
                return ResourceManager.GetString("ChannelListCacheEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No cached version of the channel list exists. You have to refresh the list..
        /// </summary>
        internal static string ChannelListNoCache {
            get {
                return ResourceManager.GetString("ChannelListNoCache", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The program is about to proceed to download the electronic program guide (EPG) information for the first time. The download of the EPG is a very slow process (between 10 and 15 minutes).
        ///
        ///During the download process, the EPG information will either be unavailable or incomplete.
        ///In subsequent executions of the program, the EPG information will be updated in the background automatically (if more than {0} hours have ellapsed since the last update)..
        /// </summary>
        internal static string EpgDownloadFirstTime {
            get {
                return ResourceManager.GetString("EpgDownloadFirstTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to At least one inactive service is now active. The list will be refreshed to show these new active services..
        /// </summary>
        internal static string MulticastScannerScanCompleteRefresh {
            get {
                return ResourceManager.GetString("MulticastScannerScanCompleteRefresh", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to start the application.
        /// </summary>
        internal static string MyAppCtxExceptionCaption {
            get {
                return ResourceManager.GetString("MyAppCtxExceptionCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error has occurred while initializating the application..
        /// </summary>
        internal static string MyAppCtxExceptionMsg {
            get {
                return ResourceManager.GetString("MyAppCtxExceptionMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application configuration error.
        /// </summary>
        internal static string MyAppCtxInitializationErrorCaption {
            get {
                return ResourceManager.GetString("MyAppCtxInitializationErrorCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Starting application....
        /// </summary>
        internal static string MyAppCtxStarting {
            get {
                return ResourceManager.GetString("MyAppCtxStarting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error has occurred while processing your request.
        /// </summary>
        internal static string MyAppHandleExceptionDefaultCaption {
            get {
                return ResourceManager.GetString("MyAppHandleExceptionDefaultCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No information has been supplied as to where or why the error occurred. Find error details below:.
        /// </summary>
        internal static string MyAppHandleExceptionDefaultMessage {
            get {
                return ResourceManager.GetString("MyAppHandleExceptionDefaultMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error has occured while loading the application configuration.
        ///The application will now end..
        /// </summary>
        internal static string MyAppLoadConfigException {
            get {
                return ResourceManager.GetString("MyAppLoadConfigException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application configuration error.
        /// </summary>
        internal static string MyAppLoadConfigExceptionCaption {
            get {
                return ResourceManager.GetString("MyAppLoadConfigExceptionCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to find the &apos;Recorder Launcher&apos; program at &apos;{0}&apos;..
        /// </summary>
        internal static string MyAppRecorderLauncherNotFound {
            get {
                return ResourceManager.GetString("MyAppRecorderLauncherNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;Description has not been provided&gt;.
        /// </summary>
        internal static string NotProvidedDescription {
            get {
                return ResourceManager.GetString("NotProvidedDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;Not provided&gt;.
        /// </summary>
        internal static string NotProvidedValue {
            get {
                return ResourceManager.GetString("NotProvidedValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No service provider has been selected.
        /// </summary>
        internal static string NotSelectedServiceProvider {
            get {
                return ResourceManager.GetString("NotSelectedServiceProvider", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to open browser for URL &apos;{0}&apos;.
        ///
        ///{1}.
        /// </summary>
        internal static string OpenUrlError {
            get {
                return ResourceManager.GetString("OpenUrlError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Channel description and data.
        /// </summary>
        internal static string Payload02DisplayName {
            get {
                return ResourceManager.GetString("Payload02DisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Packages and channel numbers.
        /// </summary>
        internal static string Payload05DisplayName {
            get {
                return ResourceManager.GetString("Payload05DisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The selected TV channel has been marked as inactive and might not be available.
        ///
        ///Do you still want to schedule a recording of {0}?.
        /// </summary>
        internal static string RecordDeadTvChannel {
            get {
                return ResourceManager.GetString("RecordDeadTvChannel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The recording task has been sucessfully created..
        /// </summary>
        internal static string SchedulerCreateTaskOk {
            get {
                return ResourceManager.GetString("SchedulerCreateTaskOk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to movistar+ DVB-IPTV decoder version 1.5 “Kruger 60” Alpha 4
        ///
        ///NOTICE: This software IS NOT SUPPORTED NOR ENDORSED by Movistar or Telefónica. Telefónica de España has no responsibility if there are channels that can not be seen on the PC, even if they are part of contracted services.
        ///DO NOT EVER CALL 1002 nor 1004 if channels disappear from the list or can not be seen or any other abnormality. Telefónica only provides help and support for their physical decoder and only for the contracted services.
        ///
        ///This  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SolutionLicense {
            get {
                return ResourceManager.GetString("SolutionLicense", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {\rtf1\ansi\ansicpg1252\deff0\deflang3082{\fonttbl{\f0\fnil\fcharset0 Segoe UI;}{\f1\fnil\fcharset2 Symbol;}}
        ///{\colortbl ;\red0\green0\blue255;}
        ///{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\nowidctlpar\sa200\sl276\slmult1\qj\lang10\b\f0\fs22 movistar+ DVB-IPTV decoder version 1.5 \ldblquote Kruger 60\rdblquote  Alpha 3c\ul\fs18\par
        ///NOTICE:\ulnone\b0  \b\i This software IS NOT SUPPORTED NOR ENDORSED by Movistar or Telef\&apos;f3nica\b0\i0 . Telef\&apos;f3nica de Espa\&apos;f1a has no responsibility if there  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SolutionLicenseRtf {
            get {
                return ResourceManager.GetString("SolutionLicenseRtf", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to obtain the list of service providers.
        /// </summary>
        internal static string SPListUnableRefresh {
            get {
                return ResourceManager.GetString("SPListUnableRefresh", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Obtaining the list of service providers....
        /// </summary>
        internal static string SPObtainingList {
            get {
                return ResourceManager.GetString("SPObtainingList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parsing and extracting the list of providers....
        /// </summary>
        internal static string SPParsingList {
            get {
                return ResourceManager.GetString("SPParsingList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Properties of the service provider.
        /// </summary>
        internal static string SPProperties {
            get {
                return ResourceManager.GetString("SPProperties", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The refreshing of the list has been cancelled by the user..
        /// </summary>
        internal static string UserCancelListRefresh {
            get {
                return ResourceManager.GetString("UserCancelListRefresh", resourceCulture);
            }
        }
    }
}
