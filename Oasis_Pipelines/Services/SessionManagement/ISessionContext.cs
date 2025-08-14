using Oasis_Pipelines.Services.BlockCalculation;
using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;

namespace Oasis_Pipelines.Services.SessionManagement;

public interface ISessionContext
{
    public string SessionTitle { get; set; }
    IConnectionManager ConnectionManager { get; }
    IBlockManager BlockManager { get; }
    IBlockCalculation BlockCalculation { get; }
}