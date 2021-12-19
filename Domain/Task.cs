using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Task:BaseModel
    {
        [Required]
        public int FlowId { get; set; }

        [Required]
        public int StateId { get; set; }

        public int CurrentStateIndex { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
