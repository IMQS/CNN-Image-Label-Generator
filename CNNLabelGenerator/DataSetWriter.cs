using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace WeightEditor
{
    class classSet
    {
        public string name;
        public int id;
    }

    class DataSetWriter
    {
        public static string write_path;

        public static string GetLine(int class_id,Point start,Point end,double width,double height)
        {
            double dw = 1.0 / width;
            double dh = 1.0 / height;
            double x = (end.X + start.X)/2;
            double y = (end.Y + start.Y)/2;
            double w = end.X - start.X;
            double h = end.Y - start.Y;
            x = x * dw;
            w = w * dw;
            y = y * dh;
            h = h * dh;
            return class_id + " " + x + " " + y + " " + w + " " + h;
        }
        public static List<string> GetClassList(List<ImageSet> imageSet)
        {
            List<string>tempList = new List<string>();

            for (int a = 0; a < imageSet.Count; a++)
            {
                for (int b = 0; b < imageSet[a].classes.Count; b++)
                {
                    if (tempList.Contains(imageSet[a].classes[b]))//already has it
                    {
                        //ignore
                    }
                    else 
                    {
                        tempList.Add(imageSet[a].classes[b]);
                    }
                }
            }
            return tempList;
        }

        public static List<string> getLabels(ImageSet imageSet)
        {
            List<string> tp = DataSetWriter.GetClassList(ImageSet.imageSets);
            List<string> result = new List<string>(){};

            for (int i = 0; i < imageSet.classes.Count; i++)
            {
                int id = tp.IndexOf(imageSet.classes[i]);
                Point start = new Point(imageSet.rects[i].Margin.Left,imageSet.rects[i].Margin.Top);
                Point end = new Point(imageSet.rects[i].Margin.Right,imageSet.rects[i].Margin.Bottom);
                result.Add(DataSetWriter.GetLine(id,start,end,imageSet.image.Source.Width,imageSet.image.Source.Height));
            }
            return result;
        }

        public static void WriteLabels()
        {

            for(int i =0;i<ImageSet.imageSets.Count;i++)
            {
                StreamWriter filewriter = File.CreateText(DataSetWriter.write_path+"Labels\\"+ImageSet.imageSets[i].image_name+".txt");
                List<string> Labels = getLabels(ImageSet.imageSets[i]);
                foreach (string s in Labels)
                {
                    filewriter.WriteLine(s);
                }
                filewriter.Close();
            }
        }

        public static List<string> GetFilePaths()
        {
            List<string> paths = new List<string>() { };
            for (int i = 0; i < ImageSet.imageSets.Count; i++)
            {
                paths.Add(write_path + ImageSet.imageSets[i].image_name + ImageSet.imageSets[i].extention);
            }
            return paths;
        }

    }
}
