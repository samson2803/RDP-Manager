Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Reflection

''' <summary>
''' Zentrale Farben und Styling-Helfer für eine flache Optik im Stil von Windows 11.
''' </summary>
Public Module Theme
    Public ReadOnly Accent As Color = Color.FromArgb(0, 103, 192)
    Public ReadOnly AccentHover As Color = Color.FromArgb(25, 117, 197)
    Public ReadOnly AccentPressed As Color = Color.FromArgb(49, 131, 202)
    Public ReadOnly AccentLight As Color = Color.FromArgb(204, 228, 247)
    Public ReadOnly Surface As Color = Color.White
    Public ReadOnly SurfaceAlt As Color = Color.FromArgb(243, 243, 243)
    Public ReadOnly Hover As Color = Color.FromArgb(235, 235, 235)
    Public ReadOnly Pressed As Color = Color.FromArgb(224, 224, 224)
    Public ReadOnly Border As Color = Color.FromArgb(229, 229, 229)
    Public ReadOnly ControlBorder As Color = Color.FromArgb(209, 209, 209)
    Public ReadOnly TextPrimary As Color = Color.FromArgb(27, 27, 27)
    Public ReadOnly TextSecondary As Color = Color.FromArgb(96, 96, 96)

    ''' <summary>Zeichen aus "Segoe Fluent Icons" (Windows 11) bzw. "Segoe MDL2 Assets" (Windows 10).</summary>
    Public NotInheritable Class Glyph
        Public Const Connect As Char = ChrW(&HE768)
        Public Const Add As Char = ChrW(&HE710)
        Public Const Edit As Char = ChrW(&HE70F)
        Public Const Delete As Char = ChrW(&HE74D)
        Public Const Settings As Char = ChrW(&HE713)
        Public Const Power As Char = ChrW(&HE7E8)
        Public Const Monitor As Char = ChrW(&HE7F4)
        Public Const FullScreen As Char = ChrW(&HE740)
        Public Const Disconnect As Char = ChrW(&HE711)
        Public Const OpenExternal As Char = ChrW(&HE8A7)
        Public Const Close As Char = ChrW(&HE8BB)
    End Class

    Private ReadOnly IconFont As FontFamily = FindIconFont()

    ''' <summary>Setzt den flachen Renderer für alle Menüs, Toolbars und Statusleisten.</summary>
    Public Sub Initialize()
        ToolStripManager.Renderer = New ToolStripProfessionalRenderer(New FlatColorTable()) With {.RoundedEdges = False}
    End Sub

    ''' <summary>Rechnet einen Wert für 96 DPI auf die DPI des Controls um.</summary>
    Public Function ScaleForDpi(control As Control, value As Integer) As Integer
        Return CInt(Math.Round(value * control.DeviceDpi / 96.0))
    End Function

