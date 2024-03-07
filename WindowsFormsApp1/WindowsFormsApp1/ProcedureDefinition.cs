using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ProcedureDefinition
    {
        public virtual int Id { get; set; }
        public virtual string DatabaseName { get; set; }
        public virtual string SchemaName { get; set; }
        public virtual string ProcedureName { get; set; }
        public virtual string ParamName { get; set; }
        public virtual string ParamType { get; set; }
        public virtual int OrderValue { get; set; }
        public virtual int Status { get; set; }
        public virtual int InOutOrder { get; set; }
    }
}
