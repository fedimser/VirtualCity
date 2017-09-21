Imports System.Math
Imports System.Drawing.Drawing2D
Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports VirtualCity.Geometry

'Этот класс прдставляет город как набор перекрёстков, дорог, машин и зданий, привязанных к геометрическим координатам

Public Class City

#Region "Declarations"


    'Содержимое города
    Const ArraysSize% = 1000                    'Максимальное число объектов каждого рода  
    Public IntScs(ArraysSize) As Intersection   'Перекрёстки
    Public ISCounter As Integer = 0             'Счётчик перекрёстков (максимальный номер существующего перекрёстка)
    Public Roads(ArraysSize) As Road            'Дороги
    Public RoadCounter As Integer = 0           'Счётчик дорог
    Public Cars(ArraysSize) As Car              'Машины
    Public CarsCounter As Integer = 0           'Счётчик машин
    Public Houses(ArraysSize) As Building       'Дома
    Public HsCounter As Integer = 0             'Счётчик домов

    'Представление
    Public Name As String                       'Название города
    Public BgColor As Color = Color.White       'Цвет фона  
    Public Width As Integer                     'Ширина (пикс)
    Public Height As Integer                    'Высота (пикс)
    Public ShowAnalyse As Boolean = False       'Показывать ли аналитическаие метки
    '' Public AreaToDraw As Rectangle              'Отрисовываемый участок (в координатах города)
    Private DrawBorder As Boolean = True

    'Modelling declarations
    Public Time As Single = 0                   'Время от полуночи в секундах - зарезервировано
    Public IsModelling As Boolean               'Происходит ли сейчас моделирование

    'Константы
    Public Const RoadWidth As Integer = 10          'Ширина полосы всех дорог
    Public Const IntersectionZone As Integer = 10   'Зона перекрёстка всех дорог (в перспективе для вычисляется для каждой дороги отдельно)
    Public Const SafeDist As Single = 8             'Безопасная дистанция
    Public Const ScaleReal As Single = 0.25         'Число метров в 1 еденице длины горда




    'Будут ли водители при просчёте маршрута учитывать пробки
    Public CleverDrivers As Boolean = True

    'Конструктор по умолчанию
    Public Sub New()
        Me.Width = 1000
        Me.height = 1000

        For i = 1 To ArraysSize
            IntScs(i) = New Intersection()
            Roads(i) = New Road()
            Cars(i) = New Car()
            Houses(i) = New Building()
        Next
    End Sub

    'Конструктор по размерам
    Public Sub New(ByVal x As Integer, ByVal y As Integer)
        Me.Width = x
        Me.height = y

        For i = 1 To ArraysSize
            IntScs(i) = New Intersection()
            Roads(i) = New Road()
            Cars(i) = New Car()
            Houses(i) = New Building()
        Next

        Me.Name = "Нове місто"
    End Sub



#End Region


#Region "General"
    Public Sub Init()
        FindBridges()
    End Sub
#End Region

#Region "Editing"


    Public Enum ObjectType
        None
        Intersection
        Road
        Car
        Building
    End Enum

    Const Sensivity As Integer = 8  'Максимальное расстояние от курсора до точечного объекта (напр. перекрёстка), при котором будет выделяться этот объект
    Public Cursor As PointF         'Где находится курсор



    Public Function CreateGrid(ByVal QarterLength As Integer) As Boolean
        If QarterLength < 50 Then Return False
        Dim xNum As Integer = (Me.Width - 100) \ QarterLength
        If 2 * xNum * (xNum + 1) > ArraysSize Then Return (False)

        Me.ClearAll()

        For i = 0 To xNum
            For j = 0 To xNum
                Dim num As Integer = j * (xNum + 1) + i + 1
                IntScs(num) = New Intersection(50 + i * QarterLength, 50 + j * QarterLength)
                IntScs(num).Name = "Перехрестя " & num
                IntScs(num).Number = num
            Next
        Next
        ISCounter = (xNum + 1) * (xNum + 1)

        RoadCounter = 0
        For i = 0 To xNum
            For j = 0 To xNum - 1
                RoadCounter += 1 
                Roads(RoadCounter) = New Road(j * (xNum + 1) + i + 1, (j + 1) * (xNum + 1) + i + 1)
                Roads(RoadCounter).Name = "Дорога " & RoadCounter
                Roads(RoadCounter).DetermineGeom(Me)
            Next
        Next
        For j = 0 To xNum
            For i = 0 To xNum - 1
                RoadCounter += 1 
                Roads(RoadCounter) = New Road(j * (xNum + 1) + i + 1, j * (xNum + 1) + i + 2)
                Roads(RoadCounter).Name = "Дорога " & RoadCounter
                Roads(RoadCounter).DetermineGeom(Me)
            Next
        Next

        Return True
    End Function

    'Clear City
    Public Sub ClearAll()
        For i = 1 To CarsCounter
            Cars(i).Exists = False
        Next
        CarsCounter = 0
        For i = 1 To RoadCounter
            Roads(i).Exists = False
        Next
        RoadCounter = 0
        For i = 1 To ISCounter
            IntScs(i).Exists = False
        Next
        ISCounter = 0
        For i = 1 To HsCounter
            Houses(i).Exists = False
        Next
        HsCounter = 0
    End Sub

    'Удаляет все машины
    Public Sub RemoveAllCars()
        For i = 1 To CarsCounter
            Cars(i).Exists = False
        Next
        CarsCounter = 0
        For i = 1 To ISCounter
            If IntScs(i).Exists Then
                IntScs(i).WaitingCars.Clear()
                IntScs(i).MovingCars.Clear()
            End If
        Next
    End Sub

