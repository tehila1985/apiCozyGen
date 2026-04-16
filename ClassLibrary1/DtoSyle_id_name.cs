using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record DtoSyle_id_name(
        int StyleId,
        string Name,
        string Description,
        string ImageUrl
    );
}