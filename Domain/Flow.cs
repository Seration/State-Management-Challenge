using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Flow : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
