Public NotInheritable Class logger

    Private Sub New()

    End Sub

    Public Shared Sub Log(message As Object)
        If PDMMenu.optLogging Then System.IO.File.AppendAllText(".\PDM.log", DateTime.Now & ":" & message & Environment.NewLine)
    End Sub

    Public Shared Sub PinPoint(message As GTA.Math.Vector3)
        System.IO.File.AppendAllText(".\PinPoint.log", String.Format("{0}:{1},{2},{3}{4}", DateTime.Now, message.X, message.Y, message.Z, Environment.NewLine))
    End Sub

End Class