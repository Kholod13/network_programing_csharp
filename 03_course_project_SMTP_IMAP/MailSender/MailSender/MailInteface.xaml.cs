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
using MimeKit;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MailInteface.xaml
    /// </summary>
    public partial class MailInteface : Window
    {
        private string UserLogin { get; set; }
        private string UserPassword { get; set; }
        
        public MailInteface(string userLogin, string userPassword)
        {
            InitializeComponent();
            UserLogin = userLogin;
            UserPassword = userPassword;

            
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate(UserLogin, UserPassword);

                // ------------------------------------------------------------------------------

                // --------------- get all folders

                
                foreach (var item in client.GetFolders(client.PersonalNamespaces[0]))
                {
                    FolderList.Items.Add(item.Name);
                }

                if (FolderList.SelectedItem != null)
                {
                    /*
                    var uids = client.Inbox.Search(SearchQuery.Seen);
                    MessagesList.Items.Add($"Folder");
                    foreach (var uid in uids)
                    {
                        var message = client.Inbox.GetMessage(uid);
                        MessagesList.Items.Add($"{message.Date}: {message.Subject} - {new string(message.TextBody?.Take(10).ToArray())}...");
                    }
                    */
                    var selectedText = (FolderList.SelectedItem as ListBoxItem).Content.ToString();
                    MessageBox.Show($"Ви обрали: {selectedText}");
                }

                // -------------- get all sent messages
                /*
                var folder = client.GetFolder(MailKit.SpecialFolder.Sent);
                folder.Open(FolderAccess.ReadWrite);

                IList<MailKit.UniqueId> uids = folder.Search(SearchQuery.Seen);
                
                foreach (var i in uids)
                {
                    MimeMessage message = folder.GetMessage(i);
                    MessagesList.Items.Add($"{message.Date}: {message.Subject} - {new string(message.TextBody?.Take(10).ToArray())}...");
                };
                */



                //// -------------------- show Inbox 
                /*
                client.Inbox.Open(FolderAccess.ReadOnly);

                Console.WriteLine("--------- Inbox:");
                foreach (var uid in client.Inbox.Search(SearchQuery.All))
                {
                    var m = client.Inbox.GetMessage(uid);
                    // show message details
                    Console.WriteLine($"Mail: {m.Subject} - {new string(m.TextBody.Take(10).ToArray())}...");
                }

                // ---------------------- delete message
                folder.Open(FolderAccess.ReadWrite);
                var id = folder.Search(SearchQuery.All).FirstOrDefault();
                var mail = folder.GetMessage(id);

                Console.WriteLine(mail.Date + " " + mail.Subject);

                folder.AddFlags(new MailKit.UniqueId[] { id }, MessageFlags.Deleted, false);

                folder.Expunge();

                //folder.MoveTo(id, client.GetFolder(SpecialFolder.Junk));  // move to spam
                //folder.AddFlags(id, MessageFlags.Seen, false);            // mark as read
                //folder.MoveTo(id, client.GetFolder(SpecialFolder.Trash)); // delete mail

                Console.WriteLine("Press to exit!");
                Console.ReadKey();
                */
            }
            

        }

        private void FolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate(UserLogin, UserPassword);


                if (FolderList.SelectedItem != null)
                {
                    if (FolderList.SelectedItem == FolderList.Items[0])
                    {
                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadOnly);

                        // Отримання всіх повідомлень з папки "Вхідні"
                        var uids = inbox.Search(SearchQuery.NotSeen);
                        MessagesList.Items.Add("Folder");
                        foreach (var uid in uids)
                        {
                            var message = inbox.GetMessage(uid);
                            MessagesList.Items.Add($"{message.Date}: {message.Subject} - {new string(message.TextBody?.Take(10).ToArray())}...");
                        }
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
    }
}
