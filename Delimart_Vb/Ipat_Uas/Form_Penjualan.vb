Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class Form_Penjualan
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
    Private Sub Form_Penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = Form_Transaksi.Width
        Me.Height = Form_Transaksi.Height
        RemoveControlBoxButtons(Me)
        Me.KeyPreview = True
        kode_auto()
        txt_kd_brg.Focus()
        txt_total_belanja.Text = 0

    End Sub
    Public Sub GetData()
        'Code untuk tampil data
        'Dim no As Integer
        'Dim total As Integer
        'Dim jumlah As Integer
        'Dim diskon As Integer

        ''dg_transaksi.AllowUserToAddRows = True
        'Me.dg_transaksi.Rows.Clear()

        'buka()

        '' Query untuk mendapatkan data barang
        'Dim query_barang As String = "SELECT penjualan.kd_transaksi, barang.nama, barang.harga_jual,barang.diskon, penjualan.jumlah_beli " &
        '                             "FROM penjualan, barang " &
        '                             "WHERE penjualan.struk='" & txt_struk.Text & "' AND barang.kd_barang=penjualan.kd_barang AND penjualan.jenis_produk='barang' " &
        '                             "ORDER BY penjualan.kd_transaksi DESC"

        '' Query untuk mendapatkan data pulsa
        'Dim query_pulsa As String = "SELECT penjualan.kd_transaksi, pulsa.nama_produk_pulsa AS nama, pulsa.harga AS harga_jual, penjualan.jumlah_beli " &
        '                            "FROM penjualan, pulsa " &
        '                            "WHERE penjualan.struk='" & txt_struk.Text & "' AND pulsa.kd_pulsa=penjualan.kd_pulsa AND penjualan.jenis_produk='pulsa' " &
        '                            "ORDER BY penjualan.kd_transaksi DESC"

        '' Mengambil data barang
        'rek.Open(query_barang, kon, 3, 2)
        'no = 0
        'total = 0

        '' Menambahkan data barang ke DataGridView
        'Do While Not rek.EOF
        '    no = no + 1
        '    jumlah = rek.Fields("harga_jual").Value * rek.Fields("jumlah_beli").Value
        '    diskon = jumlah * (rek.Fields("diskon").Value / 100)
        '    total = total + jumlah
        '    Me.dg_transaksi.Rows.Add(no, rek.Fields("nama").Value.ToString.ToUpper, rek.Fields("harga_jual").Value, rek.Fields("jumlah_beli").Value, rek.Fields("diskon").Value, total - diskon, rek.Fields("kd_transaksi").Value)


        '    rek.MoveNext()
        'Loop
        'rek.Close()

        '' Mengambil data pulsa
        'rek.Open(query_pulsa, kon, 3, 2)

        '' Menambahkan data pulsa ke DataGridView
        'Do While Not rek.EOF
        '    no = no + 1
        '    Me.dg_transaksi.Rows.Add(no, rek.Fields("nama").Value.ToString.ToUpper, rek.Fields("harga_jual").Value, rek.Fields("jumlah_beli").Value, 0, (rek.Fields("harga_jual").Value * rek.Fields("jumlah_beli").Value), rek.Fields("kd_transaksi").Value)
        '    jumlah = rek.Fields("harga_jual").Value * rek.Fields("jumlah_beli").Value
        '    total = total + jumlah
        '    rek.MoveNext()
        'Loop
        'tutup()

        '' Code untuk mengisi jumlah harga pada setiap row yang ada di data grid
        'lbl_diskon.Text = Format(diskon, "##,##0")
        'Me.txt_total_belanja.Text = Format(total, "##,##0")

        Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/get_penjualan/" & txt_struk.Text.Trim()
        Try
            'Form_Login.SetSecurityProtocol()
            Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
            request.Method = "GET"
            request.ContentType = "application/json"


            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseContent As String = reader.ReadToEnd()
                    Dim jsonData As JArray = JArray.Parse(responseContent)

                    Me.dg_transaksi.Rows.Clear()

                    Dim no As Integer = 0
                    Dim total As Integer = 0
                    Dim diskon As Integer = 0

                    For Each item As JObject In jsonData
                        no += 1
                        Dim nama As String = item("nama").ToString.ToUpper()
                        Dim hargaJual As Integer = item("harga_jual").Value(Of Integer)()
                        Dim jumlahBeli As Integer = item("jumlah_beli").Value(Of Integer)()
                        Dim diskonPersen As Integer = If(item("diskon") IsNot Nothing, item("diskon").Value(Of Integer), 0)

                        Dim jumlah As Integer = hargaJual * jumlahBeli
                        Dim diskonItem As Integer = jumlah * (diskonPersen / 100)
                        total += jumlah - diskonItem
                        diskon += diskonItem

                        Me.dg_transaksi.Rows.Add(no, nama, hargaJual, jumlahBeli, diskonPersen, jumlah - diskonItem, item("kd_transaksi").Value(Of String))
                    Next

                    Me.lbl_diskon.Text = Format(diskon, "##,##0")
                    Me.txt_total_belanja.Text = Format(total, "##,##0")
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Public Sub kode_auto()
        ''api(done)
        'buka()
        'rek.Open("Select * from penjualan where struk in (select max(struk) from penjualan)", kon, 3, 2)
        'Dim urutan As String
        'Dim hitung As Long
        'If Not rek.EOF Then
        '    hitung = Microsoft.VisualBasic.Right(rek.Fields("struk").Value, 9) + 1
        '    urutan = "J" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & hitung, 3)
        'Else
        '    urutan = "J" & Format(Now, "yyMMdd") & "001"
        'End If
        'txt_struk.Text = urutan

        'tutup()

        Try
            'Form_Login.SetSecurityProtocol()
            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/struk_auto"
            Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
            request.Method = "GET"
            request.ContentType = "application/json"

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Dim responseContent As String = reader.ReadToEnd()
                Dim responseData As JObject = JObject.Parse(responseContent)

                Dim urutan As String = responseData("struk")
                txt_struk.Text = urutan
            End Using
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub



    Private Sub txt_total_belanja_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_total_belanja.TextChanged
        Dim total_belanja As Integer = txt_total_belanja.Text
        If total_belanja > 0 Then
            btn_bayar.Enabled = True
            btn_penerimaan_barang.Enabled = False
            btn_cancel.Enabled = True
            Form_Transaksi.btn_logout.Enabled = False
        Else
            btn_penerimaan_barang.Enabled = True
            btn_bayar.Enabled = False
            btn_cancel.Enabled = False
            Form_Transaksi.btn_logout.Enabled = True
        End If
    End Sub

    Private Sub btn_bayar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_bayar.Click

        Form_Pembayaran.txt_pajak.Text = lbl_pajak.Text
        Form_Pembayaran.txt_diskon.Text = lbl_diskon.Text
        Form_Pembayaran.txt_totbay.Text = lbl_grand_total.Text
        Form_Pembayaran.ShowDialog()
    End Sub


    Public Sub rumus_cari_item()
        Dim hitung As Integer = 0

        For i As Integer = 0 To dg_transaksi.Rows.Count - 1
            hitung += 1
        Next

        lbl_item.Text = hitung
    End Sub
    Public Sub CallAfterUpdate()
        GetData()
        rumus_cari_item()
        rumus_cari_qty()
        rumus_cari_grand_total()
        lbl_sub_total.Text = txt_total_belanja.Text
    End Sub
    Public Sub rumus_cari_qty()
        Dim hitung As Integer = 0

        For i As Integer = 0 To dg_transaksi.Rows.Count - 1
            hitung += CInt(dg_transaksi.Rows(i).Cells(3).Value)
        Next
        Me.lbl_qty.Text = hitung.ToString()
    End Sub

    Public Sub rumus_cari_grand_total()
        Dim pajak As Double = 0.1
        Dim diskon As Integer = lbl_diskon.Text
        Dim sub_total As Integer = txt_total_belanja.Text
        Dim hasil_pajak As Integer
        Dim hasil_grand_total As Integer

        hasil_pajak = sub_total * pajak
        lbl_pajak.Text = Format(hasil_pajak, "##,##0")

        hasil_grand_total = sub_total + hasil_pajak
        lbl_grand_total.Text = Format(hasil_grand_total, "##,##0")

    End Sub
    Private Sub txt_kd_brg_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_kd_brg.KeyPress

        'Dim sql_tambah As String
        'Dim sql_update As String

        'If (e.KeyChar = Chr(13)) Then
        '    buka()
        '    rek.Open("select * from barang where kd_barang='" & txt_kd_brg.Text & "'", kon, 3, 2)
        '    If Not rek.EOF Then
        '        Dim stok As Integer
        '        stok = rek.Fields("stok").Value

        '        If stok < 1 Then
        '            tutup()
        '            MsgBox("Maaf Stok Hanya Tersisa " & stok & "", MsgBoxStyle.Critical, "Peringatan")

        '        Else
        '            tutup()
        '            txt_kd_brg.Text = UCase(txt_kd_brg.Text)
        '            buka()
        '            ' Cek apakah barang sudah ada dalam database untuk transaksi ini
        '            rek.Open("SELECT * FROM penjualan WHERE struk='" & txt_struk.Text & "' AND kd_barang='" & txt_kd_brg.Text & "'", kon, 3, 2)
        '            If Not rek.EOF Then
        '                ' Barang sudah ada, update jumlahnya
        '                Dim jumlah_baru As Integer = rek.Fields("jumlah_beli").Value + 1
        '                sql_update = "UPDATE penjualan SET jumlah_beli=" & jumlah_baru & " WHERE struk='" & txt_struk.Text & "' AND kd_barang='" & txt_kd_brg.Text & "'"
        '                kon.Execute(sql_update)

        '            Else
        '                ' Barang belum ada, tambahkan data baru
        '                sql_tambah = "insert into penjualan values(' ','" & txt_struk.Text & "','" & txt_kd_brg.Text & "','1',NULL,'barang',NOW(),'')"
        '                kon.Execute(sql_tambah)

        '            End If
        '            tutup()
        '            CallAfterUpdate()
        '            txt_kd_brg.Text = ""


        '        End If
        '    Else
        '        tutup()
        '        MsgBox("Kode Barang Tidak ada !!", vbInformation, "DeliMart")
        '        txt_kd_brg.Text = ""
        '    End If

        'End If


        If (e.KeyChar = Chr(13)) Then
            Dim struk As String = txt_struk.Text.Trim()
            Dim kd_barang As String = UCase(txt_kd_brg.Text).Trim()
            Dim jumlah_beli As Integer = 1
            Dim jenis_produk As String = "barang"

            Dim penjualan As New JObject()
            penjualan.Add("struk", struk)
            penjualan.Add("kd_barang", kd_barang)
            penjualan.Add("jumlah_beli", jumlah_beli)
            penjualan.Add("jenis_produk", jenis_produk)

            Try
                'Form_Login.SetSecurityProtocol()
                Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/penjualan"
                Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
                request.Method = "POST"
                request.ContentType = "application/json"

                Using streamWriter As New StreamWriter(request.GetRequestStream())
                    streamWriter.Write(penjualan.ToString())
                    streamWriter.Flush()
                    streamWriter.Close()
                End Using

                Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                    If response.StatusCode = HttpStatusCode.OK Then

                        CallAfterUpdate()
                        txt_kd_brg.Text = ""
                    Else
                        MsgBox("Gagal menyimpan data Penjualan. Status code: " & response.StatusCode.ToString(), MsgBoxStyle.Exclamation, "Error")
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

    Private Sub dg_transaksi_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg_transaksi.CellContentClick
        Form_Dialog_Transaksi.txt_kode_transaksi.Text = dg_transaksi.Rows(e.RowIndex).Cells(6).Value
        Form_Dialog_Transaksi.txt_nama_barang.Text = dg_transaksi.Rows(e.RowIndex).Cells(1).Value
        Form_Dialog_Transaksi.txt_jumlah_beli.Text = dg_transaksi.Rows(e.RowIndex).Cells(3).Value


        Form_Dialog_Transaksi.ShowDialog()
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        'Dim jawab As Integer
        'jawab = MsgBox("Apakah Anda yakin ingin membatalkan transaksi ini?", vbQuestion + vbYesNo, "Konfirmasi Pembatalan")
        'If jawab = vbYes Then
        '    buka()
        '    Dim sql_hapus As String = "DELETE FROM penjualan WHERE struk='" & txt_struk.Text & "'"
        '    kon.Execute(sql_hapus)
        '    tutup()
        '    Me.dg_transaksi.Rows.Clear()
        '    CallAfterUpdate()
        '    txt_kd_brg.Text = ""

        '    MsgBox("Transaksi telah dibatalkan.", MsgBoxStyle.Information, "DeliMart")
        'End If

        'delete all with golang api
        Dim struk As String = txt_struk.Text
        Dim jawab As Integer

        jawab = MsgBox("Apakah Anda yakin ingin membatalkan transaksi ini?", vbQuestion + vbYesNo, "Konfirmasi Pembatalan")
        If jawab = vbYes Then
            Try
                'Form_Login.SetSecurityProtocol()
                Dim request As HttpWebRequest = CType(WebRequest.Create("https://praktikum-cpanel-unbin.com/golang_api/delimart_api/delete_all_penjualan/" & struk), HttpWebRequest)
                request.Method = "DELETE"

                Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                    If response.StatusCode = HttpStatusCode.OK Then
                        MsgBox("Transaksi Berhasil Dibatalkan")
                        CallAfterUpdate()

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


    Private Sub Form_Transaksi_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 And btn_bayar.Enabled = True Then
            btn_bayar.PerformClick()
        End If

        If e.KeyCode = Keys.F10 And btn_penerimaan_barang.Enabled = True Then
            btn_penerimaan_barang.PerformClick()
        End If

        If e.KeyCode = Keys.F12 And btn_bayar.Enabled = True Then
            btn_bayar.PerformClick()
        End If

        If e.KeyCode = Keys.F1 And btn_cancel.Enabled = True Then
            btn_cancel.PerformClick()
        End If

        If e.KeyCode = Keys.F2 Then
            btn_pulsa.PerformClick()
        End If
    End Sub

    Private Sub btn_pulsa_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_pulsa.Click
        Form_Pulsa.ShowDialog()
        Form_Pulsa.txt_no_hp.Focus()
    End Sub

    Private Sub btn_penerimaan_barang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_penerimaan_barang.Click
        Me.Close()
        Dispose()
        Form_Penerimaan.MdiParent = Form_Transaksi
        Form_Penerimaan.Show()
    End Sub

  
End Class