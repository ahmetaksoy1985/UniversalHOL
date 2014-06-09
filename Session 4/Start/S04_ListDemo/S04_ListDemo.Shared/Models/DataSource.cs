using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using S04_ListDemo.GroupingHelpers;
using System.Linq;

namespace S04_ListDemo.Models
{
    public class DataSource : INotifyPropertyChanged
    {
        public DataSource() 
        {
            Groups = new ObservableCollection<SampleItem>();
            Ungrouped = new ObservableCollection<SampleItem>();
        }

        //////////////////////////////////////////////////////////
        // The Groups property is the bare minimum required for grouping
        // It will not support the jumplist functionality available
        // through the SemanticZoom control
        /////////////////////////////////////////////////////////
        public ObservableCollection<SampleItem> Groups { get; set; }
        public ObservableCollection<SampleItem> Ungrouped { get; set; }

        #region GroupedForZoom (List<KeyedList<SampleItem>>)
        private List<KeyedList<string, SampleItem>> _groupedforZoom;
        public List<KeyedList<string, SampleItem>> GroupedForZoom
        {
            get 
            {
                if (_groupedforZoom == null || _groupedforZoom.Count == 0)
                    _groupedforZoom = linqGroupedList(Ungrouped);

                return _groupedforZoom; 
            }

        }
        #endregion

        #region AlphaGrouped (List<KeyedList<SampleItem>>)
        private List<AlphaKeyGroup<SampleItem>> _alphaGroupedforZoom;
        public List<AlphaKeyGroup<SampleItem>> AlphaGrouped
        {
            get
            {
                // Create the AlphaGrouped data from the Ungrouped data
                if (_alphaGroupedforZoom == null || _alphaGroupedforZoom.Count == 0)
                    _alphaGroupedforZoom = alphaGroupSorting(Ungrouped);

                return _alphaGroupedforZoom;
            }

        }
        #endregion

        private List<KeyedList<string, SampleItem>> linqGroupedList(IEnumerable<SampleItem> items)
        {
            ///////////////////////////////////////////////////
            //   To create the list grouped by company name, we use
            //     a Linq query in which we
            //   1) state which property we want to group by (TargetGroup)
            //   2) give a temporary IGrouping variable to group the items into (itemsByCompany)
            //   3) select those groupings into an IEnumerable of KeyedLists
            //   
            //   We've essentially just created a list of lists that
            //   our UI can understand for the sake of implementing the 
            //   SemanticZoom control.
            ///////////////////////////////////////////////////

            var groupedItems =
                    from item in items
                    orderby item.TargetGroup
                    group item by item.TargetGroup into itemsByCompany
                    select new KeyedList<string, SampleItem>(itemsByCompany);
            return groupedItems.ToList();
        }

        private List<AlphaKeyGroup<SampleItem>> alphaGroupSorting(IEnumerable<SampleItem> items)
        {
            ///////////////////////////////////////////////////
            //   To create the list grouped by letter, we add
            //   1) the list of items we're going to group
            //   2) how we are going to group them (in this case, by Title
            //   3) if we want to sort them (true)
            //
            //   This creates an alphabetized list of lists in which the items 
            //   in each list all starts with the same letter. This type is 
            //   recognized by our GridView and ListView controls and can 
            //   be grouped appropriately as well as being responsive to a
            //   SemanticZoom control for the sake of a jumplist interaction
            ///////////////////////////////////////////////////

            var returnGroup = AlphaKeyGroup<SampleItem>.CreateGroups(
                items,                                      // ungrouped list of items
                (SampleItem s) => { return s.Title; },  // the property to sort 
                true);                                      // order the items alphabetically 

            return returnGroup;
        }
                      
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}
