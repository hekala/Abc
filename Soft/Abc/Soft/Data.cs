using System;
using System.Threading.Tasks;

namespace Abc.Soft.Abc.Soft
{
    internal class Data
    {
        internal class ApplicationDbContext
        {
            public object MeasureView { get; internal set; }

            internal Task SaveChangesAsync()
            {
                throw new NotImplementedException();
            }
        }
    }
}