#Region "Intersections"

    'Добавление нового перекрёстка
    Public Sub AddIntersection(ByVal x As Integer, ByVal y As Integer)
        Dim j% = 0
        For i = 1 To ISCounter
            If Not IntScs(i).Exists Then j = i
        Next
        If j = 0 Then
            ISCounter += 1
            j = ISCounter
        End If
        IntScs(j) = New Intersection(x, y)
        IntScs(j).Name = "Перехрестя " & j
        IntScs(j).Number = j
    End Sub

    'Изменение координат перекрёстка
    Public Sub MoveIntersection(ByVal num%, ByVal x%, ByVal y%)
        IntScs(num).x = x
        IntScs(num).y = y
        For i = 1 To RoadCounter
            If Roads(i).Exists And (Roads(i).st = num Or Roads(i).en = num) Then Roads(i).DetermineGeom(Me)
        Next
    End Sub

    'Проверяет, не пересекаются ли дорги после того, как сдвинули перекрёсток. В дальнейшем скорее всего будет убрано
    Public Function CheckForCrossingAfterMoveIntersection(ByVal num%)
        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                If Roads(i).st = num Or Roads(i).en = num Then
                    For j = 1 To RoadCounter
                        If Not (j = i) And Roads(j).Exists Then
                            If Geometry.IsCrossing(IntScs(Roads(i).st).GetPoint, IntScs(Roads(i).en).GetPoint, IntScs(Roads(j).st).GetPoint, IntScs(Roads(j).en).GetPoint) Then Return True
                        End If
                    Next
                    'Checking if it crosses other building
                    Dim CrossingWithBuilding As Boolean = False
                    Dim gr As Graphics = Graphics.FromImage(New Bitmap(Me.Width, Me.height))
                    Dim plg As Point() = Roads(i).GetPolygon(Me)
                    For j = 1 To HsCounter
                        If Houses(j).Exists Then
                            If Houses(j).CheckForCrossing(plg, gr) Then CrossingWithBuilding = True
                            If CrossingWithBuilding Then Exit For
                        End If
                    Next
                    If CrossingWithBuilding Then Return True
                End If
            End If
        Next
        Return False
    End Function

    'Определяет, есть ли рядом с данной точко перекрёсток, и какой
    Public Function GetIntersection(ByVal pt As PointF, ByRef n As Integer) As Boolean
        For i = 1 To ISCounter
            If IntScs(i).Exists Then
                If Sqrt((IntScs(i).x - pt.X) ^ 2 + (IntScs(i).y - pt.Y) ^ 2) < Sensivity Then
                    n = i
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    'Удаляет перекрёсток
    Public Sub RemoveIntersection(ByVal num As Integer)
        IntScs(num).Exists = False
        For i = 1 To RoadCounter
            If Roads(i).st = num Or Roads(i).en = num Then Roads(i).Exists = False
        Next
    End Sub

    'Определяет, сколько дорог сходится в  перекрёстке
    Public Function GetRoadsQuantity(ByVal num As Integer)
        Dim ret As Integer = 0
        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                If Roads(i).st = num Or Roads(i).en = num Then ret += 1
            End If
        Next
        Return ret
    End Function
#End Region

#Region "Roads"
    Public AddingRoad As Integer = 0    'Один из перекрёстков дороги, которую сейчас добавляем, или 0, если не добавляем

    'Начало добавления дороги
    Public Sub StartAddRoad(ByVal st_isc As Integer)
        If IntScs(st_isc).Exists Then
            AddingRoad = st_isc
        End If
    End Sub

    'Конец добавления дороги
    Public Sub EndAddRoad(ByVal en_isc As Integer)
        If en_isc = AddingRoad Then
            Exit Sub
        End If
        Dim Crossing As Boolean = False
        Dim ExistsYet As Boolean = False
        Dim CrossingWithBuilding As Boolean = False
        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                If ((Roads(i).st = AddingRoad) And (Roads(i).en = en_isc)) Or ((Roads(i).en = AddingRoad) And (Roads(i).st = en_isc)) Then ExistsYet = True
                If Geometry.IsCrossing(IntScs(Roads(i).st).GetPoint, IntScs(Roads(i).en).GetPoint, IntScs(AddingRoad).GetPoint, IntScs(en_isc).GetPoint) Then Crossing = True
                If Crossing Or ExistsYet Then Exit For
            End If
        Next

        Dim ToAdd As Road = New Road(AddingRoad, en_isc)
        Dim plg As Point() = ToAdd.GetPolygon(Me)
        Dim gr As Graphics = Graphics.FromImage(New Bitmap(Me.Width, Me.height))


        For i = 1 To HsCounter
            If Houses(i).Exists Then
                If Houses(i).CheckForCrossing(plg, gr) Then CrossingWithBuilding = True
                If CrossingWithBuilding Then Exit For
            End If
        Next

        If Crossing Then
            'Throw New Exception("Roads cannot cross")
            Exit Sub
        End If

        If ExistsYet Then
            'Throw New Exception("This road is exist")
            Exit Sub
        End If

        If CrossingWithBuilding Then
            'Throw New Exception("Road cannot cross buildings")
            Exit Sub
        End If


        Dim j% = 0
        For i = 1 To RoadCounter
            If Not Roads(i).Exists Then j = i
        Next
        If j = 0 Then
            RoadCounter += 1
            j = RoadCounter
        End If
        Roads(j) = ToAdd
        Roads(j).Name = "Дорога " & j

        AddingRoad = 0

        RefreshRoads()
        RefreshBuildingsTargs()
        RecountRoutes()
        FindBridges()
    End Sub

    'Отмена добавдления дороги
    Public Sub CancelAddingRoad()
        AddingRoad = 0
    End Sub

    'Удаление дороги и всех машин на ней
    Public Sub RemoveRoad(ByVal num As Integer)
        Roads(num).Exists = False
        For i = 1 To CarsCounter
            If Cars(i).Exists Then
                If Cars(i).Road = num Then
                    RemoveCar(i)
                End If
            End If
        Next
        RefreshBuildingsTargs()
        FindBridges()
    End Sub

    'Обновить вычисляемые поля дорог
    Private Sub RefreshRoads()
        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                Roads(i).DetermineGeom(Me)
            End If
        Next
    End Sub
