using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CarpToolkit.Helpers;
using CarpToolkit.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Threading.Tasks;

namespace CarpToolkit.Pages
{
    public partial class MarkdownPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _markdown = "#Hello, Markdown!";

        [RelayCommand]
        private async Task OpenFilePickerAsync()
        {
            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(ClipboardHelper.Owner);

            // Start async operation to open the dialog.
            var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Choose Markdown File",
                FileTypeFilter = new[] { TextAll, FilePickerFileTypes.TextPlain },
                AllowMultiple = false,
            });

            if (file.Count == 1 && file != null)
            {
                // Open reading stream from the first file.
                await using var stream = await file[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream);
                // Reads all the content of file as a text.
                var fileContent = await streamReader.ReadToEndAsync();

                Markdown = fileContent;
            }
        }
        [RelayCommand]
        private async Task OpenFileSavingDialogAsync()
        {
            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(ClipboardHelper.Owner);

            // Start async operation to open the dialog.
            var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Save Text File",
                SuggestedFileName = "markdown.md",
            });

            if (file is not null)
            {
                // Open writing stream from the file.
                await using var stream = await file.OpenWriteAsync();
                using var streamWriter = new StreamWriter(stream);
                // Write some content to the file.
                await streamWriter.WriteLineAsync(Markdown);
            }
        }
        public static FilePickerFileType TextAll { get; } = new("all txt files")
        {
            Patterns = new[] { "*.txt", "*.md" },
            AppleUniformTypeIdentifiers = new[] { "text.md" },
            MimeTypes = new[] { "text/plain" }
        };

        [RelayCommand]
        private void Exit()
        {
            CurrentPageChanged?.Invoke(this, new ToolsViewModel());
        }
    }
}
