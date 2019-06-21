Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports GTA.Game
Imports INMNativeUI
Imports System.Reflection
Imports PDMCD4.Resources

Public Class PDM
    Inherits Script

    Public Shared SelectedVehicle As String, PlayerCash, VehiclePrice As Integer, VehPreview As Vehicle, PdmBlip As Blip
    Public Shared Price As Decimal = 0, Radius As Integer = 120, TestDrive As Integer = 1, VehicleName As String = Nothing
    Public Shared HideHud As Boolean = False, DrawSpotLight As Boolean = False, ShowVehicleName As Boolean = False
    Public Shared PdmDoor, VehPreviewPos, PlayerLastPos As Vector3, GPC As Ped, GP As Player
    Public Shared CameraPos, CameraRot As Vector3
    Public Shared PlayerHeading, PdmDoorDist As Single
    Public Shared camera As WorkshopCamera
    Public Shared cutCamera As Camera
    Public Shared lastVehMemory As Memory
    Public Shared TaskScriptStatus As Integer = -1
    Public Shared pdmPed As Ped
    Public Shared poly As Interior = New Interior(), testDeivePoly As Interior = New Interior()
    Public Shared blipName As String = "NULL"

    Public Sub New()
        Try
            LoadSettings()

            poly.Add(New Vector3(-71.54493, -1060.757, 27.5556))
            poly.Add(New Vector3(-94.17564, -1126.55, 25.79746))
            poly.Add(New Vector3(-17.57518, -1125.392, 27.11017))
            poly.Add(New Vector3(-3.737129, -1081.494, 26.67219))

            testDeivePoly.Add(New Vector3(-123.3222, -1155.505, 25.70785))
            testDeivePoly.Add(New Vector3(76.87627, -1143.797, 29.22843))
            testDeivePoly.Add(New Vector3(129.4713, -989.3712, 29.30896))
            testDeivePoly.Add(New Vector3(-55.58704, -921.9064, 29.28478))

            GP = Game.Player
            GPC = Game.Player.Character
            Select Case GPC.Name
                Case "Michael", "Franklin", "Trevor"
                    PlayerCash = Game.Player.Money
                Case Else
                    PlayerCash = 1999999999
            End Select

            camera = New WorkshopCamera

            VehPreviewPos = New Vector3(-44.45501, -1096.976, 26.42235)
            CameraPos = New Vector3(-47.45673, -1101.28, 27.54757)
            CameraRot = New Vector3(-18.12634, 0, -26.97177)
            PlayerHeading = 250.6701
            ToggleIPL("shr_int", "fakeint")
            LoadMissingProps()

            CreateEntrance()
            Game.Globals(GetGlobalValue).SetInt(1)
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub CreateEntrance()
        PdmDoor = New Vector3(-55.99228, -1098.51, 25.423)
        PdmBlip = World.CreateBlip(PdmDoor)
        PdmBlip.Sprite = BlipSprite.PersonalVehicleCar
        PdmBlip.Color = BlipColor.Red
        PdmBlip.IsShortRange = True
    End Sub

    Public Sub OnTick(o As Object, e As EventArgs) Handles Me.Tick
        If blipName = "NULL" Then
            If RequestAdditionTextFile("LFI_F") Then
                blipName = Game.GetGXTEntry("collision_vt4m0x")
                PdmBlip.Name = blipName 'GetLangEntry("PREMIUM_DELUXE_MOTORSPORT")
            End If
        End If

        Try
            If MissionFlag Or GP.WantedLevel > 1 Then
                PdmBlip.Alpha = 0
            Else
                PdmBlip.Alpha = 255
            End If
        Catch ex As Exception
            logger.Log("Error Blip Visible " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            PdmDoorDist = World.GetDistance(GPC.Position, PdmDoor)
            GP = Game.Player
            GPC = Game.Player.Character
            Select Case GPC.Name
                Case "Michael", "Franklin", "Trevor"
                    PlayerCash = Game.Player.Money
                Case Else
                    PlayerCash = 1999999999
            End Select
        Catch ex As Exception
            logger.Log("Error Get Info " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If PdmDoorDist < 10.0 Then
                If pdmPed = Nothing Then
                    Dim chairs As Prop() = World.GetNearbyProps(PdmDoor, 3.0, "v_corp_offchair")
                    Dim chair As Prop = Nothing
                    For Each props As Prop In chairs
                        chair = props
                        chair.FreezePosition = True
                    Next
                    pdmPed = World.CreatePed(PedHash.Hipster01AFY, PdmDoor, 219.5891)
                    pdmPed.IsPersistent = True
                    pdmPed.Task.StartScenario("PROP_HUMAN_SEAT_CHAIR_UPRIGHT", New Vector3(chair.Position.X, chair.Position.Y, chair.Position.Z + 0.46))
                End If
                pdmPed.Task.LookAt(GPC)
                pdmPed.AlwaysKeepTask = True
            End If
        Catch ex As Exception
            Try
                pdmPed.Delete()
            Catch exe As Exception
            End Try
            logger.Log("Error Create Ped " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If Not GPC.IsInVehicle AndAlso Not GPC.IsDead AndAlso PdmDoorDist < 3.0 AndAlso GP.WantedLevel = 0 AndAlso TaskScriptStatus = -1 Then
                DisplayHelpTextThisFrame(Game.GetGXTEntry("SHR_MENU")) 'GetLangEntry("HELP_ENTER_SHOP"))
            ElseIf Not GPC.IsInVehicle AndAlso Not GPC.IsDead AndAlso PdmDoorDist < 3.0 AndAlso GP.WantedLevel >= 1 Then
                Native.Function.Call(Hash.DISPLAY_HELP_TEXT_THIS_FRAME, New InputArgument() {"LOSE_WANTED", 0})
            End If

            If TestDrive = 3 AndAlso Not GPC.IsInVehicle Then
                FadeScreenOut(200)
                Wait(200)
                Dim penalty As Double = VehiclePrice / 99
                If VehPreview.HasBeenDamagedBy(GPC) Then
                    GP.Money = (PlayerCash - (VehiclePrice / 99))
                    DisplayHelpTextThisFrame("$" & Math.Round(penalty).ToString("###,###") & GetLangEntry("HELP_PENALTY"))
                    UI.Notify("$" & Math.Round(penalty).ToString("###,###") & GetLangEntry("HELP_PENALTY"))
                End If
                PDMMenu.ConfirmMenu.Visible = True
                VehPreview.IsDriveable = False
                VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
                VehPreview.Position = VehPreviewPos
                VehPreview.Heading = Radius
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                Native.Function.Call(Hash.SET_VEHICLE_FIXED, VehPreview)
                GPC.Position = PlayerLastPos
                TestDrive = 1
                HideHud = True
                Wait(200)
                FadeScreenIn(200)
                ShowVehicleName = True
                camera.RepositionFor(VehPreview)
            ElseIf TestDrive = 3 AndAlso Not testDeivePoly.IsInInterior(GPC.Position) Then
                FadeScreenOut(200)
                Wait(200)
                Dim penalty As Double = VehiclePrice / 99
                If VehPreview.HasBeenDamagedBy(GPC) Then
                    GP.Money = (PlayerCash - (VehiclePrice / 99))
                    UI.Notify("$" & Math.Round(penalty).ToString("###,###") & GetLangEntry("HELP_PENALTY"))
                End If
                PDMMenu.ConfirmMenu.Visible = True
                VehPreview.IsDriveable = False
                VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
                VehPreview.Position = VehPreviewPos
                VehPreview.Heading = Radius
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                Native.Function.Call(Hash.SET_VEHICLE_FIXED, VehPreview)
                GPC.Position = PlayerLastPos
                TestDrive = 1
                HideHud = True
                Wait(200)
                FadeScreenIn(200)
                ShowVehicleName = True
                camera.RepositionFor(VehPreview)
            ElseIf TestDrive = 2 AndAlso GPC.IsInVehicle Then
                TestDrive = TestDrive + 1
            End If
        Catch ex As Exception
            logger.Log("Error Show Help " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If HideHud Then
                Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 3)
                Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 4)
                Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 5)
                Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 13)
                camera.Update()
            End If

            If DrawSpotLight = True Then
                World.DrawSpotLightWithShadow(VehPreviewPos + Vector3.WorldUp * 4 + Vector3.WorldNorth * 4, Vector3.WorldSouth + Vector3.WorldDown, Color.White, 30, 30, 100, 50, -1)
                World.DrawSpotLight(VehPreviewPos + Vector3.WorldUp * 4 + Vector3.WorldNorth * 4, Vector3.WorldSouth + Vector3.WorldDown, Color.White, 30, 30, 100, 50, -1)
            End If
        Catch ex As Exception
            logger.Log("Error Hud Spotlight " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If IsControlJustPressed(0, GTA.Control.Context) AndAlso PdmDoorDist < 3.0 AndAlso Not GPC.IsInVehicle AndAlso GP.WantedLevel = 0 AndAlso TaskScriptStatus = -1 Then
                TaskScriptStatus = 0

                HideHud = True
                Wait(200)
                FadeScreenIn(200)
                SelectedVehicle = optLastVehName
                PlayerLastPos = GPC.Position
                If VehPreview = Nothing Then
                    VehPreview = CreateVehicleFromHash(optLastVehHash, VehPreviewPos, 6.122209)
                Else
                    VehPreview.Delete()
                    VehPreview = CreateVehicleFromHash(optLastVehHash, VehPreviewPos, 6.122209)
                End If
                PDMMenu.UpdateVehPreview()
                camera.RepositionFor(VehPreview)
                VehicleName = SelectedVehicle
                ShowVehicleName = True
                VehPreview.Heading = Radius
                VehPreview.LockStatus = VehicleLockStatus.CannotBeTriedToEnter
                VehPreview.DirtLevel = 0
                PDMMenu.MainMenu.Visible = True
            End If
        Catch ex As Exception
            logger.Log("Error keypress Context " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If IsControlJustReleased(0, keyDoor) AndAlso poly.IsInInterior(VehPreview.Position) AndAlso Not GPC.IsInVehicle AndAlso TaskScriptStatus = 0 Then
                If VehPreview.IsDoorOpen(VehicleDoor.FrontLeftDoor) Then
                    Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                Else
                    VehPreview.OpenDoor(VehicleDoor.BackLeftDoor, False, False)
                    VehPreview.OpenDoor(VehicleDoor.BackRightDoor, False, False)
                    VehPreview.OpenDoor(VehicleDoor.FrontLeftDoor, False, False)
                    VehPreview.OpenDoor(VehicleDoor.FrontRightDoor, False, False)
                    VehPreview.OpenDoor(VehicleDoor.Hood, False, False)
                    VehPreview.OpenDoor(VehicleDoor.Trunk, False, False)
                End If
            ElseIf IsControlJustPressed(0, keyRoof) AndAlso poly.IsInInterior(VehPreview.Position) AndAlso TaskScriptStatus = 0 Then
                If VehPreview.RoofState = VehicleRoofState.Closed Then
                    Native.Function.Call(Hash.LOWER_CONVERTIBLE_ROOF, VehPreview, False)
                Else
                    Native.Function.Call(Hash.RAISE_CONVERTIBLE_ROOF, VehPreview, False)
                End If
            ElseIf IsControlJustPressed(0, keyZoom) AndAlso poly.IsInInterior(VehPreview.Position) AndAlso TaskScriptStatus = 0 Then
                If camera.MainCameraPosition = CameraPosition.Car Then
                    If camera.CameraZoom = 5.0 Then
                        Do While camera.CameraZoom > 3.5
                            Wait(1)
                            camera.CameraZoom -= 0.1
                        Loop
                    Else
                        Do While camera.CameraZoom < 5.0
                            Wait(1)
                            camera.CameraZoom += 0.1
                        Loop
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Log("Error keypress Door Control " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
            If IsControlJustReleased(0, keyCamera) AndAlso poly.IsInInterior(VehPreview.Position) AndAlso Not GPC.IsInVehicle Then
                If Not VehPreview.ClassType = VehicleClass.Motorcycles Then
                    If camera.MainCameraPosition = CameraPosition.Car Then
                        camera.MainCameraPosition = CameraPosition.Interior
                    Else
                        camera.MainCameraPosition = CameraPosition.Car
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Log("Error keypress Change Cam " & ex.Message & " " & ex.StackTrace)
        End Try

        Try
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
        Catch ex As Exception
            logger.Log("Error Car Name " & ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub OnAborted() Handles MyBase.Aborted
        Try
            PdmBlip.Remove()
            Game.FadeScreenIn(200)
            If Not pdmPed = Nothing Then pdmPed.Delete()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub
End Class