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
        public ActionResult<List<Tasks>> Get()
        {
            try
            {
                List<Tasks> listTasks = _taskService.GetTasks();
                return Ok(listTasks);
            }catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(ex.Message);
                }

                return BadRequest("Erro ao criar pegar lista de Tarefas");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody]PostTasksDto tasksDto)
        {
            try
            {
                _taskService.CreateTasks(tasksDto);
                return Ok(new { message = "Tarefa Criada com sucesso"});
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
        public ActionResult Put(EditTasksDto editTaskDto)
        {
            try
            {
                _taskService.EditTask(editTaskDto);
                return Ok(new { message = "Tarefa "+editTaskDto.Title+" Editada com Sucesso" });
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
        public ActionResult Delete(string id){
            try
            {
                _taskService.DeleteTask(id);
                return Ok(new { message = "Tarefa Excluida com Sucesso" });
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message});
                }

                return BadRequest(new { message = "Erro ao excluir Tarefa" });

            }
            
        }

    }
}
