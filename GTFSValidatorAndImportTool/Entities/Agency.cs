using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTFSValidatorAndImportTool.Entities
{
    public class Agency
    {
        public string Name { get; set; }
        public string ContextName { get; set; }
        public string EmailAddress { get; set; }
        public string ServerIpAddress { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
