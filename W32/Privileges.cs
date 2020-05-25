using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using CC_Functions.W32.Native;

namespace CC_Functions.W32
{
    public static class Privileges
    {
        public enum SecurityEntity
        {
            SeAssignPrimaryTokenPrivilege,
            SeAuditPrivilege,
            SeBackupPrivilege,
            SeChangeNotifyPrivilege,
            SeCreateGlobalPrivilege,
            SeCreatePagefilePrivilege,
            SeCreatePermanentPrivilege,
            SeCreateSymbolicLinkPrivilege,
            SeCreateTokenPrivilege,
            SeDebugPrivilege,
            SeDelegateSessionUserImpersonatePrivilege,
            SeEnableDelegationPrivilege,
            SeImpersonatePrivilege,
            SeIncreaseBasePriorityPrivilege,
            SeIncreaseWorkingSetPrivilege,
            SeIncreaseQuotaPrivilege,
            SeLoadDriverPrivilege,
            SeLockMemoryPrivilege,
            SeMachineAccountPrivilege,
            SeManageVolumePrivilege,
            SeProfileSingleProcessPrivilege,
            SeRelabelPrivilege,
            SeRemoteShutdownPrivilege,
            SeRestorePrivilege,
            SeSecurityPrivilege,
            SeShutdownPrivilege,
            SeSyncAgentPrivilege,
            SeSystemEnvironmentPrivilege,
            SeSystemProfilePrivilege,
            SeSystemtimePrivilege,
            SeTakeOwnershipPrivilege,
            SeTcbPrivilege,
            SeTimeZonePrivilege,
            SeTrustedCredManAccessPrivilege,
            SeUndockPrivilege,
            SeUnsolicitedInputPrivilege
        }

        public enum SecurityEntity2
        {
            SE_ASSIGNPRIMARYTOKEN_NAME_TEXT,
            SE_AUDIT_NAME_TEXT,
            SE_BACKUP_NAME_TEXT,
            SE_CHANGE_NOTIFY_NAME_TEXT,
            SE_CREATE_GLOBAL_NAME_TEXT,
            SE_CREATE_PAGEFILE_NAME_TEXT,
            SE_CREATE_PERMANENT_NAME_TEXT,
            SE_CREATE_SYMBOLIC_LINK_NAME_TEXT,
            SE_CREATE_TOKEN_NAME_TEXT,
            SE_DEBUG_NAME_TEXT,
            SE_DELEGATE_SESSION_USER_IMPERSONATE_NAME_TEXT,
            SE_ENABLE_DELEGATION_NAME_TEXT,
            SE_IMPERSONATE_NAME_TEXT,
            SE_INC_BASE_PRIORITY_NAME_TEXT,
            SE_INC_WORKING_SET_NAME_TEXT,
            SE_INCREASE_QUOTA_NAME_TEXT,
            SE_LOAD_DRIVER_NAME_TEXT,
            SE_LOCK_MEMORY_NAME_TEXT,
            SE_MACHINE_ACCOUNT_NAME_TEXT,
            SE_MANAGE_VOLUME_NAME_TEXT,
            SE_PROF_SINGLE_PROCESS_NAME_TEXT,
            SE_RELABEL_NAME_TEXT,
            SE_REMOTE_SHUTDOWN_NAME_TEXT,
            SE_RESTORE_NAME_TEXT,
            SE_SECURITY_NAME_TEXT,
            SE_SHUTDOWN_NAME_TEXT,
            SE_SYNC_AGENT_NAME_TEXT,
            SE_SYSTEM_ENVIRONMENT_NAME_TEXT,
            SE_SYSTEM_PROFILE_NAME_TEXT,
            SE_SYSTEMTIME_NAME_TEXT,
            SE_TAKE_OWNERSHIP_NAME_TEXT,
            SE_TCB_NAME_TEXT,
            SE_TIME_ZONE_NAME_TEXT,
            SE_TRUSTED_CREDMAN_ACCESS_NAME_TEXT,
            SE_UNDOCK_NAME_TEXT,
            SE_UNSOLICITED_INPUT_NAME_TEXT
        }

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int ERROR_NOT_ALL_ASSIGNED = 1300;
        internal const uint TOKEN_QUERY = 0x0008;
        internal const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;

        public static void EnablePrivilege(SecurityEntity securityEntity)
        {
            if (!Enum.IsDefined(typeof(SecurityEntity), securityEntity))
                throw new InvalidEnumArgumentException("securityEntity", (int) securityEntity, typeof(SecurityEntity));
            string securityEntityValue = securityEntity.ToString();
            try
            {
                LUID locallyUniqueIdentifier = new LUID();
                if (advapi32.LookupPrivilegeValue(null, securityEntityValue, ref locallyUniqueIdentifier))
                {
                    TOKEN_PRIVILEGES TOKEN_PRIVILEGES = new TOKEN_PRIVILEGES
                    {
                        PrivilegeCount = 1,
                        Attributes = SE_PRIVILEGE_ENABLED,
                        Luid = locallyUniqueIdentifier
                    };
                    IntPtr tokenHandle = IntPtr.Zero;
                    try
                    {
                        IntPtr currentProcess = kernel32.GetCurrentProcess();
                        if (advapi32.OpenProcessToken(currentProcess,
                            TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out tokenHandle))
                        {
                            if (advapi32.AdjustTokenPrivileges(tokenHandle, false, ref TOKEN_PRIVILEGES, 1024,
                                IntPtr.Zero, IntPtr.Zero))
                            {
                                if (Marshal.GetLastWin32Error() == ERROR_NOT_ALL_ASSIGNED)
                                    throw new InvalidOperationException("AdjustTokenPrivileges failed.",
                                        new Win32Exception());
                            }
                            else
                                throw new InvalidOperationException("AdjustTokenPrivileges failed.",
                                    new Win32Exception());
                        }
                        else
                            throw new InvalidOperationException(
                                string.Format(CultureInfo.InvariantCulture,
                                    "OpenProcessToken failed. CurrentProcess: {0}", currentProcess.ToInt32()),
                                new Win32Exception());
                    }
                    finally
                    {
                        if (tokenHandle != IntPtr.Zero)
                            kernel32.CloseHandle(tokenHandle);
                    }
                }
                else
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.InvariantCulture,
                            "LookupPrivilegeValue failed. SecurityEntityValue: {0}", securityEntityValue),
                        new Win32Exception());
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.InvariantCulture, "GrantPrivilege failed. SecurityEntity: {0}",
                        securityEntityValue), e);
            }
        }

        public static SecurityEntity EntityToEntity(SecurityEntity2 entity) => (SecurityEntity) entity;

        [StructLayout(LayoutKind.Sequential)]
        internal struct LUID
        {
            internal int LowPart;
            internal uint HighPart;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct TOKEN_PRIVILEGES
        {
            internal int PrivilegeCount;
            internal LUID Luid;
            internal int Attributes;
        }
    }
}