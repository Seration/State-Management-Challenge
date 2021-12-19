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
    public class FlowController : Controller
    {
        private readonly IFlowRepository _flowRepository;

        public FlowController(IFlowRepository flowRepository)
        {
            _flowRepository = flowRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Flow>> FlowsAsync()
        {
            return await _flowRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Flow> FlowAsync(int id)
        {
            return await _flowRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> CreateFlowAsync([FromBody] Flow flow)
        {
            return await _flowRepository.AddAsync(flow);
        }

        [HttpPut]
        public async Task<bool> PutAsync([FromBody] Flow flow)
        {
            return await _flowRepository.UpdateFlowName(flow);
        }

        [HttpDelete("{id}")]
        public async Task<bool> RemoveFlowAsync(int id)
        {
            return await _flowRepository.DeleteAsync(id);
        }
    }
}
