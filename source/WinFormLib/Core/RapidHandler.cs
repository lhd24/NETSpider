using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormLib.Core
{
    public delegate void RapidHandler();
    public delegate void RapidHandler<T>(T obj);
    public delegate void RapidHandler<T1, T2>(T1 obj1, T2 obj2);
    public delegate void RapidHandler<T1, T2, T3>(T1 obj1, T2 obj2, T3 obj3);
    public delegate void RapidHandler<T1, T2, T3, T4>(T1 obj1, T2 obj2, T3 obj3, T4 obj4);
}
