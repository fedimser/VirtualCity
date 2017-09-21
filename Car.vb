Imports System.Math
Imports System.Drawing.Drawing2D
Imports VirtualCity.City


'Этот класс представляет машину как динамический объект в городе с привязкой к координатам и дорогам
Public Class Car


#Region "Declarations"
    Public Exists As Boolean            'Существует ли

    'Coordinates And Movement
    Public Pos As PointF                'Положение передней точки
    Public MovRot As Single             'Угол вектора направления движения
    Public TrueRot As Single            'Угол вектора направления машины
    Public Speed As Single              'Скорость (усл.пикс/с)
    Public Acceleration As Single       'Ускорение - зарезервировано

    'Routing
    Public TargetBuilding As Integer    'Номер дома-цели
    Private Route(1000) As Integer  'Маршрут как очередь номеров перекрёстков
    Private RouteBegPtr As Integer, RouteEndPtr As Integer
    Public RouteDetermined As Boolean = False   'Определён ли маршрут

    'Sizes
    Public Length As Single = 15        'Длина
    Public Width As Single = 7          'Ширина

    'Linking to roads
    Public Road As Integer              'Дорога, на которой находится машина
    Public Line As Integer              'Полоса, на которой находится машина
    Public PosAtLine As Single          'Расстояние от начала полосы до передней точки машины

    'Movement State
    Public AtRoad As Boolean = False        'На дороге ли?
    Public ChangingLine As Boolean = False  'Перестраивается ли?
    Public GoToRoad As Boolean = False      'Выезжает ли из дома?
    Public GoFromRoad As Boolean = False    'Въезжает ли в дом
    Public LastRoadOfRoute = False          'Является ли текущая дорога последней в маршруте

    'Presentation
    Public Color As Color = Color.Black 'Цвет
    Public Name As String               'Название
    Public Number As Integer            'Номер для свзи с родительским городом

    'Turning on intersections  (IST = InterSection Turning)
    Public IST_NewRoad As Integer       'Дорога,
    Public IST_NewLine As Integer       'Полоса,
    Public IST_NewPAT As Single         'и положение на полосе после поворота
    Public IST_Begin As PointF          'Точка начала поворота
    Public IST_Position As Single       'Текущие положение поворота
    Public IST_Len As Single            'Длина траектории поворота

    'Changing Line
    Public CL_Len As Single             'Длина траектории перестраивания
    Public CL_Pos As Single             'Положение в перестраиванииh

    'Going To/From Road
    Public GR_Len As Single             'Длина траектории въезда/выезда
    Public GR_Pos As Single             'Положение на траектории въезда/выезда
    Public GR_House As Integer = 0      'Дом, в которые въезжает/выезжает или 0 если не въезжает/выезжает

    'Analysis
    Public LastPos As PointF            'зарезервировано для анализа
    Public ActualSpeed As Single        'Скорость машины на самом деле, зарезервировано для анализа, m/s


