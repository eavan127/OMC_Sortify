Module modCart
    Public CartItems As New List(Of CartItem)

    Public Class CartItem
        Public Property ID As Integer
        Public Property Name As String
        Public Property Price As Decimal
        Public Property ImagePath As String
    End Class
End Module