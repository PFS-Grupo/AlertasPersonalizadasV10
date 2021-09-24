Imports ErpBE100
Imports ErpBS100
Imports StdBE100
Imports System
Imports System.IO
Imports StdPlatBE100
Imports StdPlatBS100
Public Class AbrirEmpresa
    Public Shared motor As New ErpBS 'Acceso a datos (Artigo, Cliente)
    Public Shared objLista As New StdPlatBS
    Public Shared objConfApl As New StdBSConfApl


    Public Shared Function abrirMotoryEmpresa(EMPRESA As String, USUARIO As String, PASS As String) As Boolean
        Try

            motor.AbreEmpresaTrabalho(, EMPRESA, USUARIO, PASS)

            objConfApl.AbvtApl = "ERP"
            objConfApl.Instancia = "DEFAULT"
            objConfApl.PwdUtilizador = PASS
            objConfApl.Utilizador = USUARIO
            objConfApl.LicVersaoMinima = "9.00"
            objLista.AbrePlataformaEmpresa(EMPRESA, Nothing, objConfApl, EnumTipoPlataforma.tpProfissional, Nothing)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Sub cerrarMotoryEmpresa()
        Try
            motor.FechaEmpresaTrabalho()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


End Class
