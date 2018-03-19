using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version
{
    public class MyTuple<T1, T2>
    {
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        internal MyTuple(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }

    public class MyTuple<T1, T2, T3>
    {
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        public T3 Third { get; private set; }
        internal MyTuple(T1 first, T2 second, T3 third)
        {
            First = first;
            Second = second;
            Third = third;
        }
    }

    public static class MyTuple
    {
        public static MyTuple<T1, T2> New<T1, T2>(T1 first, T2 second)
        {
            var tuple = new MyTuple<T1, T2>(first, second);
            return tuple;
        }
    }
}
