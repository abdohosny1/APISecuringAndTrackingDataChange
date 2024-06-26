using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAPISecuringAndTrackingDataChange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;



        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Departments.GellAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            return Ok(await _unitOfWork.Departments.AddAsync(department));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null) return NotFound($"Not found Id ={id}");

            var item = await _unitOfWork.Departments.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound($"Not found Id ={id}");
            }
            else
            {
                await _unitOfWork.Departments.DeleteAsync(item);
                return NoContent();
            }

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Department>> updateDepartment(int id, Department departmentDto)
        {
            if (id == null) return NotFound($"Not found Id ={id}");

            var item = await _unitOfWork.Departments.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound($"Not found Id ={id}");
            }
            else
            {
                item.Name = departmentDto.Name;
                await _unitOfWork.Departments.UpdateAsync(item);

                var result = new Department()
                {
                    Name = item.Name,
                    Id = item.Id,


                };
                return Ok(result);
            }

        }
    }
}
