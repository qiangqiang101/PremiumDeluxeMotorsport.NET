﻿Imports System.Windows.Forms
Imports System.Drawing
Imports GTA
Imports GTA.Math
Imports GTA.Native
Imports INMNativeUI
Imports Control = GTA.Control

Public Enum CameraPosition
    Car
    Interior
    Engine
    RearEngine
    Trunk
    FrontBumper
    RearBumper
    Grille
    Tank
    Plaque
    BackPlate
    FrontPlate
    Wheels
    Hood
    RearWindscreen
    BikeExhaust
    FrontMuguard
    RearMuguard
End Enum

Public Enum CameraRotationMode
    Around
    FirstPerson
End Enum

Public Class WorkshopCamera
    Private _mainCamera As Camera
    Private _isDragging As Boolean
    Private _dragOffset As PointF
    Private _target As Entity
    Private _targetPos As Vector3

    Private _internalCameraPosition As CameraPosition


    ' LERPING
    Public IsLerping As Boolean
    Private startTime As DateTime
    Private duration As Single

    Private startValuePosition As Vector3
    Private endValuePosition As Vector3

    Private startValueRotation As Vector3
    Private endValueRotation As Vector3

    Public Property MainCameraPosition() As CameraPosition
        Get
            Return _internalCameraPosition
        End Get
        Set
            OnCameraChange(Value)
            _internalCameraPosition = Value
        End Set
    End Property

    Public ReadOnly Property Rotation() As Vector3
        Get
            Return _mainCamera.Rotation
        End Get
    End Property

    Public Property RotationMode() As CameraRotationMode
        Get
            Return m_RotationMode
        End Get
        Set
            m_RotationMode = Value
        End Set
    End Property
    Private m_RotationMode As CameraRotationMode

    Public Property CameraZoom() As Single
        Get
            Return _cameraZoom
        End Get
        Set
            If _mainCamera IsNot Nothing Then
                Dim dir = CutsceneManager.RotationToDirection(_mainCamera.Rotation)
                _mainCamera.Position += dir * (_cameraZoom - Value)
            End If
            _cameraZoom = Value
        End Set
    End Property

    Public Property CameraClamp() As CameraClamp
        Get
            Return m_CameraClamp
        End Get
        Set
            m_CameraClamp = Value
        End Set
    End Property
    Private m_CameraClamp As CameraClamp

    Private Sub OnCameraChange(newPos As CameraPosition)
        Select Case newPos
            Case CameraPosition.Car
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    If (_internalCameraPosition <> CameraPosition.Car AndAlso _internalCameraPosition <> CameraPosition.Interior) Then
                        _mainCamera.PointAt(_target)
                        _targetPos = _target.Position
                        _cameraZoom = 5.0
                        RotationMode = CameraRotationMode.Around
                        CameraClamp = New CameraClamp() With {.MaxVerticalValue = -40.0, .MinVerticalValue = -3.0}
                        _mainCamera.Shake(CameraShake.Hand, 0.5)

                        endValuePosition = startValuePosition
                        endValueRotation = startValueRotation
                        duration = 1000.0
                        IsLerping = True
                        startTime = DateTime.Now

                        startValuePosition = _mainCamera.Position
                        startValueRotation = _mainCamera.Rotation
                    ElseIf _internalCameraPosition = CameraPosition.Interior Then
                        RepositionFor(DirectCast(_target, Vehicle))
                    End If
                End If
                Exit Select
            Case CameraPosition.Wheels
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    If _internalCameraPosition <> CameraPosition.Car Then
                        RepositionFor(DirectCast(_target, Vehicle))
                    End If
                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -60.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 130.0, '- 510.0,
                        .RightHorizontalValue = _target.Heading - 380.0 '- 380.0
                    }
                    _cameraZoom = 4.0
                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _target.Position - _target.RightVector * 4.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, _target.Heading - 90.0)
                End If
                Exit Select
            Case CameraPosition.Interior
                If True Then
                    IsLerping = False
                    Dim headPos = GetBonePosition(_target, "seat_dside_f")
                    World.DestroyAllCameras()
                    _mainCamera = World.CreateCamera(headPos + New Vector3(0.2, 0.3, 0.6), New Vector3(0, 0, _target.Heading), GameplayCamera.FieldOfView)
                    World.RenderingCamera = _mainCamera
                    _targetPos = headPos
                    RotationMode = CameraRotationMode.FirstPerson
                    Game.Player.Character.Alpha = 255
                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -60.0,
                        .MinVerticalValue = -3.0
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.Engine
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "engine")
                    _cameraZoom = 3.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * 3.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, -_target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)


                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 250.6141,
                        .RightHorizontalValue = _target.Heading - 105.30705 '- 470.79
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.Hood
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "bonnet")
                    _cameraZoom = 3.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * 3.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, -_target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)


                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 250.6141,
                        .RightHorizontalValue = _target.Heading - 105.30705 '- 470.79
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.FrontMuguard
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    If _target.HasBone("misc_i") Then
                        _targetPos = GetBonePosition(_target, "misc_i")
                    Else
                        _targetPos = GetBonePosition(_target, "forks_l")
                    End If

                    _cameraZoom = 3.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * 3.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, -_target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)


                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 250.6141,
                        .RightHorizontalValue = _target.Heading - 105.30705 '- 470.79
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.Trunk
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    If _target.HasBone("boot") Then
                        _targetPos = GetBonePosition(_target, "boot")
                    Else
                        _targetPos = GetBonePosition(_target, "bumper_r")
                    End If
                    _cameraZoom = 3.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -3.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, _target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                        .RightHorizontalValue = _target.Heading - 300.0
                         }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.BikeExhaust
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "exhaust")
                    _cameraZoom = 3.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -3.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, _target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                    .MaxVerticalValue = -40.0,
                    .MinVerticalValue = -3.0,
                    .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                    .RightHorizontalValue = _target.Heading - 300.0
                }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.RearMuguard
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "misc_d")
                    _cameraZoom = 3.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -3.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, _target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                    .MaxVerticalValue = -40.0,
                    .MinVerticalValue = -3.0,
                    .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                    .RightHorizontalValue = _target.Heading - 300.0
                }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.RearWindscreen
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    If _target.HasBone("windscreen_r") Then
                        _targetPos = GetBonePosition(_target, "windscreen_r")
                    Else
                        _targetPos = GetBonePosition(_target, "bumper_r")
                    End If
                    _cameraZoom = 3.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -3.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, _target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                    .MaxVerticalValue = -40.0,
                    .MinVerticalValue = -3.0,
                    .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                    .RightHorizontalValue = _target.Heading - 300.0
                }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.RearEngine
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    GetBonePosition(_target, "engine")
                    _cameraZoom = 3.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -3.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, _target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                    .MaxVerticalValue = -40.0,
                    .MinVerticalValue = -3.0,
                    .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                    .RightHorizontalValue = _target.Heading - 300.0
                }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.FrontBumper
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "neon_f")
                    _cameraZoom = 2.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * 2.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, -_target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)


                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 250.6141,
                        .RightHorizontalValue = _target.Heading - 105.30705 '- 470.79
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.Grille
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "neon_f")
                    _cameraZoom = 2.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * 2.0 + _target.UpVector * 3.0
                    endValueRotation = New Vector3(0, 0, -_target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)


                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 250.6141,
                        .RightHorizontalValue = _target.Heading - 105.30705 '- 470.79
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.RearBumper
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "neon_b")
                    _cameraZoom = 2.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -2.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, _target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                        .RightHorizontalValue = _target.Heading - 300.0
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.Tank
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "neon_b")
                    _cameraZoom = 2.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -2.0
                    endValueRotation = New Vector3(0, 0, _target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                        .RightHorizontalValue = _target.Heading - 300.0
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.Plaque
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around

                    Select Case VehPreview.Model
                        Case "buccaneer2", "faction2", "moonbeam2", "slamvan3", "faction3"
                            _targetPos = GetBonePosition(_target, "misc_h")
                        Case "voodoo", "chino2"
                            _targetPos = GetBonePosition(_target, "misc_j")
                        Case "primo2"
                            _targetPos = GetBonePosition(_target, "misc_d")
                        Case "sabregt2", "virgo2"
                            _targetPos = GetBonePosition(_target, "misc_n")
                        Case "tornado5"
                            _targetPos = GetBonePosition(_target, "misc_o")
                        Case "minivan2"
                            _targetPos = GetBonePosition(_target, "misc_c")
                        Case Else
                            _targetPos = GetBonePosition(_target, "windscreen_r")
                    End Select

                    _cameraZoom = 0.6

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -0.5 + _target.UpVector * 0.1
                    Dim tRot = _target.Heading
                    If tRot > 180.0 Then
                        tRot -= 360.0
                    End If
                    endValueRotation = New Vector3(0, 0, tRot)
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -20.0,
                        .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                        .RightHorizontalValue = _target.Heading - 300.0
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.BackPlate
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    If _target.HasBone("platelight") Then
                        _targetPos = GetBonePosition(_target, "platelight")
                    Else
                        _targetPos = GetBonePosition(_target, "neon_b")
                    End If
                    _cameraZoom = 1.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * -1.0 + _target.UpVector
                    endValueRotation = New Vector3(0, 0, _target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)
                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 60.0, '- 410.0,
                        .RightHorizontalValue = _target.Heading - 300.0
                    }
                    _justSwitched = True
                End If
                Exit Select
            Case CameraPosition.FrontPlate
                If True Then
                    Game.Player.Character.Alpha = 255
                    RotationMode = CameraRotationMode.Around
                    _targetPos = GetBonePosition(_target, "neon_f")
                    _cameraZoom = 1.0

                    startValueRotation = _mainCamera.Rotation
                    startValuePosition = _mainCamera.Position
                    duration = 1000.0
                    IsLerping = True
                    startTime = DateTime.Now

                    endValuePosition = _targetPos + _target.ForwardVector * 2.0 + _target.UpVector * 2.0
                    endValueRotation = New Vector3(0, 0, -_target.Heading)
                    _mainCamera.StopPointing()
                    _mainCamera.PointAt(_targetPos)


                    CameraClamp = New CameraClamp() With {
                        .MaxVerticalValue = -40.0,
                        .MinVerticalValue = -3.0,
                        .LeftHorizontalValue = _target.Heading - 250.6141,
                        .RightHorizontalValue = _target.Heading - 105.30705 '- 470.79
                    }
                    _justSwitched = True
                End If
                Exit Select
        End Select
    End Sub

    Public Sub New()
        World.DestroyAllCameras()
    End Sub

    Public Sub [Stop]()
        World.RenderingCamera = Nothing
        World.DestroyAllCameras()
    End Sub

    Public Sub RepositionFor(lowrider As Vehicle)
        World.DestroyAllCameras()
        _mainCamera = World.CreateCamera(lowrider.Position - lowrider.ForwardVector * 5.0 + New Vector3(0, 0, 1.5), CutsceneManager.DirectionToRotation(lowrider.ForwardVector * -5.0), GameplayCamera.FieldOfView)
        _mainCamera.PointAt(lowrider)
        _mainCamera.Position = CameraPos
        _mainCamera.Rotation = CameraRot
        World.RenderingCamera = _mainCamera
        _target = lowrider
        _targetPos = lowrider.Position
        _cameraZoom = 5.0
        _internalCameraPosition = CameraPosition.Car
        RotationMode = CameraRotationMode.Around
        CameraClamp = New CameraClamp() With {
            .MaxVerticalValue = -40.0,
            .MinVerticalValue = -3.0
        }
        _mainCamera.Shake(CameraShake.Hand, 0.5)
    End Sub

    Public Function IsMouseInMenu() As Boolean
        Dim safe = UIMenu.GetSafezoneBounds()
        Return UIMenu.IsMouseInBounds(New Point(safe.X, safe.Y), New Size(431, 107 + 37 + (38 * 10) + 18 + 18))
    End Function



    Private _justSwitched As Boolean
    Public Sub Update()
        If _mainCamera Is Nothing Then
            Return
        End If
        Game.DisableControlThisFrame(0, Control.VehicleMouseControlOverride)

        If IsLerping Then
            Dim t = DateTime.Now
            If t.Subtract(startTime).TotalMilliseconds > duration Then
                IsLerping = False
                _mainCamera.Position = endValuePosition
                _mainCamera.Rotation = endValueRotation
                Return
            End If

            _mainCamera.Position = LerpVector(CSng(t.Subtract(startTime).TotalMilliseconds), duration, startValuePosition, endValuePosition)
            _mainCamera.Rotation = LerpVector(CSng(t.Subtract(startTime).TotalMilliseconds), duration, startValueRotation, endValueRotation)
            Return
        End If

        If _justSwitched Then
            _justSwitched = False
            Return
        End If

        If Game.IsControlJustPressed(0, Control.Attack) AndAlso Not _isDragging AndAlso Not IsMouseInMenu() Then
            _isDragging = True
            Dim mouseX = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.CursorX))
            Dim mouseY = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.CursorY))

            Native.Function.Call(DirectCast(&H8DB8CFFD58B62552UL, Hash), 4)


            mouseX = (mouseX * 2) - 1
            mouseY = (mouseY * 2) - 1

            _dragOffset = New PointF(mouseX, mouseY)
        End If
        If Game.IsControlJustReleased(0, Control.Attack) AndAlso _isDragging Then
            _isDragging = False
            _dragOffset = New Point()
            Native.Function.Call(DirectCast(&H8DB8CFFD58B62552UL, Hash), 0)
        End If

        If RotationMode = CameraRotationMode.Around Then
            If _isDragging Then
                Native.Function.Call(Hash._SHOW_CURSOR_THIS_FRAME)
                Dim dir = CutsceneManager.RotationToDirection(_mainCamera.Rotation)

                Dim len = (_targetPos - _mainCamera.Position).Length()


                Dim rotLeft = _mainCamera.Rotation + New Vector3(0, 0, -10)
                Dim rotRight = _mainCamera.Rotation + New Vector3(0, 0, 10)
                Dim right = CutsceneManager.RotationToDirection(rotRight) - CutsceneManager.RotationToDirection(rotLeft)

                Dim rotUp = _mainCamera.Rotation + New Vector3(-20, 0, 0)
                Dim rotDown = _mainCamera.Rotation + New Vector3(20, 0, 0)
                Dim up = CutsceneManager.RotationToDirection(rotUp) - CutsceneManager.RotationToDirection(rotDown)


                Dim mouseX = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.CursorX))
                Dim mouseY = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.CursorY))

                mouseX = (mouseX * 2) - 1
                mouseY = (mouseY * 2) - 1

                Dim rotation As New Vector3()

                If Not IsCameraClamped(True, mouseX - _dragOffset.X) Then
                    rotation += right * 15 * (mouseX - _dragOffset.X)
                End If
                If Not IsCameraClamped(False, mouseY - _dragOffset.Y) Then
                    rotation += up * -((mouseY - _dragOffset.Y) * 15)
                End If
                rotation += dir * (len - CameraZoom)

                _mainCamera.Position += rotation

                _dragOffset = New PointF(mouseX, mouseY)
            End If

            If Game.CurrentInputMode = InputMode.GamePad Then
                Dim dir = CutsceneManager.RotationToDirection(_mainCamera.Rotation)

                Dim len = (_targetPos - _mainCamera.Position).Length()

                Dim rotLeft = _mainCamera.Rotation + New Vector3(0, 0, -10)
                Dim rotRight = _mainCamera.Rotation + New Vector3(0, 0, 10)
                Dim right = CutsceneManager.RotationToDirection(rotRight) - CutsceneManager.RotationToDirection(rotLeft)

                Dim rotUp = _mainCamera.Rotation + New Vector3(-20, 0, 0)
                Dim rotDown = _mainCamera.Rotation + New Vector3(20, 0, 0)
                Dim up = CutsceneManager.RotationToDirection(rotUp) - CutsceneManager.RotationToDirection(rotDown)


                Dim mouseX = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.LookLeftRight))
                Dim mouseY = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.LookUpDown))

                Dim rotation As New Vector3()

                If Not IsCameraClamped(True, mouseX) Then
                    rotation += right * mouseX * 0.6
                End If
                If Not IsCameraClamped(False, mouseY) Then
                    rotation += up * -mouseY * 0.5
                End If
                rotation += dir * (len - CameraZoom)

                _mainCamera.Position += rotation
            End If
        ElseIf RotationMode = CameraRotationMode.FirstPerson Then
            If _isDragging Then
                Native.Function.Call(Hash._SHOW_CURSOR_THIS_FRAME)

                Dim mouseX = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.CursorX))
                Dim mouseY = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.CursorY))


                mouseX = (mouseX * 2) - 1
                mouseY = (mouseY * 2) - 1

                mouseY *= -1

                Dim right = New Vector3(0, 0, 1)
                Dim up = New Vector3(1, 0, 0)

                Dim rotation As New Vector3()

                If Not IsCameraClamped(True, mouseX - _dragOffset.X) Then
                    rotation += right * 20 * (mouseX - _dragOffset.X)
                End If
                If Not IsCameraClamped(False, mouseY - _dragOffset.Y) Then
                    rotation += up * -((mouseY - _dragOffset.Y) * 20)
                End If

                _mainCamera.Rotation += rotation

                _dragOffset = New PointF(mouseX, mouseY)
            End If
            If Game.CurrentInputMode = InputMode.GamePad Then
                Dim mouseX = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.LookLeftRight))
                Dim mouseY = Native.Function.Call(Of Single)(Hash.GET_CONTROL_NORMAL, 0, CInt(Control.LookUpDown))

                mouseX *= -1

                Dim right = New Vector3(0, 0, 1)
                Dim up = New Vector3(1, 0, 0)

                Dim rotation As New Vector3()

                If Not IsCameraClamped(True, mouseX) Then
                    rotation += right * mouseX * 4.0
                End If
                If Not IsCameraClamped(False, mouseY) Then
                    rotation += up * -mouseY * 4.0
                End If

                _mainCamera.Rotation += rotation
            End If
        End If

        If Not MainCameraPosition = CameraPosition.Interior Then
            CameraPos = _mainCamera.Position
            CameraRot = _mainCamera.Rotation
        End If
    End Sub

    Public Function IsCameraClamped(horizontally As Boolean, delta As Single) As Boolean
        If horizontally Then
            Dim goingLeft = delta < 0
            If goingLeft AndAlso CameraClamp.LeftHorizontalValue = Nothing Then
                Return False
            End If
            If Not goingLeft AndAlso CameraClamp.RightHorizontalValue = Nothing Then
                Return False
            End If


            If CameraClamp.LeftHorizontalValue > 180.0 Then
                CameraClamp.LeftHorizontalValue = CameraClamp.LeftHorizontalValue - (360 * CInt(CameraClamp.LeftHorizontalValue / 360) + 1)
            End If

            If CameraClamp.RightHorizontalValue > 180.0 Then
                CameraClamp.RightHorizontalValue = CameraClamp.RightHorizontalValue - (360 * CInt(CameraClamp.RightHorizontalValue / 360) + 1)
            End If

            Dim sameHemisphereLeft = (_mainCamera.Rotation.Z > 0F AndAlso CameraClamp.LeftHorizontalValue > 0F) OrElse (_mainCamera.Rotation.Z < 0F AndAlso CameraClamp.LeftHorizontalValue < 0F)

            Dim sameHemisphereRight = (_mainCamera.Rotation.Z > 0F AndAlso CameraClamp.RightHorizontalValue > 0F) OrElse (_mainCamera.Rotation.Z < 0F AndAlso CameraClamp.RightHorizontalValue < 0F)

            If goingLeft AndAlso _mainCamera.Rotation.Z > CameraClamp.RightHorizontalValue AndAlso sameHemisphereRight Then
                Return True
            End If
            If Not goingLeft AndAlso _mainCamera.Rotation.Z < CameraClamp.LeftHorizontalValue AndAlso sameHemisphereLeft Then
                Return True
            End If
            Return False
        Else
            Dim goingDown = delta < 0
            If goingDown AndAlso CameraClamp.MinVerticalValue = Nothing Then
                Return False
            End If
            If Not goingDown AndAlso CameraClamp.MaxVerticalValue = Nothing Then
                Return False
            End If
            If goingDown AndAlso _mainCamera.Rotation.X > CameraClamp.MinVerticalValue Then
                Return True
            End If
            If Not goingDown AndAlso _mainCamera.Rotation.X < CameraClamp.MaxVerticalValue Then
                Return True
            End If
            Return False
        End If
    End Function

    Public Shared Function Clamp(value As Single, min As Single, max As Single) As Single
        If value > max Then
            Return max
        End If
        If value < min Then
            Return min
        End If
        Return value
    End Function

    Public Shared Function GetBonePosition(target As Entity, bone As String) As Vector3
        Return target.GetBoneCoord(bone)
    End Function

    Public Shared Function QuadraticEasing(currentTime As Single, startValue As Single, changeInValue As Single, duration As Single) As Single
        currentTime /= duration / 2
        If currentTime < 1 Then
            Return changeInValue / 2 * currentTime * currentTime + startValue
        End If
        currentTime -= 1
        Return -changeInValue / 2 * (currentTime * (currentTime - 2) - 1) + startValue
    End Function

    Public Shared Function LerpVector(currentTime As Single, duration As Single, startValue As Vector3, destination As Vector3) As Vector3
        Dim output = New Vector3()
        output.X = QuadraticEasing(currentTime, startValue.X, destination.X - startValue.X, duration)
        output.Y = QuadraticEasing(currentTime, startValue.Y, destination.Y - startValue.Y, duration)
        output.Z = QuadraticEasing(currentTime, startValue.Z, destination.Z - startValue.Z, duration)
        Return output
    End Function

    Private _cameraZoom As Single
End Class
