using System;

namespace Banky.Dto.V1.Login.Request
{
    public class UpdateAccountRequest
    {
        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PrimaryContactNumber { get; set; }
        public int? Timezone { get; set; }
    }
}
