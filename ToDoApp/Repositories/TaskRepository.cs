namespace ToDoApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        ToDoAppDbContext _db;
        public TaskRepository(ToDoAppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Data.Models.Task>> GetTasksAsync()
        {
            return await _db.Tasks.ToListAsync();
        }

        public async Task<Data.Models.Task> GetTaskAsync(int id)
        {
            return await _db.Tasks.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> PostAsync([FromBody] Data.Models.Task value)
        {
            await _db.Tasks.AddAsync(value);
            await _db.SaveChangesAsync();

            return value.Id;
        }

        public async Task PutAsync(int id, [FromBody] Data.Models.Task value)
        {
            var employee = _db.Tasks.FirstOrDefault(s => s.Id == id);
            if (employee != null)
            {
                value.Id = id;
                _db.Entry<Data.Models.Task>(employee).CurrentValues.SetValues(value);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            int result = 0;

            var task = _db.Tasks.FirstOrDefault(s => s.Id == id);
            if (task != null)
            {
                _db.Tasks.Remove(task);
                result = await _db.SaveChangesAsync();
            }

            return result;
        }

    }
}
