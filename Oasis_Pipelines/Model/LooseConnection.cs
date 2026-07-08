using System.Drawing;
using Oasis_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Pipelines.Model;

[AddINotifyPropertyChangedInterface]
public class LooseConnection : IConnection
{
    public PointF Position { get; set; }
    public PointF EndPosition { get; set; }

    public LooseConnection(PointF startingPosition)
    {
        Position = startingPosition;
        // EndPosition = startingPosition;
    }

    public LooseConnection(PointF position, PointF endPosition)
    {
        Position = position;
        EndPosition = endPosition;
    }

    public void Disconnect()
    {
        // Do nothing
    }
}