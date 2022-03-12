using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp102.Entities;
using TodoApp102.Respository;
using Util.ExceptionLibrary.Repository;
using static Util.ExceptionLibrary.DatabaseUtil;
namespace TodoApp102.DAL
{
    public class DALRespository 
    {
       private readonly ITodoRepository m_todoRepository;

        private readonly IItemRepository m_itemRepository;
        public DALRespository(ITodoRepository todoRepository,IItemRepository itemRepository)
        {
            m_todoRepository = todoRepository;
            m_itemRepository = itemRepository;
        }
     
        #region Todo
        //public IEnumerable<TodoInfo>LastUpdateMonth (int month)
        //{
        //    try
        //    {
        //        return m_todoRepository.findLastUpdadeMonth(month);

        //    }
        //    catch (Exception exp)
        //    {

        //        throw new RepositoryException("DAL.LastUFindMonth",exp);
        //    }

        //}
        public  Task<IEnumerable<TodoInfo>>FindMonthAnYearAsyncs(int mount ,int year)
        {
            return SubscribeRepository(() => m_todoRepository.FindMonthAndYearsAsync(mount, year), "DALRespository.FindMonthAnYearAsyncs");

        }
        public   Task  DeleteAsync(TodoInfo todoInfo)
        {
            return SubscribeRepository(()=>m_todoRepository.DeleteAsync(todoInfo), "DALRespository.Delete");

        }
        public  Task<IEnumerable<TodoInfo>> ShowTable()
        {
            return SubscribeRepository(()=>m_todoRepository.FindAllAsync(), "DALRespository.Show Table");
         
        }
     
    
        public Task<TodoInfo> SaveTodoInfoAsync(TodoInfo entity) {
            return SubscribeRepository(()=>m_todoRepository.SaveAsync(entity), "DALRespository.FindByIdAsync");
         
        }
       
        public  Task<TodoInfo> FindByIdAsync(int b)
        {
            return SubscribeRepository(()=>m_todoRepository.FindByIdAsync(b), "DALRespository.FindByIdAsync");
         
        }
   public  Task <long> TodoCountAsync()
        {
         //   return await m_todoRepository.CountAsync();
            return SubscribeRepository(()=>m_todoRepository.CountAsync(), "DALRespository.SaveTodoInfo");
        }
        #endregion
        #region Item
        public long ItemCount()
        {
            try
            {
                return m_itemRepository.Count(0);
            }
            catch (Exception exp)
            {

                throw new RepositoryException("DALRespository.ItemCount",exp);
            }
        }
        public IEnumerable<ItemInfo> FindByIdc(int id)
        {
            try
            {
                return m_itemRepository.FindByIds(id);
            }
            catch (Exception exp)
            {

                throw new RepositoryException("DALRespository.ItemFindById",exp);
            }

        }
        public  IEnumerable<ItemInfo>lastUpdate(int id)
        {

            try
            {
                return m_itemRepository.findByTodoIdOrderByLastUpdateDesc(id);

            }
            catch (Exception exp)
            {

                throw new RepositoryException("DALRespository.ItemLastUpdate", exp);
            }

        }
        #endregion
    }

}
