using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class TextBoxConSombra : UserControl
{
    // Colores de fondo
    private Color blueColor = ColorTranslator.FromHtml("#84AEFF");
    private Color whiteColor = Color.White;

    // Variables para el efecto sombra
    private int shadowOffset = 5;      // Desfase para que se vea la sombra (derecha e inferior)
    private int cornerRadius = 15;     // Radio de esquinas para ambos rectángulos

    // TextBox interno para escribir
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    private TextBox innerTextBox;

    public TextBoxConSombra()
    {
        this.DoubleBuffered = true;
        this.ResizeRedraw = true;
        this.Size = new Size(300, 45);

        // Debido a que DesignMode no es fiable en el constructor,
        // comprobamos mediante LicenseManager.UsageMode
        if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        {
            InitializeControl();
        }
        else
        {
            innerTextBox = new TextBox();
            this.Controls.Add(innerTextBox);
        }
    }

    private void InitializeControl()
    {
        innerTextBox = new TextBox();
        innerTextBox.BorderStyle = BorderStyle.None;
        innerTextBox.ForeColor = Color.Black;
        innerTextBox.BackColor = whiteColor;
        innerTextBox.Font = new Font("Arial", 10, FontStyle.Regular);
        innerTextBox.Multiline = true;
        this.Controls.Add(innerTextBox);
    }

    public override string Text
    {
        get { return innerTextBox.Text; }
        set { innerTextBox.Text = value; }
    }

    public int MaxLength { get; internal set; }
    public bool Multiline { get; internal set; }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // 1) Dibuja el rectángulo azul (fondo completo del control)
        Rectangle blueRect = this.ClientRectangle;
        using (GraphicsPath bluePath = GetRoundedRectangle(blueRect, cornerRadius))
        using (SolidBrush blueBrush = new SolidBrush(blueColor))
        {
            e.Graphics.FillPath(blueBrush, bluePath);
        }

        // 2) Dibuja el rectángulo blanco (más pequeño y posicionado en la parte superior izquierda)
        Rectangle whiteRect = new Rectangle(0, 0, this.Width - shadowOffset, this.Height - shadowOffset);
        using (GraphicsPath whitePath = GetRoundedRectangle(whiteRect, cornerRadius))
        using (SolidBrush whiteBrush = new SolidBrush(whiteColor))
        {
            e.Graphics.FillPath(whiteBrush, whitePath);
        }

        PositionInnerTextBox();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        // Opcional: establecer la región completa del control para que sea redondeada
        if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        {
            using (GraphicsPath path = GetRoundedRectangle(this.ClientRectangle, cornerRadius))
            {
                this.Region = new Region(path);
            }
        }
        PositionInnerTextBox();
    }

    // Posiciona el TextBox dentro del rectángulo blanco, dejando márgenes internos
    private void PositionInnerTextBox()
    {
        int margin = 8;
        if (innerTextBox != null)
        {
            innerTextBox.SetBounds(
                margin,
                margin,
                (this.Width - shadowOffset) - margin * 2,
                (this.Height - shadowOffset) - margin * 2
            );
        }
    }

    // Crea un GraphicsPath con esquinas redondeadas a partir de un rectángulo y un radio
    private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        // Esquina superior izquierda
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        // Esquina superior derecha
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        // Esquina inferior derecha
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        // Esquina inferior izquierda
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

        path.CloseFigure();
        return path;
    }

    internal void Select(int length, int v)
    {
        throw new NotImplementedException();
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // TextBoxConSombra
            // 
            this.Name = "TextBoxConSombra";
            this.Load += new System.EventHandler(this.TextBoxConSombra_Load);
            this.ResumeLayout(false);

    }

    private bool soloNumeros = false;

    public bool SoloNumeros
    {
        get { return soloNumeros; }
        set
        {
            soloNumeros = value;
            if (soloNumeros)
                innerTextBox.KeyPress += InnerTextBox_KeyPress_SoloNumeros;
            else
                innerTextBox.KeyPress -= InnerTextBox_KeyPress_SoloNumeros;
        }
    }

    private void InnerTextBox_KeyPress_SoloNumeros(object sender, KeyPressEventArgs e)
    {
        // Permitir solo números y control (como backspace)
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    public bool ReadOnly
    {
        get { return innerTextBox.ReadOnly; }
        set { innerTextBox.ReadOnly = value; }
    }

    private void TextBoxConSombra_Load(object sender, EventArgs e)
    {

    }
}
