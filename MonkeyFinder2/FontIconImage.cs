namespace MonkeyFinder2
{
    public class FontIconImage : Image
    {
        public static readonly BindableProperty FontIconColorProperty = BindableProperty.Create(nameof(FontIconColor), typeof(Color), typeof(FontIconImage), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var image = (FontIconImage)bindable;
            image.FontIconColor = (Color)newValue;
        });

        private Color _fontIconColor;
        public Color FontIconColor
        {
            get => _fontIconColor;
            set
            {
                _fontIconColor = value;
                (Source as FontImageSource).Color = value;
            }
        }
    }
}
