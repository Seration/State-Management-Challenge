using System;
using Application.Repositories;
using Domain;
using Persistence;

namespace Application.Services
{
    public class TaskValueRepository : Repository<TaskValue>, ITaskValueRepository
    {
        public TaskValueRepository(DataContext context) : base(context)
        {

        }
    }
}
