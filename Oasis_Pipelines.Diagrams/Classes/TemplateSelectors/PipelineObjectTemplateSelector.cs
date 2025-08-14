using System.Windows;
using System.Windows.Controls;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Diagrams.Classes.TemplateSelectors;

public class PipelineObjectTemplateSelector : DataTemplateSelector
{
    public DataTemplate BlockTemplate { get; set; }
    public DataTemplate ConnectionTemplate { get; set; }
    
    /// <inheritdoc />
    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        return item switch
        {
            Block => BlockTemplate,
            Connection => ConnectionTemplate,
            _ => throw new NotImplementedException(),
        };
    }
}