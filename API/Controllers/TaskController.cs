using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
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
            return await _taskRepository.AddAsync(task);
        }

        [HttpPut("{id}")]
        public async Task<bool> PutAsync([FromBody] Domain.Task task)
        {
            return await _taskRepository.UpdateTaskName(task);
        }

        [HttpDelete("{id}")]
        public async Task<bool>RemoveTaskAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

        [HttpGet("BelongToFlow/{id}")]
        public async Task<IEnumerable<Domain.Task>> TasksBelongTheFlowAsync(int flowId)
        {
           return await _taskRepository.GetAllTaskBelongToFlowAsync(flowId);
        }

        [HttpGet("BelongToState/{id}")]
        public async Task<IEnumerable<Domain.Task>> TasksBelongTheStateAsync(int stateId)
        {
            return await _taskRepository.GetAllTaskBelongToStateAsync(stateId);
        }

        [HttpPost("ForwardTask")]
        public async Task<Tuple<int, string>> ForwardTaskAsync(Domain.Task task)
        {
            return await _taskRepository.ForwardTaskAsync(task);
        }

        [HttpPost("BackwardTask")]
        public async Task<Tuple<int, string>> BackwardTaskAsync(Domain.Task task)
        {
            return await _taskRepository.BackwardTaskAsync(task);
        }
    }
}
