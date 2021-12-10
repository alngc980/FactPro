Public Class EnviarCorreo
    Private Sub btnenviar_Click(sender As Object, e As EventArgs) Handles btnenviar.Click

        Dim rest = Util.EnviarCorreo(txtdestinatarios.Text, txtasunto.Text, txtdescripcion.Text, txtadjunto.Text, txtcorreo.Text, txtcorreo.Text, txtclave.Text, 587, "smtp.gmail.com")
        If rest Then
            MsgBox("Correo Enviado")
        Else
            MsgBox("No se pudo enviar el correo")
        End If
    End Sub

    Private Sub txtadjunto_Click(sender As Object, e As EventArgs) Handles txtadjunto.Click
        Dim choofdlog = New OpenFileDialog()
        choofdlog.Filter = "All Files (*.*)|*.*"
        choofdlog.FilterIndex = 1
        choofdlog.Multiselect = True

        If choofdlog.ShowDialog() = DialogResult.OK Then
            txtadjunto.Text = txtadjunto.Text & ";" & choofdlog.FileName
        End If
    End Sub
End Class