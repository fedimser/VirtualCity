'Дополнительные функции, используемые в проекте

Module Additional


    Public Function FixLen(ByVal _str As String, ByVal _length As Integer)
        If _str.Length >= _length Then _str = _str.Substring(0, _length - 1)
        Dim ToAdd As Integer = _length - _str.Length()
        For i = 1 To ToAdd
            _str &= " "
        Next
        Return _str
    End Function


    Public Sub ShellExecute(ByVal File As String)
        Dim myProcess As New Process
        myProcess.StartInfo.FileName = File
        myProcess.StartInfo.UseShellExecute = True
        myProcess.StartInfo.RedirectStandardOutput = False
        myProcess.Start()
        myProcess.Dispose() 
    End Sub

    Private Function DescrColor(ByVal k As Single) As Color
        If k >= 0 And k <= 0.5 Then
            Return Color.FromArgb(255, 255 * (k / 0.5), 255, 0)
        ElseIf k <= 1 Then
            Return Color.FromArgb(255, 255, 255 - 255 * ((k - 0.5) / 0.5), 0)
        Else
            Return Color.White
        End If
    End Function

    Public Function DescrBrush(ByVal k As Single)
        Return New SolidBrush(DescrColor(k))
    End Function


    Structure Pair
        Dim First As Integer
        Dim Second As Integer
    End Structure

End Module
