//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LecturasRestAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ValorLeido
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public Nullable<int> LecturaId { get; set; }
        public int Valor { get; set; }
    }
}
