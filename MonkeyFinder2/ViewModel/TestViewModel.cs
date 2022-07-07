using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFinder2.ViewModel;
public partial class TestViewModel : BaseViewModel
{
    Object data;

    [RelayCommand]
    void Drop(Ellipse ellipse)
    {
        Color color = data as Color;
        ellipse.Fill = new SolidColorBrush(color);
    }

    [RelayCommand]
    void Drag(Brush fill)
    {
        data = (fill as SolidColorBrush).Color;
    }
}
