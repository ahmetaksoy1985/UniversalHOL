using S04_ListDemo.SampleData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace S04_ListDemo.Models
{
    public class SampleItem : INotifyPropertyChanged
    {
        public SampleItem()
        {
        }


        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (value != title)
                {
                    title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private string subTitle;
        public string SubTitle
        {
            get { return subTitle; }
            set
            {
                if (value != subTitle)
                {
                    subTitle = value;
                    NotifyPropertyChanged("SubTitle");
                }
            }
        }

        private string itemImage;
        public string ItemImage
        {
            get { return itemImage; }
            set
            {
                if (value != itemImage)
                {
                    itemImage = value;
                    NotifyPropertyChanged("ItemImage");
                }
            }
        }

        private string gprop;
        public string TargetGroup
        {
            get { return gprop; }
            set
            {
                if (value != gprop)
                {
                    gprop = value;
                    NotifyPropertyChanged("TargetGroup");
                }
            }
        }

        private ObservableCollection<SampleItem> items = new ObservableCollection<SampleItem>();
        public ObservableCollection<SampleItem> Items
        {
            get { return items; }
            set
            {
                if (value != items)
                {
                    items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public static ObservableCollection<SampleItem> GetSampleItems(int count)
        {
            ObservableCollection<SampleItem> returnValue = new ObservableCollection<SampleItem>();
            var companies = LoremIpsumGen.GetCompanyList(count);
            var images = LoremIpsumGen.GetImagesList(count);
            var text1 = LoremIpsumGen.GetLoremTextList(count, 3);
            var text2 = LoremIpsumGen.GetLoremTextList(count, 5);

            for (int i = 0; i < count; i++)
            {
                SampleItem si = new SampleItem()
                {
                    Title = text1[i],
                    SubTitle = text2[i],
                    ItemImage = images[i],
                    TargetGroup = companies[i]
                };
                returnValue.Add(si);
            }
            return returnValue;
        }
        public static ObservableCollection<SampleItem> GetSampleItems(int count, string company)
        {
            ObservableCollection<SampleItem> returnValue = GetSampleItems(count);

            foreach(SampleItem si in returnValue)
                si.TargetGroup = company;
            return returnValue;
        }

        public static ObservableCollection<SampleItem> GetGroupedSampleItems(int count, int innerCount){
            ObservableCollection<SampleItem> returnValue = GetSampleItems(count);
            foreach(SampleItem si in returnValue)
                si.Items = GetSampleItems(innerCount, si.TargetGroup);

            return returnValue;
        }

        public static SampleItem GetSampleItem()
        {
            SampleItem si = new SampleItem();
            Random r = new Random();
            si.Title = LoremIpsumGen.GetLoremText(5);
            si.SubTitle = LoremIpsumGen.GetLoremText(3);
            si.ItemImage = (LoremIpsumGen.GetImagesList(1))[0];
            
            return si;
        }
    }

}
