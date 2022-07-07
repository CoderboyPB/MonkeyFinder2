using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls.Shapes;

namespace MonkeyFinder2.View;

public partial class Test : ContentPage
{
    public Test()
    {
        InitializeComponent();
        BindingContext = new TestViewModel();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //((SolidColorBrush)this.elli.Fill).Color = Colors.Blue;
        //elli.BackgroundColorTo(Colors.Blue);
    }

    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        Shape shape = (sender as Element).Parent as Shape;
        e.Data.Properties.Add("square", new Square(shape.Width, shape.Height));
    }
}


