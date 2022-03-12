using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp102.Configrasyon;
using TodoApp102.Entities;
using System.Data.SqlClient;
namespace TodoApp102.Respository
{
    public class ItemRepository : IItemRepository
    {
        private readonly  List<ItemInfo> list;
        private readonly ConnectionString m_connectionStrings;
        private readonly SqlConnection m_sqlConnection;
        private readonly string ms_FindByItemId = "select * from ItemInfo where(Id)=@Id";
        private readonly string ms_findCount = "select count(*) from ItemInfo";
        private readonly string m_findByTodoIdOrderByLastUpdateDesc = "Select * from ItemInfo where(TodoId)=@TodoId   ORDER BY(LastUpdateDateTime) Desc ";
        public ItemRepository(ConnectionString connectionString)
        {
            m_connectionStrings = connectionString;

            m_sqlConnection = new SqlConnection(m_connectionStrings.m_connectionString);
            list = new List<ItemInfo>(); 

        }
        public  ItemInfo GetItemInfo(SqlDataReader reader) {

            var read = new ItemInfo {
                Id = (int)reader[0],
                TodoId = (int)reader[1],
                Text = (string)reader[2], 
                CreatedDateTime = (DateTime)reader[3],
                LastUpdateDateTime = (DateTime)reader[4], 
                Completed = (bool)reader[5]
            };
            return read;

        
        }
        public ItemInfo FindById(int id)
        {
            throw new NotImplementedException();
        }
        public long Count(int count)
        {
           
            try {
                var command = new SqlCommand(ms_findCount,m_sqlConnection);
                m_sqlConnection.Open();
              //  command.ExecuteNonQuery();
                var reader = command.ExecuteReader();
                reader.Read();

                return (int)reader[0];
            
            
            } finally {

                if (m_sqlConnection.State == System.Data.ConnectionState.Open)
                    m_sqlConnection.Close();

            }
        }

        public void Delete(ItemInfo entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void DeleteAll(IEnumerable<ItemInfo> entities)
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

        public IEnumerable<ItemInfo> FindAll()
        {
            throw new NotImplementedException();
        }

      

        public ItemInfo Save(ItemInfo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemInfo> SaveAll(IEnumerable<ItemInfo> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemInfo> FindByIds(int Id)
        {
            try
            {
              //  var list = new List<ItemInfo>();
                var command = new SqlCommand(ms_FindByItemId, m_sqlConnection);
                command.Parameters.AddWithValue("Id", Id);
                m_sqlConnection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    list.Add(GetItemInfo(reader));

                return list;
            }
            finally
            {

                if (m_sqlConnection.State == System.Data.ConnectionState.Open)
                    m_sqlConnection.Close();
            }
        }

        public IEnumerable<ItemInfo> findByTodoIdOrderByLastUpdateDesc(int TodoId)
        {
            try {
             //   var list = new List<ItemInfo>();
                var command = new SqlCommand(m_findByTodoIdOrderByLastUpdateDesc, m_sqlConnection);
                command.Parameters.AddWithValue("@TodoId", TodoId);
                m_sqlConnection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(GetItemInfo(reader));
                }
                return list;
            } finally {
                if (m_sqlConnection.State == System.Data.ConnectionState.Open)
                    m_sqlConnection.Close();

            }
        }

        public Task<ItemInfo> SaveAsync(ItemInfo entity)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ItemInfo entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAsync(IEnumerable<ItemInfo> entities)
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

        public Task<ItemInfo> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExitsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemInfo>> SaveAllAsync(IEnumerable<ItemInfo> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemInfo>> FindAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
