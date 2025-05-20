using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ComboBoxConSombra : UserControl
{
    // Colores de fondo
    private Color blueColor = ColorTranslator.FromHtml("#84AEFF");
    private Color whiteColor = Color.White;

    // Variables para el efecto sombra
    private int shadowOffset = 5;
    private int cornerRadius = 15;

    // ComboBox interno
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    private ComboBox innerComboBox;

    public ComboBoxConSombra()
    {
        this.DoubleBuffered = true;
        this.ResizeRedraw = true;
        this.Size = new Size(300, 45);

        InitializeControl(); // Inicializar siempre el ComboBox
    }

    private void InitializeControl()
    {
        innerComboBox = new ComboBox();
        innerComboBox.FlatStyle = FlatStyle.Flat;
        innerComboBox.ForeColor = Color.Black;
        innerComboBox.BackColor = whiteColor;
        innerComboBox.Font = new Font("Arial", 10, FontStyle.Regular);
        innerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        this.Controls.Add(innerComboBox);
        PositionInnerComboBox();
    }

    // Propiedades CORREGIDAS (vinculadas al ComboBox interno)
    public object DataSource
    {
        get => innerComboBox.DataSource;
        set
        {
            innerComboBox.DataSource = value;
            if (value != null)
            {
                innerComboBox.DisplayMember = DisplayMember;
                innerComboBox.ValueMember = ValueMember;
            }
        }
    }

    public string DisplayMember
    {
        get => innerComboBox.DisplayMember;
        set => innerComboBox.DisplayMember = value;
    }

    public string ValueMember
    {
        get => innerComboBox.ValueMember;
        set => innerComboBox.ValueMember = value;
    }

    public object SelectedValue
    {
        get => innerComboBox.SelectedValue;
        set => innerComboBox.SelectedValue = value;
    }

    public object SelectedItem
    {
        get => innerComboBox.SelectedItem;
        set => innerComboBox.SelectedItem = value;
    }

    // Resto del código original se mantiene igual
    public new string Text
    {
        get => innerComboBox.Text;
        set => innerComboBox.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ComboBox.ObjectCollection Items => innerComboBox.Items;

    public ComboBoxStyle DropDownStyle
    {
        get => innerComboBox.DropDownStyle;
        set => innerComboBox.DropDownStyle = value;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // 1) Rectángulo azul de fondo (sombra)
        Rectangle blueRect = new Rectangle(
            shadowOffset,
            shadowOffset,
            Width - shadowOffset,
            Height - shadowOffset
        );
        using (GraphicsPath bluePath = GetRoundedRectangle(blueRect, cornerRadius))
        using (SolidBrush blueBrush = new SolidBrush(blueColor))
        {
            e.Graphics.FillPath(blueBrush, bluePath);
        }

        // 2) Rectángulo blanco principal
        Rectangle whiteRect = new Rectangle(0, 0, Width - shadowOffset, Height - shadowOffset);
        using (GraphicsPath whitePath = GetRoundedRectangle(whiteRect, cornerRadius))
        using (SolidBrush whiteBrush = new SolidBrush(whiteColor))
        {
            e.Graphics.FillPath(whiteBrush, whitePath);
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        PositionInnerComboBox();
    }

    private void PositionInnerComboBox()
    {
        int margin = 8;
        if (innerComboBox != null)
        {
            innerComboBox.SetBounds(
                margin,
                margin,
                (Width - shadowOffset) - margin * 2,
                (Height - shadowOffset) - margin * 2
            );
        }
    }
    public int SelectedIndex
    {
        get => innerComboBox.SelectedIndex;
        set => innerComboBox.SelectedIndex = value;
    }

    private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

        path.CloseFigure();
        return path;
    }

    public event EventHandler SelectedIndexChanged
    {
        add => innerComboBox.SelectedIndexChanged += value;
        remove => innerComboBox.SelectedIndexChanged -= value;
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // ComboBoxConSombra
            // 
            this.Name = "ComboBoxConSombra";
            this.Load += new System.EventHandler(this.ComboBoxConSombra_Load);
            this.ResumeLayout(false);

    }

    public override BindingContext BindingContext
    {
        get => base.BindingContext;
        set
        {
            base.BindingContext = value;
            if (innerComboBox != null)
                innerComboBox.BindingContext = value;
        }
    }

    private void ComboBoxConSombra_Load(object sender, EventArgs e)
    {

    }

}