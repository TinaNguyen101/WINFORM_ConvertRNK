Imports System.IO

Public Class ConvertRNKTool

    Dim errors As New List(Of ErrorInfo)

#Region "Delegate"
    Public Delegate Sub ExportStatusUpdatingDelegate(statusText As String, percent As Integer)
    Public ExportStatusUpdating As ExportStatusUpdatingDelegate
    Public Delegate Sub ExportCompletedDelegate(errors As List(Of ErrorInfo))
    Public ExportCompleted As ExportCompletedDelegate
    Public Delegate Sub FileLockedDelegate()
    Public FileLocked As FileLockedDelegate
#End Region

    Public Structure RNKContent
        Public Property FileName() As String  '出力ファイル名
        Public Property Header() As String  'ヘッダー情報
        Public Property Content() As String  'ヘッダー情報
    End Structure
    Public Structure DataCSV
        Public Property B1() As String
        Public Property B2() As String
        Public Property B3() As String
        Public Property B4() As String
        Public Property E1() As String
        Public Property groupCol As String
    End Structure

    Public Structure ErrorInfo
        Public Sub New(ExportFunc As String, ReadFile As String, ExportFile As String, ErrorContent As String, ErrorlineNumber As String, Optional ByVal Ex As Exception = Nothing)
            Me.ExportFunc = ExportFunc
            Me.ReadFile = ReadFile
            Me.ExportFile = ExportFile
            Me.ErrorContent = ErrorContent
            Me.Ex = Ex
            Me.Time = Date.Now.ToString("yyyyMMddhhmmss")
            Me.ErrorLineNumber = ErrorlineNumber
        End Sub

        Public Property ExportFunc() As String  '出力機能
        Public Property ReadFile() As String  '読込ファイル
        Public Property ExportFile() As String  '出力ファイル
        Public Property ErrorContent() As String  'エラー内容
        Public Property Ex() As Exception  'エラー内容
        Public Property Time As String '年月日時分秒
        Public Property ErrorLineNumber As String '行番号

    End Structure

    Public Function IsFileLocked(ByVal fileName As String) As Boolean
        If File.Exists(fileName) Then
            Try
                Using stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None)
                End Using
                File.Delete(fileName)
                Return False
            Catch __unusedIOException1__ As IOException
                Return True
            End Try
        End If

        Return False
    End Function

    Public Function IsFileExist(ByVal fileName As String) As Boolean

        If File.Exists(fileName) Then
            Return True
        End If
        Return False

    End Function

    Public Sub ConvertToRNK(ByVal fileNames As String(), ByVal folderOutput As String)
        Dim currentlineNumber As String = ""
        Dim currentFileInput As String = ""
        Dim currentFileOutput As String = ""

        Try
            Dim arrFileNameOutput As New List(Of String)
            Dim arrRNKContent As New List(Of RNKContent)
            Dim lstData As New List(Of DataCSV)

