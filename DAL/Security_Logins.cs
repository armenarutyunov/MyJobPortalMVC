//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Security_Logins : IPoco
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Security_Logins()
        {
            this.Applicant_Profiles = new HashSet<Applicant_Profiles>();
            this.Security_Logins_Log = new HashSet<Security_Logins_Log>();
            this.Security_Logins_Roles = new HashSet<Security_Logins_Roles>();
        }
    
        public System.Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public System.DateTime Created_Date { get; set; }
        public Nullable<System.DateTime> Password_Update_Date { get; set; }
        public Nullable<System.DateTime> Agreement_Accepted_Date { get; set; }
        public bool Is_Locked { get; set; }
        public bool Is_Inactive { get; set; }
        public string Email_Address { get; set; }
        public string Phone_Number { get; set; }
        public string Full_Name { get; set; }
        public bool Force_Change_Password { get; set; }
        public string Prefferred_Language { get; set; }
        public byte[] Time_Stamp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Applicant_Profiles> Applicant_Profiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security_Logins_Log> Security_Logins_Log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Security_Logins_Roles> Security_Logins_Roles { get; set; }
    }
}
