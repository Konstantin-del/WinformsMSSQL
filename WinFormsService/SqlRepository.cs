using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WinFormsService
{
    public class LookupRow
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SqlRepository
    {
        private readonly string _connectionString;

        public SqlRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public async Task<List<LookupRow>> GetStatusesAsync()
        {
            const string proc = "dbo.usp_status_list";
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(proc, cn) { CommandType = CommandType.StoredProcedure };
            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var result = new List<LookupRow>();
            while (await reader.ReadAsync())
            {
                result.Add(new LookupRow
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            return result;
        }

        public async Task<List<LookupRow>> GetDepsAsync()
        {
            const string proc = "dbo.usp_deps_list";
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(proc, cn) { CommandType = CommandType.StoredProcedure };
            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var result = new List<LookupRow>();
            while (await reader.ReadAsync())
            {
                result.Add(new LookupRow { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }
            return result;
        }

        public async Task<List<LookupRow>> GetPostsAsync()
        {
            const string proc = "dbo.usp_posts_list";
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(proc, cn) { CommandType = CommandType.StoredProcedure };
            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var result = new List<LookupRow>();
            while (await reader.ReadAsync())
            {
                result.Add(new LookupRow { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }
            return result;
        }

        public async Task<DataTable> GetPersonsAsync(int? statusId, int? depId, int? postId, DateTime? dateFrom, DateTime? dateTo, string? lastNameLike)
        {
            const string proc = "dbo.usp_persons_search";
            using var cn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(proc, cn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@status", statusId.HasValue ? statusId.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id_dep", depId.HasValue ? depId.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id_post", postId.HasValue ? postId.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@date_from", dateFrom.HasValue ? dateFrom.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@date_to", dateTo.HasValue ? dateTo.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@last_name_like", string.IsNullOrEmpty(lastNameLike) ? (object)DBNull.Value : lastNameLike);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var table = new DataTable();
            table.Load(reader);
            return table;
        }
    }
}
