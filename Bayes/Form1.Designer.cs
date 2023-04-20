namespace Bayes
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listView1 = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.graph1 = new Graph();
			this.panel1 = new Panel();
			this.panel3 = new Panel();
			this.panel2 = new Panel();
			this.button1 = new Button();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
			this.listView1.Dock = DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new Size(200, 450);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = View.Details;
			this.listView1.SelectedIndexChanged += this.listView1_SelectedIndexChanged;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "X";
			this.columnHeader1.Width = 30;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Y";
			this.columnHeader2.Width = 30;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Classification";
			this.columnHeader3.Width = 120;
			// 
			// graph1
			// 
			this.graph1.Dock = DockStyle.Fill;
			this.graph1.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point);
			this.graph1.Form1 = null;
			this.graph1.Location = new Point(0, 0);
			this.graph1.Name = "graph1";
			this.graph1.Padding = new Padding(0, 40, 0, 0);
			this.graph1.Size = new Size(600, 450);
			this.graph1.TabIndex = 1;
			this.graph1.Text = "graph1";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(800, 450);
			this.panel1.TabIndex = 2;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.graph1);
			this.panel3.Dock = DockStyle.Fill;
			this.panel3.Location = new Point(200, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(600, 450);
			this.panel3.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.button1);
			this.panel2.Controls.Add(this.listView1);
			this.panel2.Dock = DockStyle.Left;
			this.panel2.Location = new Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(200, 450);
			this.panel2.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Dock = DockStyle.Bottom;
			this.button1.Location = new Point(0, 427);
			this.button1.Name = "button1";
			this.button1.Size = new Size(200, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += this.button1_Click;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(800, 450);
			this.Controls.Add(this.panel1);
			this.KeyPreview = true;
			this.Name = "Form1";
			this.Text = "2";
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private ListView listView1;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private Graph graph1;
		private Panel panel1;
		private Panel panel3;
		private Panel panel2;
		private Button button1;
	}
}
