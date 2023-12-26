using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml;
using static System.Environment;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MailInteface.xaml
    /// </summary>
    public partial class MailInteface : Window
    {
        private string UserLogin { get; set; }
        private string UserPassword { get; set; }

        List<MainFoledr> folders = new List<MainFoledr> 
        {
             new MainFoledr() { Name = "Уся пошта", ServiceName = MailKit.SpecialFolder.All },
             new MainFoledr() { Name = "Відправлені", ServiceName = MailKit.SpecialFolder.Sent },
             new MainFoledr() { Name = "Чернетки", ServiceName = MailKit.SpecialFolder.Drafts },
             new MainFoledr() { Name = "Спам", ServiceName = MailKit.SpecialFolder.Junk },
             new MainFoledr() { Name = "Видалені", ServiceName = MailKit.SpecialFolder.Trash },
        };

        private string TmpString { get; set; }

        public MailInteface(string userLogin, string userPassword)
        {
            InitializeComponent();
            UserLogin = userLogin;
            UserPassword = userPassword;

            
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate(UserLogin, UserPassword);

                // --------------- get all folders
                foreach (var folder in folders)
                {
                    FolderList.Items.Add(folder.Name);
                }
            }
        }
        public class MainFoledr
        {
            public string Name { get; set; }
            public MailKit.SpecialFolder ServiceName { get; set; }
        }
        private async void FolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessagesList.Items.Clear();

            if (FolderList.SelectedItems != null && FolderList.SelectedItems.Count > 0)
            {
                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(UserLogin, UserPassword);

                    try
                    {
                        foreach (var selectedItem in FolderList.SelectedItems)
                        {
                            for (int i = 0; i < FolderList.Items.Count; i++)
                            {
                                if (selectedItem == FolderList.Items[i])
                                {
                                    var mailFolder = client.GetFolder(folders[i].ServiceName);

                                    // Відкриття папки для читання
                                    await mailFolder.OpenAsync(FolderAccess.ReadWrite);

                                    // Отримання всіх повідомлень з папки
                                    var uids = await mailFolder.SearchAsync(SearchQuery.All);

                                    // Додавання назви папки до списку повідомлень
                                    MessagesList.Items.Add($"Messages in {folders[i].Name}");

                                    // Виведення інформації про кожне повідомлення
                                    foreach (var uid in uids)
                                    {
                                        var message = await mailFolder.GetMessageAsync(uid);
                                        MessagesList.Items.Add($"{message.Date}: {message.Subject} - {new string(message.TextBody?.ToArray())}...");
                                    }
                                }
                            }
                        }
                    }
                    catch(Exception ex) 
                    { 
                        
                    }
                }
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(UserLogin, UserPassword);
            window1.Show();
            this.Close();
        }

        private async void MessagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MessagesList.SelectedItems != null && MessagesList.SelectedItems.Count > 0)
            {
                // Взяти перший вибраний елемент (якщо є)
                var selectedMessage = MessagesList.SelectedItem as string;

                if (selectedMessage != null)
                {
                    // Припустимо, що дані повідомлення мають фіксований формат для прикладу
                    string[] messageParts = selectedMessage.Split('-');

                    if (messageParts.Length >= 3)
                    {
                        DateTxtBlock.Text = messageParts[0].Trim();
                        SubjectTxtBlock.Text = messageParts[1].Trim();
                        MessageTxtBlock.Text = messageParts[2].Trim();
                    }
                }
            }
        }

        private async void CreateNewFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(UserLogin, UserPassword);

                // Створення нової папки з ім'ям "Нова Папка"
                change_folder folderName = new change_folder();

                // Додавання події MyWindow_Closed до folderName
                folderName.Closed += MyWindow_Closed;

                // Показ вікна і очікування його закриття
                folderName.Show();

                // Закриття підключення
                client.Disconnect(true);
            }
        }

        private void MyWindow_Closed(object sender, EventArgs e)
        {
            // Код, який виконується після закриття вікна
            if (sender is change_folder folderName)
            {
                TmpString = folderName.Message;

                // Вивести MessageBox тут, коли значення TmpString вже встановлено
                MessageBox.Show(TmpString);
            }
        }

        private void AddToFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessagesList.SelectedItem != null)
            {
                // Отримати вибраний елемент
                var selectedMessage = MessagesList.SelectedItem as string;
                if (selectedMessage != null)
                {
                    
                }
            }
            else
            {
                MessageBox.Show("Please enter the message.");
            }
        }

        private void RenameFolderBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteFolderBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
