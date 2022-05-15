using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.SharedRepositories
{
    public class PagedData<T> where T : class
    {
        public IList<T> Items { get; set; }
        public int ItemCount { get; set; }
    }
}
