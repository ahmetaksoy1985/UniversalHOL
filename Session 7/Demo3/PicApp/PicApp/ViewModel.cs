using PicApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PicApp
{
    class ViewModel : PropertyChangeNotification
    {
        public ViewModel()
        {
            this.isReadVisible = true;
        }
        public DataItem DataItem
        {
            get
            {
                return (this.dataItem);
            }
            set
            {
                base.SetProperty(ref this.dataItem, value);
                this.titleCopy = this.dataItem.Title;
            }
        }
        public void ConfirmTitleChanges()
        {
            this.titleCopy = this.dataItem.Title;
        }
        public void RevertTitleChanges()
        {
            this.dataItem.Title = this.titleCopy;
        }
        public void SetReadMode()
        {
            this.isReadVisible = true;
            this.RaiseVisibilityChanges();
        }
        public void SetEditMode()
        {
            this.isReadVisible = false;
            this.RaiseVisibilityChanges();
        }
        void RaiseVisibilityChanges()
        {
            base.OnPropertyChanged("IsReadVisible");
            base.OnPropertyChanged("IsEditVisible");
        }
        public Visibility IsReadVisible
        {
            get
            {
                return (BoolToVisibility(this.isReadVisible));
            }
        }
        public Visibility IsEditVisible
        {
            get
            {
                return (BoolToVisibility(!this.isReadVisible));
            }
        }
        public bool IsEditing
        {
            get
            {
                return (!this.isReadVisible);
            }
        }
        static Visibility BoolToVisibility(bool value)
        {
            return (value ? Visibility.Visible : Visibility.Collapsed);
        }
        string titleCopy;
        DataItem dataItem;
        bool isReadVisible;
    }
}
