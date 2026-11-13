
namespace MunicipalServicesApp
{
    partial class LocalEventsForm
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtEventSearch = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.chkFilterByDate = new System.Windows.Forms.CheckBox();
            this.dtpEventDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.lblUpcomingEvents = new System.Windows.Forms.Label();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.lblRecommendations = new System.Windows.Forms.Label();
            this.lstRecommendations = new System.Windows.Forms.ListBox();
            this.btnBack = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.SuspendLayout();

            // lblHeading
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font(
                "Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(0, 82, 155);
            this.lblHeading.Location = new System.Drawing.Point(190, 25);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(500, 46);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Local Events and Announcements";
            this.lblHeading.Click += new System.EventHandler(this.label1_Click);

            // lblSearch
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(45, 100);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(140, 20);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search by keyword";

            // txtEventSearch
            this.txtEventSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEventSearch.Location = new System.Drawing.Point(195, 96);
            this.txtEventSearch.Name = "txtEventSearch";
            this.txtEventSearch.Size = new System.Drawing.Size(420, 30);
            this.txtEventSearch.TabIndex = 2;

            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(45, 151);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(75, 20);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Category";

            // cmbCategory
            this.cmbCategory.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(195, 147);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(250, 31);
            this.cmbCategory.TabIndex = 4;

            // chkFilterByDate
            this.chkFilterByDate.AutoSize = true;
            this.chkFilterByDate.Location = new System.Drawing.Point(45, 203);
            this.chkFilterByDate.Name = "chkFilterByDate";
            this.chkFilterByDate.Size = new System.Drawing.Size(120, 20);
            this.chkFilterByDate.TabIndex = 5;
            this.chkFilterByDate.Text = "Filter by date";
            this.chkFilterByDate.UseVisualStyleBackColor = true;

            // dtpEventDate
            this.dtpEventDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEventDate.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEventDate.CustomFormat = "dd MMMM yyyy";
            this.dtpEventDate.Location = new System.Drawing.Point(195, 198);
            this.dtpEventDate.Name = "dtpEventDate";
            this.dtpEventDate.Size = new System.Drawing.Size(250, 30);
            this.dtpEventDate.TabIndex = 6;

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(195, 248);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 40);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search Events";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // btnClearSearch
            this.btnClearSearch.Location = new System.Drawing.Point(330, 248);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(120, 40);
            this.btnClearSearch.TabIndex = 8;
            this.btnClearSearch.Text = "Clear Search";
            this.btnClearSearch.UseVisualStyleBackColor = true;

            // lblUpcomingEvents
            this.lblUpcomingEvents.AutoSize = true;
            this.lblUpcomingEvents.Font = new System.Drawing.Font(
                "Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblUpcomingEvents.ForeColor =
                System.Drawing.Color.FromArgb(0, 82, 155);
            this.lblUpcomingEvents.Location = new System.Drawing.Point(45, 310);
            this.lblUpcomingEvents.Name = "lblUpcomingEvents";
            this.lblUpcomingEvents.Size = new System.Drawing.Size(190, 30);
            this.lblUpcomingEvents.TabIndex = 9;
            this.lblUpcomingEvents.Text = "Upcoming Events";

            // dgvEvents
            this.dgvEvents.AllowUserToAddRows = false;
            this.dgvEvents.AllowUserToDeleteRows = false;
            this.dgvEvents.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEvents.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.Location = new System.Drawing.Point(50, 350);
            this.dgvEvents.MultiSelect = false;
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.ReadOnly = true;
            this.dgvEvents.RowHeadersVisible = false;
            this.dgvEvents.RowTemplate.Height = 28;
            this.dgvEvents.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEvents.Size = new System.Drawing.Size(800, 220);
            this.dgvEvents.TabIndex = 10;

            // lblRecommendations
            this.lblRecommendations.AutoSize = true;
            this.lblRecommendations.Font = new System.Drawing.Font(
                "Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecommendations.ForeColor =
                System.Drawing.Color.FromArgb(0, 82, 155);
            this.lblRecommendations.Location = new System.Drawing.Point(45, 590);
            this.lblRecommendations.Name = "lblRecommendations";
            this.lblRecommendations.Size = new System.Drawing.Size(210, 28);
            this.lblRecommendations.TabIndex = 11;
            this.lblRecommendations.Text = "Recommended Events";

            // lstRecommendations
            this.lstRecommendations.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstRecommendations.FormattingEnabled = true;
            this.lstRecommendations.ItemHeight = 22;
            this.lstRecommendations.Location = new System.Drawing.Point(50, 625);
            this.lstRecommendations.Name = "lstRecommendations";
            this.lstRecommendations.Size = new System.Drawing.Size(550, 92);
            this.lstRecommendations.TabIndex = 12;

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(750, 675);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.TabIndex = 13;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // LocalEventsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.ClientSize = new System.Drawing.Size(900, 750);

            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lstRecommendations);
            this.Controls.Add(this.lblRecommendations);
            this.Controls.Add(this.dgvEvents);
            this.Controls.Add(this.lblUpcomingEvents);
            this.Controls.Add(this.btnClearSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpEventDate);
            this.Controls.Add(this.chkFilterByDate);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtEventSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblHeading);

            this.Name = "LocalEventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services - Local Events and Announcements";
            this.Load += new System.EventHandler(this.LocalEventsForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtEventSearch;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.CheckBox chkFilterByDate;
        private System.Windows.Forms.DateTimePicker dtpEventDate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Label lblUpcomingEvents;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Windows.Forms.Label lblRecommendations;
        private System.Windows.Forms.ListBox lstRecommendations;
        private System.Windows.Forms.Button btnBack;
    }
}
