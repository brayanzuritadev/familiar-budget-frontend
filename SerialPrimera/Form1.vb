Imports System.Data
Imports System.Data.OleDb
Public Class Form1
    Dim cadena As New OleDbConnection
    Dim comando As New OleDbCommand
    Dim dato, resultado As String
    Dim tiempo, tiempoSeleccionado, disparosSeleccionados, tiempoObtenido, a As Integer
    Dim minutos, segundos, minutoObtenido, segundoObtenido As Integer
    Dim puertoSeleccionado As Object
    Dim dispositivos(11), indexDispositivos(11) As Integer
    Dim estadoDispositivos(11), listoDispositivos(11) As Integer
    Dim campoNombreBD(11), campoSerieBD(11), campoTiempoBD(11) As String
    Dim cantPuntos(11), cantDisparos(11), puntosA1(11), puntosA2(11), puntosB(11), puntosC(11), puntosD(11), puntosE(11) As Integer
    Dim cantRows As Integer
    Dim cantParticipantes, contDispositivosListos As Integer
    ' Reiniciar variables
    Public Function Reiniciar_Variables() As Integer
        For disp As Integer = 1 To 10
            estadoDispositivos(disp) = 0
            listoDispositivos(disp) = 0
            cantPuntos(disp) = 0
            cantDisparos(disp) = 0
            dtgvParticipantes.Rows(indexDispositivos(disp)).Cells(3).Value = "0"
            dtgvParticipantes.Rows(indexDispositivos(disp)).Cells(4).Value = "0"
            puntosA1(disp) = 0
            puntosA2(disp) = 0
            puntosB(disp) = 0
            puntosC(disp) = 0
            puntosD(disp) = 0
            puntosE(disp) = 0
        Next
        ' Limpiar campos de la tabla
        cantRows = dtgvParticipantes.DisplayedRowCount(1) - 1
        For j As Integer = 0 To cantRows
            dtgvParticipantes.Rows(j).Cells(3).Value = "0"
            dtgvParticipantes.Rows(j).Cells(4).Value = "0"
            dtgvParticipantes.Rows(j).Cells(5).Value = ""
        Next
    End Function
    ' ComboBox de puertos: Click -> Mostrar puertos disponibles
    Private Sub ComboBox1_Click(sender As Object, e As EventArgs) Handles ComboBox1.Click
        ComboBox1.Items.Clear()
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next
    End Sub
    ' ComboBox de puertos: Seleccion -> Leer puerto seleccionado
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex > -1 Then
            puertoSeleccionado = ComboBox1.SelectedItem
        End If
    End Sub
    ' Boton de Conectar a puerto COM
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Conectar" Then
            CheckForIllegalCrossThreadCalls = False
            SerialPort1.PortName = puertoSeleccionado
            SerialPort1.Open()
            If SerialPort1.IsOpen = True Then
                Button1.Text = "Deconectar"
                Label3.Text = "Conectado"
                Button2.Enabled = True
                ' Desactivar en todos los dispositivos
                For index As Integer = 0 To 9
                    SerialPort1.Write(Convert.ToString(index))
                    SerialPort1.Write("a")
                Next
            End If
        Else
            SerialPort1.Close()
            SerialPort1.Dispose()
            If SerialPort1.IsOpen = False Then
                Timer1.Enabled = False
                Button1.Text = "Conectar"
                Label3.Text = "Desconectado"
                Button2.Enabled = False
                Button2.Text = "INICIAR"
                Label7.Text = "Sistema apagado"
                Label7.BackColor = Color.Red
                tiempo = 0
                Label10.Text = ""
                Label17.Text = ""
                Reiniciar_Variables()
            End If
        End If
    End Sub
    ' Temporizador
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        tiempo = tiempo - 1
        minutos = tiempo \ 60
        segundos = tiempo - (minutos * 60)
        Label10.Text = minutos
        Label17.Text = segundos
        If tiempo = 0 Then
            ' Tiempo obtenido de los que no completaron los disparos
            tiempoObtenido = tiempoSeleccionado - tiempo
            minutoObtenido = tiempoObtenido \ 60
            segundoObtenido = tiempoObtenido - (minutoObtenido * 60)
            Dim tiempoBD As String = minutoObtenido & " min con " & segundoObtenido & " seg"
            For disp As Integer = 0 To 9
                If listoDispositivos(disp + 1) = 0 Then
                    campoTiempoBD(indexDispositivos(disp + 1)) = tiempoBD
                End If
            Next
            Button2.Text = "INICIAR"
            Label7.Text = "Sistema apagado"
            Label7.BackColor = Color.Red
            Label5.Text = 0
            Timer1.Enabled = False
            Label14.Text = 0
            ' Guardar en base de datos
            resultado = MessageBox.Show("¿Deseas registrar los resultados en la base de datos?", "Confirmacion de registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultado = DialogResult.Yes Then
                Try
                    cadena.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\EL SER EXCELENTE\Desktop\BDserial.accdb"
                    'cadena.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\OvidioDj\Desktop\TRABAJOS\Sistema RF Modificacion V2\BDserial.accdb"
                    cadena.Open()
                    For i As Integer = 0 To 9
                        If estadoDispositivos(i + 1) = 1 Then
                            Dim d As Integer = dispositivos(i)
                            comando = New OleDbCommand("INSERT INTO tabla(Nombre,Serie,Tiempo,Disparos,[A1=5],[A2=5],[B=4],[C=3],[D=2],[E=1],TotalPuntos) VALUES('" & campoNombreBD(i) & "','" & campoSerieBD(i) & "','" & campoTiempoBD(i) & "','" & cantDisparos(d) & "','" & puntosA1(d) & "','" & puntosA2(d) & "','" & puntosB(d) & "','" & puntosC(d) & "','" & puntosD(d) & "','" & puntosE(d) & "','" & cantPuntos(d) & "')", cadena)
                            comando.ExecuteNonQuery()
                        End If
                    Next
                    'comando = New OleDbCommand("INSERT INTO tabla(Nombre,Serie,Tiempo,Disparos,[A1=5],[A2=5],[B=4],[C=3],[D=2],[E=1],TotalPuntos) VALUES('" & campoNombreBD(i) & "','" & campoSerieBD(i) & "','" & campoTiempoBD(i) & "','" & cantDisparos(d) & "','" & puntosA1(d) & "','" & puntosA2(d) & "','" & puntosB(d) & "','" & puntosC(d) & "','" & puntosD(d) & "','" & puntosE(d) & "','" & cantPuntos(d) & "')", cadena)
                    'comando.ExecuteNonQuery()
                    MsgBox("Valores registrados", vbInformation, "Aviso")
                Catch ex As Exception
                    MsgBox("No se pudieron registrar los valores", vbCritical, "Aviso")
                End Try
                cadena.Close()
            End If
            ' Desactivar en todos los dispositivos
            For disp As Integer = 0 To 9
                If estadoDispositivos(disp + 1) = 1 Then
                    SerialPort1.Write(Convert.ToString(disp))
                    SerialPort1.Write("a")
                End If
            Next
            Reiniciar_Variables()
            Label10.Text = 0
            Label17.Text = 0
        End If
    End Sub
    ' Boton de Iniciar
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "INICIAR" Then
            Button2.Text = "TERMINAR"
            Label7.Text = "Sistema funcionando"
            Label7.BackColor = Color.Green
            If TextBox2.Text = "" Or TextBox2.Text = " " Then
                minutos = 0
            Else
                minutos = TextBox2.Text
            End If
            If TextBox3.Text = "" Or TextBox3.Text = " " Then
                segundos = 0
            Else
                segundos = TextBox3.Text
            End If
            tiempo = minutos * 60
            tiempo = tiempo + segundos
            tiempoSeleccionado = tiempo
            Label10.Text = minutos
            Label17.Text = segundos
            disparosSeleccionados = Convert.ToInt16(TextBox1.Text)
            Timer1.Enabled = True
            ' Leer DataGridView
            cantRows = dtgvParticipantes.DisplayedRowCount(1) - 1
            contDispositivosListos = 0
            cantParticipantes = 0
            For index As Integer = 0 To cantRows
                ' Leer numero del dispositivo
                Dim dis As Integer = Convert.ToInt32(dtgvParticipantes.Rows(index).Cells(2).Value)
                dispositivos(index) = dis
                indexDispositivos(dispositivos(index)) = index
                If (dis >= 1) And (dis <= 10) Then
                    estadoDispositivos(dis) = 1 ' Estado del dispositivo activado
                    cantParticipantes = cantParticipantes + 1
                    campoNombreBD(index) = Convert.ToString(dtgvParticipantes.Rows(index).Cells(0).Value)
                    campoSerieBD(index) = Convert.ToString(dtgvParticipantes.Rows(index).Cells(1).Value)
                    dtgvParticipantes.Rows(index).Cells(5).Value = "Activo"
                    dtgvParticipantes.Rows(index).Cells(3).Value = "0"
                    dtgvParticipantes.Rows(index).Cells(4).Value = "0"
                    ' Enviar por UART la activacion
                    SerialPort1.Write(Convert.ToString(dis - 1))
                    SerialPort1.Write("A")
                Else
                    estadoDispositivos(dis) = 0 ' Estado del dispositivo desactivado
                    dtgvParticipantes.Rows(index).Cells(5).Value = ""
                End If
            Next
            Label4.Text = Convert.ToString(cantParticipantes) + " participantes"
        Else
            ' Desactivar en todos los dispositivos
            For disp As Integer = 0 To 9
                If estadoDispositivos(disp + 1) = 1 Then
                    SerialPort1.Write(Convert.ToString(disp))
                    SerialPort1.Write("a")
                End If
            Next
            Button2.Text = "INICIAR"
            Label7.Text = "Sistema apagado"
            Label7.BackColor = Color.Red
            Timer1.Enabled = False
            tiempo = 0
            Label10.Text = ""
            Label17.Text = 0
            Reiniciar_Variables()
        End If
    End Sub
    ' Rutina de recepcion serial
    Private Sub SerialPort1_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        If SerialPort1.IsOpen = True Then
            dato = SerialPort1.ReadTo("z")
            Dim datoArray() As String = Split(dato)
            Dim numDispositivRec As Integer = Convert.ToInt32(datoArray(0)) + 1
            Dim puntosRec As Integer = Convert.ToInt32(datoArray(1))
            Dim auxPuntos As Integer
            If estadoDispositivos(numDispositivRec) = 1 And listoDispositivos(numDispositivRec) = 0 Then
                If puntosRec = 99 Then
                Else
                    If puntosRec = 1 Then           ' Punto en A1
                        puntosA1(numDispositivRec) = puntosA1(numDispositivRec) + 1
                        auxPuntos = 5
                    ElseIf puntosRec = 2 Then       ' Punto en A2
                        puntosA2(numDispositivRec) = puntosA2(numDispositivRec) + 1
                        auxPuntos = 5
                    ElseIf puntosRec = 3 Then       ' Punto en B
                        puntosB(numDispositivRec) = puntosB(numDispositivRec) + 1
                        auxPuntos = 4
                    ElseIf puntosRec = 4 Then       ' Punto en C
                        puntosC(numDispositivRec) = puntosC(numDispositivRec) + 1
                        auxPuntos = 3
                    ElseIf puntosRec = 5 Then       ' Punto en D
                        puntosD(numDispositivRec) = puntosD(numDispositivRec) + 1
                        auxPuntos = 2
                    ElseIf puntosRec = 6 Then       ' Punto en E
                        puntosE(numDispositivRec) = puntosE(numDispositivRec) + 1
                        auxPuntos = 1
                    End If
                    cantPuntos(numDispositivRec) = cantPuntos(numDispositivRec) + auxPuntos
                    dtgvParticipantes.Rows(indexDispositivos(numDispositivRec)).Cells(4).Value = Convert.ToString(cantPuntos(numDispositivRec))
                    cantDisparos(numDispositivRec) = cantDisparos(numDispositivRec) + 1
                    dtgvParticipantes.Rows(indexDispositivos(numDispositivRec)).Cells(3).Value = Convert.ToString(cantDisparos(numDispositivRec))

                    If cantDisparos(numDispositivRec) = disparosSeleccionados Then
                        listoDispositivos(numDispositivRec) = 1
                        ' Tiempo obtenido
                        tiempoObtenido = tiempoSeleccionado - tiempo
                        minutoObtenido = tiempoObtenido \ 60
                        segundoObtenido = tiempoObtenido - (minutoObtenido * 60)
                        Dim tiempoBD As String = minutoObtenido & " min con " & segundoObtenido & " seg"
                        campoTiempoBD(indexDispositivos(numDispositivRec)) = tiempoBD
                        ' Desactivar sistema para ese dispositivo
                        SerialPort1.Write(Convert.ToString(numDispositivRec - 1))
                        SerialPort1.Write("a")
                        ' Incrementar cantidad de terminados
                        contDispositivosListos = contDispositivosListos + 1
                        If contDispositivosListos = cantParticipantes Then
                            Button2.Text = "INICIAR"
                            Label7.Text = "Sistema apagado"
                            Label7.BackColor = Color.Red
                            Label5.Text = 0
                            Timer1.Enabled = False
                            Label14.Text = 0
                            ' Guardar en base de datos
                            resultado = MessageBox.Show("¿Deseas registrar los resultados en la base de datos?", "Confirmacion de registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If resultado = DialogResult.Yes Then
                                Try
                                    cadena.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\EL SER EXCELENTE\Desktop\BDserial.accdb"
                                    'cadena.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\Users\OvidioDj\Desktop\TRABAJOS\Sistema RF Modificacion V2\BDserial.accdb"
                                    cadena.Open()
                                    For i As Integer = 0 To 9
                                        If estadoDispositivos(i + 1) = 1 Then
                                            Dim d As Integer = dispositivos(i)
                                            comando = New OleDbCommand("INSERT INTO tabla(Nombre,Serie,Tiempo,Disparos,[A1=5],[A2=5],[B=4],[C=3],[D=2],[E=1],TotalPuntos) VALUES('" & campoNombreBD(i) & "','" & campoSerieBD(i) & "','" & campoTiempoBD(i) & "','" & cantDisparos(d) & "','" & puntosA1(d) & "','" & puntosA2(d) & "','" & puntosB(d) & "','" & puntosC(d) & "','" & puntosD(d) & "','" & puntosE(d) & "','" & cantPuntos(d) & "')", cadena)
                                            comando.ExecuteNonQuery()
                                        End If
                                    Next
                                    'comando = New OleDbCommand("INSERT INTO tabla(Nombre,Serie,Tiempo,Disparos,[A1=5],[A2=5],[B=4],[C=3],[D=2],[E=1],TotalPuntos) VALUES('" & campoNombreBD(i) & "','" & campoSerieBD(i) & "','" & campoTiempoBD(i) & "','" & cantDisparos(d) & "','" & puntosA1(d) & "','" & puntosA2(d) & "','" & puntosB(d) & "','" & puntosC(d) & "','" & puntosD(d) & "','" & puntosE(d) & "','" & cantPuntos(d) & "')", cadena)
                                    'comando.ExecuteNonQuery()
                                    MsgBox("Valores registrados", vbInformation, "Aviso")
                                Catch ex As Exception
                                    MsgBox("No se pudieron registrar los valores", vbCritical, "Aviso")
                                End Try
                                cadena.Close()
                            End If
                            ' Desactivar en todos los dispositivos
                            For disp As Integer = 0 To 9
                                If estadoDispositivos(disp + 1) = 1 Then
                                    SerialPort1.Write(Convert.ToString(disp))
                                    SerialPort1.Write("a")
                                End If
                            Next
                            Reiniciar_Variables()
                            Label10.Text = 0
                            Label17.Text = 0
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class
