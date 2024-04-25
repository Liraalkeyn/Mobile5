using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mobile5.Context;
using Mobile5.Models;

namespace Mobile5.ViewModels
{
    public class TransportViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler? PropertyChanged; 

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Transport> _Transports; 
        public IEnumerable<Transport> Transports 
        {
            get { return _Transports; }
            set { _Transports = value; OnPropertyChanged(); } 
        }
        

        private void GetTransport() 
        {
            using (var context = new Rchtest1Context()) { Transports = context.Transports.ToList(); }
        }

        public TransportViewModel() 
        {
            newTransport = new Transport();
            GetTransport();
        }

        private Transport _newTransport; 
        public Transport newTransport 
        {
            get { return _newTransport; }
            set { _newTransport = value; OnPropertyChanged(); } 
        }

        public DelegateCommand AddTransportCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddTransport();
                });
            }
        }

        private void AddTransport() 
        {
            using (var context = new Rchtest1Context())
            {
                context.Transports.Add(newTransport);
                context.SaveChanges();
                Transports = context.Transports.ToList();
            }
            newTransport = new Transport();
        }

        private void DeleteTransport(int TransportId) 
        {
            using (var context = new Rchtest1Context())
            {
                Transport TransportToDelete = context.Transports.Find(TransportId);
                context.Transports.Remove(TransportToDelete);
                context.SaveChanges();
                Transports = context.Transports.ToList();
            }
        }

        public DelegateCommand DeleteTransportCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteTransport((int)o);
                });
            }
        }

        private Transport _editTransport; 
        public Transport editTransport 
        {
            get { return _editTransport; }
            set { _editTransport = value; OnPropertyChanged(); } 
        }
        private void UseTransport(int TransportId)
        {
            using (var context = new Rchtest1Context())
            {
                editTransport = context.Transports.Find(TransportId);
            }
        }

        public DelegateCommand UseTransportCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseTransport((int)o);
                });
            }
        }

        private void EditTransport(int TransportId) 
        {
            using (var context = new Rchtest1Context())
            {
                context.Transports.Update(editTransport);
                context.SaveChanges();
                Transports = context.Transports.ToList();
            }
        }

        public DelegateCommand EditTransportCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditTransport((int)o);
                });
            }
        }

    }
}


