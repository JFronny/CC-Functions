namespace CC_Functions.W32
{
    public static class GenericExtensions
    {
        public static Wnd32 GetWindow(this IntPtr handle) => Wnd32.FromHandle(handle);
        public static Wnd32 GetMainWindow(this Process handle) => Wnd32.GetProcessMain(handle);
        public static Wnd32 GetWnd32(this Form frm) => Wnd32.FromForm(frm);
        public static bool IsDown(this Keys key) => KeyboardReader.IsKeyDown(key);

        public static Privileges.SecurityEntity GetEntity(this Privileges.SecurityEntity2 entity) =>
            Privileges.EntityToEntity(entity);
    }
}