<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AlertasV10
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnConfigurarEmpresa = New System.Windows.Forms.Button()
        Me.btnAbrirempresa = New System.Windows.Forms.Button()
        Me.btnCrearalerta = New System.Windows.Forms.Button()
        Me.btnListaralertas = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.lstUsuarios = New System.Windows.Forms.ListBox()
        Me.btnCargarUsuarios = New System.Windows.Forms.Button()
        Me.txbproyecto = New System.Windows.Forms.TextBox()
        Me.txbarticulo = New System.Windows.Forms.TextBox()
        Me.btnAñadirART = New System.Windows.Forms.Button()
        Me.btnBorrarART = New System.Windows.Forms.Button()
        Me.lstArticulos = New System.Windows.Forms.ListBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DirectoryEntry1 = New System.DirectoryServices.DirectoryEntry()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.btnCrearAlertas = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnConfigurarEmpresa
        '
        Me.btnConfigurarEmpresa.Location = New System.Drawing.Point(123, 148)
        Me.btnConfigurarEmpresa.Name = "btnConfigurarEmpresa"
        Me.btnConfigurarEmpresa.Size = New System.Drawing.Size(106, 61)
        Me.btnConfigurarEmpresa.TabIndex = 0
        Me.btnConfigurarEmpresa.Text = "Configurar empresa"
        Me.btnConfigurarEmpresa.UseVisualStyleBackColor = True
        '
        'btnAbrirempresa
        '
        Me.btnAbrirempresa.Location = New System.Drawing.Point(46, 37)
        Me.btnAbrirempresa.Name = "btnAbrirempresa"
        Me.btnAbrirempresa.Size = New System.Drawing.Size(106, 60)
        Me.btnAbrirempresa.TabIndex = 1
        Me.btnAbrirempresa.Text = "Abrir Empresa"
        Me.btnAbrirempresa.UseVisualStyleBackColor = True
        '
        'btnCrearalerta
        '
        Me.btnCrearalerta.Location = New System.Drawing.Point(210, 37)
        Me.btnCrearalerta.Name = "btnCrearalerta"
        Me.btnCrearalerta.Size = New System.Drawing.Size(105, 59)
        Me.btnCrearalerta.TabIndex = 2
        Me.btnCrearalerta.Text = "Crear Alerta"
        Me.btnCrearalerta.UseVisualStyleBackColor = True
        '
        'btnListaralertas
        '
        Me.btnListaralertas.Location = New System.Drawing.Point(358, 37)
        Me.btnListaralertas.Name = "btnListaralertas"
        Me.btnListaralertas.Size = New System.Drawing.Size(102, 58)
        Me.btnListaralertas.TabIndex = 3
        Me.btnListaralertas.Text = "Listar Alertas"
        Me.btnListaralertas.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(490, 37)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 57)
        Me.btnCancelar.TabIndex = 4
        Me.btnCancelar.Text = "Cerrar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'lstUsuarios
        '
        Me.lstUsuarios.FormattingEnabled = True
        Me.lstUsuarios.Location = New System.Drawing.Point(43, 140)
        Me.lstUsuarios.Name = "lstUsuarios"
        Me.lstUsuarios.Size = New System.Drawing.Size(203, 303)
        Me.lstUsuarios.TabIndex = 5
        '
        'btnCargarUsuarios
        '
        Me.btnCargarUsuarios.Location = New System.Drawing.Point(43, 459)
        Me.btnCargarUsuarios.Name = "btnCargarUsuarios"
        Me.btnCargarUsuarios.Size = New System.Drawing.Size(203, 23)
        Me.btnCargarUsuarios.TabIndex = 6
        Me.btnCargarUsuarios.Text = "Cargar Usuarios"
        Me.btnCargarUsuarios.UseVisualStyleBackColor = True
        '
        'txbproyecto
        '
        Me.txbproyecto.Location = New System.Drawing.Point(370, 140)
        Me.txbproyecto.Name = "txbproyecto"
        Me.txbproyecto.Size = New System.Drawing.Size(195, 20)
        Me.txbproyecto.TabIndex = 7
        '
        'txbarticulo
        '
        Me.txbarticulo.Location = New System.Drawing.Point(371, 187)
        Me.txbarticulo.Name = "txbarticulo"
        Me.txbarticulo.Size = New System.Drawing.Size(195, 20)
        Me.txbarticulo.TabIndex = 8
        '
        'btnAñadirART
        '
        Me.btnAñadirART.Location = New System.Drawing.Point(370, 238)
        Me.btnAñadirART.Name = "btnAñadirART"
        Me.btnAñadirART.Size = New System.Drawing.Size(75, 23)
        Me.btnAñadirART.TabIndex = 9
        Me.btnAñadirART.Text = "Añadir"
        Me.btnAñadirART.UseVisualStyleBackColor = True
        '
        'btnBorrarART
        '
        Me.btnBorrarART.Location = New System.Drawing.Point(490, 238)
        Me.btnBorrarART.Name = "btnBorrarART"
        Me.btnBorrarART.Size = New System.Drawing.Size(75, 23)
        Me.btnBorrarART.TabIndex = 10
        Me.btnBorrarART.Text = "Eliminar"
        Me.btnBorrarART.UseVisualStyleBackColor = True
        '
        'lstArticulos
        '
        Me.lstArticulos.FormattingEnabled = True
        Me.lstArticulos.Location = New System.Drawing.Point(371, 283)
        Me.lstArticulos.Name = "lstArticulos"
        Me.lstArticulos.Size = New System.Drawing.Size(199, 160)
        Me.lstArticulos.TabIndex = 11
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(46, 527)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 12
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(370, 527)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker2.TabIndex = 13
        '
        'btnCrearAlertas
        '
        Me.btnCrearAlertas.Location = New System.Drawing.Point(170, 594)
        Me.btnCrearAlertas.Name = "btnCrearAlertas"
        Me.btnCrearAlertas.Size = New System.Drawing.Size(345, 23)
        Me.btnCrearAlertas.TabIndex = 14
        Me.btnCrearAlertas.Text = "Crear alerta"
        Me.btnCrearAlertas.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(586, 140)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 23)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(586, 187)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(35, 23)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'AlertasV10
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 658)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnCrearAlertas)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.lstArticulos)
        Me.Controls.Add(Me.btnBorrarART)
        Me.Controls.Add(Me.btnAñadirART)
        Me.Controls.Add(Me.txbarticulo)
        Me.Controls.Add(Me.txbproyecto)
        Me.Controls.Add(Me.btnCargarUsuarios)
        Me.Controls.Add(Me.lstUsuarios)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnListaralertas)
        Me.Controls.Add(Me.btnCrearalerta)
        Me.Controls.Add(Me.btnAbrirempresa)
        Me.Controls.Add(Me.btnConfigurarEmpresa)
        Me.Name = "AlertasV10"
        Me.ShowInTaskbar = False
        Me.Text = "AlertasV10"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnConfigurarEmpresa As Button
    Friend WithEvents btnAbrirempresa As Button
    Friend WithEvents btnCrearalerta As Button
    Friend WithEvents btnListaralertas As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents lstUsuarios As ListBox
    Friend WithEvents btnCargarUsuarios As Button
    Friend WithEvents txbproyecto As TextBox
    Friend WithEvents txbarticulo As TextBox
    Friend WithEvents btnAñadirART As Button
    Friend WithEvents btnBorrarART As Button
    Friend WithEvents lstArticulos As ListBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents DirectoryEntry1 As DirectoryServices.DirectoryEntry
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents btnCrearAlertas As Button

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Private Sub btnCrearalerta_Click(sender As Object, e As EventArgs) Handles btnCrearalerta.Click

    End Sub

    Private Sub MenuStrip1_ItemClicked_1(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button