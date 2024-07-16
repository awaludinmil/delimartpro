Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class Form_Dialog_Transaksi

    Private Sub Guna2Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guna2Button2.Click
        'Dim update As String
        'Dim kd_transaksi As Integer
        'Dim jmlh_beli As Integer
        'Dim jmlh_beli_sebelumnya As Integer
        'Dim jenis_produk As String
        'kd_transaksi = CInt(txt_kode_transaksi.Text)
        'jmlh_beli = CInt(txt_jumlah_beli.Text)

        'If jmlh_beli < 1 Then
        '    MsgBox("Minimal Pembelian Barang Harus 1 ", MsgBoxStyle.Critical, "Peringatan")
        '    txt_jumlah_beli.Text = ""
        'Else
        '    buka()
        '    rek.Open("SELECT jumlah_beli, jenis_produk FROM penjualan WHERE kd_transaksi=" & kd_transaksi, kon, 3, 2)
        '    If Not rek.EOF Then
        '        jmlh_beli_sebelumnya = CInt(rek.Fields("jumlah_beli").Value)
        '        jenis_produk = rek.Fields("jenis_produk").Value

        '        If jenis_produk = "barang" Then
        '            tutup()
        '            buka()
        '            rek.Open("SELECT * FROM barang WHERE nama='" & txt_nama_barang.Text & "'", kon, 3, 2)
        '            If Not rek.EOF Then
        '                Dim stok As Integer = rek.Fields("stok").Value
        '                Dim stok_terbaru As Integer = stok + jmlh_beli_sebelumnya


        '                If jmlh_beli > stok_terbaru Then
        '                    MsgBox("Stok Barang Tersisa " & stok & "", MsgBoxStyle.Critical, "Peringatan")
        '                    tutup()
        '                Else
        '                    tutup()
        '                    buka()
        '                    ' Update jumlah beli
        '                    update = "UPDATE penjualan SET jumlah_beli=" & jmlh_beli & " WHERE kd_transaksi=" & kd_transaksi
        '                    kon.Execute(update)


        '                    tutup()
        '                    MsgBox("Data Barang Berhasil Diubah")
        '                    Form_Penjualan.CallAfterUpdate()
        '                    Me.Close()
        '                End If
        '            End If
        '        ElseIf jenis_produk = "pulsa" Then
        '            tutup()
        '            buka()
        '            rek.Open("SELECT * FROM pulsa WHERE nama_produk_pulsa='" & txt_nama_barang.Text & "'", kon, 3, 2)
        '            If Not rek.EOF Then
        '                tutup()
        '                buka()
        '                ' Update jumlah beli
        '                update = "UPDATE penjualan SET jumlah_beli=" & jmlh_beli & " WHERE kd_transaksi=" & kd_transaksi
        '                kon.Execute(update)
        '                tutup()
        '                MsgBox("Data Pulsa Berhasil Diubah")
        '                Form_Penjualan.CallAfterUpdate()
        '                Me.Close()
        '            End If
        '        End If
        '    End If
        'End If


        Dim kd_transaksi As Integer = txt_kode_transaksi.Text.Trim()
        Dim jmlh_beli As Integer = txt_jumlah_beli.Text.Trim()


        If jmlh_beli < 1 Then
            MsgBox("Minimal Pembelian Barang Harus 1 ", MsgBoxStyle.Critical, "Peringatan")
            txt_jumlah_beli.Text = ""
        Else
            Try
                'Form_Login.SetSecurityProtocol()
                Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/penjualan/" & kd_transaksi
                Dim requestData As New JObject()

                requestData.Add("jumlah_beli", jmlh_beli)

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
                        MsgBox("Data Berhasil Diubah", MsgBoxStyle.Information, "Sukses")
                        Form_Penjualan.CallAfterUpdate()
                        Me.Close()
                    Else
                        MsgBox("Gagal mengubah data. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
                    End If
                End Using
            Catch ex As WebException
                If ex.Response IsNot Nothing Then
                    Using errorResponse As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                        Using reader As New StreamReader(errorResponse.GetResponseStream())
                            Dim errorText As String = reader.ReadToEnd()
                            Dim errorJson As JObject = JObject.Parse(errorText)
                            If errorJson("error") IsNot Nothing Then
                                MsgBox("Error: " & errorJson("error").ToString(), MsgBoxStyle.Critical, "Error")
                            Else
                                MsgBox("Unknown Error", MsgBoxStyle.Critical, "Error")
                            End If
                        End Using
                    End Using
                Else
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub


    Private Sub Guna2Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guna2Button1.Click
        'Dim delete As String
        'Dim kd_transaksi As Integer
        'Dim jawab As Integer
        'kd_transaksi = txt_kode_transaksi.Text
        'jawab = MsgBox("Apakah Anda yakin ingin membatalkan transaksi ini?", vbQuestion + vbYesNo, "Konfirmasi Pembatalan")
        'If jawab = vbYes Then
        '    buka()
        '    delete = "delete from penjualan where kd_transaksi='" & kd_transaksi & "'"
        '    kon.Execute(delete)
        '    tutup()
        '    MsgBox("Data Berhasil Dihapus")
        '    Form_Transaksi.CallAfterUpdate()
        '    Me.Close()
        'End If

        'delete with golang api
        Dim kd_transaksi As String = txt_kode_transaksi.Text
        Dim jawab As Integer

        jawab = MsgBox("Apakah Anda yakin ingin membatalkan transaksi ini?", vbQuestion + vbYesNo, "Konfirmasi Pembatalan")
        If jawab = vbYes Then
            Try
                'Form_Login.SetSecurityProtocol()
                Dim request As HttpWebRequest = CType(WebRequest.Create("https://praktikum-cpanel-unbin.com/golang_api/delimart_api/penjualan/" & kd_transaksi), HttpWebRequest)
                request.Method = "DELETE"

                Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                    If response.StatusCode = HttpStatusCode.OK Then
                        MsgBox("Data Berhasil Dihapus")
                        Form_Penjualan.CallAfterUpdate()
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

   
    Private Sub txt_jumlah_beli_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_jumlah_beli.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Menahan karakter jika bukan angka atau karakter kontrol
        End If
    End Sub
End Class
