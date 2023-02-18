using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeFormatter.Domain.Entities
{
    public class Keywords : BaseEntity
    {
        public required string UserId { get; set; }
        public required List<string> Keyword { get; set; }
    }
}
