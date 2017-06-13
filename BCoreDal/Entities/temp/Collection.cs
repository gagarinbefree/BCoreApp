using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BCoreDal.Entities.temp
{
    public class Collection : Entity
    {        
        public string Name { set; get; }

        public Guid? ParentId { set; get; }
                
        public virtual ICollection<Item> Items { set; get; }
    }
}