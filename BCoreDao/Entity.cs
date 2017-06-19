using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BCoreDao
{
    public class Entity
    {
        [Key]
        public Guid Id { set; get; }               
    }
}