Imports MySql.Data.MySqlClient

Public Class login
    Dim conn As MySqlConnection = New MySqlConnection("Server=Localhost; User ID=root; Password=''; Database=cbmdb")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles LoginBTN.Click
        AuthenticateUser(UserNameTB.Text, PasswordTB.Text)
    End Sub

    Private Sub AuthenticateUser(username As String, password As String)
        Try
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM user WHERE username = @username AND password = @password"
            Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@username", username)
            cmd.Parameters.AddWithValue("@password", password)
            Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If result > 0 Then
                dashboard.Show()
                ClearTextBoxes()
            Else
                MsgBox("Invalid username or password!")
            End If
        Catch ex As Exception
            MsgBox("Error logging in: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub RegisterLbL_Click(sender As Object, e As EventArgs) Handles RegisterLbL.Click
        register.Show()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles PasswordTB.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles UserNameTB.TextChanged

    End Sub

    Private Sub ClearTextBoxes()
        UserNameTB.Text = ""
        PasswordTB.Text = ""
    End Sub
End Class
