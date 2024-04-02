using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;
using System.IO;
using System.Linq;

namespace CarpToolkit.Pages
{
    public partial class MarkdownPageView : UserControl
    {
        public MarkdownPageView()
        {
            InitializeComponent();

            DragDropRectangle.AddHandler(DragDrop.DragOverEvent, DropOver);
            DragDropRectangle.AddHandler(DragDrop.DragLeaveEvent, DropLeave);
            DragDropRectangle.AddHandler(DragDrop.DropEvent, Drop);
        }

        private void DropLeave(object? sender, DragEventArgs e)
        {
            DragDropRectangle.Opacity = 0;
        }

        private void DropOver(object? sender, DragEventArgs e)
        {
            DragDropRectangle.Opacity = 0.4;
        }

        private void Drop(object? sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.Files))
            {
                var files = e.Data.GetFiles();
                if (files != null)
                {
                    // Handle the dropped files here
                    var file = files.SingleOrDefault();

                    string content = File.ReadAllText(file.Path.LocalPath);
                    MarkdownTextBox.Text = content;
                }
            }
        }
    }
}
