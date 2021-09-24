Public Class Form1
    Imports System.Globalization
    Imports System.Text
    Imports Interop.GcpBE900
    Imports Interop.StdBE900
    Imports Interop.StdPlatBE900
    Imports Interop.StdPlatBS900

    Public Class Form1

        Private empresaAbierta As Boolean = False
        Private mensajeBorrando As New StringBuilder

        Private TotalRegistros As Integer

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'clsERP.abrirMotoryEmpresa()
        End Sub

        Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
            clsERP.cerrarMotoryEmpresa()
        End Sub

        Private Sub btnConfiguraEmpresa_Click(sender As Object, e As EventArgs) Handles btnConfiguraEmpresa.Click
            Dim formu As New frmSettings
            formu.ShowDialog()
        End Sub

        Private Sub bDatosTransportista_Click(sender As Object, e As EventArgs) Handles btnAbrirempresa.Click
            If clsERP.abrirMotoryEmpresa(My.Settings.empresa, My.Settings.usuario, My.Settings.pass) Then
                MsgBox("Empresa Abierta")
                empresaAbierta = True
                Label2.Text = "Empresa abierta: " & clsERP.motor.Contexto.CodEmp
            Else
                MsgBox("Error al abrir la empresa")
                empresaAbierta = False
            End If
        End Sub

        Private Sub btnCargarFechas_Click(sender As Object, e As EventArgs) Handles btnCargarAlertas.Click
            pnlAlerta.Visible = False
            pnlListaAlertas.Visible = True
            If empresaAbierta Then
                CargarUtilizadores(ComboBox1)
                CargarListaAlertas(lstAlertas, ComboBox1.Text)
            End If

        End Sub

        Private Sub CargarListaAlertas(listaV As ListView, usuario As String)
            Dim consulta As String = ""
            Dim lista As New StdBELista
            'Cargamos las alertas que empiezen por AP - que son las que se crean desde el desarrollo
            'consulta = "select al.nome [NOMBRE],cal.DurDataInicio [FECHAINICIO],DurDataFim  [FECHAFIN],al.BatchSQL [CONSULTA], al.Id [ID] from AlertasUtilizador AL INNER JOIN CalendariosGerais CAL ON cal.Id=al.Id where left(nome,4)='AP -'"

            consulta = "Select al.nome [NOMBRE],cal.DurDataInicio [FECHAINICIO],DurDataFim  [FECHAFIN],al.BatchSQL [CONSULTA], al.Id [ID], au.Utilizador" _
            & " From AlertasUtilizador AL " _
            & " INNER Join CalendariosGerais CAL ON cal.Id=al.Id  " _
            & " INNER Join AlertasUtilizadorUtilizadores au ON al.Id=au.IdAlerta " _
            & " where Left(nome, 4) ='AP -'"

            If usuario <> "" Then
                consulta += " and au.utilizador='" & usuario & "'"
            End If


            listaV.Items.Clear()

            lista = clsERP.motor.Consulta(consulta)
            If Not lista.Vazia Then
                While Not lista.NoFim
                    Dim fila As New ListViewItem
                    Dim fechaI As Date = Convert.ToDateTime(lista.Valor("FECHAINICIO"))
                    Dim fechaF As Date = Convert.ToDateTime(lista.Valor("FECHAFIN"))
                    fila.Text = lista.Valor("NOMBRE").ToString
                    fila.SubItems.Add(fechaI.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                    fila.SubItems.Add(fechaF.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                    fila.SubItems.Add(lista.Valor("CONSULTA").ToString)
                    fila.SubItems.Add(lista.Valor("ID").ToString)
                    listaV.Items.Add(fila)
                    lista.Seguinte()
                End While
            End If

        End Sub

        Private Sub CargarUtilizadores(listaU As ListBox)
            Dim lista As New StdBELista

            listaU.Items.Clear()
            lista = clsERP.motor.Consulta("select codigo from priempre.dbo.Utilizadores")
            If Not lista.Vazia Then
                While Not lista.NoFim
                    listaU.Items.Add(lista.Valor("codigo").ToString)
                    lista.Seguinte()
                End While
            End If
        End Sub

        Private Sub AnadirArticulo(lista As ListBox)
            lista.Items.Add(tbxArticulo.Text)
        End Sub

        Private Sub CargarUtilizadores(combo As ComboBox)
            Dim lista As New StdBELista

            combo.Items.Clear()
            lista = clsERP.motor.Consulta("select codigo from priempre.dbo.Utilizadores")
            If Not lista.Vazia Then
                While Not lista.NoFim
                    combo.Items.Add(lista.Valor("codigo").ToString)
                    lista.Seguinte()
                End While
            End If
        End Sub


        Private Sub bCancelar_Click(sender As Object, e As EventArgs) Handles bCancelar.Click
            Close()
        End Sub

        Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs)
            mensajeBorrando = New StringBuilder
            If empresaAbierta Then

                If MsgBox("¿Desea realmente borrar todos los datos? Esta acción es irreversible...", vbYesNo) = vbYes Then

                End If
            Else
                MsgBox("Debe abrir primero la empresa")
            End If

        End Sub



        Private Function DevuelveIdProyecto(proyecto As String) As String
            Dim idproyecto As String = ""

            If clsERP.motor.Comercial.Projectos.Existe(proyecto) Then
                Return clsERP.motor.Comercial.Projectos.Edita(proyecto).ID.ToString
            Else
                Return ""
            End If

        End Function

        Private Function ExisteArticulo(articulo As String) As Boolean
            Return clsERP.motor.Comercial.Artigos.Existe(articulo)
        End Function

        Private Sub CrearAlertasUtilizador(lista As ListBox, idalerta As String)
            'Hacemos tantos insert como utilizadores se hayan marcado en la lista
            Try
                Dim consulta As String = ""
                clsERP.motor.IniciaTransaccao()

                For Each item In lista.SelectedItems
                    consulta = " INSERT INTO [dbo].[AlertasUtilizadorUtilizadores]" _
                   & "([IdAlerta],[Utilizador],[Perfil],[Activo],[MonitorNegocios])" _
                   & "VALUES (" _
                   & "'" & idalerta & "'" _ '(<IdAlerta, uniqueidentifier,>
                   & ",'" & item.ToString & "'" _ '<Utilizador, varchar(20),>
                   & ",'---'" _ '<Perfil, varchar(15),>
                   & ",1" _ '<Activo, bit,>
                   & ",0)" '<MonitorNegocios, bit,>)

                    clsERP.motor.DSO.BDAPL.Execute(consulta)
                Next
                clsERP.motor.TerminaTransaccao()
            Catch ex As Exception
                clsERP.motor.DesfazTransaccao()
                MsgBox(ex.Message)
            End Try
        End Sub

        Private Function CreaCategoriaAlerta(idalerta As String) As Boolean
            Try

                Dim consulta As String = ""
                consulta = "INSERT INTO [dbo].[AlertasUtilizadorCategorias] ([IdAlerta] ,[Categoria])  VALUES" _
            & "('" & idalerta & "','Alertas Automáticas')"
                clsERP.motor.DSO.BDAPL.Execute(consulta)
                Return True
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
        End Function

        Private Function CreaCalendarioAlerta(idAlerta As String, fechainicia As Date, fechafin As Date) As String
            Dim consulta As String = ""
            Dim idCalendario As String
            Dim HoraInicioAlerta As String = "1900/01/01 08:00:00"
            Dim ProximoInicio As String
            Dim InicioActual As String

            Try
                idCalendario = Guid.NewGuid.ToString

                ProximoInicio = DateAdd(DateInterval.Day, 1, Now).ToString("yyyy/MM/dd 08:00:00", CultureInfo.InvariantCulture)
                InicioActual = Now.ToString("yyyy/MM/dd 08:00:00", CultureInfo.InvariantCulture)

                consulta = "INSERT INTO [dbo].[CalendariosGerais]" _
               & "([Id],[Activa],[Periodo],[FreqUnicaHora],[FreqRecQdePeriodo],[FreqRecPeriodo],[FreqRecHoraInicio],[FreqRecHoraFim]" _
               & ",[DurDataInicio],[DurComDataFim],[DurDataFim],[PrDiarioDias],[PrSemanalSemana],[PrSemanalSegunda],[PrSemanalTerca],[PrSemanalQuarta]" _
               & ",[PrSemanalQuinta],[PrSemanalSexta],[PrSemanalSabado],[PrSemanalDomingo],[PrMensalTipo],[PrMensalDia],[PrMensalMeses],[PrMensalNumDia]" _
               & ",[PrMensalDiaSemana],[PrMensalMes],[ProximaOcorrencia],[UltimaOcorrencia],[RecuperaExecucoes])" _
               & " VALUES" _
               & "('" & idAlerta & "',1,0,'" & HoraInicioAlerta & "',0,0,null,null,'" & fechainicia.ToString("yyyy/MM/dd 00:00:00", CultureInfo.InvariantCulture) & "',1,'" & fechafin.ToString("yyyy/MM/dd 00:00:00", CultureInfo.InvariantCulture) & "',1," _
               & "0,0,0,0,0,0,0,0,0,0,0,0,0,0,'" & ProximoInicio & "','" & InicioActual & "',0)"


                clsERP.motor.DSO.BDAPL.Execute(consulta)

                Return idCalendario
            Catch ex As Exception
                MsgBox(ex.Message)
                Return ""
            End Try

        End Function


        Private Function CrearAlertaUsuario(listaArticulos As ListBox, proyecto As String, idCalendario As String) As String

            Dim IdAlerta As String = ""
            Try

                ' Dim configuracion As String = "<Configuration><Results OnlyLastExecution=""0"" AlertType=""1"" ResultsAlert=""-1""></Results><Post-Execution><Notification Email=""0"" SMS=""0""></Notification><Publish Type=""0"" Format=""0"" Zip=""0"" Path="""" Filename="""" Suffix=""0"" Port=""0"" URL="""" Username="""" Password=""""></Publish></Post-Execution><Execution UserIndependent=""0"" AllowsManualExec=""1"" MinExecBreak=""5""></Execution></Configuration>  "
                Dim configuracion As String = "<Configuration><Results OnlyLastExecution=""0"" AlertType=""1"" ResultsAlert=""-1""></Results><Post-Execution><Notification Email=""-1"" SMS=""0""><Email Type=""0"" Format=""0"" Zip=""False"" TopNActive=""False"" TopN=""0""></Email><Email></Email></Notification><Publish Type=""0"" Format=""0"" Zip=""0"" Path="""" Filename="""" Suffix=""0"" Port=""0"" URL="""" Username="""" Password=""""></Publish></Post-Execution><Execution UserIndependent=""0"" AllowsManualExec=""1"" MinExecBreak=""0""></Execution></Configuration>"
                Dim consulta As String = ""
                Dim consultaAlerta As String = ""
                Dim listadoArticulos As String = ""
                For i = 0 To listaArticulos.Items.Count - 1
                    If i = listaArticulos.Items.Count - 1 Then
                        listadoArticulos += "''" & listaArticulos.Items(i).ToString & "''"
                    Else
                        listadoArticulos += "''" & listaArticulos.Items(i).ToString & "'',"
                    End If
                Next


                consultaAlerta = "select LC.artigo,lc.descricao,lc.Quantidade from LinhasCompras LC inner join cabeccompras CC on CC.id=LC.IdCabecCompras where LC.artigo IN (" & listadoArticulos & ") and LC.obraid=''" & proyecto & "'' and LC.datadoc BETWEEN ''" & dtpFechaInicio.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) & "'' and ''" & dtpFechaFin.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) & "'' and CC.tipodoc=''ALB''"


                IdAlerta = Guid.NewGuid.ToString

                CreaCalendarioAlerta(IdAlerta, dtpFechaInicio.Value, dtpFechaFin.Value) 'se crea el calendario con la misma id que la alerta

                If CreaCategoriaAlerta(IdAlerta) Then 'creamos la categoria de la alerta


                    consulta = "INSERT INTO [dbo].[AlertasUtilizador] " _
           & "([Id]" _
           & ",[Nome]" _
           & ",[Descricao] " _
           & ",[Tipo]" _
           & ",[TipoNotificacoes]" _
           & ",[Activo]" _
           & ",[UltimaExecucao]" _
           & ",[ProximaExecucao]" _
           & ",[Calendario]" _
           & ",[CategoriaLista]" _
           & ",[IdLista]" _
           & ",[BatchSQL]" _
           & ",[Sistema]" _
           & ",[TipoPublicacao]" _
           & ",[ExecutaAccao]" _
           & ",[Disponibilidade]" _
           & ",[Prioridade]" _
           & ",[MonitorNegocios]" _
           & ",[DataCriacao]" _
           & ",[Autor]" _
           & ",[Configuracao]" _
           & ",[Validade])" _
           & "VALUES " _
           & "('" & IdAlerta & "'" _ '<Id, uniqueidentifier,>" 
           & ",'" & "AP - Alerta Personalizada: " & tbxProyecto.Text & " [" & Now.ToLongTimeString & "]'" _ '<Nome, varchar(100),>" _
           & ",'" & "Alerta Generada automaticamente desde el desarrollo externo" & "'" _ '<Descricao, varchar(1000),>" _
           & ",1" _ '<Tipo, tinyint,>" _
           & ",1" _ '<TipoNotificacoes, tinyint,>" _
           & ",1" _ '<Activo, bit,>" _
           & ",null" _ '<UltimaExecucao, datetime,>" _
           & ",'" & DateAdd(DateInterval.Day, 1, Now).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) & "'" _ '<ProximaExecucao, datetime,>" _
           & ",'" & "87CADEDE-14CE-4157-8029-FF4CDEE0EFBD" & "'" _ '<Calendario, uniqueidentifier,>" _
           & ",null" _ '<CategoriaLista, varchar(50),>" _
           & ",null" _ '<IdLista, uniqueidentifier,>" _
           & ",'" & Replace(consultaAlerta, """", "'") & "'" _ '<BatchSQL, varchar(2000),>" _
           & ",0" _ '<Sistema, bit,>" _
           & ",0" _ '<TipoPublicacao, tinyint,>" _
           & ",0" _ '<ExecutaAccao, bit,>" _
           & ",1" _ '<Disponibilidade, bit,>" _
           & ",1" _ '<Prioridade, tinyint,>" _
           & ",0" _ '<MonitorNegocios, bit,>" _
           & ",'" & Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) & "'" _ '<DataCriacao, datetime,>" _
           & ",'Automatico'" _ '<Autor, varchar(20),>" _
           & ",'" & configuracion & "'" _ '<Configuracao, text,>" _
           & ",30)" '<Validade, int,>)"

                    clsERP.motor.DSO.BDAPL.Execute(consulta)
                    Return IdAlerta
                Else
                    Return ""
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
                Return ""
            End Try

        End Function

        Private Sub btnCrearAlerta_Click(sender As Object, e As EventArgs) Handles btnCrearAlerta.Click
            If Not empresaAbierta Then
                MsgBox("Debe abrir primero la empresa")
                Exit Sub
            End If



            Dim cadenaErrores As New StringBuilder
            Dim idalerta As String = ""
            Dim idCalendario As String = ""

            Try
                'If Not ExisteArticulo(tbxArticulo.Text) Then cadenaErrores.AppendLine("Articulo no encontrado")
                If DevuelveIdProyecto(tbxProyecto.Text) = "" Then cadenaErrores.AppendLine("Proyecto no encontrado")
                If lstUsuarios.SelectedItems.Count = 0 Then cadenaErrores.AppendLine("Debe seleccionar al menos un usuario para la alerta")

                If Len(cadenaErrores.ToString) <> 0 Then
                    MsgBox("Se han encontrado los siguientes errorres:" & vbNewLine & cadenaErrores.ToString)
                Else
                    idalerta = CrearAlertaUsuario(lstArticulos, DevuelveIdProyecto(tbxProyecto.Text), idCalendario)
                    If idalerta <> "" Then
                        CrearAlertasUtilizador(lstUsuarios, idalerta)
                        MsgBox("Alerta creada correctamente")
                    End If
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCargarUsuarios.Click
            If empresaAbierta Then
                CargarUtilizadores(lstUsuarios)
            Else
                MsgBox("Debe abrir primero la empresa")
            End If
        End Sub

        Private Sub btnVerAlerta_Click(sender As Object, e As EventArgs) Handles btnVerAlerta.Click
            pnlAlerta.Visible = True
            pnlListaAlertas.Visible = False
        End Sub

        Private Sub lstAlertas_DoubleClick(sender As Object, e As EventArgs) Handles lstAlertas.DoubleClick
            If lstAlertas.SelectedItems.Count > 0 Then
                Dim consulta As String = ""
                Dim lista As New StdBELista
                Dim cadena As New StringBuilder

                consulta = "Select UTILIZADOR FROM AlertasUtilizadorUtilizadores WHERE IDALERTA='" & lstAlertas.SelectedItems(0).SubItems(4).Text & "'"
                cadena.AppendLine("CONSULTA SQL UTILIZADA EN LA ALERTA:")
                cadena.AppendLine("------------------------------------")
                cadena.AppendLine(lstAlertas.SelectedItems(0).SubItems(3).Text)

                cadena.AppendLine("LISTA DE USUARIOS ASIGNADOS A LA ALERTA:")
                cadena.AppendLine("----------------------------------------")
                lista = clsERP.motor.Consulta(consulta)
                If Not lista.Vazia Then
                    While Not lista.NoFim
                        cadena.AppendLine(lista.Valor("utilizador").ToString)
                        lista.Seguinte()
                    End While
                End If

                MsgBox(cadena.ToString)
            End If
        End Sub

        Private Sub btnEliminarAlerta_Click(sender As Object, e As EventArgs) Handles btnEliminarAlerta.Click
            If empresaAbierta Then
                If lstAlertas.SelectedItems.Count > 0 Then
                    If MsgBox("¿Desea realmente elininar la alerta?. Esta acción no podrá deshacerse", MsgBoxStyle.YesNo, "Mensaje del sistema") = MsgBoxResult.Yes Then
                        EliminarAlerta(lstAlertas.SelectedItems(0).SubItems(4).Text)
                        CargarListaAlertas(lstAlertas, ComboBox1.Text)
                    End If

                End If
            Else
                MsgBox("Debe abrir primero la empresa")
            End If

        End Sub

        Private Sub EliminarAlerta(idAlerta As String)
            Dim consulta As String = ""
            Try

                clsERP.motor.IniciaTransaccao()

                'AlertasUtilizadorLog
                consulta = "delete from AlertasUtilizadorLog where idalerta='" & idAlerta & "'"
                clsERP.motor.DSO.BDAPL.Execute(consulta)

                '--AlertasUtilizadorUtilizadores
                consulta = "delete from AlertasUtilizadorUtilizadores where idalerta='" & idAlerta & "'"
                clsERP.motor.DSO.BDAPL.Execute(consulta)

                '--AlertasUtilizador
                consulta = "delete from AlertasUtilizador where id='" & idAlerta & "'"
                clsERP.motor.DSO.BDAPL.Execute(consulta)

                '--AlertasUtilzadorCategorias
                consulta = "delete from AlertasUtilizadorCategorias where idalerta='" & idAlerta & "'"
                clsERP.motor.DSO.BDAPL.Execute(consulta)

                '--CalendarioGerais
                consulta = "delete from CalendariosGerais where id='" & idAlerta & "'"
                clsERP.motor.DSO.BDAPL.Execute(consulta)

                clsERP.motor.TerminaTransaccao()

                MsgBox("Alerta eliminada corectamente")
            Catch ex As Exception
                MsgBox(ex.Message)
                clsERP.motor.DesfazTransaccao()
            End Try
        End Sub

        Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
            tbxProyecto.Text = CStr(clsERP.objLista.Listas.GetF4SQL("Proyectos", "SELECT codigo,descricao from COP_OBRAS", "codigo"))
        End Sub

        Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
            If ComboBox1.SelectedIndex <> -1 Then
                CargarListaAlertas(lstAlertas, ComboBox1.Text)
            End If
        End Sub

        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnBorrarCaducadas.Click
            If empresaAbierta Then


                If MsgBox("¿Desea realmente eliminar las alertas caducadas?. Esta acción no podrá deshacerse", MsgBoxStyle.YesNo, "Mensaje del sistema") = MsgBoxResult.Yes Then
                    For Each linea As ListViewItem In lstAlertas.Items

                        If linea.SubItems(2).Text < Now Then
                            EliminarAlerta(linea.SubItems(4).Text)
                        End If
                    Next


                    CargarListaAlertas(lstAlertas, ComboBox1.Text)
                End If


            Else
                MsgBox("Debe abrir primero la empresa")
            End If

        End Sub

        Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles btnAnadirART.Click
            If Not ExisteArticulo(tbxArticulo.Text) Then
                MsgBox("Articulo no encontrado")
            Else
                AnadirArticulo(lstArticulos)
                tbxArticulo.Text = ""
            End If

        End Sub

        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            tbxArticulo.Text = CStr(clsERP.objLista.Listas.GetF4SQL("Articulos", "SELECT artigo,descricao from artigo", "artigo"))
        End Sub

        Private Sub btnBorrarART_Click(sender As Object, e As EventArgs) Handles btnBorrarART.Click
            If lstArticulos.SelectedItems.Count > 0 Then
                lstArticulos.Items.RemoveAt(lstArticulos.SelectedIndex)
            End If
        End Sub


        'Dim alertas As New StdBSADU
        'MsgBox(alertas.Existe("8A47262D-9144-11EB-9F4C-34689542FAC0"))

        'Dim alerta As New StdBEADU
        'With alerta
        '    .Id = Guid.NewGuid.ToString
        '    .Nome = "Alerta Automatica"
        '    .Descricao = "Descipcion alerta automatica"
        '    .Tipo = 1
        '    .TipoNotificacoes = 1
        '    .Activo = 1
        '    .ProximaExecucao = DateAdd(DateInterval.Day, 1, Now)
        '    .Calendario = "8A47262D-9144-11EB-9F4C-34689542FAC0"
        '    .BatchSQL = "select * from artigo"
        '    .Sistema = 0
        '    .TipoPublicacao = 0
        '    .ExecutaAccao = 0
        '    .Disponibilidade = 1
        '    .Prioridade = 1
        '    '.monitordenegocio=0
        '    .DataCriacao = Now
        '    .Autor = "Automatico"
        '    '            .Configuracoes = "<Configuration><Results OnlyLastExecution="0" AlertType="1" ResultsAlert="-1"></Results><Post-Execution><Notification Email="0" SMS="0"></Notification><Publish Type="0" Format="0" Zip="0" Path="" Filename="" Suffix="0" Port="0" URL="" Username="" Password=""></Publish></Post-Execution><Execution UserIndependent="0" AllowsManualExec="0" MinExecBreak="5"></Execution></Configuration>  "
        '    .Validade = 30
        'End With




    End Class
End Class
