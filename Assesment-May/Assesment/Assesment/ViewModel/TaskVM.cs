using Assesment.Models;
using System.ComponentModel.DataAnnotations;

namespace Assesment.ViewModel
{
    public class TaskVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter Title")]
        [UniqueTitle(typeof(TaskManagerContext), ErrorMessage = "The title already exists. Please change it")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter DueDate")]
        [FutureDate(ErrorMessage = "DueDate must be in the future")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Please enter AttachmentType")]
        public AttachmentType AttachmentsType { get; set; }

        public DateTime TaskCreatedDate { get; set; }

        //[Required(ErrorMessage = "Please enter Task Created By")]
        //[MaxLength(150, ErrorMessage = "Task Created By must be less than 150 characters")]
        //[MinLength(5, ErrorMessage = "Task Created By must be at least 5 characters")]
        public string? TaskCreatedBy { get; set; }

        public string? Material { get; set; }

    }
}
