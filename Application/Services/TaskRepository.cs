using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class TaskRepository : Repository<Domain.Task>, ITaskRepository
    {
        public TaskRepository(DataContext context) : base(context)
        {

        }

        public async Task<Tuple<int, string>> BacwardTaskAsync(Domain.Task task)
        {
            Domain.Task currentTask = await _context.Set<Domain.Task>().FindAsync(task.Id);

            if (task.CurrentStateIndex == 1)
                return Tuple.Create(-1, "");

            task.CurrentStateIndex--;
            await _context.SaveChangesAsync();

            var taskValue = await _context.Set<TaskValue>()
                                          .Where(s => s.StateId == task.CurrentStateIndex && s.TaskId == task.Id)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync();

            return Tuple.Create(task.CurrentStateIndex, taskValue.Value);
        }

        public async Task<Tuple<int, string>> ForwardTaskAsync(Domain.Task task)
        {
            int stateCountInFlow = _context.Set<Domain.State>().Count(s => s.FlowId == task.FlowId);

            if (task.CurrentStateIndex == stateCountInFlow)
                return Tuple.Create(-1, "");

            task.CurrentStateIndex++;

            var taskValue = await _context.Set<TaskValue>()
                                          .Where(s => s.StateId == task.StateId && s.TaskId == task.Id)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync();

            if (taskValue != null)
            {
                await _context.SaveChangesAsync();
                return Tuple.Create(task.CurrentStateIndex, taskValue.Value);
            }

            var nextState = await _context.Set<State>()
                     .Where(s => s.Index == task.CurrentStateIndex)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

            var value = Guid.NewGuid().ToString().Substring(0, 4);

            await _context.Set<TaskValue>().AddAsync(new TaskValue
            {
                StateId = nextState.Id,
                TaskId = task.Id,
                Value = value
            });

            await _context.SaveChangesAsync();

            return Tuple.Create(task.CurrentStateIndex++, value);
        }

        public async Task<IEnumerable<Domain.Task>> GetAllTaskBelongTheFlowAsync(int flowId)
        {
            return await _context.Set<Domain.Task>().Where(x => x.FlowId == flowId).ToListAsync();
        }

        public async Task<IEnumerable<Domain.Task>> GetAllTaskBelongTheStateAsync(int stateId)
        {
            return await _context.Set<Domain.Task>().Where(x => x.StateId == stateId).ToListAsync();
        }
    }

}
