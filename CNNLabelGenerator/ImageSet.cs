using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace WeightEditor
{
    class ImageSet
    {

        public static List<ImageSet> imageSets = new List<ImageSet>() { };

        public List<Rectangle> rects = new List<Rectangle>() { };
        public List<string> classes = new List<string>() { };
        public Image image;
        public string image_name;
        public string extention;

        
        public static ImageSet init()
        {
            ImageSet tp = new ImageSet();
            //tp.image = new Image();
            return tp;
        }

    }
}
