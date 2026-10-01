''' <summary>
''' TabControl mit flacher Optik und Schließen-Kreuz auf allen Tabs außer dem ersten.
''' Ein Klick auf das Kreuz (oder Mittelklick auf den Tab) löst <see cref="TabCloseRequested"/> aus.
''' </summary>
Public Class FlatTabControl
    Inherits TabControl

    Public Event TabCloseRequested(sender As Object, page As TabPage)

    Private _closeIcon As Bitmap

    Public Sub New()
        DrawMode = TabDrawMode.OwnerDrawFixed
        Padding = New Point(22, 6)
        SetStyle(ControlStyles.ResizeRedraw, True)
    End Sub

    Private Function IsClosable(index As Integer) As Boolean
        Return index > 0
    End Function

    Private Function GetCloseRect(index As Integer) As Rectangle
        Dim tab = GetTabRect(index)
        Dim size = Theme.ScaleForDpi(Me, 10)
        Return New Rectangle(tab.Right - size - Theme.ScaleForDpi(Me, 8), tab.Top + (tab.Height - size) \ 2, size, size)
    End Function

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        Dim tab = GetTabRect(e.Index)
        Dim selected = (e.Index = SelectedIndex)

        Using background As New SolidBrush(If(selected, Theme.Surface, Theme.SurfaceAlt))
            e.Graphics.FillRectangle(background, tab)
        End Using
        If selected Then
            Using accent As New SolidBrush(Theme.Accent)
                e.Graphics.FillRectangle(accent, tab.Left, tab.Bottom - Theme.ScaleForDpi(Me, 2), tab.Width, Theme.ScaleForDpi(Me, 2))
            End Using
        End If

        Dim textRect = Rectangle.FromLTRB(tab.Left + Theme.ScaleForDpi(Me, 8), tab.Top, tab.Right, tab.Bottom)
        If IsClosable(e.Index) Then
            Dim closeRect = GetCloseRect(e.Index)
            textRect.Width = closeRect.Left - textRect.Left
            If _closeIcon Is Nothing Then _closeIcon = Theme.CreateIcon(Theme.Glyph.Close, closeRect.Width, Theme.TextSecondary)
            e.Graphics.DrawImage(_closeIcon, closeRect)
        End If

        TextRenderer.DrawText(e.Graphics, TabPages(e.Index).Text, Font, textRect,
                              If(selected, Theme.TextPrimary, Theme.TextSecondary),
                              TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)

        For i = 0 To TabCount - 1
            If Not IsClosable(i) Then Continue For

            Dim closeHitArea = GetCloseRect(i)
            closeHitArea.Inflate(Theme.ScaleForDpi(Me, 5), Theme.ScaleForDpi(Me, 5))
            Dim hit = (e.Button = MouseButtons.Left AndAlso closeHitArea.Contains(e.Location)) OrElse
                      (e.Button = MouseButtons.Middle AndAlso GetTabRect(i).Contains(e.Location))
            If hit Then
                RaiseEvent TabCloseRequested(Me, TabPages(i))
                Return
            End If
        Next
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then _closeIcon?.Dispose()
        MyBase.Dispose(disposing)
    End Sub
End Class
