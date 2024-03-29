﻿using System.ComponentModel.DataAnnotations;

namespace TaskIt.Core.Entities
{
    public class Step
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(400)]
        public string Description { get; set; }

        [Required]
        public Guid TaskId { get; set; }

        public virtual TaskItem Task { get; set;}
    }
}