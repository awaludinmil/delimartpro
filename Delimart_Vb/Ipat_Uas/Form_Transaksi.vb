Imports System.Net
Imports Newtonsoft.Json.Linq
Public Class Form_Transaksi

    Private Sub Form_Transaksi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        

        lbl_tgl.Text = Date.Now.ToString("dd/MM/yyyy")
        Timer1.Enabled = True
        ' Hapus tombol close, minimize, maximize, dan restore
        Form_Penjualan.MdiParent = Me
        Form_Penjualan.Show()


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.lbl_jam.Text = TimeOfDay

    End Sub

    Private Sub btn_logout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_logout.Click

        Dim jawab As Integer
        jawab = MsgBox("Apakah anda ingin logout dari akun ini ?", vbQuestion + vbYesNo, "DeliMart")
        If jawab = vbYes Then
            Try

                Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/logout/" & lbl_id.Text.Trim()

                Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
                request.Method = "POST"

                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

                If response.StatusCode = HttpStatusCode.OK Then
                    MsgBox("Logout berhasil", MsgBoxStyle.Information, "DeliMart")
                    lbl_nama.Text = "..."
                    lbl_id.Text = "..."
                    Form_Login.Show()
                    Me.Close()
                Else
                    MsgBox("Logout gagal", MsgBoxStyle.Information, "DeliMart")
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        Else

        End If
    End Sub


End Class