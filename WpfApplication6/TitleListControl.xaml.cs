using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfApplication6
{
    /// <summary>
    /// Interaction logic for TitleListControl.xaml
    /// </summary>
    public partial class TitleListControl : UserControl
    {
        public TitleListControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// get or set the items
        /// </summary>
        public List<TitleListItemModel> TitleListItems
        {
            get
            {
                var newList=new List<TitleListItemModel>();
                var list = (List<TitleListItemModel>) GetValue(TitleListItemsProperty);
                foreach (var item in list)
                {
                    if (!newList.Any(i=>i.Name==item.Name))
                    {
                        newList.Add(item);
                    }
                }

                return newList;
            }
            set{SetValue(TitleListItemsProperty,value);}
        }

        public static readonly DependencyProperty TitleListItemsProperty = DependencyProperty.Register("TitleListItems", typeof(List<TitleListItemModel>),
            typeof(TitleListControl),new PropertyMetadata(new List<TitleListItemModel>()));

        public UIElementCollection Items
        {
            get { return SpTitleList.Children; }
        }

        private void TitleListControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (TitleListItems!=null)
            {
                var items = TitleListItems;
                int index = 0;
                foreach (var item in items)
                {
                    var radiaoButton=new RadioButton()
                    {
                        Content = item.Name
                    };

                    if (index == 0)
                    {
                        radiaoButton.Style = GetStyle("first");
                    }
                    else if (index == items.Count - 1)
                    {
                        radiaoButton.Style = GetStyle("last");
                    }
                    item.Index = index;
                    radiaoButton.DataContext = item;

                    radiaoButton.Checked += ToggleButton_OnChecked;

                    SpTitleList.Children.Add(radiaoButton);
                    index++;
                }
            }
        }

        private Style GetStyle(string type)
        {
            Style style = null;
            switch (type)
            {
                case "first":
                {
                    style = this.Resources["FirstButtonStyle"] as Style;
                }
                    break;
                case "last":
                {
                    style = this.Resources["LastButtonStyle"] as Style;
                }
                    break;
            }
            return style;
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton=sender as RadioButton;
            var dataModel=radioButton.DataContext as TitleListItemModel;
            int index = dataModel.Index;
            int count = SpTitleList.Children.Count;
            var linerBrush = new LinearGradientBrush(){StartPoint=new Point(0,1),EndPoint = new Point(1,1)};
            if (index==0)
            {
                linerBrush.GradientStops.Add(new GradientStop()
                {
                    Color = Colors.White,
                    Offset = 0.2
                });
                linerBrush.GradientStops.Add(new GradientStop()
                {
                    Color = Colors.DeepSkyBlue,
                    Offset = 1
                });
            }
            else if (index == count - 1)
            {
                linerBrush.GradientStops.Add(new GradientStop()
                {
                    Color = Colors.DeepSkyBlue,
                    Offset = 0
                });
                linerBrush.GradientStops.Add(new GradientStop()
                {
                    Color = Colors.White,
                    Offset = 0.8
                });
            }
            else
            {
                double offsetValue = Convert.ToDouble(index) / count;
                linerBrush.GradientStops.Add(new GradientStop()
                {
                    Color = Colors.DeepSkyBlue,
                    Offset = 0
                });
                linerBrush.GradientStops.Add(new GradientStop()
                {
                    Color = Colors.White,
                    Offset = offsetValue
                });
                linerBrush.GradientStops.Add(new GradientStop()
                {
                    Color = Colors.DeepSkyBlue,
                    Offset = 1
                });
            }
            ControlBorder.Background = linerBrush;
        }
    }

    public class TitleListItemModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
    }
}
