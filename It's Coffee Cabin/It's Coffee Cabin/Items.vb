Imports System.Data.SqlClient
Public Class Items
    Dim cn As SqlConnection
    Dim cmd As SqlCommand
    Dim str As String
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Dim Obj = New Login
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CatTb.Text = "" Then
            MsgBox("Enter The Category")
        Else
            Try
                cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
                cn.Open()
                str = New String("insert into CategoryTbl values('" & CatTb.Text & "')")
                cmd = New SqlCommand(str, cn)
                cmd.ExecuteNonQuery()
                MsgBox("Category Added")
                cn.Close()
                CatTb.Text = ""
                FillCategory()
            Catch ex As Exception
                MsgBox("An Error occured ")
            End Try
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Application.Exit()
    End Sub

    Private Sub Reset()
        ItNameTb.Text = ""
        CatCb.SelectedIndex = 0
        QuantityTb.Text = ""
        ItPriceTb.Text = ""
    End Sub
    Private Sub FillCategory()
        cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
        cn.Open()
        cmd = New SqlCommand("select * from CategoryTbl", cn)
        da = New SqlDataAdapter(cmd)
        Dim Tbl = New DataTable()
        da.Fill(Tbl)
        CatCb.DataSource = Tbl
        CatCb.DisplayMember = "CatName"
        CatCb.ValueMember = "CatName"
        cn.Close()
    End Sub
    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub Items_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'CafeVbDbDataSet.ItemTbl' table. You can move, or remove it, as needed.
        Me.ItemTblTableAdapter.Fill(Me.CafeVbDbDataSet.ItemTbl)
        FillCategory()
        DisplayItem()
    End Sub
    Private Sub DisplayItem()
        cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
        cn.Open()
        str = New String("select * from ItemTbl")
        cmd = New SqlCommand(str, cn)
        da = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(da)
        builder = New SqlCommandBuilder(da)
        Dim ds = New DataSet()
        da.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)
        cn.Close()
    End Sub
    Private Sub AddBtn_Click(sender As Object, e As EventArgs) Handles AddBtn.Click
        If CatCb.SelectedIndex = -1 Or ItNameTb.Text = "" Or ItPriceTb.Text = "" Or QuantityTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
            cn.Open()
            str = New String("insert into ItemTbl values('" & ItNameTb.Text & "', '" & CatCb.SelectedValue.ToString() & "','" & ItPriceTb.Text & "','" & QuantityTb.Text & "')")
            cmd = New SqlCommand(str, cn)
            cmd.ExecuteNonQuery()
            MsgBox("Item Added")
            cn.Close()
            Reset()
            DisplayItem()
        End If
    End Sub
    Dim key = 0
    Private Sub ItemDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles ItemDGV.CellMouseClick
        Dim row As DataGridViewRow = ItemDGV.Rows(e.RowIndex)
        ItNameTb.Text = row.Cells(1).Value.ToString
        CatCb.SelectedValue = row.Cells(2).Value.ToString
        ItPriceTb.Text = row.Cells(3).Value.ToString
        QuantityTb.Text = row.Cells(4).Value.ToString
        If ItNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Select the item to Delete")
        Else
            Try
                cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
                cn.Open()
                str = New String("DELETE FROM ItemTbl WHERE ItId = " & key)
                cmd = New SqlCommand(str, cn)
                cmd.ExecuteNonQuery()
                MsgBox("Item Deleted")
                cn.Close()
                Reset()
                DisplayItem()
            Catch ex As Exception
                MsgBox("An error occurred: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If CatCb.SelectedIndex = -1 Or ItNameTb.Text = "" Or ItPriceTb.Text = "" Or QuantityTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
                cn.Open()
                str = New String("update ItemTbl set ItName='" & ItNameTb.Text & "',ItCat='" & CatCb.SelectedValue.ToString() & "',ItPrice=" & ItPriceTb.Text & ",ItQty=" & QuantityTb.Text & " where ItId=" & key & "")
                cmd = New SqlCommand(str, cn)
                cmd.ExecuteNonQuery()
                MsgBox("Item Edited")
                cn.Close()
                Reset()
                DisplayItem()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
End Class