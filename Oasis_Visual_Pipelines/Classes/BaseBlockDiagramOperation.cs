using CommunityToolkit.Mvvm.Messaging;
using Oasis_Visual_Pipelines.Classes.Messages;
using Oasis_Visual_Pipelines.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Oasis_Visual_Pipelines.Classes
{
    public class BaseBlockDiagramOperation : INotifyPropertyChanged, IBlockDiagramOperation
    {
        public virtual int MaxInputs => 0;
        public virtual int MaxOutputs => int.MaxValue;
        public virtual string OperationTitle => "";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(name, new PropertyChangedEventArgs(name));

            WeakReferenceMessenger.Default.Send<BlockControlPropertyChangedMessage>();
        }

        public virtual BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return BlockOperationResult.NullOperation;
        }
    }
}
