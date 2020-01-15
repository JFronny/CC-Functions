using System;
using System.Drawing;

namespace CC_Functions.W32.DCDrawer
{
    public interface IDCDrawer : IDisposable
    {
        Graphics Graphics { get; }
    }
}