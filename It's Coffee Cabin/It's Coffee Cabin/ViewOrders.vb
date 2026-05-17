Imports System.Data.SqlClient
Public Class ViewOrders
    Dim cn As SqlConnection
    Private Sub DisplayBill()
        cn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TEJAS R SHINDE\OneDrive\Documents\CafeVbDb.mdf;Integrated Security=True;Connect Timeout=30")
        cn.Open()
        Dim query = "select * from OrderTbl"
        Dim cmd = New SqlCommand(query, cn)
        Dim adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        OrdersDGV.DataSource = ds.Tables(0)
        cn.Close()
    End Sub
    Private Sub ViewOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'CafeVbDbDataSet1.OrderTbl' table. You can move, or remove it, as needed.
        Me.OrderTblTableAdapter.Fill(Me.CafeVbDbDataSet1.OrderTbl)
        DisplayBill()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Obj = New Orders
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        OrderReport.Show()
    End Sub
End Class