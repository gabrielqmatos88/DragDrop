using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace WpfFileReader
{
    public class Arquivo
    {
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime UltimaModificacao { get; set; }
        public string Caminho { get; set; }
        public string Tamanho { get; set; }
        public BitmapImage Icone
        { 
            get 
            {
                using ( var memory = new MemoryStream() )
                {
                    Icon.ExtractAssociatedIcon( Caminho ).ToBitmap().Save( memory , ImageFormat.Png );
                    memory.Position = 0;
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
            } 
        }
    }


}
