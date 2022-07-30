using System;
using System.Collections.Generic;
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

        [HttpPost("add")]
        public async Task<IActionResult> Post(AddNewTaskDto newTask)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            //Map to Task Shape 
            var taskToAdd = _mapper.Map<Models.Task>(newTask);

            //add to database
            var result = await _taskServices.AddTask(taskToAdd);
            if (result == null)
                return BadRequest("Failed to add task!");
            return CreatedAtRoute("Get" ,new {Id = taskToAdd.Id} ,taskToAdd);
            // return Ok($"New task added Id: {result}");
        }
 
        [HttpGet("get-tasks/{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var result = await _taskServices.GetTaskById(Id);
            if (result == null)
                return BadRequest("The Data is Not Found!");

            var taskReturnDto = _mapper.Map<TaskToReturnDto>(result);
            return Ok(taskReturnDto);
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

            var taskToReturnDto = _mapper.Map<List<TaskToReturnDto>>(result);
            
            return Ok(taskToReturnDto);
        }


    }
}
