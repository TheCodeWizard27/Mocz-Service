using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mocz_Parser
{
    public class MoczHandler : INotifyPropertyChanged
    {

        private MoczModel _moczModel;
        public readonly string GoogleAPI = "http://www.google.com/transliterate";

        public ObservableCollection<string> ResultList { get; set; } = new ObservableCollection<string>();

        public MoczHandler()
        {
            _moczModel = new MoczModel();
        }

        public void Init()
        {
            _moczModel.Load();
        }

        public void LoadResults(string input, bool usegoogleApi = false)
        {
            ResultList.Clear();

            if(usegoogleApi)
            {
                LoadResultsWithGoogleApi(input);
                return;
            }

            LoadResultsWithLocalApi(input);
        }

        #region Private Methods

        private void LoadResultsWithLocalApi(string input)
        {
            /* Example of file Structure:
             * 
             * Input Text | leftId | middleId | RightId |    Text
             *    あいたた	  2584	      142	   5884	  あいたた
             *    
             * First approach select other Parts according to the first id.
             */
            var leftId = _moczModel.Ipadic.FirstOrDefault(x => x.Item2.Item1 == input).Item1;

            foreach (var entry in _moczModel.Ipadic.Where(x => x.Item1 == leftId))
            {
                ResultList.Add(entry.Item2.Item2);
            }
        }

        private void LoadResultsWithGoogleApi(string input)
        {
            var requestUrl = $"{GoogleAPI}?langpair=ja-Hira%7Cja&text={input}";
            var request = WebRequest.Create(requestUrl);
            
            using(var response = request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    dynamic d = JObject.Parse($"{{data:{result}}}");

                    dynamic data = d.data;
                    dynamic firstPart = data[0];
                    dynamic list = firstPart[1];

                    foreach(var entry in list)
                    {
                        ResultList.Add(entry.Value);
                    }
                }
            }

        }

        #endregion

        #region Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