#End Region

 

    'Конструктор по умолчанию
    Public Sub New()
        Exists = False 
    End Sub


    'Изменияет положение машины так, как это должно произойти за Time секунд
    Public Sub Move(ByVal Time As Single, ByRef CT As City)
        Dim dist As Single = Speed * Time   'Distance

        If Not AtRoad Then
            If GoToRoad Then     'If car is going from the building to the road
                GR_Pos += dist
                Drag(dist)
                If GR_Pos > GR_Len Then
                    Me.GoToRoad = False
                    Me.AtRoad = True
                    Me.MovRot = CT.Roads(Me.Road).Rotat
                    If Me.Line > 0 Then MovRot += PI
                    CT.Houses(GR_House).GR_IsCarGoingOut = False
                    Me.GR_House = 0
                End If
            End If
            Exit Sub
        End If

        If GoFromRoad Then                   'If car is going from the road to the building
            GR_Pos += dist
            Drag(dist)
            If GR_Pos >= GR_Len Then
                CT.Houses(GR_House).Analyse_InComeCounter += 1
                CT.Houses(GR_House).CarsNum += 1
                CT.Houses(GR_House).CarsGoingIn -= 1
                Me.Exists = False
            End If

            Exit Sub
        End If

        If ChangingLine Then    'If car is changing line
            CL_Pos += dist
            Drag(dist)
            If CL_Pos >= CL_Len Then
                ChangingLine = False
                Me.MovRot = CT.Roads(Me.Road).Rotat
                If Me.Line < 0 Then Me.MovRot += PI
                Me.TrueRot = Me.MovRot
            End If
            Exit Sub
        End If


        'Start going from the road to the building
        If LastRoadOfRoute And PosAtLine + dist >= CT.Houses(TargetBuilding).Targ_PosAtLine Then

            'First, check if there is place for this car. If it isn't, go somewhere else.
            If CT.Houses(TargetBuilding).CarsNum + CT.Houses(TargetBuilding).CarsGoingIn >= CT.Houses(TargetBuilding).MaxCarsNum Then
                Me.SetRandomTarget(CT)
                Me.DetermineRoute(CT)
            Else
                dist = CT.Houses(TargetBuilding).Targ_PosAtLine - PosAtLine
                Me.GoFromRoad = True
                Me.RouteDetermined = False
                Me.MovRot = Geometry.AngleBy2Pt(Me.Pos, CT.Houses(TargetBuilding).Targ_InPoint)
                Me.GR_Len = Geometry.Dist(Me.Pos, CT.Houses(TargetBuilding).Targ_InPoint)
                Me.GR_Pos = 0
                Me.GR_House = TargetBuilding

                CT.Houses(GR_House).CarsGoingIn += 1

                PosAtLine += dist
                Pos = GetPos(CT)
                Exit Sub
            End If



        End If

        'If there is car forward, decrease speed
        Dim IsCarForward As Boolean = False
        Dim SafeDist As Single = City.SafeDist
        For i = 1 To CT.CarsCounter
            If CT.Cars(i).Exists And Not (i = Me.Number) And CT.Cars(i).Road = Me.Road And CT.Cars(i).Line = Me.Line Then
                Dim CrBack As Single = CT.Cars(i).PosAtLine - CT.Cars(i).Length
                If CrBack > PosAtLine And CrBack - SafeDist < PosAtLine Then
                    dist = 0
                    IsCarForward = True
                ElseIf CrBack > PosAtLine And CrBack < PosAtLine + dist Then
                    dist = CrBack - SafeDist - PosAtLine
                    IsCarForward = True
                End If
            End If
        Next




        'Try start changing line
        Dim LineChOk As Boolean = False
        If IsCarForward And Me.PosAtLine < CT.Roads(Me.Road).Length - IntZone(CT) - Me.Length Then
            If Me.Line > 0 Then
                If CT.Roads(Me.Road).n1 > Me.Line Then
                    TryStartChangeLine(Me.Line + 1, CT, LineChOk)
                    If Me.Line > 1 Then TryStartChangeLine(Me.Line - 1, CT, LineChOk)
                End If
            Else
                If CT.Roads(Me.Road).n2 > Abs(Me.Line) Then
                    TryStartChangeLine(Me.Line - 1, CT, LineChOk)
                    If Me.Line < -1 Then TryStartChangeLine(Me.Line + 1, CT, LineChOk)
                End If
            End If
        End If
        If LineChOk Then Exit Sub

        'If car has reached intersection
        If PosAtLine + dist > CT.Roads(Road).Length - IntZone(CT) Then
            dist = CT.Roads(Road).Length - IntZone(CT) - PosAtLine
            IST_NewRoad = GetNextRoad(CT)
            Dim NextIntersection As Integer = NextInsc(CT)
            CT.IntScs(NextIntersection).RequestPass(Number, IST_NewRoad, CT)
        End If




        PosAtLine += dist
        Drag(dist)
        Pos = GetPos(CT)

    End Sub

    'Попытка начала перестраивания
    Private Sub TryStartChangeLine(ByVal NewLine As Integer, ByRef CT As City, ByRef Ok As Boolean)
        Dim Can As Boolean = True
        For i = 1 To CT.CarsCounter
            If CT.Cars(i).Exists Then
                If CT.Cars(i).Road = Me.Road And CT.Cars(i).Line = NewLine Then
                    If CT.Cars(i).PosAtLine > Me.PosAtLine - 2 * Me.Length And CT.Cars(i).PosAtLine < Me.PosAtLine + 2 * Me.Length Then Can = False
                End If
            End If
        Next
        If PosAtLine + 4 * Length > CT.Roads(Me.Road).Length Then Can = False
        If Can Then
            Ok = True 
            Me.Line = NewLine
            'Me.PosAtLine +=  Me.Length
            Me.ChangingLine = True
            Dim NewPt As PointF = CT.Roads(Me.Road).GetPoint(NewLine, Me.PosAtLine, CT)
            Me.MovRot = Geometry.AngleBy2Pt(Me.Pos, NewPt)
            Me.CL_Len = Geometry.Dist(Me.Pos, NewPt)
            Me.CL_Pos = 0
        End If
    End Sub

    'Перекрёсток в конце текущей полосы
    Private Function NextInsc(ByRef CT As City) As Integer
        If Me.Line > 0 Then
            Return CT.Roads(Me.Road).st
        Else    'If  Me.Line < 0 Then
            Return CT.Roads(Me.Road).en
        End If
    End Function

    'Зона перекрёстка в конце текущей полосы
    Private Function IntZone(ByRef CT As City) As Single
        If Me.Line > 0 Then
            Return CT.Roads(Me.Road).IntscZone1
        Else  'If Me.Line < 0 Then
            Return CT.Roads(Me.Road).IntscZone2
        End If
    End Function

    'Следующая дорога (чтобы знать куда поворачивать)
    Private Function GetNextRoad(ByRef CT As City) As Integer
        'Если есть маршрут, выбираем следующую по mаршруту дорогу, иначе - выбираем любую, ведущую с перекрёстка
        If RouteDetermined Then
            Dim NumIntsc As Integer = Me.NextInsc(CT)
            Dim NewRoad As Integer = 0
            If Route(RouteBegPtr) = NumIntsc Then RouteBegPtr += 1
            If RouteBegPtr = RouteEndPtr Then
                NewRoad = CT.Houses(Me.TargetBuilding).Targ_Road
                LastRoadOfRoute = True
            Else
                Dim NewIntsc As Integer = Route(RouteBegPtr)
                Dim j As Integer = 0
                Do
                    j += 1
                Loop Until j = CT.RoadCounter Or (CT.Roads(j).st = NumIntsc And CT.Roads(j).en = NewIntsc) Or (CT.Roads(j).en = NumIntsc And CT.Roads(j).st = NewIntsc)
                NewRoad = j

                If Not ((CT.Roads(j).st = NumIntsc And CT.Roads(j).en = NewIntsc) Or (CT.Roads(j).en = NumIntsc And CT.Roads(j).st = NewIntsc)) Then
                    Throw New Exception("Невозможно выбрать дорогу согласно маршруту")
                End If

            End If
            Return NewRoad
        Else 
            Dim c = 0
            Dim a(10) As Integer
            For i = 1 To CT.RoadCounter
                If CT.Roads(i).Exists Then
                    If (CT.Roads(i).st = NextInsc(CT) Or CT.Roads(i).en = NextInsc(CT)) And Not (i = Me.Road) Then
                        c += 1
                        a(c) = i
                    End If
                End If
            Next
            Return a(Int(Rnd() * c) + 1)
        End If
    End Function

    ' "Протаскує" машину на відстань dist під кутом MovRot за передню точку
    'Pos - положення передньої точки, MovRot - напрям руху, TrueRot - орієнтація
    Public Sub Drag(ByVal dist As Single)
        Pos = New PointF(Pos.X + dist * Cos(MovRot), Pos.Y + dist * Sin(MovRot))
         
        Dim l As Single = Me.Length
        Dim ang As Single = TrueRot - MovRot
        Dim x As Single = Sqrt(l * l + dist * dist + 2 * l * dist * Cos(ang))
        TrueRot -= Asin(dist / x * Sin(ang))
    End Sub

    'Установить случайную цель
    Public Function SetRandomTarget(ByRef CT As City) As Boolean   'Устанавливает данной машине случайную цель
        Dim a(1000) As Integer
        Dim kf(1000) As Single
        Dim kn As Single = 0
        Dim c As Integer = 0
        For i = 1 To CT.HsCounter
            If CT.Houses(i).Exists And CT.Houses(i).Targ_CanBe And Not (GR_House = i) Then
                c += 1
                a(c) = i
                kn += CT.Houses(i).IncomKoef
                kf(c) = kn
            End If
        Next
        If c > 0 Then
            Dim rk As Integer = Rnd() * kn
            For i = 1 To c
                If kf(i) >= rk Then
                    Me.TargetBuilding = a(i)
                    Return True
                End If
            Next
            Return False
        Else
            Return False
        End If
    End Function

     

    'Определить маршрут
    Public Sub DetermineRoute(ByRef CT As City)
        If TargetBuilding = 0 Then Exit Sub

        Dim Targ_Road As Integer = CT.Houses(TargetBuilding).Targ_Road
        Dim Targ_Line As Integer = CT.Houses(TargetBuilding).Targ_Line
        Dim Targ_PosAtLine As Single = CT.Houses(TargetBuilding).Targ_PosAtLine

        Me.RouteBegPtr = 0
        Me.RouteEndPtr = 0
        Me.LastRoadOfRoute = False

        If Me.Road = Targ_Road And Sign(Me.Line) = Sign(Targ_Line) And Me.PosAtLine <= Targ_PosAtLine Then
            Me.RouteDetermined = True
            Me.LastRoadOfRoute = True
            Exit Sub
        End If

        Dim StartIntsc As Integer
        Dim EndIntsc As Integer

        If Me.Line < 0 Then
            StartIntsc = CT.Roads(Me.Road).en
        Else
            StartIntsc = CT.Roads(Me.Road).st
        End If

        If Targ_Line < 0 Then
            EndIntsc = CT.Roads(Targ_Road).st
        Else
            EndIntsc = CT.Roads(Targ_Road).en
        End If

        If StartIntsc = EndIntsc Then
            Route(0) = StartIntsc
            RouteEndPtr += 1
            Me.RouteDetermined = True 
            Exit Sub
        End If


        Dim n As Integer = CT.ISCounter
        Dim ar(n, n) As Single
        For i = 0 To n
            For j = 0 To n
                ar(i, j) = Single.MaxValue
            Next
        Next

        For i = 1 To CT.RoadCounter
            If CT.Roads(i).Exists Then
                Dim len As Single = CT.Roads(i).Length
                If CT.CleverDrivers Then
                    If CT.Roads(i).Analyse_CarCounter < 5 Then
                        len *= Me.Speed
                    Else
                        len *= CT.Roads(i).Analyse_AvgSpeed
                    End If
                End If
                If CT.Roads(i).n1 > 0 Then ar(CT.Roads(i).en, CT.Roads(i).st) = len
                If CT.Roads(i).n2 > 0 Then ar(CT.Roads(i).st, CT.Roads(i).en) = len
            End If
        Next

        If Me.Line < 0 Then
            ar(CT.Roads(Me.Road).en, CT.Roads(Me.Road).st) = Single.MaxValue
        Else
            ar(CT.Roads(Me.Road).st, CT.Roads(Me.Road).en) = Single.MaxValue
        End If

        If Targ_Line < 0 Then
            ar(CT.Roads(Targ_Road).en, CT.Roads(Targ_Road).st) = Single.MaxValue
        Else
            ar(CT.Roads(Targ_Road).st, CT.Roads(Targ_Road).en) = Single.MaxValue
        End If

        Dim ans As New List(Of Integer)
        If Geometry.FindShortestWay(n, ar, StartIntsc, EndIntsc, ans) Then
            Me.RouteDetermined = True
            For i = 0 To ans.Count - 1
                Route(RouteEndPtr) = ans(i)
                RouteEndPtr += 1
            Next 
        Else
            ' Throw New Exception("Неможливо доїхати")
            Me.RouteDetermined = False
            CT.RemoveCar(Me.Number)
        End If
         
    End Sub

    'Установить случайную скорость
    Public Sub SetRandomSpeed()
        Dim RealSpeed As Single = 10 + Rnd() * 10
        Me.Speed = RealSpeed / City.ScaleReal
    End Sub


