using CommunityToolkit.Mvvm.Input;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Controls;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Oasis_Visual_Pipelines.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Block<T> : Block
    {
        public T? Data { get; set; }

        internal Block(T data)
        {
            Data = data;
        }

        internal Block(Point position, BlockDiagramControl blockDiagram)
            : base(position, blockDiagram)
        {
            Data = Block<T>.TryToCreateDataObject();
        }

        private static T? TryToCreateDataObject()
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch
            {
                try
                {
                    return default;
                }
                catch
                {
                    throw;
                }
            }
        }

        public BlockOperationResult CalculateFlowPathResult()
        {
            if (Data is not IBlockDiagramOperation blockOperation)
                return new BlockOperationResult(additionalInputs => Data);

            BlockOperationResult[] leftOperations = LeftConnections
                .Select(connection => connection.LeftBlock)
                .Cast<dynamic>()
                .Select(block => block.CalculateFlowPathResult())
                .Cast<BlockOperationResult>()
                .ToArray();

            return blockOperation.ExecuteOperation(leftOperations);
        }
    }

    [AddINotifyPropertyChangedInterface]
    public class Block : IBlockDiagramObject<BlockControl>, IBlockDiagramObject
    {
        public BlockDiagramControl? BlockDiagram;

        public Point Position { get; set; }

        public string? Title { get; set; }

        public BlockControl CanvasElement { get; protected set; }

        public ObservableSet<Connection> LeftConnections { get; set; } = new ObservableSet<Connection>();

        public ObservableSet<Connection> RightConnections { get; set; } = new ObservableSet<Connection>();

        internal Block()
        {
            CanvasElement = CreateDefaultCanvasElement();
        }

        internal Block(Point position, BlockDiagramControl blockDiagram)
            : this()
        {
            BlockDiagram = blockDiagram;

            Position = new Point
            {
                X = Math.Max(0, position.X - (CanvasElement.Width / 2)),
                Y = Math.Max(0, position.Y - (CanvasElement.MinHeight / 2))
            };
        }

        #region Commands
        public ICommand RemoveBlockCommand => new RelayCommand(() =>
        {
            foreach (Connection connection in LeftConnections.Concat(RightConnections).ToList())
                RemoveConnectionTo(connection);

            BlockDiagram!.BlockDiagramItems.Remove(this);
        });
        #endregion

        #region Methods
        internal object[] GetLeftInputs()
        {
            return LeftConnections
                .Select(connection => connection.LeftBlock)
                .Cast<dynamic>()
                .Select(block => block.CalculateOperationalResult())
                .Cast<object>()
                .ToArray();
        }

        internal IBlockDiagramOperation[] GetLeftOperations()
        {
            return LeftConnections
                .Select(connection => connection.LeftBlock)
                .Cast<dynamic>()
                .Select(block => block.Data)
                .Cast<IBlockDiagramOperation>()
                .ToArray();
        }

        public BlockControl CreateDefaultCanvasElement()
        {
            return new BlockControl(this);
        }

        #region Connections
        public Connection ConnectTo(Block secondBlock)
        {
            Connection newConnection = new Connection(this, secondBlock);

            this.RightConnections.Add(newConnection);
            secondBlock.LeftConnections.Add(newConnection);

            BlockDiagram!.BlockDiagramItems.Add(newConnection);

            Application.Current.Dispatcher.Invoke(
                newConnection.LeftBlock!.RedrawAnyConnections,
                DispatcherPriority.Render);
            Application.Current.Dispatcher.Invoke(
                newConnection.RightBlock!.RedrawAnyConnections,
                DispatcherPriority.Render);

            return newConnection;
        }

        public void RemoveConnectionTo(Block secondBlock)
        {
            Connection? foundConnection = LeftConnections
                .Concat(RightConnections)
                .FirstOrDefault(connection => connection.LeftBlock == secondBlock || connection.RightBlock == secondBlock);
            if (foundConnection is null) return;

            RemoveConnectionTo(foundConnection);
        }

        public void RemoveConnectionTo(Connection connectionToRemove)
        {
            connectionToRemove.LeftBlock!.RightConnections.Remove(connectionToRemove);
            connectionToRemove.RightBlock!.LeftConnections.Remove(connectionToRemove);

            BlockDiagram!.BlockDiagramItems.Remove(connectionToRemove);

            Application.Current.Dispatcher.Invoke(
                connectionToRemove.LeftBlock.RedrawAnyConnections,
                DispatcherPriority.Render);
            Application.Current.Dispatcher.Invoke(
                connectionToRemove.RightBlock.RedrawAnyConnections,
                DispatcherPriority.Render);
        }

        internal bool IsConnectedTo(Block secondBlock)
        {
            if (secondBlock is null) throw new ArgumentNullException(nameof(secondBlock));

            return LeftConnections.Any(connection => connection.LeftBlock == secondBlock)
                || RightConnections.Any(connection => connection.RightBlock == secondBlock);
        }
        #endregion

        #region Graphics
        internal void RedrawAnyConnections()
        {
            foreach (Connection connection in LeftConnections.Concat(RightConnections).ToList())
                connection.UpdateCanvasElementVector();
        }

        internal ConnectorNodeControl? GetConnectionNode(Connection connection)
        {
            return CanvasElement.FindConnectorNodeFromConnection(connection);
        }
        #endregion

        public static bool IsGenericBlock(Block block)
        {
            return block
                .GetType()
                .GetInterfaces()
                .Any(blockInterface => blockInterface.IsGenericType
                    && blockInterface.GetGenericTypeDefinition() == typeof(Block<>));
        }
        #endregion
    }
}
