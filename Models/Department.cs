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
    
    public partial class Department
    {
        public Department()
        {
            this.DeanGroup = new HashSet<DeanGroup>();
        }
    
        public decimal department_id { get; set; }
        public Nullable<decimal> direction_id { get; set; }
        public string department_name { get; set; }
    
        public virtual ICollection<DeanGroup> DeanGroup { get; set; }
        public virtual Direction Direction { get; set; }
    }
}
