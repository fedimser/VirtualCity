Imports System.Math
Imports System.Drawing.Drawing2D

'Этот класс представляет дорогу, как элемент горда, привязанный к двум перекрёсткам

Public Class Road

    Public st As Integer        'Номер перекрёстка с одной стороны
    Public en As Integer        'Номер перекрёстка с другой стороны
    Public Exists As Boolean    'Существует ли дорога
    Public n1, n2 As Integer    'Число полос в одну и в другую сторону

    Public Name As String

    'Presentation
    Public AlertState As Byte = 0    '0-Всё ОК, 1-255-пробка


    'Вычисляемые поля
    Public Length As Single     'Длина дорги
    Public Rotat As Single      'Угол вектора "Начло-Конец"
    Public IntscZone1 As Single 'Зона перекрёстка (т.е расстояние от центра перекрётска до точки, скоторой машинам следуети начинать поворот) с одной стороны
    Public IntscZone2 As Single 'Зона перекрёстка с другой стороны
    Public Width As Single      'Ширина доргои
    Public Middle As Point

    


    'Конструктор по умолчанию
    Public Sub New()
        Exists = False
    End Sub

    'Конструктор по двум перекрёсткам
    Public Sub New(ByVal i1%, ByVal i2%)
        If i1 > i2 Then
            Dim i3% = i1 : i1 = i2 : i2 = i3
        End If
        st = i1
        en = i2
        n1 = 1
        n2 = 1
        Exists = True
    End Sub

#Region "Road.FileIO"
    'Для вывода в файл
    Public Overrides Function ToString() As String
        Return st & "|" & en & "|" & n1 & "|" & n2 & "|" & Name
    End Function

    'Для чтения из файла
    Public Sub New(ByVal StringPresentation As String)
        Dim s() As String = StringPresentation.Split("|")
        Me.st = CInt(s(0))
        Me.en = CInt(s(1))
        Me.n1 = CInt(s(2))
        Me.n2 = CInt(s(3))
        Me.Name = s(4)
        Me.Exists = True
    End Sub
#End Region

#Region "Graphics"
    'Контур дорги
    Public Function GetPath(ByRef CT As City) As GraphicsPath
        Dim RoadWidth As Integer = City.RoadWidth

        Dim p1 As PointF = CT.IntScs(st).GetPoint
        Dim p2 As PointF = CT.IntScs(en).GetPoint

        Dim angle As Double = Atan2(p2.Y - p1.Y, p2.X - p1.X)
        Dim ang1 = angle + PI / 2
        Dim ang2 = angle - PI / 2
        Dim pts(4) As Point
        pts(0) = New Point(p1.X + RoadWidth * n2 * Cos(ang1), p1.Y + RoadWidth * n2 * Sin(ang1))
        pts(1) = New Point(p2.X + RoadWidth * n2 * Cos(ang1), p2.Y + RoadWidth * n2 * Sin(ang1))
        pts(2) = New Point(p2.X + RoadWidth * n1 * Cos(ang2), p2.Y + RoadWidth * n1 * Sin(ang2))
        pts(3) = New Point(p1.X + RoadWidth * n1 * Cos(ang2), p1.Y + RoadWidth * n1 * Sin(ang2))
        pts(4) = pts(0)
        Dim pth As New GraphicsPath
        pth.AddPolygon(pts)
        Return pth
    End Function

    Public Function GetMiddle(ByVal CT As City) As Point
        Return Geometry.Middle(CT.IntScs(st).GetPoint, CT.IntScs(en).GetPoint)
    End Function
