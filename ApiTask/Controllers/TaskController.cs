using System;
using System.Threading.Tasks;
using ApiTask.DTOs;
using ApiTask.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiTask.Controllers
{
    public class TaskController : ControllerBase
    {
        private readonly  ITaskServices _taskServices;
        private  readonly IMapper _mapper;

        public TaskController(ITaskServices taskServices, IMapper mapper)
        {
            _taskServices = taskServices;
            _mapper = mapper;
            
        }
        [HttpGet("get-tasks/{Id}")]
        public IActionResult Get(string id)
        {
          var result =  _taskServices.GetTaskById(id);
            return Ok(result);
        }

        [HttpGet("get-all-tasks")]
        public async Task<IActionResult> GetList(int size, int page)
        {
            size = size < 1 ? 10 : size;
            page = page < 1 ? 1 : page;

            var result = await _taskServices.PaginatedResult(size, page);
            if (result == null || result.Count == 0)
            {
                return NotFound("No Data Record is Found");
            }

            // map result from Task to TaskToReturnDto

            var taskToReturnDto = _mapper.Map<TaskToReturnDto>(result);
            
            return Ok(taskToReturnDto);
        }


    }
}