#Region "Icons"

    Private Function FindIconFont() As FontFamily
        Using installed As New InstalledFontCollection()
            For Each name In {"Segoe Fluent Icons", "Segoe MDL2 Assets"}
                If installed.Families.Any(Function(f) f.Name = name) Then Return New FontFamily(name)
            Next
        End Using
        Return Nothing
    End Function

    ''' <summary>Zeichnet ein Icon-Zeichen zentriert in eine Bitmap. Ohne Icon-Schrift bleibt die Bitmap leer.</summary>
    Public Function CreateIcon(glyph As Char, size As Integer, color As Color) As Bitmap
        Dim bmp As New Bitmap(size, size)
        If IconFont Is Nothing Then Return bmp

        Using g = Graphics.FromImage(bmp),
              path As New GraphicsPath(),
              brush As New SolidBrush(color)
            path.AddString(glyph.ToString(), IconFont, FontStyle.Regular, size * 0.9F, PointF.Empty, StringFormat.GenericTypographic)

            Dim bounds = path.GetBounds()
            Using centering As New Matrix()
                centering.Translate(size / 2.0F - (bounds.X + bounds.Width / 2.0F),
                                    size / 2.0F - (bounds.Y + bounds.Height / 2.0F))
                path.Transform(centering)
            End Using

            g.SmoothingMode = SmoothingMode.AntiAlias
            g.FillPath(brush, path)
        End Using
        Return bmp
    End Function

    Public Function CreateWindowIcon(glyph As Char, color As Color) As Icon
        Using bmp = CreateIcon(glyph, 32, color)
            Return Icon.FromHandle(bmp.GetHicon())
        End Using
    End Function

#End Region

#Region "Controls"

    Public Sub StylePrimaryButton(button As Button)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.FlatAppearance.MouseOverBackColor = AccentHover
        button.FlatAppearance.MouseDownBackColor = AccentPressed
        button.BackColor = Accent
        button.ForeColor = Color.White
    End Sub

    Public Sub StyleSecondaryButton(button As Button)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderColor = ControlBorder
        button.FlatAppearance.MouseOverBackColor = SurfaceAlt
        button.FlatAppearance.MouseDownBackColor = Pressed
        button.BackColor = Surface
        button.ForeColor = TextPrimary
    End Sub

    ''' <summary>Flache Liste: keine Rahmen, nur feine Trennlinien zwischen den Zeilen, höhere Zeilen.</summary>
    Public Sub StyleGrid(grid As DataGridView)
        grid.BorderStyle = BorderStyle.None
        grid.BackgroundColor = Surface
        grid.GridColor = Border
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        grid.RowHeadersVisible = False
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.ColumnHeadersHeight = ScaleForDpi(grid, 36)
        grid.RowTemplate.Height = ScaleForDpi(grid, 36)

        Dim cellPadding As New Padding(ScaleForDpi(grid, 10), 0, ScaleForDpi(grid, 10), 0)
        With grid.ColumnHeadersDefaultCellStyle
            .BackColor = Surface
            .ForeColor = TextSecondary
            .SelectionBackColor = Surface
            .SelectionForeColor = TextSecondary
            .Font = New Font("Segoe UI Semibold", 9.0F)
            .Padding = cellPadding
        End With
        With grid.DefaultCellStyle
            .BackColor = Surface
            .ForeColor = TextPrimary
            .SelectionBackColor = AccentLight
            .SelectionForeColor = TextPrimary
            .Padding = cellPadding
        End With

        ' Flackern beim Scrollen vermeiden (DoubleBuffered ist bei DataGridView nicht öffentlich).
        GetType(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic).SetValue(grid, True)

        AddHandler grid.RowPrePaint, AddressOf Grid_RowPrePaint
        AddHandler grid.CellPainting, AddressOf Grid_CellPainting
    End Sub

    ' Gepunktetes Fokus-Rechteck ausblenden – die ganze Zeile ist ja schon markiert.
    Private Sub Grid_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs)
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus
    End Sub

    ' Feine Linie unter den Spaltenköpfen.
    Private Sub Grid_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs)
        If e.RowIndex <> -1 Then Return

        e.Paint(e.ClipBounds, DataGridViewPaintParts.All)
        Using pen As New Pen(Border)
            e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1)
        End Using
        e.Handled = True
    End Sub

#End Region

    ''' <summary>Farben für Menüs/Toolbars: flach, ohne Verläufe.</summary>
    Private Class FlatColorTable
        Inherits ProfessionalColorTable

        Public Overrides ReadOnly Property MenuStripGradientBegin As Color = Surface
        Public Overrides ReadOnly Property MenuStripGradientEnd As Color = Surface
        Public Overrides ReadOnly Property ToolStripGradientBegin As Color = Surface
        Public Overrides ReadOnly Property ToolStripGradientMiddle As Color = Surface
        Public Overrides ReadOnly Property ToolStripGradientEnd As Color = Surface
        Public Overrides ReadOnly Property ToolStripBorder As Color = Border
        Public Overrides ReadOnly Property ToolStripDropDownBackground As Color = Surface
        Public Overrides ReadOnly Property MenuBorder As Color = ControlBorder
        Public Overrides ReadOnly Property MenuItemBorder As Color = Hover
        Public Overrides ReadOnly Property MenuItemSelected As Color = Hover
        Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color = Hover
        Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color = Hover
        Public Overrides ReadOnly Property MenuItemPressedGradientBegin As Color = Pressed
        Public Overrides ReadOnly Property MenuItemPressedGradientMiddle As Color = Pressed
        Public Overrides ReadOnly Property MenuItemPressedGradientEnd As Color = Pressed
        Public Overrides ReadOnly Property ImageMarginGradientBegin As Color = Surface
        Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color = Surface
        Public Overrides ReadOnly Property ImageMarginGradientEnd As Color = Surface
        Public Overrides ReadOnly Property ButtonSelectedHighlight As Color = Hover
        Public Overrides ReadOnly Property ButtonSelectedGradientBegin As Color = Hover
        Public Overrides ReadOnly Property ButtonSelectedGradientMiddle As Color = Hover
        Public Overrides ReadOnly Property ButtonSelectedGradientEnd As Color = Hover
        Public Overrides ReadOnly Property ButtonSelectedBorder As Color = Hover
        Public Overrides ReadOnly Property ButtonPressedGradientBegin As Color = Pressed
        Public Overrides ReadOnly Property ButtonPressedGradientMiddle As Color = Pressed
        Public Overrides ReadOnly Property ButtonPressedGradientEnd As Color = Pressed
        Public Overrides ReadOnly Property ButtonPressedBorder As Color = Pressed
        Public Overrides ReadOnly Property SeparatorDark As Color = Border
        Public Overrides ReadOnly Property SeparatorLight As Color = Surface
        Public Overrides ReadOnly Property StatusStripGradientBegin As Color = SurfaceAlt
        Public Overrides ReadOnly Property StatusStripGradientEnd As Color = SurfaceAlt
    End Class
End Module
