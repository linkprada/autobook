using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace autobook.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}