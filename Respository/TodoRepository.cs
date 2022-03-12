using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TodoApp102.Configrasyon;
using TodoApp102.Entities;
using static Util.Error.ExceptionUtil;
using static TaskUtilLib.TaskUtil;
namespace TodoApp102.Respository
{
    public class TodoRepository : ITodoRepository
    {
        #region sql commands
        private readonly ConnectionString m_connectionString;
        private readonly SqlConnection m_sqlConnection;
        private const String ms_SqlConnectionInsert = "Insert into TodoInfo (Title,Description) values (@Title ,@Description)";
        private const String ms_SqlCommendStrCount = "select count(*) from TodoInfo";
        private const String ms_MonthAndYearSqlCommendStrCount = "select * from  TodoInfo where month(CreatedDateTime)=@month and year(CreatedDateTime)=@year";

        private const string m_deleteSqlCommandStr = "delete  from TodoInfo where Title =@Title";
        private const String ms_selectAll = "select * from TodoInfo";
        private const string ms_findById = "select * from TodoInfo where (Id) = @id ";
        #endregion
        // call back is  return not task ; 
        #region CallBack
        private void closeConnectionCallBack( )
        {

            if (m_sqlConnection.State == System.Data.ConnectionState.Open)
            {
                m_sqlConnection.Close();
            }
        }
        private long countCallBack()
        {
         

                var command = new SqlCommand(ms_SqlCommendStrCount, m_sqlConnection);


                m_sqlConnection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                return (int)reader[0];
        

        
           

        }
        private Task<IEnumerable<TodoInfo>> findAllCallBack()
        {

            Func<IEnumerable<TodoInfo>> func = () => {
                var command = new SqlCommand(ms_selectAll, m_sqlConnection);
                m_sqlConnection.Open();
                var list = new List<TodoInfo>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(SqlRead(reader));

                }
                return list;
            };
            var task = new Task<IEnumerable<TodoInfo>>(func);
            task.Start();
  
            return task;

        }
      
        private void deleteCallBack(TodoInfo entity)
        {
          
            var command = new SqlCommand(m_deleteSqlCommandStr, m_sqlConnection);
            command.Parameters.AddWithValue("@Title", entity.Title);
         m_sqlConnection.Open();
            
        }



        private Task<TodoInfo> findByIdCallBack(int ?id)
        {
            Func<TodoInfo> func = ()=>{
                TodoInfo todoInfo = new TodoInfo();
                var command = new SqlCommand(ms_findById, m_sqlConnection);
                command.Parameters.AddWithValue("@id", id);
                m_sqlConnection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    todoInfo = SqlRead(reader);
                }
                return todoInfo;
            };
            var task = new Task<TodoInfo>(func);
            task.Start();
            return task;


        }
        private Task<TodoInfo> saveCallBack(TodoInfo entity)
        {
            Func<TodoInfo> func = () =>
            {
                var command = new SqlCommand(ms_SqlConnectionInsert, m_sqlConnection);
                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Description", entity.Description);
                m_sqlConnection.Open();
                command.ExecuteNonQuery();//No query
                return entity;
            };
            var task = new Task <TodoInfo>(func);
            task.Start();
            return task;
        }
        private Task<IEnumerable<TodoInfo>> findMonthAndYearsCallBack(int month, int year)
        {
            Func<IEnumerable<TodoInfo>> func = () =>
            {
                var command = new SqlCommand(ms_MonthAndYearSqlCommendStrCount, m_sqlConnection);
                var list = new List<TodoInfo>();
                command.Parameters.AddWithValue("@month", month);
            
                command.Parameters.AddWithValue("@year", year);
                m_sqlConnection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(SqlRead(reader));
                }
                return list;
            };
           var task = new Task<IEnumerable<TodoInfo>>(func);
            task.Start();
            return task;

        }
        #endregion
        #region async
        private Task closeConnectionAsync()
        {
            var task = new Task(closeConnectionCallBack);
            task.Start();
            return task;
        }
        public Task<long> CountAsync()
        {
        
            return SubscribeAsync(()=>new Task<long>(countCallBack).Create(), ()=>new Task(closeConnectionCallBack).Create() );

        }
        public Task<IEnumerable<TodoInfo>> FindAllAsync()
        {
          
            return SubscribeAsync(findAllCallBack, closeConnectionAsync);
               
            }
        public Task DeleteAsync(TodoInfo entity)
        {
            Func<Task> func = () => {
                var task = new Task(() => deleteCallBack(entity));
                task.Start();
                return task;
            };


            return SubscribeAsync(func, closeConnectionAsync);
        }
        public Task<TodoInfo> SaveAsync(TodoInfo entity)
        {

            return SubscribeAsync(()=>saveCallBack(entity), closeConnectionAsync) ;

        }

        public Task<IEnumerable<TodoInfo>> FindMonthAndYearsAsync(int mount, int years)
        {
            return SubscribeAsync(()=>findMonthAndYearsCallBack(mount,years), closeConnectionAsync);

        }

        public Task<TodoInfo> FindByIdAsync(int id)
        {
            return SubscribeAsync(()=> findByIdCallBack(id), closeConnectionAsync);
        }


        #endregion
        #region ctor and sql reader
        public TodoRepository(ConnectionString connectionString)
        {
            m_connectionString = connectionString;
            m_sqlConnection = new SqlConnection(m_connectionString.m_connectionString);
        }
        public TodoInfo SqlRead(SqlDataReader sqlDataReader)
        {
            var reader = new TodoInfo { Id = (int)sqlDataReader[0],
                Title = (string)sqlDataReader[1],
                Description = (string)sqlDataReader[2], 
                CreatedDateTime = (DateTime)sqlDataReader[3], 
                Completed = (bool)sqlDataReader[4] };
            return reader;
        }
        #endregion
        #region old method
        public void Delete(TodoInfo entity)
        {
            throw new Exception();
        }
        public long Count()
        {
            throw new Exception();
        }
        public IEnumerable<TodoInfo> FindAll()
        {
            throw new Exception();
        }
        public TodoInfo FindById(int id)
        {
            throw new Exception();
        }
        public TodoInfo Save(TodoInfo entity)
        {
            throw new Exception();
        }
        public IEnumerable<TodoInfo> FindMonthAndYears(int mount, int years)
        {
            throw new Exception();
        }
        #endregion
        #region not implemented
        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void DeleteAll(IEnumerable<TodoInfo> entities)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExitsById(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<TodoInfo> SaveAll(IEnumerable<TodoInfo> entities)
        {
            throw new NotImplementedException();
        }

        public long Count(int count)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAsync(IEnumerable<TodoInfo> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllByIdAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExitsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoInfo>> SaveAllAsync(IEnumerable<TodoInfo> entities)
        {
            throw new NotImplementedException();
        }





        //public IEnumerable<TodoInfo> findLastUpdadeMonth(int month)
        //  {
        //      try {
        //          var list = new List<TodoInfo>();

        //          var command = new SqlCommand(ms_LastUpdateMonth,m_sqlConnection);
        //          command.Parameters.AddWithValue("@UpdateDateTime",month);
        //          m_sqlConnection.Open();
        //          var reader = command.ExecuteReader();
        //          while (reader.Read())
        //          {
        //              list.Add(SqlRead(reader));
        //          }
        //          return list;

        //      } finally {
        //          if (m_sqlConnection.State == System.Data.ConnectionState.Open)
        //              m_sqlConnection.Close();
        //      }

        //  }
        #endregion
   
  }
}
