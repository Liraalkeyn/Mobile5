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
    public class PositionViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler? PropertyChanged; 

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Position> _Positions;
        public IEnumerable<Position> Positions 
        {
            get { return _Positions; }
            set { _Positions = value; OnPropertyChanged(); }
        }

        private void GetPosition()
        {
            using (var context = new Rchtest1Context()) { Positions = context.Positions.ToList(); }
        }

        public PositionViewModel() 
        {
            newPosition = new Position();
            GetPosition();
        }

        private Position _newPosition;
        public Position newPosition 
        {
            get { return _newPosition; }
            set { _newPosition = value; OnPropertyChanged(); }
        }

        public DelegateCommand AddPositionCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddPosition();
                });
            }
        }

        private void AddPosition() 
        {
            using (var context = new Rchtest1Context())
            {
                context.Positions.Add(newPosition);
                context.SaveChanges();
                Positions = context.Positions.ToList();
            }
            newPosition = new Position();
        }

        private void DeletePosition(int PositionId) 
        {
            using (var context = new Rchtest1Context())
            {
                Position PositionToDelete = context.Positions.Find(PositionId);
                context.Positions.Remove(PositionToDelete);
                context.SaveChanges();
                Positions = context.Positions.ToList();
            }
        }

        public DelegateCommand DeletePositionCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeletePosition((int)o);
                });
            }
        }

        private Position _editPosition; 
        public Position editPosition
        {
            get { return _editPosition; }
            set { _editPosition = value; OnPropertyChanged(); } 
        }
        private void UsePosition(int PositionId)
        {
            using (var context = new Rchtest1Context())
            {
                editPosition = context.Positions.Find(PositionId);
            }
        }

        public DelegateCommand UsePositionCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UsePosition((int)o);
                });
            }
        }

        private void EditPosition(int PositionId) 
        {
            using (var context = new Rchtest1Context())
            {
                context.Positions.Update(editPosition);
                context.SaveChanges();
                Positions = context.Positions.ToList();
            }
        }

        public DelegateCommand EditPositionCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditPosition((int)o);
                });
            }
        }

    }
}

