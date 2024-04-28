// EmployeeController.cs (Controller)
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
        var employee = _employeeService.GetEmployeeById(id);
        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployee([FromBody] Employee employee)
    {
        var createdEmployee = _employeeService.CreateEmployee(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
    {
        if (id != employee.Id)
            return BadRequest();

        var updatedEmployee = _employeeService.UpdateEmployee(employee);
        if (updatedEmployee == null)
            return NotFound();

        return Ok(updatedEmployee);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var deletedEmployee = _employeeService.DeleteEmployee(id);
        if (deletedEmployee == null)
            return NotFound();

        return Ok(deletedEmployee);
    }
}