#Region "FileIO"

    'Для вывода в файл
    Public Overrides Function ToString() As String
        Dim ret As String = ""

        ret &= Pos.X & "|" & Pos.Y & "|" & MovRot & "|" & TrueRot & "|" & Speed & "|" & Acceleration & "|" & TargetBuilding
        ret &= "|" & Length & "|" & Width & "|" & Road & "|" & Line & "|" & PosAtLine & "|"
        If AtRoad Then ret &= "1" Else ret &= "0"
        ret &= "|" & Color.R & "|" & Color.G & "|" & Color.B
        ret &= "|" & Name & "|" & Number
        ret &= "|" & IST_NewRoad & "|" & IST_NewLine & "|" & IST_NewPAT & "|" & IST_Begin.X & "|" & IST_Begin.Y & "|" & IST_Position & "|" & IST_Len & "|"
        If GoToRoad Then ret &= "1" Else ret &= "0"
        ret &= "|"
        If GoFromRoad Then ret &= "1" Else ret &= "0"
        ret &= "|"
        If ChangingLine Then ret &= "1" Else ret &= "0"
        ret &= "|"
        ret &= CL_Len & "|" & CL_Pos & "|" & GR_Len & "|" & GR_Pos & "|" & GR_House

        Return ret
    End Function

    'Для чтения из файла
    Public Sub New(ByVal StringPresntation As String)
        Dim s() = StringPresntation.Split("|")
        Me.Pos = New PointF(s(0), s(1))
        Me.MovRot = s(2)
        Me.TrueRot = s(3)
        Me.Speed = s(4)
        Me.Acceleration = s(5)
        Me.TargetBuilding = s(6)
        Me.Length = s(7)
        Me.Width = s(8)
        Me.Road = CInt(s(9))
        Me.Line = CInt(s(10))
        Me.PosAtLine = s(11)
        Me.AtRoad = (s(12) = "1")
        Me.Color = Color.FromArgb(255, CInt(s(13)), CInt(s(14)), CInt(s(15)))
        Me.Name = s(16)
        Me.Number = CInt(s(17))
        Me.IST_NewRoad = CInt(s(18))
        Me.IST_NewLine = CInt(s(19))
        Me.IST_NewPAT = s(20)
        Me.IST_Begin = New PointF(s(21), s(22))
        Me.IST_Position = s(23)
        Me.IST_Len = s(24)
        Me.GoToRoad = (s(25) = "1")
        Me.GoFromRoad = (s(26) = "1")
        Me.ChangingLine = (s(27) = "1")
        CL_Len = s(28)
        CL_Pos = s(29)
        GR_Len = s(30)
        GR_Pos = s(31)
        Try : GR_House = s(32) : Catch ex As Exception : End Try



        Me.Exists = True
    End Sub


