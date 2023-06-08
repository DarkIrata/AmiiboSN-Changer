using System;
using System.Windows;
using System.Windows.Controls;

namespace ASNC.Controls
{
    // 2023 - https://stackoverflow.com/a/37608994
    /// <summary>
    /// IFileDragDropTarget Interface
    /// </summary>
    public interface IFileDragDropTarget
    {
        void OnFileDrop(string[] filepaths);
    }

    /// <summary>
    /// FileDragDropHelper
    /// </summary>
    public class FileDragDropHelper
    {
        public static readonly DependencyProperty IsFileDragDropEnabledProperty =
                DependencyProperty.RegisterAttached("IsFileDragDropEnabled", typeof(bool), typeof(FileDragDropHelper), new PropertyMetadata(OnFileDragDropEnabled));

        public static readonly DependencyProperty FileDragDropTargetProperty =
                DependencyProperty.RegisterAttached("FileDragDropTarget", typeof(object), typeof(FileDragDropHelper), null);

        public static bool GetIsFileDragDropEnabled(DependencyObject obj)
            => (bool)obj.GetValue(IsFileDragDropEnabledProperty);

        public static void SetIsFileDragDropEnabled(DependencyObject obj, bool value)
            => obj.SetValue(IsFileDragDropEnabledProperty, value);

        public static bool GetFileDragDropTarget(DependencyObject obj)
            => (bool)obj.GetValue(FileDragDropTargetProperty);

        public static void SetFileDragDropTarget(DependencyObject obj, bool value)
            => obj.SetValue(FileDragDropTargetProperty, value);

        private static void OnFileDragDropEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
            {
                return;
            }

            if (d is Control c)
            {
                c.Drop += OnDrop;
            }
        }

        private static void OnDrop(object sender, DragEventArgs dragEventArgs)
        {
            if (sender is DependencyObject dobj)
            {
                var target = dobj.GetValue(FileDragDropTargetProperty);
                if (target is IFileDragDropTarget dropTarget)
                {
                    if (dragEventArgs.Data.GetDataPresent(DataFormats.FileDrop))
                    {
                        dropTarget.OnFileDrop((string[])dragEventArgs.Data.GetData(DataFormats.FileDrop));
                    }
                }
                else
                {
                    throw new Exception("FileDragDropTarget object must be of type IFileDragDropTarget");
                }
            }


        }
    }
}