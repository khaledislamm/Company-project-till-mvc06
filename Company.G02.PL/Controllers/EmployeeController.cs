using AutoMapper;
using Company.G02.BLL.Interfaces;
using Company.G02.DAL.Models;
using Company.G02.PL.Dto;
using Company.G02.PL.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepratmentRepository _depratmentRepository;
        private readonly IMapper _mapper;

        // ASK CLR Create Object From DepartmentRepository
        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepratmentRepository depratmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_depratmentRepository = depratmentRepository;
            _mapper = mapper;
        }

        [HttpGet] // GET: /Department/Index
        public async Task<IActionResult> Index(string? SearchInput)
        {   
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            // Dictionary : 3 Property
            // 1.ViewData : Transfer Extra Information From Controllerc (Action) To View
            //ViewData["Message"] = "Hello From ViewData";

            // 2.ViewBag  : Transfer Extra Information From Controllerc (Action) To View
            //ViewBag.Message = new {Message = "Hello From ViewBag"};

            // 3.TempData

            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           var departments = await _unitOfWork.DepratmentRepository.GetAllAsync();
            ViewData["departments"] = departments;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
                }

                var employee = _mapper.Map<Employee>(model);
                 await _unitOfWork.EmployeeRepository.AddAsync(employee);
                
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created !!";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);


        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id :{id} is not found" });

            return View(viewName, employee);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await _unitOfWork.DepratmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Department With Id :{id} is not found" });
            var employeeDto = new CreateEmployeeDto()
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                CreateAt = employee.CreateAt,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                Phone = employee.Phone,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            };
            var dto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //  هتمنع اي تول خارجيه انها تبعت ريكويست علي الاند بوينت بتاعتي
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model, string viewNAme = "Edit")
        {
            if (ModelState.IsValid) // Server Side Validation
            {

                if (model.ImageName is not null && model.Image is not null)
                { 
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                }

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }

                //if (id != model.Id) return BadRequest(); //400
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    DepartmentId = model.DepartmentId

                };
                     _unitOfWork.EmployeeRepository.Update(employee);
                    var count = await _unitOfWork.CompleteAsync();

                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
            }
            return View(viewNAme, model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken] // يفضل اننا نستخدمه مع اي اكشن شغال ب الفيرب بوست
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{
        //    if (ModelState.IsValid) // Server Side Validation
        //    {
        //        {
        //            var department = new Department()
        //            {
        //                Id = id,
        //                Name = model.Name,
        //                Code = model.Name,
        //                CreateAt = model.CreateAt,
        //            };
        //            var count = _departmentRepository.Update(department);
        //            if (count > 0)
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }

        //        }

        //    }

        //    return View(model);
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //  هتمنع اي تول خارجيه انها تبعت ريكويست علي الاند بوينت بتاعتي
        public async Task<IActionResult> Delete([FromRoute] int id, Employee model)
        {
           // if (ModelState.IsValid) // Server Side Validation
           // {
                if (id != model.Id) return BadRequest(); //400
                {
                     _unitOfWork.EmployeeRepository.Delete(model);
                     var count = await _unitOfWork.CompleteAsync();
                     if (count > 0)
                     {
                        if (model.ImageName is not null)
                        { 
                        DocumentSettings.DeleteFile(model.ImageName, "images");
                        }
                        return RedirectToAction(nameof(Index));
                     }

                }

           // }

            return View(model);
        }
    }
}