#End Region

    Public Function ShowRoute() As String
        Dim s As String = ""
        For i = 0 To RouteEndPtr - 1
            s &= CStr(Route(i)) & " "
        Next
        Return s
    End Function


#Region "Graphics"

    'Returns polygon (rectangle) that determines position of car
    Public Function GetRect(ByRef CT As City) As PointF()
        Dim pnts(4) As PointF
        'Перпендикуляр до напряму орієнтації машини
        Dim ang1 As Single = TrueRot - PI / 2
        Dim p3 As PointF = Pos  'Передня точка машини
        'Задня точка машини
        Dim p4 As PointF = New PointF(p3.X - Length * Cos(TrueRot), p3.Y - Length * Sin(TrueRot))
        'Визначаємо координати вершин прямокутника
        pnts(0) = New PointF(p3.X - 0.5 * Width * Cos(ang1), p3.Y - 0.5 * Width * Sin(ang1))
        pnts(1) = New PointF(p3.X + 0.5 * Width * Cos(ang1), p3.Y + 0.5 * Width * Sin(ang1))
        pnts(3) = New PointF(p4.X - 0.5 * Width * Cos(ang1), p4.Y - 0.5 * Width * Sin(ang1))
        pnts(2) = New PointF(p4.X + 0.5 * Width * Cos(ang1), p4.Y + 0.5 * Width * Sin(ang1))
        pnts(4) = pnts(0)
        Return pnts
    End Function


    'Находится ли точка внутри машины
    Public Function IsPointInside(ByVal pnt As PointF, ByRef CT As City) As Boolean
        Dim gp As New GraphicsPath
        gp.AddPolygon(GetRect(CT))
        Return gp.IsVisible(pnt)
    End Function

    'Положение передней точке в системе координат города
    Public Function GetPos(ByVal CT As City) As PointF
        Dim ang1, angle As Single
        Dim RoadWidth As Integer = City.RoadWidth
        Dim Position As Single
        If Line > 0 Then
            Position = CT.Roads(Road).Length - PosAtLine
        ElseIf Line < 0 Then
            Position = PosAtLine
        End If


        Dim p1 As PointF = CT.IntScs(CT.Roads(Me.Road).st).GetPoint
        Dim p2 As PointF = CT.IntScs(CT.Roads(Me.Road).en).GetPoint
        angle = Atan2(p2.Y - p1.Y, p2.X - p1.X)
        ang1 = angle - PI / 2
        Dim p3 As PointF = New PointF(p1.X + Position * Cos(angle), p1.Y + Position * Sin(angle))
        Dim dist As Single = Me.Line - 0.5 * Sign(Me.Line)
        Return New PointF(p3.X + RoadWidth * dist * Cos(ang1), p3.Y + RoadWidth * dist * Sin(ang1))
    End Function

    'Нарисовать маршрут
    Public Sub DrawRoute(ByRef CT As City, ByRef g As Graphics)
        If Not RouteDetermined Then Exit Sub
        Dim p(1000) As PointF
        p(0) = Me.Pos
        Dim c As Integer = 1
        For i = RouteBegPtr To RouteEndPtr - 1
            p(c) = CT.IntScs(Route(i)).GetPoint
            c += 1
        Next
        p(c) = CT.Roads(CT.Houses(TargetBuilding).Targ_Road).GetPoint(CT.Houses(TargetBuilding).Targ_Line, CT.Houses(TargetBuilding).Targ_PosAtLine, CT)
        ReDim Preserve p(c)

        g.DrawLines(New Pen(Color.Red, 3), p)
    End Sub

    Public Sub SetRandomColor(ByRef CT As City)
        Dim n As Integer = Int(Rnd() * CT.CarColors.Length)
        Me.Color = CT.CarColors(n)
    End Sub

#End Region






End Class