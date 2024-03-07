using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ProcedureDefinitionMapping : ClassMap<ProcedureDefinition>
    {
        public ProcedureDefinitionMapping()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.DatabaseName);
            Map(x => x.SchemaName);
            Map(x => x.ProcedureName);
            Map(x => x.ParamName);
            Map(x => x.ParamType);
            Map(x => x.OrderValue);
            Map(x => x.Status);
            Map(x => x.InOutOrder);

        }


    }
}
