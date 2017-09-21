Imports System.Drawing.Imaging
Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports System.Math

'Главное окно программы, обеспечивающее редактирование и моделирование горда

Public Class Editor 
    Public ProgTitle As String = "Virtual City"    'Заголовок

    Public EdMode As EditMode       'Режим редактирования

    Public CT As City               'Город, который моделируем
    Private CT_Loaded As Boolean = False

    Private CityScale As Single = 1             'Масштаб  
    Private CityTranslate As Point
 
    Dim MousePressed As Boolean = False 
    Dim HsEditMode As Integer = 0

    Const StandartModelStep As Integer = 50 'Стандартный шаг моделирования в милисекундах
    Const MaxCitySize As Integer = 4000
     
    Const ConfirmDeleting As Boolean = False

    'Video
    Dim NeedMoveCB As Boolean = False
    Dim NewCBRect As Rectangle
    Dim VideoOptimize As Boolean

    'Config 
    'Private PresentationMode As Boolean = False 

    Public Enum EditMode
        None
        AddIntersection
        AddRoad1
        AddRoad2
        AddBuilding
        AddBuilding1
        AddCar
        MoveIntersection
        MoveHouse
    End Enum

    Dim OldIntScPos As PointF

    'Возвращает точку в координатах города, где находится курсор
    Public Function GetCursorPoint() As PointF
        Dim pnt As Point = PointToClient(Cursor.Position)
        Dim x As Integer = pnt.X - ContainerPanel.Location.X - CityContainer.Location.X  '- BeautyMargin
        Dim y As Integer = pnt.Y - ContainerPanel.Location.Y - CityContainer.Location.Y '- BeautyMargin
        Return New PointF(x / CityScale, y / CityScale)
    End Function

    'Происходит при нажатии на область города
    'Если левой кнопкой - то в зависимости от режима редактирования добавляем или выделяем какой-то объект
    'Еслит правой кнопкой - удалеям объект / начинаем перетакскивать город
    Private Sub CityBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CityBox.MouseDown
        Dim pnt = GetCursorPoint()
        CT.Cursor = pnt
        PropertiesMenu.Deselect()

        Select Case e.Button
            Case MouseButtons.Left
                MousePressed = True

                Select Case EdMode
                    Case EditMode.None
                        CT.SelectObject()
                        PropertiesMenu.ChangeSelectedObject(CT)
                        If CT.SelObj = City.ObjectType.Intersection Then
                            OldIntScPos = CT.IntScs(CT.SelNum).GetPoint
                            EdMode = EditMode.MoveIntersection
                        End If

                  

                    Case (EditMode.AddIntersection)

                        CT.AddIntersection(pnt.X, pnt.Y)
                        ' CT.DrawCity(CityBox.Image)
                    Case EditMode.AddRoad1
                        Dim n As Integer
                        If CT.GetIntersection(pnt, n) Then
                            CT.StartAddRoad(n)
                            EdMode = EditMode.AddRoad2
                        End If
                    Case EditMode.AddRoad2
                        Dim n As Integer
                        If CT.GetIntersection(pnt, n) Then
                            CT.EndAddRoad(n)
                            EdMode = EditMode.AddRoad1
                        End If
                    Case EditMode.AddCar
                        CT.AddCar(pnt)
                    Case EditMode.AddBuilding
                        CT.BeginAddBuilding()
                        EdMode = EditMode.AddBuilding1
                    Case EditMode.AddBuilding1
                        CT.EndAddBuilding()
                        EdMode = EditMode.AddBuilding

                End Select

            Case MouseButtons.Right
                Select Case EdMode
                    Case EditMode.None
                        CT.Cursor = GetCursorPoint()
                        CT.SelectObject()
                        PropertiesMenu.ChangeSelectedObject(CT)
                        If CT.SelObj = City.ObjectType.Intersection Then
                            If ConfirmDel("Видалити перехрестя " & CT.IntScs(CT.SelNum).Name) Then
                                CT.RemoveIntersection(CT.SelNum)
                                CT.SelectObject()
                            End If
                        ElseIf CT.SelObj = City.ObjectType.Road Then
                            If ConfirmDel("Видалити дорогу " & CT.Roads(CT.SelNum).Name & "?") Then
                                CT.RemoveRoad(CT.SelNum)
                                CT.SelectObject()
                            End If
                        ElseIf CT.SelObj = City.ObjectType.Car Then
                            If ConfirmDel("Видалити машину " & CT.Cars(CT.SelNum).Name & "?") Then
                                CT.RemoveCar(CT.SelNum)
                                CT.SelectObject()
                            End If
                        ElseIf CT.SelObj = City.ObjectType.Building Then
                            If ConfirmDel("Видалити будинок " & CT.Houses(CT.SelNum).Name & "?") Then
                                CT.RemoveBuilding(CT.SelNum)
                                CT.SelectObject()
                            End If
                        End If
                    Case EditMode.AddRoad2
                        EdMode = EditMode.AddRoad1
                        CT.CancelAddingRoad()
                    Case EditMode.AddBuilding1
                        EdMode = EditMode.AddBuilding
                        CT.CancelAddBuilding()

                End Select
 
        End Select
    End Sub

    Public Function ConfirmDel(ByVal Text As String) As Boolean
        If Not ConfirmDeleting Then Return True
        Return MessageBox.Show(Text, "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes
    End Function

    'Завершение перемещения перекрёстка
    Public Sub EndMoveIntSc()
        If CT.CheckForCrossingAfterMoveIntersection(CT.SelNum) Then
            CT.MoveIntersection(CT.SelNum, OldIntScPos.X, OldIntScPos.Y)
        End If
        EdMode = EditMode.None
    End Sub

    'Происходит при отпускании кнопки мыши на области города
    Private Sub CityBox_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CityBox.MouseUp
 
        Dim pnt = GetCursorPoint()

        Select Case e.Button
            Case MouseButtons.Left
                MousePressed = False
            
                Select Case EdMode
                    Case EditMode.MoveIntersection
                        EndMoveIntSc()
                End Select

            Case MouseButtons.Right
                Select Case EdMode

                End Select 
        End Select
    End Sub


    'Инициализация при запуске
    Private Sub LoadingEditor(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = ProgTitle

        Tools.Show()
        Tools.Owner = Me
        Tools.DisableAll() 

        PropertiesMenu.Show()
        PropertiesMenu.Owner = Me 

        Dim ScrWdh = My.Computer.Screen.Bounds.Width - 10
        Tools.Location = New Point(ScrWdh - Tools.Width, 50)
        PropertiesMenu.Location = New Point(ScrWdh - PropertiesMenu.Width, Tools.Height + 70)

        Randomize()

        'Если программа запустилась с аргументом, пытаемся открыть указанный в нём файл
        If Command.Length > 0 Then
            If Not LoadFromFile(Command.Replace("""", "")) Then Application.Exit()
        End If

        CityBox.Location = New Point(0, 0)
        CityBox.Size = New Size(0, 0)
        CityContainer.Location = New Point(0, 0)
        CityContainer.Size = New Size(0, 0)

        PrepareImages()
    End Sub

    'Прорисовка города независимо от действий пользователя и моделирования - 20 раз в секунду
    Private Sub DrawStep() Handles TimerDraw.Tick
 

        Dim pnt As PointF = GetCursorPoint()
        CT.Cursor = pnt
        If EdMode = EditMode.MoveIntersection Then CT.MoveIntersection(CT.SelNum, pnt.X, pnt.Y)

        Dim EdBuil As Integer = CT.CanEditHouse
        If EdBuil = 1 Or EdBuil = 3 Then CityBox.Cursor = Cursors.VSplit
        If EdBuil = 2 Or EdBuil = 4 Then CityBox.Cursor = Cursors.HSplit
        If EdBuil = 0 Then CityBox.Cursor = Cursors.Default
        If Not MousePressed Then
            HsEditMode = EdBuil
        End If

        Dim bmp As Bitmap = New Bitmap(CityBox.Width, CityBox.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)


        g.TranslateTransform(CityTranslate.X - BeautyMargin, CityTranslate.Y - BeautyMargin)
        g.ScaleTransform(CityScale, CityScale)

        CT.DrawCity(g)


        If NeedMoveCB Then
            If VideoOptimize Then
                CityBox.Size = NewCBRect.Size
                CityBox.Location = NewCBRect.Location
                CityBox.Image = bmp
            Else
                CityBoxDouble.Size = NewCBRect.Size
                CityBoxDouble.Location = NewCBRect.Location
                CityBoxDouble.Image = bmp
                CityBoxDouble.Show()
                CityBox.Size = NewCBRect.Size
                CityBox.Location = NewCBRect.Location
                CityBox.Image = bmp
                CityBoxDouble.Hide()
            End If
            NeedMoveCB = False

        Else
            CityBox.Image = bmp
        End If

        g.Dispose()
        LabelTime.Text = CT.GetTime
    End Sub


#Region "GraphicComponenets"

    'Делаем неактивными ползунки, если мышь не на них
    Private Sub FitTrackBar(ByRef trbar As TrackBar, ByVal pnt As Point)
        trbar.Enabled = (pnt.X > 0 And pnt.X < trbar.Width And pnt.Y > 0 And pnt.Y < trbar.Height)
    End Sub

    'Если курсор покинул область города
    Private Sub CityBox_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CityBox.MouseLeave
        If EdMode = EditMode.MoveIntersection Then
            EndMoveIntSc()
        End If
        MousePressed = False

    End Sub

    'Обновление  размеров компонентов при изменении размеров города
    Public Sub UpDateSize(ByVal Immideately As Boolean)
        CityContainer.Size = New Point(CT.Width * CityScale, CT.Height * CityScale)
        UpDatePos(Immideately)
    End Sub


    Private BeautyMargin As Integer = 150 'Насколько прорисовывается город вне оласти видимости

    'Обновление положений при масштабировании и смещении
    Sub UpDatePos(ByVal Immideately As Boolean)

        NewCBRect.Location = New Point(-CityContainer.Left, -CityContainer.Top)
        NewCBRect.Size = New Point(Min(ContainerPanel.Width, CityContainer.Right) + 2 * BeautyMargin, Min(ContainerPanel.Height, CityContainer.Bottom) + 2 * BeautyMargin)

        If Immideately Then
            CityBox.Location = NewCBRect.Location
            CityBox.Size = NewCBRect.Size
        Else
            NeedMoveCB = True
        End If

        CityTranslate = New Point(CityContainer.Left + BeautyMargin, CityContainer.Top + BeautyMargin)
    End Sub

    Sub ContainerChanged() Handles ContainerPanel.Scroll, ContainerPanel.SizeChanged
        If Not CT_Loaded Then Exit Sub
        UpDatePos(False)
        Console.WriteLine(ContainerPanel.AutoScrollPosition)
        ' If Not VideoOptimize Then DrawStep()
    End Sub

    'Включение-выключение оптимизации видео
    'При включенной оптимизации отключается дублирование пикчербокса и избыточная прорисовка, что вызывает неприятный вид при прокрутке и масштабировании
    Private Sub OptimizeViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptimizeViewToolStripMenuItem.Click
        sender.checked = Not sender.checked
        VideoOptimize = sender.checked
        If VideoOptimize Then
            BeautyMargin = 10
        Else
            BeautyMargin = 200
        End If
        UpDateSize(True)
    End Sub


#End Region

    Public Sub UpDateTitle()
        Me.Text = CT.Name & " - " & ProgTitle
    End Sub



#Region "Imaging"
    Public BitmapControl As Bitmap
    Public BitmapNarrow As Bitmap
    Public BitmapBridge As Bitmap

    Private Sub PrepareImages()
        BitmapControl = My.Resources.Control
        BitmapControl.MakeTransparent()
        BitmapNarrow = My.Resources.narrow
        BitmapNarrow.MakeTransparent()
        BitmapBridge = My.Resources.Bridge
        BitmapBridge.MakeTransparent()
    End Sub

#End Region

#Region "Modelling"

    'Начало моделирования
    Public Sub StartModelling() Handles ToolStripStart.Click, НачатьToolStripMenuItem.Click
        CT.IsModelling = True
        ModellingTimer.Start()
        EnableModellingButtons(True)
        PropertiesMenu.CityModelStart()
    End Sub

    'Остановка моделирования
    Public Sub StopModelling() Handles ToolStripStop.Click, ОстановитьToolStripMenuItem.Click
        CT.IsModelling = False
        ModellingTimer.Stop()
        EnableModellingButtons(False)
        PropertiesMenu.CityModelStop()
    End Sub

    'Шаг моделирования
    Private Sub ModellingTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ModellingTimer.Tick
        Try
            CT.ModelStep(StandartModelStep / 1000)
        Catch ex As Exception
            MessageBox.Show("Під час моделювання виникла помилка:" & vbCrLf & ex.ToString & vbCrLf & "Створене Вами місто не відповыдає вимогам або Ви виявили баг в програмі.")
        End Try
    End Sub

#End Region

#Region "FileIO"
    Public NowEditingFile As String
    Public IsSaved As Boolean = False

    'Создание пустого города
    Private Sub CreateNewCity() Handles СоздатьToolStripMenuItem.Click, СоздатьToolStripButton.Click
        FormNewCity.NumericUpDown1.Maximum = MaxCitySize
        FormNewCity.NumericUpDown2.Maximum = MaxCitySize

        If FormNewCity.ShowDialog = DialogResult.OK Then
            CT = New City(FormNewCity.NumericUpDown1.Value, FormNewCity.NumericUpDown2.Value)
            CT.Name = FormNewCity.TextBox1.Text
            Me.UpDateSize(True)
            Me.UpDateTitle()
            Me.RecountMaxScale()
            TimerDraw.Start()
            Tools.EnableAll()
            PanelControl.Enabled = True
            NowEditingFile = ""
            EnableButtons()
        End If
        IsSaved = False
    End Sub

    'Сохранение города в файл
    Private Sub SaveToFile() Handles СохранитькакToolStripMenuItem.Click
        Dim sfd As New SaveFileDialog()
        sfd.Title = "Зберегти як"
        sfd.Filter = "Проекти Virtual City|*.vcity"
        sfd.InitialDirectory = CurDir()
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            CT.Save(sfd.FileName)
        End If
        IsSaved = True
    End Sub


    'Сохранение открытого файла
    Private Sub SaveEditingCity(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles СохранитьToolStripMenuItem.Click, СохранитьToolStripButton.Click
        If IsSaved Then
            CT.Save(NowEditingFile)
        Else
            SaveToFile()
        End If
    End Sub

    'Попытка загрузки города из файла
    Private Function LoadFromFile(ByVal FileName As String) As Boolean
        Dim t As New City()
        Try
            Dim s As String = ReadAllText(FileName)
            t = New City(s)
            CT = t
            Me.UpDateSize(True)
            Me.UpDateTitle()

            Me.RecountMaxScale()
            TimerDraw.Start()
            Tools.EnableAll()
            PanelControl.Enabled = True
            NowEditingFile = FileName
            IsSaved = True
            EnableButtons()
            PropertiesMenu.Deselect()

            CT.Init()
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub LoadFromFileWithDialog() Handles ОткрытьToolStripButton.Click, ОткрытьToolStripMenuItem.Click
        Dim ofd As New OpenFileDialog()
        ofd.Title = "Відкрити"
        ofd.Filter = "Проекти Virtual City|*.vcity"
        ofd.InitialDirectory = CurDir()
        If ofd.ShowDialog = DialogResult.OK Then
            If Not LoadFromFile(ofd.FileName) Then
                MessageBox.Show("Помилка під час завантаження!")
            End If
        End If

    End Sub


    'Сохранение скриншота в картинку
    Private Sub ToPicture() Handles ЭкспортИзображенияToolStripMenuItem.Click, ToolStripExportImage.Click
        Dim sfd As New SaveFileDialog
        sfd.Title = "Експорт зображення"
        sfd.Filter = "Графічні файли JPEG|*.jpg|Графічні файли BMP|*.bmp"
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            CityBox.Image.Save(sfd.FileName)
        End If
    End Sub
#End Region

#Region "Interface"
    Private Sub EnableButtons()
        СохранитьToolStripButton.Enabled = True
        СохранитьToolStripMenuItem.Enabled = True
        СохранитькакToolStripMenuItem.Enabled = True
        ToolStripStart.Enabled = True
        НачатьToolStripMenuItem.Enabled = True
        АвтомоделированиеToolStripMenuItem.Enabled = True
        ToolStripButtonAutoModel.Enabled = True
        ПравкаToolStripMenuItem.Enabled = True
        ToolStripAlert.Enabled = True
        ПоказуватиВідміткиToolStripMenuItem.Enabled = True
        Me.KeyPreview = True
        CT_Loaded = True
        CT.ShowAnalyse = ToolStripAlert.Checked
    End Sub

    Private Sub EnableModellingButtons(ByVal IsModelling As Boolean)
        ToolStripStart.Enabled = Not IsModelling
        НачатьToolStripMenuItem.Enabled = Not IsModelling
        ToolStripStop.Enabled = IsModelling
        ОстановитьToolStripMenuItem.Enabled = IsModelling
    End Sub



#End Region

    'Изменение ускорения времени
    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Label1.Text = TrackBar1.Value
        ModellingTimer.Interval = StandartModelStep / TrackBar1.Value
    End Sub

    'Изменение масштаба
    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        ChangeScale(TrackBar2.Value / 100)
    End Sub

    Private Sub CityBox_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CityBox.MouseMove
        If MousePressed And HsEditMode > 0 Then CT.EditHouse(HsEditMode)
    End Sub

    Private Sub ExitApp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ВыходToolStripMenuItem.Click
        Application.Exit()
    End Sub


    Private Sub PropWindow(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ОкноПToolStripMenuItem.Click, ToolStripTrammel.Click
        If ToolStripTrammel.Checked Then
            ToolStripTrammel.Checked = False
            ОкноПToolStripMenuItem.Checked = False
            PropertiesMenu.Hide()
        Else
            ToolStripTrammel.Checked = True
            ОкноПToolStripMenuItem.Checked = True
            PropertiesMenu.Show()
        End If
    End Sub


    Private Sub ShowHelp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles РуководствоПользователяToolStripMenuItem.Click, ToolStripHelp.Click
        Try

            Additional.ShellExecute(CurDir() & "/Help.chm")
        Catch ex As Exception
            MessageBox.Show("Довідку не знайдено")
        End Try
    End Sub


    Private Sub AllignToGridToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ВыровнятьПоСеткеToolStripMenuItem.Click
        Dim GridStep As Integer
        Try
            GridStep = InputBox("Введите шаг сетки от 2 до 10")
            If GridStep < 2 Or GridStep > 10 Then Throw New Exception
            CT.AllignToGrid(GridStep)
        Catch ex As Exception
            MessageBox.Show("Ошибка!")
        End Try

    End Sub



    Private Sub AutoModelling(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles АвтомоделированиеToolStripMenuItem.Click, ToolStripButtonAutoModel.Click
        StopModelling()
        Dim dlg As New DialogAutoModel
        dlg.ShowDialog()
    End Sub


    Private Sub CreateGrid(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles СоздатьСеткуКварталовToolStripMenuItem1.Click
        Try
            If Not CT.CreateGrid(InputBox("Введіть довжину квартала")) Then Throw New Exception()
        Catch ex As Exception
            MsgBox("Помилка")
        End Try
    End Sub

    Private Sub RemoveAllCars(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles УдалитьВсеМашиныToolStripMenuItem.Click
        Try
            CT.RemoveAllCars()
        Catch ex As Exception
            MessageBox.Show("Помилка!")
        End Try
    End Sub

    Private Sub ОчиститьToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ОчиститьToolStripMenuItem.Click
        If MessageBox.Show("Очистити місто?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            CT.ClearAll()
        End If
    End Sub



    Private Sub ЗадатьОдинЦветToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ЗадатьОдинЦветToolStripMenuItem.Click
        Dim cd As New ColorDialog()
        If cd.ShowDialog = Windows.Forms.DialogResult.OK Then
            For i = 1 To CT.HsCounter
                If CT.Houses(i).Exists Then
                    CT.Houses(i).Color = cd.Color
                End If
            Next
        End If
    End Sub

    Private Sub ЗадатьСлучайныеЦветаToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ЗадатьСлучайныеЦветаToolStripMenuItem.Click
        For i = 1 To CT.HsCounter
            If CT.Houses(i).Exists Then CT.Houses(i).Color = Color.FromArgb(255, 255 * Rnd(), 255 * Rnd(), 255 * Rnd())
        Next
    End Sub

    'Пересчёт максимально возможного масштаба
    Public Sub RecountMaxScale()
        Dim maxSize As Integer = Max(CT.Width, CT.Height)
        Dim MaxScale As Single = Min(5, (MaxCitySize / maxSize))
        Dim mx As Integer = Int(100 * MaxScale)
        If TrackBar2.Value > mx Then TrackBar2.Value = mx
        TrackBar2.Maximum = mx
    End Sub


    'Изменение масштаба
    Private Sub ChangeScale(ByVal NewScale As Single)

        CityScale = NewScale
        UpDateSize(False)
        Label3.Text = String.Format("{0}%, {1:f2} м/пікс", Int(100 * CityScale), City.ScaleReal / CityScale)


    End Sub


    Private Sub ПроПрограмуToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ПроПрограмуToolStripMenuItem.Click
        AboutBox1.Show()
    End Sub

    Private Sub Editor_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub


    Private Sub ToolStripAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripAlert.Click, ПоказуватиВідміткиToolStripMenuItem.Click
        sender.Checked = Not sender.Checked
        CT.ShowAnalyse = sender.checked
        ToolStripAlert.Checked = CT.ShowAnalyse
        ПоказуватиВідміткиToolStripMenuItem.Checked = CT.ShowAnalyse
    End Sub


    Private CheatString As String = "0123456789"
    Private Sub Editor_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If Not CT_Loaded Then Exit Sub
        Dim s As String = e.KeyChar
        CheatString += s
        If CheatString.Length > 10 Then CheatString = CheatString.Substring(1, CheatString.Length - 1)

        If CheatString.Substring(7, 3).ToLower = "gta" And Not CT.GTA_Active Then
            CT.GTA_Activate()
        End If
        If CheatString.Substring(6, 4).ToLower = "drug" Then
            CT.DRUG()
        End If

    End Sub


    Private Sub Editor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If Not CT_Loaded Then Exit Sub
        If Not CT.GTA_Active Then Exit Sub
        If e.KeyCode >= 37 And e.KeyCode <= 40 Then CT.GTA_StartMove(e.KeyCode)
        If e.KeyCode = 87 Then CT.GTA_StartMove(38)
        If e.KeyCode = 68 Then CT.GTA_StartMove(39)
        If e.KeyCode = 83 Then CT.GTA_StartMove(40)
        If e.KeyCode = 65 Then CT.GTA_StartMove(37)

        If e.KeyCode = 32 Then CT.GTA_Shot()
    End Sub

    Private Sub Editor_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If Not CT_Loaded Then Exit Sub
        If Not CT.GTA_Active Then Exit Sub
        If e.KeyCode >= 37 And e.KeyCode <= 40 Then CT.GTA_StopMove()
        If e.KeyCode = 87 Or e.KeyCode = 68 Or e.KeyCode = 83 Or e.KeyCode = 65 Then CT.GTA_StopMove()
    End Sub


    Private Sub ЗадатьСкоростьВыездаМашинToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ЗадатьСкоростьВыездаМашинToolStripMenuItem.Click
        Dim MaxNum, Num As Integer
        Try
            MaxNum = InputBox(String.Format("Задайте місткість всіх будинків (0..{0})", Building.MaxMaxCarNum))
            If MaxNum < 0 Or MaxNum > 100 Then Throw New Exception
            Num = InputBox(String.Format("Задайте кількість автомобілів в кожному будинку (0..{0})", MaxNum))
            If Num < 0 Or Num > MaxNum Then Throw New Exception

            For i = 1 To CT.HsCounter
                If CT.Houses(i).Exists Then
                    CT.Houses(i).MaxCarsNum = MaxNum
                    CT.Houses(i).CarsNum = Num
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Помилка.")
        End Try

    End Sub

    Private Sub TimerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerRefresh.Tick
        If Not CT_Loaded Then Exit Sub
        PropertiesMenu.RefreshNumbers(CT)

        Dim pnt As Point = PointToClient(Cursor.Position)
        FitTrackBar(TrackBar1, pnt - TrackBar1.Location - PanelControl.Location)
        FitTrackBar(TrackBar2, pnt - TrackBar2.Location - PanelControl.Location)
    End Sub

     
End Class
