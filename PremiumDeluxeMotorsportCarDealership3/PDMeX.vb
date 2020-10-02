Imports System.Drawing
Imports GTA
Imports GTA.Native
Imports INMNativeUI

Public Class PDMeX
    Inherits Script

    Private Sub PDMeX_Tick(sender As Object, e As EventArgs) Handles Me.Tick
        _menuPool.ProcessMenus()
        _menuPool.UpdateStats(GetVehTopSpeed(VehPreview), GetVehAcceleration(VehPreview), GetVehBraking(VehPreview), GetVehTraction(VehPreview))

        If HideHud Then
            Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
            Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 3)
            Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 4)
            Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 5)
            Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 13)
            wsCamera.Update()
        End If

        If _menuPool.IsAnyMenuOpen Then
            If ShowVehicleName = True AndAlso Not VehicleName = Nothing AndAlso poly.IsInInterior(VehPreview.Position) AndAlso TaskScriptStatus = 0 Then
                Dim sr = Size.Round(UIMenu.GetScreenResolutionMaintainRatio)
                Dim sz = UIMenu.GetSafezoneBounds
                Select Case Game.Language.ToString
                    Case "Chinese", "Korean", "Japanese", "ChineseSimplified"
                        Dim vn As New UIResText(VehicleName, New Point(sr.Width - sz.X - 100, sr.Height - sz.Y - 240), 2.0F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Right) With {.DropShadow = True} : vn.Draw()
                        Dim vc As New UIResText(VehPreview.GetClassDisplayName, New Point(sr.Width - sz.X, sr.Height - sz.Y - 170), 2.0F, Color.DodgerBlue, GTA.Font.HouseScript, UIResText.Alignment.Right) With {.DropShadow = True} : vc.Draw()
                    Case Else
                        Dim vn As New UIResText(VehicleName, New Point(sr.Width - sz.X - 100, sr.Height - sz.Y - 240), 2.0F, Color.White, GTA.Font.ChaletComprimeCologne, UIResText.Alignment.Right) With {.DropShadow = True} : vn.Draw()
                        Dim vc As New UIResText(VehPreview.GetClassDisplayName, New Point(sr.Width - sz.X, sr.Height - sz.Y - 170), 2.0F, Color.DodgerBlue, GTA.Font.HouseScript, UIResText.Alignment.Right) With {.DropShadow = True} : vc.Draw()
                End Select
            End If
        End If
    End Sub

End Class
