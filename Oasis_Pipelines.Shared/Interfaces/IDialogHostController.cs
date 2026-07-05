namespace Oasis_Pipelines.Shared.Interfaces;

public interface IDialogHostController
{
    Task<object?> CreateAndShowDialog<TDialogViewModel>()
        where TDialogViewModel : IDialog;
}