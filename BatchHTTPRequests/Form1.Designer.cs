
namespace BatchHTTPRequests
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_GET = new System.Windows.Forms.Button();
            this.btn_POST = new System.Windows.Forms.Button();
            this.btn_PUT = new System.Windows.Forms.Button();
            this.btn_DELETE = new System.Windows.Forms.Button();
            this.btn_PUT_template = new System.Windows.Forms.Button();
            this.btn_POST_template = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(14, 239);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(568, 199);
            this.richTextBox2.TabIndex = 21;
            this.richTextBox2.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "接口地址：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(83, 6);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(403, 21);
            this.textBox2.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(492, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "执行次数：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(563, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(588, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "验证Json";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(14, 33);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(568, 199);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.Text = "";
            // 
            // btn_GET
            // 
            this.btn_GET.Location = new System.Drawing.Point(588, 149);
            this.btn_GET.Name = "btn_GET";
            this.btn_GET.Size = new System.Drawing.Size(75, 23);
            this.btn_GET.TabIndex = 14;
            this.btn_GET.Text = "GET";
            this.btn_GET.UseVisualStyleBackColor = true;
            this.btn_GET.Click += new System.EventHandler(this.btn_GET_Click);
            // 
            // btn_POST
            // 
            this.btn_POST.Location = new System.Drawing.Point(588, 120);
            this.btn_POST.Name = "btn_POST";
            this.btn_POST.Size = new System.Drawing.Size(75, 23);
            this.btn_POST.TabIndex = 13;
            this.btn_POST.Text = "POST";
            this.btn_POST.UseVisualStyleBackColor = true;
            this.btn_POST.Click += new System.EventHandler(this.btn_POST_Click);
            // 
            // btn_PUT
            // 
            this.btn_PUT.Location = new System.Drawing.Point(588, 91);
            this.btn_PUT.Name = "btn_PUT";
            this.btn_PUT.Size = new System.Drawing.Size(75, 23);
            this.btn_PUT.TabIndex = 12;
            this.btn_PUT.Text = "PUT";
            this.btn_PUT.UseVisualStyleBackColor = true;
            this.btn_PUT.Click += new System.EventHandler(this.btn_PUT_Click);
            // 
            // btn_DELETE
            // 
            this.btn_DELETE.Location = new System.Drawing.Point(588, 62);
            this.btn_DELETE.Name = "btn_DELETE";
            this.btn_DELETE.Size = new System.Drawing.Size(75, 23);
            this.btn_DELETE.TabIndex = 11;
            this.btn_DELETE.Text = "DELETE";
            this.btn_DELETE.UseVisualStyleBackColor = true;
            this.btn_DELETE.Click += new System.EventHandler(this.btn_DELETE_Click);
            // 
            // btn_PUT_template
            // 
            this.btn_PUT_template.Location = new System.Drawing.Point(669, 91);
            this.btn_PUT_template.Name = "btn_PUT_template";
            this.btn_PUT_template.Size = new System.Drawing.Size(75, 23);
            this.btn_PUT_template.TabIndex = 22;
            this.btn_PUT_template.Text = "PUT样例";
            this.btn_PUT_template.UseVisualStyleBackColor = true;
            this.btn_PUT_template.Click += new System.EventHandler(this.btn_PUT_template_Click);
            // 
            // btn_POST_template
            // 
            this.btn_POST_template.Location = new System.Drawing.Point(669, 120);
            this.btn_POST_template.Name = "btn_POST_template";
            this.btn_POST_template.Size = new System.Drawing.Size(75, 23);
            this.btn_POST_template.TabIndex = 22;
            this.btn_POST_template.Text = "POST样例";
            this.btn_POST_template.UseVisualStyleBackColor = true;
            this.btn_POST_template.Click += new System.EventHandler(this.btn_POST_template_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(525, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 60);
            this.label3.TabIndex = 23;
            this.label3.Text = "PUT支持：\r\n{{time(d|h|m|s)(+|-)7:2020-03-29 20:00:00}}\r\n\r\nPOST支持：\r\n{{date(+|-)7:2020" +
    "-03-29}}";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_POST_template);
            this.Controls.Add(this.btn_PUT_template);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_GET);
            this.Controls.Add(this.btn_POST);
            this.Controls.Add(this.btn_PUT);
            this.Controls.Add(this.btn_DELETE);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_GET;
        private System.Windows.Forms.Button btn_POST;
        private System.Windows.Forms.Button btn_PUT;
        private System.Windows.Forms.Button btn_DELETE;
        private System.Windows.Forms.Button btn_PUT_template;
        private System.Windows.Forms.Button btn_POST_template;
        private System.Windows.Forms.Label label3;
    }
}

