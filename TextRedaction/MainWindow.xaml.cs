using System;
using System.Collections.Generic;
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
        RichTextBox RichTextList = new RichTextBox();
        string Now = "";

        
        public MainWindow()
        {
            InitializeComponent();

            FamilyChoice.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FamilyChoiceMenu.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);

            RichTextList.Name = TextEl.Name;
        }

        private void ItemNew_Click(object sender, RoutedEventArgs e)
        {
            Now = "New";
            Products.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = Now }, 
                Content = RichTextList
            });
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

        private void LanguageEng_Click(object sender, RoutedEventArgs e)
        {
            ItemFile.Header = "File";
            ItemNew.Header = "New";
            ItemOpen.Header = "Open";
            ItemSave.Header = "Save";
            exitMenu.Header = "Exit";
            ItemEdit.Header = "Edit";
            ItemCut.Header = "Cut";
            ItemCopy.Header = "Copy";
            ItemPaste.Header = "Paste";
            ItemFormat.Header = "Format";
            ItemAlignment.Header = "Alignment";
            ItemLeft.Header = "Left";
            ItemCenter.Header = "Center";
            ItemRight.Header = "Right";
            ItemJustify.Header = "Justify";
            TextColor.Text = "Color: ";
            TextBlack.Text = "Black";
            TextBlue.Text = "Blue";
            TextGreen.Text = "Green";
            TextOrange.Text = "Orange";
            TextPurple.Text = "Purple";
            TextRed.Text = "Red";
            TextYellow.Text = "Yellow";
            TextCol.Text = "Col: ";
            TextRow.Text = "Row: ";
            fontMenu.Header = "Font";
        }

        private void LanguageRu_Click(object sender, RoutedEventArgs e)
        {
            ItemFile.Header = "Файл";
            ItemNew.Header = "Новый файл";
            ItemOpen.Header = "Открыть";
            ItemSave.Header = "Сохранить";
            exitMenu.Header = "Закрыть";
            ItemEdit.Header = "Изменить";
            ItemCut.Header = "Вырезать";
            ItemCopy.Header = "Скопировать";
            ItemPaste.Header = "Вставить";
            ItemFormat.Header = "Формат";
            ItemAlignment.Header = "Расположение";
            ItemLeft.Header = "Налево";
            ItemCenter.Header = "Центр";
            ItemRight.Header = "Направо";
            ItemJustify.Header = "Выровнить";
            TextColor.Text = "Цвет: ";
            TextBlack.Text = "Черный";
            TextBlue.Text = "Синий";
            TextGreen.Text = "Зеленый";
            TextOrange.Text = "Оранжевый";
            TextPurple.Text = "Фиолетовый";
            TextRed.Text = "Красный";
            TextYellow.Text = "Желтый";
            TextCol.Text = "Столбец: ";
            TextRow.Text = "Строка: ";
            fontMenu.Header = "Шрифт";
        }

    }
}
