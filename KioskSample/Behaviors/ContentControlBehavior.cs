using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KioskSample.Behaviors
{
    public class ContentControlBehavior : Behavior<ContentControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }
        protected override void OnDetaching()
        {

        }

        public string ViewName
        {
            get { return (string)GetValue(ViewNameProperty); }
            set { SetValue(ViewNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewNameProperty =
            DependencyProperty.Register("ViewName", typeof(string), typeof(ContentControl), new PropertyMetadata(null));


        public Type ViewType
        {
            get { return (Type)GetValue(ViewTypeProperty); }
            set { SetValue(ViewTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewTypeProperty =
            DependencyProperty.Register("ViewType", typeof(Type), typeof(ContentControlBehavior), new PropertyMetadata(null));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (e.Property.Name)
            {
                case nameof(ViewName):
                case nameof(ViewType):
                    // viewname 이나 viewtype이 변경되면 실행 
                    ResolveView();
                    break;
            }
            base.OnPropertyChanged(e);
        }
        private void ResolveView()
        {
            if (AssociatedObject == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(ViewName) == false)
            {
                Type type = Type.GetType(ViewName);
                if (type != null)
                {
                    object view = App.ContainerProvider.Resolve(type);
                    AssociatedObject.Content = view;
                }
            }
            else if (ViewType != null)
            {
                object view = App.ContainerProvider.Resolve(ViewType);
                AssociatedObject.Content = view;
            }
        }



    }
}
