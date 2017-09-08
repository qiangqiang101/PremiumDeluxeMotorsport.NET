Public Class frmDEdit

    Private _editMode As Boolean = True
    Private _lvItem As ListViewItem
    Private _lv As ListView

    Public Property EditMode() As Boolean
        Get
            Return _editMode
        End Get
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    Public Property LVItem() As ListViewItem
        Get
            Return _lvItem
        End Get
        Set(value As ListViewItem)
            _lvItem = value
        End Set
    End Property

    Public Property xListView() As ListView
        Get
            Return _lv
        End Get
        Set(value As ListView)
            _lv = value
        End Set
    End Property

    'Private Sub ReadLanguage()
    '    Dim file As String = (Application.StartupPath & "\Languages\" & My.Settings.Language & ".cfg")
    '    GroupBox1.Text = ReadCfgValue("GroupGeneral", file)
    '    GroupBox2.Text = ReadCfgValue("GroupImage", file)
    '    btnSave.Text = ReadCfgValue("SaveButton", file)
    '    btnClose.Text = ReadCfgValue("CloseButton", file)
    '    Label1.Text = ReadCfgValue("ListName", file)
    '    Label2.Text = ReadCfgValue("ListPrice", file)
    '    Label3.Text = ReadCfgValue("ListModel", file)
    '    Label4.Text = ReadCfgValue("ListGXT", file)
    '    Label5.Text = ReadCfgValue("ListBrand", file)
    '    Label6.Text = ReadCfgValue("ListCategory", file)
    '    Label7.Text = ReadCfgValue("ListDesc", file)
    '    'ReadCfgValue("", file)
    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If txtName.Text = Nothing Then
                MsgBox("Please enter Vehicle Name.", MsgBoxStyle.Exclamation, "SPA Tool")
            ElseIf txtModel.Text = Nothing Then
                MsgBox("Please enter Vehicle Model.", MsgBoxStyle.Exclamation, "SPA Tool")
            ElseIf txtGXT.Text = Nothing Then
                MsgBox("Please enter Model GXT.", MsgBoxStyle.Exclamation, "SPA Tool")
            Else
                If EditMode = True Then
                    LVItem.SubItems(1).Text = numPrice.Value
                    LVItem.SubItems(2).Text = txtModel.Text
                    LVItem.SubItems(3).Text = txtGXT.Text
                    LVItem.SubItems(4).Text = cmbBrand.Text
                    LVItem.Text = txtName.Text
                ElseIf EditMode = False Then
                    Dim items As New ListViewItem()
                    items = xListView.Items.Add(txtName.Text)
                    With items
                        .SubItems.Add(numPrice.Value)
                        .SubItems.Add(txtModel.Text)
                        .SubItems.Add(txtGXT.Text)
                        .SubItems.Add(cmbBrand.Text)
                    End With
                End If
            End If
            Close()
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub frmDEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ReadLanguage()
    End Sub
End Class