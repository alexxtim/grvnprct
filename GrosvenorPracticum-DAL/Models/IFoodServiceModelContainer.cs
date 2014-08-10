using System.Data.Entity;

namespace GrosvenorPracticum_DAL.Models
{
    public interface IFoodServiceModelContainer
    {
        IDbSet<DishTypeEntity> DishTypeEntities { get; set; }
        IDbSet<DayTimeEntity> DayTimeEntities { get; set; }
        IDbSet<DishEntity> DishEntities { get; set; }
        IDbSet<DayTimeDishEntity> DayTimeDishEntities { get; set; }
        IDbSet<DishTypeDishEntity> DishTypeDishEntities { get; set; }
    }
}