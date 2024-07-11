Module Config
    Public kon As New ADODB.Connection
    Public rek As New ADODB.Recordset
    Public Sub buka()
        kon.Open("Dsn=db_ipat_uas")
    End Sub
    Public Sub tutup()
        kon.Close()
    End Sub
End Module
