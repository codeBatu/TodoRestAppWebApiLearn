using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp102.Entities;
using Util.ExceptionLibrary.Repository;

namespace TodoApp102.Respository
{
    public interface ITodoRepository : ICrudRepository<TodoInfo, int>
    {
 public Task<IEnumerable<TodoInfo>> FindMonthAndYearsAsync(int mount, int years);
    }
}
