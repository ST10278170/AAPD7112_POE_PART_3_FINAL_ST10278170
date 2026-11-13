
using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ReportIssuesForm : Form
    {
        private string _mediaPath;

        public ReportIssuesForm()
        {
            InitializeComponent();
        }


        // ==========================================
        // FORM LOAD
        // ==========================================

        private void ReportIssuesForm_Load(object sender, EventArgs e)
        {
            ApplyUnifiedTheme(this);

            this.Font = new Font("Segoe UI", 10);

            // Prevent duplicate category entries.
            cmbCategory.Items.Clear();

            cmbCategory.Items.Add("Sanitation");
            cmbCategory.Items.Add("Roads");
            cmbCategory.Items.Add("Utilities");
            cmbCategory.Items.Add("Electricity");
            cmbCategory.Items.Add("Water");

            cmbCategory.SelectedIndex = -1;
        }


        // ==========================================
        // ATTACH MEDIA
        // ==========================================

        private void btnAttachMedia_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Attach Supporting Evidence";

                ofd.Filter =
                    "Images and Documents|*.jpg;*.jpeg;*.png;*.pdf;*.doc;*.docx|" +
                    "Image Files|*.jpg;*.jpeg;*.png|" +
                    "Document Files|*.pdf;*.doc;*.docx";

                ofd.Multiselect = false;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    _mediaPath = ofd.FileName;

                    LoadImagePreview(_mediaPath);

                    lblEngagement.Text =
                        "Attachment added. Thank you for providing more detail.";
                }
                catch (Exception ex)
                {
                    _mediaPath = null;

                    ClearImagePreview();

                    MessageBox.Show(
                        "The selected attachment could not be loaded.\n\n" +
                        ex.Message,
                        "Attachment Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }


        // Load an image without locking the original file.
        private void LoadImagePreview(string filePath)
        {
            string extension =
                Path.GetExtension(filePath).ToLowerInvariant();

            bool isImage =
                extension == ".jpg" ||
                extension == ".jpeg" ||
                extension == ".png";

            if (!isImage)
            {
                ClearImagePreview();
                return;
            }

            using (Image sourceImage = Image.FromFile(filePath))
            {
                Image previewImage = new Bitmap(sourceImage);

                ClearImagePreview();

                picPreview.Image = previewImage;
            }
        }


        // Safely clear and dispose of the current preview.
        private void ClearImagePreview()
        {
            if (picPreview.Image != null)
            {
                Image oldImage = picPreview.Image;

                picPreview.Image = null;

                oldImage.Dispose();
            }
        }


        // ==========================================
        // SUBMIT ISSUE
        // ==========================================

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate required fields before confirmation.
            bool locationError =
                string.IsNullOrWhiteSpace(txtLocation.Text);

            bool categoryError =
                cmbCategory.SelectedItem == null;

            bool descriptionError =
                string.IsNullOrWhiteSpace(rtbDescription.Text);

            HighlightControl(txtLocation, locationError);
            HighlightControl(cmbCategory, categoryError);
            HighlightControl(rtbDescription, descriptionError);

            if (locationError || categoryError || descriptionError)
            {
                MessageBox.Show(
                    "Please complete all required fields.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                lblEngagement.Text =
                    "Let's complete all fields so your report can help the community.";

                return;
            }


            // Ask for confirmation before submitting.
            DialogResult result = MessageBox.Show(
                "Submit this report?",
                "Confirm Submission",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;


            // Create the issue object.
            Issue issue = new Issue
            {
                Location = txtLocation.Text.Trim(),
                Category = cmbCategory.SelectedItem.ToString(),
                Description = rtbDescription.Text.Trim(),
                MediaPath = _mediaPath,
                CreatedAt = DateTime.Now
            };


            // Generate a unique service request ID.
            int requestId =
                ServiceRequestRepository.GetNextRequestId();


            // Link the issue to its corresponding service request.
            issue.RequestId = requestId;


            // Create the corresponding service request.
            ServiceRequest serviceRequest = new ServiceRequest(
                requestId,
                issue.Description,
                issue.Category,
                issue.Location,
                "Pending",
                issue.CreatedAt
            );


            // Add the service request to the repository first.
            bool requestAdded =
                ServiceRequestRepository.AddRequest(serviceRequest);

            if (!requestAdded)
            {
                MessageBox.Show(
                    "The service request could not be added. " +
                    "Please try again.",
                    "Submission Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // Store the issue after successful request creation.
            IssueRepository.Issues.Add(issue);


            // Confirm successful submission.
            MessageBox.Show(
                "Your issue has been submitted successfully.\n\n" +
                "Your Service Request ID is: " + requestId +
                "\n\nThank you for helping improve municipal services.",
                "Submission Successful",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);


            lblEngagement.Text =
                "Great job reporting this issue—your input supports better service delivery.";


            // Clear the form after successful submission.
            ClearFormFields();

        }


        // ==========================================
        // CLEAR FORM
        // ==========================================

        private void ClearFormFields()
        {
            txtLocation.Clear();

            cmbCategory.SelectedIndex = -1;

            rtbDescription.Clear();

            ClearImagePreview();

            _mediaPath = null;

            HighlightControl(txtLocation, false);
            HighlightControl(cmbCategory, false);
            HighlightControl(rtbDescription, false);
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Clear all fields?",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            ClearFormFields();

            lblEngagement.Text =
                "Fields cleared. You can start a new report anytime.";
        }


        // ==========================================
        // NAVIGATION
        // ==========================================

        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // ==========================================
        // INPUT VALIDATION AND FORM FEEDBACK
        // ==========================================

        private void HighlightControl(Control control, bool hasError)
        {
            control.BackColor =
                hasError ? Color.LightPink : Color.White;
        }


        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                HighlightControl(rtbDescription, false);
            }
        }


        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedItem != null)
            {
                HighlightControl(cmbCategory, false);
            }
        }


        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                HighlightControl(txtLocation, false);
            }
        }


        // ==========================================
        // IMAGE PREVIEW
        // ==========================================

        private void picPreview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_mediaPath))
                return;

            if (!File.Exists(_mediaPath))
            {
                MessageBox.Show(
                    "The attachment file could not be found.",
                    "Attachment Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _mediaPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The attachment could not be opened.\n\n" +
                    ex.Message,
                    "Unable to Open Attachment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // ==========================================
        // UNIFIED APPLICATION THEME
        // ==========================================

        private void ApplyUnifiedTheme(Control parent)
        {
            // Modern municipal background.
            parent.BackColor = Color.FromArgb(240, 244, 248);

            foreach (Control ctrl in parent.Controls)
            {
                // Buttons.
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(0, 122, 204);
                    btn.ForeColor = Color.White;

                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;

                    btn.Font = new Font(
                        "Segoe UI",
                        10,
                        FontStyle.Bold);

                    btn.Cursor = Cursors.Hand;
                }

                // Labels.
                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.FromArgb(33, 37, 41);

                    if (lbl.Name.Contains("Heading"))
                    {
                        lbl.ForeColor = Color.FromArgb(0, 82, 155);

                        lbl.Font = new Font(
                            "Segoe UI",
                            18,
                            FontStyle.Bold);
                    }
                }

                // TextBoxes.
                if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;

                    txt.Font = new Font(
                        "Segoe UI",
                        10);
                }

                // RichTextBoxes.
                if (ctrl is RichTextBox rtb)
                {
                    rtb.BackColor = Color.White;

                    rtb.Font = new Font(
                        "Segoe UI",
                        10);
                }

                // ComboBoxes.
                if (ctrl is ComboBox cmb)
                {
                    cmb.BackColor = Color.White;

                    cmb.Font = new Font(
                        "Segoe UI",
                        10);
                }

                // PictureBoxes.
                if (ctrl is PictureBox pic)
                {
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.BackColor = Color.White;
                }

                // Engagement label.
                if (ctrl.Name == "lblEngagement")
                {
                    ctrl.ForeColor = Color.FromArgb(108, 117, 125);

                    ctrl.Font = new Font(
                        "Segoe UI",
                        10,
                        FontStyle.Italic);
                }

                // Apply the theme to child controls.
                if (ctrl.HasChildren)
                {
                    ApplyUnifiedTheme(ctrl);
                }
            }
        }
    }
}
