using System.Security.Principal;

namespace CC_Functions.W32
{
    public static class MiscFunctions
    {
        public static bool IsAdministrator =>
            new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }
}