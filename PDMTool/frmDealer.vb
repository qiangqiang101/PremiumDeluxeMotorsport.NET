Imports System.Xml

Public Class frmDealer

    Public currentFile As String = "Untitled"
    Public currentFilePath As String = Nothing
    Public Parameters As String() = {"[name]", "[price]", "[model]", "[gxt]", "[make]"}
    Public items As New ListViewItem()

    'Private Sub ReadLanguage()
    '    Dim file As String = (Application.StartupPath & "\Languages\" & My.Settings.Language & ".cfg")
    '    Text = ReadCfgValue("TabShopping", file)
    '    FileToolStripMenuItem.Text = ReadCfgValue("ToolStripFile", file)
    '    FileToolStripMenuItem.ToolTipText = ReadCfgValue("ToolStripFile", file)
    '    OpenToolStripMenuItem.Text = ReadCfgValue("ToolStripOpen", file)
    '    OpenToolStripMenuItem.ToolTipText = ReadCfgValue("ToolStripOpen", file)
    '    SaveToolStripMenuItem.Text = ReadCfgValue("SaveButton", file)
    '    SaveToolStripMenuItem.ToolTipText = ReadCfgValue("SaveButton", file)
    '    NewToolStripMenuItem.Text = ReadCfgValue("ToolStripNew", file)
    '    NewToolStripMenuItem.ToolTipText = ReadCfgValue("ToolStripNew", file)
    '    EditToolStripMenuItem1.Text = ReadCfgValue("ToolStripEdit", file)
    '    EditToolStripMenuItem1.ToolTipText = ReadCfgValue("ToolStripEdit", file)
    '    DeleteToolStripMenuItem1.Text = ReadCfgValue("ToolStripDelete", file)
    '    DeleteToolStripMenuItem1.ToolTipText = ReadCfgValue("ToolStripDelete", file)
    '    chName.Text = ReadCfgValue("ListName", file)
    '    chPrice.Text = ReadCfgValue("ListPrice", file)
    '    chModel.Text = ReadCfgValue("ListModel", file)
    '    chGXT.Text = ReadCfgValue("ListGXT", file)
    '    chBrand.Text = ReadCfgValue("ListBrand", file)
    '    chCategory.Text = ReadCfgValue("ListCategory", file)
    '    chDesc.Text = ReadCfgValue("ListDesc", file)
    '    'ReadCfgValue("", file)
    'End Sub

    Private Sub ReadFiles(File As String)
        Try
            Dim Format As New Reader(File, Parameters)
            For i As Integer = 0 To Format.Count - 1
                items = lvCars.Items.Add(Format(i)("name"))
                With items
                    .SubItems.Add(Format(i)("price"))
                    .SubItems.Add(Format(i)("model"))
                    .SubItems.Add(Format(i)("gxt"))
                    .SubItems.Add(Format(i)("make"))
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub WriteFiles(File As String)
        Try
            IO.File.WriteAllText(File, "")
            Dim sw As System.IO.StreamWriter
            sw = My.Computer.FileSystem.OpenTextFileWriter(File, True)
            For Each item As ListViewItem In lvCars.Items
                sw.WriteLine("[name]" & item.Text & "[price]" & item.SubItems(1).Text & "[model]" & item.SubItems(2).Text & "[gxt]" & item.SubItems(3).Text & "[make]" & item.SubItems(4).Text)
            Next
            sw.Close()

            Dim textToSave As TextBox = New TextBox
            textToSave.Multiline = True
            textToSave.Text = IO.File.ReadAllText(File)
            Dim lineList As New List(Of String)
            For Each line As String In textToSave.Lines
                If line.Trim <> "" Then lineList.Add(line)
            Next
            textToSave.Lines = lineList.ToArray
            IO.File.WriteAllText(File, textToSave.Text)

            MsgBox("Save Successfully", MsgBoxStyle.Information, "PDM Tool")
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EditToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem1.Click
        Try
            If lvCars.SelectedItems.Count = 0 Then
                Exit Sub
            Else
                Dim NewForm As frmDEdit = New frmDEdit
                NewForm.Show()
                NewForm.EditMode = True
                NewForm.LVItem = lvCars.SelectedItems.Item(0)
                NewForm.xListView = lvCars
                NewForm.Text = "Edit: " & lvCars.SelectedItems.Item(0).Text
                NewForm.txtName.Text = lvCars.SelectedItems.Item(0).Text
                NewForm.numPrice.Value = lvCars.SelectedItems.Item(0).SubItems(1).Text
                NewForm.txtModel.Text = lvCars.SelectedItems.Item(0).SubItems(2).Text
                NewForm.txtGXT.Text = lvCars.SelectedItems.Item(0).SubItems(3).Text
                NewForm.cmbBrand.Text = lvCars.SelectedItems.Item(0).SubItems(4).Text
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "PDM Tool")
        End Try
    End Sub

    Private Sub lvCars_DoubleClick(sender As Object, e As EventArgs) Handles lvCars.DoubleClick
        EditToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        EditToolStripMenuItem1.PerformClick()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Try
            Dim NewForm As frmDEdit = New frmDEdit
            NewForm.Show()
            NewForm.EditMode = False
            NewForm.xListView = lvCars
            NewForm.Text = "Untitled"
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "PDM Tool")
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            If lvCars.SelectedItems.Count = 0 Then
                Exit Sub
            Else
                If MsgBox("Are you sure you want to delete " & lvCars.SelectedItems.Item(0).Text & "?", MsgBoxStyle.YesNo, "Delete") = MsgBoxResult.Yes Then
                    For Each i As ListViewItem In lvCars.SelectedItems
                        lvCars.Items.Remove(i)
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "PDM Tool")
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        DeleteToolStripMenuItem.PerformClick()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            If currentFilePath = Nothing Then
                SaveAsToolStripMenuItem.PerformClick()
            Else
                WriteFiles(currentFilePath)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "PDM Tool")
        End Try
    End Sub

    Private Sub frmDealer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ReadLanguage()
    End Sub

    Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click
        frmImport.Show()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Try
            If ofDialog.ShowDialog = DialogResult.OK Then
                lvCars.Items.Clear()
                For Each file As String In ofDialog.FileNames
                    ReadFiles(file)
                    currentFile = IO.Path.GetFileNameWithoutExtension(file)
                    currentFilePath = file
                    Me.Text = String.Format("{0} - Dealership Editor", currentFile)

                    NewToolStripMenuItem.Enabled = True
                    EditToolStripMenuItem1.Enabled = True
                    DeleteToolStripMenuItem1.Enabled = True
                    SaveToolStripMenuItem.Enabled = True
                    ImportToolStripMenuItem.Enabled = True
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "PDM Tool")
        End Try
    End Sub

    Private Sub NewToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem1.Click
        currentFile = "Untitled"
        currentFilePath = Nothing
        Me.Text = String.Format("{0} - Dealership Editor", currentFile)
        lvCars.Items.Clear()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Try
            If sfDialog.ShowDialog = DialogResult.OK Then
                currentFile = IO.Path.GetFileNameWithoutExtension(sfDialog.FileName)
                currentFilePath = sfDialog.FileName
                Me.Text = String.Format("{0} - Dealership Editor", currentFile)
                WriteFiles(sfDialog.FileName)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "PDM Tool")
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub
End Class