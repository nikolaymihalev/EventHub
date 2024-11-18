using EventHub.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Infrastructure.Models
{
    /// <summary>
    /// Represents a category of events.
    /// </summary>
    [Comment("Represents a category")]
    public class Category
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        [Comment("Category's identifier")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        [Required]
        [Comment("Category's name")]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Events that belong to this category.
        /// </summary>
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
