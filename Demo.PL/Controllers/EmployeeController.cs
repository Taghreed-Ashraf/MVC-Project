using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entites;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            var employees = Enumerable.Empty<Employee>();


            if (string.IsNullOrEmpty(SearchValue))
                employees = await _unitOfWork._EmployeeRepository.GetAll();
            else
                employees = _unitOfWork._EmployeeRepository.GetEmpByName(SearchValue);

            var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployees);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork._DepartmentRepository.GetAll();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                if (employeeVM.Image != null)
                    employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");

                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork._EmployeeRepository.Add(MappedEmployee);
                TempData["Message"] = "Employee Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }



        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var employee = await _unitOfWork._EmployeeRepository.GetById(id.Value);
            if (employee == null)
                return NotFound();


            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, MappedEmployee);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            ViewBag.Departments = await _unitOfWork._DepartmentRepository.GetAll();
            return await Details(Id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? Id, EmployeeViewModel employeeVM)
        {
            if (Id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.Image != null)
                    {
                        if (employeeVM.ImageName != null)
                        {
                            DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                            employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                        }
                        else
                        {
                            employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                        }

                    }

                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    int count = await _unitOfWork._EmployeeRepository.Update(MappedEmployee);
                    TempData["Message"] = "Employee Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(employeeVM);
                }
            }
            return View(employeeVM);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            return await Details(Id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? Id, EmployeeViewModel employeeVM)
        {
            if (Id != employeeVM.Id)
                return BadRequest();
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                int count = await _unitOfWork._EmployeeRepository.Delete(MappedEmployee);
                if (count > 0 && employeeVM.ImageName != null)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");

                TempData["Message"] = "Employee Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(employeeVM);
            }
        }
    }
}
