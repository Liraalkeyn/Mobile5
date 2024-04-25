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
    public class TypeCargoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; 

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<TypeCargo> _TypeCargos; 
        public IEnumerable<TypeCargo> TypeCargos 
        {
            get { return _TypeCargos; }
            set { _TypeCargos = value; OnPropertyChanged(); }
        }

        private void GetTypeCargo()
        {
            using (var context = new Rchtest1Context()) { TypeCargos = context.TypeCargos.ToList(); }
        }

        public TypeCargoViewModel() 
        {
            newTypeCargo = new TypeCargo();
            GetTypeCargo();
        }

        private TypeCargo _newTypeCargo;
        public TypeCargo newTypeCargo
        {
            get { return _newTypeCargo; }
            set { _newTypeCargo = value; OnPropertyChanged(); }
        }

        public DelegateCommand AddTypeCargoCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddTypeCargo();
                });
            }
        }

        private void AddTypeCargo() 
        {
            using (var context = new Rchtest1Context())
            {
                context.TypeCargos.Add(newTypeCargo);
                context.SaveChanges();
                TypeCargos = context.TypeCargos.ToList();
            }
            newTypeCargo = new TypeCargo();
        }

        private void DeleteTypeCargo(int TypeCargoId) 
        {
            using (var context = new Rchtest1Context())
            {
                TypeCargo TypeCargoToDelete = context.TypeCargos.Find(TypeCargoId);
                context.TypeCargos.Remove(TypeCargoToDelete);
                context.SaveChanges();
                TypeCargos = context.TypeCargos.ToList();
            }
        }

        public DelegateCommand DeleteTypeCargoCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteTypeCargo((int)o);
                });
            }
        }

        private TypeCargo _editTypeCargo; 
        public TypeCargo editTypeCargo
        {
            get { return _editTypeCargo; }
            set { _editTypeCargo = value; OnPropertyChanged(); } 
        }
        private void UseTypeCargo(int TypeCargoId)
        {
            using (var context = new Rchtest1Context())
            {
                editTypeCargo = context.TypeCargos.Find(TypeCargoId);
            }
        }

        public DelegateCommand UseTypeCargoCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseTypeCargo((int)o);
                });
            }
        }

        private void EditTypeCargo(int TypeCargoId) 
        {
            using (var context = new Rchtest1Context())
            {
                context.TypeCargos.Update(editTypeCargo);
                context.SaveChanges();
                TypeCargos = context.TypeCargos.ToList();
            }
        }

        public DelegateCommand EditTypeCargoCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditTypeCargo((int)o);
                });
            }
        }

    }
}



