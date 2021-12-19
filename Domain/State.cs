using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class State: BaseModel
    {
        public int FlowId { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }
}
