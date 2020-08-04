<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstInput = New System.Windows.Forms.ListBox()
        Me.btnBrowseInput = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.tbOutput = New System.Windows.Forms.TextBox()
        Me.btnBrowseOutput = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatusLog = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Meiryo UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.LightCoral
        Me.Label1.Location = New System.Drawing.Point(15, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(314, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ひび割れ判定結果rnk化ツール"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Meiryo UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 142)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "INPUTファイル："
        '
        'lstInput
        '
        Me.lstInput.AllowDrop = True
        Me.lstInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstInput.Font = New System.Drawing.Font("Meiryo UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lstInput.FormattingEnabled = True
        Me.lstInput.ItemHeight = 19
        Me.lstInput.Location = New System.Drawing.Point(10, 173)
        Me.lstInput.Margin = New System.Windows.Forms.Padding(4)
        Me.lstInput.Name = "lstInput"
        Me.lstInput.Size = New System.Drawing.Size(648, 156)
        Me.lstInput.TabIndex = 5
        '
        'btnBrowseInput
        '
        Me.btnBrowseInput.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseInput.Location = New System.Drawing.Point(580, 138)
        Me.btnBrowseInput.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowseInput.Name = "btnBrowseInput"
        Me.btnBrowseInput.Size = New System.Drawing.Size(79, 27)
        Me.btnBrowseInput.TabIndex = 6
        Me.btnBrowseInput.Text = "参照..."
        Me.btnBrowseInput.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Meiryo UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(6, 86)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 19)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "出力フォルダ："
        '
        'btnRun
        '
        Me.btnRun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRun.Font = New System.Drawing.Font("Meiryo UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRun.Location = New System.Drawing.Point(298, 337)
        Me.btnRun.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(360, 56)
        Me.btnRun.TabIndex = 7
        Me.btnRun.Text = "ファイル変換"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'tbOutput
        '
        Me.tbOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbOutput.Location = New System.Drawing.Point(110, 82)
        Me.tbOutput.Name = "tbOutput"
        Me.tbOutput.Size = New System.Drawing.Size(463, 27)
        Me.tbOutput.TabIndex = 2
        '
        'btnBrowseOutput
        '
        Me.btnBrowseOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseOutput.Location = New System.Drawing.Point(580, 81)
        Me.btnBrowseOutput.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowseOutput.Name = "btnBrowseOutput"
        Me.btnBrowseOutput.Size = New System.Drawing.Size(79, 28)
        Me.btnBrowseOutput.TabIndex = 3
        Me.btnBrowseOutput.Text = "参照..."
        Me.btnBrowseOutput.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatusLog})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 409)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(678, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatusLog
        '
        Me.lblStatusLog.ForeColor = System.Drawing.Color.DarkRed
        Me.lblStatusLog.Name = "lblStatusLog"
        Me.lblStatusLog.Size = New System.Drawing.Size(55, 17)
        Me.lblStatusLog.Text = "準備完了"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(678, 431)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnBrowseOutput)
        Me.Controls.Add(Me.tbOutput)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnBrowseInput)
        Me.Controls.Add(Me.lstInput)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Meiryo UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmMain"
        Me.Text = "ひび割れ判定結果rnk化ツール"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lstInput As ListBox
    Friend WithEvents btnBrowseInput As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btnRun As Button
    Friend WithEvents tbOutput As TextBox
    Friend WithEvents btnBrowseOutput As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatusLog As ToolStripStatusLabel
End Class
