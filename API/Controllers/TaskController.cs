using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskValueRepository _taskValueRepository;

        public TaskController(ITaskRepository taskRepository, ITaskValueRepository taskValueRepository)
        {
            _taskRepository = taskRepository;
            _taskValueRepository = taskValueRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Domain.Task>> TaksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Domain.Task> TaskAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> CreateTaskAsync([FromBody] Domain.Task task)
        {
            var state = _taskRepository.CheckStateExist(task.StateId);

            if (state == null)
                return false;

            task.CurrentStateIndex = 1;

            Domain.Task createdTask = await _taskRepository.AddWithReturnAsync(task);

            string value = Guid.NewGuid().ToString().Substring(0, 4);

            return await _taskValueRepository.AddAsync(new TaskValue
            {
                StateId = task.StateId,
                TaskId = createdTask.Id,
                Value = value
            });

        }

        [HttpPut]
        public async Task<bool> PutAsync([FromBody] Domain.Task task)
        {
            return await _taskRepository.UpdateTaskName(task);
        }

        [HttpDelete("{id}")]
        public async Task<bool> RemoveTaskAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

        [HttpGet("BelongToFlow/{flowId}")]
        public async Task<IEnumerable<Domain.Task>> TasksBelongTheFlowAsync(int flowId)
        {
            return await _taskRepository.GetAllTaskBelongToFlowAsync(flowId);
        }

        [HttpGet("BelongToState/{stateId}")]
        public async Task<IEnumerable<Domain.Task>> TasksBelongTheStateAsync(int stateId)
        {
            return await _taskRepository.GetAllTaskBelongToStateAsync(stateId);
        }

        [HttpPost("ForwardTask")]
        public async Task<Tuple<int, string>> ForwardTaskAsync(int taskId)
        {
            return await _taskRepository.ForwardTaskAsync(taskId);
        }

        [HttpPost("BackwardTask")]
        public async Task<Tuple<int, string>> BackwardTaskAsync(int taskId)
        {
            return await _taskRepository.BackwardTaskAsync(taskId);
        }
    }
}
