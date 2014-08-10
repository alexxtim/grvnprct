namespace GrosvenorPracticum_DAL.Models
{
    public interface IDishDto
    {
        /// <summary>
        /// Dish Name
        /// </summary>
        string DishName { get; set; }

        /// <summary>
        /// Dish Type Id
        /// </summary>
        string DishTypeId { get; set; }

        /// <summary>
        /// Wheter the logic allows to servie multiple count for a particular dish 
        /// </summary>
        bool IsMultiple { get; set; }

        /// <summary>
        /// Get Dish Name with a count of the dish
        /// </summary>
        /// <param name="dishCount">a dish count</param>
        /// <returns></returns>
        string GetFullDishName(int dishCount);
    }
}