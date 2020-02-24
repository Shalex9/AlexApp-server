using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Data.Models
{
    public interface IRemovableEntity
    {
        bool IsRemoved { get; set; }
    }
}
