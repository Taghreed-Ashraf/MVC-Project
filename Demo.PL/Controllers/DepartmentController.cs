using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewData["Message"] = "Hello View Data";

            ViewBag.Message = "Hello View Bag";

            var department = await _unitOfWork._DepartmentRepository.GetAll();
            var mappedDept = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(department);
            return View(mappedDept);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                await _unitOfWork._DepartmentRepository.Add(MappedDept);

                TempData["Message"] = "Department Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();

            var department = await _unitOfWork._DepartmentRepository.GetById(id.Value);

            if (department == null)
                return NotFound();
            var MappedDept = _mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, MappedDept);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            return await Details(Id, "Edit");
        }


        //[HttpPut]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int Id, DepartmentViewModel departmentVM)
        {
            if (Id != departmentVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    await _unitOfWork._DepartmentRepository.Update(MappedDept);

                    TempData["Message"] = "Department Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(departmentVM);
                }
            }
            return View(departmentVM);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            return await Details(Id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? Id, DepartmentViewModel departmentVM)
        {
            if (Id != departmentVM.Id)
                return BadRequest();
            try
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                await _unitOfWork._DepartmentRepository.Delete(MappedDept);
                TempData["Message"] = "Department Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(departmentVM);
            }
        }
    }
}