#End Region
   

    'Многоугольник дороги
    Public Function GetPolygon(ByRef CT As City) As Point()
        Dim RoadWidth As Integer = City.RoadWidth

        Dim p1 As PointF = CT.IntScs(st).GetPoint
        Dim p2 As PointF = CT.IntScs(en).GetPoint

        Dim angle As Double = Atan2(p2.Y - p1.Y, p2.X - p1.X)
        Dim ang1 = angle + PI / 2
        Dim ang2 = angle - PI / 2
        Dim pts(4) As Point
        pts(0) = New Point(p1.X + RoadWidth * n2 * Cos(ang1), p1.Y + RoadWidth * n2 * Sin(ang1))
        pts(1) = New Point(p2.X + RoadWidth * n2 * Cos(ang1), p2.Y + RoadWidth * n2 * Sin(ang1))
        pts(2) = New Point(p2.X + RoadWidth * n1 * Cos(ang2), p2.Y + RoadWidth * n1 * Sin(ang2))
        pts(3) = New Point(p1.X + RoadWidth * n1 * Cos(ang2), p1.Y + RoadWidth * n1 * Sin(ang2))
        pts(4) = pts(0)
        Return pts
    End Function

    'Находится ли точка на дороге?
    Public Function IsPointInside(ByVal pnt As PointF, ByRef CT As City) As Boolean
        Dim gp As New GraphicsPath
        gp.AddPolygon(GetPolygon(CT))
        Return gp.IsVisible(pnt)
    End Function

    'Вычислить вычисляемые поля
    Public Sub DetermineGeom(ByRef CT As City)
        Length = Geometry.Dist(CT.IntScs(Me.st).GetPoint, CT.IntScs(Me.en).GetPoint)
        Rotat = Atan2(CT.IntScs(Me.en).y - CT.IntScs(Me.st).y, CT.IntScs(Me.en).x - CT.IntScs(Me.st).x)
        Me.Width = City.RoadWidth * (n1 + n2)


        Dim iz1 As Single = 0
        Dim iz2 As Single = 0
        Dim at As Single
        Dim rw As Single = City.RoadWidth
        For i = 1 To CT.RoadCounter
            If CT.Roads(i).Exists Then
                If CT.Roads(i).st = Me.st Then
                    at = CT.Roads(i).Rotat - Me.Rotat - PI / 2
                    iz1 = Max(iz1, Max(CT.Roads(i).n2 * rw * Cos(at), -CT.Roads(i).n1 * rw * Cos(at)))
                End If
                If CT.Roads(i).en = Me.st Then
                    at = CT.Roads(i).Rotat - PI - Me.Rotat - PI / 2
                    iz1 = Max(iz1, Max(CT.Roads(i).n1 * rw * Cos(at), -CT.Roads(i).n2 * rw * Cos(at)))
                End If
                If CT.Roads(i).st = Me.en Then
                    at = CT.Roads(i).Rotat - Me.Rotat - PI / 2 - PI
                    iz2 = Max(iz2, Max(CT.Roads(i).n2 * rw * Cos(at), -CT.Roads(i).n1 * rw * Cos(at)))
                End If
                If CT.Roads(i).en = Me.en Then
                    at = CT.Roads(i).Rotat - Me.Rotat - PI / 2
                    iz2 = Max(iz2, Max(CT.Roads(i).n1 * rw * Cos(at), -CT.Roads(i).n2 * rw * Cos(at)))
                End If
            End If
        Next

        IntscZone1 = Max(City.RoadWidth, Min(iz1, City.RoadWidth * 3))
        IntscZone2 = Max(City.RoadWidth, Min(iz2, City.RoadWidth * 3))

        Middle = GetMiddle(CT)

    End Sub

    'Возвращает вектор, задающий полосу
    Public Function GetLineCoord(ByVal Line As Integer, ByRef CT As City) As Geometry.Vector
        Dim p1, p2 As PointF
        p1 = CT.IntScs(Me.st).GetPoint
        p2 = CT.IntScs(Me.en).GetPoint
        Dim Dist As Single = City.RoadWidth * (Line - 0.5 * Sign(Line))
        Dim Angle = Me.Rotat
        Dim Angle1 = Angle - PI / 2
        Dim ret As New Geometry.Vector
        If Line > 0 Then
            ret.point = New Point(p2.X + Dist * Cos(Angle1), p2.Y + Dist * Sin(Angle1))
            ret.angle = Angle + PI
            If ret.angle > 2 * PI Then ret.angle -= 2 * PI
        Else       'If Line < 0 Then
            ret.point = New Point(p1.X + Dist * Cos(Angle1), p1.Y + Dist * Sin(Angle1))
            ret.angle = Angle
        End If
        ret.Length = Me.Length
        Return ret
    End Function

    'Возвращает координаты данной точки на данной полосе
    Public Function GetPoint(ByVal Line As Integer, ByVal PosAtLine As Single, ByRef CT As City) As PointF
        Dim dl As Single = City.RoadWidth * (Line - 0.5 * Sign(Line))
        Dim ps As Single
        If Line > 0 Then
            ps = Me.Length - PosAtLine
        Else
            ps = PosAtLine
        End If
        Dim p0 As PointF = CT.IntScs(Me.st).GetPoint
        Dim p1 As PointF = New PointF(p0.X + ps * Cos(Rotat), p0.Y + ps * Sin(Rotat))
        Dim ang1 = Rotat - PI / 2
        Return New PointF(p1.X + dl * Cos(ang1), p1.Y + dl * Sin(ang1))
    End Function

    Public Function HasIntersection(ByVal num As Integer) As Boolean
        Return (Me.st = num) Or (Me.en = num)
    End Function

#Region "Analysis"
    Public IsBridge As Boolean = False
    Public IsNarrow As Boolean = False

    'Переменные-счётчики для анализа
    Public Analyse_PassCarsCounter As Integer   'Сколько машин проехало 
    Public Analyse_AvgSpeedSummator As Single   'Сумматор для вычисления долгосрочной средней скорости
    Public Analyse_AvgSpeedCounter As Integer    'Счётчик для вычисления долгосрочной средней скорости
    Public Analyse_AvgSpeed As Single
    Public Analyse_CarCounter As Integer

    Public Sub ResetCounters()
        Analyse_PassCarsCounter = 0
        Analyse_AvgSpeedSummator = 0
        Analyse_AvgSpeedCounter = 0
    End Sub
#End Region
End Class