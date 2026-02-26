namespace AOT
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainFolderDirTextBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ActualFileDirTextBox = new System.Windows.Forms.TextBox();
            this.FileSystemTreeView = new System.Windows.Forms.TreeView();
            this.RichTextBoxContent = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FileSystemTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.MainFolderDirTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.RichTextBoxContent);
            this.splitContainer1.Panel2.Controls.Add(this.ActualFileDirTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.textBox3);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 0;
            // 
            // MainFolderDirTextBox
            // 
            this.MainFolderDirTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainFolderDirTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainFolderDirTextBox.Location = new System.Drawing.Point(0, 0);
            this.MainFolderDirTextBox.Name = "MainFolderDirTextBox";
            this.MainFolderDirTextBox.Size = new System.Drawing.Size(266, 24);
            this.MainFolderDirTextBox.TabIndex = 0;
            this.MainFolderDirTextBox.Text = "C:\\Users\\modt\\Desktop\\WindowsSSH\\Docs";
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox2.Font = new System.Drawing.Font("Lucida Console", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(0, 430);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(266, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "/help";
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox3.Font = new System.Drawing.Font("Lucida Console", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(0, 430);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(530, 20);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = "/help";
            // 
            // ActualFileDirTextBox
            // 
            this.ActualFileDirTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ActualFileDirTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActualFileDirTextBox.Location = new System.Drawing.Point(0, 0);
            this.ActualFileDirTextBox.Name = "ActualFileDirTextBox";
            this.ActualFileDirTextBox.Size = new System.Drawing.Size(530, 24);
            this.ActualFileDirTextBox.TabIndex = 3;
            // 
            // FileSystemTreeView
            // 
            this.FileSystemTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileSystemTreeView.Location = new System.Drawing.Point(0, 24);
            this.FileSystemTreeView.Name = "FileSystemTreeView";
            this.FileSystemTreeView.Size = new System.Drawing.Size(266, 406);
            this.FileSystemTreeView.TabIndex = 2;
            // 
            // RichTextBoxContent
            // 
            this.RichTextBoxContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBoxContent.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextBoxContent.Location = new System.Drawing.Point(0, 24);
            this.RichTextBoxContent.Name = "RichTextBoxContent";
            this.RichTextBoxContent.Size = new System.Drawing.Size(530, 406);
            this.RichTextBoxContent.TabIndex = 4;
            this.RichTextBoxContent.Text = resources.GetString("RichTextBoxContent.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView FileSystemTreeView;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox MainFolderDirTextBox;
        private System.Windows.Forms.RichTextBox RichTextBoxContent;
        private System.Windows.Forms.TextBox ActualFileDirTextBox;
        private System.Windows.Forms.TextBox textBox3;
    }
}

