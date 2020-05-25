using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using CC_Functions.W32.Native;

namespace CC_Functions.W32.Hooks
{
    public sealed class MouseHook : IDisposable
    {
        public delegate void MouseEvent(MouseHookEventArgs args);

        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        private const int WH_MOUSE_LL = 14;

        private static readonly List<MouseHook> Instances = new List<MouseHook>();
        private static readonly user32.LowLevelProc Proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public MouseHook()
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

        public event MouseEvent? OnMouse;

        private static IntPtr SetHook(user32.LowLevelProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
                return user32.SetWindowsHookEx(WH_MOUSE_LL, proc, kernel32.GetModuleHandle(curModule.ModuleName), 0);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                for (int i = 0; i < Instances.Count; i++)
                    Instances[i].OnMouse?.Invoke(new MouseHookEventArgs(new Point(hookStruct.pt.x, hookStruct.pt.y),
                        (MouseMessages) wParam));
            }

            return user32.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public readonly int x;
            public readonly int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public readonly POINT pt;
            public readonly uint mouseData;
            public readonly uint flags;
            public readonly uint time;
            public readonly IntPtr dwExtraInfo;
        }
    }
}