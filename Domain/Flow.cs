using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Flow : BaseModel
    {
        public string Name { get; set; }
    }
}
