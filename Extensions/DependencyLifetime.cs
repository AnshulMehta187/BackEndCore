using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public enum DependencyLifetime
    {
        Transient,
        Singleton,
        Scoped
    }
}
