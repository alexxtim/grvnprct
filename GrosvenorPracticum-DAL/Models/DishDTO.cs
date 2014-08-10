namespace GrosvenorPracticum_DAL.Models
{
    public class DishDto : IDishDto
    {
        public string DishName { get; set; }
        public string DishTypeId { get; set; }
        public bool IsMultiple { get; set; }

        public string GetFullDishName(int dishCount)
        {
            return string.Format("{0}{1}", DishName, dishCount > 1 && IsMultiple ? string.Format("(x{0})", dishCount) : string.Empty);
        }
    }
}
