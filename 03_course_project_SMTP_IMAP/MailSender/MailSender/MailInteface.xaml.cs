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
using MimeKit;

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
                var personalNamespace = client.PersonalNamespaces[0];
                var foldersList = client.GetFolders(personalNamespace);

                List<string> names = new List<string>();
                List<MailKit.SpecialFolder> specialFolders = new List<MailKit.SpecialFolder>();

                foreach (var folder in foldersList)
                {
                    FolderList.Items.Add(folder.Name);
                    names.Add(folder.Name);
                }
                foreach (MailKit.SpecialFolder specialFolder in Enum.GetValues(typeof(MailKit.SpecialFolder)))
                {
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
                                        var uids = await mailFolder.SearchAsync(SearchQuery.All);

                                        MessagesList.Items.Add($"Messages in {folders[i].Name}");

                                        foreach (var uid in uids)
                                        {
                                            var message = await mailFolder.GetMessageAsync(uid);
                                            MessagesList.Items.Add($"{message.Date}: {message.Subject} - {new string(message.TextBody?.ToArray())}...");
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Folder is empty");
                                        break;
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
                var selectedMessage = MessagesList.SelectedItem as string;

                if (selectedMessage != null)
                {
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
                
                if (!string.IsNullOrEmpty(inputTxtBox.Text))
                {
                    var personalNamespace = client.PersonalNamespaces[0];
                    var inbox = client.Inbox;

                    var newFolder = inbox.Create(inputTxtBox.Text, true);
                    FolderList.Items.Add(newFolder.Name);

                    MessageBox.Show($"Folder '{inputTxtBox.Text}' created successfully!");
                    inputTxtBox.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Please input name new folder!");
                }

                client.Disconnect(true);
            }
            inputTxtBox.Clear();
        }
        private async void AddToFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessagesList.SelectedItem != null && !string.IsNullOrEmpty(inputTxtBox.Text))
            {
                // Отримати вибране повідомлення
                var selectedMessage = MessagesList.SelectedItem as string;

                if (selectedMessage != null)
                {
                    using (var client = new ImapClient())
                    {
                        try
                        {
                            client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                            await client.AuthenticateAsync(UserLogin, UserPassword);

                            // Отримати папку, в яку треба додати повідомлення
                            var destinationFolder = client.GetFolder($"inbox/{inputTxtBox.Text}");

                            if (destinationFolder != null)
                            {
                                // Отримати UID повідомлення
                                var uid = GetMessageUid(selectedMessage, client);

                                // Спробувати додати повідомлення в папку
                                destinationFolder.Append(FormatOptions.Default, GetMessageStream(uid, client));

                                MessageBox.Show($"Message added to '{inputTxtBox.Text}' successfully!");
                            }
                            else
                            {
                                MessageBox.Show($"Folder '{inputTxtBox.Text}' not found!");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Обробка помилок IMAP
                            MessageBox.Show($"An error occurred: {ex.Message}");
                        }
                        finally
                        {
                            // Завершення з'єднання
                            client.Disconnect(true);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a message and enter the destination folder.");
            }
        }

        private MailKit.UniqueId GetMessageUid(string selectedMessage, ImapClient client)
        {
            // Реалізуйте логіку для отримання UID повідомлення
            // Цей метод має повертати унікальний ідентифікатор (UID) повідомлення
            // Наприклад, можна використовувати пошук повідомлення за текстом або іншими критеріями

            // Приклад:
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            var searchResults = inbox.Search(SearchQuery.SubjectContains(selectedMessage));

            if (searchResults.Count > 0)
            {
                return searchResults[0];
            }

            // Якщо повідомлення не знайдено, можна повернути UID по замовчуванню або викликати помилку
            return MailKit.UniqueId.MinValue;
        }

        private MimeMessage GetMessageStream(MailKit.UniqueId uid, ImapClient client)
        {
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            var message = inbox.GetMessage(uid);
            return message;
        }

        private async void RenameFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FolderList.SelectedItem != null && !string.IsNullOrEmpty(inputTxtBox.Text))
            {
                using (var client = new ImapClient())
                {
                    try
                    {
                        client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                        await client.AuthenticateAsync(UserLogin, UserPassword);

                        var selectedFolderName = FolderList.SelectedItem as string;

                        var sourceFolder = client.GetFolder($"inbox/{selectedFolderName}");

                        if (sourceFolder != null)
                        { 
                            var newFolderName = inputTxtBox.Text;
                            var destinationFolder = client.GetFolder(newFolderName);

                            if (destinationFolder == null)
                            {
                                var newFolder = sourceFolder.Create(newFolderName, true);
                                destinationFolder = client.GetFolder(newFolderName);
                            }

                            foreach (var uniqueId in sourceFolder.Search(SearchQuery.All))
                            {
                                var message = sourceFolder.GetMessage(uniqueId);
                                destinationFolder.Append(message);
                            }

                            sourceFolder.Delete();
                            FolderList.Items.Remove(sourceFolder);
                            FolderList.Items.Add(newFolderName);

                            MessageBox.Show($"Folder '{selectedFolderName}' renamed to '{newFolderName}' successfully!");
                        }
                        else
                        {
                            MessageBox.Show($"Folder '{selectedFolderName}' not found!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                    finally
                    {
                        client.Disconnect(true);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a folder and enter the new name to rename.");
            }
        }


        private void DeleteFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new ImapClient())
            {
                try
                {
                    client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(UserLogin, UserPassword);

                    if (inputTxtBox.Text != string.Empty)
                    {
                        var folderToDelete = client.GetFolder($"inbox/{inputTxtBox.Text}");

                        if (folderToDelete != null)
                        {
                            folderToDelete.Delete();
                            MessageBox.Show($"Folder '{inputTxtBox.Text}' deleted successfully!");
                            FolderList.Items.Remove(inputTxtBox.Text);
                            inputTxtBox.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show($"Folder '{inputTxtBox.Text}' not found!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }

        }
    }
}
