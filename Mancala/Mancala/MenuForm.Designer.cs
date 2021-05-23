
namespace Mancala
{
    partial class MenuForm
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
            this.newGameBtn = new System.Windows.Forms.Button();
            this.aboutBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.newGameAIBtn = new System.Windows.Forms.Button();
            this.newGamePvPBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newGameBtn
            // 
            this.newGameBtn.Location = new System.Drawing.Point(324, 311);
            this.newGameBtn.Name = "newGameBtn";
            this.newGameBtn.Size = new System.Drawing.Size(136, 42);
            this.newGameBtn.TabIndex = 0;
            this.newGameBtn.Text = "New Game";
            this.newGameBtn.UseVisualStyleBackColor = true;
            this.newGameBtn.Click += new System.EventHandler(this.newGameBtn_Click);
            // 
            // aboutBtn
            // 
            this.aboutBtn.Location = new System.Drawing.Point(12, 311);
            this.aboutBtn.Name = "aboutBtn";
            this.aboutBtn.Size = new System.Drawing.Size(136, 42);
            this.aboutBtn.TabIndex = 1;
            this.aboutBtn.Text = "About";
            this.aboutBtn.UseVisualStyleBackColor = true;
            this.aboutBtn.Click += new System.EventHandler(this.aboutBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(631, 311);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(136, 42);
            this.exitBtn.TabIndex = 2;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // newGameAIBtn
            // 
            this.newGameAIBtn.Location = new System.Drawing.Point(148, 170);
            this.newGameAIBtn.Name = "newGameAIBtn";
            this.newGameAIBtn.Size = new System.Drawing.Size(181, 135);
            this.newGameAIBtn.TabIndex = 3;
            this.newGameAIBtn.Text = "Play vs AI Bot";
            this.newGameAIBtn.UseVisualStyleBackColor = true;
            this.newGameAIBtn.Visible = false;
            this.newGameAIBtn.Click += new System.EventHandler(this.newGameAIBtn_Click);
            // 
            // newGamePvPBtn
            // 
            this.newGamePvPBtn.Location = new System.Drawing.Point(458, 170);
            this.newGamePvPBtn.Name = "newGamePvPBtn";
            this.newGamePvPBtn.Size = new System.Drawing.Size(181, 135);
            this.newGamePvPBtn.TabIndex = 4;
            this.newGamePvPBtn.Text = "Play 1vs1";
            this.newGamePvPBtn.UseVisualStyleBackColor = true;
            this.newGamePvPBtn.Visible = false;
            this.newGamePvPBtn.Click += new System.EventHandler(this.newGamePvPBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 365);
            this.Controls.Add(this.newGamePvPBtn);
            this.Controls.Add(this.newGameAIBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.aboutBtn);
            this.Controls.Add(this.newGameBtn);
            this.Name = "Form1";
            this.Text = "Mancala Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newGameBtn;
        private System.Windows.Forms.Button aboutBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button newGameAIBtn;
        private System.Windows.Forms.Button newGamePvPBtn;
    }
}

