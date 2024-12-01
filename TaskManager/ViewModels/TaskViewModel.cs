using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels
{
    public class TaskViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Due date must be in the future.")]
        public DateTime DueDate { get; set; }

        public bool IsComplete { get; set; }
    }

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
