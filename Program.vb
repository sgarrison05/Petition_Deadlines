Option Strict On
Option Explicit On
Option Infer On

Imports System.Windows.Forms

Module Program

    <STAThread()>
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New frmMain())
    End Sub

End Module
