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
    
    public partial class Kirjaukset
    {
        public int Kirjaus_id { get; set; }
        public int Käyttäjä_id { get; set; }
        public System.DateTime Kirjauspäivä { get; set; }
        public System.DateTime Päivä { get; set; }
        public System.TimeSpan Aloitusaika { get; set; }
        public System.TimeSpan Lopetusaika { get; set; }
        public string Lisätiedot { get; set; }
    
        public virtual Työntekijät Työntekijät { get; set; }
    }
}
