
using System.Data;
using WinFormsService;

namespace WinForms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

	private async void Form1_Load(object sender, EventArgs e)
	{
		try
		{
			await LoadLookupsAsync();
			await LoadPersonsAsync();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	private async Task LoadLookupsAsync()
	{
		var repo = new SqlRepository();
		var statuses = await repo.GetStatusesAsync();
		var deps = await repo.GetDepsAsync();
		var posts = await repo.GetPostsAsync();

		cboStatus.Items.Clear();
		cboStatus.Items.Add(new LookupItem { Id = 0, Name = "Все" });
		foreach (var s in statuses)
			cboStatus.Items.Add(new LookupItem { Id = s.Id, Name = s.Name });
		cboStatus.SelectedIndex = 0;

		cboDep.Items.Clear();
		cboDep.Items.Add(new LookupItem { Id = 0, Name = "Все" });
		foreach (var d in deps)
			cboDep.Items.Add(new LookupItem { Id = d.Id, Name = d.Name });
		cboDep.SelectedIndex = 0;

		cboPost.Items.Clear();
		cboPost.Items.Add(new LookupItem { Id = 0, Name = "Все" });
		foreach (var p in posts)
			cboPost.Items.Add(new LookupItem { Id = p.Id, Name = p.Name });
		cboPost.SelectedIndex = 0;
	}

	private async Task LoadPersonsAsync()
	{
		var repo = new SqlRepository();

		int? statusId = (cboStatus.SelectedItem is LookupItem si && si.Id != 0) ? si.Id : null;
		int? depId = (cboDep.SelectedItem is LookupItem di && di.Id != 0) ? di.Id : null;
		int? postId = (cboPost.SelectedItem is LookupItem pi && pi.Id != 0) ? pi.Id : null;
		DateTime? dateFrom = chkUseFrom.Checked ? dtpFrom.Value.Date : null;
		DateTime? dateTo = chkUseTo.Checked ? dtpTo.Value.Date : null;
		string? lastNameLike = string.IsNullOrWhiteSpace(txtLastName.Text) ? null : txtLastName.Text.Trim();

		var rawTable = await repo.GetPersonsAsync(statusId, depId, postId, dateFrom, dateTo, lastNameLike);
		var view = BuildPersonsView(rawTable);
		gridPersons.AutoGenerateColumns = true;
		gridPersons.DataSource = view;

		if (gridPersons.Columns.Contains("Дата приема"))
			gridPersons.Columns["Дата приема"].DefaultCellStyle.Format = "d";
		if (gridPersons.Columns.Contains("Дата увольнения"))
			gridPersons.Columns["Дата увольнения"].DefaultCellStyle.Format = "d";
	}

	private static DataTable BuildPersonsView(DataTable source)
	{
		var result = new DataTable();
		result.Columns.Add("Фамилия И. О", typeof(string));
		result.Columns.Add("Статус", typeof(string));
		result.Columns.Add("Отдел", typeof(string));
		result.Columns.Add("Должность", typeof(string));
		result.Columns.Add("Дата приема", typeof(DateTime));
		result.Columns.Add("Дата увольнения", typeof(DateTime));

		string? DuplicateName(string baseName, int index)
		{
			var candidate = index == 0 ? baseName : baseName + index.ToString();
			return source.Columns.Contains(candidate) ? candidate : null;
		}

		string? statusCol = source.Columns.Contains("status_name") ? "status_name" : DuplicateName("name", 0);
		string? depCol = source.Columns.Contains("dep_name") ? "dep_name" : DuplicateName("name", 1);
		string? postCol = source.Columns.Contains("post_name") ? "post_name" : DuplicateName("name", 2);

		foreach (DataRow row in source.Rows)
		{
			string lastName = Convert.ToString(row["last_name"]) ?? string.Empty;
			string firstName = Convert.ToString(row["first_name"]) ?? string.Empty;
			string secondName = Convert.ToString(row["second_name"]) ?? string.Empty;

			string initials = string.Empty;
			if (!string.IsNullOrWhiteSpace(firstName)) initials += " " + char.ToUpperInvariant(firstName[0]) + ".";
			if (!string.IsNullOrWhiteSpace(secondName)) initials += " " + char.ToUpperInvariant(secondName[0]) + ".";
			string fio = (lastName + initials).Trim();

			string statusName = statusCol != null ? Convert.ToString(row[statusCol]) ?? string.Empty : string.Empty;
			string depName = depCol != null ? Convert.ToString(row[depCol]) ?? string.Empty : string.Empty;
			string postName = postCol != null ? Convert.ToString(row[postCol]) ?? string.Empty : string.Empty;

			object employ = DBNull.Value;
			if (!(row["date_employ"] is DBNull)) employ = row["date_employ"];

			object uneploy = DBNull.Value;
			if (source.Columns.Contains("date_uneploy") && !(row["date_uneploy"] is DBNull)) uneploy = row["date_uneploy"];

			result.Rows.Add(fio, statusName, depName, postName, employ, uneploy);
		}

		return result;
	}

	private async void btnSearch_Click(object sender, EventArgs e)
	{
		try
		{
			await LoadPersonsAsync();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
    }

public class LookupItem
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public override string ToString() => Name;
}
