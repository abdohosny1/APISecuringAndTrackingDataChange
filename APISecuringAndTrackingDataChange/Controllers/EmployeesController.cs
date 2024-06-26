using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyAPISecuringAndTrackingDataChange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;



        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Employees.GellAllAsync());
        }

        [HttpPost]

        public async Task<IActionResult> Add(Employee employee)
        {
            return Ok(await _unitOfWork.Employees.AddAsync(employee));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null) return NotFound($"Not found Id ={id}");

            var item = await _unitOfWork.Employees.GetByIDAsync(id);

            if (item == null)
            {
                return NotFound($"Not found Id ={id}");
            }
            else
            {
                await _unitOfWork.Employees.DeleteAsync(item);
                return NoContent();
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> update(int id, Employee employeeDto)
        {
            if (id == null) return NotFound($"Not found Id ={id}");
            var item = await _unitOfWork.Employees.GetByIDAsync(id);
            if (item == null)
            {
                return NotFound($"Not found Id ={id}");
            }
            else
            {
                item.Address = employeeDto.Address;
                item.Name = employeeDto.Name;

                await _unitOfWork.Employees.UpdateAsync(item);

                return Ok();
            }


        }
    }
}

