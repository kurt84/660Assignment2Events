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
using System.Windows.Shapes;

namespace EventPlanner
{
    public partial class CreateWindow : Window
    {

        EventModel db;
        Event selectedEvent;
        public CreateWindow(EventModel context, Event selectedEvent = null)
        {
            InitializeComponent();
            db = context;
            categoryCombo.ItemsSource = db.Categories.ToList();
            categoryCombo.DisplayMemberPath = "Name";
            this.selectedEvent = selectedEvent;
            if (selectedEvent != null) {
                newTitle.Text = selectedEvent.Title;
                newBody.Text = selectedEvent.Body;
                newNote.Text = selectedEvent.Note;
                newDate.SelectedDate = selectedEvent.Date;
                categoryCombo.SelectedItem = db.Categories.FirstOrDefault(c => c.Id == selectedEvent.CategoryId);
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (selectedEvent != null) {
                selectedEvent.Title = newTitle.Text;
                selectedEvent.Body = newBody.Text;
                selectedEvent.Note = newNote.Text;
                selectedEvent.Category = categoryCombo.SelectedItem as Category;
                selectedEvent.Date = newDate.SelectedDate?? DateTime.Today;
            }
            else
            {
                Event newEvent = new Event {
                    Title = newTitle.Text,
                    Body = newBody.Text,
                    Note = newNote.Text,
                    Category = categoryCombo.SelectedItem as Category,
                    Date = newDate.SelectedDate ?? DateTime.Today
            };
                db.Events.Add(newEvent);
            }
            db.SaveChanges();
            this.Close();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
