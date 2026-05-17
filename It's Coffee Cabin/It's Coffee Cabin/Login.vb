Public Class Login
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Parent = PictureBox3
        Label3.BackColor = Color.Transparent
        Label4.Parent = PictureBox3
        Label4.BackColor = Color.Transparent
        Label5.Parent = PictureBox3
        Label5.BackColor = Color.Transparent
        PictureBox2.Parent = PictureBox3
        PictureBox2.BackColor = Color.Transparent
        PasswordTb.PasswordChar = "*"

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If UnameTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Enter Username and Password")
        ElseIf UnameTb.Text = "Admin" And PasswordTb.Text = "Password" Then
            Dim Obj = New Items
            Obj.Show()
            Me.Hide()
        Else
            MsgBox("Incorrect Username or Password")
            UnameTb.Text = ""
            PasswordTb.Text = ""
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim Obj = New Orders
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Application.Exit()
    End Sub
End Class
