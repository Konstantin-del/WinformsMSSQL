

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

		var table = await repo.GetPersonsAsync(statusId, depId, postId, dateFrom, dateTo, lastNameLike);
		gridPersons.DataSource = table;
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

public sealed class LookupItem
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public override string ToString() => Name;
}
