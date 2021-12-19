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
    public class TaskValueController : Controller
    {
        private readonly ITaskValueRepository _taskValueRepository;

        public TaskValueController(ITaskValueRepository taskValueRepository)
        {
            _taskValueRepository = taskValueRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskValue>> GAsync()
        {
            return await _taskValueRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<TaskValue> GetAsync(int id)
        {
            return await _taskValueRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> PostAsync([FromBody] TaskValue taskValue)
        {
            return await _taskValueRepository.AddAsync(taskValue);
        }

        [HttpDelete("{id}")]
        public async Task<bool> RemoveTaskValueAsync(int id)
        {
            return await _taskValueRepository.DeleteAsync(id);
        }
    }
}
