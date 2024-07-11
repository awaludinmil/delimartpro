Imports System.Net
Imports System.IO
Imports Newtonsoft.Json.Linq


Public Class Form_Pulsa
    Public Sub GetData()
        'api(done)
        'Dim no As Integer

        'Me.dg_pulsa.Rows.Clear()
        'buka()
        'rek.Open("SELECT pulsa.kd_pulsa, pulsa.nama_produk_pulsa, pulsa.harga, provider.provider FROM pulsa, provider WHERE pulsa.kd_provider=provider.kd_provider AND provider= '" & txt_provider.Text & "'", kon, 3, 2)
        'no = 1
        'Do While Not rek.EOF
        '    Me.dg_pulsa.Rows.Add(no, rek.Fields("nama_produk_pulsa").Value.ToString.ToUpper, rek.Fields("harga").Value, rek.Fields("kd_pulsa").Value)
        '    no = no + 1
        '    rek.MoveNext()
        'Loop
        'tutup()

        Try
            'Form_Login.SetSecurityProtocol()
            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/get_pulsa/" & txt_provider.Text.Trim()
            Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "GET"
            request.ContentType = "application/json"

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            If response.StatusCode = HttpStatusCode.OK Then
                Using responseStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(responseStream)
                        Dim responseContent As String = reader.ReadToEnd()

                        If String.IsNullOrEmpty(responseContent) Then
                            MsgBox("Error: Response is empty", MsgBoxStyle.Critical, "Error")
                            Return
                        End If

                        ' Parse JSON response
                        Dim pulsa As JArray = JArray.Parse(responseContent)

                        dg_pulsa.Rows.Clear()

                        Dim no As Integer = 0
                        For Each item As JObject In pulsa
                            no += 1
                            Dim namaPulsa As String = item("nama_produk_pulsa").ToString().ToUpper()
                            Dim harga As Integer = Integer.Parse(item("harga").ToString())
                            Dim kdPulsa As String = item("kd_pulsa").ToString()


                            dg_pulsa.Rows.Add(no, namaPulsa, harga, kdPulsa)

                        Next

                    End Using
                End Using
            Else
                MsgBox("Failed to retrieve data. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error get data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Guna2Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_batal.Click
        txt_no_hp.Text = ""
        txt_provider.Text = ""
    End Sub

    Private Sub Guna2ControlBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guna2ControlBox1.Click
        txt_no_hp.Text = ""
        txt_provider.Text = ""
    End Sub


    Private Sub txt_no_hp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_no_hp.TextChanged
        Dim no_hp_length As Integer = txt_no_hp.Text.Length
        If no_hp_length < 10 Or no_hp_length > 13 Then

            Me.dg_pulsa.Rows.Clear()
        Else
            GetData()

        End If
        'api(done)
        'buka()
        'rek.Open("SELECT prefix.prefix AS nama_prefix, provider.provider AS nama_provider FROM prefix JOIN provider ON prefix.kd_provider = provider.kd_provider WHERE prefix.prefix ='" & txt_no_hp.Text & "'", kon, 3, 2)
        'If txt_no_hp.Text = "" Or txt_no_hp.Text = "0" Or txt_no_hp.Text = "08" Then
        '    txt_provider.Text = ""
        'Else
        '    If Not rek.EOF Then
        '        txt_provider.Text = rek.Fields("nama_provider").Value.ToString.ToUpper
        '    End If
        'End If
        'tutup()

        If txt_no_hp.Text.Length < 4 Then
            txt_provider.Text = ""
        Else
            Try
                Form_Login.SetSecurityProtocol()
                Dim noHp As String = txt_no_hp.Text.Trim()

                Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/get_provider/" & noHp
                Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
                request.Method = "GET"
                request.ContentType = "application/json"

                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

                If response.StatusCode = HttpStatusCode.OK Then
                    Using reader As New StreamReader(response.GetResponseStream())
                        Dim responseContent As String = reader.ReadToEnd()

                        ' Periksa apakah respons kosong atau tidak
                        If String.IsNullOrEmpty(responseContent) Then
                            ' Data provider tidak ditemukan
                            txt_provider.Text = ""
                        Else
                            ' Parse JSON response
                            Dim providerData As JObject = JObject.Parse(responseContent)
                            ' Tampilkan data provider di TextBox txt_provider
                            txt_provider.Text = providerData("provider").ToString().ToUpper()
                        End If
                    End Using
                Else
                    ' Gagal mengambil data provider
                    txt_provider.Text = ""
                    MsgBox("Gagal mengambil data provider. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
                End If
            Catch ex As WebException
                Dim httpResponse As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                If Not httpResponse.StatusCode = HttpStatusCode.NotFound Then
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End If
            End Try
        End If


    End Sub

    Private Sub dg_pulsa_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg_pulsa.CellContentClick
        'Dim sql_tambah As String
        'Dim sql_update As String

        'buka()
        'rek.Open("SELECT * FROM penjualan WHERE struk='" & Form_Penjualan.txt_struk.Text & "' AND kd_pulsa='" & dg_pulsa.Rows(e.RowIndex).Cells(3).Value() & "'", kon, 3, 2)
        'If Not rek.EOF Then
        '    Dim jumlah_baru As Integer = rek.Fields("jumlah_beli").Value + 1
        '    sql_update = "UPDATE penjualan SET jumlah_beli=" & jumlah_baru & " WHERE struk='" & Form_Penjualan.txt_struk.Text & "' AND kd_pulsa='" & dg_pulsa.Rows(e.RowIndex).Cells(3).Value() & "'"
        '    kon.Execute(sql_update)
        'Else
        '    sql_tambah = "INSERT INTO penjualan VALUES(' ', '" & Form_Penjualan.txt_struk.Text & "',NULL, '1', '" & dg_pulsa.Rows(e.RowIndex).Cells(3).Value() & "' , 'pulsa',NOW(),'')"
        '    kon.Execute(sql_tambah)
        'End If
        'tutup()
        'Form_Penjualan.CallAfterUpdate()
        'Me.Close()
        'txt_no_hp.Text = ""
        'Me.dg_pulsa.Rows.Clear()


        Try
            'Form_Login.SetSecurityProtocol()

            Dim struk As String = Form_Penjualan.txt_struk.Text.Trim()
            Dim kd_pulsa As String = dg_pulsa.Rows(e.RowIndex).Cells(3).Value().ToString.Trim()
            Dim jenis_produk As String = "pulsa"
            Dim jumlah_beli As Integer = 1

            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/penjualan"
            Dim requestData As New JObject()

            requestData.Add("struk", struk)
            requestData.Add("kd_pulsa", kd_pulsa)
            requestData.Add("jumlah_beli", jumlah_beli)
            requestData.Add("jenis_produk", jenis_produk)

            Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
            request.Method = "POST"
            request.ContentType = "application/json"

            Using streamWriter As New StreamWriter(request.GetRequestStream())
                streamWriter.Write(requestData.ToString())
                streamWriter.Flush()
                streamWriter.Close()
            End Using

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then

                    txt_no_hp.Text = ""
                    Me.dg_pulsa.Rows.Clear()
                    Me.Close()
                    Form_Penjualan.CallAfterUpdate()

                Else
                    MsgBox("Gagal menyimpan data Penerimaan. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error CreateOrUpdatePenerimaan: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub txt_no_hp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_no_hp.KeyPress
        ' Memeriksa apakah karakter yang dimasukkan adalah digit atau karakter kontrol
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Menahan karakter jika bukan angka atau karakter kontrol
        End If
    End Sub
End Class
