//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GrosvenorPracticum_DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DishEntity
    {
        public DishEntity()
        {
            this.DayTimeDishes = new HashSet<DayTimeDishEntity>();
            this.DishTypeDishEntities = new HashSet<DishTypeDishEntity>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<DayTimeDishEntity> DayTimeDishes { get; set; }
        public virtual ICollection<DishTypeDishEntity> DishTypeDishEntities { get; set; }
    }
}
