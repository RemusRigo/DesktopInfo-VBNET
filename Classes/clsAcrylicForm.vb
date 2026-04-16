Imports System.Runtime.InteropServices

'--------------------------------------------------------------------------------------------------
' Acrylic Form

Public Class clsAcrylicForm
   Inherits Form

   Private Shared ReadOnly KEY_COLOR As Color = Color.FromArgb(0, 0, 1) ' near-black, unique

   Private ReadOnly Property IsInDesigner As Boolean
      Get
         Return System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime
      End Get
   End Property
   'Private rows As New List(Of (x As Integer, y As Integer, text As String))

   'Public Sub AddLine(x As Integer, y As Integer, text As String)
   '   rows.Add((x, y, text))
   '   Me.Invalidate()  ' trigger repaint
   'End Sub


   <StructLayout(LayoutKind.Sequential)>
   Private Structure AccentPolicy
      Public AccentState As Integer
      Public AccentFlags As Integer
      Public GradientColor As Integer
      Public AnimationId As Integer
   End Structure

   Protected Overrides ReadOnly Property CreateParams As CreateParams
      Get
         Dim cp As CreateParams = MyBase.CreateParams
         'cp.ExStyle = cp.ExStyle Or &H200000  ' WS_EX_NOREDIRECTIONBITMAP
         'cp.ExStyle = cp.ExStyle Or &H20  ' WS_EX_TRANSPARENT
         cp.ExStyle = cp.ExStyle Or WS_EX_LAYERED Or WS_EX_TOOLWINDOW
         Return cp
      End Get
   End Property

   Protected Overrides Sub WndProc(ByRef m As Message)
      If m.Msg = &H14 Then
         m.Result = New IntPtr(1)
         Return
      End If
      MyBase.WndProc(m)
   End Sub

   Public Sub EnableAcrylic(Optional alpha As Byte = 80, Optional tint As Color = Nothing)
      'If Me.DesignMode OrElse Me.Handle = IntPtr.Zero Then Return

      If tint = Nothing Then tint = Color.Black

      Dim accent As New AccentPolicy With {
          .AccentState = 4,  ' ACCENT_ENABLE_ACRYLICBLURBEHIND
          .GradientColor = (alpha << 24) Or (tint.R << 16) Or (tint.G << 8) Or tint.B
      }

      Dim size As Integer = Marshal.SizeOf(accent)
      Dim ptr As IntPtr = Marshal.AllocHGlobal(size)
      Marshal.StructureToPtr(accent, ptr, False)

      Dim data As New user32.WindowCompositionAttributeData With {
          .Attribute = 19,
          .Data = ptr,
          .SizeOfData = size
      }

      user32.SetWindowCompositionAttribute(Me.Handle, data)
      Marshal.FreeHGlobal(ptr)
   End Sub

   Protected Overrides Sub OnLoad(e As EventArgs)
      MyBase.OnLoad(e)
      If IsInDesigner Then
         Me.BackColor = Color.CornflowerBlue       ' GDI fills background with this color...
      Else
         Me.FormBorderStyle = FormBorderStyle.None ' Removes the border and caption bar
         Me.ControlBox = False                     ' Removes Max/Min/Close logic
         Me.Text = ""                              ' Clears any title text
         Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or
                ControlStyles.UserPaint Or
                ControlStyles.OptimizedDoubleBuffer, True)
         Me.BackColor = KEY_COLOR       ' GDI fills background with this color...
         Me.TransparencyKey = KEY_COLOR ' ...and Windows punches it through to DWM acrylic
      End If
   End Sub

   'Protected Overrides Sub OnPaint(e As PaintEventArgs)
   '   Using brush As New SolidBrush(Color.White)
   '      Dim fnt As New Font("Segoe UI", 10)
   '      e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
   '      For Each row In rows
   '         e.Graphics.DrawString(row.text, fnt, brush, New PointF(row.x, row.y))
   '      Next
   '   End Using
   'End Sub

   Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
      ' Instead of doing nothing, fill with the TransparencyKey color
      ' This ensures the "hole" is punched correctly for the Acrylic to show,
      ' but gives child controls a surface to render against.
      Using br As New SolidBrush(Me.BackColor)
         e.Graphics.FillRectangle(br, e.ClipRectangle)
      End Using
   End Sub

   Protected Overrides Sub OnShown(e As EventArgs)
      MyBase.OnShown(e)
      If Not IsInDesigner Then
         EnableAcrylic()  ' call with defaults, or override in subclass
      End If
   End Sub

End Class
