using System;

namespace Pottencial.Domain.Dto
{
    public class CostumerPhysicalPersonDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal IncomeAmount { get; set; }
        public long Cpf { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
