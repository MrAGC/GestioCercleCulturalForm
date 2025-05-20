namespace GestioCercleCultural
{
    partial class FormReservaUsuari
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.roundedButtonReservar = new RoundedButton();
            this.comboBoxSeleccionarUsuari = new ComboBoxConSombra();
            this.label1 = new System.Windows.Forms.Label();
            this.labelEntrades = new System.Windows.Forms.Label();
            this.comboBoxConSombraEtrades = new ComboBoxConSombra();
            this.SuspendLayout();
            // 
            // roundedButtonReservar
            // 
            this.roundedButtonReservar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(54)))));
            this.roundedButtonReservar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButtonReservar.FlatAppearance.BorderSize = 0;
            this.roundedButtonReservar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButtonReservar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.roundedButtonReservar.ForeColor = System.Drawing.Color.Black;
            this.roundedButtonReservar.Location = new System.Drawing.Point(325, 398);
            this.roundedButtonReservar.Name = "roundedButtonReservar";
            this.roundedButtonReservar.Size = new System.Drawing.Size(120, 50);
            this.roundedButtonReservar.TabIndex = 0;
            this.roundedButtonReservar.Text = "Reservar";
            this.roundedButtonReservar.UseVisualStyleBackColor = false;
            this.roundedButtonReservar.Click += new System.EventHandler(this.roundedButtonReservar_Click);
            // 
            // comboBoxSeleccionarUsuari
            // 
            this.comboBoxSeleccionarUsuari.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.comboBoxSeleccionarUsuari.DataSource = null;
            this.comboBoxSeleccionarUsuari.DisplayMember = "";
            this.comboBoxSeleccionarUsuari.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeleccionarUsuari.Location = new System.Drawing.Point(255, 175);
            this.comboBoxSeleccionarUsuari.Name = "comboBoxSeleccionarUsuari";
            this.comboBoxSeleccionarUsuari.SelectedIndex = -1;
            this.comboBoxSeleccionarUsuari.SelectedItem = null;
            this.comboBoxSeleccionarUsuari.SelectedValue = null;
            this.comboBoxSeleccionarUsuari.Size = new System.Drawing.Size(263, 45);
            this.comboBoxSeleccionarUsuari.TabIndex = 1;
            this.comboBoxSeleccionarUsuari.ValueMember = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(251, 148);
            this.label1.MaximumSize = new System.Drawing.Size(650, 450);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seleccioneu usuari";
            // 
            // labelEntrades
            // 
            this.labelEntrades.AutoSize = true;
            this.labelEntrades.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEntrades.Location = new System.Drawing.Point(251, 266);
            this.labelEntrades.MaximumSize = new System.Drawing.Size(650, 450);
            this.labelEntrades.Name = "labelEntrades";
            this.labelEntrades.Size = new System.Drawing.Size(206, 24);
            this.labelEntrades.TabIndex = 5;
            this.labelEntrades.Text = "Entrades disponibles: 0";
            // 
            // comboBoxConSombraEtrades
            // 
            this.comboBoxConSombraEtrades.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.comboBoxConSombraEtrades.DataSource = null;
            this.comboBoxConSombraEtrades.DisplayMember = "";
            this.comboBoxConSombraEtrades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConSombraEtrades.Location = new System.Drawing.Point(255, 293);
            this.comboBoxConSombraEtrades.Name = "comboBoxConSombraEtrades";
            this.comboBoxConSombraEtrades.SelectedIndex = -1;
            this.comboBoxConSombraEtrades.SelectedItem = null;
            this.comboBoxConSombraEtrades.SelectedValue = null;
            this.comboBoxConSombraEtrades.Size = new System.Drawing.Size(263, 45);
            this.comboBoxConSombraEtrades.TabIndex = 6;
            this.comboBoxConSombraEtrades.ValueMember = "";
            // 
            // FormReservaUsuari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(799, 530);
            this.Controls.Add(this.comboBoxConSombraEtrades);
            this.Controls.Add(this.labelEntrades);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSeleccionarUsuari);
            this.Controls.Add(this.roundedButtonReservar);
            this.Name = "FormReservaUsuari";
            this.Text = "FormReservaUsuari";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundedButton roundedButtonReservar;
        private ComboBoxConSombra comboBoxSeleccionarUsuari;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelEntrades;
        private ComboBoxConSombra comboBoxConSombraEtrades;
    }
}