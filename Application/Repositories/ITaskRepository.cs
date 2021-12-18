using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.Repositories
{
    public interface ITaskRepository: IRepository<Domain.Task>
    {
        Task<IEnumerable<Domain.Task>> GetAllTaskBelongTheFlowAsync(int flowId);
        Task<IEnumerable<Domain.Task>> GetAllTaskBelongTheStateAsync(int stateId);
        Task<Tuple<int,string>> ForwardTaskAsync(Domain.Task task);
        Task<Tuple<int, string>> BacwardTaskAsync(Domain.Task task);
    }
}
