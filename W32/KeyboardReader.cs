using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CC_Functions.W32
{
    public static class KeyboardReader
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        public static bool IsKeyDown(Keys key)
        {
            try
            {
                int state = 0;
                short retVal = GetKeyState((int)key);
                if ((retVal & 0x8000) == 0x8000)
                    state |= 1;
                if ((retVal & 1) == 1)
                    state |= 2;
                return (state & 1) == 1;
            }
            catch
            {
                throw;
            }
        }
    }
}