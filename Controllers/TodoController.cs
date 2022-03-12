using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp102.Entities;
using TodoApp102.Services;
using Util.ExceptionLibrary.Service;
using static Util.ExceptionLibrary.DatabaseUtil;
namespace TodoApp102.Controllers
{
    [Route("apis/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoAppServices m_todoAppService;
        public TodoController(TodoAppServices todoAppServices)
        {
            m_todoAppService = todoAppServices;
        }
        //[[HttpGet("Delete")]
        //        public IActionResult Delete([FromBody] TodoInfo todoInfo)
        //        {

        //            try
        //            {
        //               m_todoAppService.Delete(todoInfo);
        //                object a="";

        //                return new ObjectResult(a);

        //            }
        //            catch (Exception exp)
        //            {

        //                throw new DataServiceException  ("Controller App Delete", exp);
        //            }

        //        }
        //[HttpGet("LastUpdateMonth")]
        //public IActionResult FindLastUpdateMonth(int month)
        //{

        //    try
        //    {
        //        return new ObjectResult(m_todoAppService.LastFindUpdateMonth(month));
        //    }
        //    catch (DataServiceException exp)
        //    {

        //        throw new DataServiceException("App.LastUpdateMonth",exp);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DataServiceException("App.LastUpdateMonth", ex);
        //    }
        //}
        [HttpGet("ItemFindId")]
        public IActionResult FindByItemId(int id) {

            try
            {
                return new ObjectResult(m_todoAppService.findByItemId(id));
            }
            catch (Exception exp)
            {

                throw new DataServiceException ("Controoller Find Item Id",exp);
            }
        
        
        }
        [HttpGet("ItemCount")]
        public IActionResult FindItemCount()
        {

            try
            {
                return new ObjectResult(new { Count= m_todoAppService.ItemCount() });
            }
            catch (Exception exp)
            {

                throw new DataServiceException("Controller", exp); ;
            }

        }
        [HttpGet("Itemlast")]
        public IActionResult FindItemlast(int id)
        {

            try
            {
                return new ObjectResult(m_todoAppService.LastUpdate(id));
            }
            catch (Exception exp)
            {

                throw new DataServiceException("Controller", exp); ;
            }

        }
        [HttpGet("FindById")]
        public async Task<IActionResult> FindById(int Id)
        {
            try
            {
                return new ObjectResult(await m_todoAppService.FindById(Id));
            }
            catch (Exception exp)
            {

                throw new DataServiceException("Controller",exp); ;
            }

        }
        [HttpPost("Save2")]
        public async Task< IActionResult> SaveInfosAsync([FromBody] TodoInfo todoInfo)
        {
            try
            {
                return  new ObjectResult(await m_todoAppService.SaveAsync(todoInfo));
            }
            catch (Exception exp)
            {
                throw new DataServiceException("Controller.Save", exp);
            }
        }
        [HttpGet("findMonthAndYears")]
        public async Task< IActionResult> FindMonthAndYear(int month, int year)
        {
            try
            {
                return new ObjectResult(await m_todoAppService.FindMonthandYear(month, year));
            }
            catch (Exception exp)
            {
                throw new DataServiceException("Controller Exp",exp);
            }

        }
        [HttpGet]
        public async Task<IActionResult> ShowTable()
        {
            try
            {
                return new ObjectResult(await m_todoAppService.ShowTable());
            }
            catch (Exception exp)
            {

                throw new DataServiceException("Son Kısım",exp);
            }
        }
            [HttpGet("Count")]
        public  async Task<IActionResult> Count()
        {
          try
            {
                return new ObjectResult( new {Count = await m_todoAppService.TodoCountAsync() });// Gelen Sonuç {Count = } Göstermek için
            }
            catch (Exception exp )
            {
                throw new DataServiceException("Controller.Count", exp);
            }
        }
    }
}