#End Region

#Region "Cars"

    'Добавляет новую машину как можно ближе к точке _pos, если эта точка попадает на какую-нибудь дорогу, и затем вычисляет все необходимые параметры
    Public Function AddCar(ByVal _pos As PointF) As Boolean

        Dim rd As Integer = 0
        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                If Roads(i).IsPointInside(_pos, Me) Then
                    rd = i
                    Exit For
                End If
            End If
        Next
        If rd = 0 Then Return False
        Dim a0 As Single = AngleBy2Pt(IntScs(Roads(rd).st).GetPoint, _pos)
        Dim d0 As Single = Dist(IntScs(Roads(rd).st).GetPoint, _pos)
        Dim a1 As Single = Roads(rd).Rotat - a0
        Dim d1 As Single = d0 * Cos(a1)
        Dim d2 As Single = -d0 * Sin(a1)

        Dim ln As Integer
        If d2 > 0 Then
            ln = -Int(d2 / 10) - 1
        ElseIf d2 < 0 Then
            ln = Int(-d2 / 10) + 1
        End If

        Dim ps As Integer
        If ln > 0 Then
            ps = Roads(rd).Length - d1
        ElseIf ln < 0 Then
            ps = d1
        End If


        Dim num As Integer = 0
        Do
            num += 1
        Loop Until Cars(num).Exists = False
        If num > CarsCounter Then CarsCounter = num

        Cars(num) = New Car
        Cars(num).Exists = True

        Cars(num).Road = rd
        Cars(num).Line = ln
        Cars(num).PosAtLine = ps

        Cars(num).Pos = Cars(num).GetPos(Me)
        Cars(num).MovRot = Roads(rd).Rotat
        If ln > 0 Then Cars(num).MovRot += PI
        Cars(num).TrueRot = Cars(num).MovRot

        Cars(num).AtRoad = True
        Cars(num).Number = num
        Cars(num).Name = "Автомобіль " & num
        Cars(num).SetRandomSpeed()

        Cars(num).SetRandomTarget(Me)
        Cars(num).DetermineRoute(Me)

        Return True
    End Function

    'Удаляет машину
    Public Sub RemoveCar(ByVal num As Integer)
        Cars(num).Exists = False

        If Cars(num).IST_NewRoad > 0 Then
            IntScs(Roads(Cars(num).IST_NewRoad).st).RmoveCar(num)
            IntScs(Roads(Cars(num).IST_NewRoad).en).RmoveCar(num)
        End If
    End Sub

    'Пересчитывает все маршруты
    Private Sub RecountRoutes()
        For i = 1 To CarsCounter
            If Cars(i).Exists Then
                Cars(i).DetermineRoute(Me)
            End If
        Next
    End Sub
#End Region

