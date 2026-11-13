namespace MunicipalServicesApp
{
    partial class ServiceRequestStatusForm
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblRequestId = new System.Windows.Forms.Label();
            this.txtRequestId = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblLocationTitle = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblDescriptionTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDateTitle = new System.Windows.Forms.Label();
            this.lblDateSubmitted = new System.Windows.Forms.Label();
            this.lblFilterStatus = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.dgvServiceRequests = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHighestPriority = new System.Windows.Forms.Button();
            this.btnBFS = new System.Windows.Forms.Button();
            this.btnDFS = new System.Windows.Forms.Button();
            this.btnMST = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Location = new System.Drawing.Point(22, 54);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(147, 16);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Service Request Status";
            // 
            // lblRequestId
            // 
            this.lblRequestId.AutoSize = true;
            this.lblRequestId.Location = new System.Drawing.Point(22, 132);
            this.lblRequestId.Name = "lblRequestId";
            this.lblRequestId.Size = new System.Drawing.Size(77, 16);
            this.lblRequestId.TabIndex = 1;
            this.lblRequestId.Text = "Request ID:";
            // 
            // txtRequestId
            // 
            this.txtRequestId.Location = new System.Drawing.Point(113, 129);
            this.txtRequestId.Name = "txtRequestId";
            this.txtRequestId.Size = new System.Drawing.Size(100, 22);
            this.txtRequestId.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(225, 128);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search Request";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Location = new System.Drawing.Point(26, 174);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(65, 16);
            this.lblCategoryTitle.TabIndex = 4;
            this.lblCategoryTitle.Text = "Category:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(103, 174);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(11, 16);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "-";
            // 
            // lblLocationTitle
            // 
            this.lblLocationTitle.AutoSize = true;
            this.lblLocationTitle.Location = new System.Drawing.Point(26, 200);
            this.lblLocationTitle.Name = "lblLocationTitle";
            this.lblLocationTitle.Size = new System.Drawing.Size(61, 16);
            this.lblLocationTitle.TabIndex = 6;
            this.lblLocationTitle.Text = "Location:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(103, 200);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(11, 16);
            this.lblLocation.TabIndex = 7;
            this.lblLocation.Text = "-";
            // 
            // lblDescriptionTitle
            // 
            this.lblDescriptionTitle.AutoSize = true;
            this.lblDescriptionTitle.Location = new System.Drawing.Point(26, 230);
            this.lblDescriptionTitle.Name = "lblDescriptionTitle";
            this.lblDescriptionTitle.Size = new System.Drawing.Size(78, 16);
            this.lblDescriptionTitle.TabIndex = 8;
            this.lblDescriptionTitle.Text = "Description:";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(110, 230);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(284, 60);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "-";
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Location = new System.Drawing.Point(26, 305);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(47, 16);
            this.lblStatusTitle.TabIndex = 10;
            this.lblStatusTitle.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(93, 305);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(11, 16);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "-";
            // 
            // lblDateTitle
            // 
            this.lblDateTitle.AutoSize = true;
            this.lblDateTitle.Location = new System.Drawing.Point(26, 334);
            this.lblDateTitle.Name = "lblDateTitle";
            this.lblDateTitle.Size = new System.Drawing.Size(102, 16);
            this.lblDateTitle.TabIndex = 12;
            this.lblDateTitle.Text = "Date Submitted:";
            // 
            // lblDateSubmitted
            // 
            this.lblDateSubmitted.AutoSize = true;
            this.lblDateSubmitted.Location = new System.Drawing.Point(143, 334);
            this.lblDateSubmitted.Name = "lblDateSubmitted";
            this.lblDateSubmitted.Size = new System.Drawing.Size(11, 16);
            this.lblDateSubmitted.TabIndex = 13;
            this.lblDateSubmitted.Text = "-";
            // 
            // lblFilterStatus
            // 
            this.lblFilterStatus.AutoSize = true;
            this.lblFilterStatus.Location = new System.Drawing.Point(594, 127);
            this.lblFilterStatus.Name = "lblFilterStatus";
            this.lblFilterStatus.Size = new System.Drawing.Size(97, 16);
            this.lblFilterStatus.TabIndex = 14;
            this.lblFilterStatus.Text = "Filter by Status:";
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Location = new System.Drawing.Point(746, 124);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(121, 24);
            this.cmbStatusFilter.TabIndex = 15;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(765, 167);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 16;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(606, 167);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(75, 23);
            this.btnShowAll.TabIndex = 17;
            this.btnShowAll.Text = "Show All Requests";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // dgvServiceRequests
            // 
            this.dgvServiceRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiceRequests.Location = new System.Drawing.Point(411, 216);
            this.dgvServiceRequests.Name = "dgvServiceRequests";
            this.dgvServiceRequests.RowHeadersWidth = 51;
            this.dgvServiceRequests.RowTemplate.Height = 24;
            this.dgvServiceRequests.Size = new System.Drawing.Size(844, 164);
            this.dgvServiceRequests.TabIndex = 18;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(606, 407);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHighestPriority
            // 
            this.btnHighestPriority.Location = new System.Drawing.Point(765, 395);
            this.btnHighestPriority.Name = "btnHighestPriority";
            this.btnHighestPriority.Size = new System.Drawing.Size(75, 23);
            this.btnHighestPriority.TabIndex = 20;
            this.btnHighestPriority.Text = "Highest Priority";
            this.btnHighestPriority.UseVisualStyleBackColor = true;
            this.btnHighestPriority.Click += new System.EventHandler(this.btnHighestPriority_Click);
            // 
            // btnBFS
            // 
            this.btnBFS.Location = new System.Drawing.Point(865, 395);
            this.btnBFS.Name = "btnBFS";
            this.btnBFS.Size = new System.Drawing.Size(75, 23);
            this.btnBFS.TabIndex = 21;
            this.btnBFS.Text = "BFS Related Requests";
            this.btnBFS.UseVisualStyleBackColor = true;
            // 
            // btnDFS
            // 
            this.btnDFS.Location = new System.Drawing.Point(967, 395);
            this.btnDFS.Name = "btnDFS";
            this.btnDFS.Size = new System.Drawing.Size(75, 23);
            this.btnDFS.TabIndex = 22;
            this.btnDFS.Text = "DFS Related Requests";
            this.btnDFS.UseVisualStyleBackColor = true;
            this.btnDFS.Click += new System.EventHandler(this.btnDFS_Click);
            // 
            // btnMST
            // 
            this.btnMST.Location = new System.Drawing.Point(1066, 395);
            this.btnMST.Name = "btnMST";
            this.btnMST.Size = new System.Drawing.Size(75, 23);
            this.btnMST.TabIndex = 23;
            this.btnMST.Text = "Show MST";
            this.btnMST.UseVisualStyleBackColor = true;
            this.btnMST.Click += new System.EventHandler(this.btnMST_Click);
            // 
            // ServiceRequestStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 453);
            this.Controls.Add(this.btnMST);
            this.Controls.Add(this.btnDFS);
            this.Controls.Add(this.btnBFS);
            this.Controls.Add(this.btnHighestPriority);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvServiceRequests);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.cmbStatusFilter);
            this.Controls.Add(this.lblFilterStatus);
            this.Controls.Add(this.lblDateSubmitted);
            this.Controls.Add(this.lblDateTitle);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblStatusTitle);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblDescriptionTitle);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblLocationTitle);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblCategoryTitle);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtRequestId);
            this.Controls.Add(this.lblRequestId);
            this.Controls.Add(this.lblHeading);
            this.Name = "ServiceRequestStatusForm";
            this.Text = "ServiceRequestStatusForm";
            this.Load += new System.EventHandler(this.ServiceRequestStatusForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceRequests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblRequestId;
        private System.Windows.Forms.TextBox txtRequestId;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblLocationTitle;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblDescriptionTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDateTitle;
        private System.Windows.Forms.Label lblDateSubmitted;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.DataGridView dgvServiceRequests;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnHighestPriority;
        private System.Windows.Forms.Button btnBFS;
        private System.Windows.Forms.Button btnDFS;
        private System.Windows.Forms.Button btnMST;
    }
}