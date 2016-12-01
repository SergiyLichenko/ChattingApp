using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Service
{
    public interface IMappingService
    {
        TDest Map<TSrc, TDest>(TSrc source) where TDest : class;
    }
}
