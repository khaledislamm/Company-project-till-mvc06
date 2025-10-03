using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.Dto
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; }


        [Range(18,60, ErrorMessage = "Age Must Be Between 22 and 60")]
        public int? Age { get; set; }


        [EmailAddress]
        public string Email { get; set; }


        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
                           , ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }


        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime CreateAt { get; set; }

        [DisplayName("Department")]
        public int? DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
