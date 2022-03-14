
namespace Mediatek86.vue
{
    partial class FrmCommandes
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
            this.dgvListeCommandes = new System.Windows.Forms.DataGridView();
            this.gpbConsulter = new System.Windows.Forms.GroupBox();
            this.btnMaJ = new System.Windows.Forms.Button();
            this.lblCommande = new System.Windows.Forms.Label();
            this.lblEtat = new System.Windows.Forms.Label();
            this.cbxEtatCommande = new System.Windows.Forms.ComboBox();
            this.txbCommande = new System.Windows.Forms.TextBox();
            this.btnSupprCommande = new System.Windows.Forms.Button();
            this.gpbPasserCommande = new System.Windows.Forms.GroupBox();
            this.lblDateFin = new System.Windows.Forms.Label();
            this.dtpDateFin = new System.Windows.Forms.DateTimePicker();
            this.txbIdCommande = new System.Windows.Forms.TextBox();
            this.lblNoCommande = new System.Windows.Forms.Label();
            this.btnAjouterCommande = new System.Windows.Forms.Button();
            this.txbMontant = new System.Windows.Forms.TextBox();
            this.nudQuantite = new System.Windows.Forms.NumericUpDown();
            this.lblmontant = new System.Windows.Forms.Label();
            this.lblQte = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbTitre = new System.Windows.Forms.TextBox();
            this.txbNumero = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvDocuments = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListeCommandes)).BeginInit();
            this.gpbConsulter.SuspendLayout();
            this.gpbPasserCommande.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListeCommandes
            // 
            this.dgvListeCommandes.AllowUserToAddRows = false;
            this.dgvListeCommandes.AllowUserToDeleteRows = false;
            this.dgvListeCommandes.AllowUserToResizeColumns = false;
            this.dgvListeCommandes.AllowUserToResizeRows = false;
            this.dgvListeCommandes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListeCommandes.Location = new System.Drawing.Point(13, 25);
            this.dgvListeCommandes.Margin = new System.Windows.Forms.Padding(4);
            this.dgvListeCommandes.MultiSelect = false;
            this.dgvListeCommandes.Name = "dgvListeCommandes";
            this.dgvListeCommandes.ReadOnly = true;
            this.dgvListeCommandes.RowHeadersVisible = false;
            this.dgvListeCommandes.RowHeadersWidth = 51;
            this.dgvListeCommandes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListeCommandes.Size = new System.Drawing.Size(1125, 187);
            this.dgvListeCommandes.TabIndex = 5;
            this.dgvListeCommandes.SelectionChanged += new System.EventHandler(this.dgvListeCommandes_SelectionChanged);
            // 
            // gpbConsulter
            // 
            this.gpbConsulter.Controls.Add(this.btnMaJ);
            this.gpbConsulter.Controls.Add(this.lblCommande);
            this.gpbConsulter.Controls.Add(this.lblEtat);
            this.gpbConsulter.Controls.Add(this.cbxEtatCommande);
            this.gpbConsulter.Controls.Add(this.txbCommande);
            this.gpbConsulter.Controls.Add(this.btnSupprCommande);
            this.gpbConsulter.Controls.Add(this.dgvListeCommandes);
            this.gpbConsulter.Location = new System.Drawing.Point(12, 28);
            this.gpbConsulter.Name = "gpbConsulter";
            this.gpbConsulter.Size = new System.Drawing.Size(1153, 338);
            this.gpbConsulter.TabIndex = 6;
            this.gpbConsulter.TabStop = false;
            this.gpbConsulter.Text = "Consulter les commandes";
            // 
            // btnMaJ
            // 
            this.btnMaJ.Location = new System.Drawing.Point(22, 284);
            this.btnMaJ.Name = "btnMaJ";
            this.btnMaJ.Size = new System.Drawing.Size(123, 34);
            this.btnMaJ.TabIndex = 33;
            this.btnMaJ.Text = "Mettre à jour";
            this.btnMaJ.UseVisualStyleBackColor = true;
            this.btnMaJ.Click += new System.EventHandler(this.btnMaJ_Click);
            // 
            // lblCommande
            // 
            this.lblCommande.AutoSize = true;
            this.lblCommande.Location = new System.Drawing.Point(15, 237);
            this.lblCommande.Name = "lblCommande";
            this.lblCommande.Size = new System.Drawing.Size(87, 17);
            this.lblCommande.TabIndex = 32;
            this.lblCommande.Text = "Commande :";
            // 
            // lblEtat
            // 
            this.lblEtat.AutoSize = true;
            this.lblEtat.Location = new System.Drawing.Point(671, 239);
            this.lblEtat.Name = "lblEtat";
            this.lblEtat.Size = new System.Drawing.Size(41, 17);
            this.lblEtat.TabIndex = 31;
            this.lblEtat.Text = "Etat :";
            // 
            // cbxEtatCommande
            // 
            this.cbxEtatCommande.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEtatCommande.FormattingEnabled = true;
            this.cbxEtatCommande.Location = new System.Drawing.Point(735, 234);
            this.cbxEtatCommande.Name = "cbxEtatCommande";
            this.cbxEtatCommande.Size = new System.Drawing.Size(263, 24);
            this.cbxEtatCommande.TabIndex = 30;
            // 
            // txbCommande
            // 
            this.txbCommande.Location = new System.Drawing.Point(113, 236);
            this.txbCommande.Margin = new System.Windows.Forms.Padding(4);
            this.txbCommande.Name = "txbCommande";
            this.txbCommande.ReadOnly = true;
            this.txbCommande.Size = new System.Drawing.Size(520, 22);
            this.txbCommande.TabIndex = 29;
            // 
            // btnSupprCommande
            // 
            this.btnSupprCommande.Location = new System.Drawing.Point(169, 284);
            this.btnSupprCommande.Name = "btnSupprCommande";
            this.btnSupprCommande.Size = new System.Drawing.Size(123, 34);
            this.btnSupprCommande.TabIndex = 18;
            this.btnSupprCommande.Text = "Supprimer";
            this.btnSupprCommande.UseVisualStyleBackColor = true;
            this.btnSupprCommande.Click += new System.EventHandler(this.btnSupprCommande_Click);
            // 
            // gpbPasserCommande
            // 
            this.gpbPasserCommande.Controls.Add(this.lblDateFin);
            this.gpbPasserCommande.Controls.Add(this.dtpDateFin);
            this.gpbPasserCommande.Controls.Add(this.txbIdCommande);
            this.gpbPasserCommande.Controls.Add(this.lblNoCommande);
            this.gpbPasserCommande.Controls.Add(this.btnAjouterCommande);
            this.gpbPasserCommande.Controls.Add(this.txbMontant);
            this.gpbPasserCommande.Controls.Add(this.nudQuantite);
            this.gpbPasserCommande.Controls.Add(this.lblmontant);
            this.gpbPasserCommande.Controls.Add(this.lblQte);
            this.gpbPasserCommande.Controls.Add(this.label1);
            this.gpbPasserCommande.Controls.Add(this.txbTitre);
            this.gpbPasserCommande.Controls.Add(this.txbNumero);
            this.gpbPasserCommande.Controls.Add(this.label7);
            this.gpbPasserCommande.Controls.Add(this.dgvDocuments);
            this.gpbPasserCommande.Location = new System.Drawing.Point(12, 397);
            this.gpbPasserCommande.Name = "gpbPasserCommande";
            this.gpbPasserCommande.Size = new System.Drawing.Size(1156, 456);
            this.gpbPasserCommande.TabIndex = 7;
            this.gpbPasserCommande.TabStop = false;
            this.gpbPasserCommande.Text = "Passer une commande";
            // 
            // lblDateFin
            // 
            this.lblDateFin.AutoSize = true;
            this.lblDateFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFin.Location = new System.Drawing.Point(15, 320);
            this.lblDateFin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateFin.Name = "lblDateFin";
            this.lblDateFin.Size = new System.Drawing.Size(76, 17);
            this.lblDateFin.TabIndex = 40;
            this.lblDateFin.Text = "Expire le ";
            this.lblDateFin.Visible = false;
            // 
            // dtpDateFin
            // 
            this.dtpDateFin.Location = new System.Drawing.Point(207, 319);
            this.dtpDateFin.Name = "dtpDateFin";
            this.dtpDateFin.Size = new System.Drawing.Size(223, 22);
            this.dtpDateFin.TabIndex = 39;
            this.dtpDateFin.Visible = false;
            // 
            // txbIdCommande
            // 
            this.txbIdCommande.Location = new System.Drawing.Point(207, 253);
            this.txbIdCommande.Margin = new System.Windows.Forms.Padding(4);
            this.txbIdCommande.Name = "txbIdCommande";
            this.txbIdCommande.Size = new System.Drawing.Size(132, 22);
            this.txbIdCommande.TabIndex = 38;
            // 
            // lblNoCommande
            // 
            this.lblNoCommande.AutoSize = true;
            this.lblNoCommande.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoCommande.Location = new System.Drawing.Point(15, 251);
            this.lblNoCommande.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNoCommande.Name = "lblNoCommande";
            this.lblNoCommande.Size = new System.Drawing.Size(179, 17);
            this.lblNoCommande.TabIndex = 37;
            this.lblNoCommande.Text = "Numéro de commande :";
            // 
            // btnAjouterCommande
            // 
            this.btnAjouterCommande.Location = new System.Drawing.Point(22, 403);
            this.btnAjouterCommande.Name = "btnAjouterCommande";
            this.btnAjouterCommande.Size = new System.Drawing.Size(165, 34);
            this.btnAjouterCommande.TabIndex = 36;
            this.btnAjouterCommande.Text = "Passer commande";
            this.btnAjouterCommande.UseVisualStyleBackColor = true;
            this.btnAjouterCommande.Click += new System.EventHandler(this.btnAjouterCommande_Click);
            // 
            // txbMontant
            // 
            this.txbMontant.Location = new System.Drawing.Point(207, 349);
            this.txbMontant.Margin = new System.Windows.Forms.Padding(4);
            this.txbMontant.Name = "txbMontant";
            this.txbMontant.Size = new System.Drawing.Size(132, 22);
            this.txbMontant.TabIndex = 35;
            this.txbMontant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMontant_KeyPress);
            // 
            // nudQuantite
            // 
            this.nudQuantite.Location = new System.Drawing.Point(207, 319);
            this.nudQuantite.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantite.Name = "nudQuantite";
            this.nudQuantite.Size = new System.Drawing.Size(120, 22);
            this.nudQuantite.TabIndex = 34;
            this.nudQuantite.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblmontant
            // 
            this.lblmontant.AutoSize = true;
            this.lblmontant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmontant.Location = new System.Drawing.Point(15, 349);
            this.lblmontant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblmontant.Name = "lblmontant";
            this.lblmontant.Size = new System.Drawing.Size(76, 17);
            this.lblmontant.TabIndex = 33;
            this.lblmontant.Text = "Montant :";
            // 
            // lblQte
            // 
            this.lblQte.AutoSize = true;
            this.lblQte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQte.Location = new System.Drawing.Point(15, 319);
            this.lblQte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQte.Name = "lblQte";
            this.lblQte.Size = new System.Drawing.Size(80, 17);
            this.lblQte.TabIndex = 31;
            this.lblQte.Text = "Quantite :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(378, 288);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 29;
            this.label1.Text = "Titre :";
            // 
            // txbTitre
            // 
            this.txbTitre.Location = new System.Drawing.Point(433, 286);
            this.txbTitre.Margin = new System.Windows.Forms.Padding(4);
            this.txbTitre.Name = "txbTitre";
            this.txbTitre.ReadOnly = true;
            this.txbTitre.Size = new System.Drawing.Size(520, 22);
            this.txbTitre.TabIndex = 28;
            // 
            // txbNumero
            // 
            this.txbNumero.Location = new System.Drawing.Point(207, 286);
            this.txbNumero.Margin = new System.Windows.Forms.Padding(4);
            this.txbNumero.Name = "txbNumero";
            this.txbNumero.ReadOnly = true;
            this.txbNumero.Size = new System.Drawing.Size(132, 22);
            this.txbNumero.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 286);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "Numéro de document :";
            // 
            // dgvDocuments
            // 
            this.dgvDocuments.AllowUserToAddRows = false;
            this.dgvDocuments.AllowUserToDeleteRows = false;
            this.dgvDocuments.AllowUserToResizeColumns = false;
            this.dgvDocuments.AllowUserToResizeRows = false;
            this.dgvDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocuments.Location = new System.Drawing.Point(13, 37);
            this.dgvDocuments.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDocuments.MultiSelect = false;
            this.dgvDocuments.Name = "dgvDocuments";
            this.dgvDocuments.ReadOnly = true;
            this.dgvDocuments.RowHeadersVisible = false;
            this.dgvDocuments.RowHeadersWidth = 51;
            this.dgvDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDocuments.Size = new System.Drawing.Size(1119, 187);
            this.dgvDocuments.TabIndex = 6;
            this.dgvDocuments.SelectionChanged += new System.EventHandler(this.dgvDocuments_SelectionChanged);
            // 
            // FrmCommandes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 883);
            this.Controls.Add(this.gpbPasserCommande);
            this.Controls.Add(this.gpbConsulter);
            this.Name = "FrmCommandes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commandes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvListeCommandes)).EndInit();
            this.gpbConsulter.ResumeLayout(false);
            this.gpbConsulter.PerformLayout();
            this.gpbPasserCommande.ResumeLayout(false);
            this.gpbPasserCommande.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListeCommandes;
        private System.Windows.Forms.GroupBox gpbConsulter;
        private System.Windows.Forms.GroupBox gpbPasserCommande;
        private System.Windows.Forms.DataGridView dgvDocuments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbTitre;
        private System.Windows.Forms.TextBox txbNumero;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSupprCommande;
        private System.Windows.Forms.NumericUpDown nudQuantite;
        private System.Windows.Forms.Label lblmontant;
        private System.Windows.Forms.Label lblQte;
        private System.Windows.Forms.TextBox txbMontant;
        private System.Windows.Forms.TextBox txbCommande;
        private System.Windows.Forms.ComboBox cbxEtatCommande;
        private System.Windows.Forms.Label lblCommande;
        private System.Windows.Forms.Label lblEtat;
        private System.Windows.Forms.Button btnMaJ;
        private System.Windows.Forms.Button btnAjouterCommande;
        private System.Windows.Forms.TextBox txbIdCommande;
        private System.Windows.Forms.Label lblNoCommande;
        private System.Windows.Forms.Label lblDateFin;
        private System.Windows.Forms.DateTimePicker dtpDateFin;
    }
}