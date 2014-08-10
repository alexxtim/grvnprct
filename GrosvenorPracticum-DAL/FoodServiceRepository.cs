using System.Collections.Generic;
using System.Linq;
using GrosvenorPracticum_DAL.Models;

namespace GrosvenorPracticum_DAL
{
    public class FoodServiceRepository : IFoodServiceRepository
    {
        private readonly IFoodServiceModelContainer _container;

        public FoodServiceRepository(IFoodServiceModelContainer foodServiceModelContainer)
        {
            _container = foodServiceModelContainer;
        }

        public IEnumerable<IDishDto> GetDishes(string dayTime, IEnumerable<string> dishTypeIdList)
        {
            var dishList = from dish in _container.DishEntities
                           join dayTimeDish in _container.DayTimeDishEntities on dish equals dayTimeDish.Dish
                           join dishTypeDish in _container.DishTypeDishEntities on dish equals dishTypeDish.DishEntity
                           where
                               dayTimeDish.DayTime.TimeName == dayTime &&
                               dishTypeIdList.Contains(dishTypeDish.DishTypeEntity.TypeId)
                select new DishDto
                {
                    DishName = dish.Name,
                    DishTypeId = dishTypeDish.DishTypeEntity.TypeId,
                    IsMultiple = dayTimeDish.IsMultiple
                };

            return dishList.ToList();
        }
    }
}