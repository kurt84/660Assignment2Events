using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventPlanner
{
    public partial class MainWindow : Window
    {
        EventModel db;
        Event selectedEvent;
        string filter;
        public MainWindow()
        {
            InitializeComponent();
            db = new EventModel();
            selectedEvent = db.Events.FirstOrDefault();
            RefreshDisplay();
            foreach (Category c in db.Categories.ToList())
            {
                filterComboBox.Items.Add(new ComboBoxItem { Content = c.Name });
            }
            filterComboBox.SelectionChanged += new SelectionChangedEventHandler(FilterEventPreviews);
        }

        private void NewButtonClick(object sender, RoutedEventArgs e)
        {
            var win = new CreateWindow(db);
            win.Owner = this;
            win.ShowDialog();
            RefreshDisplay();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to delete this event?", "Delete Event", MessageBoxButton.OKCancel);
            if (confirm == MessageBoxResult.OK) {
                db.Events.Remove(selectedEvent);
                db.SaveChanges();
                selectedEvent = db.Events.FirstOrDefault();
                RefreshDisplay();
            }
        }
        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            var win = new CreateWindow(db, selectedEvent);
            win.Owner = this;
            win.ShowDialog();
            RefreshDisplay();
        }
        private void DisplayEventDetails()
        {
            if (selectedEvent != null)
            {
                eventTitle.Text = selectedEvent.Title;
                eventBody.Text = selectedEvent.Body;
                eventNote.Text = selectedEvent.Note;
                eventCategory.Text = db.Categories.FirstOrDefault(x => x.Id == selectedEvent.CategoryId).Name;
                eventDate.Text = selectedEvent.Date.ToShortDateString();
                eventDetailsBorder.Background = EventPreviewMaker.GetColoredBackground(selectedEvent.CategoryId);
            }
        }
        private void DisplayEventPreviews()
        {
            previewList.Children.Clear();
            foreach (Event e in FilteredDbList(this.filter))
            {
                e.Category = db.Categories.FirstOrDefault(c => c.Id == e.CategoryId);
                var child = EventPreviewMaker.CreatePreview(e);
                child.MouseDown += SelectEvent;
                previewList.Children.Add(child);
            }
        }
        private void RefreshDisplay()
        {
            DisplayEventDetails();
            DisplayEventPreviews();
        }

        private void FilterEventPreviews(object sender, RoutedEventArgs e)
        {
            filter = (filterComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            DisplayEventPreviews();
        }
        List<Event> FilteredDbList(string filter = null)
        {
            Func<Event, bool> test;
            switch (filter)
            {
                case "All Time":
                    test = x => true;
                    break;
                case "Today":
                    test = x => x.Date == DateTime.Today;
                    break;
                case "This Week":
                    test = x => (x.Date >= DateTime.Today) && (x.Date <= DateTime.Today.AddDays(7));
                    break;
                default:
                    Category category = db.Categories.FirstOrDefault(c => c.Name == filter);
                    if(category != null)
                        test = x => x.CategoryId == category.Id;
                    else
                        test = x => true;
                    break;
            }
            return this.db.Events.Where(test).ToList();
        }
        private void SelectEvent(object sender, RoutedEventArgs e)
        {
            selectedEvent = ((sender as Grid).Tag as Event);
            DisplayEventDetails();
        }

        private void PrintButton(object sender, RoutedEventArgs e)
        {
            var print = new PrintDialog();
            if (print.ShowDialog() == true)
                print.PrintVisual(previewList, "Currently Selected Events");
        }
    }
}
