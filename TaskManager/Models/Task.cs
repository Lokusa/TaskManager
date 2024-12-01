using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title is required and cannot be empty.")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Description is required and cannot be empty.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Due date must be in the future.")]
        public DateTime DueDate { get; set; }

        public bool IsComplete { get; set; } = false;

        // Ensure due date is in the future
        public class FutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is DateTime date)
                {
                    return date > DateTime.Now;
                }
                return false;
            }
        }
    }
}
