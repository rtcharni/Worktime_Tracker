//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kirjautuminen___DataBase.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Työntekijät
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Työntekijät()
        {
            this.Kirjaukset = new HashSet<Kirjaukset>();
        }
    
        public int Käyttäjä_id { get; set; }
        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public string Käyttäjätunnus { get; set; }
        public string Salasana { get; set; }
        public System.DateTime Luomispäivä { get; set; }
        public bool Admin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kirjaukset> Kirjaukset { get; set; }
    }
}