#Region "Buildings"
    Dim AddingBuilding As Boolean = False   'Добавляем ли сейчас здание?
    Dim BuildingFirstPos As PointF          'Одна из точек добавляемого здания

    'Начало добавления здания (по первому нажатию мышки)
    Public Sub BeginAddBuilding()
        AddingBuilding = True
        BuildingFirstPos = Cursor
    End Sub

    'Отмена добавления здания
    Public Sub CancelAddBuilding()
        AddingBuilding = False
    End Sub

    'Конец добавления здания (по Второму нажатию мышки)
    Public Sub EndAddBuilding()
        AddingBuilding = False
        AddBuilding(New Building(Geometry.GetRect(Cursor, BuildingFirstPos)))
    End Sub

    'Добавляет здание
    Private Sub AddBuilding(ByVal bld As Building)
        Dim j% = 0
        For i = 1 To HsCounter
            If Not Houses(i).Exists Then j = i
        Next
        If j = 0 Then
            HsCounter += 1
            j = HsCounter
        End If
        Houses(j) = bld
        Houses(j).Number = j
        Houses(j).Name = "Будинок " & j
        Houses(j).DetermineTargetParam(Me)
    End Sub

    'Удаляет здание
    Public Sub RemoveBuilding(ByVal num As Integer)
        Houses(num).Exists = False
    End Sub

    'Пересчитывает точку-цель для всех зданий
    Public Sub RefreshBuildingsTargs()
        For i = 1 To HsCounter
            If Houses(i).Exists Then
                Houses(i).DetermineTargetParam(Me)
            End If
        Next
    End Sub

    'Определяет, находится ли курсор над границей выделенного дома. Если да, то над какой
    Public Function CanEditHouse() As Integer
        Dim sens As Integer = 10

        If SelObj = ObjectType.Building Then
            If Houses(SelNum).BType = Building.BuildingType.Rectangle Then
                Dim rct As Rectangle = Houses(SelNum).Rect
                If Cursor.X > rct.Left And Cursor.X < rct.Left + sens And Cursor.Y > rct.Top And Cursor.Y < rct.Bottom Then Return 1
                If Cursor.X > rct.Left And Cursor.X < rct.Right And Cursor.Y > rct.Top And Cursor.Y < rct.Top + sens Then Return 2
                If Cursor.X > rct.Right - sens And Cursor.X < rct.Right And Cursor.Y > rct.Top And Cursor.Y < rct.Bottom Then Return 3
                If Cursor.X > rct.Left And Cursor.X < rct.Right And Cursor.Y > rct.Bottom - sens And Cursor.Y < rct.Bottom Then Return 4
            End If
        End If
        Return 0
    End Function

    'Передвижение границы дома
    Public Sub EditHouse(ByVal EdgeNum)
        If Not SelObj = ObjectType.Building Then Exit Sub
        Dim rct As Rectangle = Houses(SelNum).Rect
        Select Case EdgeNum
            Case 1
                Dim NewLeft As Integer = Int(Cursor.X)
                If rct.Right - NewLeft <= 10 Then Exit Sub
                Dim NewRect As New Rectangle(NewLeft, rct.Top, rct.Width + rct.Left - NewLeft, rct.Height)
                Houses(SelNum).Rect = NewRect
            Case 2
                Dim NewTop As Integer = Int(Cursor.Y)
                If rct.Bottom - NewTop <= 10 Then Exit Sub
                Dim NewRect As New Rectangle(rct.Left, NewTop, rct.Width, rct.Height + rct.Top - NewTop)
                Houses(SelNum).Rect = NewRect
            Case 3
                Dim NewRight As Integer = Int(Cursor.X)
                If NewRight - rct.Left <= 10 Then Exit Sub
                Dim NewRect As New Rectangle(rct.Left, rct.Top, rct.Width - rct.Right + NewRight, rct.Height)
                Houses(SelNum).Rect = NewRect
            Case 4
                Dim NewBottom As Integer = Int(Cursor.Y)
                If NewBottom - rct.Top <= 10 Then Exit Sub
                Dim NewRect As New Rectangle(rct.Left, rct.Top, rct.Width, rct.Height - rct.Bottom + NewBottom)
                Houses(SelNum).Rect = NewRect
        End Select

    End Sub

     
#End Region



#Region "Selection"

    Public SelObj As City.ObjectType    'Что за объект сейчас выбран (выделен)?
    Public SelNum As Integer            'Номер этого выбранного объекта

    'Выбирает объект, если в него ппадает курсор ( по нажатию мышки в режиме выделения)
    Public Sub SelectObject()
        SelObj = ObjectType.None

        For i = 1 To CarsCounter
            If Cars(i).Exists Then
                If Cars(i).IsPointInside(Cursor, Me) Then
                    SelObj = ObjectType.Car
                    SelNum = i
                    Exit Sub
                End If
            End If
        Next

        For i = 1 To ISCounter
            If IntScs(i).Exists Then
                If Sqrt((IntScs(i).x - Cursor.X) ^ 2 + (IntScs(i).y - Cursor.Y) ^ 2) < Sensivity Then
                    SelObj = ObjectType.Intersection
                    SelNum = i
                    Exit Sub
                End If
            End If
        Next

        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                Dim pth = Roads(i).GetPath(Me)
                If pth.IsVisible(Cursor) Then
                    SelObj = ObjectType.Road
                    SelNum = i
                    Exit Sub
                End If
            End If
        Next

        For i = 1 To HsCounter
            If Houses(i).Exists Then
                If Houses(i).CheckPointInside(Cursor) Then
                    SelObj = ObjectType.Building
                    SelNum = i
                    Exit Sub
                End If
            End If
        Next


    End Sub

    'Снять выделение
    Public Sub CancelSelection()
        SelObj = ObjectType.None
    End Sub
#End Region



#End Region

