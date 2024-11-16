using CommunityToolkit.Mvvm.Messaging;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes.Messages;
using Oasis_Visual_Pipelines.Interfaces;
using System.ComponentModel;
using System.Reflection;

namespace Oasis_Visual_Pipelines.Classes
{
    public class BaseBlockDiagramOperation : INotifyPropertyChanged, IBlockDiagramOperation
    {
        public virtual int MaxInputs => 0;
        public virtual int MaxOutputs => int.MaxValue;
        public virtual string OperationTitle => "";

        private readonly Dictionary<string, DateTime> lastUpdateTime = new Dictionary<string, DateTime>();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            PropertyChanged?.Invoke(this, eventArgs);

            DoNotReflowOnPropertyChangedAttribute? doNotReflowAttribute = this.GetType()
                .GetProperty(eventArgs.PropertyName!)
                ?.GetCustomAttribute<DoNotReflowOnPropertyChangedAttribute>();

            if (doNotReflowAttribute is not null)
            {
                if (lastUpdateTime.TryGetValue(eventArgs.PropertyName!, out DateTime lastCallTime))
                {
                    if (DateTime.Now - lastCallTime < TimeSpan.FromMilliseconds(10))
                    {
                        lastUpdateTime[eventArgs.PropertyName!] = DateTime.Now;
                        return;
                    }
                }

                lastUpdateTime[eventArgs.PropertyName!] = DateTime.Now;
            }

            WeakReferenceMessenger.Default.Send<BlockControlPropertyChangedMessage>();
        }

        public virtual BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return BlockOperationResult.NullOperation;
        }
    }
}
