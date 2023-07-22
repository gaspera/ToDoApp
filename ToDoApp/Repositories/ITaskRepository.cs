namespace ToDoApp.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Data.Models.Task>> GetTasksAsync();
        Task<Data.Models.Task> GetTaskAsync(int id);
        Task<int> PostAsync([FromBody] Data.Models.Task value);
        Task PutAsync(int id, [FromBody] Data.Models.Task value);
        Task<int> DeleteAsync(int id);
    }
}