#Region "Graphics" 


    Public CarColors As Color() = {Color.Green, Color.Yellow, Color.Blue, Color.Black, Color.Brown, Color.FromArgb(255, 193, 35, 255)}
     
    Private RoadBrush As Brush = New SolidBrush(Color.Gray)

    'Draw city
    Public Sub DrawCity(ByRef g As Graphics)
        g.Clear(BgColor)

        If DrawBorder Then
            g.DrawRectangle(Pens.Black, 0, 0, Width, Height)
        End If

        'Drawing Intersections
        For i = 1 To ISCounter
            If IntScs(i).Exists Then
                g.FillEllipse(RoadBrush, IntScs(i).x - RoadWidth, IntScs(i).y - RoadWidth, 2 * RoadWidth, 2 * RoadWidth)
                g.FillEllipse(Brushes.Blue, IntScs(i).x - 5, IntScs(i).y - 5, 10, 10)
            End If
        Next

        'Drawing Roads
        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                Dim p1 As PointF = IntScs(Roads(i).st).GetPoint
                Dim p2 As PointF = IntScs(Roads(i).en).GetPoint

                Dim angle As Double = Atan2(p2.Y - p1.Y, p2.X - p1.X)
                Dim ang1 = angle + PI / 2
                Dim ang2 = angle - PI / 2

                Dim pts() As Point = Roads(i).GetPolygon(Me)

                g.FillPolygon(RoadBrush, pts)



                g.DrawLine(New Pen(Color.White, 2), p1, p2)
                Dim DotPen As New Pen(Color.White, 1)
                DotPen.DashStyle = DashStyle.Dash
                For j = 1 To Roads(i).n2 - 1
                    g.DrawLine(DotPen, New Point(p1.X + j * RoadWidth * Cos(ang1), p1.Y + j * RoadWidth * Sin(ang1)), New Point(p2.X + j * RoadWidth * Cos(ang1), p2.Y + j * RoadWidth * Sin(ang1)))
                Next
                For j = 1 To Roads(i).n1 - 1
                    g.DrawLine(DotPen, New Point(p1.X - j * RoadWidth * Cos(ang1), p1.Y - j * RoadWidth * Sin(ang1)), New Point(p2.X - j * RoadWidth * Cos(ang1), p2.Y - j * RoadWidth * Sin(ang1)))
                Next

                If ShowAnalyse Then
                    If Roads(i).AlertState > 0 Then
                        Dim AlertPen As New Pen(Color.FromArgb(255, 255, Roads(i).AlertState, 0), 3)
                        g.DrawPolygon(AlertPen, pts)
                    End If
                End If

            End If
        Next

        'Drawing Cars
        For i = 1 To CarsCounter
            If Cars(i).Exists Then
                Dim _Brush As SolidBrush = New SolidBrush(Cars(i).Color)
                g.FillPolygon(_Brush, Cars(i).GetRect(Me))
            End If
        Next

        'Drawing Buildings
        For i = 1 To HsCounter
            If Houses(i).Exists Then Houses(i).Draw(g, False, ShowAnalyse)
        Next


        'Drawing Selected Components
        Select Case SelObj
            Case ObjectType.Intersection
                g.FillEllipse(Brushes.Red, IntScs(SelNum).x - 7, IntScs(SelNum).y - 7, 14, 14)
                If ShowAnalyse Then IntScs(SelNum).MarkRoads(g, Me)
            Case ObjectType.Road
                g.DrawPolygon(New Pen(Color.Red, 2), Roads(SelNum).GetPolygon(Me))
            Case ObjectType.Car
                g.DrawPolygon(New Pen(Color.Red, 2), Cars(SelNum).GetRect(Me))
                Cars(SelNum).DrawRoute(Me, g)
            Case ObjectType.Building
                Houses(SelNum).Draw(g, True, ShowAnalyse)
                If Houses(SelNum).Targ_Road > 0 And ShowAnalyse Then
                    Dim p0 As PointF = Roads(Houses(SelNum).Targ_Road).GetPoint(Houses(SelNum).Targ_Line, Houses(SelNum).Targ_PosAtLine, Me)
                    g.FillEllipse(Brushes.Green, Int(p0.X) - 5, Int(p0.Y) - 5, 10, 10)
                End If

        End Select



        'Drawing Editing Components
        If AddingRoad > 0 Then
            g.DrawLine(New Pen(Color.Black, 1), IntScs(AddingRoad).GetPoint, Cursor)
        End If

        If AddingBuilding Then
            g.DrawRectangle(Pens.Gray, Geometry.GetRect(Cursor, BuildingFirstPos))
        End If


        'Draw Marks
        If ShowAnalyse Then
            For i = 1 To ISCounter
                If IntScs(i).Exists Then
                    If IntScs(i).PassMode = 2 Then
                        g.DrawImage(Editor.BitmapControl, IntScs(i).x - 40, IntScs(i).y - 40, 30, 30)
                    End If
                End If
            Next

            For i = 1 To RoadCounter
                If Roads(i).Exists Then
                    If Roads(i).IsBridge Then
                        g.DrawImage(Editor.BitmapBridge, Roads(i).Middle.X - 30, Roads(i).Middle.Y - 30, 30, 30)
                    End If
                    If Roads(i).IsNarrow Then
                        g.DrawImage(Editor.BitmapNarrow, Roads(i).Middle.X - 30, Roads(i).Middle.Y - 0, 30, 30)
                    End If
                End If
            Next
        End If


        If GTA_Active Then GTA_Draw(g)
        If DRUG_Active Then DRUG_Upd()
    End Sub

    Public Sub AllignToGrid(ByVal GridStep As Integer)
        For i = 1 To ISCounter
            If IntScs(i).Exists Then IntScs(i).AllignToGrid(GridStep)
        Next
        For i = 1 To HsCounter
            If Houses(i).Exists Then Houses(i).AllignToGrid(GridStep)
        Next
    End Sub
#End Region

