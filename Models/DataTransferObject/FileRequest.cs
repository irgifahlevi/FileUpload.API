using FileUpload.API.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FileUpload.API.Models.DataTransferObject
{
    public class FileRequest
    {
        [StringLength(20)]
        [Unicode(false)]
        public string ImageCode { get; set; } = null!;

        [StringLength(20)]
        [Unicode(false)]
        public string ImageName { get; set; } = null!;
    }
}
