using System;
using System.Threading.Tasks;
using Application.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class FlowRepository : Repository<Domain.Flow>, IFlowRepository
    {
        public FlowRepository(DataContext context) : base(context)
        {

        }

        public async Task<bool> UpdateFlowName(Flow flow)
        {
            Flow currentFlow = await _context.Set<Flow>().SingleOrDefaultAsync(s => s.Id == flow.Id);
            currentFlow.Name = flow.Name;

            bool IsSuccess = await _context.SaveChangesAsync() > 0;

            return IsSuccess;
        }
    }
}
