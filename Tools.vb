Public Class Tools

    'Это окно переключает режимы моделирования

    Private Sub Tools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Deselect()
        Select Case Editor.EdMode
            Case Editor.EditMode.AddBuilding
                Panel4.BorderStyle = BorderStyle.Fixed3D
            Case Editor.EditMode.AddCar
                Panel5.BorderStyle = BorderStyle.Fixed3D
            Case Editor.EditMode.AddIntersection
                Panel2.BorderStyle = BorderStyle.Fixed3D
            Case Editor.EditMode.AddRoad1 Or Editor.EditMode.AddRoad2
                Panel3.BorderStyle = BorderStyle.Fixed3D
            Case Editor.EditMode.None Or Editor.EditMode.MoveIntersection
                Panel1.BorderStyle = BorderStyle.Fixed3D
            Case Else
                Panel1.BorderStyle = BorderStyle.Fixed3D
        End Select
    End Sub

    Private Sub Deselect()
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel3.BorderStyle = BorderStyle.FixedSingle
        Panel4.BorderStyle = BorderStyle.FixedSingle
        Panel5.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub Panel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1.Click
        Deselect()
        Panel1.BorderStyle = BorderStyle.Fixed3D
        Editor.EdMode = Editor.EditMode.None
        Editor.CT.AddingRoad = 0
        Editor.CT.CancelAddBuilding()
    End Sub

    Private Sub Panel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel2.Click
        Deselect()
        Panel2.BorderStyle = BorderStyle.Fixed3D
        'Editor.CityBox.Cursor = Cursors.Cross
        Editor.EdMode = Editor.EditMode.AddIntersection
        Editor.CT.CancelSelection()
        Editor.CT.AddingRoad = 0
    End Sub

    Private Sub Panel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel3.Click
        Deselect()
        Panel3.BorderStyle = BorderStyle.Fixed3D
        Editor.EdMode = Editor.EditMode.AddRoad1
        Editor.CT.CancelSelection()
    End Sub

    Private Sub Panel4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel4.Click
        Deselect()
        Panel4.BorderStyle = BorderStyle.Fixed3D
        Editor.EdMode = Editor.EditMode.AddBuilding
        Editor.CT.CancelSelection()
    End Sub

    Private Sub Panel5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel5.Click
        Deselect()
        Panel5.BorderStyle = BorderStyle.Fixed3D
        Editor.EdMode = Editor.EditMode.AddCar
        Editor.CT.CancelSelection()
    End Sub

    Public Sub DisableAll()
        Panel1.Enabled = False
        Panel2.Enabled = False
        Panel3.Enabled = False
        Panel4.Enabled = False
        Panel5.Enabled = False
    End Sub


    Public Sub EnableAll()
        Panel1.Enabled = True
        Panel2.Enabled = True
        Panel3.Enabled = True
        Panel4.Enabled = True
        Panel5.Enabled = True
    End Sub

End Class