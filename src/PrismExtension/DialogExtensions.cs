using System;
using Prism.Services.Dialogs;

namespace PrismExtension
{
    /// <summary>
    ///     Extensions for simplify dialogs.
    /// </summary>
    public static class PrismExtension
    {
        /// <summary>
        ///     Return a value from dialog.
        /// </summary>
        /// <param name="dialogResult">Contains <see cref="IDialogParameters"/> from the dialog and the <see cref="ButtonResult"/> of the dialog.</param>
        /// <param name="value">Output parameter.</param>
        /// <typeparam name="TValue">Value as <see cref="TValue"/>.</typeparam>
        /// <returns>Returns a parameter from the <see cref="IDialogResult"/> where key is <see cref="TValue"/> name.</returns>
        public static bool TryGetDialogParameter<TValue>(this IDialogResult dialogResult, out TValue value)
        {
            if (dialogResult.Result == ButtonResult.OK)
                return dialogResult.Parameters.TryGetValue(typeof(TValue).Name, out value);

            value = default;
            return false;
        }

        /// <summary>
        ///     Return a value from dialog.
        /// </summary>
        /// <param name="dialogResult">Contains <see cref="IDialogParameters"/> from the dialog and the <see cref="ButtonResult"/> of the dialog.</param>
        /// <param name="value">Output <see cref="TValue"/>.</param>
        /// <typeparam name="TValue">Value as <see cref="TValue"/></typeparam>
        /// <returns>Returns a parameter from the <see cref="IDialogResult"/> where key is <see cref="TValue"/> name.</returns>
        public static bool TryGetDialogParameter<TValue>(this IDialogParameters dialogResult, out TValue value)
        {
            if (dialogResult.TryGetValue(typeof(TValue).Name, out value)) 
                return true;

            value = default;
            return false;
        }

        /// <summary>
        ///     Create new dialog parameter with key as <see cref="TValue"/> name.
        /// </summary>
        /// <typeparam name="TValue"><see cref="TValue"/> parameter type.</typeparam>
        /// <param name="value">Parameter to add</param>
        /// <returns>Return new dialog parameter with key as <see cref="TValue"/> name and <see cref="TValue"/> as value.</returns>
        public static DialogParameters CreateDialogParameter<TValue>(TValue value) => new() {{typeof(TValue).Name, value}};

        /// <summary>
        ///     Show a modal dialog.
        /// </summary>
        /// <param name="dialogService">Interface to show modal and non-modal dialogs.</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameter">The parameters to pass to the dialog. The parameter key is <see cref="TParameter"/> name.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <typeparam name="TParameter">Value to add</typeparam>
        public static void ShowDialog<TParameter>(this IDialogService dialogService, string name, TParameter parameter,
            Action<IDialogResult> callback) =>
            dialogService.ShowDialog(name, new DialogParameters {{typeof(TParameter).Name, parameter}}, callback);

        /// <summary>
        ///     Create new Ok dialog result with key as <see cref="TValue"/> name and <see cref="TValue"/> as value.
        /// </summary>
        /// <typeparam name="TValue"><see cref="TValue"/> parameter type.</typeparam>
        /// <param name="dialogAware">Interface that provides dialog functions and events to ViewModels.</param>
        /// <param name="value">Parameter to add</param>
        /// <returns>Return new dialog result with button result and new dialog parameter with value with key nameof(TValue)</returns>
        public static DialogResult CreateDialogResult<TValue>(this IDialogAware dialogAware, TValue value) => 
            new(ButtonResult.OK, new DialogParameters {{typeof(TValue).Name, value}});
    }
}