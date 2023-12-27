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
using Renci.SshNet.Messages;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MailInteface.xaml
    /// </summary>
    public partial class MailInteface : Window
    {
        private string UserLogin { get; set; }
        private string UserPassword { get; set; }
        List<MainFoledr> folders = new List<MainFoledr>();
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

                // Отримати особистий простір імен клієнта
                var personalNamespace = client.PersonalNamespaces[0];

                // Отримати список всіх папок
                var foldersList = client.GetFolders(personalNamespace);

                List<string> names = new List<string>();
                List<MailKit.SpecialFolder> specialFolders = new List<MailKit.SpecialFolder>();

                // Додати імена папок до списку або вивести їх у консоль
                foreach (var folder in foldersList)
                {
                    FolderList.Items.Add(folder.Name);

                    // Створити об'єкт MainFoledr та додати його до списку folders
                    names.Add(folder.Name);
                }
                // Отримання всіх спеціальних папок
                foreach (MailKit.SpecialFolder specialFolder in Enum.GetValues(typeof(MailKit.SpecialFolder)))
                {
                    // Отримати конкретну спеціальну папку
                    specialFolders.Add(specialFolder);
                }

                for (int i = 0; i < names.Count && i < specialFolders.Count; i++)
                {
                    folders.Add(new MainFoledr { Name = names[i], ServiceName = specialFolders[i] });
                }


                client.Disconnect(true);
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

                                    if (mailFolder != null)
                                    {
                                        await mailFolder.OpenAsync(FolderAccess.ReadWrite);
                                        // Решта коду
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
                                    else
                                    {
                                        MessageBox.Show($"Folder is empty");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
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
                
                // Створення нової папки з ім'ям TmpString
                if (!string.IsNullOrEmpty(inputTxtBox.Text))
                {
                    var personalNamespace = client.PersonalNamespaces[0];
                    var inbox = client.Inbox;

                    // Спробуємо відкрити папку (це також створить папку, якщо її немає)
                    var newFolder = inbox.Create(inputTxtBox.Text, true);
                    FolderList.Items.Add(newFolder.Name);

                    MessageBox.Show($"Folder '{inputTxtBox.Text}' created successfully!");
                }

                // Закриття підключення
                client.Disconnect(true);
            }
            inputTxtBox.Clear();
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
