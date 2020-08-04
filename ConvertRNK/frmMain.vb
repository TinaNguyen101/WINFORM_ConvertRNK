Option Explicit On
Imports System.IO
Imports System.Threading

Public Class frmMain
    Dim configReader As XMLReader
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            'TODO:起動時に、xmlファイルに保存されているディレクトリパスを読み込んで表示する。
            lstInput.DisplayMember = "Name"
            lstInput.ValueMember = "Path"
            lblStatusLog.Text = ""
            configReader = New XMLReader()
            If configReader.ReadConfigParameter() IsNot Nothing Then
                tbOutput.Text = configReader.getValue(Of String)("EXPORTPATH", "")
                If Not String.IsNullOrEmpty(tbOutput.Text) Then
                    If Not Directory.Exists(tbOutput.Text) Then
                        'xmlファイルがない場合、エラー
                        MessageBox.Show(String.Format(Constants.XMLCannotRead), "エラー", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                        lblStatusLog.Text = String.Format(Constants.XMLCannotRead)
                    End If
                End If
            Else
                'xmlファイルに保存されているディレクトリパスが存在しない場合、エラー
                MessageBox.Show(String.Format(Constants.FolderNotExist), "エラー", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                lblStatusLog.Text = String.Format(Constants.FolderNotExist)
            End If


        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MessageBox.Show(ex.Message & "(" & Reflection.MethodBase.GetCurrentMethod.Name & ")")
        Finally

        End Try

    End Sub

    Private Sub btnBrowseOutput_Click(sender As Object, e As EventArgs) Handles btnBrowseOutput.Click

        Try
            'TODO:
            '・フォルダ選択ダイアログを表示し、取得結果を①のテキストボックスに表示する。
            '・フォルダ選択ダイアログを記述、変更があればxmlファイルに保存

            Dim dialog As FolderBrowserDialog = New FolderBrowserDialog()
            If (dialog.ShowDialog() = DialogResult.OK) Then
                tbOutput.Text = dialog.SelectedPath

                Dim param As Hashtable = New Hashtable()
                param.Add("EXPORTPATH", tbOutput.Text)
                If configReader.UpdateConfigParameter(param) Then
                    lblStatusLog.Text = String.Format(Constants.SaveSuccess)
                Else
                    lblStatusLog.Text = String.Format(Constants.SaveError)
                    MessageBox.Show(String.Format(Constants.SaveError), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MessageBox.Show(ex.Message & "(" & Reflection.MethodBase.GetCurrentMethod.Name & ")")
        Finally

        End Try

    End Sub

    Private Sub btnBrowseInput_Click(sender As Object, e As EventArgs) Handles btnBrowseInput.Click

        Try
            'TODO:ファイル選択ダイアログを記述
            '・複数選択可
            '・csvファイルを開く
            '・選択したファイル一覧をlstInputに表示

            Dim dialog As OpenFileDialog = New OpenFileDialog()
            dialog.Filter = "csv files (*.csv)|*.csv"
            dialog.Multiselect = True
            If dialog.ShowDialog = DialogResult.OK Then
                For Each mFile As String In dialog.FileNames
                    Dim item = New FileName(Path.GetFileName(mFile), mFile)
                    lstInput.Items.Add(item)
                Next
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MessageBox.Show(ex.Message & "(" & Reflection.MethodBase.GetCurrentMethod.Name & ")")
        Finally

        End Try

    End Sub

    Private Sub lstInput_DragDrop(sender As Object, e As DragEventArgs) Handles lstInput.DragDrop
        lstInput.Items.Clear()
        Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
        For Each file As String In files
            If Directory.Exists(file) Then
                Dim fld As New System.IO.DirectoryInfo(file)
                Dim lstItem = fld.GetFiles("*.csv")
                If lstItem.Count = 0 Then
                    lblStatusLog.Text = String.Format(Constants.CSVNotExist)
                Else
                    For Each item As FileInfo In lstItem
                        lstInput.Items.Add(New FileName(Path.GetFileName(item.FullName), item.FullName))
                    Next
                End If
            Else
                If System.IO.Path.GetExtension(file).Equals(".csv", StringComparison.InvariantCultureIgnoreCase) Then
                    Dim item = New FileName(Path.GetFileName(file), file)
                    lstInput.Items.Add(item)
                End If

            End If

        Next
    End Sub

    Private Sub lstInput_DragEnter(sender As Object, e As DragEventArgs) Handles lstInput.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If


    End Sub


    Private Sub lstInput_DoubleClick(sender As Object, e As EventArgs) Handles lstInput.DoubleClick

        Try
            'TODO:選択したファイルが存在するならファイルを起動する
            If lstInput.SelectedIndex <> -1 Then
                Process.Start(DirectCast(lstInput.SelectedItem, FileName).Path)
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MessageBox.Show(ex.Message & "(" & Reflection.MethodBase.GetCurrentMethod.Name & ")")
        Finally

        End Try

    End Sub

    Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click

        Try
            'TODO:メイン処理を記述
            '・INPUTファイル　に表示されているファイル一覧を読み込み、メイン処理を実施する。出力物は「出力フォルダ」に保存される																
            '・処理の進行状況をステータスバーのラベルに表示すること
#Region "エラー"

            '・出力フォルダ が空白ならエラー
            If String.IsNullOrEmpty(tbOutput.Text.Trim()) Then
                lblStatusLog.Text = String.Format(Constants.CSVCannotRead)
                MessageBox.Show(String.Format(Constants.CSVCannotRead), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If Not Directory.Exists(tbOutput.Text.Trim()) Then
                lblStatusLog.Text = String.Format(Constants.FolderOutNotExist)
                MessageBox.Show(String.Format(Constants.FolderOutNotExist, tbOutput.Text.Trim()), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            '・INPUTファイルが空白ならエラー
            If lstInput.Items.Count = 0 Then
                lblStatusLog.Text = String.Format(Constants.INPUTCannotRead)
                MessageBox.Show(String.Format(Constants.INPUTCannotRead), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
#End Region

            '・処理開始前に「ファイル変換処理を開始します。よろしいですか？」のメッセージを表示する。結果がNOならば処理中止
            If MessageBox.Show(String.Format(Constants.ConfirmProcess), "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                Dim convert_tool As New ConvertRNKTool

                convert_tool.ExportCompleted = New ConvertRNKTool.ExportCompletedDelegate(AddressOf ExportCompleted)
                convert_tool.ExportStatusUpdating = New ConvertRNKTool.ExportStatusUpdatingDelegate(AddressOf ExportStatusUpdating)
                convert_tool.FileLocked = New ConvertRNKTool.FileLockedDelegate(AddressOf FileLocked)

                Me.UseWaitCursor = True
                EnableForm(False)

                Dim thrd As New Thread(Sub()
                                           convert_tool.ConvertToRNK(lstInput.Items.Cast(Of FileName).Select(Function(x) x.Path).ToArray(), tbOutput.Text.Trim())
                                       End Sub)
                thrd.SetApartmentState(ApartmentState.STA)
                thrd.Start()


            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MessageBox.Show(ex.Message & "(" & Reflection.MethodBase.GetCurrentMethod.Name & ")")
        Finally

        End Try

    End Sub

    Private Sub EnableForm(isEnabled As Boolean)

        tbOutput.Enabled = isEnabled
        btnBrowseOutput.Enabled = isEnabled
        btnBrowseInput.Enabled = isEnabled
        lstInput.Enabled = isEnabled
        btnRun.Enabled = isEnabled And CheckButtonEnable()

    End Sub

    Private Function CheckButtonEnable() As Boolean
        Return (Not (tbOutput.Text.Trim() = String.Empty) AndAlso (lstInput.Items.Count > 0))
    End Function
    Private Sub ExportCompleted(errors As List(Of ConvertRNKTool.ErrorInfo))
        BeginInvoke(New MethodInvoker(Sub()
                                          Me.UseWaitCursor = False
                                          EnableForm(True)
                                      End Sub))
        If errors.Any() Then
            BeginInvoke(New MethodInvoker(Sub()
                                              lblStatusLog.Text = String.Format(Constants.ProcessError, errors.First.ReadFile)
                                              'tsProccessBar.Value = 0
                                              'tsProccessBar.Visible = False
                                          End Sub))
        Else
            BeginInvoke(New MethodInvoker(Sub()
                                              lblStatusLog.Text = String.Format(Constants.ConvertSuccess)
                                              'tsProccessBar.Value = 0
                                              'tsProccessBar.Visible = False
                                          End Sub))

        End If
    End Sub
    Private Sub ExportStatusUpdating(fileName As String, percent As Integer)

        BeginInvoke(New MethodInvoker(Sub()
                                          lblStatusLog.Text = fileName
                                          'tsProccessBar.Value = percent
                                      End Sub))
    End Sub
    Private Sub FileLocked()
        BeginInvoke(New MethodInvoker(Sub()
                                          Me.UseWaitCursor = False
                                          EnableForm(True)
                                      End Sub))

        BeginInvoke(New MethodInvoker(Sub()
                                          lblStatusLog.Text = String.Format(Constants.fileBlock)
                                          'tsProccessBar.Value = 0
                                          'tsProccessBar.Visible = False
                                      End Sub))

    End Sub

End Class
