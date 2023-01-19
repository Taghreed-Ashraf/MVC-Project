using Demo.DAL.Entites;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Is Requierd")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Requierd")]
        [MaxLength(50, ErrorMessage = "Max Length Name is 50 Chars")]
        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employee { get; set; } = new HashSet<Employee>();
    }
}
