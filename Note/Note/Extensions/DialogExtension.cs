using Note.Views.Dialogs;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;

namespace Note.Extensions
{
    public static class DialogExtension
    {
        public static Task<string> ShowDialogAsync(this IDialogService dialogService, string message)
        {
            var tcs = new TaskCompletionSource<string>();

            var parameter = new DialogParameters
            {
                { "lang", message }
            };

            dialogService.ShowDialog(nameof(SettingListDialog), parameter, (IDialogResult result) =>
            {
                tcs.SetResult(result.Parameters.GetValue<string>("selectedList"));
            });

            return tcs.Task;
        }
    }
}
