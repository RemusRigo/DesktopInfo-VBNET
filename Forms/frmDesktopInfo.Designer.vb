<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDesktopInfo
   Inherits clsAcrylicForm

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()>
   Protected Overrides Sub Dispose(disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()>
   Private Sub InitializeComponent()
      SuspendLayout()
      ' 
      ' frmDesktopInfo
      ' 
      AutoScaleDimensions = New SizeF(7.0F, 15.0F)
      AutoScaleMode = AutoScaleMode.Font
      ClientSize = New Size(800, 450)
      Name = "frmDesktopInfo"
      Text = "DesktopInfo"
      TransparencyKey = Color.FromArgb(CByte(0), CByte(0), CByte(1))
      ResumeLayout(False)
   End Sub

End Class
