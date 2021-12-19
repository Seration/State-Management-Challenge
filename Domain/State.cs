using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class State : BaseModel
    {
        [Required]
        public int FlowId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Index { get; set; }
    }
}
