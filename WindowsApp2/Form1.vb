Imports System.Net.Sockets
Imports System.Threading
Imports System.IO
Imports System.Net

Public Class Form1
    Dim listener As New TcpListener(IPAddress.Any, 4305)
    Dim client As TcpClient
    Dim Message As String = ""





    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim listthread As New Thread(New ThreadStart(AddressOf Listening))
        listthread.Start()
    End Sub
    Private Sub Listening()
        listener.Start()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        listener.Stop()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        client = New TcpClient(TextBox1.Text, 4305)
        Dim writer As New StreamWriter(client.GetStream())

        writer.Write(TextBox2.Text)

    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If listener.Pending = True Then
            Message = ""
            client = listener.AcceptTcpClient()

            Dim reader As New StreamReader(client.GetStream)
            While reader.Peek > -1
                Message = Message + Convert.ToChar(reader.Read()).ToString

            End While
            TextBox3.Text = TextBox3.Text & Message & vbCrLf




        Else : End If
    End Sub
End Class
