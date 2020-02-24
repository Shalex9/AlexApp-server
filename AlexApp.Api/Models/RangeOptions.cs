using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexApp.Api.Models
{
    public class RangeOptions<TFilter>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 200;
        public TFilter Filter { get; set; }
    }
}
