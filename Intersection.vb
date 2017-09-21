Imports System.Math


'Этот класс представляет перекрёсток как объект в городе, заданный геометрическими координатами (фактически точка)
Public Class Intersection
    Public Exists As Boolean            'Существует ли
    Public x As Single                  'Горизонтальная
    Public y As Single                  'и вертикальная координаты

    Public Name As String               'Имя
    Public Number As Integer            'Номер

    'Режим проезда по дороге
    Public PassMode As Byte = 0          '0-нерегулируемый,1-гл.дорога,2-светофор
    Public MR1, MR2                      'Главная дорога
    Public TrControl As TrafficControl   'Светофор


    ' Public RoadCount As Integer         'Число дорог, исходящих из перекрёстка (вычисляемое поле)
    Public WaitingCars As Generic.List(Of Integer) = New Generic.List(Of Integer)   'Машины, ожидающие проезда
    Public MovingCars As Generic.List(Of Integer) = New Generic.List(Of Integer)    'Машины, которые проезжают по данному перекрёстку


    'Конструктор по умолчанию
    Public Sub New()
        Exists = False 
    End Sub

    'Конструктор по координатам
    Public Sub New(ByVal _x As Single, ByVal _y As Single)
        Exists = True
        x = _x
        y = _y 
    End Sub

    'Возвращает точку, задающую данный   перекрёсток
    Public Function GetPoint() As PointF
        Return New PointF(x, y)
    End Function

#Region "Graphics"
    'Выровнять координаты по сетке
    Public Sub AllignToGrid(ByVal GridStep As Integer)
        Me.x = GridStep * ((Me.x + GridStep \ 2) \ GridStep)
        Me.y = GridStep * ((Me.y + GridStep \ 2) \ GridStep)
    End Sub

    'Отметить номера подходящих дорог
    Public Sub MarkRoads(ByRef g As Graphics, ByRef CT As City)
        Dim fnt As New Font("Consolas", 15)
        For i = 1 To CT.RoadCounter
            If CT.Roads(i).Exists Then
                If CT.Roads(i).st = Me.Number Then
                    Dim pt As Point = New Point(Me.x + 3 * City.RoadWidth * Cos(CT.Roads(i).Rotat) - 10, Me.y + 3 * City.RoadWidth * Sin(CT.Roads(i).Rotat) - 10)
                    g.DrawString(CStr(i), fnt, Brushes.Red, pt)
                ElseIf CT.Roads(i).en = Me.Number Then
                    Dim pt As Point = New Point(Me.x - 3 * City.RoadWidth * Cos(CT.Roads(i).Rotat) - 10, Me.y - 3 * City.RoadWidth * Sin(CT.Roads(i).Rotat) - 10)
                    g.DrawString(CStr(i), fnt, Brushes.Red, pt)
                End If
            End If
        Next
    End Sub

#End Region

#Region "Cars"
    Public Sub RmoveCar(ByVal num As Integer) 'Удаляет машину из списков, если она там есть
        WaitingCars.Remove(num)
        MovingCars.Remove(num)
    End Sub
#End Region




