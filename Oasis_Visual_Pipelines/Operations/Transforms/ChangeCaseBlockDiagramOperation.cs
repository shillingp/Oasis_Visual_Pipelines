﻿using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Globalization;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(Enums.BlockOperationType.Text, Enums.BlockOperationGroup.Transforms)]
    public class ChangeCaseBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 1;
        public string OperationTitle => "Change Text Case";

        public TextCase TextCaseChoice { get; set; } = TextCase.LowerCase;

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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