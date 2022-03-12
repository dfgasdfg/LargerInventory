using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace LargerInventory
{
    internal class ItemStackComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            if (x is null || y is null)
            {
                Flag.CatchException(new NullReferenceException("The object being compared against cannot be null"));
                return 0;
            }
            int spacex = x.maxStack - x.stack, spacey = y.maxStack - y.stack;
            if (spacex < spacey)
            {
                return -1;
            }
            if (spacey > spacex)
            {
                return 1;
            }
            return 0;
        }
    }
    internal class CustomTComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> ComparerFunction;
        internal CustomTComparer([NotNullWhen(true)] Func<T, T, int> comparerfunc)
        {
            ComparerFunction = comparerfunc;
        }

        public int Compare([NotNullWhen(true)] T x, [NotNullWhen(true)] T y)
        {
            return ComparerFunction(x, y);
        }
    }
}
