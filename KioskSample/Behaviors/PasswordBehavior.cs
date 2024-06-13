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
    public class PasswordBehavior : Behavior<PasswordBox>
    {
        private bool _isWork;

        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        private void AssociatedObject_PasswordChanged(object sender, EventArgs e)
        {
            if (_isWork)
            {
                return;
            }
            _isWork = true;
            BindingPassword = AssociatedObject.Password;
            _isWork = false;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }

        public string BindingPassword
        {
            get { return (string)GetValue(BindingPasswordProperty); }
            set { SetValue(BindingPasswordProperty, value);}            
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingPasswordProperty =
            DependencyProperty.Register(nameof(BindingPassword), typeof(string), typeof(PasswordBehavior), new PropertyMetadata(null));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            // ViewModel의 Password 프로퍼티로 값 전달
            // BindingPassword 프로퍼티의 이름이 변경되었을 때 작업 수행
            switch (e.Property.Name)
            {
                case nameof(BindingPassword):
                    if (_isWork)
                    {
                        return;
                    }
                    _isWork = true;
                    AssociatedObject.Password = BindingPassword;
                    _isWork = false;
                    break;              
            }
            base.OnPropertyChanged(e);
        }
        // PasswordBox의 Password 속성은 일반적인 DependencyProperty가 아니기 때문에, TwoWay 바인딩이 자동으로 작동하지 않습니다.
        // 따라서, PasswordBox와 바인딩된 속성 간의 동기화를 수동으로 처리해야 합니다.

    }
}
