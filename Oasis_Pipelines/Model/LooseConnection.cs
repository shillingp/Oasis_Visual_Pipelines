using System.Drawing;
using Oasis_Pipelines.Interfaces;

namespace Oasis_Pipelines.Model;

public class LooseConnection : IPipelineObject, IConnection
{
    public PointF Position { get; set; }
    public PointF EndPosition { get; set; }
    
    public LooseConnection(PointF startingPosition)
    {
        Position = startingPosition;
        EndPosition = startingPosition;
    }
    
    public LooseConnection(PointF position, PointF endPosition)
    {
        Position = position;
        EndPosition = endPosition;
    }
}