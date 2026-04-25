'--------------------------------------------------------------------------------------------------
' libNetworking - Networking functions
'    (C) 2026 Remus Rigo
'       v1.0.2026-04-24
'--------------------------------------------------------------------------------------------------

Imports System.Net
Imports System.Net.Http
Imports System.Net.NetworkInformation
Imports System.Net.Sockets

Module libNetworking

   ' Reuse a single HttpClient instance for better performance
   Private ReadOnly _httpClient As New HttpClient()

   '-----------------------------------------------------------------------------------------------
   ' Get the first non-loopback IPv4 address of the local machine
   Function GetPrivateIP() As String
      Dim hostName As String = Dns.GetHostName()
      Dim addresses = Dns.GetHostAddresses(hostName)

      For Each addr In addresses
         ' Skip loopback
         If addr.AddressFamily = Sockets.AddressFamily.InterNetwork AndAlso Not IPAddress.IsLoopback(addr) Then
            Return addr.ToString()
         End If
      Next

      Return "Not found"
   End Function

   '-----------------------------------------------------------------------------------------------
   ' Get all non-loopback IPv4 addresses of the local machine
   Function GetAllPrivateIPs() As List(Of String)
      Dim result As New List(Of String)

      For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
         ' Skip adapters that are not operational
         If ni.OperationalStatus <> OperationalStatus.Up Then Continue For

         Dim ipProps = ni.GetIPProperties()

         For Each ua In ipProps.UnicastAddresses
            If ua.Address.AddressFamily = AddressFamily.InterNetwork Then
               ' Skip loopback
               If Not IPAddress.IsLoopback(ua.Address) Then
                  result.Add(ua.Address.ToString())
               End If
            End If
         Next
      Next

      Return result
   End Function

   '-----------------------------------------------------------------------------------------------
   ' Get the public IP address by querying an external service
   Function GetPublicIP() As String
      Try
         Return _httpClient.GetStringAsync("https://api.ipify.org").Result.Trim()
      Catch ex As Exception
         Return "Not found: " & ex.Message
      End Try
   End Function

End Module
