﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.Areas.Identity.Pages.Account.Manage {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SetPassword {

        private static System.Resources.ResourceManager resourceMan;

        private static System.Globalization.CultureInfo resourceCulture;

        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SetPassword() {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Resources.Areas.Identity.Pages.Account.Manage.SetPassword", typeof(SetPassword).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        public static string SetPasswordTitle {
            get {
                return ResourceManager.GetString("SetPasswordTitle", resourceCulture);
            }
        }

        public static string SetYourPassword {
            get {
                return ResourceManager.GetString("SetYourPassword", resourceCulture);
            }
        }

        public static string NoLocalUserOnThisSite {
            get {
                return ResourceManager.GetString("NoLocalUserOnThisSite", resourceCulture);
            }
        }

        public static string NewPassword {
            get {
                return ResourceManager.GetString("NewPassword", resourceCulture);
            }
        }

        public static string ConfirmPassword {
            get {
                return ResourceManager.GetString("ConfirmPassword", resourceCulture);
            }
        }

        public static string SetPasswordSubmit {
            get {
                return ResourceManager.GetString("SetPasswordSubmit", resourceCulture);
            }
        }
    }
}
