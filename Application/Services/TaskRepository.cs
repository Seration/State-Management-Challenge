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

        public async Task<Tuple<int, string>> BackwardTaskAsync(int taskId)
        {
            Domain.Task currentTask = await _context.Set<Domain.Task>().FindAsync(taskId);

            if (currentTask.CurrentStateIndex == 1)
                return Tuple.Create(-1, "");

            currentTask.CurrentStateIndex = currentTask.CurrentStateIndex -1;

            var previousState = await _context.Set<State>().Where(s => s.Index == currentTask.CurrentStateIndex)
                                                           .FirstOrDefaultAsync();
            currentTask.StateId = previousState.Id;
            await _context.SaveChangesAsync();

            var taskValue = await _context.Set<TaskValue>()
                                          .Where(s => s.StateId == currentTask.StateId && s.TaskId == taskId)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync();

            return Tuple.Create(currentTask.CurrentStateIndex, taskValue.Value);
        }

        public async Task<bool> CheckStateExist(int stateId)
        {
            bool isStateExist = await _context.Set<State>().AnyAsync(s => s.Id == stateId);
            return isStateExist;
        }

        public async Task<Tuple<int, string>> ForwardTaskAsync(int taskId)
        {
            var currentTask = await _context.Set<Domain.Task>().SingleOrDefaultAsync(s => s.Id == taskId);
            int stateCountInFlow = _context.Set<Domain.State>().Count(s => s.FlowId == currentTask.FlowId);

            if (currentTask.CurrentStateIndex == stateCountInFlow)
                return Tuple.Create(-1, "");

            currentTask.CurrentStateIndex = currentTask.CurrentStateIndex + 1;

            var nextState = await _context.Set<State>().Where(s => s.Index == currentTask.CurrentStateIndex)
                                                       .FirstOrDefaultAsync();
            currentTask.StateId = nextState.Id;

            var taskValue = await _context.Set<TaskValue>()
                                          .Where(s => s.StateId == currentTask.StateId && s.TaskId == taskId)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync();

            if (taskValue != null)
            {
                await _context.SaveChangesAsync();
                return Tuple.Create(currentTask.CurrentStateIndex, taskValue.Value);
            }

            var value = Guid.NewGuid().ToString().Substring(0, 4);

            await _context.Set<TaskValue>().AddAsync(new TaskValue
            {
                StateId = nextState.Id,
                TaskId = taskId,
                Value = value
            });

            await _context.SaveChangesAsync();

            return Tuple.Create(currentTask.CurrentStateIndex, value);
        }

        public async Task<IEnumerable<Domain.Task>> GetAllTaskBelongToFlowAsync(int flowId)
        {
            return await _context.Set<Domain.Task>().Where(x => x.FlowId == flowId && x.IsActive == true).ToListAsync();
        }

        public async Task<IEnumerable<Domain.Task>> GetAllTaskBelongToStateAsync(int stateId)
        {
            return await _context.Set<Domain.Task>().Where(x => x.StateId == stateId && x.IsActive == true).ToListAsync();
        }

        public async Task<bool> UpdateTaskName(Domain.Task task)
        {
            Domain.Task currentTask = await _context.Set<Domain.Task>().SingleOrDefaultAsync(s => s.Id == task.Id);
            currentTask.Name = task.Name;

            bool IsSuccess = await _context.SaveChangesAsync() > 0;

            return IsSuccess;
        }
    }

}
