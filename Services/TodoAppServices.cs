using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp102.DAL;
using TodoApp102.Entities;
using Util.ExceptionLibrary.Repository;
using Util.ExceptionLibrary.Service;
using static Util.ExceptionLibrary.DatabaseUtil;
namespace TodoApp102.Services
{
    public class TodoAppServices
    {
        private readonly DALRespository m_dALRespository;
        public TodoAppServices(DALRespository dALRespository)
        {
            m_dALRespository = dALRespository;
            
        }

        //public IEnumerable<TodoInfo> LastFindUpdateMonth(int month) {

        //    try
        //    {
        //        return m_dALRespository.LastUpdateMonth(month);
        //    }
        //    catch (Exception exp)
        //    {

        //        throw new DataServiceException("Service.LastUpadteMonth",exp);
        //    }
        //}


        #region Todo
        public  Task<TodoInfo> SaveAsync(TodoInfo entity)
        {
            return SubscribeServiceAsync(()=>m_dALRespository.SaveTodoInfoAsync(entity),"");

        }
        public async Task<long> TodoCountAsync()
        {
            //   return SubscribeServiceAsync(()=>m_dALRespository.TodoCountAsync(),"");
            return await m_dALRespository.TodoCountAsync();

        }   public  Task<IEnumerable<TodoInfo>> FindMonthandYear(int month , int years  ) {

            return SubscribeServiceAsync(()=>m_dALRespository.FindMonthAnYearAsyncs(month,  years),"");
        }
        public  Task<IEnumerable<TodoInfo>> ShowTable()
        {
        
                return SubscribeServiceAsync(m_dALRespository.ShowTable, "");
       
        }
        public  Task Delete(TodoInfo todoInfo)
        {
            return SubscribeServiceAsync(()=>m_dALRespository.DeleteAsync(todoInfo),"");


        }
        public Task<TodoInfo> FindById(int b)
        {

            return SubscribeServiceAsync(()=>m_dALRespository.FindByIdAsync(b),"");


        }
        public  Task<TodoInfo> SaveTodoInfos(TodoInfo entity)
        {
            return SubscribeServiceAsync(() => m_dALRespository.SaveTodoInfoAsync(entity), "");
            
        }
        #endregion
        #region Item
     
        public long ItemCount()
        {

            try
            {
                return m_dALRespository.ItemCount();
            }
            catch (Exception exp)
            {

                throw new DataServiceException("Service Item Count", exp);
            }

        }


        public IEnumerable<ItemInfo> findByItemId(int id) {

            try
            {
                return m_dALRespository.FindByIdc(id);
            }
            catch (Exception exp)
            {

                throw new DataServiceException("Service Item Find By Id",exp);
            }
        }
        public IEnumerable<ItemInfo> LastUpdate(int id)
        {

            try
            {
                return m_dALRespository.lastUpdate(id);
            }
            catch (Exception exp)
            {

                throw new DataServiceException("Service Item Find By Id", exp);
            }
        }

        #endregion




    }
}
