namespace GrosvenorPracticum_BL
{
    public interface IFoodServiceManager
    {
        /// <summary>
        /// Get completed dish list string 
        /// </summary>
        /// <param name="dishes">raw string with a day type and dishes separated by delimiter</param>
        /// <returns></returns>
        string GetDishes(string dishes);
    }
}