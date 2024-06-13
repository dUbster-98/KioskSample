using KioskSample.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSample.Models
{
    public interface IAppContext
    {
        bool IsDisabledUI { get; set; }

        bool IsLogin { get; set; }

        bool IsOpenCase { get; set; }   

        StatusEnum KioskStatus { get; set; }    

        bool IsEatIn { get; set; }

        Order CurrentOrder { get; set; }

        IList<Order> Orders { get; set; }
    }
}
