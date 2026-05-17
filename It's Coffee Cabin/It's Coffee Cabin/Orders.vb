Imports System.Data.SqlClient
Public Class Orders
    Dim cn As SqlConnection
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim Obj = New Login
        Obj.Show()
        Me.Hide()
    End Sub
    Private Sub FillCategory()
        cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
        cn.Open()
        Dim cmd = New SqlCommand("select * from CategoryTbl", cn)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim Tbl = New DataTable()
        adapter.Fill(Tbl)
        CatCb.DataSource = Tbl
        CatCb.DisplayMember = "CatName"
        CatCb.ValueMember = "CatName"
        cn.Close()
    End Sub
    Private Sub DisplayItem()
        cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
        cn.Open()
        Dim query = "select * from ItemTbl"
        Dim cmd = New SqlCommand(query, cn)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)
        cn.Close()
    End Sub
    Private Sub FilterByCategory()
        cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
        cn.Open()
        Dim query = "select * from ItemTbl where ItCat='" & CatCb.SelectedValue.ToString() & "'"
        Dim cmd = New SqlCommand(query, cn)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)
        cn.Close()
    End Sub
    Private Sub Orders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'CafeVbDbDataSet2.ItemTbl' table. You can move, or remove it, as needed.
        Me.ItemTblTableAdapter.Fill(Me.CafeVbDbDataSet2.ItemTbl)
        DisplayItem()
        FillCategory()
    End Sub

    Private Sub CatCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CatCb.SelectionChangeCommitted
        FilterByCategory()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        DisplayItem()
    End Sub
    Dim ProdName As String
    Dim i = 0, GrdTotal = 0, price, qty
    Private Sub AddBill()
        cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
        cn.Open()
        Dim query = "insert into OrderTbl values('" & DateTime.Today.Date & "', " & GrdTotal & ")"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(query, cn)
        cmd.ExecuteNonQuery()
        MsgBox("Bill Added")
        cn.Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddBill()
        PrintPreviewDialog1.Show()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString("Its Coffee Cabin", New Font("Arial", 22), Brushes.BlueViolet, 335, 35)
        e.Graphics.DrawString("***YourBill***", New Font("Arial", 14), Brushes.BlueViolet, 350, 60)

        Dim bm As New Bitmap(Me.BillDGV.Width, Me.BillDGV.Height)
        BillDGV.DrawToBitmap(bm, New Rectangle(0, 0, Me.BillDGV.Width, Me.BillDGV.Height))
        e.Graphics.DrawImage(bm, 0, 90)
        e.Graphics.DrawString("Total Amount" + GrdTotal.ToString(), New Font("Arial", 15), Brushes.Crimson, 325, 580)
        e.Graphics.DrawString("================Thanks For Buying Visit Again================", New Font("Arial", 15), Brushes.Crimson, 130, 600)
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Application.Exit()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Obj = New ViewOrders
        Obj.Show()
        Me.Hide()
    End Sub
    Private Sub UpdateItem()
        Try
            Dim newQty = stock - Convert.ToInt32(QuantityTb.Text)
            cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
            cn.Open()
            Dim query = "update ItemTbl set ItQty=" & newQty & " where ItId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, cn)
            cmd.ExecuteNonQuery()
            MsgBox("Item Edited")
            cn.Close()
            'Reset()
            DisplayItem()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub AddBillBtn_Click(sender As Object, e As EventArgs) Handles AddBillBtn.Click
        If key = 0 Then
            MsgBox("Select An Item")
        ElseIf Convert.ToInt32(QuantityTb.Text) > stock Then
            MsgBox("Not Enough Stock")
        Else
            Dim rnum As Integer = BillDGV.Rows.Add()
            Dim total = Convert.ToInt32(QuantityTb.Text) * price
            i = i + 1
            BillDGV.Rows.Item(rnum).Cells("Column1").Value = i
            BillDGV.Rows.Item(rnum).Cells("Column2").Value = ProdName
            BillDGV.Rows.Item(rnum).Cells("Column3").Value = price
            BillDGV.Rows.Item(rnum).Cells("Column4").Value = QuantityTb.Text
            BillDGV.Rows.Item(rnum).Cells("Column5").Value = total
            GrdTotal = GrdTotal + total
            TotalLbl.Text = "Rs" + Convert.ToString(GrdTotal)
            UpdateItem()
            QuantityTb.Text = ""
            key = 0

        End If
    End Sub

    Dim key = 0, stock
    Private Sub ItemDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles ItemDGV.CellMouseClick
        Dim row As DataGridViewRow = ItemDGV.Rows(e.RowIndex)
        ProdName = row.Cells(1).Value.ToString
        price = Convert.ToInt32(row.Cells(3).Value.ToString)
        If ProdName = "" Then
            key = 0
            stock = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
            stock = Convert.ToInt32(row.Cells(4).Value.ToString)
            price = Convert.ToInt32(row.Cells(3).Value.ToString)
        End If
    End Sub
End Class