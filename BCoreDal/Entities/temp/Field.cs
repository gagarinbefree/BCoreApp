using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BCoreDal.Entities.temp
{
    public class Field : Entity
    {
        public string Name { set; get; }
        public string Value { set; get; }
        public int Type { set; get; }
       
        [ForeignKey("Item")]
        public Guid ItemId { set; get; }
        public virtual Item Item { set; get; }
    }
}