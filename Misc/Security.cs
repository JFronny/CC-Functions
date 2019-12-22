using System.Security.Principal;

namespace CC_Functions.Misc
{
    public static class MiscFunctions
    {
        public static bool IsAdministrator =>
            new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }
}