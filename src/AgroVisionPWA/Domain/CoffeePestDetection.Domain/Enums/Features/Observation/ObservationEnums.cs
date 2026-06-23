using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Enums.Features.Observation;

public static class ObservationEnums
{
    public enum SourceType
    {
        [Description("ia")]
        IA,

        [Description("manual")]
        Manual
    }
}