#Region "Modelling"
    Private TimeCounter As Single = 0   'Таймер для инициализации долгопериодических операций

    'Происходит движение города за малый промежуток времени Time в секундах. Предполагается вызов по таймеру или в цикле
    Public Sub ModelStep(ByVal dt As Single)
        'Рухаємо всі машини
        For i = 1 To CarsCounter
            If Cars(i).Exists Then Cars(i).Move(dt, Me)
        Next
        'Рухаємо машини на перезрестях
        For i = 1 To ISCounter
            If IntScs(i).Exists Then IntScs(i).Move(dt, Me)
        Next
        'Випадкови1 виїзд із будинків
        For i = 1 To HsCounter
            If Rnd() < Building.MaxOutPossibility * (Houses(i).CarsNum / Houses(i).MaxCarsNum) * dt Then Houses(i).CreateCar(Me)
        Next

        TimeCounter += dt
        If TimeCounter >= 0.5 Then
            For i = 1 To ISCounter
                If IntScs(i).Exists Then
                    IntScs(i).TryStartWaitingCars(Me)
                    If IntScs(i).PassMode = 2 Then IntScs(i).TrControl.Refresh(TimeCounter)
                End If
            Next
            AnalyseStep(TimeCounter)
            TimeCounter = 0
        End If

        If GTA_Active Then GTA_Move()

        Time += dt  'Збільшуємо час міста
    End Sub

#End Region

#Region "FileIO"
    Const ServiceSymbols As String = "|$­­¬"

    'Сохранить данный город в файл FileName
    Public Sub Save(ByVal FileName As String)
        WriteAllText(FileName, Me.ToString, False)
    End Sub

    'Текстовое представление города для сохранения
    Public Overrides Function ToString() As String
        Dim ret As String = Me.Name & "|" & Me.BgColor.R & "|" & Me.BgColor.G & "|" & Me.BgColor.B & "|" & Width & "|" & height & _
            "|" & ISCounter & "|" & RoadCounter & "|" & CarsCounter & "|" & HsCounter & "|" & Int(Time) & "$"

        'Write Intersections
        For i = 1 To ISCounter
            If IntScs(i).Exists Then
                ret &= IntScs(i).ToString & "$"
            Else
                ret &= "Empty$"
            End If
        Next

        'Write Roads
        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                ret &= Roads(i).ToString & "$"
            Else
                ret &= "Empty$"
            End If
        Next

        'Write Cars
        For i = 1 To CarsCounter
            If Cars(i).Exists Then
                ret &= Cars(i).ToString & "$"
            Else
                ret &= "Empty$"
            End If
        Next

        'Write Buildings
        For i = 1 To HsCounter
            If Houses(i).Exists Then
                ret &= Houses(i).ToString & "$"
            Else
                ret &= "Empty$"
            End If
        Next

        Return ret
    End Function

    'Создать город по строчному представлению и инициализировать все объекты
    Public Sub New(ByVal StringPresentation As String)
        For i = 1 To ArraysSize
            IntScs(i) = New Intersection()
            Roads(i) = New Road()
            Cars(i) = New Car
            Houses(i) = New Building
        Next

        Dim s() As String = StringPresentation.Split("$")
        Dim header() As String = s(0).Split("|")

        Me.Name = header(0)
        Me.BgColor = Color.FromArgb(255, CInt(header(1)), CInt(header(2)), CInt(header(3)))
        Me.Width = CInt(header(4))
        Me.height = CInt(header(5))
        Me.ISCounter = CInt(header(6))
        Me.RoadCounter = CInt(header(7))
        Me.CarsCounter = CInt(header(8))
        Me.HsCounter = CInt(header(9))
        Me.Time = CInt(header(10))

        Dim counter As Integer = 0

        For i = 1 To ISCounter
            counter += 1
            If s(counter) = "Empty" Then IntScs(i) = New Intersection() Else IntScs(i) = New Intersection(s(counter), i)
        Next

        For i = 1 To RoadCounter
            counter += 1
            If s(counter) = "Empty" Then
                Roads(i) = New Road()
            Else
                Roads(i) = New Road(s(counter))
                Roads(i).DetermineGeom(Me)
            End If
        Next

        For i = 1 To CarsCounter
            counter += 1
            If s(counter) = "Empty" Then
                Cars(i) = New Car()
            Else
                Cars(i) = New Car(s(counter))
            End If
        Next

        For i = 1 To HsCounter
            counter += 1
            If s(counter) = "Empty" Then Houses(i) = New Building() Else Houses(i) = New Building(s(counter))
            If Houses(i).Exists Then
                Houses(i).Number = i
                Houses(i).DetermineTargetParam(Me)
            End If
        Next

        For i = 1 To CarsCounter
            If Cars(i).Exists Then
                Cars(i).SetRandomTarget(Me)
                Cars(i).DetermineRoute(Me)
            End If
        Next

        For i = 1 To RoadCounter
            If Roads(i).Exists Then Roads(i).DetermineGeom(Me)
        Next
    End Sub

#End Region

