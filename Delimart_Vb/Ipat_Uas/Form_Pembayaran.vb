Imports System.Drawing.Printing
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class Form_Pembayaran
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim PrintDlg As New PrintDialog
    Dim t_qty As Long
    Dim panjang As Integer

    Sub ubah_panjang()
        Dim rowcount As Integer
        panjang = 0
        rowcount = Form_Penjualan.dg_transaksi.RowCount
        panjang = rowcount * 15
        panjang = panjang + 290
    End Sub
    Private Sub Form_Pembayaran_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txt_uang_konsumen.Text = 0
        txt_kembalian.Text = -txt_totbay.Text
    End Sub

    Private Sub btn_batal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_batal.Click
        txt_uang_konsumen.Text = 0
    End Sub

    Private Sub Guna2ControlBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guna2ControlBox1.Click
        txt_diskon.Text = ""
        txt_pajak.Text = ""
        txt_totbay.Text = ""
        txt_kembalian.Text = ""
    End Sub
    Private Sub txt_uang_konsumen_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txt_uang_konsumen.KeyPress
        ' Cek apakah karakter yang diinput bukan angka
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Batalkan karakter jika bukan angka
        End If
    End Sub
    Private Sub txt_uang_konsumen_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_uang_konsumen.TextChanged

        If txt_uang_konsumen.Text <> "" Then
            Dim uang_konsumen As Integer = txt_uang_konsumen.Text
            Dim totbay As Integer = txt_totbay.Text
            Dim kembalian As Integer

            kembalian = uang_konsumen - totbay
            txt_kembalian.Text = kembalian
            Me.txt_kembalian.Text = Format(kembalian, "##,##0")
            If uang_konsumen < totbay Then
                btn_proses.Enabled = False
            Else
                btn_proses.Enabled = True

            End If
        Else
            txt_uang_konsumen.Text = 0
        End If
    End Sub

    Private Sub btn_proses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_proses.Click




        'buka()
        'Dim sql_tambah As String = "insert into detail_penjualan values('" & Form_Penjualan.txt_struk.Text & "','" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "','" & item & "','" & qty & "','" & diskon & "','" & pajak & "','" & totalbayar & "','" & txt_uang_konsumen.Text & "','" & kembalian & "','" & Form_Transaksi.lbl_id.Text & "')"
        'kon.Execute(sql_tambah)
        'tutup()

        Try
            'Form_Login.SetSecurityProtocol()
            Dim struk As String = Form_Penjualan.txt_struk.Text.Trim()
            Dim tgl As String = Format(Now, "yyyy-MM-dd HH:mm:ss")
            Dim diskon As Integer = txt_diskon.Text.Trim()
            Dim pajak As Integer = txt_pajak.Text.Trim()
            Dim item As Integer = Form_Penjualan.lbl_item.Text.Trim()
            Dim qty As Integer = Form_Penjualan.lbl_qty.Text.Trim()

            Dim totbay As Integer = txt_totbay.Text.Trim()
            Dim dibayar As Integer = txt_uang_konsumen.Text.Trim()
            Dim kembalian As Integer = txt_kembalian.Text.Trim()
            Dim kdPegawai As String = Form_Transaksi.lbl_id.Text.Trim()

            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/detail_penjualan"
            Dim requestData As New JObject()

            requestData.Add("struk", struk)
            requestData.Add("tanggal_jual", tgl)
            requestData.Add("total_item", item)
            requestData.Add("total_qty", qty)
            requestData.Add("diskon", diskon)
            requestData.Add("pajak", pajak)
            requestData.Add("grand_total", totbay)
            requestData.Add("dibayar", dibayar)
            requestData.Add("kembalian", kembalian)
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
                        ubah_panjang()
                        PPD.Document = PD
                        PPD.ShowDialog()

                        If PrintDlg.ShowDialog() = DialogResult.OK Then
                            PD.Print()
                        End If


                        txt_diskon.Text = ""
                        txt_pajak.Text = ""
                        txt_totbay.Text = ""
                        txt_kembalian.Text = ""
                        Form_Penjualan.kode_auto()
                        Form_Penjualan.CallAfterUpdate()
                        Me.Close()

                    Else
                        MsgBox("Gagal menyimpan data Penjualan. Status code: " & response.StatusCode.ToString() & " Response: " & responseText, MsgBoxStyle.Exclamation, "Error")
                    End If
                End Using
            End Using
        Catch ex As WebException
            Using response As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()
                    MsgBox("Error Detail Penjualan: " & ex.Message & " Response: " & responseText, MsgBoxStyle.Critical, "Error")

                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error Detail Penjualan: " & ex.Message, MsgBoxStyle.Critical, "Error")

        End Try

    End Sub

    Private Sub btn_1k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_1k.Click
        txt_uang_konsumen.Text = 1000

    End Sub

    Private Sub btn_2k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_2k.Click
        txt_uang_konsumen.Text = 2000

    End Sub

    Private Sub btn_5k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_5k.Click
        txt_uang_konsumen.Text = 5000
    End Sub

    Private Sub btn_10k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_10k.Click
        txt_uang_konsumen.Text = 10000
    End Sub

    Private Sub btn_20k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_20k.Click
        txt_uang_konsumen.Text = 20000
    End Sub

    Private Sub btn_50k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_50k.Click
        txt_uang_konsumen.Text = 50000
    End Sub

    Private Sub btn_75k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_75k.Click
        txt_uang_konsumen.Text = 75000
    End Sub

    Private Sub btn_100k_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_100k.Click
        txt_uang_konsumen.Text = 100000
    End Sub

    Private Sub btn_uang_pas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_uang_pas.Click
        txt_uang_konsumen.Text = txt_totbay.Text
    End Sub
    Private Sub PD_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 250, panjang)
        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PD_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PD.PrintPage
        Dim f10 As New Font("Times New Roman", 10, FontStyle.Regular)
        Dim f10b As New Font("Times New Roman", 10, FontStyle.Bold)
        Dim f14 As New Font("Times New Roman", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        Dim kanan As New StringFormat
        Dim tengah As New StringFormat
        kanan.Alignment = StringAlignment.Far
        tengah.Alignment = StringAlignment.Center

        Dim garis As String
        garis = "---------------------------------------------------------"

        'e.Graphics.DrawString("DELIMART", f14, Brushes.Black, centermargin, 5, tengah)
        Dim logo As Image = My.Resources.ResourceManager.GetObject("logo_struk")
        e.Graphics.DrawImage(logo, CInt((e.PageBounds.Width - 150) / 2), 5, 150, 50)
        e.Graphics.DrawString("Jl. Tanah Baru No.69", f10, Brushes.Black, centermargin, 55, tengah)
        e.Graphics.DrawString("Telp: 0816-2201-1203", f10, Brushes.Black, centermargin, 70, tengah)

        e.Graphics.DrawString("No Struk", f10, Brushes.Black, 0, 90)
        e.Graphics.DrawString(" : ", f10, Brushes.Black, 65, 90)
        e.Graphics.DrawString(Form_Penjualan.txt_struk.Text, f10, Brushes.Black, 75, 90)

        e.Graphics.DrawString("Kasir", f10, Brushes.Black, 0, 105)
        e.Graphics.DrawString(" : ", f10, Brushes.Black, 65, 105)
        e.Graphics.DrawString(Form_Transaksi.lbl_nama.Text, f10, Brushes.Black, 75, 105)

        e.Graphics.DrawString(Date.Now.ToString("dd-MM-yyyy ") & Form_Transaksi.lbl_jam.Text, f10, Brushes.Black, 0, 130)

        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, 135)

        Dim tinggi As Integer
        Dim i As Long
        For baris As Integer = 0 To Form_Penjualan.dg_transaksi.RowCount - 1
            tinggi += 15
            e.Graphics.DrawString(Form_Penjualan.dg_transaksi.Rows(baris).Cells(3).Value, f10, Brushes.Black, 0, 130 + tinggi)
            e.Graphics.DrawString(Form_Penjualan.dg_transaksi.Rows(baris).Cells(1).Value, f10, Brushes.Black, 25, 130 + tinggi)

            i = Form_Penjualan.dg_transaksi.Rows(baris).Cells(2).Value
            Form_Penjualan.dg_transaksi.Rows(baris).Cells(2).Value = Format(i, "##,##0")
            e.Graphics.DrawString(Form_Penjualan.dg_transaksi.Rows(baris).Cells(2).Value, f10, Brushes.Black, rightmargin, 130 + tinggi, kanan)
        Next
        ht_qty()
        tinggi = 140 + tinggi
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString("Total : " & Format(Form_Penjualan.txt_total_belanja.Text), f10, Brushes.Black, rightmargin, 10 + tinggi, kanan)
        e.Graphics.DrawString(t_qty, f10, Brushes.Black, 0, 10 + tinggi)

        tinggi += 25

        e.Graphics.DrawString("Diskon: ", f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString(Format(txt_diskon.Text), f10, Brushes.Black, rightmargin, tinggi, kanan)
        tinggi += 15

        e.Graphics.DrawString("PPN: ", f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString(Format(txt_pajak.Text), f10, Brushes.Black, rightmargin, tinggi, kanan)
        tinggi += 15

        e.Graphics.DrawString("Total Belanja: ", f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString(Format(txt_totbay.Text), f10, Brushes.Black, rightmargin, tinggi, kanan)
        tinggi += 15
        Dim tunai As Integer = txt_uang_konsumen.Text
        e.Graphics.DrawString("Tunai: ", f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString(Format(tunai, "##,##0"), f10, Brushes.Black, rightmargin, tinggi, kanan)
        tinggi += 15

        e.Graphics.DrawString("Kembalian: ", f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString(Format(txt_kembalian.Text), f10, Brushes.Black, rightmargin, tinggi, kanan)
        tinggi += 25

        ' Pesan penutup
        e.Graphics.DrawString("~ Terimakasih Telah Berbelanja ~", f10, Brushes.Black, centermargin, tinggi, tengah)
        tinggi += 15
        e.Graphics.DrawString("~ Di Toko Kami ~", f10, Brushes.Black, centermargin, tinggi, tengah)
    End Sub
    Sub ht_qty()
        t_qty = Form_Penjualan.lbl_qty.Text
    End Sub
End Class
