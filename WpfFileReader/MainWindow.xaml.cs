using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFileReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        BindingList<Arquivo> arquivos;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            arquivos = new BindingList<Arquivo>();
            listBoxArquivos.ItemsSource = arquivos;
        }

        private void Grid_Drop( object sender , DragEventArgs e )
        {
            if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
            {
                string[] files = (string[]) e.Data.GetData( DataFormats.FileDrop );
                files.ToList().ForEach( Armazenar );
            }
        }

        private void Armazenar( string filename )
        {
            if ( !arquivos.Any( a => a.Caminho == filename ) )
            {
                var info = new FileInfo( filename );
                arquivos.Insert( 0 , new Arquivo() 
                {
                    Caminho = filename , 
                    Nome = info.Name ,
                    DataCriacao = info.CreationTime ,
                    UltimaModificacao = info.LastAccessTime ,
                    Tamanho = BytesToString( info.Length )
                } );
            }
        }

        private void Grid_DragOver( object sender , DragEventArgs e )
        {
            if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
            {
                e.Effects = DragDropEffects.Copy;
            }
            else 
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Grid_DragLeave( object sender , DragEventArgs e )
        {
            //if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
            //{
            //    e.Effects = DragDropEffects.None;
            //}
        }

        private String BytesToString( long byteCount )
        {
            string[] suf = { "B" , "KB" , "MB" , "GB" , "TB" , "PB" , "EB" }; 
            if ( byteCount == 0 )
                return "0" + suf[0];
            long bytes = Math.Abs( byteCount );
            int place = Convert.ToInt32( Math.Floor( Math.Log( bytes , 1024 ) ) );
            double num = Math.Round( bytes / Math.Pow( 1024 , place ) , 1 );
            return ( Math.Sign( byteCount ) * num ).ToString() + suf[place];
        }
    }
}
