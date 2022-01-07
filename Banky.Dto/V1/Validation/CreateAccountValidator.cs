

using System.Collections.Generic;

namespace Banky.Dto.V1.Validation
{
    public class CreateAccountValidator
    {
        private ValidationDto Validitator { get; }
        public CreateAccountValidator()
        {
            Validitator = new ValidationDto();
        }
        public CreateAccountValidator GetValidation(string name)
        {
            if (string.IsNullOrEmpty(name))
                Validitator.Errors.Add(new Error { Message = "a unique account number is required" });
            return this;
        }
        public bool HasError => Validitator.Errors.Count > 0;
        public List<Error> Errors => Validitator.Errors;
    }
}
