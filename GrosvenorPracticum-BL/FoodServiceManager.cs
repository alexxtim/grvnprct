using System;
using System.Collections.Generic;
using System.Linq;
using GrosvenorPracticum_DAL;
using GrosvenorPracticum_DAL.Models;

namespace GrosvenorPracticum_BL
{
    public class FoodServiceManager : IFoodServiceManager
    {
        private readonly IFoodServiceRepository _repository;

        public FoodServiceManager(IFoodServiceRepository repository)
        {
            _repository = repository;
        }

        public string GetDishes(string dishes)
        {
            bool isValidationError = true;
            var fullDishNameList = new List<string>();

            //Split the input string into an items' collection separating with a delimiter
            var itemList = dishes.Split(Char.Parse(Resource.Delimiter)).Select(x => x.Trim()).ToList();

            if (itemList.Count > 1)
            {
                var dayTime = itemList.First();
                var dishTypeIds = itemList.Skip(1).ToList();

                //Get dishes list which corresponds to da ay time and dish type ids
                var dishList = _repository.GetDishes(dayTime, dishTypeIds).ToList();

                //Get dish type ids till the point of invalid item in the input string
                var validDishTypeIdList = GetDishTypeIdListUpToPotentialError(dishTypeIds, dishList,
                    out isValidationError);

                //Order, Aggregate, Return dish full names (with dish count)
                fullDishNameList = validDishTypeIdList
                    .OrderBy(x => x.Key.DishTypeId)
                    .Select(dishDto => dishDto.Key.GetFullDishName(dishDto.Value))
                    .ToList();
            }

            //Return dish error in case of invalid input
            if (isValidationError)
                fullDishNameList.Add(Resource.InputError);
    
            //Join dishes full names with a delimiter
            return string.Join(string.Format("{0} ", Resource.Delimiter), fullDishNameList.ToArray());
        }

        private static IEnumerable<KeyValuePair<IDishDto, int>> GetDishTypeIdListUpToPotentialError(
            IEnumerable<string> dishTypeIdList, IList<IDishDto> dishList, out bool isValidationError)
        {
            isValidationError = false;
            var validDishTypeIdList = new Dictionary<IDishDto, int>();

            foreach (var dishTypeId in dishTypeIdList)
            {
                var dishDto = dishList.FirstOrDefault(dish => dish.DishTypeId == dishTypeId);
                if (dishDto == null ||
                    (!dishDto.IsMultiple && validDishTypeIdList.Count(x => x.Key.DishTypeId == dishDto.DishTypeId) > 0))
                {
                    isValidationError = true;
                    break;
                }

                if (validDishTypeIdList.ContainsKey(dishDto))
                    validDishTypeIdList[dishDto]++;
                else
                    validDishTypeIdList.Add(dishDto, 1);
            }

            return validDishTypeIdList;
        }
    }
}