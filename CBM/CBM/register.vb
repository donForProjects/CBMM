Imports MySql.Data.MySqlClient

Public Class register
    Dim conn As MySqlConnection = New MySqlConnection("Server=Localhost; User ID=root; Password=''; Database=cbmdb")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles RegisterBTN.Click
        RegisterUser()
    End Sub

    Private Sub RegisterUser()
        Try
            conn.Open()
            Dim query As String = "INSERT INTO user (username, password) VALUES (@username, @password)"
            Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@username", UsernameTB.Text)
            cmd.Parameters.AddWithValue("@password", PasswordTB.Text)
            cmd.ExecuteNonQuery()
            MsgBox("Registered successfully!")


            UsernameTB.Clear()
            PasswordTB.Clear()
        Catch ex As Exception
            MsgBox("Error registering user: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub register_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        login.Hide()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles LoginLBL.Click
        Me.Hide()
        login.Show()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles UsernameTB.TextChanged

    End Sub

    Private Sub PasswordTB_TextChanged(sender As Object, e As EventArgs) Handles PasswordTB.TextChanged

    End Sub
End Class
