using KioskSample.Bases;
using KioskSample.Commons;
using KioskSample.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace KioskSample.ViewModels
{
    public class SelectMenuViewModel : ViewModelBase
    {
        // 한 페이지에 노출되는 상품의 수
        private const int _pageSize = 4;

        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

        private int _totalPages;

        // 전체 상품 목록
        private List<Product> _allCoffees;

        private IList<Product> _coffees;
        public IList<Product> Coffees
        {
            get { return _coffees; }
            set { SetProperty(ref _coffees, value); }
        }

        private IList<Product> _frappuccinos;
        public IList<Product> Frappuccinos
        {
            get { return _frappuccinos; }
            set { SetProperty(ref _frappuccinos, value); }
        }

        public ICommand SelectProductCommand { get; set; } 
        public ICommand PaymentCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }


        public SelectMenuViewModel()
        {
            Coffees = new List<Product>
            {
                new Product{ Category = ProductCategory.Coffee, Name = "아이스 아메리카노", Price = 1500 },
                new Product{ Category = ProductCategory.Coffee, Name = "아메리카노", Price = 1500 },
                new Product{ Category = ProductCategory.Coffee, Name = "아이스 바닐라 라떼", Price = 3500 },
                new Product{ Category = ProductCategory.Coffee, Name = "바닐라 라떼", Price = 3500 },
            };
        }

        public SelectMenuViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            Init();
        }

        private void Init()
        {
            CreateProducts();

            SelectProductCommand = new DelegateCommand<Product>(OnSelectProduct);
            PaymentCommand = new DelegateCommand(OnPayment);

            // currentPage가 0보다 클 때만 명령을 실행, CurrentPage 속성을 관찰하여 변경될 때 마다 명령의 실행 가능 여부를 다시 평가
            PreviousCommand = new DelegateCommand(OnPrevious, () => CurrentPage > 0).ObservesProperty(() => CurrentPage);
            NextCommand = new DelegateCommand(OnNext, () => CurrentPage < _totalPages).ObservesProperty(() => CurrentPage);
            AddCommand = new DelegateCommand<OrderDetail>(OnAdd);
            RemoveCommand = new DelegateCommand<OrderDetail>(OnRemove);

            // 전체 페이지를 계산
            _totalPages = _allCoffees.Count / _pageSize - 1;
            DisplayProducts(CurrentPage);

        }

        private void OnSelectProduct(Product product)
        {
            // RelativeSource AncestorType = ItemsControl : 현재 VisualTree 위치에서부터 위로 올라가면서 처음 만나는 ItemsControl을 찾습니다.
            // Path = DataContext.SelectProductCommand : ItemsControl을 찾으면 DataContext.SelectProductCommand를 실행합니다.
            // ItemsControl의 DataContext에는 ViewModel이 들어가 있기 때문에 DataContext.SelectProductCommand라고 하면 내가 원하는 커맨드를 찾을 수 있기 때문입니다.
            // CommandParameter="{Binding}" : 현재 위치가 DataTemplate이기 때문에 여기서 이야기하는 {Binding} 은 데이터를 가르키게 됩니다.
            // 현재 파라미터에 표시하는 데이터 -> ItemsControl에 바인딩된 Coffee 객체(Product)

            // AppContext.CurrentOrder.Items 컬렉션에서 ProductName이 product.Name과 일치하는 항목이 없으면 existItem은 null이 됩니다.
            var existItem = AppContext.CurrentOrder.Items.FirstOrDefault(od => od.ProductName == product.Name);
            if (existItem != null)
            {
                existItem.Quantity++;
            }
            else
            {
                AppContext.CurrentOrder.Items.Insert(0,
                    new OrderDetail
                    {
                        OrderId = AppContext.CurrentOrder.OrderId,
                        ProductName = product.Name,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                AppContext.CurrentOrder.UpdateProperties();
            }
        }

        private void OnPayment()
        {
            RegionManager.RequestNavigate("KioskContentRegion", "Payment");
        }

        private void DisplayProducts(int pageIndex)
        {
            CurrentPage = pageIndex;
            Coffees = _allCoffees.Skip(CurrentPage * _pageSize).Take(_pageSize).ToList();
        }

        private void OnPrevious()
        {
            if (CurrentPage > 0)
            {
                DisplayProducts(CurrentPage - 1);
            }
        }

        private void OnNext()
        {
            if (CurrentPage < _totalPages)
            {
                DisplayProducts(CurrentPage + 1);
            }
        }

        private void OnAdd(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return;
            }
            orderDetail.Quantity++;
        }

        private void OnRemove(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return;
            }
            orderDetail.Quantity--;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            AppContext.KioskStatus = StatusEnum.SelectMenu;
            AppContext.CurrentOrder = new Order();
            AppContext.CurrentOrder.OrderId = Guid.NewGuid();
        }

        private void CreateProducts()
        {
            // 외부 리소스 사용, 객체 초기화를 위해 기본 생성자와 같이 실행
            _allCoffees = new List<Product>
            {
                new Product{ Category = ProductCategory.Coffee, Name = "아이스 아메리카노", Price = 1500, ImageUri = new Uri("pack://application:,,,/Assets/Images/delicious-ice-cream-in-studio.jpg") },
                new Product{ Category = ProductCategory.Coffee, Name = "아메리카노", Price = 1500, ImageUri = new Uri("pack://application:,,,/Assets/Images/delicious-ice-cream-in-studio.jpg") },
                new Product{ Category = ProductCategory.Coffee, Name = "아이스 바닐라 라떼", Price = 3500, ImageUri = new Uri("pack://application:,,,/Assets/Images/delicious-ice-cream-in-studio.jpg") },
                new Product{ Category = ProductCategory.Coffee, Name = "바닐라 라떼", Price = 3500, ImageUri = new Uri("pack://application:,,,/Assets/Images/delicious-ice-cream-in-studio.jpg") },
                new Product{ Category = ProductCategory.Coffee, Name = "페이지2-1", Price = 1500 , ImageUri = new Uri("pack://application:,,,/Assets/Images/delicious-ice-cream-in-studio.jpg")},
                new Product{ Category = ProductCategory.Coffee, Name = "페이지2-2", Price = 1500 , ImageUri = new Uri("pack://application:,,,/Assets/Images/delicious-ice-cream-in-studio.jpg")},
                new Product{ Category = ProductCategory.Coffee, Name = "페이지2-3", Price = 3500 , ImageUri = new Uri("pack://application:,,,/Assets/Images/delicious-ice-cream-in-studio.jpg")},
                new Product{ Category = ProductCategory.Coffee, Name = "페이지2-4", Price = 3500 , ImageUri = new Uri("pack://application:,,,/Assets/Images/delicious-ice-cream-in-studio.jpg")},
            };
        }
    }
}
