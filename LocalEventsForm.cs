
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        // Stores the user's most recent category searches.
        private readonly Queue<string> searchHistory =
            new Queue<string>();

        // Organises events according to their dates.
        private readonly SortedDictionary<DateTime, List<EventItem>>
            sortedEvents =
                new SortedDictionary<DateTime, List<EventItem>>();

        // Allows fast lookup of events by title.
        // A list is used because different events may share a title.
        private readonly Dictionary<string, List<EventItem>> eventLookup =
            new Dictionary<string, List<EventItem>>(
                StringComparer.OrdinalIgnoreCase);

        public LocalEventsForm()
        {
            InitializeComponent();

            dgvEvents.CellDoubleClick += DgvEvents_CellDoubleClick;
            btnClearSearch.Click += btnClearSearch_Click;
            chkFilterByDate.CheckedChanged += chkFilterByDate_CheckedChanged;
        }

        private void LocalEventsForm_Load(object sender, EventArgs e)
        {
            ApplyUnifiedTheme(this);
            ConfigureForm();
            ConfigureEventGrid();
            BuildEventDataStructures();
            LoadCategories();

            chkFilterByDate.Checked = false;
            dtpEventDate.Enabled = false;

            LoadEvents();

            lstRecommendations.Items.Clear();
            lstRecommendations.Items.Add(
                "Search by category to receive event recommendations.");
        }

        private void ConfigureForm()
        {
            this.Font = new Font("Segoe UI", 10);
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.Text =
                "Municipal Services - Local Events and Announcements";
        }

        private void ConfigureEventGrid()
        {
            dgvEvents.ReadOnly = true;
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.MultiSelect = false;
            dgvEvents.AutoGenerateColumns = true;

            dgvEvents.BackgroundColor = Color.White;
            dgvEvents.BorderStyle = BorderStyle.None;
            dgvEvents.GridColor = Color.FromArgb(220, 225, 230);
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.EnableHeadersVisualStyles = false;

            dgvEvents.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(0, 82, 155);

            dgvEvents.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvEvents.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvEvents.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvEvents.ColumnHeadersHeight = 38;

            dgvEvents.DefaultCellStyle.BackColor = Color.White;
            dgvEvents.DefaultCellStyle.ForeColor =
                Color.FromArgb(33, 37, 41);

            dgvEvents.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(207, 226, 243);

            dgvEvents.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(20, 46, 75);

            dgvEvents.DefaultCellStyle.Font =
                new Font("Segoe UI", 9);

            dgvEvents.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(247, 249, 252);
        }

        private void BuildEventDataStructures()
        {
            sortedEvents.Clear();
            eventLookup.Clear();

            foreach (EventItem evt in EventRepository.Events)
            {
                if (evt == null)
                {
                    continue;
                }

                DateTime eventDate = evt.EventDate.Date;

                // SortedDictionary groups events by date.
                if (!sortedEvents.ContainsKey(eventDate))
                {
                    sortedEvents[eventDate] =
                        new List<EventItem>();
                }

                sortedEvents[eventDate].Add(evt);

                // Dictionary groups events by title.
                if (!eventLookup.ContainsKey(evt.Title))
                {
                    eventLookup[evt.Title] =
                        new List<EventItem>();
                }

                eventLookup[evt.Title].Add(evt);
            }

            // Sort events sharing the same date by priority.
            // Lower priority numbers appear first.
            foreach (List<EventItem> eventsOnDate in sortedEvents.Values)
            {
                eventsOnDate.Sort(
                    (firstEvent, secondEvent) =>
                        firstEvent.Priority.CompareTo(
                            secondEvent.Priority));
            }
        }

        private void LoadCategories()
        {
            // HashSet ensures that categories remain unique.
            HashSet<string> categories =
                new HashSet<string>(
                    EventRepository.Events
                        .Where(evt => evt != null)
                        .Select(evt => evt.Category)
                        .Where(category =>
                            !string.IsNullOrWhiteSpace(category)),
                    StringComparer.OrdinalIgnoreCase);

            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");

            foreach (string category in categories.OrderBy(
                category => category))
            {
                cmbCategory.Items.Add(category);
            }

            cmbCategory.SelectedIndex = 0;
        }

        private void LoadEvents()
        {
            // Display only events scheduled for today or later.
            List<EventItem> upcomingEvents = sortedEvents
                .Where(dateGroup =>
                    dateGroup.Key >= DateTime.Today)
                .SelectMany(dateGroup => dateGroup.Value)
                .OrderBy(evt => evt.EventDate)
                .ThenBy(evt => evt.Priority)
                .ToList();

            DisplayEvents(upcomingEvents);
        }

        private void DisplayEvents(IEnumerable<EventItem> eventsToDisplay)
        {
            List<EventItem> eventList = eventsToDisplay.ToList();

            dgvEvents.DataSource = null;
            dgvEvents.DataSource = eventList;

            if (dgvEvents.Columns["Title"] != null)
            {
                dgvEvents.Columns["Title"].HeaderText = "Event Title";
                dgvEvents.Columns["Title"].FillWeight = 120;
            }

            if (dgvEvents.Columns["Category"] != null)
            {
                dgvEvents.Columns["Category"].HeaderText = "Category";
                dgvEvents.Columns["Category"].FillWeight = 75;
            }

            if (dgvEvents.Columns["EventDate"] != null)
            {
                dgvEvents.Columns["EventDate"].HeaderText = "Event Date";
                dgvEvents.Columns["EventDate"].DefaultCellStyle.Format =
                    "dd MMMM yyyy";
                dgvEvents.Columns["EventDate"].FillWeight = 90;
            }

            if (dgvEvents.Columns["Description"] != null)
            {
                dgvEvents.Columns["Description"].HeaderText = "Description";
                dgvEvents.Columns["Description"].FillWeight = 180;
            }

            if (dgvEvents.Columns["Priority"] != null)
            {
                dgvEvents.Columns["Priority"].HeaderText = "Priority Level";
                dgvEvents.Columns["Priority"].FillWeight = 65;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchEvents();
        }

        private void SearchEvents()
        {
            string searchText = txtEventSearch.Text.Trim();

            string selectedCategory =
                cmbCategory.SelectedItem?.ToString()
                ?? "All Categories";

            DateTime selectedDate = dtpEventDate.Value.Date;

            RecordSearch(selectedCategory);

            // Start with future events only.
            IEnumerable<EventItem> filteredEvents =
                EventRepository.Events.Where(evt =>
                    evt != null &&
                    evt.EventDate.Date >= DateTime.Today);

            // Search event titles, descriptions and categories.
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredEvents = filteredEvents.Where(evt =>
                    (evt.Title ?? "").IndexOf(
                        searchText,
                        StringComparison.OrdinalIgnoreCase) >= 0

                    ||

                    (evt.Description ?? "").IndexOf(
                        searchText,
                        StringComparison.OrdinalIgnoreCase) >= 0

                    ||

                    (evt.Category ?? "").IndexOf(
                        searchText,
                        StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Apply the category filter when a specific category is selected.
            if (selectedCategory != "All Categories")
            {
                filteredEvents = filteredEvents.Where(evt =>
                    string.Equals(
                        evt.Category,
                        selectedCategory,
                        StringComparison.OrdinalIgnoreCase));
            }

            // Apply the date filter only when the checkbox is selected.
            if (chkFilterByDate.Checked)
            {
                filteredEvents = filteredEvents.Where(evt =>
                    evt.EventDate.Date == selectedDate);
            }

            List<EventItem> searchResults = filteredEvents
                .OrderBy(evt => evt.EventDate)
                .ThenBy(evt => evt.Priority)
                .ToList();

            DisplayEvents(searchResults);
            UpdateRecommendations();

            if (searchResults.Count == 0)
            {
                MessageBox.Show(
                    "No upcoming events matched your search. " +
                    "Try another keyword, category or date.",
                    "No Events Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void chkFilterByDate_CheckedChanged(
            object sender,
            EventArgs e)
        {
            dtpEventDate.Enabled = chkFilterByDate.Checked;
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtEventSearch.Clear();
            cmbCategory.SelectedIndex = 0;
            chkFilterByDate.Checked = false;
            dtpEventDate.Enabled = false;

            LoadEvents();

            lstRecommendations.Items.Clear();
            lstRecommendations.Items.Add(
                "Search by category to receive event recommendations.");
        }

        private void RecordSearch(string selectedCategory)
        {
            // All Categories does not indicate a specific preference.
            if (selectedCategory == "All Categories")
            {
                return;
            }

            searchHistory.Enqueue(selectedCategory);

            // Keep only the 10 most recent searches.
            if (searchHistory.Count > 10)
            {
                searchHistory.Dequeue();
            }
        }

        private void UpdateRecommendations()
        {
            lstRecommendations.Items.Clear();

            if (searchHistory.Count == 0)
            {
                lstRecommendations.Items.Add(
                    "Search for a specific category to receive recommendations.");

                return;
            }

            Dictionary<string, int> searchFrequency =
                new Dictionary<string, int>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (string category in searchHistory)
            {
                if (!searchFrequency.ContainsKey(category))
                {
                    searchFrequency[category] = 0;
                }

                searchFrequency[category]++;
            }

            string preferredCategory = searchFrequency
                .OrderByDescending(entry => entry.Value)
                .ThenBy(entry => entry.Key)
                .First()
                .Key;

            List<EventItem> recommendedEvents =
                EventRepository.Events
                    .Where(evt =>
                        evt != null &&
                        evt.EventDate.Date >= DateTime.Today &&
                        string.Equals(
                            evt.Category,
                            preferredCategory,
                            StringComparison.OrdinalIgnoreCase))
                    .OrderBy(evt => evt.EventDate)
                    .ThenBy(evt => evt.Priority)
                    .Take(3)
                    .ToList();

            if (recommendedEvents.Count == 0)
            {
                lstRecommendations.Items.Add(
                    "No upcoming recommendations are currently available.");

                return;
            }

            lstRecommendations.Items.Add(
                "Based on your interest in " + preferredCategory + ":");

            foreach (EventItem recommendedEvent in recommendedEvents)
            {
                string recommendation =
                    recommendedEvent.Title +
                    " | " +
                    recommendedEvent.EventDate.ToString("dd MMM yyyy") +
                    " | " +
                    recommendedEvent.Category;

                lstRecommendations.Items.Add(recommendation);
            }
        }

        private void DgvEvents_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            // Retrieve the actual event object from the selected row.
            EventItem selectedEvent =
                dgvEvents.Rows[e.RowIndex].DataBoundItem as EventItem;

            if (selectedEvent == null)
            {
                return;
            }

            MessageBox.Show(
                $"Title: {selectedEvent.Title}\n\n" +
                $"Category: {selectedEvent.Category}\n" +
                $"Date: {selectedEvent.EventDate:dd MMMM yyyy}\n" +
                $"Priority: {selectedEvent.Priority}\n\n" +
                $"Description:\n{selectedEvent.Description}",
                "Event Details",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // No action is required for the heading label.
        }

        private void ApplyUnifiedTheme(Control parent)
        {
            parent.BackColor = Color.FromArgb(240, 244, 248);

            foreach (Control ctrl in parent.Controls)
            {
                // Buttons
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
                    btn.Height = Math.Max(btn.Height, 36);
                }

                // Labels
                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.FromArgb(33, 37, 41);
                    lbl.Font = new Font(
                        "Segoe UI",
                        10,
                        FontStyle.Regular);

                    if (lbl.Name == "lblHeading")
                    {
                        lbl.ForeColor = Color.FromArgb(0, 82, 155);
                        lbl.Font = new Font(
                            "Segoe UI",
                            20,
                            FontStyle.Bold);
                    }
                    else if (lbl.Name == "lblUpcomingEvents" ||
                             lbl.Name == "lblRecommendations")
                    {
                        lbl.ForeColor = Color.FromArgb(0, 82, 155);
                        lbl.Font = new Font(
                            "Segoe UI",
                            12,
                            FontStyle.Bold);
                    }
                }

                // Text boxes
                if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.White;
                    txt.ForeColor = Color.FromArgb(33, 37, 41);
                    txt.Font = new Font("Segoe UI", 10);
                }

                // ComboBoxes
                if (ctrl is ComboBox cmb)
                {
                    cmb.BackColor = Color.White;
                    cmb.ForeColor = Color.FromArgb(33, 37, 41);
                    cmb.Font = new Font("Segoe UI", 10);
                    cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                }

                // Date controls
                if (ctrl is DateTimePicker dtp)
                {
                    dtp.CalendarMonthBackground = Color.White;
                    dtp.Font = new Font("Segoe UI", 10);
                }

                // Checkboxes
                if (ctrl is CheckBox chk)
                {
                    chk.ForeColor = Color.FromArgb(33, 37, 41);
                    chk.Font = new Font("Segoe UI", 10);
                }

                // Recommendation list
                if (ctrl is ListBox lst)
                {
                    lst.BackColor = Color.White;
                    lst.ForeColor = Color.FromArgb(33, 37, 41);
                    lst.BorderStyle = BorderStyle.FixedSingle;
                    lst.Font = new Font("Segoe UI", 10);
                }

                if (ctrl.HasChildren)
                {
                    ApplyUnifiedTheme(ctrl);
                }
            }
        }
    }
}
