using System.Windows.Forms;


namespace WinForms
{
    partial class Form1
    {
        
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            cboStatus = new ComboBox();
            cboDep = new ComboBox();
            cboPost = new ComboBox();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            chkUseFrom = new CheckBox();
            chkUseTo = new CheckBox();
            btnSearch = new Button();
            gridPersons = new DataGridView();
            txtLastName = new TextBox();
            lblStatus = new Label();
            lblDep = new Label();
            lblPost = new Label();
            lblFrom = new Label();
            lblTo = new Label();
            lblLastName = new Label();
            ((System.ComponentModel.ISupportInitialize)gridPersons).BeginInit();
            SuspendLayout();
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.FormattingEnabled = true;
            cboStatus.Location = new Point(14, 37);
            cboStatus.Margin = new Padding(3, 4, 3, 4);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(228, 28);
            cboStatus.TabIndex = 0;
            // 
            // cboDep
            // 
            cboDep.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDep.FormattingEnabled = true;
            cboDep.Location = new Point(249, 37);
            cboDep.Margin = new Padding(3, 4, 3, 4);
            cboDep.Name = "cboDep";
            cboDep.Size = new Size(228, 28);
            cboDep.TabIndex = 1;
            // 
            // cboPost
            // 
            cboPost.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPost.FormattingEnabled = true;
            cboPost.Location = new Point(485, 37);
            cboPost.Margin = new Padding(3, 4, 3, 4);
            cboPost.Name = "cboPost";
            cboPost.Size = new Size(228, 28);
            cboPost.TabIndex = 2;
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(14, 115);
            dtpFrom.Margin = new Padding(3, 4, 3, 4);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(228, 27);
            dtpFrom.TabIndex = 3;
            // 
            // dtpTo
            // 
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(249, 115);
            dtpTo.Margin = new Padding(3, 4, 3, 4);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(228, 27);
            dtpTo.TabIndex = 4;
            // 
            // chkUseFrom
            // 
            chkUseFrom.AutoSize = true;
            chkUseFrom.Location = new Point(14, 87);
            chkUseFrom.Margin = new Padding(3, 4, 3, 4);
            chkUseFrom.Name = "chkUseFrom";
            chkUseFrom.Size = new Size(135, 24);
            chkUseFrom.TabIndex = 5;
            chkUseFrom.Text = "Дата приема c:";
            chkUseFrom.UseVisualStyleBackColor = true;
            // 
            // chkUseTo
            // 
            chkUseTo.AutoSize = true;
            chkUseTo.Location = new Point(249, 87);
            chkUseTo.Margin = new Padding(3, 4, 3, 4);
            chkUseTo.Name = "chkUseTo";
            chkUseTo.Size = new Size(146, 24);
            chkUseTo.TabIndex = 6;
            chkUseTo.Text = "Дата приема по:";
            chkUseTo.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(720, 35);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(137, 36);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Найти";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // gridPersons
            // 
            gridPersons.AllowUserToAddRows = false;
            gridPersons.AllowUserToDeleteRows = false;
            gridPersons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridPersons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridPersons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPersons.Location = new Point(14, 197);
            gridPersons.Margin = new Padding(3, 4, 3, 4);
            gridPersons.MultiSelect = false;
            gridPersons.Name = "gridPersons";
            gridPersons.ReadOnly = true;
            gridPersons.RowHeadersVisible = false;
            gridPersons.RowHeadersWidth = 51;
            gridPersons.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPersons.Size = new Size(843, 387);
            gridPersons.TabIndex = 8;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(485, 115);
            txtLastName.Margin = new Padding(3, 4, 3, 4);
            txtLastName.Name = "txtLastName";
            txtLastName.PlaceholderText = "Фамилия (часть)";
            txtLastName.Size = new Size(228, 27);
            txtLastName.TabIndex = 9;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(14, 13);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(52, 20);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "Статус";
            // 
            // lblDep
            // 
            lblDep.AutoSize = true;
            lblDep.Location = new Point(249, 13);
            lblDep.Name = "lblDep";
            lblDep.Size = new Size(50, 20);
            lblDep.TabIndex = 11;
            lblDep.Text = "Отдел";
            // 
            // lblPost
            // 
            lblPost.AutoSize = true;
            lblPost.Location = new Point(485, 13);
            lblPost.Name = "lblPost";
            lblPost.Size = new Size(86, 20);
            lblPost.TabIndex = 12;
            lblPost.Text = "Должность";
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(14, 160);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(107, 20);
            lblFrom.TabIndex = 13;
            lblFrom.Text = "";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(249, 160);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(13, 20);
            lblTo.TabIndex = 14;
            lblTo.Text = "";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(485, 91);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(73, 20);
            lblLastName.TabIndex = 15;
            lblLastName.Text = "Фамилия";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 600);
            Controls.Add(lblLastName);
            Controls.Add(lblTo);
            Controls.Add(lblFrom);
            Controls.Add(lblPost);
            Controls.Add(lblDep);
            Controls.Add(lblStatus);
            Controls.Add(txtLastName);
            Controls.Add(gridPersons);
            Controls.Add(btnSearch);
            Controls.Add(chkUseTo);
            Controls.Add(chkUseFrom);
            Controls.Add(dtpTo);
            Controls.Add(dtpFrom);
            Controls.Add(cboPost);
            Controls.Add(cboDep);
            Controls.Add(cboStatus);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Persons";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)gridPersons).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cboStatus;
        private ComboBox cboDep;
        private ComboBox cboPost;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private CheckBox chkUseFrom;
        private CheckBox chkUseTo;
        private Button btnSearch;
        private DataGridView gridPersons;
        private TextBox txtLastName;
        private Label lblStatus;
        private Label lblDep;
        private Label lblPost;
        private Label lblFrom;
        private Label lblTo;
        private Label lblLastName;
    }
}
