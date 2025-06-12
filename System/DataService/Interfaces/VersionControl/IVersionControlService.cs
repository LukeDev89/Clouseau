using DataModel.Response.VersionControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interfaces.VersionControl
{
    public interface IVersionControlService
    {
        Task<List<VersionControlProjectModel>> GetVersionControlAsync();

        Task<List<WorkItemProjectModel>> GetWorkItemsAsync();
    }
}