#Region "Analysis"




    Private RoadVelSum(1000) As Integer

    Const ALertCarQuant As Integer = 5      'Мин.число машин для пробки
    Const AlertAvgSpeed As Single = 10      'Ср.скорость, говорящая о начале пробки
    Const NarrowAngSpeed As Single = 5      'Лимит средней скорости для выдачи сообщения об узкой дороге

    Public Sub AnalyseStep(ByVal dTime As Single)     'Шаг анализа (пересчёт средних скоростей)
        For i = 1 To RoadCounter
            RoadVelSum(i) = 0
            Roads(i).Analyse_CarCounter = 0
        Next

        RecountActualSpeeds(dTime)
        For i = 1 To CarsCounter
            If Cars(i).Exists Then
                RoadVelSum(Cars(i).Road) += Cars(i).ActualSpeed
                Roads(Cars(i).Road).Analyse_CarCounter += 1
            End If
        Next

        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                Roads(i).IsNarrow = False
                Roads(i).AlertState = 0
                If Roads(i).Analyse_CarCounter > 0 Then
                    Dim avgSpeed As Single = RoadVelSum(i) / Roads(i).Analyse_CarCounter
                    Roads(i).Analyse_AvgSpeedSummator += avgSpeed
                    Roads(i).Analyse_AvgSpeedCounter += 1
                    Roads(i).Analyse_AvgSpeed = avgSpeed

                    If Roads(i).Analyse_CarCounter > ALertCarQuant Then
                        If avgSpeed < AlertAvgSpeed Then
                            Roads(i).AlertState = 1 + 254 * (1 - avgSpeed / AlertAvgSpeed)
                        End If
                        If avgSpeed < NarrowAngSpeed Then 
                            Roads(i).IsNarrow = True
                        End If
                    End If
                End If
            End If
        Next

    End Sub



    'Пересчёт фактических скоростей машин
    Public Sub RecountActualSpeeds(ByVal dTime As Single)
        For i = 1 To CarsCounter
            If Cars(i).Exists Then
                Cars(i).ActualSpeed = (Dist(Cars(i).LastPos, Cars(i).Pos) / dTime) * ScaleReal
                Cars(i).LastPos = Cars(i).Pos
            End If
        Next

    End Sub



    Public Function GetTime() As String   'Возвращает текущее время города в формате ЧЧ:ММ:СС
        Dim tm As Integer = Int(Time)
        Dim Seconds = tm Mod 60
        tm = tm \ 60
        Dim minutes = tm Mod 60
        tm = tm \ 60
        Dim hours = tm Mod 24
        Dim ret As String = ""
        If hours < 10 Then ret &= "0"
        ret &= hours & ":"
        If minutes < 10 Then ret &= "0"
        ret &= minutes & ":"
        If Seconds < 10 Then ret &= "0"
        ret &= Seconds '
        Return ret
    End Function

    Public Sub ResetCounters()      'Обнуляет счётчики машин
        For i = 1 To RoadCounter
            If Roads(i).Exists Then Roads(i).ResetCounters()
        Next
        For i = 1 To HsCounter
            If Houses(i).Exists Then Houses(i).ResetCounters()
        Next
    End Sub

#Region "Bridges"
    Dim Bridges_Used(ArraysSize) As Boolean
    Dim Bridges_Tin(ArraysSize) As Integer
    Dim Bridges_Fup(ArraysSize) As Integer
    Dim Bridges_Timer As Integer
    Dim AjList(ArraysSize) As Generic.List(Of Pair)
     

    Public Sub FindBridges()
        Bridges_Timer = 0
        MakeAjList()
        For i = 1 To ISCounter
            Bridges_Used(i) = False
        Next
        For i = 1 To RoadCounter
            If Roads(i).Exists Then Roads(i).IsBridge = False
        Next
        For i = 1 To ISCounter
            If Not Bridges_Used(i) Then Bridges_DFS(-1, i)
        Next
    End Sub

    Private Sub Bridges_DFS(ByVal p As Integer, ByVal v As Integer)
        Bridges_Used(v) = True

        Bridges_Timer += 1
        Bridges_Fup(v) = Bridges_Timer
        Bridges_Tin(v) = Bridges_Timer

        For Each i In AjList(v)
            Dim vto As Integer = i.First
            If (vto = p) Then Continue For
            If Bridges_Used(vto) Then
                Bridges_Fup(v) = Min(Bridges_Fup(v), Bridges_Tin(vto))
            Else
                Bridges_DFS(v, vto)
                Bridges_Fup(v) = Min(Bridges_Fup(v), Bridges_Fup(vto))
                If Bridges_Fup(vto) > Bridges_Tin(v) Then
                    Roads(i.Second).IsBridge = True
                End If
            End If
        Next
    End Sub

    Private Sub MakeAjList()
        For i = 1 To ISCounter
            AjList(i) = New Generic.List(Of Pair)
        Next
        Dim pr As Pair
        For i = 1 To RoadCounter
            If Roads(i).Exists Then
                pr.Second = i
                If Roads(i).n1 > 0 Then
                    pr.First = Roads(i).st
                    AjList(Roads(i).en).Add(pr)
                End If
                If Roads(i).n2 > 0 Then
                    pr.First = Roads(i).en
                    AjList(Roads(i).st).Add(pr)
                End If
            End If
        Next
    End Sub
#End Region



#End Region

