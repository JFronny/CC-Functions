using CC_Functions.W32.Native;

namespace CC_Functions.W32.Hooks
{
    public sealed class KeyboardHook : IDisposable
    {
        public delegate void KeyPress(KeyboardHookEventArgs args);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private static readonly List<KeyboardHook> Instances = new List<KeyboardHook>();
        private static readonly user32.LowLevelProc Proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public KeyboardHook()
        {
            if (Instances.Count == 0)
                _hookID = SetHook(Proc);
            Instances.Add(this);
        }

        public void Dispose()
        {
            Instances.Remove(this);
            if (Instances.Count == 0)
                user32.UnhookWindowsHookEx(_hookID);
        }

        public event KeyPress? OnKeyPress;

        private IntPtr SetHook(user32.LowLevelProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
                return user32.SetWindowsHookEx(WH_KEYBOARD_LL, proc, kernel32.GetModuleHandle(curModule.ModuleName), 0);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr) WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                for (int i = 0; i < Instances.Count; i++)
                    Instances[i].OnKeyPress?.Invoke(new KeyboardHookEventArgs((Keys) vkCode));
            }

            return user32.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}