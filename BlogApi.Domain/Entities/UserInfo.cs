using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Domain.Entities
{
    public class UserInfo
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? Bio { get; set; }
        public string? FullName { get; set; }
    }
}
