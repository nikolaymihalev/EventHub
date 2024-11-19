namespace EventHub.Core.Models.Category
{
    /// <summary>
    /// Model for Category information
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
