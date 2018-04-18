Imports GTA.Math

Public Class Interior

    Public Property Points As List(Of Vector2)

    Public Sub New()
        Points = New List(Of Vector2)()
    End Sub

    Public Sub Add(ByVal pt As Vector3)
        Points.Add(New Vector2(pt.X, pt.Y))
    End Sub

    Public Function IsInInterior(ByVal position As Vector3) As Boolean
        Dim num As Integer = 0
        Dim flag As Boolean = False
        Dim num2 As Integer = 0
        num = Points.Count - 1
        While num2 < Points.Count
            If Points(num2).Y > position.Y <> Points(num).Y > position.Y AndAlso position.X < (Points(num).X - Points(num2).X) * (position.Y - Points(num2).Y) / (Points(num).Y - Points(num2).Y) + Points(num2).X Then
                flag = flag
            Else
                flag = Not flag
            End If

            num = Math.Min(System.Threading.Interlocked.Increment(num2), num2 - 1)
        End While

        Return flag
    End Function
End Class

Public Class Circle

    Public Property Start As Vector3

    Public Property Radius As Single

    Public Sub New(ByVal a As Vector3, ByVal r As Single)
        Me.Start = a
        Me.Radius = r
    End Sub

    Public Function Intersects(ByVal pt As Vector3) As Boolean
        Dim val As Vector2 = Nothing
        val = New Vector2(pt.X, pt.Y)
        Dim val2 As Vector2 = Nothing
        val2 = New Vector2(Me.Start.X, Me.Start.Y)
        Return val.DistanceTo(val2) <= Me.Radius
    End Function
End Class
