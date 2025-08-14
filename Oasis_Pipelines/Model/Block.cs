using System.Collections.ObjectModel;
using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Operations;

namespace Oasis_Pipelines.Model;

public class Block : IPipelineObject
{
    public string BlockTitle { get; set; }
    public BlockOperation Operation { get; set; }
    public ObservableCollection<Connection> UpstreamConnections { get; init; } = [];
    public ObservableCollection<Connection> DownstreamConnections { get; init; } = [];

    public Block(string blockTitle, BlockOperation operation)
    {
        BlockTitle = blockTitle;
        Operation = operation;
    }
}
