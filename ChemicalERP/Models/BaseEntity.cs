using System.ComponentModel.DataAnnotations.Schema;

namespace ChemicalERP.Models
{
   
    public class BaseEntity
    {
        public int? SaveOption { get; set; } = 0;
        public int? IdentityValue { get; set; } = 0;
        public int? ErrNo { get; set; } = 0;
        public int? ResultId { get; set; }
        public int? NoofRows { get; set; }
        public string? Message { get; set; }
        public string? ExceptionError { get; set; }
        public int? ErrorNo { get; set; }
        public string? ReturnValue { get; set; }
        public int UserBy { get; set; }
    }
}
