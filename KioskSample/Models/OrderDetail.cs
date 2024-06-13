using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSample.Models
{
    public class OrderDetail : BindableBase
    {
        public Guid OrderId { get; set; }

        private int _sortOrder;
        public int SortOrder
        {
            get { return _sortOrder; }
            set { SetProperty(ref _sortOrder, value); }
        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        private int _unitPrice;
        public int UnitPrice
        {
            get { return _unitPrice; }
            set { SetProperty(ref _unitPrice, value, () => RaisePropertyChanged(nameof(Amount))); }
        }
        // 수량이나 단가가 변경되면 RaisePropertyChanged() 메서드를 이용해서, Amount 프로퍼티가 변경되었음을 알림
        // RaisePropertyChanged()는 Prism에서 제공하는 강제로 프로퍼티 체인지 이벤트를 발생시켜주는 메서드입니다.

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value, () => RaisePropertyChanged(nameof(Amount))); }
        }

        public int Amount
        {
            get { return Quantity * UnitPrice; }
        }
    }
}
