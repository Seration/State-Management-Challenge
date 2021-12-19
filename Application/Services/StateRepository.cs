using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        private DbSet<State> stateContext;

        public StateRepository(DataContext context) : base(context)
        {
            stateContext = _context.Set<State>();
        }

        public async Task<bool> ChangeStateOrderAsync(IEnumerable<ChangeStateOrderDto> newIndexList, int flowId)
        {
            bool IsThereAnyTaskOverTheFlow = await _context.Set<Domain.Task>().AnyAsync(s => s.FlowId == flowId);

            if (IsThereAnyTaskOverTheFlow)
                return false;

            foreach (var item in newIndexList)
            {
                bool isThereSameInNewIndexList = newIndexList.Count(x => x.StateIndex == item.StateIndex) > 1;

                if (isThereSameInNewIndexList)
                    return false;
            }


            var flowStates = await stateContext.Where(x => x.FlowId == flowId).ToListAsync();

            foreach (var item in flowStates)
            {
                var state = await stateContext.FindAsync(item.Id);
                state.Index = newIndexList.Where(x => x.StateId == state.Id).FirstOrDefault().StateIndex;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<State>> GetAllStateBelongToFlowAsync(int flowId)
        {
            return await stateContext.Where(x => x.FlowId == flowId && x.IsActive == true)
                                     .AsNoTracking().ToListAsync();
        }

        public async Task<int> GetNextStateIndexAsync(int flowId)
        {
            bool isThereAnyStateInFlow = await stateContext.Where(s=> s.IsActive == true)
                                                           .AnyAsync(s => s.FlowId == flowId);

            if (isThereAnyStateInFlow)
                return stateContext.Max(s => s.Index) + 1;

            return 1;
        }

        public async Task<bool> UpdateStateName(State state)
        {
            State currentState = await stateContext.SingleOrDefaultAsync(s => s.Id == state.Id);
            currentState.Name = state.Name;

            bool IsSuccess = await _context.SaveChangesAsync() > 0;

            return IsSuccess;
        }
    }
}
