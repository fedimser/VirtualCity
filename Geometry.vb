Imports System.Math

Public Module Geometry
    Public Function IsCrossing(ByVal ax1!, ByVal ay1!, ByVal ax2!, ByVal ay2!, ByVal bx1!, ByVal by1!, ByVal bx2!, ByVal by2!) As Boolean
        Dim v1 As Double = (bx2 - bx1) * (ay1 - by1) - (by2 - by1) * (ax1 - bx1)
        Dim v2 As Double = (bx2 - bx1) * (ay2 - by1) - (by2 - by1) * (ax2 - bx1)
        Dim v3 As Double = (ax2 - ax1) * (by1 - ay1) - (ay2 - ay1) * (bx1 - ax1)
        Dim v4 As Double = (ax2 - ax1) * (by2 - ay1) - (ay2 - ay1) * (bx2 - ax1)
        Return ((v1 * v2 < 0) And (v3 * v4 < 0))
    End Function

    Public Function IsCrossing(ByVal a1 As Point, ByVal a2 As Point, ByVal b1 As Point, ByVal b2 As Point) As Boolean
        Return IsCrossing(a1.X, a1.Y, a2.X, a2.Y, b1.X, b1.Y, b2.X, b2.Y)
    End Function

    Public Function IsCrossing(ByVal a1 As PointF, ByVal a2 As PointF, ByVal b1 As PointF, ByVal b2 As PointF) As Boolean
        Return IsCrossing(a1.X, a1.Y, a2.X, a2.Y, b1.X, b1.Y, b2.X, b2.Y)
    End Function

  

    Public Function Dist(ByVal p1 As Point, ByVal p2 As Point) As Single
        Return Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2)
    End Function

    Function Dist(ByVal p1 As PointF, ByVal p2 As PointF) As Single
        Return Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2)
    End Function

    Function AngleBy2Pt(ByVal Pt1 As PointF, ByVal Pt2 As PointF) As Single
        Return Atan2(Pt2.Y - Pt1.Y, Pt2.X - Pt1.X)
    End Function

    Public Function GetRect(ByVal p1 As Point, ByVal p2 As Point) As Rectangle
        Dim x1% = Min(p1.X, p2.X)
        Dim x2% = Max(p1.X, p2.X)
        Dim y1% = Min(p1.Y, p2.Y)
        Dim y2% = Max(p1.Y, p2.Y)
        Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
    End Function

    Public Function GetRect(ByVal p1 As PointF, ByVal p2 As PointF) As Rectangle
        Dim x1% = Min(p1.X, p2.X)
        Dim x2% = Max(p1.X, p2.X)
        Dim y1% = Min(p1.Y, p2.Y)
        Dim y2% = Max(p1.Y, p2.Y)
        Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
    End Function

    Public Function Middle(ByVal pt1 As PointF, ByVal pt2 As PointF) As Point
        Return New Point((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2)
    End Function

    Public Function FindShortestWay(ByVal n As Integer, ByVal matrix(,) As Single, ByVal StPt As Integer, ByVal EndPt As Integer, ByRef route As List(Of Integer)) As Boolean    'matrix[0..n,0..n]
        'Dijkstra Algorythm
        Dim way(n) As Single
        Dim mark(n) As Boolean
        Dim enter(n) As Integer
        Dim Choosen As Integer
        Dim i As Integer

        For i = 0 To n
            mark(i) = False
            way(i) = Single.MaxValue
            enter(i) = -1
        Next
        way(StPt) = 0


        Do
            Choosen = -1
            For i = 0 To n
                If (Not mark(i)) And way(i) < Single.MaxValue Then
                    If Choosen = -1 Then
                        Choosen = i
                    ElseIf way(i) < way(Choosen) Then
                        Choosen = i
                    End If
                End If
            Next
            If Choosen = -1 Then Exit Do
            mark(Choosen) = True
            For i = 0 To n
                Dim len As Single = matrix(Choosen, i)
                If len < Single.MaxValue Then
                    If way(Choosen) + len < way(i) Then
                        way(i) = way(Choosen) + len
                        enter(i) = Choosen
                    End If
                End If
            Next
        Loop Until Choosen = -1

        If enter(EndPt) = -1 Then Return False

        i = EndPt
        route.Clear()
        Do
            route.Add(i)
            i = enter(i)
        Loop Until i = StPt
        route.Add(StPt)
        route.Reverse()

        Return True
    End Function

    Public Class Vector
        Public point As PointF
        Public angle As Single
        Public Length As Single

        Public Sub New()
            Me.point = New Point()
        End Sub

        Public Sub New(ByVal _x As Single, ByVal _y As Single, ByVal _an As Single, ByVal _len As Single)
            Me.point = New PointF(_x, _y)
            Me.angle = _an
            Me.Length = _len
        End Sub

        Public Sub GetDistFromPoint(ByVal _point As PointF, ByRef Answer As Single, ByRef PCoord As Single)  'Находит расстояние от точки _point до себя как отрезка, PCoord - координата ближайшей на векторе точки к _point
            Dim ang0 = AngleBy2Pt(Me.point, _point)
            Dim ang1 = ang0 - Me.angle

            Dim Len = Dist(Me.point, _point)

            PCoord = Abs(Len * Cos(ang1))
            If (Cos(ang1) >= 0 And PCoord <= Me.Length) Then
                Answer = Abs(Len * Sin(ang1))
            ElseIf PCoord < 0 Then
                PCoord = 0
                Answer = Len
            Else
                PCoord = Me.Length
                Answer = Dist(Me.EndPoint(), _point)
            End If
        End Sub

        Private Function EndPoint() As PointF
            Return New PointF(Me.point.X + Me.Length * Cos(Me.angle), Me.point.Y + Me.Length * Sin(Me.angle))
        End Function 

    End Class

    Public Function GetCross(ByVal Vec1 As Vector, ByVal Vec2 As Vector, ByRef Pnt As PointF) As Boolean
        Try
            Dim k1, k2, b1, b2, x, y As Single
            k1 = Tan(Vec1.angle)
            b1 = Vec1.point.Y - k1 * Vec1.point.X
            k2 = Tan(Vec2.angle)
            b2 = Vec2.point.Y - k2 * Vec2.point.X
            x = (b2 - b1) / (k1 - k2)
            y = k1 * x + b1
            Pnt = New Point(x, y)
            Return True
        Catch
            Return False
        End Try
         

    End Function




End Module
