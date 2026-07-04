using System.Runtime.Versioning;

namespace Oasis_Pipelines.Shared;

public interface IDialogHostController
{
    Task<object?> CreateAndShowDialog<TDialogViewModel>()
        where TDialogViewModel : IDialogViewModel;
}
