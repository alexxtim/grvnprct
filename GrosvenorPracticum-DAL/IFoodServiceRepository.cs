using System.Collections.Generic;
using GrosvenorPracticum_DAL.Models;

namespace GrosvenorPracticum_DAL
{
    public interface IFoodServiceRepository
    {
        /// <summary>
        /// Get all dishes which corresponds day Time and contains in dish type ids' list
        /// </summary>
        /// <param name="dayTime">day time</param>
        /// <param name="dishTypeIdList">dish type id's list</param>
        /// <returns></returns>
        IEnumerable<IDishDto> GetDishes(string dayTime, IEnumerable<string> dishTypeIdList);
    }
}