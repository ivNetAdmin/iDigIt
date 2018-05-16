using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using iDigIt.Helpers;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.ViewModels
{
    public class ReviewContentSearchViewModel : BaseModel
    {
        public ReviewContentSearchViewModel()
        {
            Title = "Search Review";
        }

        #region Properties
        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }

        private ObservableCollection<SearchResult> _listOfSearchResults;
        public ObservableCollection<SearchResult> SearchResults
        {
            get { return _listOfSearchResults; }
            set
            {
                _listOfSearchResults = value;
                OnPropertyChanged(); // Added the OnPropertyChanged Method
            }
        }
        #endregion

        #region Commands
        public Command SearchCommand // for ADD
        {
            get
            {
                return new Command(() =>
                {
                    SearchResults = new ObservableCollection<SearchResult>(Search());
                });
            }
        }       
        #endregion

        #region private methods
        private IEnumerable<SearchResult> Search()
        {
            var rtnList = new List<SearchResult>();

            var plants = _realmInstance.All<Plant>()
                .Where(x => x.Name.Contains(_searchTerm) || x.Notes.Contains(_searchTerm))
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Variety).ToList();

            var jobs = _realmInstance.All<Job>()
                .Where(x => x.Name.Contains(_searchTerm) || x.Notes.Contains(_searchTerm))
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Plant)
                .ThenBy(x => x.Type).ToList();

            var yields = _realmInstance.All<Yield>()
                .Where(x => x.Notes.Contains(_searchTerm))
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Plant).ToList();

            var frosts = _realmInstance.All<Frost>()
               .Where(x => x.Notes.Contains(_searchTerm))
               .OrderBy(x => x.Year)
               .ThenBy(x => x.Month).ToList();

            foreach(var plant in plants)
            {
                rtnList.Add(new SearchResult
                {
                    Plant = string.Format("{0} * {1}", plant.Name, plant.Variety),
                    Notes = plant.Notes
                });
            }

            foreach (var job in jobs)
            {
                rtnList.Add(new SearchResult
                {
                    Name = string.Format("{0} ({1})", job.Name, job.Type),
                    Date = (((DateTimeOffset)job.Date).LocalDateTime).ToString("dd-MMM-yy"),
                    Notes = job.Notes
                });
            }

            foreach (var yield in yields)
            {
                rtnList.Add(new SearchResult
                {
                    Name = yield.Crop,
                    Plant = yield.Plant,
                    Notes = yield.Notes
                });
            }

            foreach (var frost in frosts)
            {
                rtnList.Add(new SearchResult
                {
                    Name = "Frost",
                    Date = string.Format("{0}/{1}/{2}", frost.Day, frost.Month, frost.Year),
                    Notes = frost.Notes
                });
            }
            return rtnList;
        }
        #endregion
    }
}
