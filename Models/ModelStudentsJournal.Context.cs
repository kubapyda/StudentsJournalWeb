﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JournalWebEntities : DbContext
    {
        public JournalWebEntities()
            : base("name=JournalWebEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Administrators> Administrators { get; set; }
        public DbSet<ClassessType> ClassessType { get; set; }
        public DbSet<DeanGroup> DeanGroup { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Direction> Direction { get; set; }
        public DbSet<GradlesProjects> GradlesProjects { get; set; }
        public DbSet<GradlesSubjects> GradlesSubjects { get; set; }
        public DbSet<Leaders> Leaders { get; set; }
        public DbSet<MembersInProject> MembersInProject { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
