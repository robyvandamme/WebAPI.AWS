namespace API.Model
{
    public enum Category
    {
        Unspecified,
        Books,
        Apps,
        Sites
    }

    public static class CategoryExtensions
    {
        public static bool IsSpecified(this Category category)
        {
            return category != Category.Unspecified;
        }
    }
}