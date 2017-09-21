Public Class PropertiesMenu

    'Это окно отвечает за редактирование параметров выделенных объектов

    Public Sub ChangeSelectedObject(ByRef CT As City)
        HideAllPanels()
        Select Case CT.SelObj

            Case City.ObjectType.Building
                With CT.Houses(CT.SelNum)
                    Me.Text = "Властивості будинку " & .Name
                    TextBox3.Text = .Name

                    NumericUpDown6.Value = .CarsNum
                    NumericUpDown7.Value = .MaxCarsNum
                    NumericUpDown6.Maximum = .MaxCarsNum
                    NumericUpDown7.Maximum = Building.MaxMaxCarNum
                    NumericUpDown10.Value = .IncomKoef
                    Panel2.BackColor = .Color
                End With
                PanelBuildingProp.Show()
                PanelBuildingProp.Enabled = True
            Case City.ObjectType.Car
                Me.Text = "Властивості автомобіля " & CT.Cars(CT.SelNum).Name
                PanelCarProp.Show()
                PanelCarProp.Enabled = False

                TextBox2.Text = CT.Cars(CT.SelNum).Name
                NumericUpDown13.Value = CT.Cars(CT.SelNum).Speed * City.ScaleReal
                PanelCarProp.Enabled = True
            Case City.ObjectType.Intersection
                Me.Text = "Властивості перехрестя " & CT.IntScs(CT.SelNum).Name
                PanelIntersectProp.Show()


                Label8.Text = "Координати: " & CT.IntScs(CT.SelNum).GetPoint.ToString
                Label10.Text = "Кількість доріг: " & CT.GetRoadsQuantity(CT.SelNum)
                ComboBox1.SelectedIndex = CT.IntScs(CT.SelNum).PassMode
                GroupBoxControlProgram.Hide()
                LinkLabelMainRoad.Hide()
                If CT.IntScs(CT.SelNum).PassMode = 1 Then
                    LinkLabelMainRoad.Show()
                    LinkLabelMainRoad.Text = "Головна дорога: " & CT.IntScs(CT.SelNum).MR1 & "->" & CT.IntScs(CT.SelNum).MR2
                End If
                If CT.IntScs(CT.SelNum).PassMode = 2 Then
                    GroupBoxControlProgram.Show()
                    Try
                        CT.IntScs(Editor.CT.SelNum).TrControl.ExportMap(ListBox1)
                        NumericUpDown5.Value = CT.IntScs(Editor.CT.SelNum).TrControl.Cycle
                    Catch ex As Exception
                    End Try
                End If

                PanelIntersectProp.Enabled = True
            Case City.ObjectType.None
                Me.Text = "Властивості міста " & CT.Name

                TextBox1.Text = CT.Name
                NumericUpDown1.Value = CT.Width
                NumericUpDown2.Value = CT.Height
                Panel1.BackColor = CT.BgColor

                PanelCityProp.Show()
                PanelCityProp.Enabled = True
            Case City.ObjectType.Road
                Me.Text = "Властивості дороги " & CT.Roads(CT.SelNum).Name

                TextBox4.Text = CT.Roads(CT.SelNum).Name
                NumericUpDown3.Value = CT.Roads(CT.SelNum).n1
                NumericUpDown4.Value = CT.Roads(CT.SelNum).n2

                PanelRoadProp.Show()
                PanelRoadProp.Enabled = True
        End Select
    End Sub


    Public Sub RefreshNumbers(ByRef CT As City)
        Select Case CT.SelObj
            Case City.ObjectType.Building
                NumericUpDown6.Value = CT.Houses(CT.SelNum).CarsNum
        End Select
    End Sub


    Public Sub Deselect()
        Me.Text = "Вікно властивостей"
        HideAllPanels()
        LinkLabelMainRoad.Hide()
        GroupBoxControlProgram.Hide()
    End Sub


    Private Sub HideAllPanels()
        PanelBuildingProp.Hide()
        PanelCarProp.Hide()
        PanelCityProp.Hide()
        PanelIntersectProp.Hide()
        PanelRoadProp.Hide()
        PanelBuildingProp.Enabled = False
        PanelCarProp.Enabled = False
        PanelCityProp.Enabled = False
        PanelIntersectProp.Enabled = False
        PanelRoadProp.Enabled = False
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If Not PanelCityProp.Enabled Then Exit Sub
        Editor.CT.Name = sender.text
        Editor.UpDateTitle()
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If Not PanelCityProp.Enabled Then Exit Sub
        Editor.CT.Width = sender.value
        Editor.RecountMaxScale()
        Editor.UpDateSize(True)
    End Sub

    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        If Not PanelCityProp.Enabled Then Exit Sub
        Editor.CT.height = sender.value
        Editor.RecountMaxScale()
        Editor.UpDateSize(True)
    End Sub

    Private Sub PropertiesMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Prepare()
        Deselect()
    End Sub


    Private Sub Panel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Panel1.BackColor = ColorDialog1.Color
            Editor.CT.BgColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Panel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel2.Click
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Panel2.BackColor = ColorDialog1.Color
            Editor.CT.Houses(Editor.CT.SelNum).Color = ColorDialog1.Color
        End If
    End Sub

    Private Sub UpDateRoadProperties(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged, NumericUpDown3.ValueChanged, NumericUpDown4.ValueChanged
        If Not PanelRoadProp.Enabled Then Exit Sub
        With Editor.CT.Roads(Editor.CT.SelNum)
            .Name = TextBox4.Text
            .n1 = NumericUpDown3.Value
            .n2 = NumericUpDown4.Value
            Me.Text = "Властивості дороги " & .Name
        End With
    End Sub

    Private Sub CheckRoadLines() Handles NumericUpDown3.ValueChanged, NumericUpDown4.ValueChanged
        If NumericUpDown3.Value = 0 And NumericUpDown4.Value = 0 Then
            MsgBox("Дорога не може мати нуль смуг!")
            NumericUpDown3.Value = 1
            NumericUpDown4.Value = 1
        End If
    End Sub



    Private Sub UpDateBuildingProperties(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged, _
           NumericUpDown10.ValueChanged, NumericUpDown6.ValueChanged, NumericUpDown7.ValueChanged
        If Not PanelBuildingProp.Enabled Then Exit Sub
        With Editor.CT.Houses(Editor.CT.SelNum)
            .Name = TextBox3.Text
            Me.Text = "Властивості будинку " & .Name
            .CarsNum = NumericUpDown6.Value
            .MaxCarsNum = NumericUpDown7.Value
            .IncomKoef = NumericUpDown10.Value

            NumericUpDown6.Maximum = .MaxCarsNum
        End With
    End Sub



    Private Sub Prepare()
        PanelBuildingProp.Location = New Point(0, 0)
        PanelCarProp.Location = New Point(0, 0)
        PanelCityProp.Location = New Point(0, 0)
        PanelIntersectProp.Location = New Point(0, 0)
        PanelRoadProp.Location = New Point(0, 0)
        Me.Size = New Point(250, 250)
    End Sub



    Private Sub UpDateCarProperties(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged, NumericUpDown13.ValueChanged
        If Not PanelCarProp.Enabled Then Exit Sub
        With Editor.CT.Cars(Editor.CT.SelNum)
            .Name = TextBox2.Text
            .Speed = NumericUpDown13.Value / City.ScaleReal
        End With
    End Sub

#Region "Traffic Control Settings"


    Private Sub ComboBox1_SelectedItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Not PanelIntersectProp.Enabled Then Exit Sub

        Select Case ComboBox1.SelectedIndex
            Case 0
                Editor.CT.IntScs(Editor.CT.SelNum).PassMode = 0
                GroupBoxControlProgram.Hide()
                LinkLabelMainRoad.Hide()
            Case 1
                Editor.CT.IntScs(Editor.CT.SelNum).PassMode = 1
                GroupBoxControlProgram.Hide()
                LinkLabelMainRoad.Show()
                Try
                    Dim MR1 As Integer = InputBox("Номер під'їзду з головної дороги")
                    Dim MR2 As Integer = InputBox("Номер іншого під'їзду з головної дороги")
                    If (MR1 = MR2) Or (Editor.CT.Roads(MR1).Exists = False) Or (Editor.CT.Roads(MR2).Exists = False) Then Throw (New Exception)
                    Editor.CT.IntScs(Editor.CT.SelNum).MR1 = MR1
                    Editor.CT.IntScs(Editor.CT.SelNum).MR2 = MR2
                    LinkLabelMainRoad.Text = "Головна дорога: " & Editor.CT.IntScs(Editor.CT.SelNum).MR1 & "->" & Editor.CT.IntScs(Editor.CT.SelNum).MR2
                Catch ex As Exception
                    Editor.CT.IntScs(Editor.CT.SelNum).PassMode = 0
                    ComboBox1.SelectedIndex = 0
                End Try

            Case 2
                GroupBoxControlProgram.Show()
                LinkLabelMainRoad.Hide()
                Editor.CT.IntScs(Editor.CT.SelNum).PassMode = 2
                Try
                    Editor.CT.IntScs(Editor.CT.SelNum).TrControl.CheckExist()
                Catch ex As Exception
                    Editor.CT.IntScs(Editor.CT.SelNum).TrControl = New TrafficControl(100)
                End Try

                Editor.CT.IntScs(Editor.CT.SelNum).TrControl.ExportMap(ListBox1)
                NumericUpDown5.Value = Editor.CT.IntScs(Editor.CT.SelNum).TrControl.Cycle
        End Select

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim tb% = InputBox("Час початку режиму")
            Dim te% = InputBox("Час кінця режиму")
            Dim ip% = InputBox("Дорога-під'їзд")
            Dim op% = InputBox("Дорога-від'їзд")
            Editor.CT.IntScs(Editor.CT.SelNum).TrControl.AddWay(tb, te, ip, op)
        Catch ex As Exception
            MsgBox("Помилка")
        End Try
        Editor.CT.IntScs(Editor.CT.SelNum).TrControl.ExportMap(ListBox1)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Editor.CT.IntScs(Editor.CT.SelNum).TrControl.RemoveWay(ListBox1.SelectedIndex + 1)
        Catch ex As Exception
            Exit Sub
        End Try
        Editor.CT.IntScs(Editor.CT.SelNum).TrControl.ExportMap(ListBox1)
    End Sub

    Private Sub NumericUpDown5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown5.ValueChanged
        If Not PanelIntersectProp.Enabled Then Exit Sub
        Editor.CT.IntScs(Editor.CT.SelNum).TrControl.Cycle = sender.Value
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim ccl As Integer
        Try
            ccl = InputBox("Половина циклу (сек)")
            If (ccl <= 0) Or (ccl > 1000) Then Throw New Exception
        Catch ex As Exception
            Exit Sub
        End Try
        If Not Editor.CT.IntScs(Editor.CT.SelNum).SetDefaultControl(ccl, Editor.CT) Then
            MessageBox.Show("Помилка. Має бути рівно 4 дороги.")
        End If
        Editor.CT.IntScs(Editor.CT.SelNum).TrControl.ExportMap(ListBox1)
        NumericUpDown5.Value = Editor.CT.IntScs(Editor.CT.SelNum).TrControl.Cycle
    End Sub

    Private Sub LinkLabelMainRoad_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelMainRoad.LinkClicked
        GroupBoxControlProgram.Hide()
        Try
            Dim MR1 As Integer = InputBox("Номер під'їзду з головної дороги")
            Dim MR2 As Integer = InputBox("Номер іншого під'їзду з головної дороги")
            If (MR1 = MR2) Or Not (Editor.CT.Roads(MR1).HasIntersection(Editor.CT.SelNum)) Or Not (Editor.CT.Roads(MR2).HasIntersection(Editor.CT.SelNum)) Then Throw (New Exception)
            Editor.CT.IntScs(Editor.CT.SelNum).MR1 = MR1
            Editor.CT.IntScs(Editor.CT.SelNum).MR2 = MR2
        Catch ex As Exception
            Editor.CT.IntScs(Editor.CT.SelNum).PassMode = 0
            ComboBox1.SelectedIndex = 0
        End Try
        LinkLabelMainRoad.Text = "Головна дорога: " & Editor.CT.IntScs(Editor.CT.SelNum).MR1 & "->" & Editor.CT.IntScs(Editor.CT.SelNum).MR2
    End Sub

#End Region


    Public Sub CityModelStart()
        NumericUpDown6.Enabled = False
        NumericUpDown7.Enabled = False
    End Sub

    Public Sub CityModelStop()
        NumericUpDown6.Enabled = True
        NumericUpDown7.Enabled = True
    End Sub
End Class