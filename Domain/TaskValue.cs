using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class TaskValue: BaseModel
    {
        [Required]
        public int TaskId { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
