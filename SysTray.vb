Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Module SysTray

   Private WithEvents tray As NotifyIcon
   Private trayContextMenu As ContextMenuStrip
   Private mnuItemOrganize As ToolStripMenuItem
   Private frm As frmDesktopInfo

   <STAThread>
   Sub Main()
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)

      mnuItemOrganize = New ToolStripMenuItem("Organize")
      mnuItemOrganize.CheckOnClick = True
      mnuItemOrganize.Checked = False
      AddHandler mnuItemOrganize.CheckedChanged, AddressOf SetOrganizeMode

      ' Build context menu
      trayContextMenu = New ContextMenuStrip()
      trayContextMenu.Items.Add(mnuItemOrganize)
      trayContextMenu.Items.Add(New ToolStripSeparator())
      trayContextMenu.Items.Add("Exit", Nothing, AddressOf OnExit)

      ' Configure tray icon
      tray = New NotifyIcon()
      tray.Text = "DesktopInfo v1.0"
      tray.Icon = SystemIcons.Application  ' swap with your own .ico
      tray.ContextMenuStrip = trayContextMenu
      tray.Visible = True
      tray.ShowBalloonTip(500, "DesktopInfo", "Running in background.", ToolTipIcon.Info)

      frm = New frmDesktopInfo()
      frm.ShowInTaskbar = False

      Application.Run(frm)
   End Sub

   Private Sub SetOrganizeMode(sender As Object, e As EventArgs)
      'clsWidget.isOrganizableMode = mnuItemOrganize.Checked
   End Sub

   Private Sub OnExit(sender As Object, e As EventArgs)
      tray.Visible = False
      tray.Dispose()

      If frm IsNot Nothing AndAlso Not frm.IsDisposed Then
         frm.Close()
      End If
   End Sub

End Module
