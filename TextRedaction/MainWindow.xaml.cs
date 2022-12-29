using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TextRedaction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FamilyChoice.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FamilyChoiceMenu.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            TextEl.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
        }

        private void btnCenter_Click(object sender, RoutedEventArgs e)
        {
            TextEl.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            TextEl.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
        }

        private void btnJustify_Click(object sender, RoutedEventArgs e)
        {
            TextEl.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
        }

        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            var weightProperty = (FontWeight)TextEl.Selection.GetPropertyValue(FontWeightProperty);

            if (weightProperty == FontWeights.Bold)
            {
                TextEl.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                TextEl.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
            }
        }

        private void btnItaly_Click(object sender, RoutedEventArgs e)
        {
            var weightProperty = (FontStyle)TextEl.Selection.GetPropertyValue(FontStyleProperty);

            if (weightProperty == FontStyles.Italic)
            {
                TextEl.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                TextEl.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
            }
        }

        private void btnUnderline_Click(object sender, RoutedEventArgs e)
        {
            var weightProperty = (TextDecorationCollection)TextEl.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            if (weightProperty == TextDecorations.Underline)
            {
                TextEl.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                TextEl.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        private void familyChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FamilyChoice.SelectedItem != null)
                TextEl.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FamilyChoice.SelectedItem);

            if (FamilyChoiceMenu.SelectedItem != null)
                TextEl.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FamilyChoiceMenu.SelectedItem);
        }

        private void sizeChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            TextEl.Selection.ApplyPropertyValue(Inline.FontSizeProperty, selectedItem.Content);
        }

        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            TextEl.Cut();
        }
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            TextEl.Copy();
        }
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            TextEl.Paste();
        }

        private void TextEl_GotFocus(object sender, RoutedEventArgs e)
        {
            TextPointer tp1 = TextEl.Selection.Start.GetLineStartPosition(0);
            TextPointer tp2 = TextEl.Selection.Start;

            int column = tp1.GetOffsetToPosition(tp2);
            ColTxt.Text = column.ToString();
        }

        private void TextEl_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextPointer tp1 = TextEl.Selection.Start.GetLineStartPosition(0);
            TextPointer tp2 = TextEl.Selection.Start;

            int column = tp1.GetOffsetToPosition(tp2);

            int someBigNumber = int.MaxValue;
            int lineMoved, currentLineNumber;
            TextEl.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);
            currentLineNumber = -lineMoved;
            currentLineNumber++;
            ColTxt.Text = column.ToString();
            RowTxt.Text = currentLineNumber.ToString();
        }

        private void exitMenu_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void alignmentLeftMenu_Click(object sender, RoutedEventArgs e)
        {
            btnLeft_Click(sender, e);
        }
    }
}
