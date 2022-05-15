using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.SharedModels
{
    public class KeyedModel<T> : ModelBase where T : IEquatable<T>
    {
        public T Id { get; set; }
    }

    public class ModelBase
    {
    }
}
