using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp102.Entities;
using Util.ExceptionLibrary.Repository;

namespace TodoApp102.Respository
{
    public interface IItemRepository : ICrudRepository<ItemInfo, int>
    {
   
        IEnumerable<ItemInfo> FindByIds(int id);
        IEnumerable<ItemInfo> findByTodoIdOrderByLastUpdateDesc(int id);
    }
}
