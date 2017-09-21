<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tools
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.VirtualCity.My.Resources.Resources.menu1
        Me.Panel1.Location = New System.Drawing.Point(5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(40, 40)
        Me.Panel1.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.Panel1, "Редагування")
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.VirtualCity.My.Resources.Resources.menu2
        Me.Panel2.Location = New System.Drawing.Point(50, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(40, 40)
        Me.Panel2.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.Panel2, "Додати перехрестя")
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.VirtualCity.My.Resources.Resources.menu3
        Me.Panel3.Location = New System.Drawing.Point(95, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(40, 40)
        Me.Panel3.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.Panel3, "Додати дорогу")
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.VirtualCity.My.Resources.Resources.menu4
        Me.Panel4.Location = New System.Drawing.Point(5, 53)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(40, 40)
        Me.Panel4.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.Panel4, "Добавить здание")
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.VirtualCity.My.Resources.Resources.menu5
        Me.Panel5.Location = New System.Drawing.Point(50, 53)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(40, 40)
        Me.Panel5.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.Panel5, "Додати автомобіль")
        '
        'Tools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(140, 101)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Tools"
        Me.ShowInTaskbar = False
        Me.Text = "Інструменти"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
