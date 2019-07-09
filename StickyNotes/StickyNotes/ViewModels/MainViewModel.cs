using Rene.Xam.Extensions.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using StickyNotes.Model;
using StickyNotes.Settings;
using StickyNotes.DeviceHelper;
using Xamarin.Forms;

namespace StickyNotes.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Todo> _todoList;
        public ObservableCollection<Todo> TodoList
        {
            get { return _todoList; }
            set { SetProperty(ref _todoList, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand AddClick { get; set; }
        public ICommand RemoveClick { get; set; }

        private readonly IUserSettings _userSettings;

        public MainViewModel(IUserSettings userSettings)
        {
            _userSettings = userSettings;
            AddClick = new Command(async () => await OnAddClickAsync());
            RemoveClick = new Command<Todo>(OnRemoveClick);
            Initialize();
        }

        public async void Initialize()
        {
            TodoList = new ObservableCollection<Todo>(_userSettings.Todos ?? new List<Todo>());

            foreach (Todo todo in TodoList)
            {
                await Task.Run(async () =>
                 {
                     DependencyService.Get<INotification>().CreateNotification(todo.Id, todo.Title);
                 });
            }

            MessagingCenter.Subscribe<IMessager, List<Todo>>(this, "todos", (sender, arg) => {
                TodoList = new ObservableCollection<Todo>(arg);
            });
        }

        public async Task OnAddClickAsync()
        {
            _userSettings.MaxId = _userSettings.MaxId + 1;
            TodoList.Add(new Todo { Id = _userSettings.MaxId, Title = Title });
            _userSettings.Todos = TodoList.ToList();
            //TodoList = new ObservableCollection<Todo>(_userSettings.Todos ?? new List<Todo>());
            await Task.Run(async () =>
            {
                DependencyService.Get<INotification>().CreateNotification(_userSettings.MaxId, Title);
            });
            Title = string.Empty;
        }

        public void OnRemoveClick(Todo Todo)
        {
            TodoList.Remove(Todo);
            _userSettings.Todos = TodoList.ToList();
            if (TodoList.Count == 0) _userSettings.MaxId = 0;
            DependencyService.Get<INotification>().RemoveNotification(Todo.Id);
        }

    }
}
