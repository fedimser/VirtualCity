Imports System.Drawing.Drawing2D
Imports VirtualCity.Geometry
Imports VirtualCity.Additional


'Этот класс представляет здание (дом) как объект в городе, из которого могут выезжать и в который могут въезжать машины

Public Class Building
    Public Exists As Boolean                'Существует ли дом
    Public points() As PointF               'Многоугольник, задающий дом (не используется, зарезервировано)
    Public Rect As Rectangle                'Прямоугольник (или эллипс), задающий дом
    Private clr As Color                    'Цвет дома
    Private brush As Brush                  'Кисть для прорисовки дома
    Public BType As BuildingType            'Форма дома (Прямоугольник/Эллипс/Многоугольник)

    Public Name As String                   'Название дома
    Public Number As Integer                'Номер дома - для связи с родительским объектом Город


    'Переменные, отвечающие за представление дома как цели для машин
    Public Targ_Road As Integer             'Дорога,
    Public Targ_Line As Integer             'полоса,
    Public Targ_PosAtLine As Single         'и координата точки на какой-то дороге, с которой машина съезжает в дом
    Public Targ_InPoint As PointF           'Точка внутри дома, где машины появляются и исчезают
    Public Targ_CanBe As Boolean = True     'Может ли этот дом быть целью

    Public IncomKoef As Single = 1              'Коэффициент, характеризующий вероятность въезда
    Public GR_IsCarGoingOut As Boolean = False  'Выезжает ли сейчас из дома машина?
    Public CarsNum As Integer                   'Число машин внутри дома
    Public MaxCarsNum As Integer                'Максимально возможное число машин внутри дома (вместимость)
    Public CarsGoingIn As Integer = 0           'Число машин, движущихся к дому.


    Public Const MaxOutPossibility As Single = 0.2   'Вероятность выезда машины в течение произвольно взятой секунды, когда дом полностью заполнен
    Public Const MaxMaxCarNum As Integer = 100        'Максимально возможная вместимость
      

    'Перечисление для поля BType
    Public Enum BuildingType
        Round
        Polygon
        Rectangle
    End Enum

    'Конструктор по умолчанию 
    Public Sub New()
        Exists = False
    End Sub

    'Конструктор по прямоугольнику
    Public Sub New(ByVal Rct As Rectangle)
        Exists = True
        BType = BuildingType.Rectangle
        Rect = Rct
        clr = Color.FromArgb(255, 214, 147, 92)
        brush = New SolidBrush(clr)
    End Sub

#Region "Building.FileIO"
    'Текстовое представление дома (для записи в файл)
    Public Overrides Function ToString() As String
        Dim ret As String = Me.Name & "|" & Me.Color.R & "|" & Me.Color.G & "|" & Me.Color.B
        Select Case Me.BType
            Case BuildingType.Polygon
                ret &= "|0|" & Me.points.Length
                For i = 0 To Me.points.Length - 1
                    ret &= "|" & CInt(Me.points(i).X) & "|" & CInt(Me.points(i).Y)
                Next
            Case BuildingType.Rectangle
                ret &= "|1|" & Me.Rect.Left & "|" & Me.Rect.Top & "|" & Me.Rect.Width & "|" & Me.Rect.Height
            Case BuildingType.Round
                ret &= "|2|" & Me.Rect.Left & "|" & Me.Rect.Top & "|" & Me.Rect.Width & "|" & Me.Rect.Height
        End Select
        ret &= "|" & Me.IncomKoef & "|" & Me.CarsNum & "|" & Me.MaxCarsNum

        Return ret
    End Function

    'Конструктор по текстовому предсталению (для чтения из файла)
    Public Sub New(ByVal StringPresentation As String)
        Dim s() As String = StringPresentation.Split("|")
        Me.Name = s(0)
        Me.Color = Color.FromArgb(255, CInt(s(1)), CInt(s(2)), CInt(s(3)))
        Dim type As Integer = CInt(s(4))
        Dim count As Integer
        Select Case type
            Case 0
                Me.BType = BuildingType.Polygon
                Dim n As Integer = CInt(s(5))
                ReDim Me.points(n - 1)
                For i = 0 To n - 1
                    Me.points(i) = New PointF(CInt(s(6 + 2 * i)), CInt(s(7 + 2 * i)))
                Next
                count = 6 + 2 * n
            Case 1
                Me.BType = BuildingType.Rectangle
                Me.Rect = New Rectangle(CInt(s(5)), CInt(s(6)), CInt(s(7)), CInt(s(8)))
                count = 9
            Case 2
                Me.BType = BuildingType.Round
                Me.Rect = New Rectangle(CInt(s(5)), CInt(s(6)), CInt(s(7)), CInt(s(8)))
                count = 9
        End Select

        Me.IncomKoef = s(count)
        Me.CarsNum = s(count + 1)
        Me.MaxCarsNum = s(count + 2)
        Me.brush = New SolidBrush(Me.Color)
        Me.Exists = True
    End Sub
