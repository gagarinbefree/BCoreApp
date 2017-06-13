using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BCoreDal.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { set; get; }               
    }
}