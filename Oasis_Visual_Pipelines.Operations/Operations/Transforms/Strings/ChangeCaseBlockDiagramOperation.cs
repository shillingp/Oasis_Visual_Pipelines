using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using System.Globalization;

namespace Oasis_Visual_Pipelines.Operations.Transforms.Strings
{
    [BlockOperationGroup(Enums.BlockOperationType.Text, Enums.BlockOperationGrouping.Transforms)]
    public sealed class ChangeCaseBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Change Text Case";

        public TextCase TextCaseChoice { get; set; } = TextCase.LowerCase;

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                if (!inputOperations.Any() || inputOperations[0].Result() is not string textInput)
                    return null;

                return TextCaseChoice switch
                {
                    TextCase.LowerCase => CultureInfo.CurrentCulture.TextInfo.ToLower(textInput),
                    TextCase.UpperCase => CultureInfo.CurrentCulture.TextInfo.ToUpper(textInput),
                    TextCase.TitleCase => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textInput),
                    _ => textInput
                };
            });
        }
    }

    public enum TextCase
    {
        LowerCase,
        UpperCase,
        TitleCase
    }
}
