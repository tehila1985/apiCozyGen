using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public record DtoCategory_Name_Id(
        int CategoryId,
        string Name
    );
}
