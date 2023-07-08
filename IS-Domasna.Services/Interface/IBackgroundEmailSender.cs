using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IS_Domasna.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
