using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OzyaysanBusinessEngine
{
    public class ViewModel:INotifyPropertyChanged
    {

        private string m_CoilNo = "";
        #region Fields
        public int WireDiameter { get; set; }
        public int Hardness { get; set; }
        public string Compound { get; set; }
        public int Amount { get; set; }
        public string Quality { get; set; }
        public string SurfaceCondition { get; set; }
        public string RegistryNo { get; set; }
        public string CoilNo { get { return this.m_CoilNo; } set { this.m_CoilNo = value; NotifyPropertyChanged("sss"); } }
        public decimal BeginningInventory { get; set; }
        public decimal CurrentInventory { get; set; }
        #endregion
        // Gets or sets the CollectionViewSource
        public CollectionViewSource ViewSource { get; set; }

        // Gets or sets the ObservableCollection
        public ObservableCollection<ViewModel> Collection { get; set; }

        // Instantiates the objets.
        public ViewModel()
        {

            this.Collection = new ObservableCollection<ViewModel>();
            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Collection;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged( String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}
