using System.ComponentModel.DataAnnotations;

namespace Assesment.Models
{
    public class Tasks
    {
        [Key]
        public int ID { get; set; }

     
        //[UniqueTitle(typeof(TaskManagerContext),ErrorMessage ="The title allready exists Please change it")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public AttachmentType AttachmentsType { get; set; }

        public DateTime TaskCreatedDate { get; set; }

        public string? TaskCreatedBy { get; set; }
       [Required]
        public string? Material { get; set; }

    }
    public enum AttachmentType
    {
        Doc,
        Img
    }
}
