Public Class Constants
    Public Shared SaveSuccess As String = "データを保存しました。"
    Public Shared SaveError As String = "データの保存が失敗しました。"
    Public Shared XMLCannotRead As String = "XMLファイルを読み込めません。"
    Public Shared FolderNotExist As String = "フォルダが見つかりません。"
    Public Shared CSVNotExist As String = "CSVファイルが検出されませんでした。"
    Public Shared CSVCannotRead As String = "出力フォルダを選択してください。"
    Public Shared INPUTCannotRead As String = "INPUTファイルを選択してください。"
    Public Shared ConfirmProcess As String = "ファイル変換処理を開始します。よろしいですか？"
    Public Shared ConfirmOverwrite As String = "出力フォルダに同名のファイルが既に存在します。上書きしてよろしいですか？"
    Public Shared fileBlock As String = "ファイルが開かれているため保存できませんでした。"
    Public Shared FolderOutNotExist As String = "{0}というフォルダが見つかりません。"
    Public Shared ConvertSuccess As String = "ファイル変換が完了しました。"
    Public Shared NoData As String = "データがありません。"
    Public Shared ProcessError As String = "エラーが発生したため、処理を中断しました。詳細は{0}ご確認ください。"

    Public Shared BError As String = "B必ず３つ含まれている(それ以外はエラー)。"
    Public Shared B4Error As String = "B4必ず６つ含まれている（それ以外はエラー）。"


    Public Shared Function CircledNumber(number As Integer) As String
        If number < 21 Then
            CircledNumber = Convert.ToChar(9311 + number)
        Else
            CircledNumber = Convert.ToChar(12880 + number - 20)
        End If
    End Function
End Class
