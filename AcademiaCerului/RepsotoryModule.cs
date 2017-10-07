using System;
using System.Collections;
using System.Collections.Generic;
using Ninject.Modules;

namespace AcademiaCerului
{
    internal class RepsotoryModule : IEnumerable<INinjectModule>
    {
        public IEnumerator<INinjectModule> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}