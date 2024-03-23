using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FileUpload.API.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace FileUpload.API.Models;


[Table("FileHandling")]
public partial class FileHandling : BaseModel
{

    [StringLength(20)]
    [Unicode(false)]
    public string ImageCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string ImageName { get; set; } = null!;

    
}
