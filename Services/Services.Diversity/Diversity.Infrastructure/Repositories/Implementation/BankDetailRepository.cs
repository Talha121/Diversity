using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Interfaces;
using Diversity.Infrastructure.SharedRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.Repositories.Implementation
{
    public class BankDetailRepository : GenericRepository<BankDetails>, IBankDetailRepository
    {
        public BankDetailRepository(DataContext context) : base(context)
        {
        }
    }
}
