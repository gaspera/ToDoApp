using Microsoft.Extensions.Configuration;

namespace ToDoApp.Test
{
    public class TaskTests
    {
        private readonly TaskRepository _repository;
        public static DbContextOptions<ToDoAppDbContext> dbContextOptions { get; }

        static TaskTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["ConnectionStrings:ToDoAppDb"];

            dbContextOptions = new DbContextOptionsBuilder<ToDoAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public TaskTests()
        {
            var context = new ToDoAppDbContext(dbContextOptions);
            var db = new TestDataDBInitializer();
            db.Seed(context);

            _repository = new TaskRepository(context);
        }

        [Fact]
        public async void GetTasks_Return_OkResult()
        {
            //Arrange  
            //Act  
            var taskList = await _repository.GetTasksAsync();

            //Assert  
            Assert.Equal(3, taskList.Count());
            Assert.Equal("Task 1", taskList[0].Naslov);
            Assert.Equal("Task 2", taskList[1].Naslov);
            Assert.Equal("Task 3", taskList[2].Naslov);
        }


        [Fact]
        public async void GetTask_Return_OkResult()
        {
            //Arrange  
            var taskId = 2;

            //Act  
            var task = await _repository.GetTaskAsync(taskId);

            //Assert  
            Assert.Equal("Task 2", task.Naslov);
            Assert.Equal("Opis task 2", task.Opis);
            Assert.True(DateTime.Now.AddMinutes(-1) < task.DatumUstvarjanja);
            Assert.Equal(true, task.Opravljeno);
        }

        [Fact]
        public async void PostTask_Return_OkResult()
        {
            //Arrange  
            var newTask = new Data.Models.Task
            {
                Naslov = "Task 4",
                Opis = "Opis task 4",
                DatumUstvarjanja = DateTime.Now,
                Opravljeno = true
            };

            //Act  
            int taskId = await _repository.PostAsync(newTask);

            var insertedTask = await _repository.GetTaskAsync(taskId);

            //Assert  
            Assert.Equal(newTask.Naslov, insertedTask.Naslov);
            Assert.Equal(newTask.Opis, insertedTask.Opis);
            Assert.Equal(newTask.DatumUstvarjanja, insertedTask.DatumUstvarjanja);
            Assert.Equal(newTask.Opravljeno, insertedTask.Opravljeno);
        }

        [Fact]
        public async void PutTask_Return_OkResult()
        {
            //Arrange  
            int updateTaskId = 3;
            var updateTask = new Data.Models.Task
            {
                Naslov = "Task 3 Updated",
                Opis = "Opis task 3 Updated",
                DatumUstvarjanja = DateTime.Now,
                Opravljeno = false
            };

            //Act  
            await _repository.PutAsync(updateTaskId, updateTask);

            var updatedTask = await _repository.GetTaskAsync(updateTaskId);

            //Assert  
            Assert.Equal(updateTask.Naslov, updatedTask.Naslov);
            Assert.Equal(updateTask.Opis, updatedTask.Opis);
            Assert.Equal(updateTask.DatumUstvarjanja, updatedTask.DatumUstvarjanja);
            Assert.Equal(updateTask.Opravljeno, updatedTask.Opravljeno);
        }

        [Fact]
        public async void DeleteTask_Return_OkResult()
        {
            //Arrange  
            int deleteTaskId = 3;
            var initialTask = await _repository.GetTasksAsync();

            //Act  
            var result = await _repository.DeleteAsync(deleteTaskId);

            var afterDeleteTask = await _repository.GetTasksAsync();

            //Assert  
            Assert.Equal(1, result);
            Assert.Equal(initialTask.Count(), afterDeleteTask.Count() + 1);
            Assert.Contains(initialTask, x => x.Id == deleteTaskId);
            Assert.DoesNotContain(afterDeleteTask, x => x.Id == deleteTaskId);
        }

    }
}
