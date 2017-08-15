using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASNC_GUI.Extensions
{
    public static class RichTextBoxExtensions
    {
        public static void WriteErrorLine(this RichTextBox richTextBox, string text, bool updateLastLine = false)
        {
            richTextBox.WritePrefixedLine(text, Color.Red, "ERROR", updateLastLine);
        }

        public static void WriteInfoLine(this RichTextBox richTextBox, string text, bool updateLastLine = false)
        {
            richTextBox.WritePrefixedLine(text, Color.DarkCyan, "INFO", updateLastLine);
        }

        public static void WriteDoneLine(this RichTextBox richTextBox, string text, bool updateLastLine = false)
        {
            richTextBox.WritePrefixedLine(text, Color.Green, "DONE", updateLastLine);
        }

        public static void WritePrefixedLine(this RichTextBox richTextBox, string text, Color color, string prefix, bool updateLastLine = false)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke((Action)(() => richTextBox.WritePrefixedLine(text, color, prefix, updateLastLine)));
                return;
            }

            if (updateLastLine &&
                richTextBox.Lines.Length >= 2)
            {
                // HACK - SelectedText doesn't work if ReadOnly is True....
                richTextBox.ReadOnly = false;
                var line = richTextBox.Lines.Length - 2;
                richTextBox.SelectionStart = richTextBox.GetFirstCharIndexFromLine(line);
                richTextBox.SelectionLength = richTextBox.Lines[line].Length + 1;
                richTextBox.SelectedText = String.Empty;
                richTextBox.ReadOnly = true;
            }

            richTextBox.AppendText("[");
            richTextBox.AppendText(prefix, color);
            richTextBox.AppendText($"] {text}{Environment.NewLine}");
        }

        public static void AppendText(this RichTextBox richTextBox, string text, Color color)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;

            richTextBox.SelectionColor = color;
            richTextBox.AppendText(text);
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }
    }
}
