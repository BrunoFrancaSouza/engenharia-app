using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia.Domain.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public virtual int Id { get; set; }
    }
}

