Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class Form_Dialog_Penerimaan

    Private Sub btn_hapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_hapus.Click
        'delete with golang api
        Dim kd_transaksi As String = txt_kode_transaksi.Text
        Dim jawab As Integer

        jawab = MsgBox("Apakah Anda yakin ingin membatalkan transaksi ini?", vbQuestion + vbYesNo, "Konfirmasi Pembatalan")
        If jawab = vbYes Then
            Try
                'Form_Login.SetSecurityProtocol()
                Dim request As HttpWebRequest = CType(WebRequest.Create("https://praktikum-cpanel-unbin.com/golang_api/delimart_api/penerimaan/" & kd_transaksi), HttpWebRequest)
                request.Method = "DELETE"

                Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                    If response.StatusCode = HttpStatusCode.OK Then
                        MsgBox("Data Berhasil Dihapus")
                        Form_Penerimaan.CallAfterUpdate()
                        Me.Close()
                    Else
                        Using reader As New StreamReader(response.GetResponseStream())
                            Dim responseString As String = reader.ReadToEnd()
                            MsgBox("Error: " & responseString)
                        End Using
                    End If
                End Using
            Catch ex As WebException
                MsgBox("Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btn_ubah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ubah.Click
        Dim kd_transaksi As Integer = txt_kode_transaksi.Text.Trim()
        Dim jmlh_brg As Integer = txt_jumlah_barang_masuk.Text.Trim()
        If jmlh_brg < 1 Then
            MsgBox("Minimal barang masuk 1")
            txt_jumlah_barang_masuk.Text = "1"
            'jmlh_brg = 1
        Else
            'buka()
            'update = "update penerimaan set jumlah_barang_terima='" & jmlh_brg & "' where kd_transaksi_terima='" & kd_transaksi & "'"
            'kon.Execute(update)
            'tutup()
            Try
                ''Form_Login.SetSecurityProtocol()
               

                Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/penerimaan/" & kd_transaksi
                Dim requestData As New JObject()


                requestData.Add("jumlah_barang_terima", jmlh_brg)

                Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
                request.Method = "PUT"
                request.ContentType = "application/json"

                Using streamWriter As New StreamWriter(request.GetRequestStream())
                    streamWriter.Write(requestData.ToString())
                    streamWriter.Flush()
                    streamWriter.Close()
                End Using

                Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                    If response.StatusCode = HttpStatusCode.OK Then
                        MsgBox("Data Penerimaan berhasil disimpan", MsgBoxStyle.Information, "Sukses")
                        ' Call any necessary update or refresh methods here
                        Form_Penerimaan.CallAfterUpdate()

                    Else
                        MsgBox("Gagal menyimpan data Penerimaan. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
                    End If
                End Using
            Catch ex As Exception
                MsgBox("Error CreateOrUpdatePenerimaan: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
            Form_Penerimaan.CallAfterUpdate()
            Me.Close()
        End If

    End Sub

    Private Sub txt_jumlah_barang_masuk_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_jumlah_barang_masuk.TextChanged
        If txt_jumlah_barang_masuk.Text <> "" Then
            btn_ubah.Enabled = True
        Else
            btn_ubah.Enabled = False

        End If
    End Sub

    Private Sub txt_jumlah_barang_masuk_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_jumlah_barang_masuk.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Menahan karakter jika bukan angka atau karakter kontrol
        End If
    End Sub
End Class