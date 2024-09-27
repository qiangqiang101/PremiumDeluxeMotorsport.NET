Imports System.Drawing
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports GTA.Game
Imports LemonUI.Scaleform
Imports LemonUI
Imports NativeFunction = GTA.Native.Function

Public Class PDM
    Inherits Script

    Public Sub New()
        Try
            LoadSettings()
            BtnRotLeft = New InstructionalButton(Game.GetLocalizedString("CMM_MOD_S6"), keyDoor) 'GetLangEntry("BTN_DOOR"))
            BtnRotRight = New InstructionalButton(Game.GetLocalizedString("CMOD_MOD_ROF"), keyRoof) 'GetLangEntry("BTN_ROOF"))
            BtnCamera = New InstructionalButton(Game.GetLocalizedString("CTRL_0"), keyCamera) 'GetLangEntry("BTN_CHANGE_CAM"))
            BtnZoom = New InstructionalButton(Game.GetLocalizedString("HUD_INPUT91"), keyZoom) 'GetLangEntry("BTN_ZOOM"))

            CreateEntrance()
            GlobalVariable.Get(GetGlobalValue).SetBit(1)

            _menuPool = New ObjectPool()

            poly.Add(New Vector3(-71.54493, -1060.757, 27.5556))
            poly.Add(New Vector3(-94.17564, -1126.55, 25.79746))
            poly.Add(New Vector3(-17.57518, -1125.392, 27.11017))
            poly.Add(New Vector3(-3.737129, -1081.494, 26.67219))

            testDeivePoly.Add(New Vector3(-123.3222, -1155.505, 25.70785))
            testDeivePoly.Add(New Vector3(76.87627, -1143.797, 29.22843))
            testDeivePoly.Add(New Vector3(129.4713, -989.3712, 29.30896))
            testDeivePoly.Add(New Vector3(-55.58704, -921.9064, 29.28478))

            NativeFunction.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "VEHICLE_SHOP_HUD_1", False, -1)
            NativeFunction.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "VEHICLE_SHOP_HUD_2", False, -1)
            CreateMenus()

            ToggleIPL("shr_int", "fakeint")
            LoadMissingProps()
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub CreateEntrance()
        PdmDoor = New Vector3(-55.99228F, -1098.51F, 25.423F)
        PdmBlip = World.CreateBlip(PdmDoor)
        PdmBlip.Sprite = BlipSprite.SimeonCarShowroom
        PdmBlip.Color = BlipColor.White
        PdmBlip.IsShortRange = True
    End Sub

    Public Sub PDM_OnTick(o As Object, e As EventArgs) Handles Me.Tick
        Try
            GP = Game.Player
            GPC = Game.Player.Character
            Select Case GPC.Name
                Case "Michael", "Franklin", "Trevor"
                    PlayerCash = Game.Player.Money
                Case Else
                    PlayerCash = 1999999999
            End Select
            PdmDoorDist = World.GetDistance(GPC.Position, PdmDoor)

            Try
                If PdmDoorDist < 10.0 Then
                    If pdmPed = Nothing Then
                        Dim chairs As Prop() = World.GetNearbyProps(PdmDoor, 3.0, New Model("v_corp_offchair"))
                        Dim chair As Prop = Nothing
                        For Each props As Prop In chairs
                            chair = props
                            chair.IsPositionFrozen = True
                        Next
                        pdmPed = World.CreatePed(PedHash.Hipster01AFY, PdmDoor, 219.5891)
                        pdmPed.IsPersistent = True
                        pdmPed.Task.StartScenarioAtPosition("PROP_HUMAN_SEAT_CHAIR_UPRIGHT", New Vector3(chair.Position.X, chair.Position.Y, chair.Position.Z + 0.46F), 0F)
                    End If
                    pdmPed.Task.LookAt(GPC)
                    pdmPed.KeepTaskWhenMarkedAsNoLongerNeeded = True
                End If
            Catch ex As Exception
                Try
                    pdmPed.Delete()
                Catch exe As Exception
                End Try
                logger.Log("Error Create Ped " & ex.Message & " " & ex.StackTrace)
            End Try

            If Not GPC.IsInVehicle AndAlso Not GPC.IsDead AndAlso PdmDoorDist < 3.0 AndAlso GP.WantedLevel = 0 AndAlso TaskScriptStatus = -1 Then
                UI.Screen.ShowHelpTextThisFrame(Game.GetLocalizedString("SHR_MENU"), True) 'GetLangEntry("HELP_ENTER_SHOP"))
            ElseIf Not GPC.IsInVehicle AndAlso Not GPC.IsDead AndAlso PdmDoorDist < 3.0 AndAlso GP.WantedLevel >= 1 Then
                NativeFunction.Call(Hash.DISPLAY_HELP_TEXT_THIS_FRAME, New InputArgument() {"LOSE_WANTED", 0})
            End If

            If TestDrive = 3 AndAlso Not GPC.IsInVehicle Then
                UI.Screen.FadeOut(200)
                Wait(200)
                Dim penalty As Double = VehiclePrice / 99
                If VehPreview.HasBeenDamagedBy(GPC) Then
                    GP.Money = (PlayerCash - (VehiclePrice / 99))
                    UI.Screen.ShowSubtitle("$" & System.Math.Round(penalty).ToString("###,###") & GetLangEntry("HELP_PENALTY"))
                End If
                ConfirmMenu.Visible = True
                VehPreview.IsUndriveable = True
                VehPreview.LockStatus = VehicleLockStatus.IgnoredByPlayer
                VehPreview.Position = VehPreviewPos
                VehPreview.Heading = Radius
                NativeFunction.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                NativeFunction.Call(Hash.SET_VEHICLE_FIXED, VehPreview)
                GPC.Position = PlayerLastPos
                TestDrive = 1
                HideHud = True
                Wait(200)
                UI.Screen.FadeIn(200)
                ShowVehicleName = True
                wsCamera.RepositionFor(VehPreview)
            ElseIf TestDrive = 3 AndAlso Not testDeivePoly.IsInInterior(GPC.Position) Then
                UI.Screen.FadeOut(200)
                Wait(200)
                Dim penalty As Double = VehiclePrice / 99
                If VehPreview.HasBeenDamagedBy(GPC) Then
                    GP.Money = (PlayerCash - (VehiclePrice / 99))
                    UI.Notification.PostTicker($"${System.Math.Round(penalty).ToString("###,###")} {GetLangEntry("HELP_PENALTY")}", False)
                End If
                ConfirmMenu.Visible = True
                VehPreview.IsUndriveable = True
                VehPreview.LockStatus = VehicleLockStatus.IgnoredByPlayer
                VehPreview.Position = VehPreviewPos
                VehPreview.Heading = Radius
                NativeFunction.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                NativeFunction.Call(Hash.SET_VEHICLE_FIXED, VehPreview)
                GPC.Position = PlayerLastPos
                TestDrive = 1
                HideHud = True
                Wait(200)
                UI.Screen.FadeIn(200)
                ShowVehicleName = True
                wsCamera.RepositionFor(VehPreview)
            ElseIf TestDrive = 2 AndAlso GPC.IsInVehicle Then
                TestDrive = TestDrive + 1
            End If

            If DrawSpotLight = True Then
                World.DrawSpotLightWithShadow(VehPreviewPos + Vector3.WorldUp * 4 + Vector3.WorldNorth * 4, Vector3.WorldSouth + Vector3.WorldDown, Color.White, 30, 30, 100, 50, -1)
                World.DrawSpotLight(VehPreviewPos + Vector3.WorldUp * 4 + Vector3.WorldNorth * 4, Vector3.WorldSouth + Vector3.WorldDown, Color.White, 30, 30, 100, 50, -1)
            End If

            If IsControlJustPressed(GTA.Control.Context) AndAlso PdmDoorDist < 3.0 AndAlso Not GPC.IsInVehicle AndAlso GP.WantedLevel = 0 AndAlso TaskScriptStatus = -1 Then
                TaskScriptStatus = 0

                HideHud = True
                Wait(200)
                UI.Screen.FadeIn(200)
                SelectedVehicle = optLastVehName
                PlayerLastPos = GPC.Position
                If VehPreview = Nothing Then
                    VehPreview = CreateVehicleFromHash(optLastVehHash, VehPreviewPos, 6.122209)
                Else
                    VehPreview.Delete()
                    VehPreview = CreateVehicleFromHash(optLastVehHash, VehPreviewPos, 6.122209)
                End If
                UpdateVehPreview()
                wsCamera.RepositionFor(VehPreview)
                VehicleName = SelectedVehicle
                ShowVehicleName = True
                VehPreview.Heading = Radius
                VehPreview.LockStatus = VehicleLockStatus.IgnoredByPlayer
                VehPreview.DirtLevel = 0
                MainMenu.Visible = True
            End If

            If _menuPool.AreAnyVisible Then
                SuspendKeys()

                If IsControlJustReleased(keyDoor) AndAlso TaskScriptStatus = 0 Then
                    If VehPreview.Doors.Item(VehicleDoorIndex.FrontLeftDoor).IsOpen Then
                        NativeFunction.Call(Hash.SET_VEHICLE_DOORS_SHUT, VehPreview, False)
                    Else
                        VehPreview.Doors.Item(VehicleDoorIndex.BackLeftDoor).Open()
                        VehPreview.Doors.Item(VehicleDoorIndex.BackRightDoor).Open()
                        VehPreview.Doors.Item(VehicleDoorIndex.FrontLeftDoor).Open()
                        VehPreview.Doors.Item(VehicleDoorIndex.FrontRightDoor).Open()
                        VehPreview.Doors.Item(VehicleDoorIndex.Hood).Open()
                        VehPreview.Doors.Item(VehicleDoorIndex.Trunk).Open()
                    End If
                ElseIf IsControlJustPressed(keyRoof) AndAlso TaskScriptStatus = 0 Then
                    If VehPreview.RoofState = VehicleRoofState.Closed Then
                        NativeFunction.Call(Hash.LOWER_CONVERTIBLE_ROOF, VehPreview, False)
                    Else
                        NativeFunction.Call(Hash.RAISE_CONVERTIBLE_ROOF, VehPreview, False)
                    End If
                End If

                If IsControlJustReleased(keyCamera) Then
                    If Not VehPreview.ClassType = VehicleClass.Motorcycles Then
                        If wsCamera.MainCameraPosition = CameraPosition.Car Then
                            wsCamera.MainCameraPosition = CameraPosition.Interior
                        Else
                            wsCamera.MainCameraPosition = CameraPosition.Car
                        End If
                    End If
                End If

                If Game.IsControlJustPressed(keyZoom) Then
                    If wsCamera.MainCameraPosition = CameraPosition.Car Then
                        If wsCamera.CameraZoom = 5.0 Then
                            Do While wsCamera.CameraZoom > 3.5
                                Yield()
                                wsCamera.CameraZoom -= 0.1
                            Loop
                        Else
                            Do While wsCamera.CameraZoom < 5.0
                                Yield()
                                wsCamera.CameraZoom += 0.1
                            Loop
                        End If
                    End If
                End If
            End If

            If blipName = "NULL" Then
                If RequestAdditionTextFile("LFI_F") Then
                    blipName = Game.GetLocalizedString("collision_vt4m0x")
                    PdmBlip.Name = blipName 'GetLangEntry("PREMIUM_DELUXE_MOTORSPORT")
                End If
            End If

            If IsMissionActive Or GP.WantedLevel > 1 Then
                PdmBlip.Alpha = 0
            Else
                PdmBlip.Alpha = 255
            End If
        Catch ex As Exception
            logger.Log(ex.Message & " " & ex.StackTrace)
            logger.Log(blipName)
        End Try
    End Sub

    Public Sub OnAborted() Handles MyBase.Aborted
        Try
            PdmBlip.Delete()
            UI.Screen.FadeIn(200)
            If Not pdmPed = Nothing Then pdmPed.Delete()
        Catch ex As Exception
            logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub
End Class