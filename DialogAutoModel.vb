Imports System.Windows.Forms
Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports VirtualCity.Additional

Public Class DialogAutoModel
    Private TextReportFileSelected As Boolean = False
    Private GraphReportFileSelected As Boolean = False

    Private ModellingDone As Boolean

    'AM = AutoModelling
    Public AM_Time As Integer
    Public AM_Interval As Single
    Public AM_DoTextReport As Boolean
    Public AM_DoGraphReport As Boolean
    Public AM_TextReportFile As String
    Public AM_GraphReportFile As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If (CheckBox1.Checked And Not TextReportFileSelected) Or (CheckBox2.Checked And Not GraphReportFileSelected) Then
            MessageBox.Show("Файл не обрано!")
            Exit Sub
        End If


        AM_DoGraphReport = CheckBox2.Checked
        AM_DoTextReport = CheckBox1.Checked
        AM_GraphReportFile = TextBox2.Text
        AM_TextReportFile = TextBox1.Text
        AM_Interval = NumericUpDown2.Value
        AM_Time = NumericUpDown1.Value

        Label3.Text = "Моделювання..."

        Timer1.Start()
    End Sub

    'Таймер обеспечивает задержку чтобы показать надпись "Моделювання..."
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        AutoModel(Editor.CT)
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Button1.Enabled = sender.Checked
        If CheckBox1.Checked Then Button1.PerformClick()
    End Sub
 
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Button2.Enabled = sender.Checked
        Panel1.Enabled = sender.checked
        If CheckBox2.Checked Then Button2.PerformClick()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ModellingDone Then
            ShellExecute(TextBox1.Text)
        Else
            SaveFileDialog1.Filter = "Текстовые файлы|*.txt"
            SaveFileDialog1.FileName = ""
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                TextBox1.Text = SaveFileDialog1.FileName
                TextReportFileSelected = True
            End If
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ModellingDone Then
            ShellExecute(TextBox2.Text)
        Else
            SaveFileDialog1.Filter = "Графические файлы JPEG|*.jpg|Графические файлы BMP|*.bmp"
            SaveFileDialog1.FileName = ""
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                TextBox2.Text = SaveFileDialog1.FileName
                GraphReportFileSelected = True
            End If
        End If
    End Sub

    Private Sub DialogAutoModel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        OK_Button.Enabled = True
        ModellingDone = False
        ProgressBar1.Value = 0

        Label3.Text = ""

        SaveFileDialog1.InitialDirectory = CurDir()
        SaveFileDialog1.Title = "Выберите месторасположение отчёта"
    End Sub

    Private Sub AutoModel(ByRef CT As City)

        Dim StartTime As String = CT.GetTime()
        Dim NumSteps As Integer = AM_Time / AM_Interval
        CT.ResetCounters()
        Me.Cursor = Cursors.WaitCursor
        OK_Button.Enabled = False
        For i = 1 To NumSteps
            CT.ModelStep(AM_Interval)
            ProgressBar1.Value = CInt(100 * i / NumSteps)
        Next



        Dim AvgAvgSpeed(CT.RoadCounter) As Single

        For i = 1 To CT.RoadCounter
            If CT.Roads(i).Exists Then
                AvgAvgSpeed(i) = CT.Roads(i).Analyse_AvgSpeedSummator / CT.Roads(i).Analyse_AvgSpeedCounter
                If AvgAvgSpeed(i) > 40 Then AvgAvgSpeed(i) = 40
            End If
        Next



        If AM_DoTextReport Then
            Dim ToWrite As String = "Звіт автоматичного моделювання програми " & Editor.ProgTitle & vbCrLf
            ToWrite &= "Створений: " & Now & vbCrLf
            ToWrite &= vbCrLf & "**************************" & vbCrLf & vbCrLf
            ToWrite &= "Інформація про місто" & vbCrLf & vbCrLf
            ToWrite &= "Файл: " & Editor.NowEditingFile & vbCrLf
            ToWrite &= "Назва: " & CT.Name & vbCrLf
            ToWrite &= "Розміри: " & CT.Width & "x" & CT.height & vbCrLf
            ToWrite &= "Час моделювання: " & StartTime & "-" & CT.GetTime & " (" & AM_Time & " секунд)." & vbCrLf
            ToWrite &= vbCrLf & "**************************" & vbCrLf & vbCrLf
            ToWrite &= "Інформація про будинки" & vbCrLf & vbCrLf
            ToWrite &= "Номер     Назва          Mашин виїхало     Mашин в'їхало" & vbCrLf
            For i = 1 To CT.HsCounter
                If CT.Houses(i).Exists Then
                    ToWrite &= FixLen(CStr(i), 10) & FixLen(CT.Houses(i).Name, 15) & FixLen(CT.Houses(i).Analyse_OutComeCounter, 18) & CT.Houses(i).Analyse_InComeCounter & vbCrLf
                End If
            Next
            ToWrite &= vbCrLf & "**************************" & vbCrLf & vbCrLf
            ToWrite &= "Информация про дороги" & vbCrLf & vbCrLf
            ToWrite &= "Номер     Назва         Mашин проїхало    Середня швидкість(м/с)" & vbCrLf
            For i = 1 To CT.RoadCounter
                If CT.Roads(i).Exists Then
                    ToWrite &= FixLen(CStr(i), 10) & FixLen(CT.Roads(i).Name, 14) & FixLen(CT.Roads(i).Analyse_PassCarsCounter, 18) & Format(AvgAvgSpeed(i), "0.00") & vbCrLf
                End If
            Next

            WriteAllText(AM_TextReportFile, ToWrite, False)

        End If

        If AM_DoGraphReport Then
            Dim Gray As Boolean = RadioButtonGray.Checked


            Dim Report As New Bitmap(CT.Width, CT.Height)
            Dim g As Graphics = Graphics.FromImage(Report)

            Dim CT_ShowAnalyse As Boolean = CT.ShowAnalyse
            CT.ShowAnalyse = False

            If Gray Then
                g.Clear(Color.White)
            Else
                g.Clear(CT.BgColor)
            End If

            Dim fnt As New Font("Arial", 10)
            Dim BestAvgSpeed As Single = 35


            For i = 1 To CT.RoadCounter
                If CT.Roads(i).Exists Then
                    Dim Koef As Integer = 256 * (AvgAvgSpeed(i) / BestAvgSpeed)
                    If Koef > 255 Then Koef = 255
                    Dim clr As Color

                    If Gray Then
                        clr = Color.FromArgb(255, 0.9 * Koef, 0.9 * Koef, 0.9 * Koef)
                    Else
                        clr = Color.FromArgb(255, 255 - Koef, Koef, 0) 
                    End If

                    g.FillPolygon(New SolidBrush(clr), CT.Roads(i).GetPolygon(CT))
                End If
            Next

            For i = 1 To CT.HsCounter
                If CT.Houses(i).Exists Then
                    Dim strToWrite As String = "->" & CT.Houses(i).Analyse_InComeCounter & vbCrLf & "<-" & CT.Houses(i).Analyse_OutComeCounter
                    If Gray Then
                        g.DrawRectangle(Pens.Black, CT.Houses(i).Rect)
                        g.DrawString(strToWrite, fnt, Brushes.Black, CT.Houses(i).GetMiddle)
                    Else
                        CT.Houses(i).Draw(g, False, CT.ShowAnalyse)
                        g.DrawString(strToWrite, fnt, Brushes.Blue, CT.Houses(i).GetMiddle)
                    End If

                End If
            Next

            For i = 1 To CT.RoadCounter
                If CT.Roads(i).Exists Then
                    Dim Mdl = CT.Roads(i).GetMiddle(CT)
                    Dim strToWrite As String = CT.Roads(i).Analyse_PassCarsCounter & vbCrLf & Format(AvgAvgSpeed(i), "0.00") & " m/s"
                    If Gray Then
                        g.DrawString(strToWrite, fnt, Brushes.Black, Mdl)
                    Else
                        g.DrawString(strToWrite, fnt, Brushes.Black, Mdl)
                    End If
                End If
            Next

            CT.ShowAnalyse = CT_ShowAnalyse

            Report.Save(AM_GraphReportFile)
        End If

        Label3.Text = ""
        Me.Cursor = Cursors.Default
        CheckBox1.Enabled = False
        CheckBox2.Enabled = False
        Panel1.Enabled = False


        Button1.Text = "Відкрити"
        Button2.Text = "Відкрити"
        MessageBox.Show("Моделювання успішно завершене.")


        ModellingDone = True
    End Sub


End Class
