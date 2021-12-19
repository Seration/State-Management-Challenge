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
        public StateRepository(DataContext context): base(context)
        {

        }

        public async Task<bool> ChangeStateOrderAsync(IEnumerable<ChangeStateOrderDto> newIndexList, int flowId)
        {
            bool IsThereAnyTaskOverTheFlow = await _context.Set<State>().AnyAsync(s => s.FlowId == flowId);

            if (IsThereAnyTaskOverTheFlow)
                return false;

            var flowStates = await _context.Set<State>().Where(x => x.FlowId == flowId).ToListAsync();

            foreach (var item in flowStates)
            {
                var state = await _context.Set<State>().FindAsync(item.Id);
                state.Index = newIndexList.Where(x => x.StateId == state.Id).FirstOrDefault().StateIndex;
            }

            return  await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<State>> GetAllStateBelongToFlowAsync(int flowId)
        {
           return await _context.Set<State>().Where(x => x.FlowId == flowId).ToListAsync();
        }

        public async Task<bool> UpdateStateName(State state)
        {
            State currentState = await _context.Set<State>().SingleOrDefaultAsync(s => s.Id == state.Id);
            currentState.Name = state.Name;

            bool IsSuccess = await _context.SaveChangesAsync() > 0;

            return IsSuccess;
        }
    }
}
