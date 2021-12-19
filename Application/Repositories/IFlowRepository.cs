using System;
using System.Threading.Tasks;
using Domain;

namespace Application.Repositories
{
    public interface IFlowRepository : IRepository<Flow>
    {
        Task<bool> UpdateFlowName(Flow flow);
    }
}
