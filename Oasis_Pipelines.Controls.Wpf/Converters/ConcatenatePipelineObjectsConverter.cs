using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Classes;

namespace Oasis_Pipelines.Controls.Wpf.Converters;

[ValueConversion(typeof(object[]), typeof(IPipelineObject))]
public class ConcatenatePipelineObjectsConverter : IMultiValueConverter
{
    private readonly ObservableCollection<object> _combinedCollection = [];

    /// <inheritdoc />
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [IEnumerable<Block> blocks, IEnumerable<IConnection> connections, ..])
            return _combinedCollection;

        object[] combinedInputs = [..blocks, ..connections];

        foreach (object input in _combinedCollection.Except(combinedInputs).ToArray())
            _combinedCollection.Remove(input);
        
        foreach (object input in combinedInputs.Except(_combinedCollection).ToArray())
            _combinedCollection.Add(input);
        
            
        // for (int i = _combinedCollection.Count - 1; i >= 0; i--)
        //     if (!combinedInputs.Contains(_combinedCollection[i]))
        //         _combinedCollection.RemoveAt(i);
        //
        // foreach (object input in combinedInputs)
        //     if(!_combinedCollection.Contains(input))
        //         _combinedCollection.Add(input);

        return _combinedCollection;
    }

    /// <inheritdoc />
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}