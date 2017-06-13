using BCoreDal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCoreDal.Contracts
{
    public interface IUoW
    {
        IRepository<Collection> CollectionRepository { get; }
        IRepository<Item> ItemRepository { get; }
        IRepository<Field> FieldRepository { get; }        
    }
}
