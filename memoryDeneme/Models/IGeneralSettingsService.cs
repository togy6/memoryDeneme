using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memoryDeneme.Models
{
    public interface IGeneralSettingsService
    {
        Task<GeneralSettings> GetSettingsAsync();
    }
}
