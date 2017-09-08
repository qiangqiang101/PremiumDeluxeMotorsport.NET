<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport))
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.rbClassic = New System.Windows.Forms.RadioButton()
        Me.rbCompact = New System.Windows.Forms.RadioButton()
        Me.rbCoupe = New System.Windows.Forms.RadioButton()
        Me.rbSuper = New System.Windows.Forms.RadioButton()
        Me.rbMotorcycle = New System.Windows.Forms.RadioButton()
        Me.rbMuscle = New System.Windows.Forms.RadioButton()
        Me.rbOffroad = New System.Windows.Forms.RadioButton()
        Me.rbSedan = New System.Windows.Forms.RadioButton()
        Me.rbSport = New System.Windows.Forms.RadioButton()
        Me.rbSuv = New System.Windows.Forms.RadioButton()
        Me.rbUtility = New System.Windows.Forms.RadioButton()
        Me.rbVan = New System.Windows.Forms.RadioButton()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.ofDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(12, 12)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(102, 35)
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lblSelected
        '
        Me.lblSelected.Location = New System.Drawing.Point(120, 12)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Size = New System.Drawing.Size(204, 35)
        Me.lblSelected.TabIndex = 77
        Me.lblSelected.Text = "No File Selected"
        Me.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(115, 153)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(102, 35)
        Me.btnImport.TabIndex = 15
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'rbClassic
        '
        Me.rbClassic.AutoSize = True
        Me.rbClassic.Location = New System.Drawing.Point(12, 53)
        Me.rbClassic.Name = "rbClassic"
        Me.rbClassic.Size = New System.Drawing.Size(97, 19)
        Me.rbClassic.TabIndex = 2
        Me.rbClassic.Text = "Sports Classic"
        Me.rbClassic.UseVisualStyleBackColor = True
        '
        'rbCompact
        '
        Me.rbCompact.AutoSize = True
        Me.rbCompact.Location = New System.Drawing.Point(12, 78)
        Me.rbCompact.Name = "rbCompact"
        Me.rbCompact.Size = New System.Drawing.Size(74, 19)
        Me.rbCompact.TabIndex = 6
        Me.rbCompact.Text = "Compact"
        Me.rbCompact.UseVisualStyleBackColor = True
        '
        'rbCoupe
        '
        Me.rbCoupe.AutoSize = True
        Me.rbCoupe.Location = New System.Drawing.Point(12, 103)
        Me.rbCoupe.Name = "rbCoupe"
        Me.rbCoupe.Size = New System.Drawing.Size(60, 19)
        Me.rbCoupe.TabIndex = 10
        Me.rbCoupe.Text = "Coupe"
        Me.rbCoupe.UseVisualStyleBackColor = True
        '
        'rbSuper
        '
        Me.rbSuper.AutoSize = True
        Me.rbSuper.Location = New System.Drawing.Point(268, 53)
        Me.rbSuper.Name = "rbSuper"
        Me.rbSuper.Size = New System.Drawing.Size(55, 19)
        Me.rbSuper.TabIndex = 5
        Me.rbSuper.Text = "Super"
        Me.rbSuper.UseVisualStyleBackColor = True
        '
        'rbMotorcycle
        '
        Me.rbMotorcycle.AutoSize = True
        Me.rbMotorcycle.Location = New System.Drawing.Point(115, 53)
        Me.rbMotorcycle.Name = "rbMotorcycle"
        Me.rbMotorcycle.Size = New System.Drawing.Size(85, 19)
        Me.rbMotorcycle.TabIndex = 3
        Me.rbMotorcycle.Text = "Motorcycle"
        Me.rbMotorcycle.UseVisualStyleBackColor = True
        '
        'rbMuscle
        '
        Me.rbMuscle.AutoSize = True
        Me.rbMuscle.Location = New System.Drawing.Point(115, 78)
        Me.rbMuscle.Name = "rbMuscle"
        Me.rbMuscle.Size = New System.Drawing.Size(63, 19)
        Me.rbMuscle.TabIndex = 7
        Me.rbMuscle.Text = "Muscle"
        Me.rbMuscle.UseVisualStyleBackColor = True
        '
        'rbOffroad
        '
        Me.rbOffroad.AutoSize = True
        Me.rbOffroad.Location = New System.Drawing.Point(115, 103)
        Me.rbOffroad.Name = "rbOffroad"
        Me.rbOffroad.Size = New System.Drawing.Size(71, 19)
        Me.rbOffroad.TabIndex = 11
        Me.rbOffroad.Text = "Off-road"
        Me.rbOffroad.UseVisualStyleBackColor = True
        '
        'rbSedan
        '
        Me.rbSedan.AutoSize = True
        Me.rbSedan.Location = New System.Drawing.Point(268, 78)
        Me.rbSedan.Name = "rbSedan"
        Me.rbSedan.Size = New System.Drawing.Size(57, 19)
        Me.rbSedan.TabIndex = 9
        Me.rbSedan.Text = "Sedan"
        Me.rbSedan.UseVisualStyleBackColor = True
        '
        'rbSport
        '
        Me.rbSport.AutoSize = True
        Me.rbSport.Location = New System.Drawing.Point(206, 53)
        Me.rbSport.Name = "rbSport"
        Me.rbSport.Size = New System.Drawing.Size(53, 19)
        Me.rbSport.TabIndex = 4
        Me.rbSport.Text = "Sport"
        Me.rbSport.UseVisualStyleBackColor = True
        '
        'rbSuv
        '
        Me.rbSuv.AutoSize = True
        Me.rbSuv.Location = New System.Drawing.Point(206, 78)
        Me.rbSuv.Name = "rbSuv"
        Me.rbSuv.Size = New System.Drawing.Size(46, 19)
        Me.rbSuv.TabIndex = 8
        Me.rbSuv.Text = "SUV"
        Me.rbSuv.UseVisualStyleBackColor = True
        '
        'rbUtility
        '
        Me.rbUtility.AutoSize = True
        Me.rbUtility.Location = New System.Drawing.Point(206, 103)
        Me.rbUtility.Name = "rbUtility"
        Me.rbUtility.Size = New System.Drawing.Size(56, 19)
        Me.rbUtility.TabIndex = 12
        Me.rbUtility.Text = "Utility"
        Me.rbUtility.UseVisualStyleBackColor = True
        '
        'rbVan
        '
        Me.rbVan.AutoSize = True
        Me.rbVan.Location = New System.Drawing.Point(268, 103)
        Me.rbVan.Name = "rbVan"
        Me.rbVan.Size = New System.Drawing.Size(45, 19)
        Me.rbVan.TabIndex = 13
        Me.rbVan.Text = "Van"
        Me.rbVan.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Checked = True
        Me.rbAll.Location = New System.Drawing.Point(12, 128)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(136, 19)
        Me.rbAll.TabIndex = 14
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All Available Vehicles"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'ofDialog
        '
        Me.ofDialog.Filter = "META file|*.meta"
        '
        'frmImport
        '
        Me.AcceptButton = Me.btnImport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 199)
        Me.Controls.Add(Me.rbAll)
        Me.Controls.Add(Me.rbVan)
        Me.Controls.Add(Me.rbUtility)
        Me.Controls.Add(Me.rbSuv)
        Me.Controls.Add(Me.rbSport)
        Me.Controls.Add(Me.rbSedan)
        Me.Controls.Add(Me.rbOffroad)
        Me.Controls.Add(Me.rbMuscle)
        Me.Controls.Add(Me.rbMotorcycle)
        Me.Controls.Add(Me.rbSuper)
        Me.Controls.Add(Me.rbCoupe)
        Me.Controls.Add(Me.rbCompact)
        Me.Controls.Add(Me.rbClassic)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.lblSelected)
        Me.Controls.Add(Me.btnBrowse)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnBrowse As Button
    Friend WithEvents lblSelected As Label
    Friend WithEvents btnImport As Button
    Friend WithEvents rbClassic As RadioButton
    Friend WithEvents rbCompact As RadioButton
    Friend WithEvents rbCoupe As RadioButton
    Friend WithEvents rbSuper As RadioButton
    Friend WithEvents rbMotorcycle As RadioButton
    Friend WithEvents rbMuscle As RadioButton
    Friend WithEvents rbOffroad As RadioButton
    Friend WithEvents rbSedan As RadioButton
    Friend WithEvents rbSport As RadioButton
    Friend WithEvents rbSuv As RadioButton
    Friend WithEvents rbUtility As RadioButton
    Friend WithEvents rbVan As RadioButton
    Friend WithEvents rbAll As RadioButton
    Friend WithEvents ofDialog As OpenFileDialog
End Class
