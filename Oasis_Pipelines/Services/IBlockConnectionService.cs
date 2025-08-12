using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services;

public interface IBlockConnectionService
{
    Connection AddConnection(Block leftSide, Block rightSide);
    void RemoveConnection(Connection connection);
}