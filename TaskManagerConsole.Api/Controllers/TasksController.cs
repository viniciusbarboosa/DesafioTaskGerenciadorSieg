using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerConsole.Api.DTOs.Tasks;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Services;

namespace TaskManagerConsole.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TasksService _taskService;

        public TasksController(TasksService tasksService) {
            _taskService = tasksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskPopulatedDto>>> Get()
        {
            try
            {
                List<TaskPopulatedDto> listTasks = await _taskService.GetTasks();
                return Ok(listTasks);
            } catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(ex.Message);
                }

                return BadRequest("Erro ao criar pegar lista de Tarefas");
            }
        }

        [HttpGet("Paginate/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<TaskPopulatedDto>>> GetPaginate(int pageNumber, int pageSize) {
            try
            {
                List<TaskPopulatedDto> listTasks = await _taskService.GetTaksPaginate(pageNumber, pageSize);
                return Ok(listTasks);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(ex.Message);
                }

                return BadRequest("Erro ao criar pegar lista de Tarefas");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostTasksDto tasksDto)
        {
            try
            {
                await _taskService.CreateTasks(tasksDto);
                return Ok(new { message = "Tarefa Criada com sucesso" });
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return BadRequest(new { message = "Erro ao Criar Tarefa" });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(EditTasksDto editTaskDto)
        {
            try
            {
                await _taskService.EditTask(editTaskDto);
                return Ok(new { message = "Tarefa " + editTaskDto.Title + " Editada com Sucesso" });
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return BadRequest(new { message = "Erro ao Editar Tarefa" });
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id) {
            try
            {
                await _taskService.DeleteTask(id);
                return Ok(new { message = "Tarefa Excluida com Sucesso" });
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return BadRequest(new { message = "Erro ao excluir Tarefa" });

            }

        }

        [HttpPost("CompleteTask/{id}")]
        public async Task<ActionResult> PostCompleteTask(string id)
        {
            try
            {
                await _taskService.CompleteTask(id);
                return Ok(new { message = "Tarefa Completada com Sucesso"});
            }catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return BadRequest(new { message = "Erro ao excluir Tarefa" });
            }
        }

        [HttpGet("ListTaskCategory/{idCategory}")]
        public async Task<ActionResult<List<Tasks>>> GetTaskCategory(string idCategory)
        {
            try
            {
                List<Tasks> listTasks = await _taskService.ListTaskCategory(idCategory);
                return Ok(listTasks);
            }catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return BadRequest(new { message = "Erro ao Listar Por Categoria." });
            }

        }

        [HttpGet("ListTaskStatus/{status}")]
        public async Task<ActionResult<List<Tasks>>> GetTaskStatus(string status)
        {
            try
            {
                List<Tasks> listTasks = await _taskService.ListTaskStatus(status);
                return Ok(listTasks);
            }catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return BadRequest(new { message = "Erro ao Listar Por Status." });
            }
        }

        [HttpGet("ListOrderedDueDate")]
        public async Task<ActionResult<List<Tasks>>> GetTasksOrderedDueDate()
        {
            try
            {
                List<Tasks> listTasks = await _taskService.GetTasksOrderedDueDate();
                return Ok(listTasks);
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return BadRequest(new { message = "Erro ao Listar Por ordem de Vencimento." });
            }
        }

        [HttpGet("ListOverdueTasks")]
        public async Task<ActionResult<List<Tasks>>> GetTasksOverdue()
        {
            try
            {
                List<Tasks> listTasks = await _taskService.GetTasksOverdue();
                return Ok(listTasks);
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }

                return BadRequest(new { message = "Erro ao Listar Tarefas com data de vencimento atrasada" });
            }
        }

    }
}
