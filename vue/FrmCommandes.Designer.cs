
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
            this.gpbPasserCommande = new System.Windows.Forms.GroupBox();
            this.dgvDocuments = new System.Windows.Forms.DataGridView();
            this.txbTitre = new System.Windows.Forms.TextBox();
            this.txbNumero = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSupprCommande = new System.Windows.Forms.Button();
            this.lblQte = new System.Windows.Forms.Label();
            this.lblmontant = new System.Windows.Forms.Label();
            this.nudQuantite = new System.Windows.Forms.NumericUpDown();
            this.txbMontant = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListeCommandes)).BeginInit();
            this.gpbConsulter.SuspendLayout();
            this.gpbPasserCommande.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantite)).BeginInit();
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
            // 
            // gpbConsulter
            // 
            this.gpbConsulter.Controls.Add(this.btnSupprCommande);
            this.gpbConsulter.Controls.Add(this.dgvListeCommandes);
            this.gpbConsulter.Location = new System.Drawing.Point(12, 44);
            this.gpbConsulter.Name = "gpbConsulter";
            this.gpbConsulter.Size = new System.Drawing.Size(1143, 275);
            this.gpbConsulter.TabIndex = 6;
            this.gpbConsulter.TabStop = false;
            this.gpbConsulter.Text = "Consulter les commandes";
            // 
            // gpbPasserCommande
            // 
            this.gpbPasserCommande.Controls.Add(this.txbMontant);
            this.gpbPasserCommande.Controls.Add(this.nudQuantite);
            this.gpbPasserCommande.Controls.Add(this.lblmontant);
            this.gpbPasserCommande.Controls.Add(this.lblQte);
            this.gpbPasserCommande.Controls.Add(this.label1);
            this.gpbPasserCommande.Controls.Add(this.txbTitre);
            this.gpbPasserCommande.Controls.Add(this.txbNumero);
            this.gpbPasserCommande.Controls.Add(this.label7);
            this.gpbPasserCommande.Controls.Add(this.dgvDocuments);
            this.gpbPasserCommande.Location = new System.Drawing.Point(25, 348);
            this.gpbPasserCommande.Name = "gpbPasserCommande";
            this.gpbPasserCommande.Size = new System.Drawing.Size(1143, 590);
            this.gpbPasserCommande.TabIndex = 7;
            this.gpbPasserCommande.TabStop = false;
            this.gpbPasserCommande.Text = "Passer une commande";
            // 
            // dgvDocuments
            // 
            this.dgvDocuments.AllowUserToAddRows = false;
            this.dgvDocuments.AllowUserToDeleteRows = false;
            this.dgvDocuments.AllowUserToResizeColumns = false;
            this.dgvDocuments.AllowUserToResizeRows = false;
            this.dgvDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocuments.Location = new System.Drawing.Point(7, 37);
            this.dgvDocuments.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDocuments.MultiSelect = false;
            this.dgvDocuments.Name = "dgvDocuments";
            this.dgvDocuments.ReadOnly = true;
            this.dgvDocuments.RowHeadersVisible = false;
            this.dgvDocuments.RowHeadersWidth = 51;
            this.dgvDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDocuments.Size = new System.Drawing.Size(1125, 187);
            this.dgvDocuments.TabIndex = 6;
            this.dgvDocuments.SelectionChanged += new System.EventHandler(this.dgvDocuments_SelectionChanged);
            // 
            // txbTitre
            // 
            this.txbTitre.Location = new System.Drawing.Point(207, 287);
            this.txbTitre.Margin = new System.Windows.Forms.Padding(4);
            this.txbTitre.Name = "txbTitre";
            this.txbTitre.ReadOnly = true;
            this.txbTitre.Size = new System.Drawing.Size(520, 22);
            this.txbTitre.TabIndex = 28;
            // 
            // txbNumero
            // 
            this.txbNumero.Location = new System.Drawing.Point(207, 257);
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
            this.label7.Location = new System.Drawing.Point(15, 257);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "Numéro de document :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 289);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 29;
            this.label1.Text = "Titre";
            // 
            // btnSupprCommande
            // 
            this.btnSupprCommande.Location = new System.Drawing.Point(13, 224);
            this.btnSupprCommande.Name = "btnSupprCommande";
            this.btnSupprCommande.Size = new System.Drawing.Size(123, 34);
            this.btnSupprCommande.TabIndex = 18;
            this.btnSupprCommande.Text = "Supprimer";
            this.btnSupprCommande.UseVisualStyleBackColor = true;
            // 
            // lblQte
            // 
            this.lblQte.AutoSize = true;
            this.lblQte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQte.Location = new System.Drawing.Point(15, 319);
            this.lblQte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQte.Name = "lblQte";
            this.lblQte.Size = new System.Drawing.Size(70, 17);
            this.lblQte.TabIndex = 31;
            this.lblQte.Text = "Quantite";
            // 
            // lblmontant
            // 
            this.lblmontant.AutoSize = true;
            this.lblmontant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmontant.Location = new System.Drawing.Point(15, 349);
            this.lblmontant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblmontant.Name = "lblmontant";
            this.lblmontant.Size = new System.Drawing.Size(66, 17);
            this.lblmontant.TabIndex = 33;
            this.lblmontant.Text = "Montant";
            // 
            // nudQuantite
            // 
            this.nudQuantite.Location = new System.Drawing.Point(207, 319);
            this.nudQuantite.Name = "nudQuantite";
            this.nudQuantite.Size = new System.Drawing.Size(120, 22);
            this.nudQuantite.TabIndex = 34;
            // 
            // txbMontant
            // 
            this.txbMontant.Location = new System.Drawing.Point(207, 349);
            this.txbMontant.Margin = new System.Windows.Forms.Padding(4);
            this.txbMontant.Name = "txbMontant";
            this.txbMontant.ReadOnly = true;
            this.txbMontant.Size = new System.Drawing.Size(132, 22);
            this.txbMontant.TabIndex = 35;
            this.txbMontant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbMontant_KeyPress);
            // 
            // FrmCommandes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 1053);
            this.Controls.Add(this.gpbPasserCommande);
            this.Controls.Add(this.gpbConsulter);
            this.Name = "FrmCommandes";
            this.Text = "FrmCommandes";
            this.Load += new System.EventHandler(this.FrmCommandes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListeCommandes)).EndInit();
            this.gpbConsulter.ResumeLayout(false);
            this.gpbPasserCommande.ResumeLayout(false);
            this.gpbPasserCommande.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantite)).EndInit();
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
    }
}