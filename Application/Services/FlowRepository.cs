using System;
using Application.Repositories;
using Persistence;

namespace Application.Services
{
    public class FlowRepository : Repository<Domain.Flow>, IFlowRepository
    {
        public FlowRepository(DataContext context) : base(context)
        {

        }
    }
}
