<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Editor
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Editor))
        Me.TimerDraw = New System.Windows.Forms.Timer(Me.components)
        Me.ContainerPanel = New System.Windows.Forms.Panel()
        Me.CityContainer = New System.Windows.Forms.Panel()
        Me.CityBoxDouble = New System.Windows.Forms.PictureBox()
        Me.CityBox = New System.Windows.Forms.PictureBox()
        Me.LabelTime = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.TrackBar2 = New System.Windows.Forms.TrackBar()
        Me.ModellingTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ФайлToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.СоздатьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОткрытьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.СохранитьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.СохранитькакToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ЭкспортИзображенияToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ВыходToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ПравкаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ГородToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.СоздатьСеткуКварталовToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОчиститьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ВыровнятьПоСеткеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.МашиныToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.УдалитьВсеМашиныToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ДомаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ЗадатьСкоростьВыездаМашинToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ЗадатьОдинЦветToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ЗадатьСлучайныеЦветаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОкнаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОкноПToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ПоказуватиВідміткиToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptimizeViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.МоделированиеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.НачатьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОстановитьToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.АвтомоделированиеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.НалаштуванняToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.МоваToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.УкраъToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.СправкаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.РуководствоПользователяToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ПроПрограмуToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.СоздатьToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ОткрытьToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.СохранитьToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripExportImage = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripStart = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripStop = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonAutoModel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripTrammel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripAlert = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripHelp = New System.Windows.Forms.ToolStripButton()
        Me.PanelControl = New System.Windows.Forms.Panel()
        Me.TimerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.EnglishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContainerPanel.SuspendLayout()
        Me.CityContainer.SuspendLayout()
        CType(Me.CityBoxDouble, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CityBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.PanelControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TimerDraw
        '
        '
        'ContainerPanel
        '
        Me.ContainerPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContainerPanel.AutoScroll = True
        Me.ContainerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ContainerPanel.Controls.Add(Me.CityContainer)
        Me.ContainerPanel.Location = New System.Drawing.Point(18, 168)
        Me.ContainerPanel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ContainerPanel.Name = "ContainerPanel"
        Me.ContainerPanel.Size = New System.Drawing.Size(634, 277)
        Me.ContainerPanel.TabIndex = 5
        '
        'CityContainer
        '
        Me.CityContainer.Controls.Add(Me.CityBoxDouble)
        Me.CityContainer.Controls.Add(Me.CityBox)
        Me.CityContainer.Location = New System.Drawing.Point(118, 37)
        Me.CityContainer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CityContainer.Name = "CityContainer"
        Me.CityContainer.Size = New System.Drawing.Size(300, 154)
        Me.CityContainer.TabIndex = 1
        '
        'CityBoxDouble
        '
        Me.CityBoxDouble.Location = New System.Drawing.Point(104, 25)
        Me.CityBoxDouble.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CityBoxDouble.Name = "CityBoxDouble"
        Me.CityBoxDouble.Size = New System.Drawing.Size(30, 31)
        Me.CityBoxDouble.TabIndex = 1
        Me.CityBoxDouble.TabStop = False
        Me.CityBoxDouble.Visible = False
        '
        'CityBox
        '
        Me.CityBox.Location = New System.Drawing.Point(51, 25)
        Me.CityBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CityBox.Name = "CityBox"
        Me.CityBox.Size = New System.Drawing.Size(30, 31)
        Me.CityBox.TabIndex = 0
        Me.CityBox.TabStop = False
        '
        'LabelTime
        '
        Me.LabelTime.AutoSize = True
        Me.LabelTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelTime.Location = New System.Drawing.Point(454, 32)
        Me.LabelTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelTime.Name = "LabelTime"
        Me.LabelTime.Size = New System.Drawing.Size(143, 37)
        Me.LabelTime.TabIndex = 16
        Me.LabelTime.Text = "00:00:00"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(458, 8)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 20)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Час"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 2)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(146, 20)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Прискорення часу"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(274, 2)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 20)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "100%, 0.25 м/пікс"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(148, 32)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 20)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(186, 2)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 20)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Масштаб"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(-2, 26)
        Me.TrackBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TrackBar1.Maximum = 5
        Me.TrackBar1.Minimum = 1
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(156, 69)
        Me.TrackBar1.TabIndex = 8
        Me.TrackBar1.Value = 1
        '
        'TrackBar2
        '
        Me.TrackBar2.Location = New System.Drawing.Point(183, 26)
        Me.TrackBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TrackBar2.Maximum = 300
        Me.TrackBar2.Minimum = 10
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(262, 69)
        Me.TrackBar2.TabIndex = 10
        Me.TrackBar2.Value = 100
        '
        'ModellingTimer
        '
        Me.ModellingTimer.Interval = 50
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ФайлToolStripMenuItem, Me.ПравкаToolStripMenuItem, Me.ОкнаToolStripMenuItem, Me.МоделированиеToolStripMenuItem, Me.НалаштуванняToolStripMenuItem, Me.СправкаToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(9, 3, 0, 3)
        Me.MenuStrip1.Size = New System.Drawing.Size(670, 35)
        Me.MenuStrip1.TabIndex = 15
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ФайлToolStripMenuItem
        '
        Me.ФайлToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.СоздатьToolStripMenuItem, Me.ОткрытьToolStripMenuItem, Me.toolStripSeparator, Me.СохранитьToolStripMenuItem, Me.СохранитькакToolStripMenuItem, Me.toolStripSeparator1, Me.ЭкспортИзображенияToolStripMenuItem, Me.toolStripSeparator2, Me.ВыходToolStripMenuItem})
        Me.ФайлToolStripMenuItem.Name = "ФайлToolStripMenuItem"
        Me.ФайлToolStripMenuItem.Size = New System.Drawing.Size(65, 29)
        Me.ФайлToolStripMenuItem.Text = "&Файл"
        '
        'СоздатьToolStripMenuItem
        '
        Me.СоздатьToolStripMenuItem.Image = CType(resources.GetObject("СоздатьToolStripMenuItem.Image"), System.Drawing.Image)
        Me.СоздатьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.СоздатьToolStripMenuItem.Name = "СоздатьToolStripMenuItem"
        Me.СоздатьToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.СоздатьToolStripMenuItem.Size = New System.Drawing.Size(314, 30)
        Me.СоздатьToolStripMenuItem.Text = "&Нове місто"
        '
        'ОткрытьToolStripMenuItem
        '
        Me.ОткрытьToolStripMenuItem.Image = CType(resources.GetObject("ОткрытьToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ОткрытьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ОткрытьToolStripMenuItem.Name = "ОткрытьToolStripMenuItem"
        Me.ОткрытьToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.ОткрытьToolStripMenuItem.Size = New System.Drawing.Size(314, 30)
        Me.ОткрытьToolStripMenuItem.Text = "&Відкрити"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(311, 6)
        '
        'СохранитьToolStripMenuItem
        '
        Me.СохранитьToolStripMenuItem.Enabled = False
        Me.СохранитьToolStripMenuItem.Image = CType(resources.GetObject("СохранитьToolStripMenuItem.Image"), System.Drawing.Image)
        Me.СохранитьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.СохранитьToolStripMenuItem.Name = "СохранитьToolStripMenuItem"
        Me.СохранитьToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.СохранитьToolStripMenuItem.Size = New System.Drawing.Size(314, 30)
        Me.СохранитьToolStripMenuItem.Text = "&Зберегти"
        '
        'СохранитькакToolStripMenuItem
        '
        Me.СохранитькакToolStripMenuItem.Enabled = False
        Me.СохранитькакToolStripMenuItem.Name = "СохранитькакToolStripMenuItem"
        Me.СохранитькакToolStripMenuItem.Size = New System.Drawing.Size(314, 30)
        Me.СохранитькакToolStripMenuItem.Text = "Зберегти &як"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(311, 6)
        '
        'ЭкспортИзображенияToolStripMenuItem
        '
        Me.ЭкспортИзображенияToolStripMenuItem.Image = CType(resources.GetObject("ЭкспортИзображенияToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ЭкспортИзображенияToolStripMenuItem.Name = "ЭкспортИзображенияToolStripMenuItem"
        Me.ЭкспортИзображенияToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.ЭкспортИзображенияToolStripMenuItem.Size = New System.Drawing.Size(314, 30)
        Me.ЭкспортИзображенияToolStripMenuItem.Text = "Експорт зображення"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(311, 6)
        '
        'ВыходToolStripMenuItem
        '
        Me.ВыходToolStripMenuItem.Name = "ВыходToolStripMenuItem"
        Me.ВыходToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.ВыходToolStripMenuItem.Size = New System.Drawing.Size(314, 30)
        Me.ВыходToolStripMenuItem.Text = "Вихід"
        '
        'ПравкаToolStripMenuItem
        '
        Me.ПравкаToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ГородToolStripMenuItem, Me.МашиныToolStripMenuItem, Me.ДомаToolStripMenuItem})
        Me.ПравкаToolStripMenuItem.Enabled = False
        Me.ПравкаToolStripMenuItem.Name = "ПравкаToolStripMenuItem"
        Me.ПравкаToolStripMenuItem.Size = New System.Drawing.Size(126, 29)
        Me.ПравкаToolStripMenuItem.Text = "&Редагування"
        '
        'ГородToolStripMenuItem
        '
        Me.ГородToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.СоздатьСеткуКварталовToolStripMenuItem1, Me.ОчиститьToolStripMenuItem, Me.ВыровнятьПоСеткеToolStripMenuItem})
        Me.ГородToolStripMenuItem.Name = "ГородToolStripMenuItem"
        Me.ГородToolStripMenuItem.Size = New System.Drawing.Size(175, 30)
        Me.ГородToolStripMenuItem.Text = "Місто"
        '
        'СоздатьСеткуКварталовToolStripMenuItem1
        '
        Me.СоздатьСеткуКварталовToolStripMenuItem1.Name = "СоздатьСеткуКварталовToolStripMenuItem1"
        Me.СоздатьСеткуКварталовToolStripMenuItem1.Size = New System.Drawing.Size(286, 30)
        Me.СоздатьСеткуКварталовToolStripMenuItem1.Text = "Створити сітку кварталів"
        '
        'ОчиститьToolStripMenuItem
        '
        Me.ОчиститьToolStripMenuItem.Name = "ОчиститьToolStripMenuItem"
        Me.ОчиститьToolStripMenuItem.Size = New System.Drawing.Size(286, 30)
        Me.ОчиститьToolStripMenuItem.Text = "Очистити"
        '
        'ВыровнятьПоСеткеToolStripMenuItem
        '
        Me.ВыровнятьПоСеткеToolStripMenuItem.Name = "ВыровнятьПоСеткеToolStripMenuItem"
        Me.ВыровнятьПоСеткеToolStripMenuItem.Size = New System.Drawing.Size(286, 30)
        Me.ВыровнятьПоСеткеToolStripMenuItem.Text = "Вирівняти по сітці"
        '
        'МашиныToolStripMenuItem
        '
        Me.МашиныToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.УдалитьВсеМашиныToolStripMenuItem})
        Me.МашиныToolStripMenuItem.Name = "МашиныToolStripMenuItem"
        Me.МашиныToolStripMenuItem.Size = New System.Drawing.Size(175, 30)
        Me.МашиныToolStripMenuItem.Text = "Автомобілі"
        '
        'УдалитьВсеМашиныToolStripMenuItem
        '
        Me.УдалитьВсеМашиныToolStripMenuItem.Name = "УдалитьВсеМашиныToolStripMenuItem"
        Me.УдалитьВсеМашиныToolStripMenuItem.Size = New System.Drawing.Size(279, 30)
        Me.УдалитьВсеМашиныToolStripMenuItem.Text = "Видалити всі автомобілі"
        '
        'ДомаToolStripMenuItem
        '
        Me.ДомаToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ЗадатьСкоростьВыездаМашинToolStripMenuItem, Me.ЗадатьОдинЦветToolStripMenuItem, Me.ЗадатьСлучайныеЦветаToolStripMenuItem})
        Me.ДомаToolStripMenuItem.Name = "ДомаToolStripMenuItem"
        Me.ДомаToolStripMenuItem.Size = New System.Drawing.Size(175, 30)
        Me.ДомаToolStripMenuItem.Text = "Будинки"
        '
        'ЗадатьСкоростьВыездаМашинToolStripMenuItem
        '
        Me.ЗадатьСкоростьВыездаМашинToolStripMenuItem.Name = "ЗадатьСкоростьВыездаМашинToolStripMenuItem"
        Me.ЗадатьСкоростьВыездаМашинToolStripMenuItem.Size = New System.Drawing.Size(315, 30)
        Me.ЗадатьСкоростьВыездаМашинToolStripMenuItem.Text = "Задати кількість автомобілів"
        '
        'ЗадатьОдинЦветToolStripMenuItem
        '
        Me.ЗадатьОдинЦветToolStripMenuItem.Name = "ЗадатьОдинЦветToolStripMenuItem"
        Me.ЗадатьОдинЦветToolStripMenuItem.Size = New System.Drawing.Size(315, 30)
        Me.ЗадатьОдинЦветToolStripMenuItem.Text = "Задати один колір"
        '
        'ЗадатьСлучайныеЦветаToolStripMenuItem
        '
        Me.ЗадатьСлучайныеЦветаToolStripMenuItem.Name = "ЗадатьСлучайныеЦветаToolStripMenuItem"
        Me.ЗадатьСлучайныеЦветаToolStripMenuItem.Size = New System.Drawing.Size(315, 30)
        Me.ЗадатьСлучайныеЦветаToolStripMenuItem.Text = "Задати випадкові кольори"
        '
        'ОкнаToolStripMenuItem
        '
        Me.ОкнаToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ОкноПToolStripMenuItem, Me.ПоказуватиВідміткиToolStripMenuItem, Me.OptimizeViewToolStripMenuItem})
        Me.ОкнаToolStripMenuItem.Name = "ОкнаToolStripMenuItem"
        Me.ОкнаToolStripMenuItem.Size = New System.Drawing.Size(79, 29)
        Me.ОкнаToolStripMenuItem.Text = "Вигляд"
        '
        'ОкноПToolStripMenuItem
        '
        Me.ОкноПToolStripMenuItem.Checked = True
        Me.ОкноПToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ОкноПToolStripMenuItem.Image = CType(resources.GetObject("ОкноПToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ОкноПToolStripMenuItem.Name = "ОкноПToolStripMenuItem"
        Me.ОкноПToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.ОкноПToolStripMenuItem.Size = New System.Drawing.Size(304, 30)
        Me.ОкноПToolStripMenuItem.Text = "Властивості об'єкта"
        '
        'ПоказуватиВідміткиToolStripMenuItem
        '
        Me.ПоказуватиВідміткиToolStripMenuItem.Checked = True
        Me.ПоказуватиВідміткиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ПоказуватиВідміткиToolStripMenuItem.Enabled = False
        Me.ПоказуватиВідміткиToolStripMenuItem.Image = CType(resources.GetObject("ПоказуватиВідміткиToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ПоказуватиВідміткиToolStripMenuItem.Name = "ПоказуватиВідміткиToolStripMenuItem"
        Me.ПоказуватиВідміткиToolStripMenuItem.Size = New System.Drawing.Size(304, 30)
        Me.ПоказуватиВідміткиToolStripMenuItem.Text = "Показувати відмітки"
        '
        'OptimizeViewToolStripMenuItem
        '
        Me.OptimizeViewToolStripMenuItem.Name = "OptimizeViewToolStripMenuItem"
        Me.OptimizeViewToolStripMenuItem.Size = New System.Drawing.Size(304, 30)
        Me.OptimizeViewToolStripMenuItem.Text = "Оптимізація відображення"
        '
        'МоделированиеToolStripMenuItem
        '
        Me.МоделированиеToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.НачатьToolStripMenuItem, Me.ОстановитьToolStripMenuItem, Me.АвтомоделированиеToolStripMenuItem})
        Me.МоделированиеToolStripMenuItem.Name = "МоделированиеToolStripMenuItem"
        Me.МоделированиеToolStripMenuItem.Size = New System.Drawing.Size(142, 29)
        Me.МоделированиеToolStripMenuItem.Text = "Моделювання"
        '
        'НачатьToolStripMenuItem
        '
        Me.НачатьToolStripMenuItem.Enabled = False
        Me.НачатьToolStripMenuItem.Image = Global.VirtualCity.My.Resources.Resources.StartTool
        Me.НачатьToolStripMenuItem.Name = "НачатьToolStripMenuItem"
        Me.НачатьToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.НачатьToolStripMenuItem.Size = New System.Drawing.Size(355, 30)
        Me.НачатьToolStripMenuItem.Text = "Почати"
        '
        'ОстановитьToolStripMenuItem
        '
        Me.ОстановитьToolStripMenuItem.Enabled = False
        Me.ОстановитьToolStripMenuItem.Image = CType(resources.GetObject("ОстановитьToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ОстановитьToolStripMenuItem.Name = "ОстановитьToolStripMenuItem"
        Me.ОстановитьToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.ОстановитьToolStripMenuItem.Size = New System.Drawing.Size(355, 30)
        Me.ОстановитьToolStripMenuItem.Text = "Зупинити"
        '
        'АвтомоделированиеToolStripMenuItem
        '
        Me.АвтомоделированиеToolStripMenuItem.Enabled = False
        Me.АвтомоделированиеToolStripMenuItem.Image = CType(resources.GetObject("АвтомоделированиеToolStripMenuItem.Image"), System.Drawing.Image)
        Me.АвтомоделированиеToolStripMenuItem.Name = "АвтомоделированиеToolStripMenuItem"
        Me.АвтомоделированиеToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7
        Me.АвтомоделированиеToolStripMenuItem.Size = New System.Drawing.Size(355, 30)
        Me.АвтомоделированиеToolStripMenuItem.Text = "Автоматичне моделювання..."
        '
        'НалаштуванняToolStripMenuItem
        '
        Me.НалаштуванняToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.МоваToolStripMenuItem})
        Me.НалаштуванняToolStripMenuItem.Name = "НалаштуванняToolStripMenuItem"
        Me.НалаштуванняToolStripMenuItem.Size = New System.Drawing.Size(142, 29)
        Me.НалаштуванняToolStripMenuItem.Text = "Налаштування"
        '
        'МоваToolStripMenuItem
        '
        Me.МоваToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.УкраъToolStripMenuItem, Me.EnglishToolStripMenuItem})
        Me.МоваToolStripMenuItem.Name = "МоваToolStripMenuItem"
        Me.МоваToolStripMenuItem.Size = New System.Drawing.Size(152, 30)
        Me.МоваToolStripMenuItem.Text = "Мова"
        '
        'УкраъToolStripMenuItem
        '
        Me.УкраъToolStripMenuItem.Checked = True
        Me.УкраъToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.УкраъToolStripMenuItem.Name = "УкраъToolStripMenuItem"
        Me.УкраъToolStripMenuItem.Size = New System.Drawing.Size(172, 30)
        Me.УкраъToolStripMenuItem.Text = "Українська"
        '
        'СправкаToolStripMenuItem
        '
        Me.СправкаToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.РуководствоПользователяToolStripMenuItem, Me.ПроПрограмуToolStripMenuItem})
        Me.СправкаToolStripMenuItem.Name = "СправкаToolStripMenuItem"
        Me.СправкаToolStripMenuItem.Size = New System.Drawing.Size(89, 29)
        Me.СправкаToolStripMenuItem.Text = "Довідка"
        '
        'РуководствоПользователяToolStripMenuItem
        '
        Me.РуководствоПользователяToolStripMenuItem.Name = "РуководствоПользователяToolStripMenuItem"
        Me.РуководствоПользователяToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.РуководствоПользователяToolStripMenuItem.Size = New System.Drawing.Size(298, 30)
        Me.РуководствоПользователяToolStripMenuItem.Text = "Посібник користувача"
        '
        'ПроПрограмуToolStripMenuItem
        '
        Me.ПроПрограмуToolStripMenuItem.Name = "ПроПрограмуToolStripMenuItem"
        Me.ПроПрограмуToolStripMenuItem.Size = New System.Drawing.Size(298, 30)
        Me.ПроПрограмуToolStripMenuItem.Text = "Про програму..."
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'toolStripSeparator7
        '
        Me.toolStripSeparator7.Name = "toolStripSeparator7"
        Me.toolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.СоздатьToolStripButton, Me.ОткрытьToolStripButton, Me.СохранитьToolStripButton, Me.ToolStripExportImage, Me.ToolStripSeparator6, Me.ToolStripStart, Me.ToolStripStop, Me.ToolStripButtonAutoModel, Me.ToolStripSeparator8, Me.ToolStripTrammel, Me.ToolStripAlert, Me.toolStripSeparator7, Me.ToolStripHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 35)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(670, 25)
        Me.ToolStrip1.TabIndex = 16
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'СоздатьToolStripButton
        '
        Me.СоздатьToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.СоздатьToolStripButton.Image = CType(resources.GetObject("СоздатьToolStripButton.Image"), System.Drawing.Image)
        Me.СоздатьToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.СоздатьToolStripButton.Name = "СоздатьToolStripButton"
        Me.СоздатьToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.СоздатьToolStripButton.Text = "&Створити"
        '
        'ОткрытьToolStripButton
        '
        Me.ОткрытьToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ОткрытьToolStripButton.Image = CType(resources.GetObject("ОткрытьToolStripButton.Image"), System.Drawing.Image)
        Me.ОткрытьToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ОткрытьToolStripButton.Name = "ОткрытьToolStripButton"
        Me.ОткрытьToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.ОткрытьToolStripButton.Text = "&Відкрити"
        '
        'СохранитьToolStripButton
        '
        Me.СохранитьToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.СохранитьToolStripButton.Enabled = False
        Me.СохранитьToolStripButton.Image = CType(resources.GetObject("СохранитьToolStripButton.Image"), System.Drawing.Image)
        Me.СохранитьToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.СохранитьToolStripButton.Name = "СохранитьToolStripButton"
        Me.СохранитьToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.СохранитьToolStripButton.Text = "&Зберегти"
        '
        'ToolStripExportImage
        '
        Me.ToolStripExportImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripExportImage.Image = CType(resources.GetObject("ToolStripExportImage.Image"), System.Drawing.Image)
        Me.ToolStripExportImage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripExportImage.Name = "ToolStripExportImage"
        Me.ToolStripExportImage.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripExportImage.Text = "Експорт зображення"
        '
        'ToolStripStart
        '
        Me.ToolStripStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripStart.Enabled = False
        Me.ToolStripStart.Image = CType(resources.GetObject("ToolStripStart.Image"), System.Drawing.Image)
        Me.ToolStripStart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripStart.Name = "ToolStripStart"
        Me.ToolStripStart.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripStart.Text = "Почати моделювання"
        '
        'ToolStripStop
        '
        Me.ToolStripStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripStop.Enabled = False
        Me.ToolStripStop.Image = CType(resources.GetObject("ToolStripStop.Image"), System.Drawing.Image)
        Me.ToolStripStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripStop.Name = "ToolStripStop"
        Me.ToolStripStop.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripStop.Text = "Зупинити моделювання"
        '
        'ToolStripButtonAutoModel
        '
        Me.ToolStripButtonAutoModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonAutoModel.Enabled = False
        Me.ToolStripButtonAutoModel.Image = CType(resources.GetObject("ToolStripButtonAutoModel.Image"), System.Drawing.Image)
        Me.ToolStripButtonAutoModel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonAutoModel.Name = "ToolStripButtonAutoModel"
        Me.ToolStripButtonAutoModel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonAutoModel.Text = "Автоматичне моделювання"
        '
        'ToolStripTrammel
        '
        Me.ToolStripTrammel.Checked = True
        Me.ToolStripTrammel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripTrammel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripTrammel.Image = CType(resources.GetObject("ToolStripTrammel.Image"), System.Drawing.Image)
        Me.ToolStripTrammel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripTrammel.Name = "ToolStripTrammel"
        Me.ToolStripTrammel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripTrammel.Text = "Властивості об'єкта"
        '
        'ToolStripAlert
        '
        Me.ToolStripAlert.Checked = True
        Me.ToolStripAlert.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripAlert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripAlert.Enabled = False
        Me.ToolStripAlert.Image = CType(resources.GetObject("ToolStripAlert.Image"), System.Drawing.Image)
        Me.ToolStripAlert.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripAlert.Name = "ToolStripAlert"
        Me.ToolStripAlert.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripAlert.Text = "Відмтіки"
        '
        'ToolStripHelp
        '
        Me.ToolStripHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripHelp.Image = CType(resources.GetObject("ToolStripHelp.Image"), System.Drawing.Image)
        Me.ToolStripHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripHelp.Name = "ToolStripHelp"
        Me.ToolStripHelp.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripHelp.Text = "Посібник користувача"
        '
        'PanelControl
        '
        Me.PanelControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelControl.Controls.Add(Me.LabelTime)
        Me.PanelControl.Controls.Add(Me.Label3)
        Me.PanelControl.Controls.Add(Me.Label5)
        Me.PanelControl.Controls.Add(Me.TrackBar2)
        Me.PanelControl.Controls.Add(Me.TrackBar1)
        Me.PanelControl.Controls.Add(Me.Label2)
        Me.PanelControl.Controls.Add(Me.Label1)
        Me.PanelControl.Controls.Add(Me.Label4)
        Me.PanelControl.Enabled = False
        Me.PanelControl.Location = New System.Drawing.Point(18, 80)
        Me.PanelControl.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PanelControl.Name = "PanelControl"
        Me.PanelControl.Size = New System.Drawing.Size(634, 77)
        Me.PanelControl.TabIndex = 17
        '
        'TimerRefresh
        '
        Me.TimerRefresh.Enabled = True
        Me.TimerRefresh.Interval = 1000
        '
        'EnglishToolStripMenuItem
        '
        Me.EnglishToolStripMenuItem.Name = "EnglishToolStripMenuItem"
        Me.EnglishToolStripMenuItem.Size = New System.Drawing.Size(172, 30)
        Me.EnglishToolStripMenuItem.Text = "English"
        '
        'Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(670, 465)
        Me.Controls.Add(Me.PanelControl)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.ContainerPanel)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MinimumSize = New System.Drawing.Size(559, 493)
        Me.Name = "Editor"
        Me.Text = "{Title}"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ContainerPanel.ResumeLayout(False)
        Me.CityContainer.ResumeLayout(False)
        CType(Me.CityBoxDouble, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CityBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.PanelControl.ResumeLayout(False)
        Me.PanelControl.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CityBox As System.Windows.Forms.PictureBox
    Friend WithEvents TimerDraw As System.Windows.Forms.Timer
    Friend WithEvents ContainerPanel As System.Windows.Forms.Panel
    Friend WithEvents ModellingTimer As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TrackBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ФайлToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents СоздатьToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОткрытьToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents СохранитьToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents СохранитькакToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ВыходToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents СправкаToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ЭкспортИзображенияToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОкнаToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОкноПToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents РуководствоПользователяToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ПравкаToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelTime As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents МоделированиеToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents НачатьToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОстановитьToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents АвтомоделированиеToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents СоздатьToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ОткрытьToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents СохранитьToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripExportImage As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripStart As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripStop As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonAutoModel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTrammel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripAlert As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ДомаToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ЗадатьСкоростьВыездаМашинToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents МашиныToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents УдалитьВсеМашиныToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ГородToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents СоздатьСеткуКварталовToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОчиститьToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ВыровнятьПоСеткеToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ЗадатьОдинЦветToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ЗадатьСлучайныеЦветаToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelControl As System.Windows.Forms.Panel
    Friend WithEvents ПроПрограмуToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ПоказуватиВідміткиToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerRefresh As System.Windows.Forms.Timer
    Friend WithEvents CityContainer As System.Windows.Forms.Panel
    Friend WithEvents CityBoxDouble As System.Windows.Forms.PictureBox
    Friend WithEvents OptimizeViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents НалаштуванняToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents МоваToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents УкраъToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnglishToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
