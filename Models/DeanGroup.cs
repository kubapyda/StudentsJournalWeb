//------------------------------------------------------------------------------
// <auto-generated>
//    Ten kod źródłowy został wygenerowany na podstawie szablonu.
//
//    Ręczne modyfikacje tego pliku mogą spowodować nieoczekiwane zachowanie aplikacji.
//    Ręczne modyfikacje tego pliku zostaną zastąpione w przypadku ponownego wygenerowania kodu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentsJournalWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeanGroup
    {
        public DeanGroup()
        {
            this.Students = new HashSet<Students>();
        }
    
        public decimal group_id { get; set; }
        public Nullable<decimal> department_id { get; set; }
        public string group_name { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual ICollection<Students> Students { get; set; }
    }
}
