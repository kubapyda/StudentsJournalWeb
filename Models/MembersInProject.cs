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
    
    public partial class MembersInProject
    {
        public decimal lead_ID { get; set; }
        public decimal student_ID { get; set; }
        public decimal project_ID { get; set; }
        public Nullable<bool> members_approved { get; set; }
    
        public virtual Leaders Leaders { get; set; }
        public virtual Projects Projects { get; set; }
        public virtual Students Students { get; set; }
    }
}
