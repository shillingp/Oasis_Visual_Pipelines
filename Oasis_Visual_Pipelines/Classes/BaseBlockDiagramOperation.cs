using CommunityToolkit.Mvvm.Messaging;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes.Messages;
using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Oasis_Visual_Pipelines.Classes
{
    public class BaseBlockDiagramOperation : INotifyPropertyChanged, IBlockDiagramOperation
    {
        public virtual int MaxInputs => 0;
        public virtual int MaxOutputs => int.MaxValue;
        public virtual string OperationTitle => "";

        readonly Dictionary<(IBlockDiagramOperation Operation, string Property), DateTime> lastUpdateTime
            = new Dictionary<(IBlockDiagramOperation Operation, string Property), DateTime>();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            PropertyChanged?.Invoke(this, eventArgs);

            DoNotReflowOnPropertyChangedAttribute? doNotReflowAttribute = this.GetType()
                .GetProperty(eventArgs.PropertyName!)
                ?.GetCustomAttribute<DoNotReflowOnPropertyChangedAttribute>();

            if (doNotReflowAttribute is not null)
            {
                if (lastUpdateTime.ContainsKey((this, eventArgs.PropertyName!)))
                {
                    DateTime lastCallTime = lastUpdateTime[(this, eventArgs.PropertyName!)];

                    if (DateTime.Now - lastCallTime < TimeSpan.FromMilliseconds(10))
                    {
                        lastUpdateTime[(this, eventArgs.PropertyName!)] = DateTime.Now;
                        return;
                    }
                }
                
                lastUpdateTime[(this, eventArgs.PropertyName!)] = DateTime.Now;
            }

            WeakReferenceMessenger.Default.Send<BlockControlPropertyChangedMessage>();
        }

        public virtual BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return BlockOperationResult.NullOperation;
        }
    }
}
