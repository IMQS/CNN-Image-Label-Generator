using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeightEditor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    
    
    public partial class Window1 : Window
    {
        
        
        public Window1()
        {
            InitializeComponent();
        }

        private void btGet_Click(object sender, RoutedEventArgs e)
        {
            List<string> tp = DataSetWriter.GetClassList(ImageSet.imageSets);
            listBox1.Items.Clear();
            for (int i = 0; i < tp.Count; i++)
            {
                listBox1.Items.Add(tp[i]);
            }
        }

        private void btLabels_Click(object sender, RoutedEventArgs e)
        {            
            listBox1.Items.Clear();
            getLabels(ImageSet.imageSets[0]);
            //saveCurrent();
        }
        private void getLabels(ImageSet imageSet)
        {
            List<string> tp = DataSetWriter.GetClassList(ImageSet.imageSets);
            for (int i = 0; i < imageSet.classes.Count; i++)
            {
                int id = tp.IndexOf(imageSet.classes[i]);
                Point start = new Point(imageSet.rects[i].Margin.Left,imageSet.rects[i].Margin.Top);
                Point end = new Point(imageSet.rects[i].Margin.Right,imageSet.rects[i].Margin.Bottom);
                listBox1.Items.Add(DataSetWriter.GetLine(id,start,end,imageSet.image.Source.Width,imageSet.image.Source.Height));
            }

        }

        private void btWriteLabels_Click(object sender, RoutedEventArgs e)
        {
            DataSetWriter.WriteLabels();
            listBox1.Items.Clear();
            listBox1.Items.Add("Writing Labels Complete");
        }

        private void btFilePaths_Click(object sender, RoutedEventArgs e)
        {
            List<string> tp = DataSetWriter.GetFilePaths();
            for (int i = 0; i < ImageSet.imageSets.Count; i++)
            {
                listBox1.Items.Add(tp[i]);
            }
        }
    }
}
