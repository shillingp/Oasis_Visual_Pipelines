using System.Globalization;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Transforms.Strings;

[BlockOperationGroup(BlockOperationType.Text, BlockOperationGrouping.Transforms)]
public sealed class ChangeCaseOperation : BlockOperation
{
    public override string OperationTitle => "Change Text Case";

    public TextCase TextCaseChoice { get; set; } = TextCase.LowerCase;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            if (inputOperations.Length == 0 || inputOperations[0].Result() is not string textInput)
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