using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;
using Domain;

namespace Application.Repositories
{
    public interface IStateRepository: IRepository<State>
    {
        Task<IEnumerable<Domain.State>> GetAllStateBelongTheFlowAsync(int flowId);
        Task<bool> ChangeStateOrderAsync(IEnumerable<ChangeStateOrderDto> newIndexList, int flowId);
    }
}
