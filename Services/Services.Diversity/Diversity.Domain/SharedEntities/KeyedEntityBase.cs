using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Domain.SharedEntities
{
    public abstract class KeyedEntityBase<TValue>
    {
        public TValue Id { get; set; }
    }
}
