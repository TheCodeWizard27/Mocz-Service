using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mocz_Parser
{
    public class MoczHandler : INotifyPropertyChanged
    {

        private MoczModel _moczModel;

        public ObservableCollection<string> ResultList { get; set; } = new ObservableCollection<string>();

        public MoczHandler()
        {
            _moczModel = new MoczModel();
        }

        public void Init()
        {
            _moczModel.Load();
        }

        public void LoadResults(string input)
        {
            ResultList.Clear();

            var leftId = _moczModel.Ipadic.FirstOrDefault(x => x.Item2.Item1 == input).Item1;

            foreach(var entry in _moczModel.Ipadic.Where(x => x.Item1 == leftId))
            {
                ResultList.Add(entry.Item2.Item2);
            }

        }

        #region Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
