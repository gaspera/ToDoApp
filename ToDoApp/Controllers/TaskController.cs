using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        // GET: api/<TaskController>
        [HttpGet]
        public async Task<ActionResult<List<Data.Models.Task>>> GetTasks()
        {
            try
            {
                var tasks = await _taskRepository.GetTasksAsync();
                if (tasks.IsNullOrEmpty())
                {
                    return NotFound();
                }

                return Ok(tasks);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Models.Task>> GetTask(int id)
        {
            try
            {
                var task = await _taskRepository.GetTaskAsync(id);
                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<TaskController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] Data.Models.Task value)
        {
            try
            {
                if (value?.Id != 0)
                {
                    return BadRequest("Task id is generated in application!");
                }

                var taskId = await _taskRepository.PostAsync(value);
                if (taskId > 0)
                {
                    return Ok(taskId);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Data.Models.Task value)
        {            

            if (ModelState.IsValid)
            {
                try
                {
                    await _taskRepository.PutAsync(id, value);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int result = await _taskRepository.DeleteAsync(id);

                if (result == 0)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
