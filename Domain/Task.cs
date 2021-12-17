using System;
namespace Domain
{
    public class Task:BaseModel
    {
        public int Id { get; set; }
        public int MainStateId { get; set; }
        public string Name { get; set; }
    }
}
