using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Entities
{
    public class Task
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime From { get; set; }
        [Required]
        public DateTime Until { get; set; }
        [Required]
        public Group Group { get; set; }       

    }
}
