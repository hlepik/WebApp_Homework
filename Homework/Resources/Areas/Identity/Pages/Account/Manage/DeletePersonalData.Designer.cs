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
    public class DeletePersonalData {

        private static System.Resources.ResourceManager resourceMan;

        private static System.Globalization.CultureInfo resourceCulture;

        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DeletePersonalData() {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Resources.Areas.Identity.Pages.Account.Manage.DeletePersonalData", typeof(DeletePersonalData).Assembly);
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

        public static string DeleteData {
            get {
                return ResourceManager.GetString("DeleteData", resourceCulture);
            }
        }

        public static string PermanentlyRemoveDataExplanation {
            get {
                return ResourceManager.GetString("PermanentlyRemoveDataExplanation", resourceCulture);
            }
        }

        public static string DeleteAndClose {
            get {
                return ResourceManager.GetString("DeleteAndClose", resourceCulture);
            }
        }

        public static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
    }
}