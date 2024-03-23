using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FileUpload.API.Enum;

namespace FileUpload.API.Models.Common
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string CreatedBy { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? LastModifiedBy { get; set; }

        public int RowStatus { get; set; }

        public virtual void SetRowStatus(EnumTypes values)
        {
            RowStatus = (short)values;
        }
    }
}
