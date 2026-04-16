Imports System.ComponentModel
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Runtime.Versioning
Imports System.Threading

Public Class frmDesktopInfo
   Inherits clsAcrylicForm

   Protected Overrides Sub OnVisibleChanged(e As EventArgs)
      MyBase.OnVisibleChanged(e)
      ' Ensure it stays behind other windows when shown
      SetWindowPos(Me.Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE Or SWP_NOMOVE Or SWP_NOACTIVATE)
   End Sub

   Private Sub frmDesktop_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
      SetWindowPos(Me.Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE Or SWP_NOMOVE Or SWP_NOACTIVATE)
   End Sub

   Private Sub frmDesktopInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Me.StartPosition = FormStartPosition.Manual
      Me.Location = New Point(5, 5)
      Me.Size = New Size(300, 60)
      Dim BackgroundWorker As New System.ComponentModel.BackgroundWorker()
      AddHandler BackgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
      AddHandler BackgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
      BackgroundWorker.RunWorkerAsync()

      Me.AutoSize = True
      Me.AutoSizeMode = AutoSizeMode.GrowAndShrink

      SendToBack()
   End Sub

   Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
      Dim myConnection As New ConnectionOptions()
      Dim scopePath As String
      scopePath = "\\.\root\cimv2"
      Dim scope As New ManagementScope(scopePath, myConnection)
      Dim items As New Dictionary(Of String, String)

      Try
         scope.Connect()
         Dim myQuery As New ObjectQuery("SELECT * FROM Win32_OperatingSystem")

         Using searcher As New ManagementObjectSearcher(scope, myQuery)

            For Each obj As ManagementObject In searcher.Get()
               If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Caption") Then
                  If obj("Caption") IsNot Nothing Then
                     items.Add("OS", obj("Caption").ToString())
                  End If
               End If

               If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Manufacturer") Then
                  If (obj("Manufacturer") IsNot Nothing) Then
                     items.Add("Manufacturer", obj("Manufacturer").ToString())
                  End If
               End If

               If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Version") Then
                  If (obj("Version") IsNot Nothing) Then
                     items.Add("Version", obj("Version").ToString())
                  End If
               End If

               If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "BuildNumber") Then
                  If (obj("BuildNumber") IsNot Nothing) Then
                     items.Add("Build Number", obj("BuildNumber").ToString())
                     Dim osVer, osCode, osRel, osEnd As String
                     Select Case obj("BuildNumber")
                        Case "002"
                           osVer = "3.11"
                           osCode = ""
                           osRel = "1993-11-08"
                        Case "102"
                           osVer = "3.10"
                           osCode = "Sparta"
                           osRel = "October 31, 1992"
                           osEnd = "December 31, 2001"
                        Case "103"
                           osVer = "3.10"
                           osCode = "Janus"
                           osRel = "April 6, 1992"
                           osEnd = "December 31, 2001"
                        Case "153"
                           osVer = "3.2"
                           osCode = ""
                           osRel = "November 22, 1993"
                           osEnd = "December 31, 2001"
                        Case "300"
                           osVer = "3.11"
                           osCode = "Snowball"
                           osRel = "November 8, 1993"
                           osEnd = "2001-12-31"
                        Case "528"
                           osVer = "NT 3.1"
                           osCode = "Razzle"
                           osRel = "July 27, 1993"
                           osEnd = "2000-12-31"
                        Case "807"
                           osVer = "NT 3.5"
                           osCode = "Daytona"
                           osRel = "September 21, 1994"
                           osEnd = "December 31, 2001"
                        Case "950"
                           osVer = "4.00"
                           osCode = "Chicago"
                           osRel = "August 24, 1995"
                           osEnd = "December 31, 2001"
                        Case "1057"
                           osVer = "NT 3.51"
                           osCode = "Daytona"
                           osRel = "May 30, 1995"
                           osEnd = "December 31, 2001"
                        Case "1381"
                           osVer = "NT 4.0"
                           osCode = "Shell Update Release (Tukwila)"
                           osRel = "August 24, 1996"
                           osEnd = "June 30, 2004"
                        Case "1998"
                           osVer = "4.10"
                           osCode = "Memphis"
                           osRel = "June 25, 1998"
                           osEnd = "July 11, 2006"
                        Case "2195"
                           osVer = "NT 5.0"
                           osCode = "Windows NT 5.0"
                           osRel = "February 17, 2000"
                           osEnd = "July 13, 2010"
                        Case "2222A"
                           osVer = "4.10"
                           osCode = "Memphis"
                           osRel = "June 10, 1999"
                           osEnd = "July 11, 2006"
                        Case "2600"
                           osVer = "NT 5.1"
                           osCode = "Whistler / Freestyle / Harmony"
                           osRel = "October 25, 2001 / October 29, 2002 / September 30, 2003"
                           osEnd = "April 8, 2014"
                        Case "2700"
                           osVer = "NT 5.1"
                           osCode = "Symphony"
                           osRel = "	October 12, 2004"
                           osEnd = "April 8, 2014"
                        Case "2710"
                           osVer = "NT 5.1"
                           osCode = "Emerald"
                           osRel = "	October 14, 2005"
                           osEnd = "April 8, 2014"
                        Case "3000"
                           osVer = "4.90"
                           osCode = "Millennium"
                           osRel = "September 14, 2000"
                           osEnd = "July 11, 2006"
                        Case "3790"
                           osVer = "NT 5.2"
                           osCode = "Anvil"
                           osRel = "April 25, 2005"
                           osEnd = "April 8, 2014"
                        Case "6002"
                           osVer = "NT 6.0"
                           osCode = "Longhorn"
                           osRel = "January 30, 2007"
                           osEnd = "April 11, 2017"
                        Case "7601"
                           osVer = "NT 6.1"
                           osCode = "Windows 7"
                           osRel = "January 30, 2007"
                           osEnd = "	January 14, 2020"
                        Case "9200"
                           osVer = "NT 6.2"
                           osCode = "Windows 8"
                           osRel = "October 26, 2012"
                           osEnd = "January 12, 2016"
                        Case "9600"
                           osVer = "NT 6.3"
                           osCode = "Blue"
                           osRel = "October 17, 2013"
                           osEnd = "January 10, 2023"
                        Case "10240"
                           osVer = "NT 10.0 / 1507"
                           osCode = "Threshold"
                           osRel = "July 29, 2015"
                           osEnd = "May 9, 2017"
                        Case "10586"
                           osVer = "1511"
                           osCode = "Threshold 2"
                           osRel = "November 10, 2015"
                           osEnd = "October 10, 2017"
                        Case "14393"
                           osVer = "1607"
                           osCode = "Redstone 1"
                           osRel = "August 2, 2016"
                           osEnd = "April 10, 2018"
                        Case "15063"
                           osVer = "1703"
                           osCode = "Redstone 2"
                           osRel = "April 5, 2017"
                           osEnd = "October 9, 2018"
                        Case "16299"
                           osVer = "1709"
                           osCode = "Redstone 3"
                           osRel = "October 17, 2017"
                           osEnd = "April 9, 2019"
                        Case "17134"
                           osVer = "1803"
                           osCode = "Redstone 4"
                           osRel = "April 30, 2018"
                           osEnd = "November 12, 2019"
                        Case "17763"
                           osVer = "1809"
                           osCode = "Redstone 5"
                           osRel = "November 13, 2018"
                           osEnd = "November 10, 2020"
                        Case "18362"
                           osVer = "1903"
                           osCode = "19H1"
                           osRel = "May 21, 2019"
                           osEnd = "December 8, 2020"
                        Case "18363"
                           osVer = "1909"
                           osCode = "Vanadium"
                           osRel = "November 12, 2019"
                           osEnd = "May 11, 2021"
                        Case "19041"
                           osVer = "2004"
                           osCode = "Vibranium"
                           osRel = "May 27, 2020"
                           osEnd = "December 14, 2021"
                        Case "19042"
                           osVer = "20H2"
                           osCode = "Vibranium"
                           osRel = "October 20, 2020"
                           osEnd = "August 9, 2022"
                        Case "19043"
                           osVer = "21H1"
                           osCode = "Vibranium"
                           osRel = "May 18, 2021"
                           osEnd = "December 13, 2022"
                        Case "19044"
                           osVer = "21H2"
                           osCode = "Vibranium"
                           osRel = "November 16, 2021"
                           osEnd = "June 13, 2023"
                        Case "19045"
                           osVer = "22H2"
                           osCode = "Vibranium"
                           osRel = "October 18, 2022"
                           osEnd = "October 14, 2025"
                        Case "22000"
                           osVer = "21H2"
                           osCode = "Cobalt"
                           osRel = "October 4, 2021"
                           osEnd = "October 10, 2023"
                        Case "22621"
                           osVer = "22H2"
                           osCode = "Nickel"
                           osRel = "September 20, 2022"
                           osEnd = "October 8, 2024"
                        Case "22631"
                           osVer = "23H2"
                           osCode = "Nickel"
                           osRel = "October 31, 2023"
                           osEnd = "November 11, 2025"
                        Case "26100"
                           osVer = "24H2"
                           osCode = "Germanium"
                           osRel = "October 1, 2024"
                           osEnd = "October 13, 2026"
                        Case "26200"
                           osVer = "25H2"
                           osCode = "Germanium"
                           osRel = "September 30, 2025"
                           osEnd = "October 12, 2027"
                        Case Else
                           osVer = "Unknown"
                           osCode = "Unknown"
                           osRel = "Unknown"
                           osEnd = "Unknown"
                     End Select
                     items.Add("Feature Update", osVer)
                     items.Add("Codename", osCode)
                     items.Add("Release Date", osRel)
                     items.Add("End of support", osEnd)
                  End If
               End If


            Next
         End Using
         e.Result = items
      Catch ex As Exception
         MsgBox(ex.Message)
      End Try

   End Sub
   Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
      If e.Error IsNot Nothing Then
         MsgBox("WMI Error: " & e.Error.Message)
         Return
      End If

      Dim items = TryCast(e.Result, Dictionary(Of String, String))
      Dim currentY As Integer = 10
      Dim rowHeight As Integer = 20 ' Distance between rows
      Dim titleFont As New Font("Segoe UI", 10, FontStyle.Bold)
      Dim keyFont As New Font("Segoe UI", 10, FontStyle.Bold)
      Dim valFont As New Font("Segoe UI", 10, FontStyle.Bold)

      Dim lblUserOnHost As New Label()
      lblUserOnHost.Text = Environment.UserName & " on " & Environment.MachineName
      lblUserOnHost.Location = New Point(10, currentY)
      lblUserOnHost.AutoSize = True
      'lblUserOnHost.Dock = DockStyle.Top
      lblUserOnHost.Height = rowHeight
      lblUserOnHost.Font = titleFont
      lblUserOnHost.ForeColor = Color.White ' Contrast color for keys
      lblUserOnHost.BackColor = Color.Transparent
      lblUserOnHost.UseCompatibleTextRendering = True
      Me.Controls.Add(lblUserOnHost)
      currentY += rowHeight

      For Each kvp As KeyValuePair(Of String, String) In items
         Dim lblKey As New Label()
         lblKey.Text = kvp.Key & ":"
         lblKey.Location = New Point(10, currentY)
         lblKey.AutoSize = True
         lblKey.Font = keyFont
         lblKey.ForeColor = Color.White ' Contrast color for keys
         lblKey.BackColor = Color.Transparent
         lblKey.UseCompatibleTextRendering = True

         ' Label 2: The Value
         Dim lblValue As New Label()
         lblValue.Text = kvp.Value
         lblValue.Location = New Point(120, currentY) ' Offset to the right
         lblValue.AutoSize = True
         lblValue.Font = valFont
         lblValue.ForeColor = Color.White
         lblValue.BackColor = Color.Transparent
         lblValue.UseCompatibleTextRendering = True

         ' Add to form
         Me.Controls.Add(lblKey)
         Me.Controls.Add(lblValue)

         ' Increment Y for the next iteration
         currentY += rowHeight
      Next
   End Sub

End Class
