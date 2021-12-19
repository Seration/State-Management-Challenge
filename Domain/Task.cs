using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Task:BaseModel
    {
        public int FlowId { get; set; }
        public int StateId { get; set; }
        public int CurrentStateIndex { get; set; }
        public string Name { get; set; }
    }
}
