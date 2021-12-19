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
        Task<Tuple<int,string>> ForwardTaskAsync(int taskId);
        Task<Tuple<int, string>> BackwardTaskAsync(int taskId);
        Task<bool> UpdateTaskName(Domain.Task task);
        Task<bool> CheckStateExist(int stateId);
    }
}
