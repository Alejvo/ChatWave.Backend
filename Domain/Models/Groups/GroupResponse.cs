using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Groups
{
    public record GroupResponse(
        string Id,
         string Name,
         string Description,
         int Members
        );
}
