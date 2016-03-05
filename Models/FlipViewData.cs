using System;
using System.Collections.ObjectModel;
using Template10.Mvvm;

namespace SJ5000Plus.Models
{
    public class FlipViewData : BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        private Uri _image = null;
        private int _Index;
        private String _picture = null;
        private string _subtitle = string.Empty;
        private string _subtitleOptional = string.Empty;
        private string _title = string.Empty;

        public FlipViewData(String title, String subtitle, string subtitleOptional, String picture, int index)
        {
            _title = title;
            _subtitle = subtitle;
            _picture = picture;
            _subtitleOptional = subtitleOptional;
            _Index = index;
        }

        public Uri Image
        {
            get
            {
                return new Uri(_baseUri, _picture);
            }

            set
            {
                _picture = null;
                Set(ref _image, value);
            }
        }

        public int Index
        {
            get { return _Index; }
            set { Set(ref _Index, value); }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set { Set(ref _subtitle, value); }
        }

        public string SubtitleOptional
        {
            get { return _subtitleOptional; }
            set { Set(ref _subtitleOptional, value); }
        }

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }
    }

    public sealed class FlipViewDataSource
    {
        private ObservableCollection<object> _items = new ObservableCollection<object>();

        public FlipViewDataSource()
        {
            Items.Add(new FlipViewData("Enciende tu cámara",
                    "Presiona el bótón de encendido", "",
                    "Assets/Images/Photo_1.jpg", 1
                    ));
            Items.Add(new FlipViewData("Activa el Wi-Fi de la cámara",
                    "Presiona el bótón lateral de Wi-Fi", "",
                    "Assets/Images/Photo_2.jpg", 2
                    ));
            Items.Add(new FlipViewData("Conéctate al Wi-Fi de la cámara",
                    "Nombre por defecto: SJ5000 + _XXXXXX", "Contraseña por defecto: 1234567890",
                    "Assets/Images/Photo_3.png", 3
                    ));
        }

        public ObservableCollection<object> Items
        {
            get { return _items; }
        }
    }
}