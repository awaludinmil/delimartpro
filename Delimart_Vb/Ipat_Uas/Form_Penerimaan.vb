Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json.Linq

Public Class Form_Penerimaan
    ' Deklarasi API untuk mengatur gaya jendela
    <DllImport("user32.dll")>
    Private Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    End Function

    ' Konstanta untuk gaya jendela
    Private Const GWL_STYLE As Integer = -16
    Private Const WS_SYSMENU As Integer = &H80000
    Private Const WS_MINIMIZEBOX As Integer = &H20000
    Private Const WS_MAXIMIZEBOX As Integer = &H10000
    Private Const WS_CAPTION As Integer = &HC00000
    Private Sub RemoveControlBoxButtons(ByVal frm As Form)
        ' Dapatkan gaya jendela saat ini
        Dim style As Integer = GetWindowLong(frm.Handle, GWL_STYLE)

        ' Hapus tombol close, minimize, maximize, dan restore
        style = style And Not WS_SYSMENU And Not WS_MINIMIZEBOX And Not WS_MAXIMIZEBOX And Not WS_CAPTION

        ' Atur gaya jendela yang sudah dimodifikasi
        SetWindowLong(frm.Handle, GWL_STYLE, style)
    End Sub
    Public Sub GetData()
        'api(done)
        'Dim no As Integer
        'Dim total As Integer
        'Dim jumlah As Integer

        'Me.dg_penerimaan.Rows.Clear()
        'buka()
        'rek.Open("SELECT penerimaan.kd_transaksi_terima, barang.nama,barang.harga_beli,penerimaan.jumlah_barang_terima FROM penerimaan,barang WHERE  no_terima='" & txt_nota.Text & "' and barang.kd_barang=penerimaan.kd_barang order by kd_transaksi_terima desc  ", kon, 3, 2)
        'total = 0
        'no = 0
        'Do While Not rek.EOF
        '    no = no + 1
        '    Me.dg_penerimaan.Rows.Add(no, rek.Fields("nama").Value.ToString.ToUpper, rek.Fields("harga_beli").Value, rek.Fields("jumlah_barang_terima").Value, (rek.Fields("harga_beli").Value * rek.Fields("jumlah_barang_terima").Value), rek.Fields("kd_transaksi_terima").Value)

        '    jumlah = rek.Fields("harga_beli").Value * rek.Fields("jumlah_barang_terima").Value
        '    total = total + jumlah
        '    rek.MoveNext()
        'Loop
        'tutup()
        'txt_total_harga.Text = Format(total, "##,##0")


        Try
            'Form_Login.SetSecurityProtocol()
            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/get_penerimaan/" & txt_nota.Text.Trim()
            Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "GET"
            request.ContentType = "application/json"

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            If response.StatusCode = HttpStatusCode.OK Then
                Using responseStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(responseStream)
                        Dim responseContent As String = reader.ReadToEnd()

                        ' Periksa apakah respons kosong atau null
                        If String.IsNullOrEmpty(responseContent) Then
                            MsgBox("Error: Response is empty", MsgBoxStyle.Critical, "Error")
                            Return
                        End If

                        ' Parse JSON response
                        Dim penerimaan As JArray = JArray.Parse(responseContent)

                        ' Bersihkan dan isi ulang DataGridView dg_penerimaan
                        dg_penerimaan.Rows.Clear()

                        Dim total As Integer = 0
                        Dim no As Integer = 0
                        For Each item As JObject In penerimaan
                            no += 1
                            Dim namaBarang As String = item("nama_barang").ToString().ToUpper()
                            Dim hargaBeli As Integer = Integer.Parse(item("harga_beli").ToString())
                            Dim jumlahBarang As Integer = Integer.Parse(item("jumlah_barang_terima").ToString())
                            Dim totalHarga As Integer = hargaBeli * jumlahBarang
                            Dim kdTransaksi As String = item("kd_transaksi_terima").ToString()

                            dg_penerimaan.Rows.Add(no, namaBarang, hargaBeli, jumlahBarang, totalHarga, kdTransaksi)

                            total += totalHarga
                        Next

                        ' Tampilkan total harga
                        txt_total_harga.Text = Format(total, "##,##0")
                    End Using
                End Using
            Else
                MsgBox("Failed to retrieve data. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error get data: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Public Sub kode_auto()

        'buka()
        'rek.Open("SELECT * FROM penerimaan WHERE no_terima IN (SELECT MAX(no_terima) FROM penerimaan)", kon, 3, 2)
        'Dim urutan As String
        'Dim hitung As Long
        'If Not rek.EOF Then
        '    hitung = Microsoft.VisualBasic.Right(rek.Fields("no_terima").Value, 9) + 1
        '    urutan = "T" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & hitung, 3)
        'Else
        '    urutan = "T" & Format(Now, "yyMMdd") & "001"
        'End If
        'txt_nota.Text = urutan

        'tutup()

        Try
            'Form_Login.SetSecurityProtocol()
            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/nota_auto"
            Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
            request.Method = "GET"
            request.ContentType = "application/json"
            request.AllowAutoRedirect = True

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Dim responseContent As String = reader.ReadToEnd()
                Dim responseData As JObject = JObject.Parse(responseContent)

                Dim urutan As String = responseData("nota").ToString()
                txt_nota.Text = urutan
            End Using
        Catch ex As Exception
            MsgBox("Error Kode Auto: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Public Sub tampilcbxsup()
        Try
            'Form_Login.SetSecurityProtocol()
            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/supplier"

            Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
            request.Method = "GET"

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

            If response.StatusCode = HttpStatusCode.OK Then
                Dim responseStream As System.IO.Stream = response.GetResponseStream()
                Dim reader As New System.IO.StreamReader(responseStream)
                Dim responseContent As String = reader.ReadToEnd()

                ' Parse JSON response
                Dim suppliers As JArray = JArray.Parse(responseContent)

                ' Loop through each supplier and add nama supplier ke ComboBox
                For Each supplier As JObject In suppliers
                    Dim namaSupplier As String = supplier("nama").ToString()
                    cbx_namasup.Items.Add(namaSupplier)
                Next
            Else
                MsgBox("Failed to retrieve supplier data", MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error tampilcbxsup: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Public Sub tampilcbxnb()
        ''api(done)
        'cbx_namabrg.Items.Clear()
        'buka()
        'rek.Open("select * from barang where kd_supplier ='" & txt_kodesup.Text & "'", kon, 3, 2)
        'Do While Not rek.EOF
        '    cbx_namabrg.Items.Add(rek.Fields.Item("nama").Value)
        '    rek.MoveNext()
        'Loop
        'tutup()

        Try
            'Form_Login.SetSecurityProtocol()
            Dim kdSupplier As String = txt_kodesup.Text.Trim()


            cbx_namabrg.Items.Clear()

            Dim request As HttpWebRequest = CType(WebRequest.Create("https://praktikum-cpanel-unbin.com/golang_api/delimart_api/get_barang1/" & kdSupplier), HttpWebRequest)
            request.Method = "GET"
            request.ContentType = "application/json"

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Dim responseContent As String = reader.ReadToEnd()
                Dim responseData As JArray = JArray.Parse(responseContent)

                For Each item As JObject In responseData
                    cbx_namabrg.Items.Add(item("nama").ToString())
                Next
            End Using
        Catch ex As Exception
            MsgBox("Error tampil cbxnb: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Public Sub rumus_cari_item()
        Dim hitung As Integer = 0

        For i As Integer = 0 To dg_penerimaan.Rows.Count - 1
            hitung += 1
        Next

        lbl_item.Text = hitung
    End Sub
    Public Sub CallAfterUpdate()

        GetData()
        rumus_cari_item()
        rumus_cari_qty()
        txt_jumlahbrg.Text = ""
    End Sub
    Public Sub rumus_cari_qty()
        Dim hitung As Integer = 0

        For i As Integer = 0 To dg_penerimaan.Rows.Count - 1
            hitung += CInt(dg_penerimaan.Rows(i).Cells(3).Value)
        Next
        Me.lbl_qty.Text = hitung.ToString()
    End Sub


    Private Sub Guna2Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_kembali.Click
        Me.Close()
        Dispose()
        Form_Penjualan.MdiParent = Form_Transaksi
        Form_Penjualan.Show()
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        'delete all with golang api
        Dim nota As String = txt_nota.Text
        Dim jawab As Integer

        jawab = MsgBox("Apakah Anda yakin ingin membatalkan transaksi ini?", vbQuestion + vbYesNo, "Konfirmasi Pembatalan")
        If jawab = vbYes Then
            Try
                'Form_Login.SetSecurityProtocol()
                Dim request As HttpWebRequest = CType(WebRequest.Create("https://praktikum-cpanel-unbin.com/golang_api/delimart_api/delete_all_penerimaan/" & nota), HttpWebRequest)
                request.Method = "DELETE"

                Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                    If response.StatusCode = HttpStatusCode.OK Then
                        MsgBox("Transaksi Berhasil Dibatalkan")
                        CallAfterUpdate()
                        txt_kodebrg.Text = ""
                        cbx_namasup.SelectedItem = Nothing
                        cbx_namabrg.SelectedItem = Nothing
                        txt_hargabrg.Text = ""
                        txt_kodesup.Text = ""
                    Else
                        Using reader As New StreamReader(response.GetResponseStream())
                            Dim responseString As String = reader.ReadToEnd()
                            MsgBox("Error: " & responseString)
                        End Using
                    End If
                End Using
            Catch ex As WebException
                MsgBox("Error cancel: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub dg_penerimaan_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg_penerimaan.CellContentClick
        Form_Dialog_Penerimaan.txt_kode_transaksi.Text = dg_penerimaan.Rows(e.RowIndex).Cells(5).Value
        Form_Dialog_Penerimaan.txt_nama_barang.Text = dg_penerimaan.Rows(e.RowIndex).Cells(1).Value
        Form_Dialog_Penerimaan.txt_jumlah_barang_masuk.Text = dg_penerimaan.Rows(e.RowIndex).Cells(3).Value
        Form_Dialog_Penerimaan.ShowDialog()
    End Sub

    Private Sub Form_Penerimaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = Form_Transaksi.Width
        Me.Height = Form_Transaksi.Height
        RemoveControlBoxButtons(Me)
        Me.KeyPreview = True
        txt_total_harga.Text = 0
        kode_auto()
        tampilcbxsup()

    End Sub

    Private Sub txt_total_harga_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_total_harga.TextChanged
        Dim total_belanja As Integer = txt_total_harga.Text
        If total_belanja > 0 Then

            Form_Transaksi.btn_logout.Enabled = False
            cbx_namasup.Enabled = False
            btn_simpan.Enabled = True
            btn_kembali.Enabled = False
            btn_cancel.Enabled = True
            Form_Transaksi.btn_logout.Enabled = False
        Else
            txt_kodesup.Text = ""
            cbx_namasup.SelectedItem = Nothing
            cbx_namasup.Enabled = True
            btn_kembali.Enabled = True
            btn_simpan.Enabled = False
            btn_cancel.Enabled = False
            Form_Transaksi.btn_logout.Enabled = True
        End If
    End Sub

    Private Sub Form_Penerimaan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 And btn_simpan.Enabled = True Then
            btn_simpan.PerformClick()
        End If

        If e.KeyCode = Keys.F1 And btn_cancel.Enabled = True Then
            btn_cancel.PerformClick()
        End If

        If e.KeyCode = Keys.Enter And btn_insert.Enabled = True Then
            btn_insert.PerformClick()
        End If

        If e.KeyCode = Keys.Escape And btn_kembali.Enabled = True Then
            btn_kembali.PerformClick()
        End If

    End Sub

    Private Sub cbx_namasup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_namasup.SelectedIndexChanged
        ' ''api(done)
        'buka()
        'rek.Open("select * from supplier where nama='" & cbx_namasup.Text & "'", kon, 3, 2)
        'If Not rek.EOF Then
        '    txt_kodesup.Text = rek.Fields.Item("kd_supplier").Value

        'End If
        'tutup()
        'tampilcbxnb()
        'txt_kodebrg.Text = ""
        'txt_hargabrg.Text = ""

        Dim namaSupp As String = cbx_namasup.Text.Trim()

        If String.IsNullOrEmpty(namaSupp) Then
            ' Jika combobox kosong, kosongkan juga txt_kodesup
            txt_kodesup.Text = ""
            Return
        End If

        Try
            'Form_Login.SetSecurityProtocol()
            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/get_supplier1/" & Uri.EscapeUriString(namaSupp)
            Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
            request.Method = "GET"
            request.ContentType = "application/json"

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Using reader As New StreamReader(response.GetResponseStream())
                        Dim responseContent As String = reader.ReadToEnd()
                        Dim responseData As JObject = JObject.Parse(responseContent)

                        Dim kdSupp As String = responseData("kd_supplier").ToString()
                        txt_kodesup.Text = kdSupp
                    End Using


                    tampilcbxnb()
                    txt_kodebrg.Text = ""
                    txt_hargabrg.Text = ""
                Else

                    MsgBox("Supplier not found or failed to retrieve data. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
                    txt_kodesup.Text = ""
                End If
            End Using
        Catch ex As WebException
            If ex.Response IsNot Nothing Then
                Using errorResponse As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                    MsgBox("HTTP Response Error: " & errorResponse.StatusCode.ToString() & " - " & errorResponse.StatusDescription, MsgBoxStyle.Critical, "Error")
                End Using
            Else
                MsgBox("Error selecting supplier: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error selecting supplier: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub txt_kodesup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_kodesup.TextChanged
        If txt_kodesup.Text <> "" Then
            cbx_namabrg.Enabled = True

        Else
            cbx_namabrg.Enabled = False
        End If
    End Sub

    Private Sub cbx_namabrg_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_namabrg.SelectedIndexChanged
        ' ''api(done)
        'buka()
        'rek.Open("select * from barang where nama='" & cbx_namabrg.Text & "'", kon, 3, 2)
        'If Not rek.EOF Then
        '    txt_kodebrg.Text = rek.Fields.Item("kd_barang").Value
        '    txt_hargabrg.Text = rek.Fields("harga_beli").Value
        'End If
        'tutup()
        Dim namaBrg As String = cbx_namabrg.Text.Trim()
        If String.IsNullOrEmpty(namaBrg) Then
            ' Jika combobox kosong, kosongkan juga txt_kodebrg dan txt_hargabrg
            txt_kodebrg.Text = ""
            txt_hargabrg.Text = ""
            Return
        End If

        Try
            'Form_Login.SetSecurityProtocol()
            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/get_barang2/" & Uri.EscapeUriString(namaBrg)
            Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
            request.Method = "GET"
            request.ContentType = "application/json"

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Using reader As New StreamReader(response.GetResponseStream())
                        Dim responseContent As String = reader.ReadToEnd()
                        Dim responseData As JObject = JObject.Parse(responseContent)

                        Dim kdbrg As String = responseData("kd_barang").ToString()
                        Dim hargabrg As String = responseData("harga_beli").ToString()

                        txt_kodebrg.Text = kdbrg
                        txt_hargabrg.Text = hargabrg
                    End Using
                Else
                    MsgBox("Failed to retrieve data. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
                    txt_kodebrg.Text = ""
                    txt_hargabrg.Text = ""
                End If
            End Using
        Catch ex As WebException
            If ex.Response IsNot Nothing Then
                Using errorResponse As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                    MsgBox("HTTP Response Error: " & errorResponse.StatusCode.ToString() & " - " & errorResponse.StatusDescription, MsgBoxStyle.Critical, "Error")
                End Using
            Else
                MsgBox("Error selecting barang: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error selecting barang: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub txt_kodebrg_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_kodebrg.TextChanged
        If txt_kodebrg.Text <> "" Then
            txt_jumlahbrg.Enabled = True

        Else
            txt_jumlahbrg.Enabled = False
            txt_jumlahbrg.Text = ""
        End If
    End Sub

    Private Sub txt_jumlahbrg_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_jumlahbrg.TextChanged
        If txt_jumlahbrg.Text <> "" Then
            btn_insert.Enabled = True
        Else
            btn_insert.Enabled = False
        End If
    End Sub

    Private Sub btn_insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_insert.Click
        'Dim sql_tambah As String
        'Dim sql_update As String
        'Dim jmlhbrg As Integer = txt_jumlahbrg.Text
        'If jmlhbrg < 1 Then
        '    MsgBox("Minimal barang masuk 1")
        '    txt_jumlahbrg.Text = "1"
        'Else
        '    buka()
        '    ' Cek apakah barang sudah ada dalam database untuk transaksi ini
        '    rek.Open("SELECT * FROM penerimaan WHERE no_terima='" & txt_nota.Text.Trim & "' AND kd_barang='" & txt_kodebrg.Text.Trim & "'", kon, 3, 2)

        '    If Not rek.EOF Then
        '        ' Barang sudah ada, update jumlahnya
        '        Dim jumlah_baru As Integer = rek.Fields("jumlah_barang_terima").Value + CInt(txt_jumlahbrg.Text)
        '        sql_update = "UPDATE penerimaan SET jumlah_barang_terima=" & jumlah_baru & " WHERE no_terima='" & txt_nota.Text & "' AND kd_barang='" & txt_kodebrg.Text & "'"
        '        kon.Execute(sql_update)
        '        MsgBox("Jumlah Barang Berhasil Diperbarui", vbInformation, " Delimart APPS")
        '    Else
        '        ' Barang belum ada, tambahkan data baru
        '        Dim jawab As Integer
        '        jawab = MsgBox("Apakah anda Yakin ingin menyimpan data ini?", vbQuestion + vbYesNo, "Delimart APPS")
        '        If jawab = vbYes Then
        '            sql_tambah = "insert into penerimaan values(' ','" & txt_nota.Text & "','" & txt_kodebrg.Text & "','" & txt_jumlahbrg.Text & "',NOW(),'')"
        '            kon.Execute(sql_tambah)

        '        End If
        '    End If
        '    tutup()

        '    ' Perbarui DataGridView
        '    CallAfterUpdate()
        '    cbx_namabrg.SelectedItem = Nothing
        '    txt_kodebrg.Text = ""
        '    txt_hargabrg.Text = ""
        'End If


        Try
            'Form_Login.SetSecurityProtocol()
            Dim jmlhbrg As Integer = CInt(txt_jumlahbrg.Text.Trim())
            If jmlhbrg < 1 Then

                txt_jumlahbrg.Text = "1"
            End If
            Dim noTerima As String = txt_nota.Text.Trim()
            Dim kodeBarang As String = txt_kodebrg.Text.Trim()

            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/penerimaan"
            Dim requestData As New JObject()

            requestData.Add("no_terima", noTerima)
            requestData.Add("kd_barang", kodeBarang)
            requestData.Add("jumlah_barang_terima", jmlhbrg)

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
                    MsgBox("Data Penerimaan berhasil disimpan", MsgBoxStyle.Information, "Sukses")
                    ' Call any necessary update or refresh methods here
                    CallAfterUpdate()
                    cbx_namabrg.SelectedItem = Nothing
                    txt_kodebrg.Text = ""
                    txt_hargabrg.Text = ""
                Else
                    MsgBox("Gagal menyimpan data Penerimaan. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
                End If
            End Using
        Catch ex As WebException
            If ex.Response IsNot Nothing Then
                Using errorResponse As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                    Using reader As New StreamReader(errorResponse.GetResponseStream())
                        Dim errorContent As String = reader.ReadToEnd()
                        Dim errorData As JObject = JObject.Parse(errorContent)
                        Dim errorMessage As String = errorData("error").ToString()
                        MsgBox("Error: " & errorMessage, MsgBoxStyle.Critical, "Error")
                    End Using
                End Using
            Else
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try


    End Sub

    Private Sub btn_simpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_simpan.Click
        Dim jawab As Integer
        jawab = MsgBox("Apakah Anda yakin ingin menyimpan transaksi ini?", vbQuestion + vbYesNo, "Konfirmasi Transaksi")
        If jawab = vbYes Then
            Try
                'Form_Login.SetSecurityProtocol()
                Dim noTerima As String = txt_nota.Text.Trim()
                Dim tgl As String = Format(Now, "yyyy-MM-dd HH:mm:ss")
                Dim item As Integer = lbl_item.Text.Trim()
                Dim qty As Integer = lbl_qty.Text.Trim()
                Dim totalHarga As Integer = txt_total_harga.Text.Trim()
                Dim kdSupplier As String = txt_kodesup.Text.Trim()
                Dim kdPegawai As String = Form_Transaksi.lbl_id.Text.Trim()

                Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/detail_penerimaan"
                Dim requestData As New JObject()

                requestData.Add("nota", noTerima)
                requestData.Add("tanggal_terima", tgl)
                requestData.Add("total_item_terima", item)
                requestData.Add("total_qty_terima", qty)
                requestData.Add("total_harga_beli", totalHarga)
                requestData.Add("kd_supplier", kdSupplier)
                requestData.Add("kd_pegawai", kdPegawai)

                Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
                request.Method = "POST"
                request.ContentType = "application/json"

                Using streamWriter As New StreamWriter(request.GetRequestStream())
                    streamWriter.Write(requestData.ToString())
                    streamWriter.Flush()
                    streamWriter.Close()
                End Using

                Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                    Using reader As New StreamReader(response.GetResponseStream())
                        Dim responseText As String = reader.ReadToEnd()
                        If response.StatusCode = HttpStatusCode.Created Then
                            MsgBox("Data Penerimaan berhasil disimpan", MsgBoxStyle.Information, "Sukses")

                            kode_auto()
                            cbx_namabrg.SelectedItem = Nothing
                            cbx_namasup.SelectedItem = Nothing
                            txt_kodebrg.Text = ""
                            txt_hargabrg.Text = ""
                            CallAfterUpdate()
                        Else
                            MsgBox("Gagal menyimpan data Penerimaan. Status code: " & response.StatusCode.ToString() & " Response: " & responseText, MsgBoxStyle.Exclamation, "Error")
                        End If
                    End Using
                End Using
            Catch ex As WebException
                Using response As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                    Using reader As New StreamReader(response.GetResponseStream())
                        Dim responseText As String = reader.ReadToEnd()
                        MsgBox("Error Detail Penerimaan: " & ex.Message & " Response: " & responseText, MsgBoxStyle.Critical, "Error")
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Error Detail Penerimaan: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End If


    End Sub

    Private Sub txt_jumlahbrg_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_jumlahbrg.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Menahan karakter jika bukan angka atau karakter kontrol
        End If
    End Sub
End Class