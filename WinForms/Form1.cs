
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
			txtLastName.KeyPress += TxtLastName_KeyPress;
			txtLastName.TextChanged += TxtLastName_TextChanged;
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

		UpdateRowCount();
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



		foreach (DataRow row in source.Rows)
		{
			string lastName = Convert.ToString(row["last_name"]) ?? string.Empty;
			string firstName = Convert.ToString(row["first_name"]) ?? string.Empty;
			string secondName = Convert.ToString(row["second_name"]) ?? string.Empty;
            string statusName = Convert.ToString(row["name"]) ?? string.Empty;
            string depName = Convert.ToString(row["name1"]) ?? string.Empty;
            string postName = Convert.ToString(row["name2"]) ?? string.Empty;

            string initials = string.Empty;
			if (!string.IsNullOrWhiteSpace(firstName)) initials += " " + char.ToUpperInvariant(firstName[0]) + ".";
			if (!string.IsNullOrWhiteSpace(secondName)) initials += " " + char.ToUpperInvariant(secondName[0]) + ".";
			string fio = (lastName + initials).Trim();

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
			if (!IsLettersOnly(txtLastName.Text))
			{
				MessageBox.Show("Вводите только буквы в поле Фамилия.", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			await LoadPersonsAsync();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	private bool _lastNameWarned;

	private void TxtLastName_KeyPress(object? sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
		{
			e.Handled = true;
			if (!_lastNameWarned)
			{
				_lastNameWarned = true;
				MessageBox.Show("Разрешены только буквы.", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}

	private void TxtLastName_TextChanged(object? sender, EventArgs e)
	{
		if (IsLettersOnly(txtLastName.Text))
		{
			_lastNameWarned = false;
		}
	}

	private static bool IsLettersOnly(string? text)
	{
		if (string.IsNullOrWhiteSpace(text)) return true;
		foreach (var ch in text)
		{
			if (!char.IsLetter(ch)) return false;
		}
		return true;
	}

	private void UpdateRowCount()
	{
		if (gridPersons?.DataSource is DataTable table)
		{
			lblRowCount.Text = $"Строк: {table.Rows.Count}";
		}
		else
		{
			lblRowCount.Text = $"Строк: {gridPersons?.Rows.Count ?? 0}";
		}
	}
    }

public class LookupItem
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public override string ToString() => Name;
}
