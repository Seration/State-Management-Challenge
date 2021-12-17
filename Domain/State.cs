using System;
namespace Domain
{
    public class State: BaseModel
    {
        public int Id { get; set; }
        public int MainState { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }
}
