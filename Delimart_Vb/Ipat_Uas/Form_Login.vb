Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class Form_Login

    Private isPasswordVisible As Boolean = False

    Private Sub txt_pw_IconRightClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_pw.IconRightClick
        isPasswordVisible = Not isPasswordVisible
        If isPasswordVisible Then
            txt_pw.IconRight = My.Resources.visibility
            txt_pw.UseSystemPasswordChar = False
        Else
            txt_pw.IconRight = My.Resources.eye__1_1
            txt_pw.UseSystemPasswordChar = True
        End If
    End Sub
    Public Sub SetSecurityProtocol()

        '' Menambahkan baris ini untuk memastikan TLS 1.2 digunakan
        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


        ' Menggunakan nilai numerik untuk TLS 1.2
        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
    End Sub

    Private Sub btn_login_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_login.Click

        If txt_user.Text = "" And txt_pw.Text = "" Then

            msg_eror_username.Visible = True
            msg_eror_pw.Visible = True
            Me.txt_user.Focus()

        ElseIf txt_pw.Text = "" Then
            msg_eror_pw.Visible = True
            Me.txt_pw.Focus()

        ElseIf txt_user.Text = "" Then
            msg_eror_username.Visible = True
            Me.txt_user.Focus()
        Else
            Dim apiUrl As String = "https://praktikum-cpanel-unbin.com/golang_api/delimart_api/login"
            Dim requestData As String = "username=" & txt_user.Text.Trim() & "&password=" & txt_pw.Text.Trim()

            Try

                SetSecurityProtocol()

                Dim request As HttpWebRequest = WebRequest.Create(apiUrl)
                request.Method = "POST"
                request.ContentType = "application/x-www-form-urlencoded"
                ' Tambahkan header khusus untuk menentukan platform
                request.Headers.Add("X-Platform", "desktop")

                Using writer As New StreamWriter(request.GetRequestStream())
                    writer.Write(requestData)
                End Using

                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseContent As String = reader.ReadToEnd()
                    Dim responseData As JObject = JObject.Parse(responseContent)

                    If response.StatusCode = HttpStatusCode.OK Then
                        Dim kdPegawai As String = responseData("kd_pegawai").ToString()
                        Dim namaPegawai As String = responseData("nama_pegawai").ToString()
                        Dim kdRole As Integer = Integer.Parse(responseData("kd_role").ToString())
                        Dim live As Integer = Integer.Parse(responseData("live").ToString())

                        MsgBox("Login berhasil, Selamat bekerja !", MsgBoxStyle.Information, "DeliMart")

                        Form_Transaksi.lbl_id.Text = kdPegawai
                        Form_Transaksi.lbl_nama.Text = namaPegawai
                        txt_user.Text = ""
                        txt_pw.Text = ""
                        Me.Hide()
                        Form_Transaksi.Show()
                    Else
                        Dim errorMessage As String = responseData("error").ToString()
                        MsgBox("Login gagal: " & errorMessage, MsgBoxStyle.Critical, "Error")
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

        End If
    End Sub

    Private Sub txt_user_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_user.TextChanged
        If txt_user.Text <> "" Then
            msg_eror_username.Visible = False
        End If
    End Sub

    Private Sub txt_pw_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_pw.TextChanged
        If txt_pw.Text <> "" Then
            msg_eror_pw.Visible = False
        End If
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        txt_user.Text = ""
        txt_pw.Text = ""
        msg_eror_username.Visible = False
        msg_eror_pw.Visible = False
    End Sub

    Private Sub Form_Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txt_user.Focus()
    End Sub
   
End Class
