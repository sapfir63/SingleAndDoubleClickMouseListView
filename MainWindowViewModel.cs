using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace MouseEventCommands {
	public class Item {
		public Item(string name, string matches) {
			Name = name;
			Matches = matches;
		}

		public string Name { get; set; }
		public string Matches { get; set; }
	}

	public class ItemHandler {
		public ItemHandler() {
			Items = new List<Item>();
		}

		public List<Item> Items { get; private set; }

		public void Add(Item item) {
			Items.Add(item);
		}
	}

	public class MainWindowViewModel : INotifyPropertyChanged {
		public RelayCommand MouseRightButtonUp { get; private set; }
		bool isDoubleClick = false;

		public ICommand MouseDoubleClickCommand { get; set; }
		public ICommand MouseLeftButtonUp { get; set; }

		private readonly ItemHandler _itemHandler;

		private Item _selectedItem;

		public Item SelectedItem {
			get { return _selectedItem; }
			set {
				_selectedItem = value;
				NotifyPropertyChanged("SelectedItem");
			}
		}

		public MainWindowViewModel() {
			_itemHandler = new ItemHandler();
			_itemHandler.Add(new Item("John Doe", "12"));
			_itemHandler.Add(new Item("Jane Doe", "133"));
			_itemHandler.Add(new Item("Sammy Doe", "45"));

			MouseRightButtonUp = new RelayCommand(Excute);
			MouseDoubleClickCommand = new RelayCommand(Excute1);
			MouseLeftButtonUp = new RelayCommand(Excute2);
		}

		public void Excute(object employee) {

			SelectedItem = (Item) employee;
			if (SelectedItem != null)
				Debug.Write("MouseRightButtonUp" + SelectedItem.Name + "  " + SelectedItem.Matches + "\n");
			else
				Debug.Write("MouseRightButtonUp ничего невыбрано\n");
		}

		public void Excute1(object employee) {

			SelectedItem = (Item) employee;
			isDoubleClick = true;
			if (SelectedItem != null)
				Debug.Write("MouseDoubleClick" + SelectedItem.Name + "  " + SelectedItem.Matches + "\n");
			else
				Debug.Write("MouseDoubleClick ничего невыбрано\n");

		}
		public void Excute2(object employee) {

			if (isDoubleClick != false) {
				isDoubleClick = false;
				return;
			}

			SelectedItem = (Item) employee;
			if (SelectedItem != null)
				Debug.Write("MouseLeftButtonUp" + SelectedItem.Name + "  " + SelectedItem.Matches + "\n");
			else
				Debug.Write("MouseLeftButtonUp ничего невыбрано\n");
		}
		public List<Item> Items {
			get { return _itemHandler.Items; }
		}

		public void RightClickOnItem(object item) {
			Items.Add(new Item("Items.Add", "25"));
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void NotifyPropertyChanged(string propertyName) {
			if (PropertyChanged != null) {
				PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
				this.PropertyChanged(this, args);
			}
		}
	}
}
