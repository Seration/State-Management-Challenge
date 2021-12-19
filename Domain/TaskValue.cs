using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class TaskValue: BaseModel
    {
        public int TaskId { get; set; }
        public int StateId { get; set; }
        public string Value { get; set; }
    }
}