#End Region

#Region "Graphics"
    'Рисует дом, если Selected==Истина, обводит
    Public Sub Draw(ByRef gr As Graphics, ByVal Selected As Boolean, ByVal Mark As Boolean)
        Dim SelPen As New Pen(Color.Red, 2)
        Select Case BType
            Case BuildingType.Polygon
                gr.FillPolygon(brush, points)
                If Selected Then gr.DrawPolygon(SelPen, points)
            Case BuildingType.Round
                gr.FillEllipse(brush, Rect)
                If Selected Then gr.DrawEllipse(SelPen, Rect)
            Case BuildingType.Rectangle
                gr.FillRectangle(brush, Rect)
                If Selected Then gr.DrawRectangle(SelPen, Rect)
                If Mark Then
                    Dim MarkRect1 As New Rectangle(Rect.X + Rect.Width / 2 - 31, Rect.Y + Rect.Height / 2 - 15, 30, 30)
                    gr.FillEllipse(DescrBrush(CarsNum / MaxCarsNum), MarkRect1)
                    Dim MarkRect2 As New Rectangle(Rect.X + Rect.Width / 2 + 1, Rect.Y + Rect.Height / 2 - 15, 30, 30)
                    gr.FillRectangle(DescrBrush(IncomKoef / 20), MarkRect2)
                End If

        End Select
    End Sub


    Public Function GetMiddle() As Point
        Return New Point(Rect.Left + Rect.Width / 2, Rect.Top + Rect.Height / 2)
    End Function 

        'Возвращает путь, задающий периметр дома
    Private Function GetPath() As GraphicsPath
        Dim pth As New GraphicsPath
        Select Case BType
            Case BuildingType.Polygon
                pth.AddPolygon(points)
            Case BuildingType.Round
                pth.AddEllipse(Rect)
            Case BuildingType.Rectangle
                pth.AddRectangle(Rect)
        End Select
        Return pth
    End Function

    'Проверяет, находится ли точка внутри дома
    Public Function CheckPointInside(ByVal pt As PointF) As Boolean
        Return GetPath.IsVisible(pt)
    End Function

    'Проверяет, пересекается ли дом с многоугольником plg
    Public Function CheckForCrossing(ByVal plg As Point(), ByVal gr As Graphics)
        Dim rg As New Region(GetPath)
        Dim pth As New GraphicsPath
        pth.AddPolygon(plg)
        rg.Intersect(pth)
        Return Not rg.IsEmpty(gr)
    End Function

    'Цвет дома
    Public Property Color As Color
        Get
            Return clr
        End Get
        Set(ByVal value As Color)
            clr = value
            brush = New SolidBrush(value)
        End Set
    End Property


