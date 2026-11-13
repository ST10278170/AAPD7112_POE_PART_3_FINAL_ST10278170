using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ServiceRequestStatusForm : Form
    {
        public ServiceRequestStatusForm()
        {
            InitializeComponent();

            btnBFS.Click += delegate
            {
                if (!int.TryParse(txtRequestId.Text.Trim(), out int requestId))
                {
                    MessageBox.Show(
                        "Please enter a valid numeric Request ID.",
                        "Invalid Request ID",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                ServiceRequest request =
                    ServiceRequestRepository.FindRequest(requestId);

                if (request == null)
                {
                    MessageBox.Show(
                        "No service request was found with that Request ID.",
                        "Request Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                List<int> traversal =
                    ServiceRequestRepository.GetRelatedRequestsBFS(requestId);

                string result = traversal.Count > 0
                    ? string.Join(" -> ", traversal)
                    : "No related requests found.";

                MessageBox.Show(
                    "Breadth-First Search starting from Request " +
                    requestId +
                    ":\n\n" +
                    result,
                    "BFS Graph Traversal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            };
        }


        private void ServiceRequestStatusForm_Load(object sender, EventArgs e)
        {
            // Populate status filter.
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("Pending");
            cmbStatusFilter.Items.Add("In Progress");
            cmbStatusFilter.Items.Add("Completed");

            cmbStatusFilter.SelectedIndex = 0;

            // Display all requests when the form opens.
            DisplayRequests(
                ServiceRequestRepository.GetAllRequests());

            // Configure DataGridView.
            dgvServiceRequests.ReadOnly = true;
            dgvServiceRequests.AllowUserToAddRows = false;
            dgvServiceRequests.AllowUserToDeleteRows = false;

            dgvServiceRequests.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvServiceRequests.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            ApplyTheme();
        }


        // -------------------------------------------------
        // SEARCH
        // -------------------------------------------------

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!TryGetRequestId(out int requestId))
            {
                return;
            }

            ServiceRequest request =
                ServiceRequestRepository.FindRequest(requestId);

            if (request == null)
            {
                ClearRequestDetails();

                MessageBox.Show(
                    "No service request was found with that Request ID.",
                    "Request Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DisplayRequestDetails(request);
        }


        // -------------------------------------------------
        // FILTER
        // -------------------------------------------------

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (cmbStatusFilter.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a status.",
                    "Select Status",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            string selectedStatus =
                cmbStatusFilter.SelectedItem.ToString();

            List<ServiceRequest> requests =
                ServiceRequestRepository
                .GetRequestsByStatus(selectedStatus);

            DisplayRequests(requests);
        }


        // -------------------------------------------------
        // SHOW ALL
        // Uses BST in-order traversal through repository.
        // -------------------------------------------------

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DisplayRequests(
                ServiceRequestRepository.GetAllRequests());
        }


        // -------------------------------------------------
        // HIGHEST PRIORITY
        // Demonstrates the Max Heap.
        // -------------------------------------------------

        private void btnHighestPriority_Click(
            object sender,
            EventArgs e)
        {
            ServiceRequest request =
                ServiceRequestRepository
                .GetHighestPriorityRequest();

            if (request == null)
            {
                MessageBox.Show(
                    "There are currently no service requests.",
                    "No Requests",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DisplayRequestDetails(request);

            MessageBox.Show(
                "Highest Priority Request\n\n" +
                "Request ID: " + request.RequestId + "\n" +
                "Category: " + request.Category + "\n" +
                "Location: " + request.Location + "\n" +
                "Status: " + request.Status,
                "Priority Heap Result",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        // -------------------------------------------------
        // BREADTH-FIRST SEARCH
        // Demonstrates BFS graph traversal.
        // -------------------------------------------------


        // -------------------------------------------------
        // DEPTH-FIRST SEARCH
        // Demonstrates DFS graph traversal.
        // -------------------------------------------------

        private void btnDFS_Click(object sender, EventArgs e)
        {
            if (!TryGetRequestId(out int requestId))
            {
                return;
            }

            ServiceRequest request =
                ServiceRequestRepository.FindRequest(requestId);

            if (request == null)
            {
                MessageBox.Show(
                    "No service request was found with that Request ID.",
                    "Request Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            List<int> traversal =
                ServiceRequestRepository
                .GetRelatedRequestsDFS(requestId);

            string result =
                traversal.Count > 0
                ? string.Join(" -> ", traversal)
                : "No related requests found.";

            MessageBox.Show(
                "Depth-First Search starting from Request " +
                requestId +
                ":\n\n" +
                result,
                "DFS Graph Traversal",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        // -------------------------------------------------
        // MINIMUM SPANNING TREE
        // Demonstrates weighted relationships between
        // municipal service requests.
        // -------------------------------------------------

        private void btnMST_Click(object sender, EventArgs e)
        {
            List<ServiceRequestMinimumSpanningTree.Edge> mst =
                ServiceRequestRepository
                .GetMinimumSpanningTree();

            if (mst.Count == 0)
            {
                MessageBox.Show(
                    "There are not enough connected service requests " +
                    "to generate a Minimum Spanning Tree.",
                    "Minimum Spanning Tree",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            List<string> lines = new List<string>();

            int totalWeight = 0;

            foreach (
                ServiceRequestMinimumSpanningTree.Edge edge
                in mst)
            {
                lines.Add(
                    edge.FromRequestId +
                    " -> " +
                    edge.ToRequestId +
                    "   Weight: " +
                    edge.Weight);

                totalWeight += edge.Weight;
            }

            string result =
                string.Join(
                    Environment.NewLine,
                    lines);

            MessageBox.Show(
                "Minimum Spanning Tree\n\n" +
                result +
                "\n\nTotal Weight: " +
                totalWeight,
                "MST Result",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        // -------------------------------------------------
        // CLOSE
        // -------------------------------------------------

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        // -------------------------------------------------
        // HELPER METHODS
        // -------------------------------------------------

        private bool TryGetRequestId(out int requestId)
        {
            if (!int.TryParse(
                txtRequestId.Text.Trim(),
                out requestId))
            {
                MessageBox.Show(
                    "Please enter a valid numeric Request ID.",
                    "Invalid Request ID",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }


        private void DisplayRequestDetails(
            ServiceRequest request)
        {
            lblCategory.Text = request.Category;
            lblLocation.Text = request.Location;
            lblDescription.Text = request.Description;
            lblStatus.Text = request.Status;

            lblDateSubmitted.Text =
                request.DateSubmitted
                .ToString("dd MMMM yyyy, HH:mm");
        }


        private void DisplayRequests(
            List<ServiceRequest> requests)
        {
            dgvServiceRequests.DataSource = null;
            dgvServiceRequests.DataSource = requests;
        }


        private void ClearRequestDetails()
        {
            lblCategory.Text = "-";
            lblLocation.Text = "-";
            lblDescription.Text = "-";
            lblStatus.Text = "-";
            lblDateSubmitted.Text = "-";
        }


        // -------------------------------------------------
        // THEME
        // -------------------------------------------------

        private void ApplyTheme()
        {
            BackColor =
                Color.FromArgb(240, 244, 248);

            Font =
                new Font("Segoe UI", 10);

            foreach (Control ctrl in Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor =
                        Color.FromArgb(0, 122, 204);

                    btn.ForeColor =
                        Color.White;

                    btn.FlatStyle =
                        FlatStyle.Flat;

                    btn.FlatAppearance.BorderSize = 0;

                    btn.Font =
                        new Font(
                            "Segoe UI",
                            10,
                            FontStyle.Bold);

                    btn.Cursor =
                        Cursors.Hand;
                }
            }

            lblHeading.ForeColor =
                Color.FromArgb(0, 82, 155);

            lblHeading.Font =
                new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold);

            dgvServiceRequests.BackgroundColor =
                Color.White;

            dgvServiceRequests.BorderStyle =
                BorderStyle.None;

            dgvServiceRequests.GridColor =
                Color.LightGray;

            dgvServiceRequests.EnableHeadersVisualStyles =
                false;

            dgvServiceRequests
                .ColumnHeadersDefaultCellStyle
                .BackColor =
                Color.FromArgb(0, 122, 204);

            dgvServiceRequests
                .ColumnHeadersDefaultCellStyle
                .ForeColor =
                Color.White;

            dgvServiceRequests
                .ColumnHeadersDefaultCellStyle
                .Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            dgvServiceRequests.RowHeadersVisible =
                false;
        }
    }
}