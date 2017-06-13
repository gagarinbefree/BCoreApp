using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BCoreDal.Entities.temp
{
    public class Item : Entity
    {
        public string Name { set; get; }
        public Guid CreatedBy { set; get; }
        public bool Deleted { set; get; }

        [ForeignKey("Collection")]
        public Guid CollectionId { set; get; }
        public virtual Collection Collection { set; get; }
        
        public virtual ICollection<Field> Fields { set; get; }
        
        public virtual ICollection<Item> Childs { set; get; }
    }
}