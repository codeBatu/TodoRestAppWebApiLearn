using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp102.Entities
{
    public class ItemInfo
    {
        public int Id { get; set; }
        public int TodoId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public bool Completed { get; set; }
    }
}
