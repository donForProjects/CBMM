Imports System.Diagnostics.Eventing
Imports System.Runtime.Remoting.Contexts
Imports MySql.Data.MySqlClient

Public Class manage
    Dim conn As MySqlConnection = New MySqlConnection("Server=Localhost; User ID=root; Password=''; Database=cbmdb; Convert Zero Datetime=True")
    Private originalValue As Object
    Private Sub manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dashboard.Hide()
        LoadData()
        fillSource(ItemTB, "Item")
        fillSource(ManufacturerTB, "Manufacturer")
        DateTime.Value = Format(Date.Now)
        Panel4.Hide()
    End Sub

    Private Sub LoadData()
        Try
            Dim cmd As MySqlCommand = New MySqlCommand("SELECT DISTINCT `Item`, `Category`, `Manufacturer`, SUM(`Stock`) AS 'Stock' FROM `managetbl` GROUP BY `Item`, `Category`, `Manufacturer`;", conn)
            Dim dt As DataTable = New DataTable()
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            adapter.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Database Error")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        login.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
        dashboard.Show()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Hide()
        accounting.Show()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Me.Hide()
        billing.Show()
    End Sub

    Private Sub AddItemBTN_Click(sender As Object, e As EventArgs) Handles AddItemBTN.Click
        If Not ItemTB.Text = "" Or Not StockTB.Text = "" Or Not ManufacturerTB.Text = "" Then
            Try
                Dim query As String = "INSERT INTO managetbl (Category, Item, Stock, Manufacturer, Date) VALUES (@Category, @Item, @Stock, @Manufacturer, @Date)"
                Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Category", CategoryCB.Text)
                cmd.Parameters.AddWithValue("@Item", ItemTB.Text)
                cmd.Parameters.AddWithValue("@Stock", StockTB.Text)
                cmd.Parameters.AddWithValue("@Manufacturer", ManufacturerTB.Text)
                cmd.Parameters.AddWithValue("@Date", DateTime.Value.ToString("yyyy-MM-dd"))
                conn.Open()
                cmd.ExecuteNonQuery()
                LoadData()
                ClearFields()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Database Error")
            Finally
                MessageBox.Show("Item Added.")
                conn.Close()
            End Try
        Else
            MessageBox.Show("Make sure all textboxes are not empty.", "Error")
        End If

    End Sub

    Private Sub ClearFields()
        CategoryCB.Text = ""
        ItemTB.Text = ""
        StockTB.Text = ""
        ManufacturerTB.Text = ""
    End Sub

    Public Sub fillSource(ByRef tb As Object, ByRef col As String)
        Dim Command As New MySqlCommand
        Dim Reader As MySqlDataReader
        Dim Source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Try
            conn.Open()
            Dim Query As String = "SELECT DISTINCT `" & col & "` FROM `managetbl`"
            Console.WriteLine(Query)
            With Command
                .Connection = conn
                .CommandText = Query
            End With

            Reader = Command.ExecuteReader

            Do While Reader.Read = True
                Console.WriteLine(Reader(col))
                Source.Add(Reader(col))
            Loop
            tb.AutoCompleteCustomSource = Source
            Reader.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Panel4.Show()
        Console.WriteLine("Clicked")
        Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim Category As String = row.Cells("Category").Value.ToString()
        Dim Item As String = row.Cells("Item").Value.ToString()
        Dim Manufacturer As String = row.Cells("Manufacturer").Value.ToString()
        Dim Query As String = "SELECT * FROM `managetbl` WHERE `Item` = @Item AND `Category` = @Category AND `Manufacturer` = @Manufacturer ORDER BY `Date` ASC;"
        Console.WriteLine(Query)
        Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)
        cmd.Parameters.AddWithValue("@Item", Item)
        cmd.Parameters.AddWithValue("@Category", Category)
        cmd.Parameters.AddWithValue("@Manufacturer", Manufacturer)
        Dim dt As DataTable = New DataTable()
        Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(cmd)
        adapter.Fill(dt)
        DataGridView2.DataSource = dt
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel4.Hide()
        LoadData()
    End Sub

    'Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
    '    If MsgBox("Are you sure you want to update this data?", MsgBoxStyle.Question & MsgBoxStyle.YesNo, "MessageBox Header") = MsgBoxResult.Yes Then
    '        If DataGridView2.SelectedRows.Count > 0 Then
    '            Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
    '            Dim query As String = "UPDATE managetbl SET Item=@Item, Category=@Category, Stock=@Stock, Manufacturer=@Manufacturer, Date=@Date WHERE DeliveryID=@DeliveryID"
    '            Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
    '            cmd.Parameters.AddWithValue("@DeliveryID", selectedRow.Cells(0).Value)
    '            cmd.Parameters.AddWithValue("@Item", selectedRow.Cells(1).Value)
    '            cmd.Parameters.AddWithValue("@Category", selectedRow.Cells(2).Value)
    '            cmd.Parameters.AddWithValue("@Stock", selectedRow.Cells(3).Value)
    '            cmd.Parameters.AddWithValue("@Manufacturer", selectedRow.Cells(4).Value)
    '            cmd.Parameters.AddWithValue("@Date", selectedRow.Cells(5).Value)
    '            conn.Open()
    '            cmd.ExecuteNonQuery()
    '            conn.Close()
    '        End If
    '    End If
    'End Sub
    Private Sub DataGridView2_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView2.CellBeginEdit
        ' Store the original value of the cell
        originalValue = DataGridView2(e.ColumnIndex, e.RowIndex).Value
    End Sub

    Private Sub DataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        ' Compare the new value with the original value and prompt the user
        Dim newValue As Object = DataGridView2(e.ColumnIndex, e.RowIndex).Value
        If Not originalValue.Equals(newValue) Then
            If MsgBox("Are you sure you want to update this data?", MsgBoxStyle.Question & MsgBoxStyle.YesNo, "MessageBox Header") = MsgBoxResult.Yes Then
                ' Update the database
                If DataGridView2.SelectedRows.Count > 0 Then
                    Dim selectedRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                    Dim query As String = "UPDATE managetbl SET Item=@Item, Category=@Category, Stock=@Stock, Manufacturer=@Manufacturer, Date=@Date WHERE DeliveryID=@DeliveryID"
                    Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@DeliveryID", selectedRow.Cells("DeliveryID").Value)
                    cmd.Parameters.AddWithValue("@Item", selectedRow.Cells("Item").Value)
                    cmd.Parameters.AddWithValue("@Category", selectedRow.Cells("Category").Value)
                    cmd.Parameters.AddWithValue("@Stock", selectedRow.Cells("Stock").Value)
                    cmd.Parameters.AddWithValue("@Manufacturer", selectedRow.Cells("Manufacturer").Value)
                    cmd.Parameters.AddWithValue("@Date", selectedRow.Cells("Date").Value)
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Database Error")
                    Finally
                        conn.Close()
                    End Try
                End If
            Else
                ' Revert the cell value to the original value
                DataGridView2(e.ColumnIndex, e.RowIndex).Value = originalValue
            End If
        End If
    End Sub
    Private Sub DataGridView2_UserDeletingRow(sender As Object, e As DataGridViewRowCancelEventArgs) Handles DataGridView2.UserDeletingRow
        If MsgBox("Are you sure you want to delete this data?", MsgBoxStyle.Question & MsgBoxStyle.YesNo, "MessageBox Header") = MsgBoxResult.No Then
            e.Cancel = True
        Else
            If DataGridView2.SelectedRows.Count > 0 Then
                Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
                Dim query As String = "DELETE FROM managetbl WHERE DeliveryID = @DeliveryID"
                Console.WriteLine(selectedRow.Cells(0).Value)
                Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@DeliveryID", selectedRow.Cells(0).Value)
                Console.WriteLine(query)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End If
        End If
        LoadData()
    End Sub

    Private Sub DataGridView2_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView2.UserDeletedRow
        LoadData()
    End Sub
End Class
