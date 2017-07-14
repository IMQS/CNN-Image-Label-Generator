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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WeightEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsDrawing = false;
        Point start;
        Point end;
        Rectangle rectDraw;
        List<Rectangle> rects = new List<Rectangle>();
        Brush[] brushes = new Brush[] {Brushes.Blue,Brushes.Purple,Brushes.Green,Brushes.Yellow,Brushes.Orange,Brushes.Red};
        Image image;
        int selectInd = -1;
        string[] file_paths = new string[] { };
        int image_index = 0;
        //List<ImageSet> imageSets = new List<ImageSet>() { };
        List<string> classesStr = new List<string>() { };


        public MainWindow()
        {
            start = new Point();
            end = new Point();
            InitializeComponent();
            image = new Image();
            rectDraw = new Rectangle();
            rectDraw.Stroke = Brushes.Black;
            rectDraw.StrokeThickness = 1;
            canvasMain.Children.Add(image);
            canvasMain.Children.Add(rectDraw);
        }
        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {

        }
        public void ListboxAdd(string text)
        {
            Label lb = new Label();
            lb.Content = text;
            //classesStr.Add(txtClname.Text);
            lb.Foreground = brushes[listBox1.Items.Count];
            listBox1.Items.Add(lb);
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            Label lb = new Label();
            lb.Content = txtClname.Text;
            classesStr.Add(txtClname.Text);
            lb.Foreground = brushes[listBox1.Items.Count];
            listBox1.Items.Add(lb);
            rects.Add(Default_rect(image));
            //rects.Last();
            //rects.Last().Stroke = brushes[listBox1.Items.Count];
            //rects.Last().StrokeThickness = 1;
            //rects.Last().Width = image.Source.Width;
            //rects.Last().Height = image.Source.Height;
            //rects.Last().Margin = new Thickness(0, 0, image.Source.Width, image.Source.Height);
            canvasMain.Children.Add(rects.Last());
            selectInd = listBox1.SelectedIndex;
            listBox1.SelectedIndex = selectInd; 
            saveCurrent();
        }
        public Rectangle Default_rect(Image image1)
        {
            Rectangle rect_tp = new Rectangle();
            //Label lb = new Label();
            //lb.Content = txtClname.Text;
            //classesStr.Add(txtClname.Text);
            //lb.Foreground = brushes[listBox1.Items.Count];
            //listBox1.Items.Add(lb);// txtClname.Text);
            //rects.Add(new Rectangle());
            //rects.Last();
            rect_tp.Stroke = brushes[listBox1.Items.Count];
            rect_tp.StrokeThickness = 1;
            rect_tp.Width = image1.Source.Width;
            rect_tp.Height = image1.Source.Height;
            rect_tp.Margin = new Thickness(0, 0, image1.Source.Width, image1.Source.Height);
            //canvasMain.Children.Add(rects.Last());
            //selectInd = listBox1.SelectedIndex;
            //listBox1.SelectedIndex = selectInd; 
            return rect_tp;
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                classesStr.RemoveAt(listBox1.SelectedIndex);
                canvasMain.Children.Remove(rects[listBox1.SelectedIndex]);
                rects.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                
            }
            saveCurrent();
        }

        private void btOpen_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == true)
            {
                //string path = ofd.FileName;
                file_paths = ofd.FileNames;
                listBox1.Items.Clear();
                ImageSet.imageSets = new List<ImageSet>();
                for (int i = 0; i < file_paths.Length; i++)//initializing
                {
                    ImageSet.imageSets.Add(new ImageSet());
                    ImageSet.imageSets.Last().image_name = file_paths[i].Split('\\').Last().Split('.').First();
                    ImageSet.imageSets.Last().extention = file_paths[i].Split('\\').Last().Split('.')[1];
                    ImageSet.imageSets.Last().image = new Image();
                    ImageSet.imageSets.Last().image.Source = new BitmapImage(new Uri(file_paths[i]));
                    if (chkAutoName.IsChecked == true)
                    {
                        ImageSet.imageSets.Last().rects.Add(Default_rect(ImageSet.imageSets.Last().image));
                        ImageSet.imageSets.Last().classes.Add(ImageSet.imageSets.Last().image_name);
                        if (i == 0)
                        {
                            rects.Add(ImageSet.imageSets.Last().rects.Last());
                            classesStr.Add(ImageSet.imageSets.Last().image_name);
                            ListboxAdd(ImageSet.imageSets.Last().image_name);                            
                        }
                    }
                    if (chkSameName.IsChecked == true)
                    {
                        ImageSet.imageSets.Last().rects.Add(Default_rect(ImageSet.imageSets.Last().image));
                        ImageSet.imageSets.Last().classes.Add(ImageSet.imageSets.Last().image_name);
                        if (i == 0)
                        {
                            rects.Add(ImageSet.imageSets.Last().rects.Last());
                            classesStr.Add(ImageSet.imageSets.Last().image_name);
                            ListboxAdd(txtClname.Text);
                        }

                        int last = ImageSet.imageSets.Last().classes.Count - 1;
                        if (last == -1) { ImageSet.imageSets.Last().classes.Add(txtClname.Text); last = 0; }
                        ImageSet.imageSets.Last().classes[last] = txtClname.Text;
                        last = classesStr.Count-1;
                        if (last == -1) { classesStr.Add(txtClname.Text); last = 0; }
                        classesStr[last] = txtClname.Text;
                    }
                }
                string path = file_paths[image_index];
                image.Source = new BitmapImage(new Uri(path));
                SetUpImage();
            }
        }


        void SetUpImage()
        {
            if (chbAutoScale.IsChecked == true)//auto scale
            {
                double w_sz = canvasMain.ActualWidth / image.Source.Width;
                double h_sz = canvasMain.ActualHeight / image.Source.Height;

                if (w_sz < h_sz)
                    scale_image(w_sz);
                else
                    scale_image(h_sz);
            }
            else
            {
                canvasMain.Width = image.Source.Width;
                canvasMain.Height = image.Source.Height;
            }

            if (chkAutoName.IsChecked == true)
            {
                txtClname.Text = file_paths[image_index].Split('\\').Last().Split('.').First();
                //btAdd_Click(null, null);
            }
            //ImageSet.imageSets[image_index].image_name = file_paths[image_index].Split('\\').Last().Split('.').First();

            string[] tp_write = file_paths[image_index].Split('\\');
            string write_path = "";
            for (int i = 0; i < tp_write.Length - 1; i++)
            {
                write_path += tp_write[i] +'\\';
            }
            DataSetWriter.write_path = write_path;
        }



        private void sldScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (image != null && image.Source != null)
            {
                double scale = 10/sldScale.Value;
                if (image != null)
                {
                    scale_image(scale);
                }
            }
        }

        void scale_image(double scale)
        {
            canvasMain.RenderTransform = new ScaleTransform(scale, scale);
            if (image.Source != null)
            {
                canvasMain.Width = image.Source.Width * scale;
                canvasMain.Height = image.Source.Height * scale;
            }
        }

        private void scrollViewer1_PreviewMouseMove(object sender, MouseEventArgs e)//Drawing
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (IsDrawing)
                    end = e.GetPosition(canvasMain);
                else
                {
                    start = e.GetPosition(canvasMain);
                    IsDrawing = true;
                }
            }
            else
            {
                IsDrawing = false;
            }
            double width = end.X - start.X;
            double height = end.Y - start.Y;

            if (width > 0 && height > 0 && selectInd !=-1 && IsDrawing && rects.Count>0)
            {
                rects[selectInd].Width = width;
                rects[selectInd].Height = height;
                rects[selectInd].Margin = new Thickness(start.X, start.Y, end.X, end.Y);                
            }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                selectInd = listBox1.SelectedIndex;
                //rectDraw = rects[listBox1.SelectedIndex];
                rects[listBox1.SelectedIndex].Stroke = brushes[listBox1.SelectedIndex];
            }
            IsDrawing = false;
        }

        private void scrollViewer1_LayoutUpdated(object sender, EventArgs e)
        {
            if (chbAutoScale.IsChecked == true && image.Source!=null)
            {
                double w_sz = scrollViewer1.ActualWidth / image.Source.Width;
                double h_sz = scrollViewer1.ActualHeight / image.Source.Height;

                if (w_sz < h_sz)    
                    scale_image(w_sz);
                else
                    scale_image(h_sz);
            }
        }

        public void saveCurrent()
        {
            ImageSet.imageSets[image_index].image = image;
            ImageSet.imageSets[image_index].rects = rects;
            ImageSet.imageSets[image_index].classes = classesStr;//saving
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            if (image_index+1 < file_paths.Length)
            {
                ImageSet.imageSets[image_index].image = image;
                ImageSet.imageSets[image_index].rects = rects;
                ImageSet.imageSets[image_index].classes = classesStr;//saving

                image_index++;

                image = ImageSet.imageSets[image_index].image;
                rects = ImageSet.imageSets[image_index].rects;
                classesStr = ImageSet.imageSets[image_index].classes;
                //redraw
                canvasMain.Children.Clear();
                if (image == null)
                {
                    image = new Image();
                    image.Source = new BitmapImage(new Uri(file_paths[image_index]));
                }
                canvasMain.Children.Add(image);
                foreach (Rectangle r in rects)
                {
                    canvasMain.Children.Add(r);
                }
                //reset listbox
                listBox1.Items.Clear();
                for (int i = 0; i < classesStr.Count; i++)
                {
                    Label lb = new Label();
                    lb.Content = classesStr[i];
                    lb.Foreground = brushes[i];
                    listBox1.Items.Add(lb);// txtClname.Text);
                }
            }
        }

        private void btLast_Click(object sender, RoutedEventArgs e)
        {
            if (image_index > 0)
            {
                ImageSet.imageSets[image_index].image = image;
                ImageSet.imageSets[image_index].rects = rects;
                ImageSet.imageSets[image_index].classes = classesStr;//saving

                image_index--;

                image = ImageSet.imageSets[image_index].image;
                rects = ImageSet.imageSets[image_index].rects;
                classesStr = ImageSet.imageSets[image_index].classes;
                //redraw
                canvasMain.Children.Clear();
                if (image == null)
                {
                    image = new Image();
                    image.Source = new BitmapImage(new Uri(file_paths[image_index]));
                }
                canvasMain.Children.Add(image);
                foreach (Rectangle r in rects)
                {
                    canvasMain.Children.Add(r);
                }
                //reset listbox
                listBox1.Items.Clear();
                for (int i = 0; i < classesStr.Count; i++)
                {
                    Label lb = new Label();
                    lb.Content = classesStr[i];
                    lb.Foreground = brushes[i];
                    listBox1.Items.Add(lb);// txtClname.Text);
                }
            }
        }

        private void canvasMain_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (IsDrawing)
                    end = e.GetPosition(canvasMain);
                else
                {
                    start = e.GetPosition(canvasMain);
                    IsDrawing = true;
                }
            }
            else
            {
                IsDrawing = false;
            }
            double width = end.X - start.X;
            double height = end.Y - start.Y;

            if (width > 0 && height > 0 && selectInd != -1 && IsDrawing && rects.Count > 0)
            {
                rects[selectInd].Width = width;
                rects[selectInd].Height = height;
                rects[selectInd].Margin = new Thickness(start.X, start.Y, end.X, end.Y);
            }
        }

        private void btShowResults_Click(object sender, RoutedEventArgs e)
        {
            Window resultsWindow = new Window1();
            resultsWindow.Show();
        }
    }
}
