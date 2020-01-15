using System.Windows.Forms;
using CC_Functions.W32.Native;

namespace CC_Functions.W32
{
    public static class KeyboardReader
    {
        public static bool IsKeyDown(Keys key)
        {
            int state = 0;
            short retVal = user32.GetKeyState((int) key);
            if ((retVal & 0x8000) == 0x8000)
                state |= 1;
            if ((retVal & 1) == 1)
                state |= 2;
            return (state & 1) == 1;
        }
    }
}