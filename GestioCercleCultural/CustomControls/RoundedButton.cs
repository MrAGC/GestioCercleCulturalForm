using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedButton : Button
{
    private Color _normalColor = ColorTranslator.FromHtml("#FFC336"); // Amarillo
    private Color _clickColor = ColorTranslator.FromHtml("#E6AF2C");  // Un poco más oscuro
    private int _cornerRadius = 15;

    public RoundedButton()
    {
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.BackColor = _normalColor;
        this.ForeColor = Color.Black;
        this.Font = new Font("Arial", 9, FontStyle.Bold);
        this.Size = new Size(120, 50); // Ajusta a tu gusto
        this.Cursor = Cursors.Hand;

        // Cambiar color al presionar y soltar
        this.MouseDown += (sender, e) => this.BackColor = _clickColor;
        this.MouseUp += (sender, e) => this.BackColor = _normalColor;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Área completa del botón
        Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

        using (GraphicsPath path = GetRoundedRectangle(rect, _cornerRadius))
        {
            // Relleno sin borde visible
            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                g.FillPath(brush, path);
            }
        }

        // Texto centrado
        TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
        TextRenderer.DrawText(g, this.Text, this.Font, rect, this.ForeColor, flags);
    }

    // Ajustar la región del control para que sea redondeado de verdad
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        using (GraphicsPath path = GetRoundedRectangle(new Rectangle(0, 0, this.Width, this.Height), _cornerRadius))
        {
            this.Region = new Region(path);
        }
    }

    private GraphicsPath GetRoundedRectangle(Rectangle rect, int cornerRadius)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
        path.AddArc(rect.Right - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90);
        path.AddArc(rect.Right - cornerRadius, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
        path.AddArc(rect.X, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
        path.CloseFigure();
        return path;
    }
}