#Region "GTA"
    Public GTA_Active As Boolean = False
    Private GTA_Pos As Point
    Private GTA_MoveX As Integer, GTA_MoveY As Integer
    Private GTA_BulletEx(10) As Boolean
    Private GTA_Bullets(10) As Point
    Private GTA_BulletVel(10) As Point
    Private GTA_Orient As Integer
    Public Sub GTA_Activate()
        Randomize()
        GTA_Active = True
        Dim OK As Boolean

        Do
            GTA_Pos = New Point(Rnd() * Width, Rnd() * Height)
            OK = True
            For i = 1 To HsCounter
                If Houses(i).Exists Then If Houses(i).CheckPointInside(GTA_Pos) Then OK = False
            Next
        Loop Until OK

        GTA_Orient = 1
    End Sub

    Public Sub GTA_StartMove(ByVal Key As Integer)
        Dim MoveLen As Integer = 2
        Select Case Key
            Case 37
                GTA_MoveX = -MoveLen
                GTA_MoveY = 0
                GTA_Orient = 4
            Case 38
                GTA_MoveX = 0
                GTA_MoveY = -MoveLen
                GTA_Orient = 1
            Case 39
                GTA_MoveX = MoveLen
                GTA_MoveY = 0
                GTA_Orient = 2
            Case 40
                GTA_MoveX = 0
                GTA_MoveY = MoveLen
                GTA_Orient = 3
        End Select
    End Sub

    Public Sub GTA_StopMove()
        GTA_MoveX = 0
        GTA_MoveY = 0
    End Sub

    Private Sub GTA_Move()
        For i = 1 To CarsCounter
            If Cars(i).Exists Then If Cars(i).IsPointInside(GTA_Pos, Me) Then GTA_Active = False
        Next


        If GTA_MoveX <> 0 Or GTA_MoveY <> 0 Then
            Dim NewPos As Point = New Point(GTA_Pos.X + GTA_MoveX, GTA_Pos.Y + GTA_MoveY)
            Dim OK As Boolean = True
            For i = 1 To HsCounter
                If Houses(i).Exists Then If Houses(i).CheckPointInside(NewPos) Then OK = False
            Next
            If OK Then GTA_Pos = NewPos
        End If


        For i = 1 To 10
            If GTA_BulletEx(i) Then
                GTA_Bullets(i) = New Point(GTA_Bullets(i).X + GTA_BulletVel(i).X, GTA_Bullets(i).Y + GTA_BulletVel(i).Y)
                If GTA_Bullets(i).X < 0 Or GTA_Bullets(i).X > Width Or GTA_Bullets(i).Y < 0 Or GTA_Bullets(i).Y > Height Then
                    GTA_BulletEx(i) = False
                End If
                For j = 1 To CarsCounter
                    If Cars(j).Exists Then
                        If Cars(j).IsPointInside(GTA_Bullets(i), Me) Then
                            GTA_BulletEx(i) = False
                            RemoveCar(j)
                        End If
                    End If
                Next
                For j = 1 To HsCounter
                    If Houses(j).Exists Then If Houses(j).CheckPointInside(GTA_Bullets(i)) Then GTA_BulletEx(i) = False
                Next
            End If
        Next
    End Sub

    Private Sub GTA_Draw(ByRef g As Graphics)

        g.FillEllipse(Brushes.Black, GTA_Pos.X - 4, GTA_Pos.Y - 4, 8, 8)
        Select Case GTA_Orient
            Case 1
                g.FillRectangle(Brushes.Black, GTA_Pos.X - 6, GTA_Pos.Y - 2, 12, 4)
                g.FillRectangle(Brushes.Black, GTA_Pos.X + 2, GTA_Pos.Y - 8, 2, 8)
            Case 2
                g.FillRectangle(Brushes.Black, GTA_Pos.X - 2, GTA_Pos.Y - 6, 4, 12)
                g.FillRectangle(Brushes.Black, GTA_Pos.X, GTA_Pos.Y + 2, 8, 2)
            Case 3
                g.FillRectangle(Brushes.Black, GTA_Pos.X - 6, GTA_Pos.Y - 2, 12, 4)
                g.FillRectangle(Brushes.Black, GTA_Pos.X - 4, GTA_Pos.Y, 2, 8)
            Case 4
                g.FillRectangle(Brushes.Black, GTA_Pos.X - 2, GTA_Pos.Y - 6, 4, 12)
                g.FillRectangle(Brushes.Black, GTA_Pos.X - 8, GTA_Pos.Y - 4, 8, 2)
        End Select



        For i = 1 To 10
            If GTA_BulletEx(i) Then
                g.FillEllipse(Brushes.Red, GTA_Bullets(i).X - 3, GTA_Bullets(i).Y - 3, 6, 6)
            End If
        Next
    End Sub

    Public Sub GTA_Shot()
        Dim BulVel As Integer = 5
        Dim i As Integer = 0
        Do
            i += 1
        Loop Until i = 10 Or GTA_BulletEx(i) = False
        If Not GTA_BulletEx(i) Then
            GTA_BulletEx(i) = True
            GTA_Bullets(i) = GTA_Pos
            Select Case GTA_Orient
                Case 1 : GTA_BulletVel(i) = New Point(0, -BulVel)
                Case 2 : GTA_BulletVel(i) = New Point(BulVel, 0)
                Case 3 : GTA_BulletVel(i) = New Point(0, BulVel)
                Case 4 : GTA_BulletVel(i) = New Point(-BulVel, 0)
            End Select
        End If
    End Sub
#End Region
#Region "DRUG"
    Dim DRUG_Active As Boolean = False
    Public Sub DRUG()
        DRUG_Active = Not DRUG_Active
    End Sub

    Private Sub DRUG_Upd()
        For i = 1 To HsCounter
            If Houses(i).Exists Then Houses(i).Color = RandomColor()
        Next
    End Sub

    Private Function RandomColor() As Color
        Return Color.FromArgb(255, Rnd() * 255, Rnd() * 255, Rnd() * 255)
    End Function
#End Region


End Class
