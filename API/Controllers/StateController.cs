using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class StateController : Controller
    {
        private readonly IStateRepository _stateRepository;
        private readonly IFlowRepository _flowRepository;

        public StateController(IStateRepository stateRepository, IFlowRepository flowRepository)
        {
            _stateRepository = stateRepository;
            _flowRepository = flowRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<State>> StatesAsync()
        {
            return await _stateRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<State> StateAsync(int id)
        {
            return await _stateRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> CreateStateAsync(State state)
        {
            var flow = _flowRepository.Find(s => s.Id == state.FlowId);

            if (flow == null)
                return false;

            state.Index = await _stateRepository.GetNextStateIndexAsync(state.FlowId);

            return await _stateRepository.AddAsync(state);
        }

        [HttpPut]
        public async Task<bool> Put([FromBody] State state)
        {
            var flow = _flowRepository.Find(s => s.Id == state.FlowId);

            if (flow == null)
                return false;

            return await _stateRepository.UpdateStateName(state);
        }

        [HttpDelete("{id}")]
        public async Task<bool> RemoveStateAsync(int id)
        {
            return await _stateRepository.DeleteAsync(id);
        }

        [HttpGet("BelongToFlow/{flowId}")]
        public async Task<IEnumerable<Domain.State>> StatesBelongToFlowAsync(int flowId)
        {
            return await _stateRepository.GetAllStateBelongToFlowAsync(flowId);
        }

        [HttpPost("ChangeStateOrder")]
        public async Task<bool> ChangeStateOrderAsync([FromBody]IEnumerable<ChangeStateOrderDto> newIndexList, int flowId)
        {
            return await _stateRepository.ChangeStateOrderAsync(newIndexList, flowId);
        }
    }
}
