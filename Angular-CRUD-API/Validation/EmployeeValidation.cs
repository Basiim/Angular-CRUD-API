using Angular_CRUD_API.Model;
using FluentValidation;

namespace Angular_CRUD_API.Validation
{
    public class EmployeeValidation: AbstractValidator<Employee>
    {
        public EmployeeValidation() 
        { 
            RuleFor(x => x.name).NotEmpty().NotNull();
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.salary).NotEmpty().NotNull().NotEqual(0);
            RuleFor(x => x.department).NotEmpty();
        }
    }
}
