'--------------------------------------------------------------------------------------------------
' libRights - User rights functions
'    (c) 2026 Remus Rigo
'       v1.0 2026-03-25
'--------------------------------------------------------------------------------------------------

Imports System.Security.Principal

Public Module libRights

   '-----------------------------------------------------------------------------------------------
   ' Check if the current user has administrator privileges
   Public Function IsAdministrator() As Boolean
      Dim identity = WindowsIdentity.GetCurrent()
      Dim principal = New WindowsPrincipal(identity)
      Return principal.IsInRole(WindowsBuiltInRole.Administrator)
   End Function

End Module

