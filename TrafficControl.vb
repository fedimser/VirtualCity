Public Class TrafficControl
    Private WayCount As Byte
    Private TimeBeg(256) As Integer, TimeEnd(256) As Integer, RoadBeg(256) As Integer, RoadEnd(256) As Integer
    Private Time As Single
    Public Cycle As Single

    Public Sub New(ByVal _Cycle As Single)
        WayCount = 0
        Cycle = _Cycle
    End Sub

    Public Sub New(ByRef StringPres As String)
        Dim s() As String = Split(StringPres, "­¬")
        WayCount = s(0)
        Cycle = s(1)
        Dim counter As Integer = 1
        For i = 1 To WayCount
            TimeBeg(i) = s(counter + 1)
            TimeEnd(i) = s(counter + 2)
            RoadBeg(i) = s(counter + 3)
            RoadEnd(i) = s(counter + 4)
            counter += 4
        Next
    End Sub

    Public Overrides Function ToString() As String
        Dim Ret As String = WayCount & "­¬" & Cycle
        For i = 1 To WayCount
            Ret &= "­¬" & TimeBeg(i)
            Ret &= "­¬" & TimeEnd(i)
            Ret &= "­¬" & RoadBeg(i)
            Ret &= "­¬" & RoadEnd(i)
        Next
        Return Ret
    End Function

    Public Sub Refresh(ByVal dt As Single)
        Time += dt
        If Time > Cycle Then Time -= Cycle
    End Sub

    Public Sub AddWay(ByVal _TimeBeg As Single, ByVal _TimeEnd As Single, ByVal _RoadBeg As Integer, ByVal _RoadEnd As Integer)
        WayCount += 1
        TimeBeg(WayCount) = _TimeBeg
        TimeEnd(WayCount) = _TimeEnd
        RoadBeg(WayCount) = _RoadBeg
        RoadEnd(WayCount) = _RoadEnd

    End Sub

    Public Sub RemoveWay(ByVal num As Integer)
        WayCount -= 1
        For i = num To WayCount
            TimeBeg(i) = TimeBeg(i + 1)
            TimeEnd(i) = TimeEnd(i + 1)
            RoadBeg(i) = RoadBeg(i + 1)
            RoadEnd(i) = RoadEnd(i + 1)
        Next
    End Sub

    Public Function CanPass(ByVal _RoadBeg As Integer, ByVal _RoadEnd As Integer) As Boolean
        For i = 1 To WayCount
            If (Time >= TimeBeg(i)) And (Time < TimeEnd(i)) Then
                If RoadBeg(i) = _RoadBeg And RoadEnd(i) = _RoadEnd Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Public Sub Reset()
        Time = 0
    End Sub

    Public Sub ExportMap(ByRef Lb As ListBox)
        Lb.Items.Clear()
        For i = 1 To WayCount
            Lb.Items.Add(TimeBeg(i) & "-" & TimeEnd(i) & " " & vbTab & RoadBeg(i) & "->" & RoadEnd(i))
        Next
    End Sub

    Public Sub CheckExist()
        'Does Nothing
    End Sub
End Class
