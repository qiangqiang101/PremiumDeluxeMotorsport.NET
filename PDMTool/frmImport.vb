Imports System.Xml

Public Class frmImport
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            If ofDialog.ShowDialog = DialogResult.OK Then
                lblSelected.Text = ofDialog.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "PDM Tool")
        End Try
    End Sub

    Function UppercaseFirstLetter(ByVal val As String) As String
        ' Test for nothing or empty.
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        ' Convert to character array.
        Dim array() As Char = val.ToCharArray

        ' Uppercase first character.
        array(0) = Char.ToUpper(array(0))

        ' Return new string.
        Return New String(array)
    End Function

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        If lblSelected.Text = "No File Selected" Then Exit Sub
        Dim vehicles As New XmlDocument()
        vehicles.Load(ofDialog.FileName)
        Using items As XmlNodeList = vehicles.DocumentElement.SelectNodes("/CVehicleModelInfo__InitDataList/InitDatas/Item")
            For Each item As XmlNode In items
                Dim model As String = item.SelectSingleNode("modelName").InnerText
                Dim brand As String = item.SelectSingleNode("vehicleMakeName").InnerText.ToLower
                Dim vehName As String = item.SelectSingleNode("gameName").InnerText.ToLower
                Dim vehClass As String = item.SelectSingleNode("vehicleClass").InnerText
                Dim itemName As String = String.Format("{0} {1}", UppercaseFirstLetter(brand), UppercaseFirstLetter(vehName))
                If brand = Nothing Then itemName = UppercaseFirstLetter(vehName)

                Select Case True
                    Case rbAll.Checked
                        AddItem(itemName, model, vehName, brand)
                    Case rbClassic.Checked
                        If vehClass = "VC_SPORT_CLASSIC" Then AddItem(itemName, model, vehName, brand)
                    Case rbCompact.Checked
                        If vehClass = "VC_COMPACT" Then AddItem(itemName, model, vehName, brand)
                    Case rbCoupe.Checked
                        If vehClass = "VC_COUPE" Then AddItem(itemName, model, vehName, brand)
                    Case rbMotorcycle.Checked
                        If vehClass = "VC_MOTORCYCLE" Then AddItem(itemName, model, vehName, brand)
                    Case rbMuscle.Checked
                        If vehClass = "VC_MUSCLE" Then AddItem(itemName, model, vehName, brand)
                    Case rbOffroad.Checked
                        If vehClass = "VC_OFF_ROAD" Then AddItem(itemName, model, vehName, brand)
                    Case rbSedan.Checked
                        If vehClass = "VC_SEDAN" Then AddItem(itemName, model, vehName, brand)
                    Case rbSport.Checked
                        If vehClass = "VC_SPORT" Then AddItem(itemName, model, vehName, brand)
                    Case rbSuper.Checked
                        If vehClass = "VC_SUPER" Then AddItem(itemName, model, vehName, brand)
                    Case rbSuv.Checked
                        If vehClass = "VC_SUV" Then AddItem(itemName, model, vehName, brand)
                    Case rbUtility.Checked
                        If vehClass = "VC_UTILITY" Then AddItem(itemName, model, vehName, brand)
                    Case rbVan.Checked
                        If vehClass = "VC_VAN" Then AddItem(itemName, model, vehName, brand)
                End Select
            Next
        End Using
        Me.Close()
    End Sub

    Private Sub AddItem(name As String, model As String, gxt As String, make As String)
        Dim lvitem As New ListViewItem()
        lvitem = frmDealer.lvCars.Items.Add(name)
        With lvitem
            .SubItems.Add(0)
            .SubItems.Add(model)
            .SubItems.Add(gxt)
            .SubItems.Add(make)
        End With
    End Sub
End Class