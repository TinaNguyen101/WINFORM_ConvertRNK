Public Class FileName
    Private _name As String
    Private _path As String
    Public Sub New(ByVal name As String, ByVal path As String)
        _name = name
        _path = path
    End Sub
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property Path() As String
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property
End Class
