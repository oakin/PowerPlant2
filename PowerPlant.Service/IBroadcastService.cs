using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlant.Services
{
    public interface IBroadcastService
    {
        Task BroadcastRequestAndResponse(string request, string response);
    }
}
