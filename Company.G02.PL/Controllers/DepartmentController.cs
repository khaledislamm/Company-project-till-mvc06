using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Reposatiories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Company.G02.PL.Controllers
{
    // MVC Controller
    public class DepartmentController : Controller
    {
        //private readonly IDepratmentRepository _departmentRepository; // NULL
        private readonly IUnitOfWork _unitOfWork;

        // ASK CLR Create Object From DepartmentRepository
        public DepartmentController(/*IDepratmentRepository departmentRepository*/IUnitOfWork unitOfWork)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet] // GET: /Department/Index
        public async Task<IActionResult> Index()
        {
             var departments = await _unitOfWork.DepratmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
                await _unitOfWork.DepratmentRepository.AddAsync(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0) 
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);


        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName="Details")
        {
            if(id is null) return BadRequest("Invalid Id"); //400
            var department = await _unitOfWork.DepratmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id :{id} is not found" });
            return View(viewName, department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = await _unitOfWork.DepratmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id :{id} is not found" });
            var departmentDto = new CreateDepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt,
            };
            return View(departmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //  هتمنع اي تول خارجيه انها تبعت ريكويست علي الاند بوينت بتاعتي
        public async Task<IActionResult> Edit([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                //if (id != department.Id) return BadRequest(); //400
                var department = new Department()
                {
                    Id = id,
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
                {
                     _unitOfWork.DepratmentRepository.Update(department);
                     var count = await _unitOfWork.CompleteAsync();
                     if (count > 0)
                     {
                        return RedirectToAction(nameof(Index));
                     }

                }

            }

            return View(model);
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
            //if (id is null) return BadRequest("Invalid Id"); //400

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id :{id} is not found" });

            return await Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //  هتمنع اي تول خارجيه انها تبعت ريكويست علي الاند بوينت بتاعتي
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
           // if (ModelState.IsValid) // Server Side Validation
           // {
                if (id != department.Id) return BadRequest(); //400
                {
                    _unitOfWork.DepratmentRepository.Delete(department);
                    var count = await _unitOfWork.CompleteAsync();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }

           // }

            return View(department);
        }

    }
}