#End Region


    Public Function DetermineTargetParam(ByRef CT As City) As Boolean     'Определяет, к какой точке должна ехать машина, чтобы попасть в этот дом (Targ_Road, Targ_Line, Targ_PosAtLine)
        Dim ret As Boolean = False
        Dim vec As New Geometry.Vector
        Dim d1, d2 As Single
        Dim min_dist As Single = 0

        Me.DetermineInPoint()
        For i = 1 To CT.RoadCounter
            If CT.Roads(i).Exists Then
                vec = CT.Roads(i).GetLineCoord(CT.Roads(i).n1, CT)
                vec.GetDistFromPoint(Me.Targ_InPoint, d1, d2)
                If (Not ret) Or (d1 < min_dist) Then
                    ret = True
                    min_dist = d1
                    Me.Targ_Road = i
                    Me.Targ_Line = CT.Roads(i).n1
                    Me.Targ_PosAtLine = d2
                End If


                vec = CT.Roads(i).GetLineCoord(-CT.Roads(i).n2, CT)
                vec.GetDistFromPoint(Me.Targ_InPoint, d1, d2) 
                    If (Not ret) Or (d1 < min_dist) Then
                        ret = True
                        min_dist = d1
                        Me.Targ_Road = i
                        Me.Targ_Line = -CT.Roads(i).n2
                    Me.Targ_PosAtLine = d2
                End If
            End If
        Next


        Return ret
    End Function

    'Определяет InPoint
    Private Sub DetermineInPoint()
        Me.Targ_InPoint = New PointF(Me.Rect.Left + Me.Rect.Width / 2, Me.Rect.Top + Me.Rect.Height / 2)
    End Sub

    'Создаёт машину, выезжающую из себя
    Public Sub CreateCar(ByRef CT As City)
        If GR_IsCarGoingOut Then Exit Sub
        If Me.CarsNum = 0 Then Exit Sub

        'Check if Road is free. If it isn't, dont create car
        For i = 1 To CT.CarsCounter
            If CT.Cars(i).Exists Then
                If CT.Cars(i).Road = Targ_Road And CT.Cars(i).Line = Targ_Line Then
                    If CT.Cars(i).PosAtLine >= Targ_PosAtLine And CT.Cars(i).PosAtLine - CT.Cars(i).Length <= Targ_PosAtLine Then
                        Exit Sub
                    End If
                End If
            End If
        Next

        Dim num As Integer = 0
        Do
            num += 1
        Loop Until CT.Cars(num).Exists = False
        If num > CT.CarsCounter Then CT.CarsCounter = num

        CT.Cars(num) = New Car
        CT.Cars(num).Exists = True

        CT.Cars(num).Road = Targ_Road
        CT.Cars(num).Line = Targ_Line
        CT.Cars(num).PosAtLine = Targ_PosAtLine

        CT.Cars(num).Pos = Targ_InPoint
        Dim EP As PointF = CT.Roads(Targ_Road).GetPoint(Targ_Line, Targ_PosAtLine, CT)
        CT.Cars(num).GR_Len = Dist(Targ_InPoint, EP)
        CT.Cars(num).GR_Pos = 0
        CT.Cars(num).GR_House = Me.Number
        CT.Cars(num).MovRot = AngleBy2Pt(Targ_InPoint, EP)
        CT.Cars(num).TrueRot = CT.Cars(num).MovRot

        CT.Cars(num).GoToRoad = True

        CT.Cars(num).Number = num
        CT.Cars(num).Name = "Машина " & num
        CT.Cars(num).SetRandomSpeed()
        CT.Cars(num).SetRandomColor(CT)

        CT.Cars(num).SetRandomTarget(CT)
        CT.Cars(num).DetermineRoute(CT)

        Me.CarsNum -= 1

        GR_IsCarGoingOut = True

        Analyse_OutComeCounter += 1
        CT.Roads(Targ_Road).Analyse_PassCarsCounter += 1
    End Sub



    'Выровнять по сетке
    Public Sub AllignToGrid(ByVal GridStep As Integer)
        Dim NewLeft As Integer = GridStep * ((Me.Rect.Left + GridStep \ 2) \ GridStep)
        Dim NewTop As Integer = GridStep * ((Me.Rect.Top + GridStep \ 2) \ GridStep)
        Dim NewRight As Integer = GridStep * ((Me.Rect.Right + GridStep \ 2) \ GridStep)
        Dim NewBottom As Integer = GridStep * ((Me.Rect.Bottom + GridStep \ 2) \ GridStep)
        If NewRight <= NewLeft Then NewRight += GridStep
        If NewBottom <= NewTop Then NewBottom += GridStep
        Me.Rect = New Rectangle(NewLeft, NewTop, NewRight - NewLeft, NewBottom - NewTop)
    End Sub


#Region "Analysis"

    'Переменные-счётчики для анализа
    Public Analyse_InComeCounter As Integer    'Сколько машин въехало
    Public Analyse_OutComeCounter As Integer   'Сколько машин выехало

    Public Sub ResetCounters()
        Analyse_InComeCounter = 0
        Analyse_OutComeCounter = 0
    End Sub
#End Region
End Class
