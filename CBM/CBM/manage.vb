Imports MySql.Data.MySqlClient

Public Class manage
    Dim conn As MySqlConnection = New MySqlConnection("Server=Localhost; User ID=root; Password=''; Database=cbmdb")

    Private Sub manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dashboard.Hide()
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim cmd As MySqlCommand = New MySqlCommand("SELECT * FROM managetbl", conn)
        Dim dt As DataTable = New DataTable()
        Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(cmd)
        adapter.Fill(dt)
        DataGridView1.DataSource = dt
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
        Dim query As String = "INSERT INTO managetbl (Category, Item, Stock, Manufacturer) VALUES (@Category, @Item, @Stock, @Manufacturer)"
        Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@Category", CategoryCB.Text)
        cmd.Parameters.AddWithValue("@Item", ItemTB.Text)
        cmd.Parameters.AddWithValue("@Stock", StockTB.Text)
        cmd.Parameters.AddWithValue("@Manufacturer", ManufacturerTB.Text)
        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()
        LoadData()
        ClearFields()
    End Sub

    Private Sub EditBTN_Click(sender As Object, e As EventArgs) Handles EditBTN.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to edit this item?", "Edit Item", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Dim query As String = "UPDATE managetbl SET Category=@Category, Item=@Item, Stock=@Stock, Manufacturer=@Manufacturer WHERE ItemID=@ItemID"
            Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ItemID", DataGridView1.CurrentRow.Cells("ItemID").Value)
            cmd.Parameters.AddWithValue("@Category", CategoryCB.Text)
            cmd.Parameters.AddWithValue("@Item", ItemTB.Text)
            cmd.Parameters.AddWithValue("@Stock", StockTB.Text)
            cmd.Parameters.AddWithValue("@Manufacturer", ManufacturerTB.Text)
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            LoadData()
            ClearFields()
        End If
    End Sub

    Private Sub DeleteBTN_Click(sender As Object, e As EventArgs) Handles DeleteBTN.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Delete Item", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Dim query As String = "DELETE FROM managetbl WHERE ItemID=@ItemID"
            Dim cmd As MySqlCommand = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ItemID", DataGridView1.CurrentRow.Cells("ItemID").Value)
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            LoadData()
            ClearFields()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            CategoryCB.Text = row.Cells("Category").Value.ToString()
            ItemTB.Text = row.Cells("Item").Value.ToString()
            StockTB.Text = row.Cells("Stock").Value.ToString()
            ManufacturerTB.Text = row.Cells("Manufacturer").Value.ToString()
        End If
    End Sub

    Private Sub ClearFields()
        CategoryCB.Text = ""
        ItemTB.Text = ""
        StockTB.Text = ""
        ManufacturerTB.Text = ""
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