#Region "●準備"
            ExportStatusUpdating("事前準備", 0)
            For Each fileItem As String In fileNames
                Dim fi As New IO.FileInfo(fileItem)
                If fi.Length = 0 Then
                    errors.Add(New ErrorInfo("準備", fileItem, "", String.Format(Constants.NoData), ""))
                Else
                    currentFileInput = fileItem
                    Dim arrContent As String() = New String() {}
                    'csvファイルを開き
                    Dim sr As StreamReader = New StreamReader(fileItem)
                    While Not sr.EndOfStream
                        Dim Fulltext = sr.ReadToEnd().ToString()
                        Dim rows As String() = Fulltext.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                        For i As Integer = 0 To rows.Count - 1
                            Dim cols As String() = rows(i).Split(",")
                            currentlineNumber = cols(0)
                            'B列は"/"記号で４つに区切られている。
                            Dim arrColB As String() = cols(1).Split("/")
                            If Not arrColB.Count = 4 Then
                                errors.Add(New ErrorInfo(" ", fileItem, arrColB(0), String.Format(Constants.BError), i.ToString()))
                                ExportCompleted(errors)
                                Continue For
                            Else
                                '・[B4]は、"_"記号で以下の通り７つに区切られているものとする。
                                '路線種別（3桁）路線番号（3桁）枝番（2桁）上下（1桁）車線（1桁）Y座標（5桁）X座標（1桁）
                                Dim arrColB4 As String() = arrColB(3).Split("_")
                                If Not arrColB4.Count = 7 Then
                                    errors.Add(New ErrorInfo(" ", fileItem, arrColB(0), String.Format(Constants.B4Error), i.ToString()))
                                    ExportCompleted(errors)
                                    Continue For
                                Else
                                    If Not arrFileNameOutput.Contains(arrColB(0)) Then
                                        arrFileNameOutput.Add(arrColB(0))
                                    End If
                                    lstData.Add(New DataCSV() With {
                                            .B1 = arrColB(0),
                                            .B2 = arrColB(1),
                                            .B3 = cols(0),
                                            .B4 = arrColB(3),
                                            .E1 = cols(4),
                                            .groupCol = arrColB(0) & arrColB(1) & arrColB4(0) & arrColB4(1) & arrColB4(2) & arrColB4(3) & arrColB4(4) & arrColB4(5)
                                            })
                                End If
                            End If
                        Next
                    End While
                    sr.Dispose()
                End If
            Next
            If arrFileNameOutput.Any Then

                Dim arrFileExist As New List(Of String)
                For Each item As String In arrFileNameOutput
                    Dim SavePath As String = System.IO.Path.Combine(folderOutput, item + ".rnk")
                    '処理開始時に、出力ファイルと同名のファイルがあるかどうかを確認し
                    If IsFileExist(SavePath) Then
                        arrFileExist.Add(SavePath)
                    End If
                Next
                If arrFileExist.Any Then
                    If MessageBox.Show(String.Format(Constants.ConfirmOverwrite), "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                        '「いいえ」ならば処理を中断する。
                        Exit Sub
                    Else
                        '「はい」の場合でも、ファイルが使用中でないか（ロックされているか）を確認し
                        For Each item As String In arrFileExist
                            If IsFileLocked(item) Then
                                FileLocked()
                                Exit Sub
                            End If
                            File.Delete(System.IO.Path.Combine(folderOutput, item + ".rnk"))
                        Next
                    End If
                End If

            End If
            ExportStatusUpdating("準備完了", 100)
#End Region

#Region "●ファイル変換詳細"
            If lstData.Count > 0 Then
                ExportStatusUpdating("変換前", 0)
                Dim grpData = lstData.GroupBy(Function(t) New With {Key t.groupCol})
                Dim k As Integer = 0
                Dim tempHeader As String = ""
                For Each item In grpData

                    Dim arrContent = New String() {"9", "9", "9", "9", "9", "9", "9", "9"}
                    Dim y As Integer
                    For Each inneritem In item
                        currentlineNumber = inneritem.B3
                        Dim arrColB4 As String() = inneritem.B4.Split("_")
                        Dim x As Integer = arrColB4(6).Replace(".jpg", "")
                        arrContent(9 - x) = inneritem.E1
                        y = arrColB4(5)
                    Next
                    Dim _RNKContent As New RNKContent
                    _RNKContent.FileName = item.First.B1 & ".rnk"
                    '_RNKContent.Header = item.First.B2.Replace(" ", "_").PadRight(64, " ")
                    _RNKContent.Header = item.First.B2.PadRight(64, " ")

                    Dim valuetemp = String.Join("", arrContent)
                    '⑥	10桁目～18桁目は固定で"0"を連続して9個出力する。															
                    Dim value6 = "".PadRight(9, "0")
                    '⑦	19桁目～36桁目は18桁連続して半角スペース記号を出力する。																	
                    Dim value7 = "".PadRight(18, " ")
                    '⑧	37桁目～42桁目の6桁分を使用して距離標情報を出力する。																
                    ' Dim value8 = (y * 50).ToString().PadLeft(6, " ")
                    If tempHeader = _RNKContent.Header Then
                        k = k + 1
                    Else
                        k = 1
                    End If
                    Dim value8 = ((k) * 50).ToString().PadLeft(6, " ")

                    '⑨	末尾に22桁の半角スペース記号を付与し、計64桁で出力する。																
                    Dim value9 = "".PadRight(22, " ")
                    '_RNKContent.Content = String.Format("{0}{1}{2}{3}{4}{5}{6},", 2, valuetemp, value6, value7, value8, value9, y)'test
                    _RNKContent.Content = String.Format("{0}{1}{2}{3}{4}{5},", 2, valuetemp, value6, value7, value8, value9)
                    arrRNKContent.Add(_RNKContent)
                    tempHeader = _RNKContent.Header
                Next

                ExportStatusUpdating("変換完了", 100)
            End If
#End Region

#Region "出力フォルダに出力する"
            If arrRNKContent.Any Then
                ExportStatusUpdating("ファイル出力前", 0)
                Dim tempFile As String = ""
                Dim tempHead As String = ""
                Dim rowIndex As Integer = 0
                Dim fileName = Path.Combine(folderOutput, arrRNKContent(0).FileName)
                Dim fileOutput As New StreamWriter(fileName, True)
                tempFile = arrRNKContent(0).FileName
                For Each item As RNKContent In arrRNKContent
                    rowIndex = rowIndex + 1
                    currentlineNumber = rowIndex
                    If tempFile <> item.FileName Then
                        fileOutput.Close()
                        fileName = Path.Combine(folderOutput, item.FileName)
                        fileOutput = New StreamWriter(fileName, False)
                        currentFileOutput = fileName
                    End If
                    If tempHead <> item.Header Then
                        fileOutput.WriteLine(item.Header)
                    End If
                    fileOutput.WriteLine(item.Content)
                    tempFile = item.FileName
                    tempHead = item.Header
                Next
                fileOutput.Close()
                ExportStatusUpdating("ファイル出力完了", 100)
            End If
#End Region

        Catch ex As Exception
            errors.Add(New ErrorInfo("ファイル変換", currentFileInput, currentFileOutput, ex.Message, currentlineNumber, ex))
        Finally
            WriteLog(errors)
            ExportCompleted(errors)
        End Try
    End Sub

    Private Sub WriteLog(lstError As List(Of ErrorInfo))
        For Each item As ErrorInfo In lstError
            Using w As StreamWriter = File.AppendText(DateTime.Now.ToString("yyyyMMdd") & ".txt")
                '|年月日時分秒|Inputファイル名|行番号|エラー内容|
                w.WriteLine(item.Time & "|" & item.ReadFile & "|" & item.ErrorLineNumber & "|" & item.ErrorContent)
            End Using
        Next
    End Sub


End Class