#Region "Control"

    'Этот метод вызывается из машины как запрос на пересечение перекрёстка
    'В нём определяется траектория движения машины и она либо начинает движение, либо ожидает
    Public Sub RequestPass(ByVal CarNum As Integer, ByVal NewRoad As Integer, ByRef CT As City)

        'If it wants to do turn on 180 degrees, remove  it!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        If NewRoad = CT.Cars(CarNum).Road Then
            CT.RemoveCar(CarNum)
            Exit Sub
        End If

 
        '*****Determine point to move to it
        Dim ln As Integer
        Dim ps As Single
        If CT.Roads(NewRoad).en = Number Then
            ln = 1
            ps = CT.Roads(NewRoad).Length - CT.Roads(NewRoad).IntscZone2
            CT.Cars(CarNum).IST_NewPAT = CT.Roads(NewRoad).IntscZone2
        ElseIf CT.Roads(NewRoad).st = Number Then
            ln = -1
            ps = CT.Roads(NewRoad).IntscZone1
            CT.Cars(CarNum).IST_NewPAT = ps
        Else 
            ' Dim s As String = CT.Cars(CarNum).ShowRoute()
              Throw New Exception("Запрос на выезд на дорогу, на которую отсюда выехать нельзя.")
        End If
        Dim a0 As Single = CT.Roads(NewRoad).Rotat
        Dim a1 As Single = a0 - PI / 2
        Dim pst As PointF = CT.IntScs(CT.Roads(NewRoad).st).GetPoint
        Dim p0 As PointF = New PointF(pst.X + City.RoadWidth * (ln - 0.5 * Sign(ln)) * Cos(a1), pst.Y + City.RoadWidth * (ln - 0.5 * Sign(ln)) * Sin(a1))
        Dim p1 As PointF = New PointF(p0.X + ps * Cos(a0), p0.Y + ps * Sin(a0))
        '***********************************


        'Send calculated parameters to the car
        p0 = CT.Cars(CarNum).GetPos(CT)
        CT.Cars(CarNum).IST_Begin = p0
        CT.Cars(CarNum).IST_Position = 0
        CT.Cars(CarNum).MovRot = Geometry.AngleBy2Pt(p0, p1)
        CT.Cars(CarNum).IST_Len = Geometry.Dist(p0, p1)
        CT.Cars(CarNum).IST_NewLine = ln
        CT.Cars(CarNum).AtRoad = False
        '****************************


        If CanPass(CarNum, CT) Then
            MovingCars.Add(CarNum)
        Else
            WaitingCars.Add(CarNum)
        End If

    End Sub

    'Определяет, может ли сейчас машина CarNum начать пересечение перекрёстка
    Private Function CanPass(ByVal CarNum As Integer, ByRef CT As City) As Boolean

        If PassMode = 2 Then
            Return TrControl.CanPass(CT.Cars(CarNum).Road, CT.Cars(CarNum).IST_NewRoad)
        End If

        Dim p1 As PointF = CT.Cars(CarNum).IST_Begin
        Dim p2 As PointF = New PointF(p1.X + CT.Cars(CarNum).IST_Len * Cos(CT.Cars(CarNum).MovRot), p1.Y + CT.Cars(CarNum).IST_Len * Sin(CT.Cars(CarNum).MovRot))
        Dim ret As Boolean = True
        For Each i In MovingCars
            Dim p3 As PointF = CT.Cars(i).IST_Begin
            Dim p4 As PointF = New PointF(p3.X + CT.Cars(i).IST_Len * Cos(CT.Cars(i).MovRot), p3.Y + CT.Cars(i).IST_Len * Sin(CT.Cars(i).MovRot))
            If Geometry.IsCrossing(p1, p2, p3, p4) Then ret = False
            If CT.Cars(i).IST_NewRoad = CT.Cars(CarNum).IST_NewRoad And CT.Cars(i).IST_NewLine = CT.Cars(CarNum).IST_NewLine Then ret = False
        Next

        For i = 1 To CT.CarsCounter
            If CT.Cars(i).Exists And CT.Cars(i).Road = CT.Cars(CarNum).IST_NewRoad And CT.Cars(i).Line = CT.Cars(CarNum).IST_NewLine Then
                If CT.Cars(i).PosAtLine - CT.Cars(i).Length - City.SafeDist - CT.Cars(CarNum).Length < 0 Then ret = False
            End If
        Next

        Return ret
    End Function

    'Двигает все машины, пересекающие данный перекрёсток
    Public Sub Move(ByVal Time As Single, ByRef CT As City)
        Dim ToRemove As New Generic.List(Of Integer)

        For Each i As Integer In MovingCars

            Dim dist As Single = Time * CT.Cars(i).Speed
            CT.Cars(i).IST_Position += dist
            CT.Cars(i).Drag(dist)


            If CT.Cars(i).IST_Position > CT.Cars(i).IST_Len Then
                ToRemove.Add(i)
                CT.Cars(i).Road = CT.Cars(i).IST_NewRoad
                CT.Cars(i).Line = CT.Cars(i).IST_NewLine
                CT.Cars(i).PosAtLine = CT.Cars(i).IST_NewPAT
                CT.Cars(i).AtRoad = True

                CT.Cars(i).MovRot = CT.Roads(CT.Cars(i).IST_NewRoad).Rotat
                If CT.Cars(i).Line > 0 Then CT.Cars(i).MovRot += PI
                CT.Cars(i).TrueRot = CT.Cars(i).MovRot
                CT.Roads(CT.Cars(i).IST_NewRoad).Analyse_PassCarsCounter += 1


            End If

        Next

        If ToRemove.Count > 0 Then
            For Each i In ToRemove
                MovingCars.Remove(i)
            Next
            'TryStartWaitingCars(CT) Дублировано в CT раз в 0.5с
        End If

    End Sub

    Public Sub TryStartWaitingCars(ByRef CT As City)    'Попытка начать движение ожидающих машин
        Dim ToRemove As New Generic.List(Of Integer)

        If Me.PassMode = 1 Then
            For Each i In WaitingCars
                If CT.Cars(i).Road = MR1 Or CT.Cars(i).Road = MR2 Then
                    If CanPass(i, CT) Then
                        ToRemove.Add(i)
                        MovingCars.Add(i)
                    End If
                End If
            Next
            For Each i In WaitingCars
                If Not (CT.Cars(i).Road = MR1 Or CT.Cars(i).Road = MR2) Then
                    If CanPass(i, CT) Then
                        ToRemove.Add(i)
                        MovingCars.Add(i)
                    End If
                End If
            Next
        Else
            For Each i In WaitingCars
                If CanPass(i, CT) Then
                    ToRemove.Add(i)
                    MovingCars.Add(i)
                End If
            Next
        End If


        For Each i In ToRemove
            WaitingCars.Remove(i)
        Next
    End Sub


    Public Function SetDefaultControl(ByVal HalfCycle As Single, ByRef CT As City) As Boolean  'Создаёт простейший светофор
        Me.TrControl = New TrafficControl(2 * HalfCycle)
        Dim Rds(3) As Integer
        Dim Rot(3) As Single
        Dim ctr As Integer = -1
        For i = 1 To CT.RoadCounter
            If CT.Roads(i).Exists Then
                If CT.Roads(i).st = Me.Number Or CT.Roads(i).en = Me.Number Then
                    ctr += 1
                    If ctr = 4 Then Return False
                    Rds(ctr) = i
                    If CT.Roads(i).st = Me.Number Then
                        Rot(ctr) = Geometry.AngleBy2Pt(Me.GetPoint, CT.IntScs(CT.Roads(i).en).GetPoint)
                    Else
                        Rot(ctr) = Geometry.AngleBy2Pt(Me.GetPoint, CT.IntScs(CT.Roads(i).st).GetPoint)
                    End If

                End If
            End If
        Next
        If Not (ctr = 3) Then Return False
        For i = 3 To 1 Step -1
            For j = 1 To i
                If (Rot(j - 1) > Rot(j)) Then
                    Dim t = Rds(j - 1) : Rds(j - 1) = Rds(j) : Rds(j) = t
                    Dim t1 = Rot(j - 1) : Rot(j - 1) = Rot(j) : Rot(j) = t1
                End If
            Next
        Next
        TrControl.AddWay(0, HalfCycle, Rds(0), Rds(1))
        TrControl.AddWay(0, HalfCycle, Rds(0), Rds(2))
        TrControl.AddWay(0, HalfCycle, Rds(0), Rds(3))
        TrControl.AddWay(0, HalfCycle, Rds(2), Rds(0))
        TrControl.AddWay(0, HalfCycle, Rds(2), Rds(1))
        TrControl.AddWay(0, HalfCycle, Rds(2), Rds(3))
        TrControl.AddWay(HalfCycle, 2 * HalfCycle, Rds(1), Rds(0))
        TrControl.AddWay(HalfCycle, 2 * HalfCycle, Rds(1), Rds(2))
        TrControl.AddWay(HalfCycle, 2 * HalfCycle, Rds(1), Rds(3))
        TrControl.AddWay(HalfCycle, 2 * HalfCycle, Rds(3), Rds(0))
        TrControl.AddWay(HalfCycle, 2 * HalfCycle, Rds(3), Rds(1))
        TrControl.AddWay(HalfCycle, 2 * HalfCycle, Rds(3), Rds(2))

        Return True
    End Function


#End Region

#Region "Intersection.FileIO"
    'Сохраняет в файл
    Public Overrides Function ToString() As String
        Dim ret As String = x & "|" & y & "||" & Name & "|" & PassMode
        If PassMode = 1 Then
            ret &= "|" & MR1 & "|" & MR2
        ElseIf PassMode = 2 Then
            ret &= "|" & TrControl.ToString()
        End If

        Return ret
    End Function

    'Загружает из файла
    Public Sub New(ByRef StringPresentation As String, ByVal _Number As Integer)
        Dim s() As String = StringPresentation.Split("|")
        Me.x = CInt(s(0))
        Me.y = CInt(s(1))
        Me.Name = s(3)
        If s.Length > 4 Then
            Me.PassMode = s(4)
            If PassMode = 1 Then
                Me.MR1 = CInt(s(5))
                Me.MR2 = CInt(s(6))
            ElseIf PassMode = 2 Then
                Me.TrControl = New TrafficControl(s(5))
            End If
        End If
        Me.Exists = True
        Me.Number = _Number
    End Sub
#End Region
End Class
 