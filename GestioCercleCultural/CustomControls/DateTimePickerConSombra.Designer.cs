// DateTimePickerConSombra.Designer.cs
namespace GestioCercleCultural.CustomControls
{
    partial class DateTimePickerConSombra
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DateTimePicker innerDateTimePicker;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.innerDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // innerDateTimePicker
            // 
            this.innerDateTimePicker.Location = new System.Drawing.Point(8, 8);
            this.innerDateTimePicker.Name = "innerDateTimePicker";
            this.innerDateTimePicker.Size = new System.Drawing.Size(184, 20);
            this.innerDateTimePicker.TabIndex = 0;
            // 
            // DateTimePickerConSombra
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.innerDateTimePicker);
            this.Name = "DateTimePickerConSombra";
            this.Size = new System.Drawing.Size(300, 45);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
