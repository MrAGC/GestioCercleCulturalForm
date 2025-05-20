// DateTimePickerConSombra.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestioCercleCultural.CustomControls
{
    public partial class DateTimePickerConSombra : UserControl
    {
        // Colores y configuración
        private readonly Color shadowColor = ColorTranslator.FromHtml("#84AEFF");
        private readonly Color backColorMain = Color.White;
        private readonly int shadowOffset = 5;
        private readonly int cornerRadius = 15;

        public DateTimePickerConSombra()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            InitializeComponent();    // carga innerDateTimePicker del diseñador
            PositionInnerControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Sombra
            var sombra = new Rectangle(shadowOffset, shadowOffset,
                                       Width - shadowOffset, Height - shadowOffset);
            using (var pathS = GetRoundedRectangle(sombra, cornerRadius))
            using (var brochaS = new SolidBrush(shadowColor))
                e.Graphics.FillPath(brochaS, pathS);

            // Fondo principal
            var fondo = new Rectangle(0, 0,
                                      Width - shadowOffset, Height - shadowOffset);
            using (var pathM = GetRoundedRectangle(fondo, cornerRadius))
            using (var brochaM = new SolidBrush(backColorMain))
                e.Graphics.FillPath(brochaM, pathM);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionInnerControl();
            Invalidate();  // repinta sombra y borde
        }

        private void PositionInnerControl()
        {
            const int margin = 8;
            innerDateTimePicker.Location = new Point(margin, margin);
            innerDateTimePicker.Size = new Size(
                Width - shadowOffset - margin * 2,
                Height - shadowOffset - margin * 2
            );
        }

        private GraphicsPath GetRoundedRectangle(Rectangle r, int radius)
        {
            var p = new GraphicsPath();
            int d = radius * 2;
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        // Propiedades expuestas
        [Browsable(true)]
        public DateTime Value
        {
            get => innerDateTimePicker.Value;
            set => innerDateTimePicker.Value = value;
        }

        [Browsable(true)]
        public DateTimePickerFormat Format
        {
            get => innerDateTimePicker.Format;
            set => innerDateTimePicker.Format = value;
        }

        [Browsable(true)]
        public string CustomFormat
        {
            get => innerDateTimePicker.CustomFormat;
            set => innerDateTimePicker.CustomFormat = value;
        }

        [Browsable(true)]
        public bool ShowUpDown
        {
            get => innerDateTimePicker.ShowUpDown;
            set => innerDateTimePicker.ShowUpDown = value;
        }

        // Evento
        public event EventHandler ValueChanged
        {
            add => innerDateTimePicker.ValueChanged += value;
            remove => innerDateTimePicker.ValueChanged -= value;
        }
    }
}
