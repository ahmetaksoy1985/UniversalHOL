using PicApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace PicApp
{
    [DataContract]
    class DataItem : PropertyChangeNotification
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public int Id
        {
            get
            {
                return (this.id);
            }
            set
            {
                base.SetProperty(ref this.id, value);
            }
        }
        [DataMember]
        public Uri ImageUri
        {
            get
            {
                return (this.imageUri);
            }
            set
            {
                base.SetProperty(ref this.imageUri, value);
            }
        }
        [DataMember]
        public string Title
        {
            get
            {
                return (this.title);
            }
            set
            {
                base.SetProperty(ref this.title, value);
            }
        }
        int id;
        Uri imageUri;
        string title;
    }
    // trying to keep this as simple, static and out of the way as possible.
    static class Data
    {
        static List<DataItem> dataItems;

        public static async Task<IList<DataItem>> GetItemsAsync()
        {
            if (dataItems == null)
            {
                var localFolder = ApplicationData.Current.LocalFolder;

                try
                {
                    var file = await localFolder.GetFileAsync(FILENAME);
                    
                    using (Stream netStream = await file.OpenStreamForReadAsync())
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(List<DataItem>));
                        dataItems = (List<DataItem>)serializer.ReadObject(netStream);
                    }
                }
                catch (FileNotFoundException)
                {
                }
                if (dataItems == null)
                {
                    dataItems = await BuildDataFromPackageAsync();
                }
            }
            return (dataItems);
        }
        public static async Task SaveItemsAsync()
        {
            if (dataItems != null)
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                var file = await localFolder.CreateFileAsync(FILENAME, CreationCollisionOption.ReplaceExisting);
                using (Stream netStream = await file.OpenStreamForWriteAsync())
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(List<DataItem>));
                    serializer.WriteObject(netStream, dataItems);
                }
            }
        }
        static async Task<List<DataItem>> BuildDataFromPackageAsync()
        {
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Images");
            var files = await folder.GetFilesAsync();
            return (files.Select(
                (f,i) => new DataItem()
                {
                    Id = i,
                    ImageUri = new Uri(string.Format(URI_BASE_FORMAT_STRING, f.Name)),
                    Title = f.Name
                }).ToList());
        }
        static readonly string URI_BASE_FORMAT_STRING = "ms-appx:///Images/{0}";
        static readonly string FILENAME = "pictures.xml";
    }
}
