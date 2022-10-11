using System;
using DW = System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DMS_Beta.Helpers
{
    public static class ExImage
    {
        public static void InitImage(this Image img, DW.Bitmap bmp)
        {
            if (bmp == null) return;
            img.Source = null;
            MemoryStream __ms = new MemoryStream();
            bmp.Save(__ms, DW.Imaging.ImageFormat.Bmp);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            __ms.Seek(0, SeekOrigin.Begin);
            bi.CacheOption = BitmapCacheOption.OnDemand;
            bi.StreamSource = __ms;
            bi.EndInit();
            img.Source = bi;
        }
    }
}
