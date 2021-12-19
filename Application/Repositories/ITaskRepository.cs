using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.Repositories
{
    public interface ITaskRepository: IRepository<Domain.Task>
    {
        Task<IEnumerable<Domain.Task>> GetAllTaskBelongToFlowAsync(int flowId);
        Task<IEnumerable<Domain.Task>> GetAllTaskBelongToStateAsync(int stateId);
        Task<Tuple<int,string>> ForwardTaskAsync(Domain.Task task);
        Task<Tuple<int, string>> BackwardTaskAsync(Domain.Task task);
        Task<bool> UpdateTaskName(Domain.Task task);
    }
}
