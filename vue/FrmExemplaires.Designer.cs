
namespace Mediatek86.vue
{
    partial class FrmExemplaires
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
            this.dgvExemplaires = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSupprExemplaire = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txbTitreDocument = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbNumExemplaire = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbIdDocument = new System.Windows.Forms.TextBox();
            this.btnMaJExemplaire = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxEtat = new System.Windows.Forms.ComboBox();
            this.lblDocument = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExemplaires)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvExemplaires
            // 
            this.dgvExemplaires.AllowUserToAddRows = false;
            this.dgvExemplaires.AllowUserToDeleteRows = false;
            this.dgvExemplaires.AllowUserToResizeColumns = false;
            this.dgvExemplaires.AllowUserToResizeRows = false;
            this.dgvExemplaires.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExemplaires.Location = new System.Drawing.Point(13, 55);
            this.dgvExemplaires.Margin = new System.Windows.Forms.Padding(4);
            this.dgvExemplaires.MultiSelect = false;
            this.dgvExemplaires.Name = "dgvExemplaires";
            this.dgvExemplaires.ReadOnly = true;
            this.dgvExemplaires.RowHeadersVisible = false;
            this.dgvExemplaires.RowHeadersWidth = 51;
            this.dgvExemplaires.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExemplaires.Size = new System.Drawing.Size(627, 246);
            this.dgvExemplaires.TabIndex = 5;
            this.dgvExemplaires.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvExemplaires_ColumnHeaderMouseClick);
            this.dgvExemplaires.SelectionChanged += new System.EventHandler(this.dgvExemplaires_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSupprExemplaire);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txbTitreDocument);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txbNumExemplaire);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txbIdDocument);
            this.groupBox1.Controls.Add(this.btnMaJExemplaire);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxEtat);
            this.groupBox1.Location = new System.Drawing.Point(13, 311);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 196);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Détail de l\'exemplaire";
            // 
            // btnSupprExemplaire
            // 
            this.btnSupprExemplaire.Location = new System.Drawing.Point(131, 152);
            this.btnSupprExemplaire.Name = "btnSupprExemplaire";
            this.btnSupprExemplaire.Size = new System.Drawing.Size(105, 33);
            this.btnSupprExemplaire.TabIndex = 9;
            this.btnSupprExemplaire.Text = "Supprimer";
            this.btnSupprExemplaire.UseVisualStyleBackColor = true;
            this.btnSupprExemplaire.Click += new System.EventHandler(this.btnSupprExemplaire_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Titre du document :";
            // 
            // txbTitreDocument
            // 
            this.txbTitreDocument.Location = new System.Drawing.Point(179, 66);
            this.txbTitreDocument.Name = "txbTitreDocument";
            this.txbTitreDocument.ReadOnly = true;
            this.txbTitreDocument.Size = new System.Drawing.Size(422, 22);
            this.txbTitreDocument.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(332, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Numéro de l\'exemplaire :";
            // 
            // txbNumExemplaire
            // 
            this.txbNumExemplaire.Location = new System.Drawing.Point(501, 34);
            this.txbNumExemplaire.Name = "txbNumExemplaire";
            this.txbNumExemplaire.ReadOnly = true;
            this.txbNumExemplaire.Size = new System.Drawing.Size(100, 22);
            this.txbNumExemplaire.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Numéro du document :";
            // 
            // txbIdDocument
            // 
            this.txbIdDocument.Location = new System.Drawing.Point(179, 34);
            this.txbIdDocument.Name = "txbIdDocument";
            this.txbIdDocument.ReadOnly = true;
            this.txbIdDocument.Size = new System.Drawing.Size(100, 22);
            this.txbIdDocument.TabIndex = 3;
            // 
            // btnMaJExemplaire
            // 
            this.btnMaJExemplaire.Location = new System.Drawing.Point(6, 152);
            this.btnMaJExemplaire.Name = "btnMaJExemplaire";
            this.btnMaJExemplaire.Size = new System.Drawing.Size(105, 33);
            this.btnMaJExemplaire.TabIndex = 2;
            this.btnMaJExemplaire.Text = "Mettre à jour";
            this.btnMaJExemplaire.UseVisualStyleBackColor = true;
            this.btnMaJExemplaire.Click += new System.EventHandler(this.btnMaJExemplaire_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Etat du document :";
            // 
            // cbxEtat
            // 
            this.cbxEtat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEtat.FormattingEnabled = true;
            this.cbxEtat.Location = new System.Drawing.Point(179, 105);
            this.cbxEtat.Name = "cbxEtat";
            this.cbxEtat.Size = new System.Drawing.Size(198, 24);
            this.cbxEtat.TabIndex = 0;
            // 
            // lblDocument
            // 
            this.lblDocument.AutoSize = true;
            this.lblDocument.Location = new System.Drawing.Point(16, 21);
            this.lblDocument.Name = "lblDocument";
            this.lblDocument.Size = new System.Drawing.Size(0, 17);
            this.lblDocument.TabIndex = 7;
            // 
            // FrmExemplaires
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 542);
            this.Controls.Add(this.lblDocument);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvExemplaires);
            this.Name = "FrmExemplaires";
            this.Text = "Exemplaires";
            ((System.ComponentModel.ISupportInitialize)(this.dgvExemplaires)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExemplaires;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbNumExemplaire;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbIdDocument;
        private System.Windows.Forms.Button btnMaJExemplaire;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxEtat;
        private System.Windows.Forms.Button btnSupprExemplaire;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbTitreDocument;
        private System.Windows.Forms.Label lblDocument;
    }
}